using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace xPosRealiz
{
    class Sprav
    {
        private static List<int> listGrp = new List<int>();
        private static DataTable dtPromoGoods = new DataTable();
        private static List<string> listPromoGoods = new List<string>();

        public static void initListGrpSettings()
        {
            listGrp.Clear();
            DataTable dtTmp = SQL.getSettings("igr1");

            if (dtTmp != null && dtTmp.Rows.Count > 0)
                foreach (DataRow row in dtTmp.Rows)
                {
                    int idGrp;
                    if (int.TryParse(row["value"].ToString(), out idGrp))
                        listGrp.Add(idGrp);
                }
        }

        public static void initPromoGoods()
        {
            listPromoGoods.Clear();
            dtPromoGoods = SQL.getCatalogPromotionalTovars();
        }

        public static string createSpravFull()
        {
            initListGrpSettings();
            initPromoGoods();
            System.IO.StreamWriter file = null;
            try
            {
                DataTable dtDeps = SQL.getListDeps();
                DataTable dtTovar = SQL.getListTovar();
                DataTable dtGrp = SQL.getListGrp();

                file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\sprav\FULL");

                var grop = from row in dtTovar.AsEnumerable()
                           group row by row.Field<int>("grp") into grp
                           select new
                           {
                               grpID = grp.Key,
                           };

                file.WriteLine("##@@&&");
                file.WriteLine("#");
                file.WriteLine("$$$ADDQUANTITY");

                foreach (DataRow rDep in dtDeps.Rows)
                {
                    file.WriteLine(inserGRP(rDep["id"].ToString(), rDep["name"].ToString(), ""));
                }

                foreach (var r in grop)
                {
                    DataRow[] row = dtGrp.Select("id = " + r.grpID.ToString());
                    if (row.Count() > 0)
                    {
                        DataRow[] rDeps = dtDeps.Select("id = " + row[0]["id_otdel"].ToString());
                        file.WriteLine(inserGRP(row[0]["id"].ToString(), row[0]["cname"].ToString(), rDeps[0]["id"].ToString()));
                    }
                }

                dtTovar.DefaultView.Sort = "grp ASC, name ASC";

                foreach (DataRowView row in dtTovar.DefaultView)
                {
                    file.WriteLine(inserTovar(row));
                }

                if (listPromoGoods.Count > 0)
                {
                    file.WriteLine("");
                    file.WriteLine("");
                    file.WriteLine("");
                    file.WriteLine("$$$ADDASPECTREMAINS");
                    foreach (string str in listPromoGoods)
                    {
                        file.WriteLine($"{str}");
                    }
                }
            }
            catch (Exception ex)
            {
                file.Close();
                File.Delete(Directory.GetCurrentDirectory() + @"\sprav\AIn");
            }
            finally
            {
                file.Close();
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Windows\System32\icacls.exe", Directory.GetCurrentDirectory() + @"\sprav\AIn" + " /grant Все:(F)");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();
                }
                catch (Exception exp)
                {
                }
            }
            return Directory.GetCurrentDirectory() + @"\sprav\AIn";
        }

        public static void createSprav(long last_id, bool all)
        {
            System.IO.StreamWriter file = null;
            
            File.Delete(Directory.GetCurrentDirectory() + @"\sprav\AInT");
            DataTable dtDeps = SQL.getListDeps();
            DataTable dtTovar = SQL.getLastListTovar(last_id, all);
           
            DataTable dtGrp = SQL.getListGrp();
            try
            {
                file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\sprav\AInT");

                var grop = from row in dtTovar.AsEnumerable()
                           group row by row.Field<int>("grp") into grp
                           select new
                           {
                               grpID = grp.Key,
                           };

                file.WriteLine("##@@&&");
                file.WriteLine("#");
                file.WriteLine("$$$ADDQUANTITY");

                foreach (DataRow rDep in dtDeps.Rows)
                {
                    file.WriteLine(inserGRP(rDep["id"].ToString(), rDep["name"].ToString(), ""));
                }

                foreach (var r in grop)
                {
                    DataRow[] row = dtGrp.Select("id = " + r.grpID.ToString());
                    if (row.Count() > 0)
                    {
                        DataRow[] rDeps = dtDeps.Select("id = " + row[0]["id_otdel"].ToString());
                        file.WriteLine(inserGRP(row[0]["id"].ToString(), row[0]["cname"].ToString(), rDeps[0]["id"].ToString()));
                    }
                }

                dtTovar.DefaultView.Sort = "grp ASC, name ASC";

                foreach (DataRowView row in dtTovar.DefaultView)
                {
                    file.WriteLine(inserTovar(row));
                }
            }
            catch (Exception ex)
            {
                file.Close();
            }
            finally
            {
                file.Close();
            }
            
        }

        public static int checkItsAll(string path, int terminal)
        {
            string fileAIn = path.ToString() + @"atol\AIn\AIn";
            string fileSprav = path.ToString() + @"atol\AIn\sprav.txt";
            string line;
            if (!File.Exists(fileSprav))
            {
                if (File.Exists(fileAIn))
                {
                    System.IO.StreamReader file = null;
                    try
                    {
                        file = new System.IO.StreamReader(fileAIn);
                        file.ReadLine();
                        line = file.ReadLine();
                        if (line.Contains("@"))
                        {
                            return 1;
                        }
                        else return 0;
                    }
                    catch { return 2; }
                    finally
                    {
                        if (file != null) file.Close();
                    }
                }
                else return 2;
            }
            else return 0;
        }

        public static void isGetSprav(string path, int terminal)
        {
            System.IO.StreamReader file = null;
            System.IO.StreamReader fileOut = null;
            string str = string.Empty;
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(path + @"atol\AOut");
                FileInfo[] fInfo = dInfo.GetFiles();
                string inf = fInfo.FirstOrDefault(x => x.Name.Contains("LoadResult") == true).Name;
                fileOut = new System.IO.StreamReader(path + @"atol\AOut\" + inf);
                str += fileOut.ReadLine();
                fileOut.ReadLine();
                str += " " + fileOut.ReadLine();
                fileOut.Close();
                fileOut = null;
            }
            catch (FileNotFoundException ex)
            {
                str += " Ошибка :" + ex.Message;
            }
            catch (Exception ex)
            {
                str += " Ошибка :" + ex.Message;
            }
            try
            {
                file = new System.IO.StreamReader(path + @"atol\AIn\AIn");
                file.ReadLine();
                str += file.ReadLine();
                file.Close();
                file = null;
            }
            catch (FileNotFoundException ex)
            {
                str += " Ошибка :" + ex.Message;
            }
            catch (Exception ex)
            {
                str += " Ошибка :" + ex.Message;
            }
            //SQL.setJSprav(terminal, str);
        }

        private static string inserGRP(string id, string name, string idParent)
        {
            string str = id + ";;" + name + ";" + name + ";;;;;;;;;;;;" + idParent + ";0;" + id;
            return str;
        }

        private static string inserTovar(DataRowView row)
        {
            string id_tovar = row["id_tovar"].ToString();
            string ean = row["ean"].ToString();
            string name = row["name"].ToString().Replace(";", " ");
            string price = row["price"].ToString();
            string grp = row["grp"].ToString();
            string kodVVO = row["kodVVO"].ToString();
            string firm = row["firm"].ToString();
            string id_post = row["id_post"].ToString();
            string weight = row["ean"].ToString().Trim().Length == 4 ? "1" : "0";

            string[] goods = ConfigurationManager.AppSettings["weightGood"].Split(',');
            if (goods.Contains(row["ean"].ToString().Trim()))
                weight = "1";

            string tax = string.Empty;
            switch (row["tax"].ToString())
            {
                case "18": tax = "1"; break;
                case "20": tax = "1"; break;
                case "10": tax = "2"; break;
                default:
                    {
                        if (!File.Exists(DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt"))
                            using (FileStream fs = File.Create(DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt"))
                            {
                                fs.Close();
                            }

                        using (StreamWriter sw = new StreamWriter(DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt",true))
                        {
                            sw.WriteLine("------------------error");
                            sw.WriteLine("id_tovar = " + id_tovar + ";ean = " + ean + ";name = " + name + ";price = " + price + ";tax = " + row["tax"].ToString());
                            sw.WriteLine("------------------");
                            sw.Close();

                        }
                        tax = "3"; break;
                    }
            }
            string _vvo = "0"; //55
            string _vvo56 = "1";

            //if (row["grp"].ToString().Equals("801") || row["grp"].ToString().Equals("802") || row["grp"].ToString().Equals("899"))
            //{
            //    _vvo = "1";
            //    _vvo56 = "1";
            //}
            if (row["grp"].ToString().Equals("714") )
            {
                _vvo = "5";
            }

            if(listGrp.Contains((int)(row["grp"])))
                 _vvo = "5";

            string value_11 = "0";

            if (dtPromoGoods != null && dtPromoGoods.Rows.Count > 0)
            {

                //if (((string)row["id_tovar"]).Equals("104796"))
                //{ }

                EnumerableRowCollection<DataRow> rowCollect = dtPromoGoods.AsEnumerable()
                    .Where(r => r.Field<int>("id_tovar") == int.Parse(row["id_tovar"].ToString()));
                if (rowCollect.Count() > 0)
                {
                    value_11 = "000000000002";
                    string _tmp = $"{row["id_tovar"]};;000000000001;{rowCollect.First()["SalePrice"]};;;";
                    listPromoGoods.Add(_tmp);
                }
            }

            // должно быть 57 полей
            string str = id_tovar + ";" + ean + ";" + name + ";" + name + ";" + price + ";1000;0;" + weight +
                ";0;0;"+ value_11 + ";0;1;1;0;" + grp + ";1;0;;" + firm + ";;" +
                ";" + tax + ";;;" + id_post + ";;;;;;;;;;;;;;;;;;;;;;;;;;;" + kodVVO.Trim() + ";;" + _vvo + ";" + _vvo56 + ";40.0";
            return str;
        }

    }
}
