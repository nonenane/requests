using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.ToExcelNew;
using Nwuram.Framework.Settings.User;

namespace Requests.ZakaznikReports
{
    public class OrderReport
    {
        public static void Show(DateTime date, string department, string tu_group, string sub_group, DataTable order_headers, DataTable order_bodies)
        {
            ExcelUnLoad report = new ExcelUnLoad();
            AddHeader(report, date, department, tu_group, sub_group);
            AddMainTableHeaders(report, 7);
            int rowNum = 8;

            List<int> id_tovars = order_headers.AsEnumerable().Select(r => Convert.ToInt32(r["id"])).Distinct().ToList<int>();
            foreach (int id_tovar in id_tovars)
            {
                DataRow[] tovar_rows = order_headers.Select("id = " + id_tovar.ToString());
                if (tovar_rows.Length > 0)
                {
                    DataRow[] body_rows = order_bodies.Select("id_order = " + tovar_rows[0]["id_order"].ToString());

                    if (body_rows.Length > 0)
                    {
                        report.Merge(rowNum, 1, rowNum + body_rows.Length + 1, 1);
                    }
                    report.AddSingleValue(tovar_rows[0]["ean"].ToString(), rowNum, 1);

                    if (body_rows.Length > 0)
                    {
                        report.Merge(rowNum, 2, rowNum + body_rows.Length + 1, 2);
                    }
                    report.AddSingleValue(tovar_rows[0]["cname"].ToString(), rowNum, 2);

                    report.SetCellAlignmentToCenter(rowNum, 1, rowNum + tovar_rows.Length + 1, 2);
                    report.SetCellAlignmentToJustify(rowNum, 1, rowNum + tovar_rows.Length + 1, 2);
                    report.SetWrapText(rowNum, 2, rowNum + tovar_rows.Length + 1, 2);

                    int colNum = 3;
                    report.AddSingleValue(tovar_rows[0]["sred_rashod"].ToString(), rowNum, colNum++);
                    report.AddSingleValue(tovar_rows[0]["ost_on_date"].ToString(), rowNum, colNum++);
                    report.AddSingleValue(tovar_rows[0]["inventory"].ToString(), rowNum, colNum++);
                    report.AddSingleValue(tovar_rows[0]["diff"].ToString(), rowNum, colNum++);
                    report.AddSingleValue(tovar_rows[0]["plan_realiz"].ToString(), rowNum, colNum++);
                    report.AddSingleValue(Convert.ToDecimal(tovar_rows[0]["zakaz"]).ToString("N0"), rowNum, colNum++);
                    report.AddSingleValue(Convert.ToDecimal(tovar_rows[0]["perezatarka"]).ToString("N3"), rowNum, colNum++);
                    report.AddSingleValue(Convert.ToDecimal(tovar_rows[0]["perezatarka_zal"]).ToString("N3"), rowNum, colNum++);
                    report.AddSingleValue(Convert.ToDecimal(tovar_rows[0]["zakaz_manager"]).ToString("N0"), rowNum, colNum++);
                    report.AddSingleValue(Convert.ToDecimal(tovar_rows[0]["spisanie"]).ToString("N0"), rowNum, colNum++);
                    report.AddSingleValue(tovar_rows[0]["sub_group"].ToString(), rowNum, colNum++);
                    report.AddSingleValue(tovar_rows[0]["rcena"].ToString(), rowNum, colNum++);

                    report.SetCellAlignmentToRight(rowNum, 3, rowNum, colNum - 2);

                    rowNum++;
                    if (body_rows.Length > 0)
                    {
                        AddDetailsTableHeaders(report, rowNum);
                        rowNum++;
                        foreach (DataRow tovar_row in body_rows)
                        {
                            colNum = 3;

                            report.Merge(rowNum, colNum, rowNum, colNum + 1);
                            report.AddSingleValue(tovar_row["post_name"].ToString(), rowNum, colNum);
                            colNum += 2;

                            report.AddSingleValue(tovar_row["fact_netto"].ToString(), rowNum, colNum++);

                            report.Merge(rowNum, colNum, rowNum, colNum + 1);
                            report.AddSingleValue(tovar_row["caliber"].ToString(), rowNum, colNum);
                            colNum += 2;

                            report.AddSingleValue(tovar_row["zcena"].ToString(), rowNum, colNum++);

                            report.Merge(rowNum, colNum, rowNum, colNum + 1);
                            report.AddSingleValue(tovar_row["subject_name"].ToString(), rowNum, colNum);
                            colNum += 2;

                            report.AddSingleValue(tovar_row["id_trequest"].ToString(), rowNum, colNum++);

                            report.SetCellAlignmentToRight(rowNum, 5, rowNum, 5);
                            report.SetCellAlignmentToRight(rowNum, 8, rowNum, 8);
                            report.SetCellAlignmentToCenter(rowNum, 11, rowNum, 11);
                            rowNum++;
                        }
                    }
                }
            }

            report.SetBorders(7, 1, rowNum - 1, 14);
            report.Show();
        }

        public static void AddHeader(ExcelUnLoad report, DateTime date, string department, string tu_group, string sub_group)
        {
            report.AddSingleValue("Заказник за " + date.ToShortDateString(), 1, 2);
            report.SetFontBold(1, 2, 1, 2);

            report.AddSingleValue("Отдел:", 2, 1);
            report.AddSingleValue(department, 2, 2);

            report.AddSingleValue("ТУ группа:", 3, 1);
            report.AddSingleValue(tu_group, 3, 2);

            report.AddSingleValue("Подгруппа:", 4, 1);
            report.AddSingleValue(sub_group, 4, 2);

            report.AddSingleValue("Дата и время выгрузки:", 2, 5);
            report.AddSingleValue(DateTime.Now.ToString(), 2, 6);

            report.AddSingleValue("Выгрузил:", 3, 5);
            report.AddSingleValue(UserSettings.User.FullUsername, 3, 6);

            report.SetFontBold(2, 1, 4, 1);
            report.SetFontBold(2, 5, 3, 5);
        }

        public static void AddMainTableHeaders(ExcelUnLoad report, int rowNum)
        {
            int colNum = 1;

            report.AddSingleValue("EAN", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Наименование", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 40);
            report.AddSingleValue("Средний расход", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Остаток на утро", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Переучёт", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Разница", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("План. реализ.", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Расчёт заказа", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Перезатарка", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Перезатарка зал", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Заказ менеджера", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Списание", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Вид", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Цена прод.", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);

            report.SetFontBold(rowNum, 1, rowNum, colNum);
            report.SetCellAlignmentToCenter(rowNum, 1, rowNum, colNum);
        }

        public static void AddDetailsTableHeaders(ExcelUnLoad report, int rowNum)
        {
            int colNum = 3;

            report.Merge(rowNum, colNum, rowNum, colNum + 1);
            report.AddSingleValue("Поставщик", rowNum, colNum);
            colNum += 2;

            report.AddSingleValue("Факт. нетто", rowNum, colNum++);

            report.Merge(rowNum, colNum, rowNum, colNum + 1);
            report.AddSingleValue("Калибр", rowNum, colNum);
            colNum += 2;

            report.AddSingleValue("Цена закупки", rowNum, colNum++);

            report.Merge(rowNum, colNum, rowNum, colNum + 1);
            report.AddSingleValue("Страна/субъект", rowNum, colNum);
            colNum += 2;

            report.AddSingleValue("Номер заявки", rowNum, colNum++);

            report.SetFontBold(rowNum, 1, rowNum, colNum);
            report.SetCellAlignmentToCenter(rowNum, 1, rowNum, colNum);
        }
    }
}
