using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;

namespace ViewSalesPromProducts
{
    public partial class frmMain : Form
    {
        DataTable dtMain = new DataTable();
        char[] special_symbols = new char[] { '%', '*' };
        bool isPrintGoog = false;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = "\"" + Nwuram.Framework.Settings.Connection.ConnectionSettings.ProgramName + "\", \"" + Nwuram.Framework.Settings.User.UserSettings.User.Status + "\", " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername + "";
            toolStripStatusLabel1.Text = ConnectionSettings.GetServer() + " " + ConnectionSettings.GetDatabase();
            dtpDateStart.Value = dtpDateStart.Value.AddDays(-3);
            this.dgvMain.AutoGenerateColumns = false;
            btnSettingProducts.Visible = !new List<string>(new string[] { "КД" }).Contains(UserSettings.User.StatusCode);
            LoadOtdel();
        }

        private void LoadOtdel()
        {
            DataTable dt = Config.connectMain.getDep();
            dt.Rows.Add(0, "_Все отделы");
            dt.DefaultView.Sort = "id ASC";
            cmbOtdel.DataSource = dt;
            cmbOtdel.ValueMember = "id";
            cmbOtdel.DisplayMember = "name";
            cmbOtdel.SelectedValue = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDateStart.Value > dtpDateEnd.Value)
                dtpDateStart.Value = dtpDateEnd.Value;

        }

        private void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDateEnd.Value < dtpDateStart.Value)
                dtpDateEnd.Value = dtpDateStart.Value;
        }
        private void update()
        {
            Cursor.Current = Cursors.WaitCursor;
            dtMain = Config.connectMain.getTablePromotionalTovar(dtpDateStart.Value.Date, dtpDateEnd.Value.Date.AddDays(1));
            dgvMain.DataSource = dtMain;
            Cursor.Current = Cursors.Default;
            if (dtMain != null ? dtMain.Rows.Count > 0 ? false : true : true)
                MessageBox.Show("Нет данных для выгрузки.", "Вывод данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            setFilter();
            enabledButton();
        }

        private void btnSettingProducts_Click(object sender, EventArgs e)
        {
            frmSettingProducts frmSettingProducts = new frmSettingProducts();
            frmSettingProducts.ShowDialog();
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            FileInfo newFile = new FileInfo(Application.StartupPath + "\\Templates\\Report.xlsx");

            ExcelPackage pck = new ExcelPackage(newFile);
            var ws = pck.Workbook.Worksheets[1];

            ws.Cells[3, 2].Value = dtpDateStart.Value.ToShortDateString() + " - " + dtpDateEnd.Value.ToShortDateString();
            ws.Cells[4, 2].Value = DateTime.Now.ToShortDateString();
            ws.Cells[5, 2].Value = Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;

            DataTable table = (DataTable)dgvMain.DataSource;
            DataTable filtered = table.DefaultView.ToTable();

            int row = 8, col = 1;
            foreach (DataRow row_table in filtered.Rows)
            {
                ws.Cells[row, col].Value = row_table["date"];
                ws.Cells[row, col].Style.Numberformat.Format = "mm-dd-yy";
                ws.Cells[row, col + 1].Value = row_table["code"];
                ws.Cells[row, col, row, col + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[row, col, row, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[row, col + 2].Value = row_table["name"];
                ws.Cells[row, col + 2].Style.WrapText = true;
                ws.Cells[row, col + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[row, col + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells[row, col + 3].Value = row_table["count"];
                ws.Cells[row, col + 4].Value = row_table["price"];
                ws.Cells[row, col + 5].Value = row_table["summa"];

                ws.Cells[row, col + 3, row, col + 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[row, col + 3, row, col + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                ws.Cells[row, col + 3].Style.Numberformat.Format = "#,##0.000";
                ws.Cells[row, col + 4, row, col + 5].Style.Numberformat.Format = "#,##0.00";

                ws.Cells[row, col, row, col + 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, col, row, col + 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, col, row, col + 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[row, col, row, col + 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                row++;
            }

            isPrintGoog = false;
            int numberReport = 0;
            while (!isPrintGoog)
                printNextReport(pck, numberReport++);
        }

        private void printNextReport(ExcelPackage pck, int number)
        {
            try
            {
                pck.SaveAs(new FileInfo(Application.StartupPath + $"\\Report({number}).xlsx"));
                System.Diagnostics.Process.Start(Application.StartupPath + $"\\Report({number}).xlsx");
                isPrintGoog = true;
            }
            catch { isPrintGoog = false; }
        }

        public void deleteOldReport()
        {
            string[] dirs = Directory.GetFiles(Application.StartupPath, "*.xlsx");

            foreach (string n in dirs)
            {
                try
                {
                    System.GC.Collect(); // попытка остановить процесс и удалить предыдущий файл excel
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(n);
                }
                catch { continue; }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void setFilter()
        {
            if (dtMain.Rows.Count < 1) return;
            string filter = "";

            if (tbSearchCode.Text.Length > 0)
                filter += "code LIKE \'%" + tbSearchCode.Text + "%\'";
            if (tbSearchName.Text.Length > 0)
                filter += filter.Length > 0 ? " AND " + $"name LIKE\'%{tbSearchName.Text}%\'" : $"name LIKE\'%{tbSearchName.Text}%\'";

            if ((Int16)cmbOtdel.SelectedValue != 0)
                filter += (filter.Length > 0 ? " AND " : "") + $"id_otdel = {cmbOtdel.SelectedValue}";

            dtMain.DefaultView.RowFilter = filter;
        }

        private void enabledButton()
        {
            btnPrintReport.Enabled = dgvMain.CurrentRow != null;
        }

        private void tbSearchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar) || !Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void tbSearchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbSearchCode_TextChanged(object sender, EventArgs e)
        {
            tbSearchCode.Text = ClearText(tbSearchCode.Text);
            setFilter();
        }

        private string ClearText(string str)
        {
            try
            {
                str = str.Remove(str.IndexOf('%'));
                str = str.Remove(str.IndexOf('*'));
            }
            catch { }
            return str;
        }

        private void tbSearchName_TextChanged(object sender, EventArgs e)
        {
            tbSearchName.Text = ClearText(tbSearchName.Text);
            setFilter();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            deleteOldReport();
            e.Cancel = DialogResult.No == MessageBox.Show("Закрыть программу?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void dgvMain_Paint(object sender, PaintEventArgs e)
        {
            tbSearchCode.Location = new System.Drawing.Point(date.Width + 12, tbSearchCode.Location.Y);
            tbSearchCode.Width = code.Width;
            tbSearchName.Location = new System.Drawing.Point(tbSearchCode.Location.X + 2 + tbSearchCode.Width, tbSearchName.Location.Y);
            tbSearchName.Width = name.Width;
        }

        private void cmbOtdel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }
    }
}
