using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Requests.sWeInOut
{
    public partial class frmSettingsWeInOut : Form
    {
        

        private DataTable dtMain, dtK, dtX;

        private bool isEdit = false;

        public frmSettingsWeInOut()
        {
            InitializeComponent();
            dgvMain.AutoGenerateColumns = false;
            dgvK.AutoGenerateColumns = false;
            dgvX.AutoGenerateColumns = false;
            init_start_data();
        }

        private void init_start_data()
        {

           groupBox3.Enabled = false;
           groupBox3.Enabled = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("кд")
           || Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("мн")
           || Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("ркв")
           || Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("дмн");


           btSaveDGV.Visible = false;
           btSaveDGV.Visible = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("кд")
           || Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("мн")
           || Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("ркв")
           || Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("дмн");

 
           btAddK.Visible = btAddX.Visible = btDelK.Visible = btDelX.Visible = false;
           btAddK.Visible = btAddX.Visible = btDelK.Visible = btDelX.Visible = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToLower().Equals("кд");
           


            DataTable dtTmp = new DataTable();
            dtMain = Config.hCntMain.getAllSettingsWeInOut(1, "");

            if (dtMain == null) return;

            dtK = dtMain.Clone();
            dtX = dtMain.Clone();

            dtTmp = Config.hCntMain.getAllSettingsWeInOut(2, "prhp");

            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                foreach (DataRow r in dtTmp.Rows)
                {
                    DataRow[] rowFind = dtMain.Select("isActive = 1 AND id =  " + r["value"].ToString());
                    dtK.ImportRow(rowFind[0]);
                    rowFind[0]["isView"] = false;
                }
                dtMain.AcceptChanges();
                dtK.AcceptChanges();
            }

            dtTmp = Config.hCntMain.getAllSettingsWeInOut(2, "vzrp");

            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                foreach (DataRow r in dtTmp.Rows)
                {
                    DataRow[] rowFind = dtMain.Select("isActive = 1 AND id =  " + r["value"].ToString());
                    dtX.ImportRow(rowFind[0]);
                    rowFind[0]["isView"] = false;
                }
                dtMain.AcceptChanges();
                dtX.AcceptChanges();
            }

            dtTmp = Config.hCntMain.getAllSettingsWeInOut(3, "PeriodPrihNakl");

            if (dtTmp != null && dtTmp.Rows.Count > 0)
                nudTimeTakeData.Value = int.Parse(dtTmp.Rows[0]["val"].ToString());
            else
                nudTimeTakeData.Value = 30;

            filter_main();
            dgvMain.DataSource = dtMain;
            dgvK.DataSource = dtK;
            dgvX.DataSource = dtX;
            isEdit = false;
        }

        private void filter_main()
        {
            try {
                string filter = "";

                filter += (filter.Length == 0 ? "" : " AND ") + "isView = 1";

                if (tbName.Text.Trim().Length > 0)
                    filter += (filter.Length == 0 ? "" : " AND ") + string.Format("cname like '%{0}%'", tbName.Text.Trim());

                if (chbIsActive.Checked)
                    filter += (filter.Length == 0 ? "" : " AND ") + "isActive = 1";

                filter += (filter.Length == 0 ? "" : " AND ") + (rb11.Checked ? "ltype = 1" : "ltype = 2");

                dtMain.DefaultView.RowFilter = filter;
            }
            catch(Exception ex)
            {
                dtMain.DefaultView.RowFilter = "id = -9999";
                Console.Error.WriteLine(ex.Message);
            }

        }

        private void chbIsActive_CheckedChanged(object sender, EventArgs e)
        {
            filter_main();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            filter_main();
        }

        private void btAddK_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null && dgvMain.CurrentRow.Index != -1)
            {
                if (!(bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["isActive"]) return;
                dtK.ImportRow(dtMain.DefaultView.ToTable().Rows[dgvMain.CurrentRow.Index]);
                dtMain.DefaultView[dgvMain.CurrentRow.Index]["isView"] = false;
                dtMain.AcceptChanges();
                dtK.AcceptChanges();
                isEdit = true;
            }
        }

        private void btDelK_Click(object sender, EventArgs e)
        {
            if (dgvK.CurrentRow != null && dgvK.CurrentRow.Index != -1)
            {
                int idSelect = int.Parse(dtK.DefaultView[dgvK.CurrentRow.Index]["id"].ToString());
                dtMain.Select("id = " + idSelect)[0]["isView"] = true;
                dtMain.AcceptChanges();
                dgvK.Rows.RemoveAt(dgvK.CurrentRow.Index);
                dtK.AcceptChanges();
                isEdit = true;
            }
        }

        private void dgvMain_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dtMain != null && dtMain.DefaultView.Count != 0 && e.RowIndex != -1)
            { 
                Color rColor = Color.White;
                if (!bool.Parse(dtMain.DefaultView[e.RowIndex]["isActive"].ToString()))
                    rColor = panel1.BackColor;

                dgvMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                //dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
            }
        }

        private void btAddX_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null && dgvMain.CurrentRow.Index != -1)
            {
                if (!(bool)dtMain.DefaultView[dgvMain.CurrentRow.Index]["isActive"]) return;
                dtX.ImportRow(dtMain.DefaultView.ToTable().Rows[dgvMain.CurrentRow.Index]);
                dtMain.DefaultView[dgvMain.CurrentRow.Index]["isView"] = false;
                dtMain.AcceptChanges();
                dtX.AcceptChanges();
                isEdit = true;
            }
        }

        private void btDelX_Click(object sender, EventArgs e)
        {
            if (dgvX.CurrentRow != null && dgvX.CurrentRow.Index != -1)
            {
                int idSelect = int.Parse(dtX.DefaultView[dgvX.CurrentRow.Index]["id"].ToString());
                dtMain.Select("id = " + idSelect)[0]["isView"] = true;
                dtMain.AcceptChanges();
                dgvX.Rows.RemoveAt(dgvX.CurrentRow.Index);
                dtX.AcceptChanges();
                isEdit = true;
            }
        }

        private void dgvK_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dtK != null && dtK.DefaultView.Count != 0 && e.RowIndex != -1)
            {
                Color rColor = Color.White;
                if (!bool.Parse(dtK.DefaultView[e.RowIndex]["isActive"].ToString()))
                    rColor = panel1.BackColor;

                dgvK.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                //dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
            }
        }

        private void dgvX_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dtX != null && dtX.DefaultView.Count != 0 && e.RowIndex != -1)
            {
                Color rColor = Color.White;
                if (!bool.Parse(dtX.DefaultView[e.RowIndex]["isActive"].ToString()))
                    rColor = panel1.BackColor;

                dgvX.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                //dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
            }
        }

        private void btSaveDGV_Click(object sender, EventArgs e)
        {
            dtK.AcceptChanges();
            dtX.AcceptChanges();

            if (dtK.DefaultView.Count == 0)
            {
                MessageBox.Show("Сохранить настройки без поставщиков \"Прихода\" невозможно!", "Сохранение настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtX.DefaultView.Count == 0)
            {
                MessageBox.Show("Сохранить настройки без поставщиков \"Возврата\" невозможно!", "Сохранение настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Config.hCntMain.setAllSettingsWeInOut("prhp", "", 2);
            foreach (DataRow r in dtK.Rows)
            {
                if ((bool)r["isActive"])
                {
                    Config.hCntMain.setAllSettingsWeInOut("prhp", r["id"].ToString(), 1);
                }
            }

            Config.hCntMain.setAllSettingsWeInOut("vzrp", "", 2);
            foreach (DataRow r in dtX.Rows)
            {
                if ((bool)r["isActive"])
                {
                    Config.hCntMain.setAllSettingsWeInOut("vzrp", r["id"].ToString(), 1);
                }
            }


            Config.hCntMain.setAllSettingsWeInOut("PeriodPrihNakl", nudTimeTakeData.Value.ToString(), 3);

            MessageBox.Show("Данные сохранены!","Сохранение настроек",MessageBoxButtons.OK,MessageBoxIcon.Information);
            isEdit = false;
            this.DialogResult = DialogResult.OK;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmSettingsKX_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEdit && DialogResult.No == MessageBox.Show("Закрыть форму без сохранения введенных данных?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void rb11_Click(object sender, EventArgs e)
        {
            filter_main();
        }
    

    }
}
