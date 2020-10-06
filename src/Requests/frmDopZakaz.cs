using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace Requests
{
    public partial class frmDopZakaz : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        char[] special_symbols = new char[] { '%', '"', '*', '(', ')', '\'' };
        string id_grp_filter = "";
        DataTable dtGrp = null;
        public frmDopZakaz()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDopZakaz_Load(object sender, EventArgs e)
        {
            cmbTUGroups_Load();
            cmbSubGroups_Load();
            dgvDopZakaz_Load();
            SetButtonsEnabled();
        }

        private void cmbTUGroups_Load()
        {
            dtGrp = proc.GetTU(UserSettings.User.IdDepartment);
            DataRow all_row = dtGrp.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["id"]) == 0);
            all_row["cname"] = "Все группы";
            cmbTUGroups.DataSource = dtGrp;
        }

        private void cmbSubGroups_Load()
        {
            DataTable dt = proc.GetSubGrp(UserSettings.User.IdDepartment);
            DataRow all_row = dt.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["id"]) == 0);
            all_row["cname"] = "Все подгруппы";
            cmbSubGroups.DataSource = dt;
        }

        private void txtEAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClearName_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
        }

        private void dgvDopZakaz_Load()
        {
            dgvDopZakaz.AutoGenerateColumns = false;
            dgvDopZakaz.DataSource = proc.GetDopOrder(dtpDate.Value.Date);
            SetButtonsEnabled();
        }

        private void btnMultiTU_Click(object sender, EventArgs e)
        {
            frmFilter frmTU = new frmFilter(0, cmbTUGroups.DataSource as DataTable, UserSettings.User.IdDepartment);
            frmTU.ShowDialog();

            id_grp_filter = frmTU.SelectedIdList;
            if (id_grp_filter.Length > 0)
            {
                dtGrp.Rows.Add(new object[] { -1, "В соответствии с фильтром", 1 });
                cmbTUGroups.SelectedValue = -1;
            }
            Filter();
        }

        private void cmbTUGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbTUGroups.SelectedValue) != -1)
            {
                DataRow[] filterRows = dtGrp.Select("id = -1");
                if (filterRows.Length > 0)
                {
                    int selectedId = Convert.ToInt32(cmbTUGroups.SelectedValue);
                    dtGrp.Rows.Remove(filterRows[0]);
                    cmbTUGroups.SelectedValue = selectedId;
                }
            }
            Filter();
        }

        private void cmbSubGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtEAN_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (dgvDopZakaz.DataSource != null)
            {
                string filter = "ean like '%" + txtEAN.Text + "%' and cname like '%" + txtName.Text + "%'";

                if (cmbTUGroups.SelectedValue != null && Convert.ToInt32(cmbTUGroups.SelectedValue) != 0)
                {
                    filter += Convert.ToInt32(cmbTUGroups.SelectedValue) != -1 ? " and id_grp1 = " + cmbTUGroups.SelectedValue.ToString() : " and id_grp1 in (" + id_grp_filter + ")";
                }

                if (cmbSubGroups.SelectedValue != null && Convert.ToInt32(cmbSubGroups.SelectedValue) != 0)
                {
                    filter += " and id_grp3 = " + cmbSubGroups.SelectedValue.ToString();
                }

                if (cbNeedZakaz.Checked)
                {
                    filter += " and dop_zakaz <> 0";
                } 

                (dgvDopZakaz.DataSource as DataTable).DefaultView.RowFilter = filter;
                SetButtonsEnabled();
            }
        }

        private void SetButtonsEnabled()
        {
            btnToExcel.Enabled = dgvDopZakaz.RowCount > 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvDopZakaz_Load();
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dgvDopZakaz.DataSource != null)
            {
                DataTable dt = (dgvDopZakaz.DataSource as DataTable).DefaultView.ToTable();
                LogPrint(dt);

                dt.Columns.Remove("id_tovar");
                dt.Columns.Remove("id_order");
                dt.Columns.Remove("id_grp1");
                dt.Columns.Remove("id_grp3");

                dt.Columns["fact_netto"].SetOrdinal(7);

                ZakaznikReports.DopOrderReport.Show(dtpDate.Value.Date, UserSettings.User.Department, cmbTUGroups.Text, cmbSubGroups.Text, dt);
            }
        }

        private void LogPrint(DataTable dt)
        {
            Logging.StartFirstLevel(154);
            Logging.Comment("Выгрузка в Excel");
            Logging.Comment("Тип отчёта = отчёт по дополнительному заказу");
            Logging.Comment("Дата формирования отчёта = " + DateTime.Now.ToString());
            Logging.Comment("Флаг необходимо дозаказать = " + cbNeedZakaz.Checked.ToString());
            Logging.Comment("id ТУ группы = " + cmbTUGroups.SelectedValue.ToString() + ", название ТУ группы = " + cmbTUGroups.Text);
            Logging.Comment("id подгруппы = " + cmbSubGroups.SelectedValue.ToString() + ", название подгруппы = " + cmbSubGroups.Text);
            Logging.Comment("Фильтр по EAN = " + txtEAN.Text + ", фильтр по наименованию товара = " + txtName.Text);

            foreach (DataRow row in dt.Rows)
            {
                Logging.Comment("id товара = " + row["id_tovar"].ToString() + ", ean = '" + row["ean"].ToString() + "', название = '" + row["cname"].ToString() + "'");
                Logging.Comment("факт.нетто = " + row["fact_netto"].ToString() + ", доп.заказ = " + row["dop_zakaz"].ToString());
            }

            Logging.StopFirstLevel();
        }   

        private void cbNeedZakaz_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }
    }
}
