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
    public partial class frmSelectTovar : Form
    {
        private int id_tovar, idTReq, id_deps;
        private DataTable dtData;
        private bool isEdit = false;
        private bool selectedTovar = false;
        private bool bSpace = false;
        public frmSelectTovar()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
           
        }

        private void frmSelectTovar_Load(object sender, EventArgs e)
        {

        }

        public void setDataToForm(int id_tovar,string ean,string name,string zakaz,int idTReq, int id_deps)
        {
            this.id_tovar = id_tovar;
            this.tbEan.Text = ean;
            this.tbName.Text = name;
            this.tbZakaz.Text = zakaz;
            this.id_deps = id_deps;
            this.idTReq = idTReq;
            init_startData();
        }

        private void init_startData()
        {
            Object sTovar = Config.dtSelectedTovar.Compute("Sum(Netto)", "id_tovar = " + id_tovar);

            decimal sTovarD = 0;
            decimal sZakazD = 0;
            decimal.TryParse(sTovar.ToString(), out sTovarD);
            decimal.TryParse(tbZakaz.Text, out sZakazD);
            if (sTovarD < sZakazD)
            {
                selectedTovar = true;
            }
            else if (Config.dtSelectedTovar != null && Config.dtSelectedTovar.Rows.Count > 0 && Config.dtSelectedTovar.Select("id_tovar <> " + id_tovar).Length > 0)
            {
                DataTable temp = Config.dtSelectedTovar.Select("id_tovar <> " + id_tovar).CopyToDataTable();
                Config.dtSelectedTovar = temp;
            }

            int countDay = 30;
            DataTable dtTmp = Config.hCntMain.getAllSettingsWeInOut(3, "PeriodPrihNakl");

            if (dtTmp != null && dtTmp.Rows.Count > 0)
                countDay = int.Parse(dtTmp.Rows[0]["val"].ToString());

            dtpStart.Value = dtpStart.Value.AddDays(-countDay);

            init_deps();
            get_data();
            sumTovar();
            isEdit = false;
            btAutoCount_Click(null, null);
        }

        private void init_deps()
        {
            DataTable dtDeps = Config.hCntMain.GetDeps();
            cbDeps.DataSource = dtDeps;
            cbDeps.ValueMember = "id";
            cbDeps.DisplayMember = "name";
            cbDeps.SelectedValue = id_deps;
            cbDeps.Enabled = false;
        }

        private void get_data()
        {
            string strOperand = ",";
            string strPost = ",";
            DataTable dtTmp = Config.hCntMain.getAllSettingsWeInOut(2, "Nltp");
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                foreach (DataRow r in dtTmp.Rows)
                {
                    strOperand += r["value"].ToString() + ",";
                }
                strOperand = strOperand.Remove(0, 1);
            }

            dtTmp = Config.hCntMain.getAllSettingsWeInOut(4, "pk21");
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                foreach (DataRow r in dtTmp.Rows)
                {
                    strPost += r["value"].ToString() + ",";
                }
                strPost = strPost.Remove(0, 1);
            }

            dtTmp = Config.hCntMain.getAllSettingsWeInOut(4, "ph14");
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                foreach (DataRow r in dtTmp.Rows)
                {
                    strPost += r["value"].ToString() + ",";
                }
                strPost = strPost.Remove(0, 1);
            }

            string list = "";
            if (Config.dtSelectedTovar.Rows.Count > 0 && selectedTovar)
            { 
                foreach(DataRow r in Config.dtSelectedTovar.Rows)
                     list += r["id_prihod"]+",";

                list.Remove(0, 1);
            }

            // Тип заявки 3 показывает по диапозону, тип 5 показывает все выбранные накладные
            if (cbAddedN.Checked)
                dtData = Config.hCntShop2.getWeInOutTovarList(id_tovar, dtpStart.Value, dtpEnd.Value, strOperand, strPost, idTReq, 5, (list.Length > 0 ? list : "-1"));
            else
                dtData = Config.hCntShop2.getWeInOutTovarList(id_tovar, dtpStart.Value, dtpEnd.Value, strOperand, strPost, idTReq, 3);
                
                

            DataTable dtTmpList = Config.hCntMain.getWeInBusyTovar(id_tovar, idTReq);

            if (Config.dtSelectedTovar.Rows.Count > 0 && dtData != null && dtData.Rows.Count > 0 && selectedTovar)
            {
                foreach(DataRow r in Config.dtSelectedTovar.Rows)
                {
                    DataRow[] rSel = dtData.Select("id = "+r["id_prihod"]);
                    if(rSel.Count()>0)
                    {
                        rSel[0]["NettoReq"]= r["Netto"];
                        rSel[0]["isV"] = 1;
                    }
                }
                dtData.AcceptChanges();
            }

            foreach (DataRow r in dtData.Rows)
            {
                DataRow[] rSel = dtTmpList.Select("id_prihod = " + r["id"]);
                if (rSel.Count() > 0)
                {
                    if (decimal.Parse(r["netto"].ToString()) - decimal.Parse(rSel[0]["Netto"].ToString()) > 0)
                        r["netto"] = decimal.Parse(r["netto"].ToString()) - decimal.Parse(rSel[0]["Netto"].ToString());
                    else
                        r["netto"] = 0;

                    r["isColor"] = true;
                }
            }

            filter();
            dgvData.DataSource = dtData;
            sumTovar();
        }

        private void dtpStart_Leave(object sender, EventArgs e)
        {
            get_data();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dgvData.Columns[e.ColumnIndex].Name == cV.Name)
            {
                if (bSpace)
                {
                    bSpace = false;
                    return;
                }

                decimal lostR = 0;
                decimal reqCount = 0;
                decimal.TryParse(tbLostRequest.Text, out lostR);
                decimal.TryParse(dgvData.Rows[e.RowIndex].Cells["cRequestCount"].Value.ToString(), out reqCount);
                if (lostR <= 0 && reqCount <= 0)
                    return;

                if (decimal.Parse(dtData.DefaultView[e.RowIndex]["netto"].ToString()) == 0 && decimal.Parse(dtData.DefaultView[e.RowIndex]["NettoReq"].ToString()) == 0)
                    return;
                
                bool _isControl = false;
                if (dtData.DefaultView[e.RowIndex]["isV"] != DBNull.Value)
                    _isControl = bool.Parse(dtData.DefaultView[e.RowIndex]["isV"].ToString());

                dtData.DefaultView[e.RowIndex]["isV"] = !_isControl;
                if (_isControl)
                    dtData.DefaultView[e.RowIndex]["NettoReq"] = 0;
                else
                {
                    decimal _need = decimal.Parse(tbLostRequest.Text.ToString());
                    decimal _used = decimal.Parse(dtData.DefaultView[e.RowIndex]["netto"].ToString());

                    if (_used < _need)
                    {
                        dtData.DefaultView[e.RowIndex]["NettoReq"] = _used;
                    }
                    else
                    {
                        dtData.DefaultView[e.RowIndex]["NettoReq"] = _need;
                    }
                }
                isEdit = true;
                sumTovar();

                bSpace = false;
                //dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
                //dgvData.Invalidate();
                //dtData.AcceptChanges();
            }
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dgvData.Rows[e.RowIndex].Cells[cRequestCount.Index].ReadOnly = !bool.Parse(dtData.DefaultView[e.RowIndex]["isV"].ToString());
            }

            Color rColor = Color.White;
            try
            {
                if (bool.Parse(dtData.DefaultView[e.RowIndex]["isColor"].ToString()))
                    rColor = panel2.BackColor; 
            }
            catch
            {

            }
            finally {
                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
            }
        }

        void tbNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else if (e.KeyChar != ',' && e.KeyChar != '\b' && (sender as TextBox).Text.ToString().Length == 10 && !(sender as TextBox).Text.ToString().Contains(','))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
                {
                    if (e.KeyChar != '\b')
                    { e.Handled = true; }
                }

        }

        private void dgvData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvData.CurrentCell.ColumnIndex == cRequestCount.Index)
            {
                TextBox tb = (TextBox)e.Control;
              
                tb.KeyPress -= tbNumber_KeyPress;
                tb.KeyPress += new KeyPressEventHandler(tbNumber_KeyPress);
            }
        }

        private void dgvData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex != -1 && dgvData.Columns[e.ColumnIndex].Name == cRequestCount.Name)
            {
                Decimal decimaltest;
                if (e.ColumnIndex == cRequestCount.Index && e.FormattedValue.ToString().Length == 0)
                {
                    MessageBox.Show("Необходимо ввести кол-во!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dgvData.Rows[e.RowIndex].Cells[cRequestCount.Index].Value = decimal.Parse("0");
                    e.Cancel = true;
                    return;
                }
                else
                    if (e.FormattedValue.ToString().Length > 10 && !e.FormattedValue.ToString().Contains(','))
                    {
                        MessageBox.Show("Необходимо формат числа!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (decimal.TryParse((e.FormattedValue.ToString() == "") ? "0" : e.FormattedValue.ToString(), out decimaltest))
                        {

                            if (decimal.Parse(dtData.DefaultView[e.RowIndex]["netto"].ToString()) < decimaltest)
                            {
                                MessageBox.Show("Превышенно кол-во товара для ввода!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                                return;
                            }
                            else
                            {
                                decimal nReq = decimal.Parse(dtData.DefaultView[e.RowIndex]["NettoReq"].ToString());
                                decimal nNeed = decimal.Parse(tbLostRequest.Text.ToString());
                                if (nReq + nNeed < decimaltest)
                                {
                                    MessageBox.Show("Превышенно кол-во товара в заявке!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    e.Cancel = true;
                                    return;
                                }

                                dgvData.Rows[e.RowIndex].Cells[cRequestCount.Index].Value = decimaltest;
                                dtData.DefaultView[e.RowIndex]["NettoReq"] = decimaltest;

                                decimal _need = decimal.Parse(tbLostRequest.Text.ToString());
                                decimal _used = decimal.Parse(dtData.DefaultView[e.RowIndex]["netto"].ToString());
                            }
                        }
                    }
                
            }
        }

        private void sumTovar()
        {
            dtData.AcceptChanges();
            object sum = dtData.Compute("SUM(NettoReq)", "isV = 1");
            if (sum != DBNull.Value)
                tbInRequest.Text = decimal.Parse(sum.ToString()).ToString("### ### ##0.00").Trim();
            else
                tbInRequest.Text = "0,00";

            tbLostRequest.Text = (decimal.Parse(tbZakaz.Text.ToString()) - decimal.Parse(tbInRequest.Text.ToString())).ToString();
            
        }

        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            sumTovar();
            decimal _need = decimal.Parse(tbLostRequest.Text.ToString());

            if (_need < 0)
                MessageBox.Show("Добавлено/отредактировано товара \n больше чем требуется в заявку!", "Ввод данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (_need == 0)
                MessageBox.Show("Вы ввели требуемое\nколичество товара для заявки!", "Ввод данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //decimal _used = decimal.Parse(dtData.DefaultView[e.RowIndex]["netto"].ToString());
            isEdit = true;
        }

        private void rb11_Click(object sender, EventArgs e)
        {
            filter();
        }

        private void filter()
        {
            string filter = "";
            if (dtData == null) return;
            try
            {
                // if (int.Parse(cbDeps.SelectedValue.ToString()) != -1)
                //    filter += string.Format("id_otdel = {0}", cbDeps.SelectedValue.ToString());

                filter += filter.Trim().Length == 0 ? "" : " AND ";
                filter += rb11.Checked ? "type = 1" : "type = 2";

                if (tbFName.Text.Trim().Length != 0)
                {
                    filter += filter.Trim().Length == 0 ? "" : " AND ";
                    filter += string.Format("cname like '%{0}%'", tbFName.Text);
                }

                if (tbFTTN.Text.Trim().Length != 0)
                {
                    filter += filter.Trim().Length == 0 ? "" : " AND ";
                    filter += string.Format("ttn like '%{0}%'", tbFTTN.Text);
                }

                if (chbUsed.Checked)
                {
                    if (idTReq == 0)
                    {
                        filter += filter.Trim().Length == 0 ? "" : " AND ";
                        filter += "netto = 0";
                    }
                    else
                    {
                        filter += filter.Trim().Length == 0 ? "" : " AND ";
                        filter += "netto =0";
                    }
                }
                else
                {
                    if (idTReq == 0)
                    {
                        filter += filter.Trim().Length == 0 ? "" : " AND ";
                        filter += "netto <>0";
                    }
                    else
                    {
                        filter += filter.Trim().Length == 0 ? "" : " AND ";
                        filter += "(netto <>0 OR (NettoReq=0 AND netto <>0))";
                    }
                }

                dtData.DefaultView.RowFilter = filter;
                if (idTReq == 0)
                    dtData.DefaultView.Sort = "dprihod DESC, id DESC";
                else
                    dtData.DefaultView.Sort = "isV DESC,dprihod DESC, id DESC";

            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -99999";
            }
        }

        private void tbFTTN_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void chbUsed_Click(object sender, EventArgs e)
        {
            filter();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            sumTovar();
            decimal _need = decimal.Parse(tbLostRequest.Text.ToString());

            if(dtData.Select("isV = 1 and NettoReq < 0").Length > 0)
            {
                MessageBox.Show("У постовщика указано \nотрицательное количетсво товара.\nСохранение невозможно!", "Ввод данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_need < 0)
            {
                MessageBox.Show("Вы ввели для заявки количество\nтовара меньше чем требуется.\nСохранение невозможно!", "Ввод данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (_need > 0)
            {
                MessageBox.Show("Вы ввели для заявки количество\nтовара больше чем требуется.\nСохранение невозможно!", "Ввод данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (dtData.Select("isV = 1 and NettoReq = 0").Length > 0)
            {
                MessageBox.Show("Имеются поставщики без товара.\nСохранение невозможно!", "Ввод данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dtData.AcceptChanges();
            
            Config.clearTovarSelectedTovar(id_tovar);

            foreach (DataRow row in dtData.Select("isV = 1"))
            {
                int id_prihod = int.Parse(row["id"].ToString());
                decimal NettoReq = decimal.Parse(row["NettoReq"].ToString());
                decimal zcena = decimal.Parse(row["zcena"].ToString());
                Config.dtSelectedTovar.Rows.Add(id_prihod, id_tovar, NettoReq, zcena);
            }

            isEdit = false;
            this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btAutoCount_Click(object sender, EventArgs e)
        {
            decimal _need = decimal.Parse(tbLostRequest.Text);
            if (_need == 0) return;

            foreach (DataRow r in dtData.Select("NettoReq=0 AND netto<>0", "dprihod DESC, id DESC"))
            {
                decimal _used = decimal.Parse(r["netto"].ToString());
                isEdit = true;
                if (_used < _need)
                {
                    r["NettoReq"] = _used;
                    r["isV"] = true;
                    _need = _need - _used;
                }
                else
                {
                    r["NettoReq"] = _need;
                    r["isV"] = true;
                    _need = _need - _used;
                }

                if (_need <= 0)
                    break;
            }

            dtData.AcceptChanges();
            sumTovar();

        }

        private void frmSelectTovar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (isEdit && MessageBox.Show("На форме есть несохраненные данные.\n Закрыть форму без сохранения данных?", "Инфомирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No);
        }

        private void cbAddedN_CheckedChanged(object sender, EventArgs e)
        {
            dtpStart.Enabled = dtpEnd.Enabled = !((CheckBox)sender).Checked;
            get_data();
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Space)
            {
                bSpace = true;
                e.Handled = true;
                return;
            }
        }
    }
}
