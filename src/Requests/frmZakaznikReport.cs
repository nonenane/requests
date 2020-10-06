using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;

namespace Requests
{
    public partial class frmZakaznikReport : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        string id_grp1, id_grp3, tu_group, sub_group; 
        public frmZakaznikReport(string id_grp1, string tu_group, string id_grp3, string sub_group)
        {
            this.id_grp1 = id_grp1;
            this.tu_group = tu_group;
            this.id_grp3 = id_grp3;
            this.sub_group = sub_group;
            InitializeComponent();
        }

        private void frmZakaznikReport_Load(object sender, EventArgs e)
        {
            dtpDate.MaxDate = DateTime.Today;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
                DataTable dt = cbInventory.Checked ? proc.GetInventoryReport(dtpDate.Value.Date) : proc.GetOrdersHeadForReport(dtpDate.Value.Date, id_grp1, id_grp3);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для отчёта!");
                }
                else
                {
                    LogPrint();
                    if (cbInventory.Checked)
                    {
                        ZakaznikReports.InventoryReport.Show(dt);
                    }
                    else
                    {
                        ZakaznikReports.OrderReport.Show(dtpDate.Value.Date, UserSettings.User.Department, tu_group, sub_group, dt, proc.GetOrdersBody(dtpDate.Value.Date));
                    }
                }
        }

        private void LogPrint()
        {
            Logging.StartFirstLevel(154);
            Logging.Comment("Выгрузка отчёта в Excel");
            Logging.Comment("Тип отчёта = " + (cbInventory.Checked ? "по инвентаризации" : "по основному заказу"));
            Logging.Comment("Дата отчёта = " + dtpDate.Value.ToShortDateString());
            Logging.StopFirstLevel();
        }
    }
}
