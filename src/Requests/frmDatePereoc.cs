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
    public partial class frmDatePereoc : Form
    {
        DateTime datePereoc;
        string mode = "";

        public frmDatePereoc()
        {
            mode = "add";
            InitializeComponent();
        }

        public frmDatePereoc(DateTime _datePereoc)
        {
            mode = "edit";
            datePereoc = _datePereoc;
            InitializeComponent();
        }        

        private void frmDatePereoc_Load(object sender, EventArgs e)
        {
            Config.curDate = Config.hCntMain.GetCurDate(false);

            if (mode == "add")
            {
                dtpDate.Value = Config.curDate.Date;                
            }

            if (mode == "edit")
            {
                if (datePereoc.Date < Config.curDate.Date)
                {
                    dtpDate.Value = Config.curDate.Date;                    
                }
                else
                {
                    dtpDate.Value = datePereoc.Date; 
                }            
            }
            
            dtpDate.MinDate = Config.curDate.Date;
            
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Config.DatePereoc = dtpDate.Value.Date;
            Config.DatePereocSelected = true;
            this.Close();
        }
    }
}
