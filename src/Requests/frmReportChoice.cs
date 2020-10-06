using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.ToExcel;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.ToExcelNew;

namespace Requests
{
    public partial class frmReportChoice : Form
    {
        DataGridView grid; 
        string department; 
        DateTime dateStart; 
        DateTime dateFinish;

        public frmReportChoice(DataGridView _grid, string _department, DateTime _dateStart, DateTime _dateFinish)
        {
            grid = _grid;
            department = _department;
            dateStart = _dateStart;
            dateFinish = _dateFinish;
            InitializeComponent();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (rbRequestList.Checked)
            {
                RequestListToExcel(grid, department, dateStart, dateFinish);                
            }

            if (rbPostData.Checked)
            {
                PostDataToExcel(grid, department, dateStart, dateFinish);                
            }

            //this.Close();
        }

        private void RequestListToExcel(DataGridView grid, string department, DateTime dateStart, DateTime dateFinish)
        {
            Config.curDate = Config.hCntMain.GetCurDate(true);
            string cellformat;
            ExcelUnLoad report = new ExcelUnLoad();
           // HandmadeReport report = new HandmadeReport();
            DataTable dtRep = Config.GetDataTableFromGridGroupByIdPost(grid);

            report.AddSingleValue("Список заявок", 1, 1);

            report.AddSingleValue("Выгрузил: " + UserSettings.User.FullUsername, 3, 1);
            report.AddSingleValue("Дата: " + Config.curDate, 3, 4);

            report.AddSingleValue("Отдел: " + department, 5, 1);

            report.AddSingleValue("Период с: " + dateStart.ToString("dd.MM.yyyy") + " по " + dateFinish.ToString("dd.MM.yyyy"), 5, 4);

            int rowStart = 7;

            report.AddMultiValue(dtRep, rowStart, 1);
            report.SetFontBold(1, 1, rowStart, dtRep.Columns.Count);
            report.SetRowHeight(rowStart, 1, rowStart, dtRep.Columns.Count, 30);

            int i = 1;
            int columnIndex = 0;
            foreach (DataGridViewColumn dgvCol in grid.Columns)
            {
                columnIndex = Config.getExcelColIndex(grid, dgvCol.Name);

                if (dgvCol.Visible && !dgvCol.GetType().Equals(typeof(DataGridViewCheckBoxColumn)))
                {
                    report.SetColumnWidth(1, i, 1, i + 1, dgvCol.Width / 6);
                    i++;

                    if (dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
                    {
                        report.SetCellAlignmentToCenter(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex);
                    }

                    if (dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                    {
                        report.SetCellAlignmentToRight(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex);
                    }

                    if (dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft
                       || dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet)
                    {
                        report.SetCellAlignmentToLeft(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex);
                    }

                    cellformat = dgvCol.DefaultCellStyle.Format;
                    if (cellformat.Length != 0
                        && cellformat.Substring(0, 1) == "N")
                    {
                        report.SetFormat(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex, "0,".PadRight(2 + int.Parse(cellformat.Substring(1, cellformat.Length - 1)), '0').Trim(','));
                    }
                }
            }

            report.SetCellAlignmentToCenter(rowStart, 1, rowStart, dtRep.Columns.Count);
            report.SetWrapText(rowStart, 1, rowStart, dtRep.Columns.Count);
            report.SetFormat(rowStart, Config.getExcelColIndex(grid, "id_req"), dtRep.Rows.Count + rowStart, Config.getExcelColIndex(grid, "id_req"), "###########");
            report.SetFormat(rowStart, Config.getExcelColIndex(grid, "reqDate"), dtRep.Rows.Count + rowStart, Config.getExcelColIndex(grid, "reqDate"), "ДД.ММ.ГГГГ");
            report.SetBorders(rowStart, 1, dtRep.Rows.Count + rowStart - 1, dtRep.Columns.Count);
            report.Show();

            Config.curDate = Config.curDate.Date;
        }

        private void PostDataToExcel(DataGridView grid, string department, DateTime dateStart, DateTime dateFinish)
        {
            Config.curDate = Config.hCntMain.GetCurDate(true);
            
            ExcelUnLoad report = new ExcelUnLoad();
            
            DataTable dtRep = Config.GetDataTableFromGridGroupByDateAndPost(grid);            

            report.AddSingleValue("Список заявок", 1, 1);

            report.AddSingleValue("Выгрузил: " + UserSettings.User.FullUsername, 3, 1);
            report.AddSingleValue("Дата: " + Config.curDate, 3, 3);
            report.AddSingleValue("Отдел: " + department, 5, 1);
            report.AddSingleValue("Период с: " + dateStart.ToString("dd.MM.yyyy") + " по " + dateFinish.ToString("dd.MM.yyyy"), 5, 3);

            int rowStart = 7;

            report.AddSingleValue("Поставщик / Дата", rowStart, 1);
            report.AddSingleValue("Вес", rowStart, 2);
            report.AddSingleValue("Кол. кор.", rowStart, 3);

            string R_req_date = "";
            int curRow = 8;

            for (int t = 0; dtRep.Rows.Count > t; t++)
            {
                //если первая запись
                if (t == 0)
                {
                    R_req_date = dtRep.Rows[t]["req_date"].ToString();
                    report.AddSingleValue(DateTime.Parse(dtRep.Rows[t]["req_date"].ToString()).ToShortDateString(), curRow, 1);
                    report.SetCellAlignmentToCenter(curRow, 1, curRow, 1);
                    report.SetFormat(curRow, 1, curRow, 1, "ДД.ММ.ГГГГ");
                    curRow++;

                    report.AddSingleValue(dtRep.Rows[t]["post_name"].ToString(), curRow, 1);
                    report.AddSingleValue(dtRep.Rows[t]["weight"].ToString(), curRow, 2);
                    report.AddSingleValue(dtRep.Rows[t]["kolkor"].ToString(), curRow, 3);
                    report.SetCellAlignmentToRight(curRow, 2, curRow, 3);
                    curRow++;
                }
                else
                {
                    //если дата новой строки отличается от добавленной ранее
                    if (R_req_date != dtRep.Rows[t]["req_date"].ToString())
                    {
                        R_req_date = dtRep.Rows[t]["req_date"].ToString();
                        report.AddSingleValue(DateTime.Parse(dtRep.Rows[t]["req_date"].ToString()).ToShortDateString(), curRow, 1);
                        report.SetCellAlignmentToCenter(curRow, 1, curRow, 1);
                        report.SetFormat(curRow, 1, curRow, 1, "ДД.ММ.ГГГГ");
                        curRow++;
                    }

                    report.AddSingleValue(dtRep.Rows[t]["post_name"].ToString(), curRow, 1);
                    report.AddSingleValue(dtRep.Rows[t]["weight"].ToString(), curRow, 2);
                    report.AddSingleValue(dtRep.Rows[t]["kolkor"].ToString(), curRow, 3);
                    report.SetCellAlignmentToRight(curRow, 2, curRow, 3);
                    curRow++;                        
                }
            }

            report.SetFontBold(1, 1, rowStart, dtRep.Columns.Count);

            //report.SetFormat(rowStart + 1, 3, curRow - 1, 3, "##########0");
            //report.SetFormat(rowStart + 1, 2, curRow - 1, 2, "########.0000");
            report.SetColumnWidth(1, 1, 1, 1, 50);

            report.SetBorders(rowStart, 1, curRow - 1, 3);
            
            report.Show();

            Config.curDate = Config.curDate.Date;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
