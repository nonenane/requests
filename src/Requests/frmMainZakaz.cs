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
using Nwuram.Framework.UI.Forms;
using Nwuram.Framework.UI.Service;
using Nwuram.Framework.Logging;

namespace Requests
{
    public partial class frmMainZakaz : Form
    {
        Procedures proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        char[] special_symbols = new char[] { '%', '"', '*', '(', ')', '\'' };
        int id_post_filter = 0;
        string id_grp_filter = "";
        DataSet dtSetOrders = null;
        DataTable dtGrp = null;
        frmLoad frmWaiting = null;
        DataTable dtHeader_old, dtBody_old;
        List<int> id_for_delete = new List<int>();
        // для изменения подтипов продукта id_grp3
        bool isChange = false;
        public frmMainZakaz()
        {
            InitializeComponent();
        }

        private void frmMainZakaz_Load(object sender, EventArgs e)
        {
            cmbTUGroups_Load();
            cmbSubGroups_Load();
            SetGridReadOnly();
            btnCreateActInv.Enabled = btnSave.Enabled = btnCreateRequests.Enabled = false;
        }

        private void cmbTUGroups_Load()
        {
            dtGrp = proc.GetTU(UserSettings.User.IdDepartment);
            DataRow all_row = dtGrp.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["id"]) == 0);
            all_row["cname"] = "Все группы";
            cmbTUGroups.DataSource = dtGrp;
        }

        private void cmbSubGroups_Load()
        {
            DataTable dt = proc.GetSubGrp(UserSettings.User.IdDepartment);

            LoadSubGroupColumn(dt.Copy());

            DataRow all_row = dt.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["id"]) == 0);
            all_row["cname"] = "Все подгруппы";
            cmbSubGroups.DataSource = dt;
        }

        private void LoadSubGroupColumn(DataTable dt)
        {
            repCmbGrp3.DataSource = dt;
        }

        private void SetGridReadOnly()
        {
            if (dtpDate.Value.Date < DateTime.Today)
            {
                SetGridReadOnly(grdHeader);
                SetGridReadOnly(grdBody);
            }
            else
            {
                if (UserSettings.User.StatusCode == "МН")
                {
                    SetGridReadOnly(grdHeader);
                }

                if (UserSettings.User.StatusCode == "РКВ")
                {
                    foreach (DevExpress.XtraGrid.Columns.GridColumn column in grdHeader.Columns)
                    {
                        column.OptionsColumn.AllowEdit = column.Name == "zakaz_manager";
                    }
                    SetGridReadOnly(grdBody);
                }

                foreach (DevExpress.XtraGrid.Columns.GridColumn column in grdBody.Columns)
                {
                    if (UserSettings.User.StatusCode == "ДМН" || column.Name == "subject_name" || column.Name == "post_name" || column.Name == "id_trequest")
                    {
                        column.OptionsColumn.AllowEdit = false;
                    }
                }

            }
        }

        private void SetGridReadOnly(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in grid.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
            }
        }

        private void grdMain_Load()
        {
            EnableControlsService.SaveControlsEnabledState(this);
            EnableControlsService.SetControlsEnabled(this, false);
            frmWaiting = new frmLoad("Ждите, идёт загрузка данных...");
            frmWaiting.Show();

            bgwLoad.RunWorkerAsync(dtpDate.Value.Date);
        }

        public void ShowAllDetails()
        {
            grdHeader.BeginUpdate();
            try
            {
                int dataRowCount = grdHeader.DataRowCount;
                for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
                {
                    if (Convert.ToInt32(grdHeader.GetDataRow(rHandle)["id_order"]) > 0)
                    {
                        grdHeader.SetMasterRowExpanded(rHandle, true);
                    }
                }
            }
            finally
            {
                grdHeader.EndUpdate();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (UserSettings.User.StatusCode == "РКВ" || (UserSettings.User.StatusCode == "ДМН" && !SomethingChangedInHeader()) || 
                (UserSettings.User.StatusCode == "МН" && !SomethingChangedInBody()) ||
                MessageBox.Show("На форме есть несохранённые изменения. Перезагрузить данные?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grdMain_Load();   
            }
        }

        private void cmbTUGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbTUGroups.SelectedValue) != -1)
            {
                DataRow[] filterRows = dtGrp.Select("id = -1");
                if (filterRows.Length > 0)
                {
                    int selectedId = Convert.ToInt32(cmbTUGroups.SelectedValue);
                    dtGrp.Rows.Remove(filterRows[0]);
                    cmbTUGroups.SelectedValue = selectedId;
                }
            }
            FilterGoods();
        }

        private void cmbSubGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            FilterGoods();
        }

        private void FilterGoods()
        {
            if (dtSetOrders != null && dtSetOrders.Tables[0] != null)
            {
                string filter = "ean like '%" + txtEAN.Text + "%' and cname like '%" + txtName.Text + "%'";

                if (cmbTUGroups.SelectedValue != null && Convert.ToInt32(cmbTUGroups.SelectedValue) != 0)
                {
                    filter += Convert.ToInt32(cmbTUGroups.SelectedValue) != -1 ? " and id_grp1 = " + cmbTUGroups.SelectedValue.ToString() : " and id_grp1 in (" + id_grp_filter + ")";
                }

                if (cmbSubGroups.SelectedValue != null && Convert.ToInt32(cmbSubGroups.SelectedValue) != 0)
                {
                    filter += " and id_grp3 = " + cmbSubGroups.SelectedValue.ToString();
                }

                string filter_body = "";

                if (id_post_filter != 0)
                {
                    filter_body = "id_post = " + id_post_filter;

                    string tovars = GetTovarWithPosts();
                    filter += " and " + (tovars.Length > 0 ? "id in (" + tovars + ")" : "id = 0");
                }

                if (cbSelected.Checked)
                {
                    filter_body = (filter_body.Length > 0 ? " and " : "") + "is_select = true";
                    string tovars_selected = GetSelectedTovars();
                    filter += " and " + (tovars_selected.Length > 0 ? "id in (" + tovars_selected + ")" : "id = 0");
                }

                grdHeader.ActiveFilterString = filter;
                grdBody.ActiveFilterString = filter_body;
            }
        }

        private string GetTovarWithPosts()
        {
            return GetTovarsByCondition("id_post = " + id_post_filter.ToString());
        }

        private string GetSelectedTovars()
        {
            return GetTovarsByCondition("is_select = true");
        }

        private string GetTovarsByCondition(string condition)
        {
            DataRow[] rows = dtSetOrders.Tables[1].Select(condition);
            string id_tovars = "";
            foreach (DataRow row in rows)
            {
                id_tovars += row["id_tovar"].ToString() + ",";
            }
            return id_tovars.Length > 0 ? id_tovars.Substring(0, id_tovars.Length - 1) : id_tovars; 
        }

        private void txtEAN_TextChanged(object sender, EventArgs e)
        {
            FilterGoods();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            FilterGoods();
        }

        private void txtEAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (special_symbols.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClearName_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
        }

        private void btnClearPost_Click(object sender, EventArgs e)
        {
            txtPostName.Text = "";
            id_post_filter = 0;
            FilterGoods();
        }

        private void grdHeader_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdHeader.Appearance.FocusedRow.BackColor = e.FocusedRowHandle % 2 == 0 ? Color.FromArgb(193, 255, 193) : Color.FromArgb(253, 253, 193);

            DataRow current_row = grdHeader.GetDataRow(e.FocusedRowHandle);
            if (current_row != null)
            {
                txtInserter.Text = current_row["insert_name"].ToString();
                txtDateInsert.Text = current_row["date_insert"] == DBNull.Value ? "" : current_row["date_insert"].ToString();
                txtEditor.Text = current_row["editor_name"].ToString();
                txtDateEdit.Text = current_row["date_edit"] == DBNull.Value ? "" : current_row["date_edit"].ToString();
            }
            else
            {
                txtInserter.Text = txtDateInsert.Text = txtEditor.Text = txtDateEdit.Text = "";
            }
            
            //ShowAddZatar();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            GeneralSettings.frmSettings frmSet = new GeneralSettings.frmSettings();
            frmSet.SetSelectedTab(2);
            frmSet.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (UserSettings.User.StatusCode == "ДМН" || UserSettings.User.StatusCode == "РКВ")
            {
                SaveOrderHead();
            }
            else if (UserSettings.User.StatusCode == "МН")
            {
                SaveOrderBody();
            }
        }

        private void SaveOrderHead()
        {
            if (grdHeader.DataSource != null)
            {
                DataTable dtHeaders = (grdHeader.DataSource as DataView).ToTable();
                DataRow[] rows_to_save = dtHeaders.Select("inventory is not null and zakaz_manager is not null");
                if (rows_to_save.Length > 0 && SomethingChangedInHeader())
                {
                    int isCalcByRemains = 0;
                    if (rbOst.Checked) isCalcByRemains = 1;
                    StartLogSaveOrderHead();
                    foreach (DataRow row in rows_to_save)
                    {
                        if (Convert.ToInt32(row["id_order"]) == 0 || ExistChanges(row))
                        {
                            LogSaveOrderHead(row);
                            int id_order = proc.SaveOrderHead(Convert.ToInt32(row["id_order"]), dtpDate.Value.Date, Convert.ToInt32(row["id"]), Convert.ToDecimal(row["inventory"]), Convert.ToDecimal(row["plan_realiz"]), Convert.ToDecimal(row["sred_rashod"]), Convert.ToDecimal(row["zakaz_manager"]), Convert.ToInt32(row["id_grp3"]), isCalcByRemains);
                            LogSaveOrderHeadId(id_order);
                        }
                    }
                    StopLogSaveOrderHead();
                    MessageBox.Show("Данные сохранены!");
                    grdMain_Load();
                }
            }
        }

        private void StartLogSaveOrderHead()
        {
            bool new_zakaz = dtSetOrders.Tables[0].Select("id_order <> 0").Length == 0;

            Logging.StartFirstLevel(new_zakaz ? 991 : 992);
            Logging.Comment("Начало " + (new_zakaz ? "добавления" : "редактирования") + " основного заказа");
            Logging.Comment("Дата заказа = " + dtpDate.Value.ToShortDateString());
        }

        private void LogSaveOrderHead(DataRow row_to_save)
        {
            int id = Convert.ToInt32(row_to_save["id_order"]);

            if (id == 0 || ExistChanges(row_to_save))
            {
                Logging.Comment(id == 0 ? "Добавление товара в заказ" : "Редактирование товара в заказе");
                Logging.Comment("id товара = " + row_to_save["id"].ToString() + ", ean = '" + row_to_save["ean"].ToString() + "', название товара = '" + row_to_save["cname"].ToString());
                if (id == 0)
                {
                    Logging.Comment("Средний расход = " + row_to_save["sred_rashod"].ToString() + ", Плановая реализация = " + row_to_save["plan_realiz"].ToString());
                    Logging.Comment("Переучёт = " + row_to_save["inventory"].ToString() + ", затарка = " + row_to_save["nzatar"].ToString() + ", кол-во тары = " + (row_to_save["nzatar"] == DBNull.Value || Convert.ToDecimal(row_to_save["nzatar"]) == 0 ? "" : (Convert.ToDecimal(row_to_save["inventory"]) / Convert.ToDecimal(row_to_save["nzatar"])).ToString()));
                    Logging.Comment("Расчёт заказа = " + row_to_save["zakaz"] + ", заказ менеджера = " + row_to_save["zakaz_manager"].ToString());
                }
                else
                {
                    DataRow old_row = dtHeader_old.Select("id = " + row_to_save["id"].ToString())[0];
                    Logging.VariableChange("Переучёт", Convert.ToDecimal(row_to_save["inventory"]), Convert.ToDecimal(old_row["inventory"]));
                    Logging.VariableChange("Затарка", Convert.ToDecimal(row_to_save["nzatar"]), Convert.ToDecimal(old_row["nzatar"]));
                    Logging.VariableChange("Кол-во тары", Convert.ToDecimal(row_to_save["inventory"]) / Convert.ToDecimal(row_to_save["nzatar"]), Convert.ToDecimal(old_row["inventory"]) / Convert.ToDecimal(old_row["nzatar"]));
                    Logging.VariableChange("Расчёт заказа", Convert.ToDecimal(row_to_save["zakaz"]), Convert.ToDecimal(old_row["zakaz"]));
                    Logging.VariableChange("Заказ менеджера", Convert.ToDecimal(row_to_save["zakaz_manager"]), Convert.ToDecimal(old_row["zakaz_manager"]));
                }
            }
        }

        private void LogSaveOrderHeadId(int id)
        {
            Logging.Comment("id записи = " + id.ToString());
        }

        private bool ExistChanges(DataRow row_to_save)
        {
            DataRow old_row = dtHeader_old.Select("id = " + row_to_save["id"].ToString())[0];
            return Convert.ToDecimal(row_to_save["inventory"]) != Convert.ToDecimal(old_row["inventory"]) ||
                    Convert.ToDecimal(row_to_save["nzatar"]) != Convert.ToDecimal(old_row["nzatar"]) ||
                    Convert.ToDecimal(row_to_save["zakaz"]) != Convert.ToDecimal(old_row["zakaz"]) ||
                    Convert.ToDecimal(row_to_save["zakaz_manager"]) != Convert.ToDecimal(old_row["zakaz_manager"]);
        }

        private void StopLogSaveOrderHead()
        {
            Logging.Comment("Конец сохранения основного заказа");
            Logging.StopFirstLevel();
        }

        private void SaveOrderBody()
        {
            DataTable dtOrderBodies = dtSetOrders.Tables[1];
            if (dtOrderBodies.Select("(fact_netto = 0 or zcena = 0) and is_select = false").Length > 0)
            {
                MessageBox.Show("Факт. нетто и цена закупки не могут быть равны нулю. Сохранение невозможно.");
                return;
            }

            if (id_for_delete.Count > 0)
            {
                StartLogDeletePost();
                foreach (int id_del in id_for_delete)
                {
                    LogDeletePost(id_del);
                    proc.DeleteOrderBody(id_del);
                }
                StopLogDeletePost();
            }

            if (ExistNewOrderBody() || OrderBodyChanged())
            {
                StartLogSavePost();
                foreach (DataRow row in dtOrderBodies.Rows)
                {
                    if (row["id_order_body"] != DBNull.Value)
                    {
                        LogSavePost(row);
                        proc.SaveOrderBody(Convert.ToInt32(row["id_order_body"]), Convert.ToInt32(row["id_order"]), Convert.ToInt32(row["id_post"]), Convert.ToInt32(row["id_subject"]), Convert.ToDecimal(row["fact_netto"]), row["caliber"].ToString(), Convert.ToDecimal(row["zcena"]), Convert.ToBoolean(row["checked"]));
                    }
                }
                StopLogSavePost();
            }
            MessageBox.Show("Данные сохранены!");
            grdMain_Load();
        }

        private void StartLogSavePost()
        {
            Logging.StartFirstLevel(1002);
            Logging.Comment("Начало сохранения поставщиков");
        }

        private void LogSavePost(DataRow row)
        {
            int id_order_post = Convert.ToInt32(row["id_order_body"]);

            DataRow old_row = id_order_post == 0 ? null : dtBody_old.Select("id_order_body = " + row["id_order_body"].ToString())[0];
            if (id_order_post == 0 || PostRowChanged(row, old_row))
            {
                DataRow tovar_row = dtSetOrders.Tables[0].Select("id_order = " + row["id_order"].ToString())[0];
                Logging.Comment("id товара = " + tovar_row["id"].ToString() + ", ean = '" + tovar_row["ean"].ToString() + "', название = '" + tovar_row["cname"].ToString() + "'");

                if (id_order_post == 0)
                {
                    Logging.Comment("id_post = " + row["id_post"].ToString() + ", название поставщика = " + row["post_name"].ToString());
                    Logging.Comment("факт.нетто = " + row["fact_netto"].ToString() + ", цена закупки = " + row["zcena"].ToString());
                    Logging.Comment("примечание = " + row["caliber"].ToString() + ", id_subject = " + row["id_subject"].ToString() + ", название субъекта = " + row["subject_name"].ToString());
                    Logging.Comment("подтверждён = " + row["checked"].ToString());
                }
                else
                {
                    Logging.VariableChange("id_post", Convert.ToInt32(row["id_post"]), Convert.ToInt32(old_row["id_post"]));
                    Logging.VariableChange("название поставщика", row["post_name"].ToString(), old_row["post_name"].ToString());
                    Logging.VariableChange("факт.нетто", Convert.ToDecimal(row["fact_netto"]), Convert.ToDecimal(old_row["fact_netto"]));
                    Logging.VariableChange("цена закупки", Convert.ToDecimal(row["zcena"]), Convert.ToDecimal(old_row["zcena"]));
                    Logging.VariableChange("примечание", row["caliber"].ToString(), old_row["caliber"].ToString());
                    Logging.VariableChange("id_subject", Convert.ToInt32(row["id_subject"]), Convert.ToInt32(old_row["id_subject"]));
                    Logging.VariableChange("название субъекта", row["subject_name"].ToString(), old_row["subject_name"].ToString());
                    Logging.VariableChange("подтверждён", Convert.ToBoolean(row["checked"]), Convert.ToBoolean(old_row["checked"]));
                }
            }
        }

        private bool PostRowChanged(DataRow row, DataRow old_row)
        {
            return row.Field<bool>("checked") != old_row.Field<bool>("checked") ||
                                !((row["id_post"] == DBNull.Value && old_row["id_post"] == DBNull.Value) || (row["id_post"] != DBNull.Value && old_row["id_post"] != DBNull.Value && row.Field<int>("id_post") == old_row.Field<int>("id_post"))) ||
                                !((row["fact_netto"] == DBNull.Value && old_row["fact_netto"] == DBNull.Value) || (row["fact_netto"] != DBNull.Value && old_row["fact_netto"] != DBNull.Value && row.Field<decimal>("fact_netto") == old_row.Field<decimal>("fact_netto"))) ||
                                !((row["caliber"] == DBNull.Value && old_row["caliber"] == DBNull.Value) || (row["caliber"] != DBNull.Value && old_row["caliber"] != DBNull.Value && row.Field<string>("caliber") == old_row.Field<string>("caliber"))) ||
                                !((row["id_subject"] == DBNull.Value && old_row["id_subject"] == DBNull.Value) || (row["id_subject"] != DBNull.Value && old_row["id_subject"] != DBNull.Value && row.Field<int>("id_subject") == old_row.Field<int>("id_subject"))) ||
                                !((row["zcena"] == DBNull.Value && old_row["zcena"] == DBNull.Value) || (row["zcena"] != DBNull.Value && old_row["zcena"] != DBNull.Value && row.Field<decimal>("zcena") == old_row.Field<decimal>("zcena")));
        }

        private void StopLogSavePost()
        {
            Logging.Comment("Конец сохранения поставщиков");
            Logging.StopFirstLevel();
        }
            
        private void StartLogDeletePost()
        {
            Logging.StartFirstLevel(1004);
            Logging.Comment("Начало удаления поставщиков");
        }

        private void LogDeletePost(int id_del_order_body)
        {
            DataRow post_row = dtBody_old.Select("id_order_body = " + id_del_order_body.ToString())[0];
            DataRow head_row = dtSetOrders.Tables[0].Select("id_order = " + post_row["id_order"].ToString())[0];

            Logging.Comment("id товара = " + head_row["id"].ToString() + ", ean = '" + head_row["ean"].ToString() + ", название = " + head_row["cname"].ToString());
            Logging.Comment("id_post = " + post_row["id_post"].ToString() + ", название = " + post_row["post_name"].ToString());
            Logging.Comment("факт.нетто = " + post_row["fact_netto"].ToString() + ", калибр = " + post_row["caliber"].ToString());
            Logging.Comment("цена закупки = " + post_row["zcena"].ToString() + ", id_subject = " + post_row["id_subject"].ToString() + ", название = " + post_row["subject_name"].ToString());
        }

        private void StopLogDeletePost()
        {
            Logging.Comment("Завершение удаления поставщиков");
            Logging.StopFirstLevel();
        }

        private void txtPostName_Click(object sender, EventArgs e)
        {
            Posts.frmPosts frmPosts = new Posts.frmPosts(UserSettings.User.IdDepartment);
            frmPosts.ShowDialog();

            id_post_filter = frmPosts.PostId;
            txtPostName.Text = frmPosts.PostName;
            FilterGoods();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            grdMain_Load();
            SetGridReadOnly();
        }

        private void grdHeader_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name != "zakaz_manager")
            {
                RecountValues();
            }
        }

        private void RecountValues()
        {
            if (grdHeader.FocusedRowHandle != -1)
            {
                DataRow current_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);

                if (current_row["inventory"] == DBNull.Value)
                {
                    current_row["diff"] = DBNull.Value;
                    current_row["perezatarka_zal"] = Convert.ToDecimal(0);//DBNull.Value;
                    current_row["zakaz"] = current_row["zakaz_manager"] = Convert.ToDecimal(current_row["part_zakaz"]);
                }
                else
                {
                    current_row["diff"] = Convert.ToDecimal(current_row["inventory"]) - Convert.ToDecimal(current_row["ost_on_date"]);
                    current_row["perezatarka_zal"] = Convert.ToDecimal(current_row["sred_rashod"]) == 0 ? 0 : Convert.ToDecimal(current_row["inventory"]) / Convert.ToDecimal(current_row["sred_rashod"]);
                    current_row["zakaz"] = current_row["zakaz_manager"] = Convert.ToDecimal(current_row["part_zakaz"]) - Convert.ToDecimal(current_row["inventory"]);
                }
            }
        }

        private void btnMultiTU_Click(object sender, EventArgs e)
        {
            frmFilter frmTU = new frmFilter(0, cmbTUGroups.DataSource as DataTable, UserSettings.User.IdDepartment);
            frmTU.ShowDialog();

            id_grp_filter = frmTU.SelectedIdList;
            if (id_grp_filter.Length > 0)
            {
                dtGrp.Rows.Add(new object[] { -1, "В соответствии с фильтром", 1 });
                cmbTUGroups.SelectedValue = -1;
            }
            else
            {
                cmbTUGroups_Load();
            }
            FilterGoods();
        }

        private void tsmRealiz_Click(object sender, EventArgs e)
        {
            ShowRealiz();
        }

        private void ShowRealiz()
        {
            DataRow current_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);
            PrihodRealizForms.frmReal frmRealiz = new PrihodRealizForms.frmReal(Convert.ToInt32(current_row["id"]), current_row["ean"].ToString(), current_row["cname"].ToString(), UserSettings.User.IdDepartment);
            frmRealiz.ShowDialog();
        }

        private void grdHeader_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.Column != null)
            {
                if (e.HitInfo.Column.Name == "sred_rashod" || e.HitInfo.Column.Name == "ost_on_date")
                {
                    cmsRealiz.Show((sender as DevExpress.XtraGrid.Views.Grid.GridView).GridControl, e.Point);
                }
                else if (UserSettings.User.StatusCode == "ДМН" && dtpDate.Value.Date >= DateTime.Today && e.HitInfo.InRowCell)
                {
                    if (e.HitInfo.Column.Name == "inventory")
                    {
                        cmsInventory.Show((sender as DevExpress.XtraGrid.Views.Grid.GridView).GridControl, e.Point);
                    }
                }
            }
        }

        private void tsmInventory_Click(object sender, EventArgs e)
        {
            DataRow current_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);
            request_zatr.EditForm frmZatar = new request_zatr.EditForm(Convert.ToInt32(current_row["id"]), Convert.ToDouble(current_row["nzatar"]), Convert.ToInt32(current_row["id_subject"]), current_row["subject_name"].ToString(), UserSettings.User.IdDepartment, current_row["cname"].ToString(), current_row["ean"].ToString(), Convert.ToBoolean(current_row["is_transparent"]));
            frmZatar.from_zakaznik = true;
            frmZatar.ShowDialog();
            current_row["nzatar"] = proc.GetGoodZatar(Convert.ToInt32(current_row["id"]));
        }

        private void tsmAddPost_Click(object sender, EventArgs e)
        {
            //DataRow current_head_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);

            //DataRow current_body_row = (grdMain.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView).GetDataRow((grdMain.FocusedView as DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle);
            //DataRow current_head_row = dtSetOrders.Tables[0].Select("id_order = " + current_body_row["id_order"].ToString())[0];

            DataRow current_head_row = grdHeader.GetDataRow((grdMain.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView).SourceRowHandle);

            Posts.frmPosts frmSelectPost = new Posts.frmPosts(UserSettings.User.IdDepartment);
            frmSelectPost.ShowDialog();

            if (frmSelectPost.PostId != 0)
            {
                DataTable dtBody = dtSetOrders.Tables[1];

                dtBody.Rows.Add(new object[] { false, Convert.ToInt32(current_head_row["id_order"]), Convert.ToInt32(current_head_row["id"]), 0, false, frmSelectPost.PostId, frmSelectPost.PostName, 0, "", 0, Convert.ToInt32(current_head_row["id_subject"]), current_head_row["subject_name"].ToString(), DBNull.Value });
            }
        }

        private void grdBody_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (UserSettings.User.StatusCode == "МН" && dtpDate.Value.Date >= DateTime.Today && (e.HitInfo.InColumn || e.HitInfo.InRowCell))
            {
                DevExpress.XtraGrid.Views.Grid.GridView grid = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DataRow current_row = grid.GetDataRow(grid.FocusedRowHandle);
                
                if (e.HitInfo.Column.Name == "post_name")
                {
                    tsmDelPost.Visible = grid.FocusedRowHandle >= 0;
                    cmsPosts.Show((sender as DevExpress.XtraGrid.Views.Grid.GridView).GridControl, e.Point);
                }
                else if (e.HitInfo.Column.Name == "subject_name" && current_row["id_trequest"] == DBNull.Value)
                {
                    cmsSubjects.Show((sender as DevExpress.XtraGrid.Views.Grid.GridView).GridControl, e.Point);
                }
            }
        }

        private void tsmDelPost_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow current_row = (grdMain.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView).GetDataRow((grdMain.FocusedView as DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle);
                if (current_row != null)
                {
                    if (Convert.ToBoolean(current_row["is_select"]))
                    {
                        MessageBox.Show("Нельзя удалить подтверждённого поставщика!");
                        return;
                    }

                    int id_order_body = Convert.ToInt32(current_row["id_order_body"]);
                    if (id_order_body != 0)
                    {
                        id_for_delete.Add(id_order_body);
                    }
                    dtSetOrders.Tables[1].Rows.Remove(current_row);
                }
            }
        }

        private void tsmSubjects_Click(object sender, EventArgs e)
        {
            request_zatr.SelectFrom frmSubj = new request_zatr.SelectFrom();
            frmSubj.ShowDialog();

            if (frmSubj.id_sub != 0)
            {
                DataRow current_row = (grdMain.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView).GetDataRow((grdMain.FocusedView as DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle);
                current_row["id_subject"] = frmSubj.id_sub;
                current_row["subject_name"] = frmSubj.Name;
            }
        }

        private void ShowAddZatar()
        {
            if (UserSettings.User.StatusCode == "ДМН" && grdHeader.FocusedColumn.Name == "inventory" && grdHeader.FocusedRowHandle != -1 && dtpDate.Value.Date >= DateTime.Today)
            {
                DataRow current_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);
                if (current_row != null && Convert.ToInt32(current_row["nzatar"]) > 0)
                {
                    frmAddZatar frmZatar = new frmAddZatar(Convert.ToInt32(current_row["nzatar"]), current_row["inventory"] == DBNull.Value ? 0 : Convert.ToDecimal(current_row["inventory"]), current_row["ean"].ToString(), current_row["cname"].ToString());
                    if (frmZatar.ShowDialog() == DialogResult.OK)
                    {
                        current_row["inventory"] = frmZatar.Count;
                        RecountValues();
                    }
                }
            }
        }

        private void ShowAddZatar(int rowHandle)
        {
            if (rowHandle != -1 && dtpDate.Value.Date >= DateTime.Today)
            {
                DataRow current_row = grdHeader.GetDataRow(rowHandle);
                if (current_row != null && Convert.ToInt32(current_row["nzatar"]) > 0)
                {
                    frmAddZatar frmZatar = new frmAddZatar(Convert.ToInt32(current_row["nzatar"]), current_row["inventory"] == DBNull.Value ? 0 : Convert.ToDecimal(current_row["inventory"]), current_row["ean"].ToString(), current_row["cname"].ToString());
                    if (frmZatar.ShowDialog() == DialogResult.OK)
                    {
                        current_row["inventory"] = frmZatar.Count;
                        RecountValues();
                    }
                }
            }
        }

        private void grdHeader_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            //if (e.FocusedColumn.Name == "inventory")
            //{
            //    ShowAddZatar();
            //}
        }

        private void grdHeader_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != -1 && e.Column.Name == "inventory")
            {
                DataRow current_row = grdHeader.GetDataRow(e.RowHandle);
                if (current_row != null && Convert.ToInt32(current_row["nzatar"]) > 0)
                {
                    e.Appearance.BackColor = pnlHasZatar.BackColor;
                }
            }
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            int id_grp_selected = Convert.ToInt32(cmbTUGroups.SelectedValue);
            frmZakaznikReport frmReport = new frmZakaznikReport(id_grp_selected == 0 ? "" : (id_grp_selected == -1 ? id_grp_filter : id_grp_selected.ToString()), cmbTUGroups.Text, Convert.ToInt32(cmbSubGroups.SelectedValue) != 0 ? cmbSubGroups.SelectedValue.ToString() : "", cmbSubGroups.Text);
            frmReport.ShowDialog();
        }

        private void grdBody_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow current_row = (grdMain.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView).GetDataRow((grdMain.FocusedView as DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle);
            if (UserSettings.User.StatusCode == "ДМН" && grdBody.FocusedColumn.Name == "check")
            {
                if (current_row != null && (!Convert.ToBoolean(current_row["is_select"]) || current_row["id_trequest"] != DBNull.Value))
                {
                    e.Cancel = true;
                }
            }
            else if (UserSettings.User.StatusCode == "МН")
            {
                if (current_row != null && Convert.ToBoolean(current_row["is_select"]))
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnCreateRequests_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Будут созданы заявки на выбранных поставщиков. Продолжить?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataTable dt = dtSetOrders.Tables[1];
                DataRow[] checked_rows = dt.Select("checked = true");
                if (checked_rows.Length > 0)
                {
                    DataTable checked_table = checked_rows.CopyToDataTable();
                    List<int> id_posts = checked_table.AsEnumerable().Select(r => r.Field<int>("id_post")).Distinct().ToList<int>();

                    foreach (int id_post in id_posts)
                    {
                        StartLogSaveRequest(id_post, checked_table);
                        DataTable result = proc.SaveOrderRequestHead(dtpDate.Value.Date, id_post);
                        if (Convert.ToInt32(result.Rows[0]["res"]) == 0)
                        {
                            MessageBox.Show(result.Rows[0]["mes"].ToString());
                            return;
                        }
                        else
                        {
                            int id_trequest = Convert.ToInt32(result.Rows[0]["res"]);

                            DataRow[] post_rows = checked_table.Select("id_post = " + id_post.ToString());
                            int npp = 1;
                            foreach (DataRow post_row in post_rows)
                            {
                                DataRow tovar_row = dtSetOrders.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<int>("id_order") == Convert.ToInt32(post_row["id_order"]));
                                if (tovar_row != null)
                                {
                                    LogSaveRequest(tovar_row, post_row);
                                    proc.SaveOrderRequestBody(id_trequest, Convert.ToInt32(tovar_row["id"]), npp++, Convert.ToDecimal(post_row["fact_netto"]), Convert.ToDecimal(post_row["zcena"]), post_row["caliber"].ToString(), Convert.ToInt32(post_row["id_subject"]));
                                    proc.SetOrderTRequest(Convert.ToInt32(post_row["id_order_body"]), id_trequest);
                                }
                            }

                            StopLogSaveRequest(id_trequest);
                        }
                    }

                    MessageBox.Show("Заявки успешно созданы!");
                    grdMain_Load();
                }
            }
        }

        private void StartLogSaveRequest(int id_post, DataTable table)
        {
            Logging.StartFirstLevel(993);
            Logging.Comment("Начало создания заявки");
            Logging.Comment("Дата заявки = " + dtpDate.Value.ToShortDateString());

            DataRow ntypeorg_row = proc.GetZakaznikNtypeorg();
            if (ntypeorg_row != null)
            {
                Logging.Comment("ntypeorg = " + ntypeorg_row["nTypeOrg"].ToString() + ", аббревиатура = " + ntypeorg_row["Abbriviation"].ToString());
            }

            Logging.Comment("id_post = " + id_post.ToString() + ", название поставщика = " + table.Select("id_post = " + id_post.ToString())[0]["post_name"].ToString());
        }

        private void LogSaveRequest(DataRow tovar_row, DataRow post_row)
        {
            Logging.Comment("id_tovar = " + tovar_row["id"].ToString() + ", ean = '" + tovar_row["ean"].ToString() + "', cname = '" + tovar_row["cname"].ToString() + "'");
            Logging.Comment("факт. заказ = " + post_row["fact_netto"].ToString() + ", цена закупки = " + post_row["zcena"].ToString());
            Logging.Comment("калибр = " + post_row["caliber"].ToString() + ", id_subject = " + post_row["id_subject"].ToString() + ", название субъекта = " + post_row["subject_name"].ToString());
        }

        private void StopLogSaveRequest(int id_trequest)
        {
            Logging.Comment("id заявки = " + id_trequest.ToString());
            Logging.Comment("Завершение создания заявки");
            Logging.StopFirstLevel();
        }

        private void btnCreateActInv_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Создать акт инвентаризации на " + dtpDate.Value.Date.ToShortDateString() + "?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow[] rows = dtSetOrders.Tables[0].Select("inventory is not null and inventory <> 0");
                if (rows.Length > 0)
                {
                    DataTable res_head = proc.SaveOrderTTost(dtpDate.Value.Date);
                    if (res_head != null && res_head.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(res_head.Rows[0]["res"]) == 0)
                        {
                            MessageBox.Show(res_head.Rows[0]["mes"].ToString());
                            return;
                        }
                        else
                        {
                            int id_tost = Convert.ToInt32(res_head.Rows[0]["res"]);
                            int npp = 1;

                            StartLogSaveTTost(id_tost);
                            foreach (DataRow row in rows)
                            {
                                LogSaveTTost(row);
                                proc.SaveOrderOst(id_tost, Convert.ToInt32(row["id"]), row["ean"].ToString(), Convert.ToDecimal(row["inventory"]), npp);
                            }

                            StopLogSaveTTost();
                            MessageBox.Show("Акт инвентаризации успешно создан!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных по переучёту!");
                }
            }
        }

        private void StartLogSaveTTost(int id_tost)
        {
            Logging.StartFirstLevel(994);
            Logging.Comment("Начало создания акта инвентаризации");
            Logging.Comment("id_tost = " + id_tost.ToString());
        }

        private void LogSaveTTost(DataRow row)
        {
            Logging.Comment("id товара = " + row["id"].ToString() + ", ean = '" + row["ean"].ToString() + "', название = '" + row["cname"].ToString() + "'");
            Logging.Comment("переучёт = " + row["inventory"].ToString());
        }

        private void StopLogSaveTTost()
        {
            Logging.Comment("Завершение создания акта инвентаризации");
            Logging.StopFirstLevel();
        }

        private void grdBody_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle != -1)
            {
                DataRow current_row = (sender as DevExpress.XtraGrid.Views.Grid.GridView).GetDataRow(e.RowHandle);
                if (current_row != null)
                {
                    if (current_row["id_trequest"] != DBNull.Value)
                    {
                        e.Appearance.BackColor = pnlHasRequest.BackColor;
                    }
                    else if (Convert.ToBoolean(current_row["is_select"]))
                    {
                        e.Appearance.BackColor = pnlSelected.BackColor;
                    }
                }
            }
        }

        private void SetButtonsEnabled()
        {
            btnCreateRequests.Enabled = UserSettings.User.StatusCode == "ДМН" && CheckedOrdersCountWithoutRequests() > 0;
            btnSave.Enabled = SomethingChanged();
            btnCreateActInv.Enabled = UserSettings.User.StatusCode == "ДМН" && grdHeader.RowCount > 0;
        }

        private int CheckedOrdersCountWithoutRequests()
        {
            return dtSetOrders.Tables[1] != null ? dtSetOrders.Tables[1].Select("checked = true and is_select = true and id_trequest is null").Length : 0;
        }

        private bool SomethingChanged()
        {
            return true;
        }

        private void cbSelected_CheckedChanged(object sender, EventArgs e)
        {
            FilterGoods();
        }

        private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSetOrders = proc.GetOrders(dtpDate.Value.Date);
            dtHeader_old = dtSetOrders.Tables[0].Copy();
            dtBody_old = dtSetOrders.Tables[1].Copy();

            DoOnUIThread(delegate()
                           {
                               inventory.Caption = "Переучёт";
                           });
            DataRow[] rowisCalcByRemains = dtSetOrders.Tables[0].Select("id_ttos is not null");
            if (rowisCalcByRemains.Count() != 0)
            {
                if (DialogResult.No == MessageBox.Show("Загрузить данные глобальной инвентаризации?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    foreach (DataRow r in dtSetOrders.Tables[0].Rows)
                    {
                        //ost_on_date
                        //sred_rashod
                        //inventory
                        //coef

                        decimal _ost_on_date = decimal.Parse(r["ost_on_date"].ToString());
                        decimal _sred_rashod = decimal.Parse(r["sred_rashod"].ToString());
                        decimal _inventory = decimal.Parse(r["inventory"] == DBNull.Value ? "0" : r["inventory"].ToString());
                        decimal _coef = decimal.Parse(r["coef"].ToString());

                        if (rbOst.Checked)
                        {
                            r["zakaz"] = _sred_rashod * _coef - _ost_on_date;
                            r["zakaz_manager"] = _sred_rashod * _coef - _ost_on_date;
                        }
                        else
                        {
                            r["zakaz"] = _sred_rashod * _coef - _inventory;
                            r["zakaz_manager"] = _sred_rashod * _coef - _inventory;
                        }
                    }

                    dtSetOrders.Tables[0].AcceptChanges();
                }
                else
                {
                    DoOnUIThread(delegate()
                              {
                                  inventory.Caption = "Глобальный переучёт";
                              });
                    foreach (DataRow r in dtSetOrders.Tables[0].Rows)
                    {
                        decimal _nettoTos = decimal.Parse(r["nettoTos"] == DBNull.Value ? "0" : r["nettoTos"].ToString());
                        if (_nettoTos != 0)
                            r["inventory"] = _nettoTos;
                        else
                            r["inventory"] = DBNull.Value;
                    }
                    dtSetOrders.Tables[0].AcceptChanges();

                    foreach (DataRow r in dtSetOrders.Tables[0].Rows)
                    {
                        //ost_on_date
                        //sred_rashod
                        //inventory
                        //coef

                        decimal _ost_on_date = decimal.Parse(r["ost_on_date"].ToString());
                        decimal _sred_rashod = decimal.Parse(r["sred_rashod"].ToString());
                        decimal _inventory = decimal.Parse(r["inventory"] == DBNull.Value ? "0" : r["inventory"].ToString());
                        decimal _coef = decimal.Parse(r["coef"].ToString());

                        if (rbOst.Checked)
                        {
                            r["zakaz"] = _sred_rashod * _coef - _ost_on_date;
                            r["zakaz_manager"] = _sred_rashod * _coef - _ost_on_date;
                        }
                        else
                        {
                            r["zakaz"] = _sred_rashod * _coef - _inventory;
                            r["zakaz_manager"] = _sred_rashod * _coef - _inventory;
                        }
                    }

                    dtSetOrders.Tables[0].AcceptChanges();
                }
            }
            else
            {
                rowisCalcByRemains = dtSetOrders.Tables[0].Select("isCalcByRemains is not null");
                if (rowisCalcByRemains.Count() == 0)
                {
                    foreach (DataRow r in dtSetOrders.Tables[0].Rows)
                    {
                        //ost_on_date
                        //sred_rashod
                        //inventory
                        //coef

                        decimal _ost_on_date = decimal.Parse(r["ost_on_date"].ToString());
                        decimal _sred_rashod = decimal.Parse(r["sred_rashod"].ToString());
                        decimal _inventory = decimal.Parse(r["inventory"] == DBNull.Value ? "0" : r["inventory"].ToString());
                        decimal _coef = decimal.Parse(r["coef"].ToString());

                        if (rbOst.Checked)
                        {
                            r["zakaz"] = _sred_rashod * _coef - _ost_on_date;
                            r["zakaz_manager"] = _sred_rashod * _coef - _ost_on_date;
                        }
                        else
                        {
                            r["zakaz"] = _sred_rashod * _coef - _inventory;
                            r["zakaz_manager"] = _sred_rashod * _coef - _inventory;
                        }
                    }

                    dtSetOrders.Tables[0].AcceptChanges();
                }
                else
                {
                    rowisCalcByRemains = dtSetOrders.Tables[0].Select("isCalcByRemains = 1");

                    if (rowisCalcByRemains.Count() != 0)
                    {
                        DoOnUIThread(delegate()
                            {
                                rbOst.Checked = true;
                            });
                    }
                    else
                    {
                        rowisCalcByRemains = dtSetOrders.Tables[0].Select("isCalcByRemains = 0");
                        if (rowisCalcByRemains.Count() != 0)
                        {
                            DoOnUIThread(delegate()
                            {
                                rbInv.Checked = true;
                            });
                        }
                    }

                    foreach (DataRow r in dtSetOrders.Tables[0].Rows)
                    {
                        decimal _ost_on_date = decimal.Parse(r["ost_on_date"].ToString());
                        decimal _sred_rashod = decimal.Parse(r["sred_rashod"].ToString());
                        decimal _inventory = decimal.Parse(r["inventory"] == DBNull.Value ? "0" : r["inventory"].ToString());
                        decimal _coef = decimal.Parse(r["coef"].ToString());

                        if (rbOst.Checked)
                        {
                            r["zakaz"] = _sred_rashod * _coef - _ost_on_date;
                        }
                        else
                        {
                            r["zakaz"] = _sred_rashod * _coef - _inventory;
                        }
                    }
                    dtSetOrders.Tables[0].AcceptChanges();
                }
            }
        }

        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }

        private void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmWaiting.Close();
            EnableControlsService.RestoreControlEnabledState(this);

            grdMain.DataSource = dtSetOrders.Tables[0];
            grdMain.ForceInitialize();
            ShowAllDetails();
            FilterGoods();
            id_for_delete.Clear();
            SetButtonsEnabled();
        }

        private void grdHeader_MouseWheel(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.CloseEditor();
            if (e.Delta < 0)
                view.MoveNext();
            else if (e.Delta > 0)
                view.MovePrev();
            DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void grdHeader_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (grdHeader.FocusedColumn.FieldName == "inventory")
            //{
            //    DataRow current_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);
            //    if (current_row != null && Convert.ToInt32(current_row["nzatar"]) > 0)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        private void grdBody_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (UserSettings.User.StatusCode == "ДМН" && e.Column.Name == "check")
            {
                DevExpress.XtraGrid.Views.Grid.GridView grid = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DataRow current_body_row = grid.GetDataRow(e.RowHandle);

                if (Convert.ToBoolean(current_body_row["is_select"]) && current_body_row["id_trequest"] == DBNull.Value)
                {
                    current_body_row["checked"] = !Convert.ToBoolean(current_body_row["checked"]);

                    btnCreateRequests.Enabled = CheckedOrdersCountWithoutRequests() > 0;
                }
            }
        }

        private void repCmbGrp3_Leave(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.GridLookUpEdit grid = sender as DevExpress.XtraEditors.GridLookUpEdit;
            DevExpress.XtraGrid.Views.Grid.GridView view = grid.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;

            DataRow current_row = view.GetDataRow(view.FocusedRowHandle);
            DataRow current_head_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);
            int tmp;
            if (current_row == null) tmp = 0;
            else tmp = Convert.ToInt32(current_row["id"]);
            if (tmp != Convert.ToInt32(current_head_row["id_grp3"]) && !isChange)
            {
                StartLogSaveGrp3(current_head_row, current_row);
                proc.SaveGrp3(Convert.ToInt32(current_head_row["id"]), Convert.ToInt32(current_head_row["id_grp3"]));
                StopLogSaveGrp3();
            }
            isChange = false;
        }

        private void repCmbGrp3_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.GridLookUpEdit grid = sender as DevExpress.XtraEditors.GridLookUpEdit;
            DevExpress.XtraGrid.Views.Grid.GridView view = grid.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
            
            DataRow current_row = view.GetDataRow(view.FocusedRowHandle);
            DataRow current_head_row = grdHeader.GetDataRow(grdHeader.FocusedRowHandle);
            int tmp;
            if (current_row == null) tmp = 0;
            else tmp = Convert.ToInt32(current_row["id"]);
            if (tmp != Convert.ToInt32(current_head_row["id_grp3"]))
            {
                StartLogSaveGrp3(current_head_row, current_row);
                proc.SaveGrp3(Convert.ToInt32(current_head_row["id"]), Convert.ToInt32(current_row["id"]));
                StopLogSaveGrp3();
                isChange = true;
            }
           
        }

        private void StartLogSaveGrp3(DataRow current_head_row, DataRow current_row)
        {
            int id_grp3 = Convert.ToInt32(current_head_row["id_grp3"]);
            Logging.StartFirstLevel(id_grp3 == 0 ? 995 : 996);
            Logging.Comment("Начало " + (id_grp3 == 0 ? "добавления" : "редактирования") + " подгруппы");
            Logging.Comment("id товара = " + current_head_row["id"].ToString() + ", ean = '" + current_head_row["ean"].ToString() + "', название товара = '" + current_head_row["cname"].ToString() + "'");
            if (current_row != null)
            if (id_grp3 == 0)
            {
                Logging.Comment("id_grp3 = " + current_row["id"].ToString() + ", название = " + current_row["cname"].ToString());
            }
            else
            {
                Logging.VariableChange("id_grp3", Convert.ToInt32(current_row["id"]), id_grp3);
                Logging.VariableChange("название", current_row["cname"].ToString(), current_head_row["sub_group"].ToString());
            }
        }

        private void StopLogSaveGrp3()
        {
            Logging.Comment("Конец сохранения подгруппы");
            Logging.StopFirstLevel();
        }

        private bool SomethingChangedInHeader()
        {
            if (dtSetOrders != null)
            {
                var changes = from table1 in dtSetOrders.Tables[0].AsEnumerable()
                              join table2 in dtHeader_old.AsEnumerable() on table1["id"].ToString() equals table2["id"].ToString()
                              where 
                              !((table1["inventory"] == DBNull.Value && table2["inventory"] == DBNull.Value) || (table1["inventory"] != DBNull.Value && table2["inventory"] != DBNull.Value && table1.Field<decimal>("inventory") == table2.Field<decimal>("inventory"))) 
                              ||
                              table1.Field<decimal>("zakaz_manager") != table2.Field<decimal>("zakaz_manager")
                              select table1;

                return changes.Count() > 0;
            }
            return false;
        }

        private bool SomethingChangedInBody()
        {
            if (dtSetOrders != null)
            {
               if (dtSetOrders.Tables[1].Rows.Count != dtBody_old.Rows.Count)
               {
                   return true;
               }

               if (ExistNewOrderBody())
               {
                   return true;
               }

               return OrderBodyChanged();
            }
            return false;
        }

        private bool ExistNewOrderBody()
        {
            return dtSetOrders.Tables[1].Select("id_order_body = 0").Length > 0;
        }

        private bool OrderBodyChanged()
        {
            var changes = from table1 in dtSetOrders.Tables[1].AsEnumerable()
                          join table2 in dtBody_old.AsEnumerable() on table1["id_order_body"].ToString() equals table2["id_order_body"].ToString()
                          where
                                table1.Field<bool>("checked") != table2.Field<bool>("checked") ||
                                !((table1["id_post"] == DBNull.Value && table2["id_post"] == DBNull.Value) || (table1["id_post"] != DBNull.Value && table2["id_post"] != DBNull.Value && table1.Field<int>("id_post") == table2.Field<int>("id_post"))) ||
                                !((table1["fact_netto"] == DBNull.Value && table2["fact_netto"] == DBNull.Value) || (table1["fact_netto"] != DBNull.Value && table2["fact_netto"] != DBNull.Value && table1.Field<decimal>("fact_netto") == table2.Field<decimal>("fact_netto"))) ||
                                !((table1["caliber"] == DBNull.Value && table2["caliber"] == DBNull.Value) || (table1["caliber"] != DBNull.Value && table2["caliber"] != DBNull.Value && table1.Field<string>("caliber") == table2.Field<string>("caliber"))) ||
                                !((table1["id_subject"] == DBNull.Value && table2["id_subject"] == DBNull.Value) || (table1["id_subject"] != DBNull.Value && table2["id_subject"] != DBNull.Value && table1.Field<int>("id_subject") == table2.Field<int>("id_subject"))) ||
                                !((table1["zcena"] == DBNull.Value && table2["zcena"] == DBNull.Value) || (table1["zcena"] != DBNull.Value && table2["zcena"] != DBNull.Value && table1.Field<decimal>("zcena") == table2.Field<decimal>("zcena")))
                          select table1;

            return changes.Count() > 0;
        }

        private void repTxtFactNetto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (! (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]))
            {
                e.Handled = true;
            }
        }

        private void grdHeader_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Column.Name == "inventory" && UserSettings.User.StatusCode == "ДМН")
                {
                    ShowAddZatar(e.RowHandle);
                }
                else if ((e.Column.Name == "sred_rashod" || e.Column.Name == "ost_on_date") && UserSettings.User.StatusCode == "РКВ")
                {
                    ShowRealiz();
                }
            }
        }

        private void grdHeader_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle != -1)
            {
                e.Appearance.BackColor = e.RowHandle % 2 == 0 ? Color.FromArgb(193, 255, 193) : Color.FromArgb(253, 253, 193);
            }
        }

        //NEW
        private void radioButton1_Click(object sender, EventArgs e)
        {
            grdMain_Load();
            SetGridReadOnly();
        }
    }
}
