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
    public partial class frmProhMessage : Form
    {
        DataTable dtReqProhGoods; //Запрещенные товары в заявке


        public frmProhMessage(DataTable dtReqProhGoods)
        {
            InitializeComponent();
            this.dtReqProhGoods = dtReqProhGoods;
        }

        private void frmProhMessage_Load(object sender, EventArgs e)
        {
            grdReqProh.AutoGenerateColumns = false;
            grdReqProh.DataSource = dtReqProhGoods;
        }
    }
}
