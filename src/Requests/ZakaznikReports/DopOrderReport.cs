using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.ToExcelNew;
using Nwuram.Framework.Settings.User;

namespace Requests.ZakaznikReports
{
    public class DopOrderReport
    {
        public static void Show(DateTime date, string department, string tu_group, string sub_group, DataTable data)
        {
            ExcelUnLoad report = new ExcelUnLoad();

            AddHeader(report, date, department, tu_group, sub_group);
            AddTableHeaders(report);

            report.AddMultiValue(data, 8, 1);

            report.SetBorders(7, 1, 7 + data.Rows.Count, 10);
            report.SetCellAlignmentToRight(8, 3, 8 + data.Rows.Count, 9);
            report.Show();
        }

        private static void AddHeader(ExcelUnLoad report, DateTime date, string department, string tu_group, string sub_group)
        {
            report.Merge(1, 1, 1, 3);
            report.AddSingleValue("Дополнительный заказ на " + date.ToShortDateString(), 1, 1);
            report.SetFontBold(1, 1, 1, 1);
            report.SetFontSize(1, 1, 1, 1, 14);

            report.AddSingleValue("Отдел:", 3, 1);
            report.AddSingleValue(department, 3, 2);

            report.AddSingleValue("ТУ группа:", 4, 1);
            report.AddSingleValue(tu_group, 4, 2);

            report.AddSingleValue("Подгруппа:", 5, 1);
            report.AddSingleValue(sub_group, 5, 2);

            report.AddSingleValue("Дата и время выгрузки:", 3, 5);
            report.AddSingleValue(DateTime.Now.ToString(), 3, 6);

            report.AddSingleValue("Выгрузил:", 4, 5);
            report.AddSingleValue(UserSettings.User.FullUsername, 4, 6);

            report.SetFontBold(3, 1, 5, 1);
            report.SetFontBold(3, 5, 4, 5);
        }

        private static void AddTableHeaders(ExcelUnLoad report)
        {
            int rowNum = 7;
            int colNum = 1;

            report.AddSingleValue("EAN", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 20);
            report.AddSingleValue("Наименование", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 45);
            report.AddSingleValue("Ср. расход", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 15);
            report.AddSingleValue("Остаток на утро", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 15);
            report.AddSingleValue("Переучёт", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 15);
            report.AddSingleValue("Текущая реализация", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 25);
            report.AddSingleValue("Примерная реализация", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 25);
            report.AddSingleValue("Факт. заказ", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 15);
            report.AddSingleValue("Доп. заказ", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 15);
            report.AddSingleValue("Вид", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 15);

            report.SetFontBold(rowNum, 1, rowNum, colNum);
            report.SetCellAlignmentToCenter(rowNum, 1, rowNum, colNum);
        }
    }
}
