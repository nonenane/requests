using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.ToExcel;
using Nwuram.Framework.Settings.User;

using Nwuram.Framework.Logging;

namespace Requests
{
    public partial class frmRequests : Form
    {        
        BindingSource bsRequests = new BindingSource();
        DataTable dtCreditTypes;
       
        Color rowBK; //Цвет для раскраски грида

        bool autoSaved = false;

        public frmRequests()
        {
            InitializeComponent();
            grdRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public frmRequests(bool autoSaved)
        {
            InitializeComponent();
            grdRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.autoSaved = autoSaved;
        }

        private void Requests_Load(object sender, EventArgs e)
        {
            GetSavedDates();
            GetStatuses();
            GetDeps();
            GetTypes();
            GetCreditTypes();
            GetManagers();

            if (UserSettings.User.StatusCode == "МН")
            {
                rukovod.Visible = manager.Visible = false;
                pnManager.Visible = lbMngr.Visible 
                    = btCopy.Visible = btNew.Visible = true;
                btDecline.Visible = btInProcess.Visible 
                    = btChangeReqDate.Visible = false;
                
                weight.Visible
                    = kolkor.Visible
                    = true;
            }

            if (UserSettings.User.StatusCode == "ДМН")
            {
                btCopy.Visible = btNew.Visible = true;

                btChangeReqDate.Visible = false;

                weight.Visible
                    = kolkor.Visible
                    = true;
            }

            if (UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР")
            {
                foreach (Control con in pnButtons.Controls)
                {
                    if (con is Button && con.Name != "btShow" && con.Name != "btPrint")
                    {
                        con.Visible = false;
                    }
                }

                weight.Visible
                    = kolkor.Visible
                    = pnNewPieceGoods.Visible
                    = lblNewPieceGoods.Visible
                    = pnNoBrutto.Visible
                    = lblNoBrutto.Visible
                    = false;
            }

            if (UserSettings.User.StatusCode == "РКВ")
            {
                weight.Visible
                    = kolkor.Visible
                    = true;
            }

            if (UserSettings.User.StatusCode == "КД")
            {
                weight.Visible
                    = kolkor.Visible
                    = true;
            }

            btExcel.Visible = (UserSettings.User.StatusCode == "РКВ");
            btnCopyActPereoc.Visible = UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "ДМН";
            //SetColumnsWidth();
            //grdRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            tbResult.Text = "";
            GetGridRequests();
            grdRequests.Select();

            //if (autoSaved)
            //{
            //    ((Main)this.MdiParent).SetTab("Создание заявки.", "frmEditRequest", new object[] { 1, 0, false, true }, 0);
            //}

        }

        /// <summary>
        /// Получаем настройки дат
        /// </summary>
        private void GetSavedDates()
        {
            Config.dtProperties = Config.hCntMain.GetProperties();
            DataRow[] dtDateStart = Config.dtProperties.Select("id_val = 'dreqst'");
            DataRow[] dtDateEnd = Config.dtProperties.Select("id_val = 'dreqfin'");
            if (dtDateStart.Count() != 0 && dtDateEnd.Count() != 0)
            {
                dtpStart.Value = DateTime.Parse(dtDateStart[0]["val"].ToString());
                dtpFinish.Value = DateTime.Parse(dtDateEnd[0]["val"].ToString());
            }
        }

        /// <summary>
        /// Получение фильтра по статусам
        /// </summary>
        private void GetStatuses()
        {
            Config.GetStatuses();
            lbStatuses.Text = Config.statusesNames;
            SetFilter();
        }

        /// <summary>
        /// Установка первоначальной ширины колонок
        /// </summary>
        private void SetColumnsWidth()
        {

            if ((UserSettings.User.StatusCode == "РКВ")
                || (UserSettings.User.StatusCode == "МН")
                || (UserSettings.User.StatusCode == "ДМН")
                || (UserSettings.User.StatusCode == "КД"))
            {
                //department.Width = (int)(grdRequests.Width * 0.1);
                id_req.FillWeight = (int)(grdRequests.Width * 0.07);
                reqDate.FillWeight = (int)(grdRequests.Width * 0.07);
                supplier.FillWeight = (int)(grdRequests.Width * 0.22); //убрал -5
                reqSum.FillWeight = (int)(grdRequests.Width * 0.08);
                rukovod.FillWeight = (int)(grdRequests.Width * 0.12);//убрал -3
                manager.FillWeight = (int)(grdRequests.Width * 0.12);//убрал-3
                ul.FillWeight = (int)(grdRequests.Width * 0.03);
                reqType.FillWeight = (int)(grdRequests.Width * 0.06);
                creditType.FillWeight = (int)(grdRequests.Width * 0.11);//убрал-1
                weight.FillWeight = (int)(grdRequests.Width * 0.08);
                kolkor.FillWeight = (int)(grdRequests.Width * 0.04);
            }
            else
            {
                //department.Width = (int)(grdRequests.Width * 0.1);
                id_req.FillWeight = (int)(grdRequests.Width * 0.07);
                reqDate.FillWeight = (int)(grdRequests.Width * 0.07);
                supplier.FillWeight = (int)(grdRequests.Width * 0.27); 
                reqSum.FillWeight = (int)(grdRequests.Width * 0.08);
                rukovod.FillWeight = (int)(grdRequests.Width * 0.15);
                manager.FillWeight = (int)(grdRequests.Width * 0.15);
                ul.FillWeight = (int)(grdRequests.Width * 0.03);
                reqType.FillWeight = (int)(grdRequests.Width * 0.06);
                creditType.FillWeight = (int)(grdRequests.Width * 0.12);                
            }

        }

        /// <summary>
        /// Получение данных для комбика с отделами
        /// </summary>
        private void GetDeps()
        {
            DataTable dtDeps = Config.hCntMain.GetDeps();

            if (UserSettings.User.StatusCode == "КД")
            {
                DataRow drAll = dtDeps.NewRow();
                drAll["id"] = 0;
                drAll["name"] = "ВСЕ ОТДЕЛЫ";
                dtDeps.Rows.InsertAt(drAll, 0);
            }

            cbDep.DataSource = dtDeps;
            cbDep.ValueMember = "id";
            cbDep.DisplayMember = "name";

            //Для менеджера и руководителя блокируем комбик
            if (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН" || UserSettings.User.StatusCode == "РКВ")
            {
                cbDep.SelectedValue = UserSettings.User.IdDepartment;
            }
            else
            {

                cbDep.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Получение данных для комбика с менеджерами
        /// </summary>
        private void GetManagers()
        {
            if (UserSettings.User.StatusCode != "МН" /*&& UserSettings.User.StatusCode != "ДМН"*/)
            {
                int id_dep;
                if (int.TryParse(cbDep.SelectedValue.ToString(), out id_dep))
                {
                    DataTable dtManagers;
                    if (id_dep == 6)
                        dtManagers = Config.hCntAdd.GetManagers(6);
                    else
                    {
                        if (id_dep == 0)
                        {
                            dtManagers = Config.hCntMain.GetManagers(0);
                            DataRow[] dtAddManagers = Config.hCntAdd.GetManagers(6).Select("id_Access NOT IN (" + Config.GetStringFromRow(dtManagers, "id_Access") + ")");

                            if(dtAddManagers.Count() > 0)
                                dtManagers.Merge(DataTableExtensions.CopyToDataTable(dtAddManagers));

                            dtManagers.DefaultView.Sort = "Num, FIO ASC";
                            dtManagers = dtManagers.DefaultView.ToTable();

                            int i;
                            for (i = 0; i < dtManagers.Rows.Count; i++)
                            {
                                if ((long)dtManagers.Rows[i]["Num"] != 0)
                                    dtManagers.Rows[i]["Num"] = i + 1;
                            }

                            dtManagers.DefaultView.Sort = "Num ASC";
                            dtManagers = dtManagers.DefaultView.ToTable();
                            
                        }
                        else
                        {
                            dtManagers = Config.hCntMain.GetManagers(id_dep);
                        }
                    }

                    cbMan.DataSource = dtManagers;
                    cbMan.DisplayMember = "FIO";
                    cbMan.ValueMember = "id_Access";
                    cbMan.SelectedIndex = 0;
                }
            }
            else
            {
                lbMan.Visible = cbMan.Visible = false;
            }
        }

        /// <summary>
        /// Получение данных для комбика с типом операции
        /// </summary>
        private void GetTypes()
        {
            DataTable dtTypes = Config.hCntMain.GetTypes("1,2,3,4,8",true, true);
            cbType.DataSource = dtTypes;
            cbType.DisplayMember = "sname";
            cbType.ValueMember = "id";

            cbType.SelectedValue = cbType.SelectedValue is int
                                    && (int)cbType.SelectedValue != -1
                                    && isFilterSettingsExists("type") ? -1 : 0;
        }

        private bool isFilterSettingsExists(string id_val)
        {
            DataTable dtSettings = Config.hCntMain.GetFilterSettings(id_val, 0);
            return dtSettings != null && dtSettings.Rows.Count > 0;
        }

        /// <summary>
        /// Получение типов кредитования
        /// </summary>
        private void GetCreditTypes()
        {
            dtCreditTypes = Config.GetCreditTypes(true);
            cbCreditType.DataSource = dtCreditTypes;
            cbCreditType.ValueMember = "id";
            cbCreditType.DisplayMember = "cname";
        }

        private void tbNuber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !"1234567890\b".Contains(e.KeyChar);
        }

        private void tbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {           
            e.Handled = "!@#$%^&*()_+-=\"№;:?'{}[]|/\\<>".Contains(e.KeyChar);
        }

        private void tbEAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !"0123456789\r\b".Contains(e.KeyChar);
            if (e.KeyChar == '\r')
            {
                //GetGridRequests();
                SendKeys.Send("{TAB}");
            }
        }

        int nstat;
        DateTime dateOfReq;
        bool emptyBuhg;
        /// <summary>
        /// Изменение доступности кнопок
        /// </summary>
        private void SetButtonsEnabled()
        {
            Config.curDate = Config.hCntMain.GetCurDate(false);
            if (grdRequests.DataSource == null || grdRequests.Rows.Count == 0)
            {
                foreach (Control con in pnButtons.Controls)
                {
                    if (con.Name != "btNew")
                        con.Enabled = false;
                }
               // pnButtons.Enabled = false;
            }
            else
            {
                if (grdRequests.CurrentRow == null)
                {
                    grdRequests.CurrentCell = grdRequests[1, 0];
                }
                DataGridViewRow curRow = grdRequests.CurrentRow;
                nstat = int.Parse(curRow.Cells["nstatus"].Value.ToString());
                dateOfReq = (DateTime)curRow.Cells["reqDate"].Value;
                emptyBuhg = int.Parse(curRow.Cells["emptyBuh"].Value.ToString()) == 1;

                pnButtons.Enabled = true;
               
                if ((int)curRow.Cells["id_oper"].Value != 4)
                {
                    btChangeReqDate.Enabled = (nstat == 12
                                             && curRow.Cells["id_priem"].Value.GetType() != typeof(DBNull)
                                             && curRow.Cells["data_factout"].Value.GetType() != typeof(DBNull)
                                             && (curRow.Cells["ttn"].Value.GetType() == typeof(DBNull) || curRow.Cells["ttn"].Value.ToString().Trim().Length == 0)
                                             && decimal.Parse(curRow.Cells["sum_ttn"].Value.ToString()) == 0);
                    btInProcess.Enabled = ((UserSettings.User.StatusCode == "КД" && new int[4] { 12, 11, 3, 8 }.Contains(nstat))
                                               || ((UserSettings.User.StatusCode == "РКВ"
                                                   || UserSettings.User.StatusCode == "ДМН") && new int[5] { 12, 11, 7, 3,8 }.Contains(nstat) 
                                               //&& new int[2] { 3, 4 }.Contains((int)curRow.Cells["Credit"].Value)
                                               )) && curRow.Cells["data_factout"].Value.GetType() == typeof(DBNull);
                    btDecline.Enabled = ((UserSettings.User.StatusCode == "КД" && new int[2] { 7, 2 }.Contains(nstat))
                                        || ((UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "ДМН") && nstat == 2)
                                        || (UserSettings.User.StatusCode == "РБ6" && nstat == 8));
                    btAccept.Enabled = (//new int[2] { 0, 6 }.Contains(nstat)
                                        ((nstat == 0 || (UserSettings.User.StatusCode == "МН" && nstat == 6)) && (DateTime)curRow.Cells["reqDate"].Value >= Config.curDate.Date)
                                        || (UserSettings.User.StatusCode == "КД" && new int[2] { 7, 2 }.Contains(nstat))
                                        || ((UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "ДМН") && nstat == 2)
                                        || (UserSettings.User.StatusCode == "РБ6" && nstat == 8));
                    btDelete.Enabled = (((new int[4] { 0, 6, 3, 2 }).Contains(nstat))
                                        || ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН") && nstat == 0));
                    
                    btEdit.Enabled = (new int[3] { 0, 6, 2 }.Contains(nstat)
                                       || (UserSettings.User.StatusCode == "КД" && nstat == 7)
                                       || ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН") && nstat == 3));
                    btNew.Enabled =
                        btShow.Enabled =
                        btPrint.Enabled =
                        btCopy.Enabled =
                        btExcel.Enabled = true;

                    btnCopyActPereoc.Enabled = false;
                }
                else if (UserSettings.User.StatusCode=="РБ6")
                {
                    btDecline.Enabled = ((UserSettings.User.StatusCode == "КД" && new int[2] { 7, 2 }.Contains(nstat))
                                       || ((UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "ДМН") && nstat == 2)
                                       || (UserSettings.User.StatusCode == "РБ6" && nstat == 8));
                    btAccept.Enabled = (//new int[2] { 0, 6 }.Contains(nstat)
                                        ((nstat == 0 || (UserSettings.User.StatusCode == "МН" && nstat == 6)) && (DateTime)curRow.Cells["reqDate"].Value >= Config.curDate.Date)
                                        || (UserSettings.User.StatusCode == "КД" && new int[2] { 7, 2 }.Contains(nstat))
                                        || ((UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "ДМН") && nstat == 2)
                                        || (UserSettings.User.StatusCode == "РБ6" && nstat == 8));
                }
                else
                {
                    btEdit.Enabled =
                    btDelete.Enabled = 
                    btnCopyActPereoc.Enabled = true;

                    btChangeReqDate.Enabled =
                    btInProcess.Enabled =
                    btDecline.Enabled =
                    btAccept.Enabled =
                    btCopy.Enabled =
                    btPrint.Enabled = false;

                    if (UserSettings.User.StatusCode == "МН")
                    {
                        btEdit.Enabled =
                            btDelete.Enabled = (nstat == 2);
                    }

                    if (UserSettings.User.StatusCode == "ДМН")
                    {
                        btDecline.Enabled =
                                btAccept.Enabled = ((emptyBuhg) && (dateOfReq.Date >= Config.curDate.Date) && (nstat == 2));
                        btInProcess.Enabled = ((emptyBuhg) && (new int[5] { 3, 7, 11, 12, 8 }.Contains(nstat)));
                        btEdit.Enabled =
                            btDelete.Enabled = (nstat == 2);
                    }

                    if (UserSettings.User.StatusCode == "РКВ")
                    {
                        btDecline.Enabled =
                                btAccept.Enabled = ((emptyBuhg) && (dateOfReq.Date >= Config.curDate.Date) && (nstat == 2));
                        btInProcess.Enabled = ((emptyBuhg) && (new int[5] { 3, 7, 11, 12, 8 }.Contains(nstat)));
                        btEdit.Enabled =
                            btDelete.Enabled = (nstat == 2);
                    }

                    if (UserSettings.User.StatusCode == "КД")
                    {
                        btAccept.Enabled = ((emptyBuhg) && (dateOfReq.Date >= Config.curDate.Date)
                                && (new int[2] { 2, 7 }.Contains(nstat)));
                        btDecline.Enabled = nstat != 1;

                        btInProcess.Enabled = ((emptyBuhg) && ((nstat == 11) || (nstat == 12) || (nstat == 3)));
                        btEdit.Enabled =
                            btDelete.Enabled = (nstat == 2) || (nstat == 7);
                    }
                }

                if (Convert.ToInt32(grdRequests.CurrentRow.Cells["trequest_spis"].Value) != 0)
                {
                    foreach (Control c in pnButtons.Controls)
                    {
                        c.Enabled = false;
                    }
                    btShow.Enabled = btPrint.Enabled = true;
                    btnCopyActPereoc.Enabled = false;
                }
                        
            }

            if (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН" || UserSettings.User.StatusCode == "РКВ")
            {
                cbDep.Enabled = false;
            }

            btnCopyActPereoc.Enabled = grdRequests.RowCount > 0 && grdRequests.CurrentRow != null && Convert.ToInt32(grdRequests.CurrentRow.Cells["id_oper"].Value) == 4;
        }

        /// <summary>
        /// Получение данных для грида
        /// </summary>
        public void GetGridRequests()
        {
            //if (UserSettings.User.StatusCode == "КД")
            //{
                department.Visible = ((int)cbDep.SelectedValue  == 0 );
            //}
            if (!bgwRequests.IsBusy)
            {
                grdRequests.DataSource = null;
                Config.ChangeFormEnabled(this, false);
                pbRequests.Visible = true;
                object[] args = new object[7] { dtpStart.Value.Date, dtpFinish.Value.Date, (int)cbDep.SelectedValue, (int)cbType.SelectedValue, (int)cbCreditType.SelectedValue, (UserSettings.User.StatusCode == "МН" /*|| UserSettings.User.StatusCode == "ДМН"*/ ? UserSettings.User.Id : (int)cbMan.SelectedValue), tbEAN.Text };
                bgwRequests.RunWorkerAsync(args);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            GetGridRequests();
        }

        /// <summary>
        /// Установка фильтра
        /// </summary>
        private void SetFilter()
        {
            //Строка фильтра
            string filter;
            filter = (tbNuber.Text.Length != 0 ? "CONVERT(req_num, 'System.String') LIKE '%" + tbNuber.Text + "%' AND " : "");
            filter += (tbSupplier.Text.Length != 0 ? "post_name LIKE '%" + tbSupplier.Text + "%' AND " : "" );

            if (chkNeedKDAccept.Checked)
            {
                filter += "nstatus = 7";
            }
            else
            {
                filter += (Config.statusesFiltr.Length != 0 ? "nstatus IN (" + Config.statusesFiltr + ")" : "");
            }
 
            filter = filter.Trim();
            if (filter.Length > 3 && filter.Substring(filter.Length - 3, 3).Equals("AND"))
                filter = filter.Substring(0, filter.Length - 3);
            try
            {
                bsRequests.Filter = filter;
            }
            catch (EvaluateException)
            {
                MessageBox.Show("Некорректное значение фильтра!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            SetButtonsEnabled();
            CalculateSum();
        }

        private void tbNuber_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void tbSupplier_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void cbDep_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManagers();
            GetGridRequests();
        }

        private void cbMan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetGridRequests();
        }

        private void cbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetGridRequests();
        }

        private void cbCreditType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            creditType.Visible = (cbCreditType.SelectedIndex == 0);
            GetGridRequests();
        }

        //красим грид
        private void grdRequests_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //if ((new int[2] { 1, 3 }).Contains(int.Parse(grdRequests.Rows[e.RowIndex].Cells["id_oper"].Value.ToString())))
            //if (int.Parse(grdRequests.Rows[e.RowIndex].Cells["id_oper"].Value.ToString()) != 4)
            {
                if (grdRequests.CurrentCell != null)
                {
                    int nstatus = int.Parse(grdRequests.Rows[e.RowIndex].Cells["nstatus"].Value.ToString());

                    bool newPieceGoods = bool.Parse(grdRequests.Rows[e.RowIndex].Cells["newPieceGoods"].Value.ToString());
                    Color newPieceGoodsColor = pnNewPieceGoods.BackColor;

                    bool NoBruttoGoods = bool.Parse(grdRequests.Rows[e.RowIndex].Cells["NoBruttoGoods"].Value.ToString());
                    Color NoBruttoGoodsColor = pnNoBrutto.BackColor;                        
                    
                    if (nstatus == 0)
                    {
                        rowBK = Color.FromArgb(255, 255, 255);
                        //rowBK = Color.White;
                    }

                    if (nstatus == 2)
                    {
                        rowBK = Color.FromArgb(255, 255, 167);
                    }

                    if (nstatus == 3)
                    {
                        rowBK = Color.FromArgb(255, 209, 209);
                    }

                    if (nstatus == 11)
                    {
                        rowBK = Color.FromArgb(198, 255, 255);
                    }

                    if (nstatus == 12)
                    {
                        rowBK = Color.FromArgb(200, 255, 181);
                    }

                    if (nstatus == 6)
                    {
                        rowBK = Color.FromArgb(38, 186, 0);
                    }

                    if (nstatus == 7)
                    {
                        rowBK = Color.FromArgb(255, 186, 244);
                    }

                    if (nstatus == 8)
                    {
                        rowBK = Color.FromArgb(195, 158, 255);
                    }

                    grdRequests.Rows[e.RowIndex].DefaultCellStyle.BackColor 
                        = grdRequests.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor 
                        = rowBK;

                    for (int i = 0; grdRequests.ColumnCount > i; i++)
                    {
                        if ((grdRequests.Columns[i].Name == "kolkor") && (grdRequests.Columns[i].Visible))
                        {
                            if (newPieceGoods)
                            {
                                grdRequests.Rows[e.RowIndex].Cells[i].Style.BackColor
                                    = grdRequests.Rows[e.RowIndex].Cells[i].Style.SelectionBackColor
                                    = newPieceGoodsColor;
                            }
                        }

                        if ((grdRequests.Columns[i].Name == "weight") && (grdRequests.Columns[i].Visible))
                        {
                            if (NoBruttoGoods)
                            {
                                grdRequests.Rows[e.RowIndex].Cells[i].Style.BackColor
                                    = grdRequests.Rows[e.RowIndex].Cells[i].Style.SelectionBackColor
                                    = NoBruttoGoodsColor;
                            }
                        }                        
                    }

                }

            }
        }

        private void dtpFinish_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpFinish.Value)
            {
                dtpStart.Value = dtpFinish.Value;
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpFinish.Value)
            {
                dtpFinish.Value = dtpStart.Value;
            }
        }

        private void dtpStart_Validated(object sender, EventArgs e)
        {
            GetGridRequests();
        }

        private void btSetStatusFilter_Click(object sender, EventArgs e)
        {
            //Форма с фильтрами
            frmChooseStatus fStat = new frmChooseStatus();
            fStat.ShowDialog();
            GetStatuses();
        }

        /// <summary>
        /// Расчет Итого
        /// </summary>
        private void CalculateSum()
        {
            if (Config.dtRequests != null)
            {
                decimal sum = Config.dtRequests.DefaultView.ToTable().Select().Sum(row => row.Field<decimal>("zak_sum"));
                tbResult.Text = (sum != 0 ? sum.ToString("### ### ###.00").Trim() : "0,00");
            }
        }

        private void btClearStatusFilter_Click(object sender, EventArgs e)
        {
            lbStatuses.Text = "Все статусы";
            Config.hCntMain.SetFilterSettings(true, "");
            GetStatuses();
        }

        private void frmRequests_Shown(object sender, EventArgs e)
        {
            SetColumnsWidth();
           // grdRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void cbDep_SelectedIndexChanged(object sender, EventArgs e)
        {
           department.Visible = (cbDep.SelectedIndex == 0);
        }

        private void grdRequests_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdRequests.CurrentCell != null)
            {
                SetButtonsEnabled();
            }
        }

        private void btDecline_Click(object sender, EventArgs e)
        {
            int id = int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString());
            if (Config.openedRequests.Contains(id))
            {
                MessageBox.Show("Заявка открыта на просмотр/редактирование. Отклонение невозможно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
            int id_dep = (int)reqInfo["id_dep"];

            if ((UserSettings.User.StatusCode == "РКВ") || (UserSettings.User.StatusCode == "КД"))
            {
                
                if (id_dep == 6)
                {
                    Config.hCntAdd.ChangePereocNstatus(id, 3);                    
                }
                else
                {
                    Config.hCntMain.ChangePereocNstatus(id, 3);
                }
                GetGridRequests();
                return;
            }


            if (requestInOutStatus.EditStatus.setTurn(id, id_dep) == DialogResult.OK)
            {                
                DataTable dtStatuses = Config.hCntMain.GetReqStatuses();
                Config.curDate = Config.hCntMain.GetCurDate(true);
                Logging.StartFirstLevel(126);
                Logging.Comment("Отклонение заявки");

                Logging.Comment("Номер: " + reqInfo["req_num"].ToString() + ", Дата выдачи:" + ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy") + ", Окно: " + reqInfo["porthole"].ToString());
                Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
                Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
                Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
                Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
                Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
                Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
                Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());

                if ((int)reqInfo["id_oper"] == 3)
                {
                    DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                    if (drBonusTypes != null)
                    {
                        Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                        if ((int)reqInfo["id_TypeBonus"] == 5)
                        {
                            DataTable dtBonusProp = id_dep != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                    : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                            if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                            {
                                Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                                Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                            }
                        }
                    }
                }

                Logging.VariableChange("Статус заявки: ", "Отклонена",
                                                            (dtStatuses.Select("id = " + reqInfo["nstatus"].ToString())[0]["cName"].ToString()));
                if (requestInOutStatus.TempValues.comment.Trim().Length != 0)
                {
                    Logging.Comment("Комментарий: " + requestInOutStatus.TempValues.comment.Trim());
                }
                Logging.Comment("Дата отклонения: " + Config.curDate.ToString());
                Logging.Comment("Завершение операции \"Отклонение заявки\"");
                Logging.StopFirstLevel();
                GetGridRequests();
            }
        }

        private void btInProcess_Click(object sender, EventArgs e)
        {
            int id = int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString());

            DataTable dtOwnLink = Config.hCntMain.getLinkTRequest(id, 4);
            if (dtOwnLink != null && dtOwnLink.Rows.Count > 0)
            {
                MessageBox.Show("Сформирована и потверждена заявка на \"Поставщика из настроек\".\n Редактирование невозможно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dtLinkTRequest = Config.hCntMain.getLinkTRequest(id,1);
            if(dtLinkTRequest!=null && dtLinkTRequest.Rows.Count>0)
                if ((UserSettings.User.StatusCode == "РКВ") || (UserSettings.User.StatusCode == "КД"))
                    return;

            
            if (Config.openedRequests.Contains(id))
            {
                MessageBox.Show("Заявка открыта на просмотр/редактирование. Смена статуса невозможна", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
            int id_dep = (int)reqInfo["id_dep"];

            if ((UserSettings.User.StatusCode == "РКВ") || (UserSettings.User.StatusCode == "КД"))
            {
                if (id_dep == 6)
                {
                    Config.hCntAdd.ChangePereocNstatus(id, 2);
                }
                else
                {
                    Config.hCntMain.ChangePereocNstatus(id, 2);
                }                
                GetGridRequests();
                return;
            }

            if (requestInOutStatus.EditStatus.setEditStatus(id, id_dep) == DialogResult.OK)
            {               
                DataTable dtStatuses = Config.hCntMain.GetReqStatuses();
                Config.curDate = Config.hCntMain.GetCurDate(true);
                Logging.StartFirstLevel(128);
                Logging.Comment("Смена статуса заявки");

                Logging.Comment("Номер: " + reqInfo["req_num"].ToString() + ", Дата выдачи:" + ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy") + ", Окно: " + reqInfo["porthole"].ToString());
                Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
                Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
                Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
                Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
                Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
                Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
                Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());

                if ((int)reqInfo["id_oper"] == 3)
                {
                    DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                    if (drBonusTypes != null)
                    {
                        Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                        if ((int)reqInfo["id_TypeBonus"] == 5)
                        {
                            DataTable dtBonusProp = id_dep != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                    : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                            if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                            {
                                Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                                Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                            }
                        }
                    }
                }

                Logging.VariableChange("Статус заявки: ", "В процессе",
                                                            (dtStatuses.Select("id = " + reqInfo["nstatus"].ToString())[0]["cName"].ToString()));
                if (requestInOutStatus.TempValues.comment != null && requestInOutStatus.TempValues.comment.Trim().Length != 0)
                {
                    Logging.Comment("Комментарий: " + requestInOutStatus.TempValues.comment.Trim());
                }
                Logging.Comment("Дата смены статуса: " + Config.curDate.ToString());
                Logging.Comment("Завершение операции \"Смена статуса заявки\"");
                Logging.StopFirstLevel();
                GetGridRequests();
            }
        }

        private void btChangeReqDate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString());

            DataTable dtLinkTRequest = Config.hCntMain.getLinkTRequest(id, 1);
            if (dtLinkTRequest != null && dtLinkTRequest.Rows.Count > 0)
            {
                int idTRequestOtguz = int.Parse(dtLinkTRequest.Rows[0]["id_TRequestOtgruz"].ToString());
                dtLinkTRequest = Config.hCntMain.getLinkTRequest(idTRequestOtguz, 3);
                if (dtLinkTRequest != null && dtLinkTRequest.Rows.Count > 0)
                {
                    //if ((UserSettings.User.StatusCode == "РКВ") || (UserSettings.User.StatusCode == "КД"))
                    return;
                }
            }

            if (Config.openedRequests.Contains(id))
            {
                MessageBox.Show("Заявка открыта на просмотр/редактирование. Смена даты выдачи невозможна", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
            int id_dep = (int)reqInfo["id_dep"];

            if (requestInOutStatus.EditStatus.setEditDate(id, id_dep) == DialogResult.OK)
            {                
                Config.curDate = Config.hCntMain.GetCurDate(true);
                Logging.StartFirstLevel(129);
                Logging.Comment("Изменение даты выдачи заявки ");

                Logging.Comment("Номер: " + reqInfo["req_num"].ToString() + ", Окно: " + reqInfo["porthole"].ToString());
                Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
                Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
                Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
                Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
                Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
                Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
                Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());

                if ((int)reqInfo["id_oper"] == 3)
                {
                    DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                    if (drBonusTypes != null)
                    {
                        Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                        if ((int)reqInfo["id_TypeBonus"] == 5)
                        {
                            DataTable dtBonusProp = id_dep != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                    : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                            if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                            {
                                Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                                Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                            }
                        }
                    }
                }

                Logging.VariableChange("Дата выдачи заявки: ", requestInOutStatus.TempValues.newSelectedDate.ToString("dd.MM.yyyy"),
                                                                ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy"));
                Logging.Comment("Дата изменения даты выдачи: " + Config.curDate.ToString());
                Logging.Comment("Завершение операции \"Изменение даты выдачи заявки \"");
                Logging.StopFirstLevel();
                GetGridRequests();
            }
        }
        
        private void btDelete_Click(object sender, EventArgs e)
        {
            DeleteReq(int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString()));
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            if (Config.linkToCurrentRequest != null)
            {
                MessageBox.Show("Для копирования заявки необходимо закрыть вкладки создания/редактирования заявок!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CopyRequest(int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString()));
        }

        #region Процедуры кнопок по работе с заявкой

        /// <summary>
        /// Удаление заявки
        /// </summary>
        /// <param name="id">id в trequest</param>
        private void DeleteReq(int id)
        {
            if (grdRequests.CurrentRow.Cells["id_priem"].Value.GetType() != typeof(DBNull)
                && grdRequests.CurrentRow.Cells["data_factout"].Value.GetType() != typeof(DBNull))
            {
                MessageBox.Show("Невозможно удалить заявку!\nНачата приемка товара!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (Config.openedRequests.Contains(id))
                {
                    MessageBox.Show("Заявка открыта на просмотр/редактирование. Удаление невозможно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить заявку № " + id.ToString() + "?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
                        int id_dep = (int)reqInfo["id_dep"];
                        if (int.Parse(grdRequests.CurrentRow.Cells["id_oper"].Value.ToString()) != 4)
                        {
                            DataTable dtStatuses = Config.hCntMain.GetReqStatuses();
                            int nstatus = int.Parse(reqInfo["nstatus"].ToString());
                            Logging.StartFirstLevel(102);
                            Logging.Comment("Удаление заявки");

                            Logging.Comment("Номер: " + reqInfo["req_num"].ToString() + ", Дата выдачи:" + reqInfo["req_date"].ToString() + ", Окно: " + reqInfo["porthole"].ToString());
                            Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
                            Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
                            Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
                            Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
                            Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
                            Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
                            Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());
                            //Logging.Comment("Статус заявки: " + (nstatus == 12 ? "Подтверждена"
                            //                                        : nstatus == 11 ? "Возврат"
                            //                                           : dtStatuses.Select("id = " + nstatus.ToString())[0]["cName"].ToString()));
                            
                            //if (UserSettings.User.StatusCode == "МН" && nstatus != 0)
                            //{
                            //    Logging.Comment("Дата передачи РКВ: " + dtpDateOut.Value.ToString());
                            //}

                            if ((int)reqInfo["id_oper"] == 3)
                            {
                                DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                                if (drBonusTypes != null)
                                {
                                    Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                                    if ((int)reqInfo["id_TypeBonus"] == 5)
                                    {
                                        DataTable dtBonusProp = id_dep != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                                : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                                        if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                                        {
                                            Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                                            Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                                        }
                                    }
                                }
                            }

                            Logging.Comment("Товары:");

                            DataTable dtGoodsInfo = id_dep != 6 ? Config.hCntMain.GetRequestBody((int)reqInfo["req_num"], false)
                                                                : Config.hCntAdd.GetRequestBody((int)reqInfo["req_num"], false);
                            if (dtGoodsInfo != null && dtGoodsInfo.Rows.Count > 0)
                            {
                                DataTable dtCoutry = Config.hCntMain.GetSubjectList();
                                DataTable dtUnit = Config.hCntMain.GetUnitList();
                                DataTable dtNds = Config.hCntMain.GetNdsList();
                                DataTable dtTu = id_dep != 6 ? Config.hCntMain.GetTU(id_dep) : Config.hCntAdd.GetTU(id_dep);
                                DataTable dtInv = id_dep != 6 ? Config.hCntMain.GetInvGrp(id_dep) : Config.hCntAdd.GetInvGrp(id_dep);
                                
                                foreach (DataRow drGood in dtGoodsInfo.Rows)
                                {
                                    Logging.Comment("ID товара: " + drGood["id_tovar"].ToString() + ", EAN: " + drGood["ean"].ToString().Trim() + ", Товар: " + drGood["cname"].ToString().Trim());
                                    Logging.Comment("Новый товар: " + ((int)drGood["nprimech"] == 1 ? "Да" : "Нет" ));
                                    Logging.Comment("Страна/субъект: " + dtCoutry.Select("id = " + drGood["id_subject"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + drGood["id_subject"].ToString());
                                    Logging.Comment("Ед. изм.: " + dtUnit.Select("id = " + drGood["id_unit"].ToString())[0]["cunit"].ToString().Trim() + ", ID: " + drGood["id_unit"].ToString());
                                    Logging.Comment("НДС: " + dtNds.Select("id = " + drGood["id_nds"].ToString())[0]["nds"].ToString().Trim() + ", ID: " + drGood["id_nds"].ToString());
                                    Logging.Comment("Т/У группа: " + dtTu.Select("id = " + drGood["id_grp1"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + drGood["id_grp1"].ToString());
                                    Logging.Comment("Инв. группа: " + dtInv.Select("id = " + drGood["id_grp2"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + drGood["id_grp2"].ToString());
                                    Logging.Comment("Затарка: " + drGood["Zatar"].ToString() + ", Тара: " + drGood["Tara"].ToString());
                                    Logging.Comment("Заказ: " + drGood["zakaz"].ToString());
                                    Logging.Comment("Цена зак.: с ндс: " + drGood["zcena"].ToString() + ", без ндс: " + drGood["zcenabnds"].ToString());
                                    Logging.Comment("Цена продажи: " + drGood["rcena"].ToString() + ", Срок хранения: " + drGood["PeriodOfStorage"].ToString());
                                    Logging.Comment("Полочное пр-во: " + drGood["ShelfSpace"].ToString() + ", Примечание: " + drGood["cprimech"].ToString());
                                    Logging.Comment("Ограничение: " + drGood["CauseOfDecline"].ToString());
                                    Logging.Comment("% наценки: " + ((decimal)drGood["procent"]).ToString("#0.00"));
                                }
                            }

                            Logging.Comment("Завершение операции \"Удаление заявки\"");
                            Logging.StopFirstLevel();
                        }
                        else
                        {
                            DataTable dtPereocInfo = id_dep != 6 ? Config.hCntMain.GetPereoc((int)grdRequests.CurrentRow.Cells["id_req"].Value)
                                                                    : Config.hCntAdd.GetPereoc((int)grdRequests.CurrentRow.Cells["id_req"].Value);
                            if(dtPereocInfo != null && dtPereocInfo.Rows.Count > 0)
                            {
                                Logging.StartFirstLevel(32);
                                Logging.Comment("Удаление акта переоценки");
                                Logging.Comment("Номер акта: " + reqInfo["req_num"].ToString());
                                Logging.Comment("Тип: " + ((int)dtPereocInfo.Rows[0]["cprimech"] == 1 ? "Переоценка" : "Дооценка"));
                                Logging.Comment("Дата создания: " + ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy"));
                                Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
                                Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
                                Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());

                                foreach (DataRow drPereocGood in dtPereocInfo.Rows)
                                {
                                    Logging.Comment("Товар: ID: " + drPereocGood["id_tovar"].ToString() + ", EAN: " + drPereocGood["ean"].ToString().Trim() + ", Наименование: " + drPereocGood["cName"].ToString().Trim());
                                    Logging.Comment("Остаток: " + drPereocGood["ostOnDate"].ToString());
                                    Logging.Comment("Цена продажи: " + drPereocGood["rcena"].ToString() + ", Цена закупки: " + drPereocGood["zcena"].ToString());
                                    Logging.Comment("Новая цена: " + drPereocGood["zcenabnds"].ToString());
                                }

                                Logging.Comment("Завершение операции \"Удаление акта переоценки\"");
                                Logging.StopFirstLevel();
                            }
                        }

                        if (id_dep != 6)
                        {
                            Config.hCntMain.DeleteRequest(id);
                        }
                        else
                        {
                            Config.hCntAdd.DeleteRequest(id);
                        }
                        GetGridRequests();
                    }
                }
            }
        }

        /// <summary>
        /// Копирование заявки
        /// </summary>
        /// <param name="id">Id заявки</param>
        private void CopyRequest(int id)
        {
            if (MessageBox.Show("Копировать заявку?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //id формы = id заявки с добавленой в конце 1
            ((Main)this.MdiParent).SetTab("Заявка № " + id.ToString() + ". Копирование.", "frmEditRequest", new object[3] { 1, id, true }, int.Parse(id.ToString() + "1"));
            //GetGridRequests();


            //DataRow drReqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
            //DataTable dtStatuses = Config.hCntMain.GetReqStatuses();
            //Config.curDate = Config.hCntMain.GetCurDate(false);
            //int nstatus = int.Parse(drReqInfo["nstatus"].ToString());

            //Logging.StartFirstLevel(593);
            //Logging.Comment("Копирование заявки");
            //Logging.Comment("Номер заявки: " + drReqInfo["req_num"].ToString());
            //Logging.Comment("Дата заявки: " + ((DateTime)drReqInfo["req_date"]).ToString("dd.MM.yyyy"));
            //Logging.Comment("Статус заявки: " + (dtStatuses.Select("id = " + nstatus.ToString())[0]["cName"].ToString()));
            //Logging.Comment("Отдел: " + drReqInfo["dep_name"].ToString() + ", ID: " + drReqInfo["id_dep"].ToString());
            //Logging.Comment("Дата копирования: " + Config.curDate.ToString("dd.MM.yyyy"));
            //Logging.Comment("ФИО менеджера: " + UserSettings.User.FullUsername);
            //Logging.Comment("Завершение операции \"Копирование заявки\"");
            //Logging.StopFirstLevel();
        }


        #endregion

        private void btShow_Click(object sender, EventArgs e)
        {
            ShowRequest(int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString()), int.Parse(grdRequests.CurrentRow.Cells["id_oper"].Value.ToString()));
        }
        
        /// <summary>
        /// Просмотр заявки/переоценки
        /// </summary>
        /// <param name="id">id заявки</param>
        /// <param name="id_oper">тип заявки</param>
        private void ShowRequest(int id, int id_oper)
        {
            if (id_oper == 4)
            {
                ((Main)this.MdiParent).SetTab("Переоценка №" + id.ToString() + ". Просмотр.", "frmPereoc", new object[] { 0, id, /*(int)cbDep.SelectedValue*/ (int)Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index]["id_dep"] }, id);
            }
            else
            {
                ((Main)this.MdiParent).SetTab("Заявка №" + id.ToString() + ". Просмотр.", "frmEditRequest", new object[] { 0, id, false }, id);
            }

            if (!Config.openedRequests.Contains(id))
            {
                Config.openedRequests.Add(id);
            }
        }

        /// <summary>
        /// Редактирование заявки/переоценки
        /// </summary>
        /// <param name="id">id заявки</param>
        /// <param name="id_oper">тип заявки</param>
        private void EditRequest(int id, int id_oper, int id_dep)
        {
            if (id_oper == 4)
            {
                ((Main)this.MdiParent).SetTab("Переоценка №" + id.ToString() + ". Редактирование.", "frmPereoc", new object[] { 1, id, id_dep, DateTime.Parse(grdRequests.CurrentRow.Cells["reqDate"].Value.ToString()) }, id);
            }
            else
            {
                if (Config.linkToCurrentRequest != null)
                {
                    MessageBox.Show("Для редактирования заявки необходимо закрыть вкладки создания/редактирования заявок!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ((Main)this.MdiParent).SetTab("Заявка №" + id.ToString() + ". Редактирование.", "frmEditRequest", new object[] { 2, id, false}, id);
                //MessageBox.Show("Coming soon");
            }

            Config.openedRequests.Add(id);
        }

        private void grdRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ShowRequest(int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString()), int.Parse(grdRequests.CurrentRow.Cells["id_oper"].Value.ToString()));
            }
        }

        private void frmRequests_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Сохраняем позиции календарей
            Config.hCntMain.SetDefaultDates("dreqst", dtpStart.Value.Date.ToString("yyyy-MM-dd"));
            Config.hCntMain.SetDefaultDates("dreqfin", dtpFinish.Value.Date.ToString("yyyy-MM-dd"));
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];

            Logging.StartFirstLevel(335);
            Logging.Comment("Печать заявки");
            Logging.Comment("Заявка " + reqInfo["req_num"].ToString()
                            + " от " + ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy")
                            + " выведена на печать сотрудником в режиме " + UserSettings.User.StatusCode
                            + ", ФИО: " + UserSettings.User.FullUsername);
            Logging.Comment("Окно: " + reqInfo["porthole"].ToString());
            Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + reqInfo["id_dep"].ToString());
            Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
            Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
            Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
            Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
            Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
            Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());

            if ((int)reqInfo["id_oper"] == 3)
            {
                DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                if (drBonusTypes != null)
                {
                    Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                    if ((int)reqInfo["id_TypeBonus"] == 5)
                    {
                        DataTable dtBonusProp = (int)reqInfo["id_dep"] != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                        if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                        {
                            Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                            Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                        }
                    }
                }
            }

            RequestPrint.Print.Print.showReport((int)reqInfo["req_num"], (int)reqInfo["id_dep"]);
            Logging.Comment("Завершение операции \"Печать заявки\"");
            Logging.StopFirstLevel();
        }

        private void bgwRequests_DoWork(object sender, DoWorkEventArgs e)
        {
            Config.dtProperties = Config.hCntMain.GetProperties();
            object[] bgwArgs = e.Argument as object[];

            if ((int)bgwArgs[2] != 6)
            {
                Config.dtRequests = Config.hCntMain.GetRequests((DateTime)bgwArgs[0], (DateTime)bgwArgs[1], (int)bgwArgs[2], (int)bgwArgs[3], (int)bgwArgs[4], (int)bgwArgs[5], (string)bgwArgs[6]);

                if ((int)bgwArgs[2] == 0 && Config.dtProperties.Select("TRIM(id_val) = 'idusr' AND TRIM(val) = '6'").Count() > 0)
                {
                    DataTable dtAddReq = Config.hCntAdd.GetRequests((DateTime)bgwArgs[0], (DateTime)bgwArgs[1], 6, (int)bgwArgs[3], (int)bgwArgs[4], (int)bgwArgs[5], (string)bgwArgs[6]);

                    if (dtAddReq != null && dtAddReq.Rows.Count > 0)
                    {
                        Config.dtRequests.Merge(dtAddReq);
                    }
                }
            }
            else
            {
                Config.dtRequests = Config.hCntAdd.GetRequests((DateTime)bgwArgs[0], (DateTime)bgwArgs[1], (int)bgwArgs[2], (int)bgwArgs[3], (int)bgwArgs[4], (int)bgwArgs[5], (string)bgwArgs[6]);
            }

            if (Config.dtRequests != null && Config.dtRequests.Rows.Count > 0)
            {
                Config.dtRequests.Columns.Add(new DataColumn("name_credit", typeof(string)));

                DataRow[] drSelectedType;

                foreach (DataRow dr in Config.dtRequests.Rows)
                {
                    drSelectedType = dtCreditTypes.Select("id = " + dr["credit_type"].ToString());

                    if (drSelectedType.Count() > 0)
                    {
                        dr["name_credit"] = drSelectedType[0]["cname"].ToString();
                    }
                    else
                    {
                        dr["name_credit"] = "";
                    }
                }
            }

            bsRequests.DataSource = Config.dtRequests;
            if(Config.dtRequests != null)
                bsRequests.Sort = "req_date ASC, req_num ASC";
        }

        private void bgwRequests_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grdRequests.AutoGenerateColumns = false;
            grdRequests.DataSource = bsRequests;
            Config.ChangeFormEnabled(this, true);
            pbRequests.Visible = false;
            if (tbEAN.Text.Trim().Length != 0 && grdRequests.Rows.Count == 0)
            {
                MessageBox.Show("Введенный код не найден!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tbEAN.Text = "";
            }

            SetFilter();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            EditRequest(int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString()), int.Parse(grdRequests.CurrentRow.Cells["id_oper"].Value.ToString()), (int)Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index]["id_dep"]);
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            if (Config.linkToCurrentRequest != null)
            {
                MessageBox.Show("Для создания новой заявки необходимо закрыть вкладки создания/редактирования заявок!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ((Main)this.MdiParent).SetTab("Создание заявки.", "frmEditRequest", new object[] { 1, 0, false }, 0);
        }

        private void btAccept_Click(object sender, EventArgs e)
        {
            AcceptRequest();
        }

        private void AcceptRequest()
        {
            bool isWeInOut = false;
            DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
            int id = int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString());
            int id_dep = (int)reqInfo["id_dep"];
            int nnstatus = reqInfo["nstatus"] != DBNull.Value ? Convert.ToInt32(reqInfo["nstatus"].ToString()) : 0;
            object realiz = 0;

            DataTable dtStatuses = Config.hCntMain.GetReqStatuses();
            Config.curDate = Config.hCntMain.GetCurDate(true);
            if (Config.openedRequests.Contains(id))
            {
                MessageBox.Show("Заявка открыта на просмотр/редактирование. Подтверждение невозможно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (UserSettings.User.StatusCode == "ДМН"
                || UserSettings.User.StatusCode == "КД"
                || UserSettings.User.StatusCode == "РКВ")
            {
                #region "Доработка от 10.08.2017"
                
                int reqIdPost = int.Parse(grdRequests.CurrentRow.Cells["id_post"].Value.ToString());
                DataTable dtTmp = Config.hCntMain.getAllSettingsWeInOut(2, "prhp");
                isWeInOut = (dtTmp != null && dtTmp.Rows.Count != 0 && dtTmp.Select("value = '" + reqIdPost + "'").Count() != 0);
                if (isWeInOut)
                {
                    string listEanZeroTovar = "";
                    bool isZeroTovar = false;

                    DataTable dtReqGoods = (id_dep != 6
                                ? Config.hCntMain.GetRequestBody(id, false, (DateTime)grdRequests.CurrentRow.Cells["reqDate"].Value)
                                : Config.hCntAdd.GetRequestBody(id, false, (DateTime)grdRequests.CurrentRow.Cells["reqDate"].Value));

                    realiz = dtReqGoods.Compute("SUM(PlanRealiz)", "");

                    DataTable dtListTovarWeInOut = Config.hCntMain.getWeInOutTovarList(0, DateTime.Now, DateTime.Now, "", "", id, 4);

                    foreach (DataRow r in dtReqGoods.Rows)
                    {
                        
                        object sumTovar = 0;
                        if (dtListTovarWeInOut != null && dtListTovarWeInOut.Rows.Count > 0)
                            sumTovar = dtListTovarWeInOut.Compute("SUM(Netto)", "id_tovar = " + r["id_tovar"].ToString());

                        if (sumTovar == DBNull.Value) sumTovar = 0;

                        if (decimal.Parse(r["zakaz"].ToString()) != decimal.Parse(sumTovar.ToString()))
                        {
                            listEanZeroTovar += "," + r["ean"].ToString();
                            isZeroTovar = true;
                        }
                    }

                    if (isZeroTovar)
                    {
                        listEanZeroTovar = listEanZeroTovar.Remove(0, 1);
                        MessageBox.Show("В заявке количество товара из накладных\n не совпадает с количеством\nзаказаного товара.\nдля товара:"
                            +listEanZeroTovar+".\nПодтверждение заявки невозможно.", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                            
                        return;
                    }

                }
                #endregion
            }
            
            //проверка ограничений для МН
            if (UserSettings.User.StatusCode == "МН" /*|| UserSettings.User.StatusCode == "ДМН"*/)
            {
                AcceptRequestMN(reqInfo, id, id_dep, dtStatuses);
            }
            else if (nnstatus == 2 && decimal.Parse(realiz.ToString()) != 0 && UserSettings.User.StatusCode == "ДМН")
            {
                AcceptRequestMN(reqInfo, id, id_dep, dtStatuses, 7);
            }
            else
            {
                if (requestInOutStatus.EditStatus.setVerify(id, id_dep) == DialogResult.OK)
                {
                    Logging.StartFirstLevel(127);
                    Logging.Comment("Подтверждение заявки");

                    Logging.Comment("Номер: " + reqInfo["req_num"].ToString() + ", Дата выдачи:" + ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy") + ", Окно: " + reqInfo["porthole"].ToString());
                    Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
                    Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
                    Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
                    Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
                    Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
                    Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
                    Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());

                    if ((int)reqInfo["id_oper"] == 3)
                    {
                        DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                        if (drBonusTypes != null)
                        {
                            Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                            if ((int)reqInfo["id_TypeBonus"] == 5)
                            {
                                DataTable dtBonusProp = id_dep != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                        : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                                if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                                {
                                    Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                                    Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                                }
                            }
                        }
                    }

                    Logging.VariableChange("Статус заявки: ", (dtStatuses.Select("id = " + (requestInOutStatus.TempValues.newStatus == 1 ? "12" : requestInOutStatus.TempValues.newStatus.ToString()))[0]["cName"].ToString()),
                                                                (dtStatuses.Select("id = " + reqInfo["nstatus"].ToString())[0]["cName"].ToString()));
                    if (requestInOutStatus.TempValues.comment.Trim().Length != 0)
                    {
                        Logging.Comment("Комментарий: " + requestInOutStatus.TempValues.comment.Trim());
                    }
                    Logging.Comment("Дата подтверждения: " + Config.curDate.ToString());

                    Logging.Comment("Завершение операции \"Подтверждение заявки\"");
                    Logging.StopFirstLevel();

                    String status = UserSettings.User.StatusCode;
                    if ((int)reqInfo["id_oper"] == 1)
                    {

                        if (status == "КД" || status == "ДМН" || status == "РКВ")
                        {
                            if (isWeInOut)
                            {
                                createSubRequest();
                            }
                        }
                    }

                    if ((int)reqInfo["id_oper"] == 4)
                    {
                        if (UserSettings.User.StatusCode == "РКВ")
                        {
                            if (id_dep == 6)
                            {
                                Config.hCntAdd.ChangePereocNstatus(id, 7);
                            }
                            else
                            {
                                Config.hCntMain.ChangePereocNstatus(id, 7);
                            }
                            
                            GetGridRequests();
                            return;
                        }

                        if (UserSettings.User.StatusCode == "КД")
                        {
                            
                            if (id_dep == 6)
                            {                                
                                Config.hCntAdd.ChangePereocNstatus(id, 1);
                            }
                            else
                            {
                                Config.hCntMain.ChangePereocNstatus(id, 1);
                            }

                            //if (isWeInOut)
                            //{
                            //    createSubRequest();
                            //}

                            GetGridRequests();
                            return;
                        }
                    }
                }
            }
            GetGridRequests();
        }

        private void AcceptRequestMN(DataRow reqInfo, int id, int id_dep, DataTable dtStatuses, int nstatusReq = 2)
        {
            //int nstatusReq = 2;
            string CauseOfDecline = "";

            DateTime dateReq = DateTime.Parse(grdRequests.CurrentRow.Cells["reqDate"].Value.ToString()).Date;

            //if (dateReq < Config.curDate
            //    && MessageBox.Show("Сменить дату выдачи заявки на текущую?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    dateReq = Config.curDate;
            //}

            DataTable dtRequestBody = id_dep != 6 ? Config.hCntMain.GetRequestBody(id, false, dateReq)
                                                                        : Config.hCntAdd.GetRequestBody(id, false, dateReq);

            if (int.Parse(grdRequests.CurrentRow.Cells["id_oper"].Value.ToString()) == 1
                       && (Config.hCntMain.GetSettings("ldep").Select("value = '" + id_dep.ToString() + "'").Count() != 0
                       && Config.hCntMain.GetSettings("Bpst").Select("value = '" + grdRequests.CurrentRow.Cells["id_post"].Value.ToString() + "'").Count() == 0))
            {
                if (dtRequestBody != null && dtRequestBody.Rows.Count != 0)
                {
                    string idToDelete;
                    string causeOD = "";
                    int rCheckResult = Config.checkRestriction(ref dtRequestBody, id_dep, id, (DateTime)grdRequests.CurrentRow.Cells["reqDate"].Value, true, out idToDelete);
                    if (rCheckResult == 1)
                    {
                        nstatusReq = 6;
                        foreach (DataRow drGoods in dtRequestBody.Select("LEN(TRIM(CauseOfDecline)) <> 0"))
                        {
                            if (idToDelete.Contains(drGoods["idReq"].ToString().Trim()))
                            {
                                causeOD = drGoods["CauseOfDecline"].ToString().Trim();
                            }
                            else
                            {
                                causeOD = " ";
                            }
                            //Формирует строку вида id|причина$, где $ переход на новую строку
                            CauseOfDecline += drGoods["idReq"].ToString().Trim() + "|" + causeOD + "$";
                        }
                        CauseOfDecline = CauseOfDecline.Remove(CauseOfDecline.Length - 1, 1);
                    }
                }
            }



            if (nstatusReq != 6)
            {
                // Config.hCntMain.SendManagerReq(id, nstatusReq, CauseOfDecline);
                DataRow reqSet = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
                //int creditPeriod = 0;
                int maxIdUnit = 0;
                int minIdUnit = 0;
                int idUnit;
                int idTReq;
                System.Collections.ArrayList idTRequests = new System.Collections.ArrayList();

                DateTime? BeginOfPeriod;
                DateTime? EndOfPeriod;
                DateTime periodDateTime;

                decimal? PlanRealiz;
                decimal outPlan;

                int idCreditType = (new int[2] { 1, 2 }.Contains((int)cbCreditType.SelectedValue) ? 5 : 1);


                if (Config.hCntMain.GetSettings("div").Select("value = '" + id_dep + "'").Count() > 0
                    && int.Parse(reqSet["id_oper"].ToString()) != 2
                    && int.Parse(reqSet["id_oper"].ToString()) != 8)
                {
                    maxIdUnit = 2;
                    minIdUnit = 1;
                }

                DataRow[] goodsToAdd;

                idTReq = (int)reqSet["req_num"];

                //Сохранение на сервер
                for (idUnit = minIdUnit; idUnit <= maxIdUnit; idUnit++)
                {
                    //Если указано в настройках - разбиваем накладную на вес/шт
                    goodsToAdd = (idUnit == 0 ? dtRequestBody.Select("zakaz > 0") : dtRequestBody.Select("id_unit = " + idUnit.ToString() + " AND zakaz > 0"));
                    if (goodsToAdd.Count() != 0)
                    {
                        if (id_dep != 6)
                        {
                            idTReq = Config.hCntMain.SetRequestHead(idTReq, (int)reqSet["id_dep"], (DateTime)reqSet["req_date"], (int)reqSet["id_post"], (int)reqSet["id_man"],
                                                            nstatusReq, int.Parse(reqSet["ntypeorg"].ToString()), reqSet["cprimech"].ToString(), (int)reqSet["id_oper"],
                                                            int.Parse(reqSet["porthole"].ToString()), (int)reqSet["id_TypeBonus"], idUnit,
                                                            (int)reqSet["credit_type"], (int)reqSet["CreditPeriod"],
                                                            -1, -1, "");

                        }
                        else
                        {
                            idTReq = Config.hCntMain.SetRequestHead(idTReq, (int)reqSet["id_dep"], (DateTime)reqSet["req_date"], (int)reqSet["id_post"], (int)reqSet["id_man"],
                                                            nstatusReq, int.Parse(reqSet["ntypeorg"].ToString()), reqSet["cprimech"].ToString(), (int)reqSet["id_oper"],
                                                            int.Parse(reqSet["porthole"].ToString()), (int)reqSet["id_TypeBonus"], idUnit,
                                                            (int)reqSet["credit_type"], (int)reqSet["CreditPeriod"],
                                                            -1, -1, "");

                        }


                        foreach (DataRow drGood in goodsToAdd)
                        {
                            if (//tbOtsechStart.Visible &&
                                DateTime.TryParse(drGood["BeginOfPeriod"].ToString(), out periodDateTime))
                            {
                                BeginOfPeriod = periodDateTime;
                            }
                            else
                            {
                                BeginOfPeriod = null;
                            }

                            if (//tbOtsechFinish.Visible &&
                                DateTime.TryParse(drGood["EndOfPeriod"].ToString(), out periodDateTime))
                            {
                                EndOfPeriod = periodDateTime;
                            }
                            else
                            {
                                EndOfPeriod = null;
                            }

                            PlanRealiz = null;
                            if (decimal.TryParse(drGood["PlanRealiz"].ToString(), out outPlan))
                            {
                                if (outPlan != -1)
                                    PlanRealiz = outPlan;
                            }

                            if (id_dep != 6)
                            {
                                Config.hCntMain.SetRequestBody((int)drGood["idReq"], idTReq, (int)drGood["id_tovar"], (int)drGood["id_unit"], decimal.Parse(drGood["Tara"].ToString()), (decimal)drGood["zakaz"],
                                                                (decimal)drGood["zcena"], (decimal)drGood["rcena"], (decimal)drGood["zcenabnds"], (int)drGood["nprimech"],
                                                                (string)drGood["cprimech"].ToString().Replace("НТ ", "").Replace("НЦ ", ""), (int)drGood["id_subject"], BeginOfPeriod, EndOfPeriod, "",
                                                                (string)drGood["PeriodOfStorage"], PlanRealiz, "", idCreditType, (string)drGood["ean"], (decimal)drGood["ShelfSpace"], (bool)drGood["isTransparent"]);
                            }
                            else
                            {
                                Config.hCntAdd.SetRequestBody((int)drGood["idReq"], idTReq, (int)drGood["id_tovar"], (int)drGood["id_unit"], decimal.Parse(drGood["Tara"].ToString()), (decimal)drGood["zakaz"],
                                                                (decimal)drGood["zcena"], (decimal)drGood["rcena"], (decimal)drGood["zcenabnds"], (int)drGood["nprimech"],
                                                                (string)drGood["cprimech"].ToString().Replace("НТ ", "").Replace("НЦ ", ""), (int)drGood["id_subject"], BeginOfPeriod, EndOfPeriod, "",
                                                                (string)drGood["PeriodOfStorage"], PlanRealiz, "", idCreditType, (string)drGood["ean"], (decimal)drGood["ShelfSpace"], (bool)drGood["isTransparent"]);
                            }

                        }

                        idTRequests.Add(idTReq);
                        idTReq = 0;
                    }
                }

                if (nstatusReq != 0)
                {
                    for (int i = 0; i < idTRequests.Count; i++)
                    {
                        if (MessageBox.Show("Хотите распечатать заявку?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            break;
                        }

                        Logging.StartFirstLevel(335);
                        Logging.Comment("Печать заявки");
                        Logging.Comment("Заявка " + idTRequests[i].ToString()
                                        + " от " + ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy")
                                        + " выведена на печать сотрудником в режиме " + UserSettings.User.StatusCode
                                        + ", ФИО: " + UserSettings.User.FullUsername);
                        Logging.Comment("Окно: " + reqInfo["porthole"].ToString());
                        Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
                        Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
                        Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
                        Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
                        Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
                        Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
                        Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());

                        if ((int)reqInfo["id_oper"] == 3)
                        {
                            DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                            if (drBonusTypes != null)
                            {
                                Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                                if ((int)reqInfo["id_TypeBonus"] == 5)
                                {
                                    DataTable dtBonusProp = (int)reqInfo["id_dep"] != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                            : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                                    if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                                    {
                                        Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                                        Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                                    }
                                }
                            }
                        }

                        RequestPrint.Print.Print.showReport((int)idTRequests[i], id_dep);
                        Logging.Comment("Завершение операции \"Печать заявки\"");
                        Logging.StopFirstLevel();
                    }
                }

            }
            else
            {
                if (id_dep != 6)
                    Config.hCntMain.SendManagerReq(id, nstatusReq, CauseOfDecline);
                else
                    Config.hCntAdd.SendManagerReq(id, nstatusReq, CauseOfDecline);
            }

            int nstatus = int.Parse(reqInfo["nstatus"].ToString());
            Logging.StartFirstLevel(594);
            Logging.Comment("Передача заявки руководителю");
            Logging.Comment("Номер: " + reqInfo["req_num"].ToString() + ", Дата выдачи:" + ((DateTime)reqInfo["req_date"]).ToString("dd.MM.yyyy") + ", Окно: " + reqInfo["porthole"].ToString());
            Logging.Comment("Отдел: " + reqInfo["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
            Logging.Comment("Тип кредитования: " + reqInfo["name_credit"].ToString().Trim() + ", ID: " + reqInfo["credit_type"].ToString());
            Logging.Comment("Поставщик: " + reqInfo["post_name"].ToString().Trim() + ", ID: " + reqInfo["id_post"].ToString());
            Logging.Comment("Тип заявки: " + reqInfo["op_name"].ToString().Trim() + ", ID " + reqInfo["id_oper"].ToString());
            Logging.Comment("ЮЛ: " + reqInfo["ul"].ToString().Trim() + ", ID: " + reqInfo["ntypeorg"].ToString());
            Logging.Comment("Примечание: " + reqInfo["cprimech"].ToString().Trim());
            Logging.Comment("Менеджер: " + reqInfo["man_name"].ToString().Trim() + ", ID: " + reqInfo["id_man"].ToString());

            if ((int)reqInfo["id_oper"] == 3)
            {
                DataRow drBonusTypes = Config.hCntMain.GetBonusTypes(false).Select("id = " + reqInfo["id_TypeBonus"].ToString())[0];
                if (drBonusTypes != null)
                {
                    Logging.Comment("Тип бонуса: " + drBonusTypes["cName"].ToString() + ", ID: " + reqInfo["id_TypeBonus"].ToString());
                    if ((int)reqInfo["id_TypeBonus"] == 5)
                    {
                        DataTable dtBonusProp = id_dep != 6 ? Config.hCntMain.GetBonusProp((int)reqInfo["req_num"])
                                                                                : Config.hCntAdd.GetBonusProp((int)reqInfo["req_num"]);
                        if (dtBonusProp != null && dtBonusProp.Rows.Count > 0)
                        {
                            Logging.Comment("П_Н: " + dtBonusProp.Rows[0]["Deficit"].ToString());
                            Logging.Comment("Т_Б: " + dtBonusProp.Rows[0]["Goods"].ToString());
                        }
                    }
                }
            }

            Logging.VariableChange("Статус заявки: ", (dtStatuses.Select("id = " + nstatusReq.ToString())[0]["cName"].ToString()),
                                                        (dtStatuses.Select("id = " + nstatus.ToString())[0]["cName"].ToString()));

            Logging.Comment("Дата передачи РКВ: " + Config.curDate.ToString());
            Logging.Comment("Завершение операции \"Передача заявки руководителю\"");
            Logging.StopFirstLevel();
        }


        private void grdRequests_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
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

        private void chbNeedKDAccept_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void btExcel_Click(object sender, EventArgs e)
        {
            frmReportChoice frmRep = new frmReportChoice(grdRequests, cbDep.Text, dtpStart.Value, dtpFinish.Value);
            frmRep.ShowDialog();            
        }

        private void tbEAN_Validated(object sender, EventArgs e)
        {
            GetGridRequests();
        }

        private void btFilter_Click(object sender, EventArgs e)
        {
            int type = -1;
            DataTable dtData = null;

            if (!(sender is Button))
            {
                return;
            }

            if (((Button)sender).Equals(btFilterType)
            && cbType.DataSource is DataTable)
            {
                type = 0;
                dtData = (DataTable)cbType.DataSource;
            }
            else
            {
                return;
            }

            if (type != -1 && dtData != null && cbType.SelectedValue is int)
            {

                //DataTable dtData2 = new DataTable();
                //dtData2.Columns.Add("id", typeof(int));
                //dtData2.Columns.Add("cname", typeof(string));
                //dtData2.AcceptChanges();

                //for (int i = 0; dtData.Rows.Count > i; i++)
                //{
                //    DataRow dr = dtData.Rows[i];
                //    dtData2.Rows.Add((int)dr["id"],(string)dr["sname"]);
                //}

                //dtData2.AcceptChanges();

                Form filterChoosing = new frmFilterRequests(type, dtData, 0);
                filterChoosing.ShowDialog();

                int newSelectedIndex = (filterChoosing.DialogResult == DialogResult.OK ? -1
                                : filterChoosing.DialogResult == DialogResult.No ? 0
                                : 1);
                switch (type)
                {
                    case 0:
                        cbType.SelectedValue = newSelectedIndex <= 0
                                                ? newSelectedIndex
                                                : cbType.SelectedValue;

                        break;
                }

                if (newSelectedIndex != 1)
                    GetGridRequests();
            }
        }

        private void btnCopyActPereoc_Click(object sender, EventArgs e)
        {
            int id = int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString());
            int id_dep = (int)Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index]["id_dep"];
            ((Main)this.MdiParent).SetTab("Переоценка №" + id.ToString() + ". Создание.", "frmPereoc", new object[] { 3, id, id_dep, DateTime.Parse(grdRequests.CurrentRow.Cells["reqDate"].Value.ToString()) }, id);
        }

        //New 11.08.2017

        private void createSubRequest()
        {
            DataTable dtMainSingleSub = new DataTable();
            dtMainSingleSub.Columns.Add("idTRequest", typeof(Int32));
            dtMainSingleSub.Columns.Add("idTRequestPrihod", typeof(Int32));
            dtMainSingleSub.Columns.Add("idTRequestOtgruz", typeof(Int32));
            dtMainSingleSub.AcceptChanges();

            DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[grdRequests.CurrentRow.Index];
            int id = int.Parse(grdRequests.CurrentRow.Cells["id_req"].Value.ToString());
            int id_dep = (int)reqInfo["id_dep"];
            DataTable dtReqGoods = (id_dep != 6
                               ? Config.hCntMain.GetRequestBody(id, false, (DateTime)grdRequests.CurrentRow.Cells["reqDate"].Value)
                               : Config.hCntAdd.GetRequestBody(id, false, (DateTime)grdRequests.CurrentRow.Cells["reqDate"].Value));
            
            DataTable dtListTovarWeInOut = Config.hCntMain.getWeInOutTovarList(0, DateTime.Now, DateTime.Now, "", "", id, 4);

            string strIdPrihod = "";
            foreach (DataRow r in dtListTovarWeInOut.Rows)
            {
                strIdPrihod += "," + r["id_prihod"].ToString();
            }
            if (strIdPrihod.Length > 0)
                strIdPrihod = strIdPrihod.Remove(0, 1);

            DataTable dtPostIdAndNtypeOrg = Config.hCntShop2.getWeInOutIdPostNtypeOrgList(strIdPrihod, 0, 0, 1);
            foreach (DataRow r in dtPostIdAndNtypeOrg.Rows)
            {
                DataTable dtTmp = Config.hCntMain.getWeInOutIdPostNtypeOrgList("", int.Parse(r["id_post"].ToString()), int.Parse(r["ntypeorg"].ToString()), 2);
                if (dtTmp == null || dtTmp.Rows.Count == 0 || dtTmp.Rows[0]["isExists"].ToString().Equals("0"))
                    MessageBox.Show("Сохранение накладной невозможно, не найден поставщик!","Информирование",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Console.WriteLine(r["id_post"].ToString());
            }

            DataTable dtTovar2Shop = Config.hCntShop2.getWeInOutIdPostNtypeOrgList(strIdPrihod, 0, 0, 3);

            // Получение getWeInOutIdPostNtypeOrgList для ЮЛ указанных в родительской заявке
            DataTable dtPostOtgruzNew = Config.hCntMain.getWeInOutIdPostNtypeOrgList(strIdPrihod, 0, int.Parse(reqInfo["ntypeorg"].ToString()), 4);
            if (dtPostOtgruzNew == null || dtPostOtgruzNew.Rows.Count <= 0)
            {
                MessageBox.Show("Сохранение накладной невозможно", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtPostOtgruzNew.Columns.Contains("error"))
            {
                MessageBox.Show("Сохранение накладной невозможно,\nне найден существующий ЮЛ!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string listNewIdTrequestSubShop = "";
            foreach (DataRow r in dtPostIdAndNtypeOrg.Rows)
            {
                dtMainSingleSub.Rows.Add();

                dtMainSingleSub.Rows[dtMainSingleSub.Rows.Count - 1]["idTRequest"] = id;

                int idNewTreqSubShop = -1;
                int idNewOtgruzTReq = -1;

                // Получение getWeInOutIdPostNtypeOrgList для ЮЛ указанных в дочерней заявке
                DataTable dtPostPrihodNew = Config.hCntMain.getWeInOutIdPostNtypeOrgList(strIdPrihod, 0, int.Parse(r["ntypeorg"].ToString()), 4);
                if (dtPostPrihodNew == null || dtPostPrihodNew.Rows.Count <= 0)
                {
                    MessageBox.Show("Сохранение накладной невозможно", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtPostPrihodNew.Columns.Contains("error"))
                {
                    MessageBox.Show("Сохранение накладной невозможно,\nне найден существующий ЮЛ!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Создание заголовка для основного коннекта
                DataTable dtInIdNewRequest = Config.hCntMain.setSubShopData(id_dep, int.Parse(dtPostPrihodNew.Rows[0]["id_post"].ToString()), DateTime.Parse(reqInfo["req_date"].ToString()), int.Parse(reqInfo["id_man"].ToString()), int.Parse(reqInfo["ntypeorg"].ToString()), int.Parse(reqInfo["porthole"].ToString()), 1, id);

                if (dtInIdNewRequest == null || dtInIdNewRequest.Rows.Count == 0)
                    return;

                idNewTreqSubShop = int.Parse(dtInIdNewRequest.Rows[0]["id"].ToString());
                listNewIdTrequestSubShop += "," + idNewTreqSubShop.ToString();
                dtMainSingleSub.Rows[dtMainSingleSub.Rows.Count - 1]["idTRequestPrihod"] = idNewTreqSubShop;

                // Создание заголовка для доп. коннекта
                DataTable dtInIdOtgruzRequest = Config.hCntShop2.setSubShopData(id_dep, int.Parse(dtPostOtgruzNew.Rows[0]["id_post"].ToString()), DateTime.Parse(reqInfo["req_date"].ToString()), int.Parse(reqInfo["id_man"].ToString()), int.Parse(r["ntypeorg"].ToString()), int.Parse(reqInfo["porthole"].ToString()), 2, idNewTreqSubShop);

                if (dtInIdOtgruzRequest == null || dtInIdOtgruzRequest.Rows.Count == 0)
                    return;

                idNewOtgruzTReq = int.Parse(dtInIdOtgruzRequest.Rows[0]["id"].ToString());
                dtMainSingleSub.Rows[dtMainSingleSub.Rows.Count - 1]["idTRequestOtgruz"] = idNewOtgruzTReq;

                //

                DataRow[] rowFind = dtTovar2Shop.Select(string.Format("ntypeorg = {0} AND id_post={1}", int.Parse(r["ntypeorg"].ToString()), int.Parse(r["id_post"].ToString())));
                foreach(DataRow rAdd in rowFind)
                {
                    DataRow[] rBusyGoods = dtListTovarWeInOut.Select(string.Format("id_prihod = {0}", rAdd["id"].ToString()));
                    if (rBusyGoods.Count() > 0)
                    {
                        DataRow[] rRequestTovar = dtReqGoods.Select(string.Format("id_tovar = {0}", rBusyGoods[0]["id_tovar"].ToString()));
                        if (rRequestTovar.Count() > 0)
                        { 
                            decimal zcenNDSNew =0;

                            zcenNDSNew = (decimal.Parse(rBusyGoods[0]["zcena"].ToString()) * 100) / (100 + decimal.Parse(rAdd["nds"].ToString()));
                            //Тут создание товара в теле
                            Config.hCntMain.setSubShopDataTovar(
                                idNewTreqSubShop,
                                int.Parse(rRequestTovar[0]["npp"].ToString()),
                                int.Parse(rBusyGoods[0]["id_tovar"].ToString()),
                                decimal.Parse(rRequestTovar[0]["Tara"].ToString()),
                                decimal.Parse(rBusyGoods[0]["Netto"].ToString()),
                                decimal.Parse(rBusyGoods[0]["zcena"].ToString()),
                                decimal.Parse(rRequestTovar[0]["rcena"].ToString()),
                                zcenNDSNew,
                                1,
                                "",
                                DateTime.Parse(reqInfo["req_date"].ToString())
                                );


                            Config.hCntShop2.setSubShopDataTovar(
                               idNewOtgruzTReq,
                               int.Parse(rRequestTovar[0]["npp"].ToString()),
                               int.Parse(rBusyGoods[0]["id_tovar"].ToString()),
                               decimal.Parse(rRequestTovar[0]["Tara"].ToString()),
                               decimal.Parse(rBusyGoods[0]["Netto"].ToString()),
                               decimal.Parse(rBusyGoods[0]["zcena"].ToString()),
                               decimal.Parse(rRequestTovar[0]["rcena"].ToString()),
                               zcenNDSNew,
                               2,
                               rRequestTovar[0]["ean"].ToString(),
                               DateTime.Parse(reqInfo["req_date"].ToString())
                               );
                        }
                    }
                

                }

                dtMainSingleSub.AcceptChanges();
            }

            //Обновление cinclude
            Config.hCntMain.updateCinTRequest(id, listNewIdTrequestSubShop);

            foreach (DataRow r in dtMainSingleSub.Rows)
            {
                //dtMainSingleSub.Columns.Add("idTRequest", typeof(Int32));
                //dtMainSingleSub.Columns.Add("idTRequestPrihod", typeof(Int32));
                //dtMainSingleSub.Columns.Add("idTRequestOtgruz", typeof(Int32));


                int idTRequest = int.Parse(Config.hCntMain.setLinkTRequest(
                    int.Parse(r["idTRequest"].ToString()),
                    int.Parse(r["idTRequestPrihod"].ToString()),
                    int.Parse(r["idTRequestOtgruz"].ToString()),
                    1).Rows[0]["id"].ToString());

                Config.hCntMain.setLinkTRequest(
                   idTRequest,
                    int.Parse(r["idTRequestPrihod"].ToString()),
                    int.Parse(r["idTRequestOtgruz"].ToString()),
                    2);

                idTRequest = int.Parse(Config.hCntShop2.setLinkTRequest(
                    int.Parse(r["idTRequest"].ToString()),
                    int.Parse(r["idTRequestPrihod"].ToString()),
                    int.Parse(r["idTRequestOtgruz"].ToString()),
                    1).Rows[0]["id"].ToString());

                Config.hCntShop2.setLinkTRequest(
                   idTRequest,
                    int.Parse(r["idTRequestPrihod"].ToString()),
                    int.Parse(r["idTRequestOtgruz"].ToString()),
                    2);
            }

        }

        private void pnNeedRBSixAccept_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbRequests_Click(object sender, EventArgs e)
        {

        }
    }
}
