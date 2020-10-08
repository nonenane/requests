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
    public partial class frmViewDiscountGoods : Form
    {
        private DataTable dtData;

        public frmViewDiscountGoods()
        {
            InitializeComponent();
            this.Text = Nwuram.Framework.Settings.Connection.ConnectionSettings.ProgramName + "; " + Nwuram.Framework.Settings.User.UserSettings.User.Status + "; " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
            dgvMain.AutoGenerateColumns = false;
        }

        private void frmViewDiscountGoods_Load(object sender, EventArgs e)
        {
            LoadOtdel();
            dgvMain_ColumnWidthChanged(null, null);
            getData();
        }

        private void addColumnInData(string name, Type type)
        {
            if (!dtData.Columns.Contains(name))
                dtData.Columns.Add(name, type);
        }

        private void delColumnInData(DataTable dt, string name)
        {
            if (dt.Columns.Contains(name))
                dt.Columns.Remove(name);
        }

        private void getData()
        {
            dtData = Config.connectMain.getCatalogPromotionalTovars();

            DataTable dtX14 = Config.connectSecond.getCatalogPromotionalTovars();
            if (dtX14 != null && dtX14.Rows.Count > 0)
            {
                addColumnInData(cPriceRealK21.DataPropertyName, typeof(decimal));
                addColumnInData(cPriceDiscountK21.DataPropertyName, typeof(decimal));
                
                addColumnInData(cPriceRealX14.DataPropertyName, typeof(decimal));
                addColumnInData(cPriceDiscountX14.DataPropertyName, typeof(decimal));


                

                DataTable dtTmp = dtData.Clone();
                delColumnInData(dtTmp,"Price");
                delColumnInData(dtTmp, "SalePrice");

                var query = (from g in dtData.AsEnumerable()
                             join k in dtX14.AsEnumerable() on new { Q = g.Field<int>("id_tovar") } equals new { Q = k.Field<int>("id_tovar") } into t1                             
                             from leftjoin1 in t1.DefaultIfEmpty()                             
                             select dtTmp.LoadDataRow(new object[]
                                              {
                                                  g.Field<int>("id_tovar"),
                                                  g.Field<string>("ean"),
                                                  g.Field<string>("cName"),
                                                  g.Field<int>("id_otdel"),
                                                  g.Field<string>("nameDep"),
                                                  //0,
                                                  //0,                                                  
                                                  g.Field<string>("FIO"),
                                                  g.Field<DateTime?>("DateEdit"),
                                                  g.Field<decimal>("Price"),
                                                  g.Field<decimal>("SalePrice"),
                                                  leftjoin1 == null ? null : leftjoin1.Field<decimal?>("Price"),
                                                  leftjoin1 == null ? null : leftjoin1.Field<decimal?>("SalePrice"),

                                              }, false));


                dtData = query.CopyToDataTable();

                dtTmp.Clear();

            }
            setFilter();
            dgvMain.DataSource = dtData;
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                //btPrint.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbSearchCode.Text.Trim().Length != 0)
                    filter += (filter.Trim().Length == 0 ? "" : " and ") + string.Format("ean like '%{0}%'", tbSearchCode.Text.Trim());

                if (tbSearchName.Text.Trim().Length != 0)
                    filter += (filter.Trim().Length == 0 ? "" : " and ") + string.Format("cName like '%{0}%'", tbSearchName.Text.Trim());

                if ((Int16)cmbOtdel.SelectedValue != 0)
                    filter += (filter.Trim().Length == 0 ? "" : " and ") + $"id_otdel = {cmbOtdel.SelectedValue}";


                dtData.DefaultView.RowFilter = filter;
                //btPrint.Enabled = dtData.DefaultView.Count != 0;
                dtData.DefaultView.Sort = "id_otdel asc, cName asc";
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id_tovar = -1";
                //btPrint.Enabled = false;
            }

        }

        private void LoadOtdel()
        {
            DataTable dt = Config.connectMain.getDep();
            dt.Rows.Add(0, "Все отделы");
            dt.DefaultView.Sort = "id ASC";
            cmbOtdel.DataSource = dt;
            cmbOtdel.ValueMember = "id";
            cmbOtdel.DisplayMember = "name";
            cmbOtdel.SelectedValue = 0;
        }

        private void dgvMain_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvMain.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(cEan.Name))
                {
                    tbSearchCode.Location = new Point(dgvMain.Location.X + 1 + width, tbSearchCode.Location.Y);
                    tbSearchCode.Size = new Size(cEan.Width, tbSearchCode.Size.Height);
                }

                if (col.Name.Equals(cName.Name))
                {
                    tbSearchName.Location = new Point(dgvMain.Location.X + 1 + width, tbSearchCode.Location.Y);
                    tbSearchName.Size = new Size(cName.Width, tbSearchCode.Size.Height);
                }

                width+=col.Width;
            }
        }

        private void tbSearchCode_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void cmbOtdel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmViewDiscountGoods_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = DialogResult.No == MessageBox.Show(Config.centralText("Вы действительно хотите выйти\nиз программы?\n"), "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            if (dtData == null || dtData.Rows.Count == 0 || dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index == -1 || dgvMain.CurrentRow.Index>=dtData.DefaultView.Count)
            {
                tbDateEdit.Text = "";
                tbEditor.Text = "";
                return;
            }

            tbDateEdit.Text = dtData.DefaultView[dgvMain.CurrentRow.Index]["DateEdit"].ToString();
            tbEditor.Text = dtData.DefaultView[dgvMain.CurrentRow.Index]["FIO"].ToString();
        }

        private void dgvMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void dgvMain_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;

                dgvMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }


    }
}
