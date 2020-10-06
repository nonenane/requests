using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.ToExcelNew;
using Nwuram.Framework.Settings.User;

namespace Requests.ZakaznikReports
{
    public class InventoryReport
    {
        public static void Show(DataTable data)
        {
            ExcelUnLoad report = new ExcelUnLoad();
            AddHeader(report);
            AddTableHeaders(report);

            report.AddMultiValue(data, 6, 1);
            report.SetBorders(5, 1, 5 + data.Rows.Count, 3);
            report.SetCellAlignmentToRight(6, 3, 6 + data.Rows.Count, 3);
            report.Show();
        }

        private static void AddHeader(ExcelUnLoad report)
        {
            report.AddSingleValue("Данные по переучёту", 1, 2);
            report.SetFontBold(1, 2, 1, 2);
            report.SetFontSize(1, 2, 1, 2, 16);

            report.AddSingleValue("Дата и время выгрузки:", 3, 1);
            report.AddSingleValue(DateTime.Now.ToString(), 3, 2);

            report.AddSingleValue("Выгрузил:", 4, 1);
            report.AddSingleValue(UserSettings.User.FullUsername, 4, 2);
            report.SetFontBold(3, 1, 4, 1);
        }

        private static void AddTableHeaders(ExcelUnLoad report)
        {
            int rowNum = 5;
            int colNum = 1;

            report.AddSingleValue("EAN", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 25);
            report.AddSingleValue("Наименование", rowNum, colNum++);
            report.SetColumnWidth(rowNum, colNum - 1, rowNum, colNum - 1, 40);
            report.AddSingleValue("Переучёт", rowNum, colNum++);

            report.SetFontBold(rowNum, 1, rowNum, colNum - 1);
            report.SetCellAlignmentToCenter(rowNum, 1, rowNum, colNum - 1);
        }
    }
}
