using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using OfficeOpenXml.Style;
using Nwuram.Framework.Data;
using Nwuram.Framework.Logging;

namespace ViewSalesPromProducts
{
    class MainCatalog
    {
        public MainCatalog()
        {

        }

        public DataTable dtOne;
        DataTable dtTwoOld;
        public DataTable dtTwo;

        private int otdel;
        public int Otdel { get { return otdel; } set { otdel = value; setFilter(); } }

        private int tygroup;
        public int tyGroup { get { return tygroup; } set { tygroup = value; setFilter(); } }

        private int invgroup;
        public int invGroup { get { return invgroup; } set { invgroup = value; setFilter(); } }

        private string searchean;
        public string searchEan { get { return (searchean == null) ? "" : searchean; } set { searchean = value; setFilter(); } }

        private string searchnametovar;
        public string searchNameTovar { get { return searchnametovar == null ? "" : searchnametovar; } set { searchnametovar = value; setFilter(); } }

        private string searchean2;
        public string searchEan2 { get { return searchean2 == null ? "" : searchean2; } set { searchean2 = value; setFilter(); } }

        private string searchnametovar2;
        public string searchNameTovar2 { get { return searchnametovar2 == null ? "" : searchnametovar2; } set { searchnametovar2 = value; setFilter(); } }

        private string Filter
        {
            get
            {
                return setFilterEan()
                      + (setFilterEan().Length > 1 ? " AND " : " ")
                      + setFilterNameTovar()
                      + (setFilterNameTovar().Length > 1 ? " AND " : " ")
                      + setFilterOtdel()
                      + (setFilterOtdel().Length > 1 ? " AND " : " ")
                      + setFilterTYGroup()
                      + (setFilterTYGroup().Length > 1 ? " AND " : " ")
                      + setFilterInvGroup()
                      + (setFilterInvGroup().Length > 1 ? " AND " : " ")
                      + "ean is not null ";


            }
        }
        private string Filter2
        {
            get
            {
                return setFilterEan2()
                      + (setFilterEan2().Length > 1 ? " AND " : " ")
                      + setFilterNameTovar2()
                      + (setFilterNameTovar2().Length > 1 ? " AND " : " ")
                      + setFilterOtdel()
                      + (setFilterOtdel().Length > 1 ? " AND " : " ")
                      + setFilterTYGroup()
                      + (setFilterTYGroup().Length > 1 ? " AND " : " ")
                      + setFilterInvGroup()
                      + (setFilterInvGroup().Length > 1 ? " AND " : " ")
                      + "ean is not null ";


            }
        }

        public void getData()
        {
            dtOne = Config.connectMain.getAllTovar();
            dtTwo = Config.connectMain.getPromotionalTovar();
            dtTwoOld = dtTwo.Copy();
            setFilter();
        }

        private void setFilter()
        {
            if (Config.CheckDtOnFull(dtOne))
                dtOne.DefaultView.RowFilter = Filter;
            if (Config.CheckDtOnFull(dtTwo))
                dtTwo.DefaultView.RowFilter = Filter2;

        }

        private string setFilterEan()
        {
            if (searchEan.Length > 0) return "ean LIKE \'%" + searchEan + "%\'";
            else return "";
        }

        private string setFilterNameTovar()
        {
            if (searchNameTovar.Length > 0) return "NameTovar LIKE \'%" + searchNameTovar + "%\'";
            else return "";
        }
        private string setFilterEan2()
        {
            if (searchEan2.Length > 0) return "ean LIKE \'%" + searchEan2 + "%\'";
            else return "";
        }

        private string setFilterNameTovar2()
        {
            if (searchNameTovar2.Length > 0) return "NameTovar LIKE \'%" + searchNameTovar2 + "%\'";
            else return "";
        }

        private string setFilterOtdel()
        {
            return Otdel == 0 ? "" : "CONVERT(otdel, 'System.Int32') = " + Otdel;
        }

        private string setFilterTYGroup()
        {
            return tygroup == 0 ? "" : "CONVERT(TYGRoup, 'System.Int32') = " + tyGroup;
        }

        private string setFilterInvGroup()
        {
            return invGroup == 0 ? "" : "CONVERT(invGroup, 'System.Int32') = " + invGroup;
        }

        public void AddAllRow(DataTable dt)
        {
            if (Config.CheckDtOnEmpty(dt)) return;
            foreach (DataRow row in dt.Rows)
            {
                AddRow(row["id_tovar"].ToString());
            }
            dtTwo.DefaultView.RowFilter = Filter2;
        }

        public void DeleteAllRow(DataTable dt)
        {
            if (Config.CheckDtOnEmpty(dt)) return;
            foreach (DataRow row in dt.Rows)
            {
                DeleteRow(row["id_tovar"].ToString());
            }
        }

        public void AddRow(string id)
        {
            if (Config.CheckDtOnFull(dtOne))
            {
                DataRow[] r = dtTwo.Select("id_tovar = " + id);
                foreach (DataRow temp in r)
                    return;
                DataRow[] row = dtOne.Select("id_tovar = " + id);
                foreach (DataRow temp in row)
                    dtTwo.ImportRow(temp);
            }
            if (Config.CheckDtOnFull(dtTwo))
                dtTwo.DefaultView.RowFilter = Filter2;
        }

        public void DeleteRow(string id)
        {
            if (Config.CheckDtOnFull(dtTwo))
            {
                for (int i = dtTwo.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtTwo.Rows[i];
                    if (dr["id_tovar"].ToString() == id.ToString())
                        dr.Delete();
                }
                dtTwo.AcceptChanges();
            }
        }

        public void Save()
        {
            DataTable dt = Config.getDifferentRecords(dtTwo, dtTwoOld, 1);
            DataTable dt2 = Config.getDifferentRecords(dtTwo, dtTwoOld, 2);
            if (Config.CheckDtOnFull(dt) || Config.CheckDtOnFull(dt2))
            {
                if (Config.CheckDtOnFull(dt)) Logging.StartFirstLevel(1322);
                foreach (DataRow row in dt.Rows)
                {
                    Config.connectMain.setDeletePromotionalTovar((int)row["id_tovar"], 1);
                    Logging.Comment("Добавлен товар. Id: " + (int)row["id_tovar"] + "; Наименование: " + row["NameTovar"].ToString());
                }
                Logging.StopFirstLevel();
                if (Config.CheckDtOnFull(dt2)) Logging.StartFirstLevel(1323);
                foreach (DataRow row in dt2.Rows)
                {
                    Config.connectMain.setDeletePromotionalTovar((int)row["id_tovar"], 2);
                    Logging.Comment("Удален товар. Id: " + (int)row["id_tovar"] + "; Наименование: " + row["NameTovar"].ToString());
                }
                Logging.StopFirstLevel();
                MessageBox.Show("Сохранено", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtTwo = Config.connectMain.getPromotionalTovar();
                dtTwoOld = dtTwo.Copy();
                setFilter();
            }
        }

        public void Print(DataTable dt, string otdel, string tyGroup, string invGroup)
        {
            if (Config.CheckDtOnEmpty(dt))
            {
                MessageBox.Show("Нет данных для печати", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dt.Columns.Remove("id_tovar");
            // dt.Columns.Remove("isRezerv");
            dt.Columns.Remove("otdel");
            dt.Columns.Remove("TYGRoup");
            dt.Columns.Remove("invGroup");

            FileInfo newFile = new FileInfo(Application.StartupPath + "\\Templates\\ReportCatalog.xlsx");

            ExcelPackage pck = new ExcelPackage(newFile);
            var ws = pck.Workbook.Worksheets[1];

            ws.Cells[3, 1].Value = "Объект: " + (ConnectionSettings.GetServer().IndexOf("K21") > 0 ? "K21" : "X14");
            ws.Cells[4, 1].Value = "Отдел: " + otdel;
            ws.Cells[5, 1].Value = "Т/У группа: " + tyGroup;
            ws.Cells[6, 1].Value = "Инв. группа: " + invGroup;
            ws.Cells[8, 1].Value = "Выгрузил: " + UserSettings.User.FullUsername;
            ws.Cells[9, 1].Value = "Дата выгрузки: " + DateTime.Now.ToShortDateString();

            int row = 12;

            for (int c = 0; c < 2; c++)
                for (int r = 0; r < dt.Rows.Count; r++)
                    ws.Cells[row + r, 1 + c].Value = dt.Rows[r][c].ToString();
            ws.Cells[row, 2, row + dt.Rows.Count, 2].Style.WrapText = true;

            ws.Cells[row, 1, row + dt.Rows.Count - 1, dt.Columns.Count - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[row, 1, row + dt.Rows.Count - 1, dt.Columns.Count - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[row, 1, row + dt.Rows.Count - 1, dt.Columns.Count - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[row, 1, row + dt.Rows.Count - 1, dt.Columns.Count - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                if (int.Parse(dt.Rows[r]["isRezerv"].ToString()) == 1)
                {
                    ws.Cells[row + r, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[row + r, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[row + r, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue);
                    ws.Cells[row + r, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue);
                }
            }

            ws.Cells[row + dt.Rows.Count + 1, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells[row + dt.Rows.Count + 1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue);
            ws.Cells[row + dt.Rows.Count + 1, 2].Value = "Резервные товары";
            try
            {
                pck.SaveAs(new FileInfo(Application.StartupPath + "\\ReportCatalog.xlsx"));
                System.Diagnostics.Process.Start(Application.StartupPath + "\\ReportCatalog.xlsx");
            }
            catch { }
        }
    }
}
