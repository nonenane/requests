using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

namespace xPosRealiz
{
    public partial class MainForm : Form
    {
        private static List<string> path = new List<string>();
        private static List<int> terminals = new List<int>();
        private static List<long> ids = new List<long>();
        private static List<long> oldids = new List<long>();
        private Boolean flag;
        public static bool можно = true;
        public static bool spravYes = true;
        public static long lastID = 0;
        public static string[] sterms;
        public static bool use;

        public MainForm()
        {
            InitializeComponent();
            setTimeToTimer();




            use = bool.Parse(ConfigurationManager.AppSettings["use"].ToString());
            sterms = ConfigurationManager.AppSettings["terminals"].Split(',');
            DataTable dt = SQL.getLastId();
            foreach (DataRow dr in dt.Rows)
            {
                if (use && sterms.Contains(dr["number"].ToString()))
                //if (dr["number"].ToString() == "58" || dr["number"].ToString() == "56" || dr["number"].ToString() == "3")
                {
                    path.Add(dr["path"].ToString());
                    terminals.Add(Convert.ToInt32(dr["number"].ToString()));
                    ids.Add(Convert.ToInt64(dr["lastID"].ToString()));
                    oldids.Add(Convert.ToInt64(dr["lastID"].ToString()));
                }
                else lastID = Convert.ToInt64(dr["lastID"].ToString());
            }
            timerRealiz.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRealiz.Stop();
        }

        private void timerRealiz_Tick(object sender, EventArgs e)
        {
            //lblSpravTimer.Text = "Проверка начнется через " + (60 - DateTime.Now.TimeOfDay.Seconds) + " сек";
            // sprav
            if (spravYes && !bwSparv.IsBusy && (DateTime.Now.Hour >= 6 || DateTime.Now.Hour < 2))
            {
                bwSparv.RunWorkerAsync();
                spravYes = false;
            }
        }

        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }

        #region Sprav
        private void btnCreate_Click(object sender, EventArgs e)
        {
            spravYes = false;
            btnPause.Enabled = false;
            btnResume.Enabled = true;
            string spravPath = string.Empty;
            Sprav.createSpravFull();
        }

        private void bwSparv_DoWork(object sender, DoWorkEventArgs e)
        {
            DoOnUIThread(delegate ()
            { rtbSprav.Text = ""; });
            string spravPath = string.Empty;
            File.Delete(@"sprav\AinT");
            File.Delete(@"sprav\Ain");
            long newID = SQL.getLastIdSprav();
            if (lastID < newID)
            {
                DoOnUIThread(delegate ()
                {
                    rtbSprav.Text += DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + ": Создание изменений справочника;\n";
                    rtbSprav.SelectionStart = rtbSprav.Text.Length;
                });
                Thread.Sleep(3000);
               // Sprav.createSprav(lastID + 1, false);


               // File.Copy(@"sprav\AInT", @"sprav\" + (lastID + 1).ToString(), true);
                //DoOnUIThread(delegate ()
                //{
                //    rtbSprav.Text += DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + ": Справочник создан;\n";
                //    rtbSprav.SelectionStart = rtbSprav.Text.Length;
                //});
            }

            DataTable dt = SQL.getLastId();
            ids.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                if (use && sterms.Contains(dr["number"].ToString())) ids.Add(Convert.ToInt64(dr["lastID"].ToString()));
            }

            Sprav.initListGrpSettings();
            Sprav.initPromoGoods();

            for (int i = 0; i < path.Count(); i++)
            {
                bool exists = true;
                try
                {
                    bool completedIN = false;
                    Thread t = new Thread(new ThreadStart(delegate ()
                    { Directory.Exists(path[i].ToString() + @"atol\AIn"); }));
                    t.Start();
                    completedIN = t.Join(3000); //half a sec of timeout
                    if (!completedIN) { exists = false; t.Abort(); }
                    t = null;
                }
                catch { exists = false; }

                if (exists && !File.Exists(path[i].ToString() + @"atol\AIn\sprav.txt"))
                {
                    try
                    {
                        #region с 6 утра до 8ми утра
                        int res = Sprav.checkItsAll(path[i], terminals[i]);
                        if (res == 1)
                        {
                            if (SQL.getJSprav(terminals[i]) != ids[i].ToString())
                            {
                                for (long j = oldids[i]; j <= ids[i]; j++) SQL.setJSprav(terminals[i], j);
                            }
                            if (ids[i] < newID)
                            {
                                Sprav.createSprav(ids[i], true);

                                if (File.Exists(path[i].ToString() + @"atol\AIn\AIn"))
                                {
                                    if (File.Exists(path[i].ToString() + @"atol\AIn\AIn" + ids[i])) File.Delete(path[i].ToString() + @"atol\AIn\AIn" + ids[i]);
                                    File.Move(path[i].ToString() + @"atol\AIn\AIn", path[i].ToString() + @"atol\AIn\AIn" + ids[i]);
                                }
                                File.Copy(@"sprav\AInT", path[i].ToString() + @"atol\AIn\AIn");
                                File.Copy(@"sprav\sprav.txt", path[i].ToString() + @"atol\AIn\sprav.txt");

                                SQL.setLastId(terminals[i], newID);
                                oldids[i] = ids[i];

                                DoOnUIThread(delegate ()
                                {
                                    rtbSprav.Text += DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " Касса " + terminals[i] + ": Справочник" + ids[i] + " отправлен;\n";
                                    rtbSprav.SelectionStart = rtbSprav.Text.Length;
                                });
                            }
                        }
                        else if (res == 2)
                        {
                            Sprav.createSprav(oldids[i], true);

                            if (File.Exists(path[i].ToString() + @"atol\AIn\AIn"))
                            {
                                if (File.Exists(path[i].ToString() + @"atol\AIn\AIn" + ids[i])) File.Delete(path[i].ToString() + @"atol\AIn\AIn" + ids[i]);
                                File.Move(path[i].ToString() + @"atol\AIn\AIn", path[i].ToString() + @"atol\AIn\AIn" + ids[i]);
                            }
                            File.Copy(@"sprav\AInT", path[i].ToString() + @"atol\AIn\AIn");
                            File.Copy(@"sprav\sprav.txt", path[i].ToString() + @"atol\AIn\sprav.txt");

                            DoOnUIThread(delegate ()
                            {
                                rtbSprav.Text += DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " Касса " + terminals[i] + ": Справочник" + ids[i] + " отправлен;\n";
                                rtbSprav.SelectionStart = rtbSprav.Text.Length;
                            });
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        DoOnUIThread(delegate ()
                        {
                            rtbSprav.Text += DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + ": Ошибка при отправке файлов;\n";
                            rtbSprav.Text += ex.Message + ";";
                            writeLog(DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + terminals[i].ToString() + " : Ошибка при отправке файлов\n");
                            writeLog(ex.Message);
                            rtbSprav.SelectionStart = rtbSprav.Text.Length;
                        });

                    }
                }
                else
                {
                    rtbSprav.Text += terminals[i].ToString() + " Нет соединения с кассой, либо сетевая папка недоступна\n";
                    writeLog(DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + terminals[i].ToString() + " Нет соединения с кассой, либо сетевая папка недоступна\n");
                }
            }
            if (lastID < newID) lastID++;
            SQL.setLastId(0, lastID);
            if (btnResume.Enabled == false) spravYes = true;
            Thread.Sleep(30000);
        }
        #endregion

        private void writeLog(string message)
        {
            if (!File.Exists(DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt"))
                using (FileStream fs = File.Create(DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt"))
                {
                    fs.Close();
                }

            using (StreamWriter sw = new StreamWriter(DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".txt", true))
            {
                sw.WriteLine(message);
                sw.Close();
            }
        }

        private void bwSparv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            spravYes = false;
            btnPause.Enabled = false;
            btnResume.Enabled = true;
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            spravYes = true;
            btnPause.Enabled = true;
            btnResume.Enabled = false;
        }

        private void bwFullSprav_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < path.Count(); i++)
            {
                bool existsIN = false;
                Thread tIN = new Thread(new ThreadStart(delegate ()
                { existsIN = System.IO.Directory.Exists(path[i] + @"atol\AIn"); })
                 );
                tIN.Start();
                bool completedIN = tIN.Join(3000); //half a sec of timeout
                if (!completedIN) { existsIN = false; tIN.Abort(); }
                tIN = null;
                try
                {
                    if (!existsIN)
                    {
                        DoOnUIThread(delegate ()
                        {
                            rtbSprav.Text += terminals[i] + " : Нет входящей папки Ain;\n";
                            writeLog(DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + terminals[i] + " : Нет входящей папки Ain;\n");
                        });
                        continue;
                    }
                    if (File.Exists(path[i].ToString() + @"atol\AIn\sprav.txt"))
                    {
                        DoOnUIThread(delegate ()
                        {
                            rtbSprav.Text += terminals[i] + " : Предыдущий справочник не залит, либо нет обмена; \n";
                            writeLog(DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + terminals[i] + " : Предыдущий справочник не залит, либо нет обмена; \n");
                        });
                        continue;
                    }
                    File.Copy(@"sprav\FULL", path[i].ToString() + @"atol\AIn\AIn", true);
                    File.Copy(@"sprav\sprav.txt", path[i].ToString() + @"atol\AIn\sprav.txt");
                }
                catch { }
                DoOnUIThread(delegate () { rtbSprav.Text += DateTime.Now.ToString() + " " + terminals[i] + " : Флаг отправлен; \n"; });

            }
            Thread.Sleep(600000);
        }

        private void bwFullSprav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spravYes = true;
            btnPause.Enabled = true;
            btnResume.Enabled = false;
        }

        private void setTimeToTimer()
        {
            int defaultValue = 5000;
            DataTable dtTmp = SQL.getSettings("grpt");

            if (dtTmp != null && dtTmp.Rows.Count > 0)
                try
                {
                    defaultValue = int.Parse(dtTmp.Rows[0]["value"].ToString()) * 1000;
                }
                catch { defaultValue = 5000; }

            timerRealiz.Interval = defaultValue;
        }
    }
}
