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
    public partial class frmReport : Form
    {
        bool isPereoc;
        DataTable srcTable;
        public frmReport(bool isPereoc, DataTable reportTable)
        {
            InitializeComponent();
            this.isPereoc = isPereoc;
            srcTable = reportTable;
        }

        private void SetReport()
        {
            if (isPereoc)
            {
                if (Config.hCntMain.NedoUser())
                {
                    PereocWoutZcena repPereoc = new PereocWoutZcena();                    
                    repPereoc.SetDataSource(srcTable);
                    crvPereoc.ReportSource = repPereoc;
                    crvPereoc.Refresh();
                }
                else
                {
                    Pereoc repPereoc = new Pereoc();
                    repPereoc.SetDataSource(srcTable);
                    crvPereoc.ReportSource = repPereoc;
                    crvPereoc.Refresh();
                }                
            }
            else
            {
                if (Config.hCntMain.NedoUser())
                {
                    DoocWoutZcena repDooc = new DoocWoutZcena();                    
                    repDooc.SetDataSource(srcTable);
                    crvPereoc.ReportSource = repDooc;
                    crvPereoc.Refresh();
                }
                else
                {
                    Dooc repDooc = new Dooc();
                    repDooc.SetDataSource(srcTable);
                    crvPereoc.ReportSource = repDooc;
                    crvPereoc.Refresh();
                }                
            }
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            SetReport();
        }
    }
}
