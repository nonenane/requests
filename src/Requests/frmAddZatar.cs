using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Requests
{
    public partial class frmAddZatar : Form
    {
        int zatarka = 0;
        int count = 0;
        decimal start_count = 0;
        string ean, cname;
        public int Count { get { return count; } }
        public frmAddZatar(int zatarka, decimal start_count, string ean, string cname)
        {
            this.zatarka = zatarka;
            this.start_count = start_count;
            this.ean = ean;
            this.cname = cname;
            InitializeComponent();
        }

        private void frmAddZatar_Load(object sender, EventArgs e)
        {
            txtEAN.Text = ean;
            txtName.Text = cname;
            txtZatarka.Text = zatarka.ToString();
            if (start_count != 0)
            {
                txtCount.Text = start_count.ToString();
                txtTara.Text = Math.Round(start_count / zatarka, 0).ToString();
            }
            SetButtonsEnabled();
            txtTara.Select();
            txtTara.SelectionStart = txtTara.Text.Length;
            txtTara.SelectionLength = 0;
        }

        private void txtTara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void txtTara_TextChanged(object sender, EventArgs e)
        {
            txtCount.Text = txtTara.Text.Length == 0 ? "" : (zatarka * Convert.ToInt32(txtTara.Text)).ToString();
            SetButtonsEnabled();
        }

        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            //txtTara.Text = "";
            SetButtonsEnabled();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAndExit();
        }

        private void SaveAndExit()
        {
            if (txtCount.Text.Length > 0)
            {
                count = Convert.ToInt32(txtCount.Text);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void SetButtonsEnabled()
        {
            btnSave.Enabled = txtCount.Text.Length > 0;
        }

        private void frmAddZatar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveAndExit();
            }
        }

        private void txtTara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveAndExit();
            }
        }

        private void txtCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveAndExit();
            }
        }
    }
}
