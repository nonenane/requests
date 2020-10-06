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
    public partial class frmChooseType : Form
    {
        public frmChooseType()
        {
            InitializeComponent();
        }

        public int choosedType { get; set; }

        private void frmChooseType_Load(object sender, EventArgs e)
        {
            GetTypes();
        }

        private void GetTypes()
        {
            DataTable dtTypes = Config.hCntMain.GetTypes("1,2,3,8", false);
            cbType.DataSource = dtTypes;
            cbType.DisplayMember = "sname";
            cbType.ValueMember = "id";
            cbType.SelectedIndex = 0;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            choosedType = (int)cbType.SelectedValue;
            this.Close();
        }
    }
}
