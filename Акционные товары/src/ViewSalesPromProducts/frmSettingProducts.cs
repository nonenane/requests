using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ViewSalesPromProducts
{
    public partial class frmSettingProducts : Form
    {
        MainCatalog mc = new MainCatalog();
        char[] special_symbols = new char[] { '%', '*' };
        public frmSettingProducts()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadAccessoryElements();
            LoadOtdel();
            mc.getData();
            dgvOne.DataSource = mc.dtOne;
            dgvTwo.DataSource = mc.dtTwo;
            Cursor.Current = Cursors.Default; 
        }
        private void LoadAccessoryElements()
        {
            dgvOne.AutoGenerateColumns = false;
            dgvTwo.AutoGenerateColumns = false;

            dgvOne.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvTwo.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
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

        private void LoadTYGroup()
        {
            DataTable dt = Config.connectMain.getTYGroup(getcmbOtdelValue());
            dt.Rows.Add(0, "_Все группы");
            dt.DefaultView.Sort = "name ASC";
            cmbTYGroup.DataSource = dt;
            cmbTYGroup.ValueMember = "id";
            cmbTYGroup.DisplayMember = "name";
            if (dt != null ? dt.Rows.Count > 0 ? true : false : false)
                cmbTYGroup.SelectedIndex = 0;
        }

        private void LoadInvGroup()
        {
            DataTable dt = Config.connectMain.getInvGroup(getcmbOtdelValue());
            dt.Rows.Add(0, "_Все группы");
            dt.DefaultView.Sort = "name ASC";
            cmbinvGroup.DataSource = dt;
            cmbinvGroup.ValueMember = "id";
            cmbinvGroup.DisplayMember = "name";
            if (dt != null ? dt.Rows.Count > 0 ? true : false : false)
                cmbinvGroup.SelectedIndex = 0;

        }
        private int getcmbOtdelValue()
        {
            int x = 9999;
            int.TryParse(cmbOtdel.SelectedValue.ToString(), out x);
            return x;
        }



        private void cmbOtdel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (getcmbOtdelValue() == 9999) return;
            LoadInvGroup(); LoadTYGroup();
            mc.Otdel = getcmbOtdelValue();
        }

        private void cmbTYGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if (cmbTYGroup.SelectedIndex < 1) return;
            if (TryParse(cmbTYGroup.SelectedValue.ToString()))
                mc.tyGroup = int.Parse(cmbTYGroup.SelectedValue.ToString());
            if (mc.tyGroup > 0)
                cmbinvGroup.SelectedIndex = 0;
        }

        private void cmbinvGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if (cmbinvGroup.SelectedIndex < 0) return;
            if (TryParse(cmbinvGroup.SelectedValue.ToString()))
                mc.invGroup = int.Parse(cmbinvGroup.SelectedValue.ToString());

            if (mc.invGroup > 0)
                cmbTYGroup.SelectedIndex = 0;
        }
        private void tbSearchEan_TextChanged(object sender, EventArgs e)
        {
            mc.searchEan = tbSearchEan.Text = ClearText(tbSearchEan.Text);
        }

        private void tbSearchName_TextChanged(object sender, EventArgs e)
        {
            mc.searchNameTovar = tbSearchName.Text = ClearText(tbSearchName.Text);
        }

        private void tbSearchEan2_TextChanged(object sender, EventArgs e)
        {
            mc.searchEan2 = tbSearchEan2.Text = ClearText(tbSearchEan2.Text);
        }

        private void tbSearchName2_TextChanged(object sender, EventArgs e)
        {
            mc.searchNameTovar2 = tbSearchName2.Text = ClearText(tbSearchName2.Text);
        }


        private bool TryParse(string str)
        {
            int x = 0;
            return int.TryParse(str, out x);
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            if (Config.CheckDtOnEmpty((DataTable)dgvOne.DataSource)) return;
            Cursor.Current = Cursors.WaitCursor;
            DataTable table = (DataTable)dgvOne.DataSource;
            DataTable filtered = table.DefaultView.ToTable();// here we get filtered datatable

            mc.AddAllRow(filtered);
            dgvTwo.DataSource = mc.dtTwo;
            Cursor.Current = Cursors.Default;
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Config.CheckDtOnEmpty((DataTable)dgvOne.DataSource)) return;

            mc.AddRow(dgvOne.CurrentRow.Cells["id_tovar"].Value.ToString());
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Config.CheckDtOnEmpty((DataTable)dgvTwo.DataSource)) return;


            mc.DeleteRow(dgvTwo.CurrentRow.Cells["_id_tovar"].Value.ToString());
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (Config.CheckDtOnEmpty((DataTable)dgvTwo.DataSource)) return;
            Cursor.Current = Cursors.WaitCursor;
            DataTable table = (DataTable)dgvTwo.DataSource;
            DataTable filtered = table.DefaultView.ToTable();

            mc.DeleteAllRow(filtered);
            Cursor.Current = Cursors.Default;
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            mc.Save();
            Cursor.Current = Cursors.Default;
            dgvTwo.DataSource = mc.dtTwo;
            btnSave.Enabled = false;
            btnPrint.Enabled = true;
            //this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled)
            {
                if (MessageBox.Show("Выйти без сохранения?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    this.DialogResult = DialogResult.Cancel;
                else return;
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)dgvTwo.DataSource;
            DataTable filtered = table.DefaultView.ToTable();// here we get filtered datatable
            mc.Print(filtered, cmbOtdel.Text, cmbTYGroup.Text, cmbinvGroup.Text);

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


        private void tbSearchEan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbSearchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbSearchEan2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbSearchName2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvOne_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvOne.Rows.Count > 0)
                dgvOne.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvOne.Rows[e.RowIndex].Cells["isRezerv"].Value.ToString() == "1" ? panel1.BackColor : Color.White;

        }

        private void dgvTwo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvTwo.Rows.Count > 0)
                dgvTwo.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvTwo.Rows[e.RowIndex].Cells["_isRezerv"].Value.ToString() == "1" ? panel1.BackColor : Color.White;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
