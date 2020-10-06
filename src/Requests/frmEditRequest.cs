using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Settings.Connection;
using System.Collections;
using Nwuram.Framework.Logging;
using System.Timers;
using System.Threading;

namespace Requests
{
    public partial class frmEditRequest : Form
    {
        /// <summary>
        /// Режим открытия заявки 0 - просмотр, 1 - создание, 2 - редактирование, -1 - закрытие формы без вывода сообщений
        /// </summary>
        private int Mode; 

        /// <summary>
        /// Номер заявки
        /// </summary>
        private int idTReq; 

        /// <summary>
        /// Параметры заявки
        /// </summary>
        DataRow reqSettings; 
        private int reqIdPost;
        decimal oldPN, oldTB;
        BindingSource bsReqGoods = new BindingSource();
        private DataTable dtReqGoods;// { get; set; }
        DataTable dtCreditTypes;
        DataTable dtOldReqData;
        DataRow dtOldReqSettings;

        int reqIdOper = -1;

        DataRow curRow; //Текущая выбраная строка в гриде
        Color rowBK; //Цвет покраски строки грида
        private bool isCopy; //Флаг копирования
        int id_dep;
        ArrayList idTRequests = new ArrayList();
        ArrayList deletedIdReq = new ArrayList();
        public int curIdType { get; set; } //тип заявки

        System.Timers.Timer autoSaveTimer;
        bool autoSave = false;
        int autoSaveTreqId = 0;
        bool restoreAutoSaved = false;
        DataTable autoRequestHead;
        ArrayList autoSaveDeletedIdReq = new ArrayList();

        bool checkSertificate = false;
        int days_sertificate = 0;

        //NEW 27.07.2017

        private bool isWeInOut = false;

        public frmEditRequest(int Mode, int idReq, bool isCopy)
        {
            InitializeComponent();
            this.Mode = Mode;
            this.idTReq = idReq;
            this.isCopy = isCopy;
        }

        public frmEditRequest(int Mode, int idReq, bool isCopy, bool autoSaved, int autoSavedTreqId)
        {
            InitializeComponent();
            this.Mode = Mode;
            this.idTReq = idReq;
            this.isCopy = isCopy;
            this.restoreAutoSaved = autoSaved;
            this.autoSaveTreqId = autoSavedTreqId;
        }

        private void frmEditRequest_Load(object sender, EventArgs e)
        {
            Config.genSelectedTovar(idTReq);
            Config.curDate = Config.hCntMain.GetCurDate(false);
            GetCbTypes();
            GetDeps();
            GetCreditTypes();
            SetReqParams();
            GetWindows((int)cbDep.SelectedValue);
            GetUl((int)cbDep.SelectedValue, dtpDateOut.Value, true);
            //SetControlsEnabled();

            //Поле с номером заявки
            lbNumber.Visible =
            tbNumber.Visible = (Mode != 1);

            if (Mode != 0)
            {
                Config.linkToCurrentRequest = this;
            }

            SetControlsEnabled();

            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН") && Mode == 1)
            {
                autoSave = Config.hCntMain.GetProperties("autosave");
                if (autoSave)
                {
                    autoSaveTimer = new System.Timers.Timer();
                    // Tell the timer what to do when it elapses
                    autoSaveTimer.Elapsed += new ElapsedEventHandler(AutoSaveRequest);
                    // Set it to go off every five seconds
                    DataTable autoSaveTime = Config.hCntMain.GetProperties();
                    DataRow[] autoSaveTimeRow = autoSaveTime.Select("id_val = 'autosavetime'");
                    if (autoSaveTimeRow.Length > 0)
                    {
                        autoSaveTimer.Interval = Convert.ToInt32(autoSaveTimeRow[0]["val"]) * 60 * 1000;
                    }
                    else
                    {
                        autoSaveTimer.Interval = 60 * 1000;
                    }
                    // And start it        
                    autoSaveTimer.Enabled = true;
                }
            }

            checkSertificate = Config.hCntMain.GetSettings("cdep").Select("value = " + UserSettings.User.IdDepartment.ToString()).Length > 0;
            DataTable settings = (id_dep == 6 ? Config.hCntAdd : Config.hCntMain).GetSettings("days");
            days_sertificate = settings != null && settings.Rows.Count > 0 ? Convert.ToInt32(settings.Rows[0]["value"]) : 0;
            lblSertifOutSoon.Text = "Сертификат заканчивается через " + days_sertificate.ToString() + " дней";
        }


        public void AutoSaveRequest(object sender, EventArgs e)
        {
            int ntypeorg = -1;
            string cprimech = "";
            int id_operand = -1;
            int porthole = -1;
            int id_typeBonus = -1;
            DateTime dateRequest = DateTime.Now;
            string post_name = "";
            string type_name = "";
            string bonus_name = "";
            DoOnUIThread(delegate()
            {
                ntypeorg = Convert.ToInt32(cbUL.SelectedValue);
                cprimech = tbPrimech.Text.Trim();
                id_operand = Convert.ToInt32(cbType.SelectedValue);
                porthole = Convert.ToInt32(cbWindow.SelectedValue);
                id_typeBonus = Convert.ToInt32(cbBonusType.SelectedValue);
                dateRequest = dtpDateOut.Value;
                post_name = tbPost.Text.Trim();
                type_name = cbType.Text;
                bonus_name = cbBonusType.Text;
            });

            bool log = false;
            if (autoSaveTreqId == 0)
            {
                StartLogAutoSave(dateRequest, post_name, ntypeorg, porthole, type_name, bonus_name);
                log = true;
            }

            autoSaveTreqId = id_dep != 6 ? Config.hCntMain.SetAutoRequestHead(autoSaveTreqId, dateRequest, reqIdPost, ntypeorg, cprimech, id_operand, porthole, id_typeBonus, Config.GetStringFromArray(autoSaveDeletedIdReq))
                                         : Config.hCntAdd.SetAutoRequestHead(autoSaveTreqId, dateRequest, reqIdPost, ntypeorg, cprimech, id_operand, porthole, id_typeBonus, Config.GetStringFromArray(autoSaveDeletedIdReq));
            autoSaveDeletedIdReq.Clear();

            if (log)
            {
                StopLogAutoSave();
                log = false;
            }
        }

        private void StartLogAutoSave(DateTime date, string post_name, int ntypeorg, int porthole, string type_name, string bonus_name)
        {
            Logging.StartFirstLevel(959);
            Logging.Comment("Начало автосохранения заявки");
            Logging.Comment("Дата заявки = " + date.ToShortDateString() + ", id поставщика = " + reqIdPost.ToString() + ", название поставщика = " + post_name + ", ЮЛ =" + ntypeorg.ToString() + ", номер окна = " + porthole.ToString() + ", тип заявки = " + type_name + ", тип бонуса = " + bonus_name + ", номер отдела = " + UserSettings.User.IdDepartment.ToString() + ", отдел = " + UserSettings.User.Department);
        }

        private void StopLogAutoSave()
        {
            Logging.Comment("id автосохранённой заявки = " + autoSaveTreqId.ToString());
            Logging.Comment("Конец автосохранения заявки");
            Logging.StopFirstLevel();
        }

        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }

        public DataTable getGoodsData()
        {
            return dtReqGoods;
        }

        /// <summary>
        /// Проставляем начальную ширину колонок
        /// </summary>
        private void SetColumnsWidth()
        {
            // Признак отображения колонок
            bool show = true;

            //колонки не отображаются только при просмотре заявок
            //в режиме ПР
            //если у пользователя в настройках установлен признак zcena = 0 (или признака в настройках нет)
            if (Mode == 0)
            {
                show = Config.hCntMain.NedoUser() ? false : true;
            }            

            zcena.Visible
                = sum.Visible
                = show;

            if (show)
            {
                ean.Width = (int)(grdRequestGoods.Width * 0.095);
                cname.Width = (int)(grdRequestGoods.Width * 0.3);
                zapas2.Width = (int)(grdRequestGoods.Width * 0.06);
                zcena.Width = (int)(grdRequestGoods.Width * 0.07);
                rcena.Width = (int)(grdRequestGoods.Width * 0.07);
                procent.Width = (int)(grdRequestGoods.Width * 0.046);
                nzatar.Width = (int)(grdRequestGoods.Width * 0.045);
                zakaz.Width = (int)(grdRequestGoods.Width * 0.07);
                sum.Width = (int)(grdRequestGoods.Width * 0.075);
                cprimech.Width = (int)(grdRequestGoods.Width * 0.085);
                CauseOfDecline.Width = (int)(grdRequestGoods.Width * 0.088);
            }
            else
            {
                ean.Width = (int)(grdRequestGoods.Width * 0.095);
                //cname.Width = (int)(grdRequestGoods.Width * 0.3);
                cname.Width = (int)(grdRequestGoods.Width * (0.3 + 0.07));
                zapas2.Width = (int)(grdRequestGoods.Width * 0.06);
                //zcena.Width = (int)(grdRequestGoods.Width * 0.07);
                rcena.Width = (int)(grdRequestGoods.Width * 0.07);
                procent.Width = (int)(grdRequestGoods.Width * 0.046);
                nzatar.Width = (int)(grdRequestGoods.Width * 0.045);
                zakaz.Width = (int)(grdRequestGoods.Width * 0.07);
                //sum.Width = (int)(grdRequestGoods.Width * 0.075);
                cprimech.Width = (int)(grdRequestGoods.Width * (0.085 + 0.075));
                CauseOfDecline.Width = (int)(grdRequestGoods.Width * 0.088);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Доступность кнопок и полей
        /// </summary>
        private void SetControlsEnabled()
        {
            if (Mode == 0)
            {
                foreach (Control con in this.Controls)
                {
                    if (con.Name != "btExcel"
                        && con.Name != "btPrint"
                        && con.Name != "btExit"
                        && con.Name != "btExcelCredit" 
                        && con.GetType() != typeof(Label)
                        && con.GetType() != typeof(DataGridView))
                    {
                        con.Enabled = false;
                    }

                }

                btExcelCredit.Enabled = (new int[2] { 3, 4 }.Contains((int)reqSettings["credit_type"]));
                btNewGood.Visible =
                    btDelGood.Visible =
                    btSave.Visible =
                    btEditGood.Visible = false;
            }
            else
            {                
                dtpDateOut.Enabled = (Mode != 0);
                btExcel.Visible =
                btPrint.Visible =
                btExcelCredit.Visible = 
                false;

                if (Mode == 2)
                {
                    lbCreditType.Visible =
                   cbCreditType.Visible =
                   ((bool)reqSettings["CreditContains"] == true);
                }

                cbDep.Enabled = false;
                btEditGood.Enabled =
                    btSave.Enabled =
                    btDelGood.Enabled = grdRequestGoods.Rows.Count != 0;

                tbMan.Enabled =
                    tbTekOst.Enabled =
                    tbCommonOrder.Enabled =
                    tbAvgRash.Enabled =
                    tbZapas.Enabled =
                    tbStorageTerm.Enabled =
                    tbOtsechStart.Enabled =
                    tbOtsechFinish.Enabled =
                    tbPlanRealiz.Enabled = false;

                //Временно заблокирован комбик кредитов
                cbCreditType.Enabled = false;

                //Блокируем выбор типов
                if ((Mode != 1 || (cbType.SelectedValue != null && (int)cbType.SelectedValue != 0)) && dtReqGoods != null && (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН"))
                {
                    cbType.Enabled = (dtReqGoods.Select("isProh = true").Count() == 0 || !new int[2] { 2, 8 }.Contains((int)cbType.SelectedValue));
                }
            }
        }

        /// <summary>
        /// Устанавливаем изначальные параметры заявки
        /// </summary>
        private void SetReqParams()
        {
            if (Mode != 1 || isCopy) //Редактирование, Просмотр, Копирование
            {
                tbNumber.Text = idTReq.ToString().Trim();
                dtOldReqSettings = reqSettings = Config.dtRequests.Select("req_num = " + idTReq.ToString())[0];
                id_dep = (int)reqSettings["id_dep"];
                dtpDateOut.Value = (isCopy ? Config.curDate: (DateTime)reqSettings["req_date"]);
                reqIdPost = (int)reqSettings["id_post"];
                tbPost.Text = reqSettings["post_name"].ToString().Trim();
                cbType.SelectedValue = curIdType = (int)reqSettings["id_oper"];              
                cbDep.SelectedValue = id_dep;
                tbMan.Text = reqSettings["man_name"].ToString();
                tbPrimech.Text = reqSettings["cprimech"].ToString();
                cbCreditType.SelectedValue = (int)reqSettings["credit_type"];
                tbTerm.Text = ((int)reqSettings["CreditPeriod"] != 0 ? reqSettings["CreditPeriod"].ToString() : "");
                if (isCopy)
                {
                    reqSettings["statComment"] =
                        reqSettings["cprimech"] = "";
                    reqSettings["id_unit"] = 0;
                }
                lbRBSixPrimech.Text = reqSettings["statComment"].ToString().Trim();
                tbPrimech.Text = reqSettings["cprimech"].ToString();
            }
            else if (Mode == 1 && restoreAutoSaved)
            {
                autoRequestHead = UserSettings.User.IdDepartment != 6 ? Config.hCntMain.GetAutoRequestHead() : Config.hCntAdd.GetAutoRequestHead();

                if (autoRequestHead != null && autoRequestHead.Rows.Count > 0)
                {
                    DataRow autoRequestHeadRow = autoRequestHead.Rows[0];
                    dtpDateOut.Value = Convert.ToDateTime(autoRequestHeadRow["DateRequest"]);
                    reqIdPost = Convert.ToInt32(autoRequestHeadRow["id_Post"]);
                    tbPost.Text = autoRequestHeadRow["post_name"].ToString();
                    cbType.SelectedValue = curIdType = Convert.ToInt32(autoRequestHeadRow["id_operand"]);
                    cbDep.SelectedValue = id_dep = UserSettings.User.IdDepartment;
                    tbMan.Text = UserSettings.User.FullUsername;
                    tbPrimech.Text = autoRequestHeadRow["cprimech"].ToString();
                    cbBonusType.SelectedValue = Convert.ToInt32(autoRequestHeadRow["id_TypeBonus"]);
                }
            }
            else //Создание
            {
                Config.curDate = Config.hCntMain.GetCurDate(false);
                reqIdPost = 0;
                cbDep.SelectedValue = id_dep = UserSettings.User.IdDepartment;
                cbType.SelectedValue = -1;
                tbMan.Text = UserSettings.User.FullUsername;
                //if(isCopy)
                //{
                //    GetGrdReqGoods();
                //}
            }
            GetGrdReqGoods();

            if (Mode != 0)
            {
                dtpDateOut.MinDate = Config.curDate;
            }

            //NEW 27.07.2017 проверка на поставщика из настроек
            viewDataWeInOut();

        }

        /// <summary>
        /// Получение данных для комбика типов
        /// </summary>
        private void GetCbTypes()
        {
            DataTable dtTypes = Config.hCntMain.GetTypes("1,2,3,8", false);
            cbType.DataSource = dtTypes;
            cbType.DisplayMember = "sname";
            cbType.ValueMember = "id";
        }

        /// <summary>
        /// Получение Срока кредита\Отсрочки
        /// </summary>
        private void SetCreditTermsVisible()
        {
            if (new int[3] { 2, 3, 4 }.Contains((int)cbCreditType.SelectedValue))
            {
                //lbTerm.Visible = tbTerm.Visible = true;
                lbTerm.Text = ((int)cbCreditType.SelectedValue == 2 ? "Отсрочка:" : "Срок кредита:");
            }
            else
            {
                lbTerm.Visible = tbTerm.Visible = false;
            }
        }

        /// <summary>
        /// Получение данных для комбика отделов
        /// </summary>
        private void GetDeps()
        {
            DataTable dtDeps = Config.hCntMain.GetDeps();
            cbDep.DataSource = dtDeps;
            cbDep.ValueMember = "id";
            cbDep.DisplayMember = "name";
        }

        /// <summary>
        /// Получение данных для комбика окон
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        private void GetWindows(int id_dep)
        {
            DataTable dtWindow = Config.hCntMain.GetWindows(id_dep);
            cbWindow.DataSource = dtWindow;
            cbWindow.DisplayMember = "cname";
            cbWindow.ValueMember = "porthole";

            if (Mode != 1 || isCopy)
            {
                if (dtWindow.Select("porthole = " + reqSettings["porthole"].ToString()).Count() > 0)
                {
                    cbWindow.SelectedValue = int.Parse(reqSettings["porthole"].ToString());
                }
                else
                {
                    cbWindow.SelectedIndex = -1;
                }
            }
            else if (Mode == 1 && restoreAutoSaved && autoRequestHead != null && autoRequestHead.Rows.Count > 0)
            {
                cbWindow.SelectedValue = Convert.ToInt32(autoRequestHead.Rows[0]["porthole"]);
            }
            else
            {
                Config.dtProperties = Config.hCntMain.GetProperties();
                DataRow[] drWind = Config.dtProperties.Select("id_val = 'def Window'");
                if (drWind != null && drWind.Count() > 0 && dtWindow.Select("porthole = " + drWind[0]["val"].ToString()).Count() > 0)
                {
                    cbWindow.SelectedValue = int.Parse(drWind[0]["val"].ToString());
                }
                else
                {
                    cbWindow.SelectedIndex = (dtWindow.Rows.Count > 0 ? 0 : -1);
                }
            }
        }

        /// <summary>
        /// Получение данных для комбика ЮЛ
        /// </summary>
        /// <param name="idDep">id отдела</param>
        /// <param name="date">на дату</param>
        private void GetUl(int idDep, DateTime date, bool firstTime)
        {
            int selectedUL = 0;

            if (cbUL.SelectedValue != null)
                selectedUL = (int)cbUL.SelectedValue;

            if (reqIdPost != 0)
            {
                DataTable dtUL = id_dep != 6 ? Config.hCntMain.GetUl(idDep, date, reqIdPost, Mode == 0, (cbType.SelectedValue != null && (int)cbType.SelectedValue == 3))
                                             : Config.hCntAdd.GetUl(idDep, date, reqIdPost, Mode == 0, (cbType.SelectedValue != null && (int)cbType.SelectedValue == 3));
                if (dtUL == null || dtUL.Rows.Count == 0)
                {
                    UlNotFound(true);
                    return;
                }
                cbUL.DataSource = dtUL;
                cbUL.DisplayMember = "Abbriviation";
                cbUL.ValueMember = "nTypeOrg";
                cbUL.Enabled = (Mode != 0);

                if (Mode == 1 && restoreAutoSaved)
                {
                    cbUL.SelectedValue = Convert.ToInt32(autoRequestHead.Rows[0]["ntypeorg"]);
                }
                else if (firstTime) //Редактирование, Просмотр, Копирование
                {
                    cbUL.SelectedValue = reqSettings["ntypeorg"];
                }
                else
                {
                    if (dtUL.Rows.Count == 1)
                    {
                        cbUL.SelectedIndex = 0;
                    }
                    else
                    {
                        if (dtUL.Select("nTypeOrg = " + selectedUL.ToString()).Count() > 0)
                        {
                            cbUL.SelectedValue = selectedUL;
                        }
                        else
                        {
                            cbUL.SelectedValue = 0;
                        }
                    }
                }
            }
            else
            {
                UlNotFound(false);
            }
        }

        /// <summary>
        /// Юр лицо не найдено
        /// </summary>
        /// <param name="showMessage">Показывать ли сообщение</param>
        private void UlNotFound(bool showMessage)
        {
            if (showMessage)
                MessageBox.Show("На указанную дату нет\nдействующих ЮЛ.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            cbUL.DataSource = null;
            cbUL.Enabled = false;
        }

        private void cbType_SelectedValueChanged(object sender, EventArgs e)
        {
            int selType;

            if (cbType.SelectedValue != null && int.TryParse(cbType.SelectedValue.ToString(), out selType) && selType == 3)
            {
                lbBonusType.Visible = cbBonusType.Visible = true;
                GetBonusTypes();
            }
            else
            {
                lbBonusType.Visible = 
                    cbBonusType.Visible = 
                    lbPN.Visible =
                    tbPN.Visible =
                    lbTB.Visible =
                    tbTB.Visible = false;
            }

            //if (grdRequestGoods.Rows.Count > 0 && cbType.Enabled == true)
            //{
            //    cbType.Enabled = (UserSettings.User.StatusCode != "МН");
            //}
        }

        /// <summary>
        /// Получение данных для комбика типов бонуса
        /// </summary>
        private void GetBonusTypes()
        {
            DataTable dtBonusTypes = Config.hCntMain.GetBonusTypes(true);
            cbBonusType.DataSource = dtBonusTypes;
            cbBonusType.DisplayMember = "cName";
            cbBonusType.ValueMember = "id";
            if (Mode != 1 || isCopy)
            {
                cbBonusType.SelectedValue = reqSettings["id_TypeBonus"];
            }
        }

        private void cbBonusType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bonusType;
            if (cbBonusType.SelectedValue != null &&
                int.TryParse(cbBonusType.SelectedValue.ToString(), out bonusType) && 
                bonusType == 5)
            {
                lbTB.Visible =
                    tbTB.Visible =
                    lbPN.Visible =
                    tbPN.Visible =
                    true;
                GetBonusProp(idTReq);
            }
            else
            {
                lbTB.Visible =
                tbTB.Visible =
                lbPN.Visible =
                tbPN.Visible =
                false;
            }
        }

        /// <summary>
        /// Получение параметров бонуса
        /// </summary>
        /// <param name="id_req">id заявки</param>
        private void GetBonusProp(int id_req)
        {
            DataTable dtBonusProp = Config.hCntMain.GetBonusProp(id_req);
            if(dtBonusProp != null && dtBonusProp.Rows.Count != 0)
            {
                tbPN.Text = dtBonusProp.Rows[0]["Deficit"].ToString();
                tbTB.Text = dtBonusProp.Rows[0]["Goods"].ToString();
                oldPN = (decimal)dtBonusProp.Rows[0]["Deficit"];
                oldTB = (decimal)dtBonusProp.Rows[0]["Goods"]; 
            }
        }

        /// <summary>
        /// Установка источника для грида
        /// </summary>
        private void GetGrdReqGoods()
        {
            Config.ChangeFormEnabled(this, false);
            this.pbEditReq.Visible = true;
            bgwEditReq.RunWorkerAsync();
        }

        private void frmEditRequest_Shown(object sender, EventArgs e)
        {
            SetColumnsWidth();
        }

        /// <summary>
        /// Получение данных для комбика типов кредита
        /// </summary>
        private void GetCreditTypes()
        {
            dtCreditTypes = Config.GetCreditTypes(false);
            cbCreditType.DataSource = dtCreditTypes;
            cbCreditType.ValueMember = "id";
            cbCreditType.DisplayMember = "cname";
        }

        private void tbTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !"0123456789".Contains(e.KeyChar);
        }

        private void cbCreditType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int crType;
            if(int.TryParse(cbCreditType.SelectedValue.ToString(),out crType))
            {
                SetCreditTermsVisible();
            }
        }

        private void grdRequestGoods_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                //Проверка на наличие библиотеки с формами
                if (!System.IO.File.Exists(Application.StartupPath + "\\PrihodRealizForms.dll"))
                {
                    MessageBox.Show("Библиотека \"Движение и реализация товара\" не найденна.\n Обратитесь в ОЭЭС.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    curRow = dtReqGoods.DefaultView[e.RowIndex].Row;
                    //Открытие формы движения товара
                    if (e.ColumnIndex ==grdRequestGoods.Columns["zcena"].Index)
                    {
                        if (int.Parse(curRow["nprimech"].ToString()) == 1)
                        {
                            MessageBox.Show("Нет данных по приходам.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            Config.ShowPrihodRealiz(int.Parse(curRow["id_tovar"].ToString()), curRow["ean"].ToString(), curRow["cName"].ToString(), (int)cbDep.SelectedValue, true);
                        }
                    }

                    //Открытие формы реализации товара
                    if (e.ColumnIndex == grdRequestGoods.Columns["rcena"].Index)
                    {
                        if (int.Parse(curRow["nprimech"].ToString()) == 1)
                        {
                            MessageBox.Show("Нет данных по реализации.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            Config.ShowPrihodRealiz(int.Parse(curRow["id_tovar"].ToString()), curRow["ean"].ToString(), curRow["cName"].ToString(), (int)cbDep.SelectedValue, false);
                        }
                    }

                    if (e.ColumnIndex == grdRequestGoods.Columns["CauseOfDecline"].Index)
                    {
                        ttLegend.Show("tip", this, Cursor.Position.X - this.MdiParent.Location.X - 240, Cursor.Position.Y - this.MdiParent.Location.Y - 40);
                    }

                    if (e.ColumnIndex == grdRequestGoods.Columns["ean"].Index)
                    {
                        dllForStatusPrihodTovar.Form1 frmStatPrih = new dllForStatusPrihodTovar.Form1(id_dep, int.Parse(curRow["id_tovar"].ToString()));
                        //frmStatPrih.FormBorderStyle = FormBorderStyle.None;
                        frmStatPrih.ShowDialog();
                    }   
               
                    //curRow = dtGoods.DefaultView[e.RowIndex].Row;
                    if (e.ColumnIndex == grdRequestGoods.Columns["cname"].Index)
                    {
                        ttGoodName.Show(id_dep != 6 ? Config.hCntMain.GetFullNameTovar((int)curRow["id_tovar"]) : Config.hCntAdd.GetFullNameTovar((int)curRow["id_tovar"]), this, Cursor.Position.X - this.MdiParent.Location.X, Cursor.Position.Y - this.MdiParent.Location.Y - 40);
                    }
                }
            }
        }

        private void grdRequestGoods_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                curRow = dtReqGoods.DefaultView[e.RowIndex].Row;
                if(Mode != 0)
                    Config.currentEan = curRow["ean"].ToString();
                SetGoodParameters();
            }
        }

        /// <summary>
        /// Заполняем параметры для текущего товара
        /// </summary>
        private void SetGoodParameters()
        {
            //if(Mode != 1)
            if (curRow != null)
            {
                tbTekOst.Text = ((decimal)curRow["TekOst"]).ToString("0.000");

                if (decimal.Parse(curRow["PlanRealiz"].ToString()) != -1)
                {
                    tbPlanRealiz.Visible = lbPlanRealiz.Visible = true;
                    tbPlanRealiz.Text = ((decimal)curRow["PlanRealiz"]).ToString("0.000");
                }
                else
                {
                    tbPlanRealiz.Visible = lbPlanRealiz.Visible = false;
                }

                if (curRow["BeginOfPeriod"].ToString().Trim().Length != 0)
                {
                    lbOtsech.Visible = tbOtsechStart.Visible = true;
                    tbOtsechStart.Text = curRow["BeginOfPeriod"].ToString();
                }
                else
                {
                    lbOtsech.Visible = tbOtsechStart.Visible = false;
                }

                if (curRow["EndOfPeriod"].ToString().Trim().Length != 0)
                {
                    lbOtsechFinish.Visible = tbOtsechFinish.Visible = true;
                    tbOtsechFinish.Text = curRow["EndOfPeriod"].ToString();
                }
                else
                {
                    lbOtsechFinish.Visible = tbOtsechFinish.Visible = false;
                }

                tbStorageTerm.Text = curRow["PeriodOfStorage"].ToString();
                //cbWindow.SelectedValue = curRow["porthole"];
                tbZapas.Text = curRow["zapas"].ToString();
                tbCommonOrder.Text = (id_dep != 6 ?
                                    Config.hCntMain.GetCommonOrder((int)curRow["id_tovar"], id_dep).ToString("0.000") :
                                    Config.hCntAdd.GetCommonOrder((int)curRow["id_tovar"], id_dep).ToString("0.000"));

                tbAvgRash.Text = curRow["AvgReal"].ToString();
                //(id_dep != 6 ?
                //                Config.hCntMain.GetAvgRealiz((int)curRow["idReq"]).ToString("0.000") :
                //                Config.hCntAdd.GetAvgRealiz((int)curRow["idReq"]).ToString("0.000"));

            }
            else
            {
                tbTekOst.Text =
                    tbCommonOrder.Text =
                    tbAvgRash.Text =
                    tbZapas.Text =
                    tbStorageTerm.Text =
                    tbOtsechStart.Text =
                    tbOtsechFinish.Text =
                    tbPlanRealiz.Text = "";

                tbPlanRealiz.Visible = lbPlanRealiz.Visible = 
                    lbOtsech.Visible = tbOtsechStart.Visible =
                     lbOtsechFinish.Visible = tbOtsechFinish.Visible = false;
            }
        }

        //красим грид
        private void grdRequestGoods_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            rowBK = Color.White;

            if (checkSertificate && grdRequestGoods.Rows[e.RowIndex].Cells["ean"].Value.ToString().Trim().Length == 4)
            {
                int sertif = CheckSertificate(e.RowIndex);
                if (sertif == 1)
                {
                    rowBK = pnlNoSertificate.BackColor;
                }
                else if (sertif == 2)
                {
                    rowBK = pnlSertifOutSoon.BackColor;
                }
            }

            if (grdRequestGoods.Rows[e.RowIndex].Cells["BeginOfPeriod"].Value.ToString().Length != 0
               && grdRequestGoods.Rows[e.RowIndex].Cells["EndOfPeriod"].Value.ToString().Length != 0)
            {
                rowBK = Color.FromArgb(0, 127, 14);
            }

            if ((decimal)grdRequestGoods.Rows[e.RowIndex].Cells["PlanRealiz"].Value != -1)
            {
                rowBK = Color.FromArgb(0, 255, 255);
            }

            switch((int)grdRequestGoods.Rows[e.RowIndex].Cells["limitType"].Value)
            {
                case 1:
                    rowBK = Color.FromArgb(89, 177, 255);
                    break;
                case 2:
                    rowBK = Color.FromArgb(0, 167, 14);
                    break;
                case 3:
                    rowBK = Color.FromArgb(255, 76, 237);
                    break;
                case 4:
                    rowBK = Color.FromArgb(255, 139, 38);
                    break;
                case 5:
                    rowBK = Color.FromArgb(255, 255, 83);
                    break;
                case 6:
                    rowBK = Color.FromArgb(143, 109, 182);
                    break;
            }

            if (dtReqGoods.Columns.Contains("zcena_sort") && dtReqGoods.DefaultView[e.RowIndex]["zcena_sort"]!=DBNull.Value && int.Parse(dtReqGoods.DefaultView[e.RowIndex]["zcena_sort"].ToString()) == 1)
                rowBK = pRCena_sort.BackColor;

            grdRequestGoods.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                 grdRequestGoods.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rowBK;


            rowBK = Color.White;
            if (int.Parse(grdRequestGoods.Rows[e.RowIndex].Cells["nprimech"].Value.ToString()) == 1
                || grdRequestGoods.Rows[e.RowIndex].Cells["CauseOfDecline"].Value.ToString().Contains("Товар с Ср.расход<0"))
            {
                rowBK = Color.FromArgb(255, 235, 122);
            }

            if ((int)grdRequestGoods.Rows[e.RowIndex].Cells["id_nds"].Value == 1
                || (decimal)grdRequestGoods.Rows[e.RowIndex].Cells["rcena"].Value == (decimal)grdRequestGoods.Rows[e.RowIndex].Cells["zcena"].Value)
            {
                rowBK = Color.Red;
            }

            grdRequestGoods.Rows[e.RowIndex].Cells["ean"].Style.BackColor =
                grdRequestGoods.Rows[e.RowIndex].Cells["ean"].Style.SelectionBackColor = rowBK;

            if ((bool)grdRequestGoods.Rows[e.RowIndex].Cells["isProh"].Value)
            {
                grdRequestGoods.Rows[e.RowIndex].Cells["cname"].Style.BackColor =
                grdRequestGoods.Rows[e.RowIndex].Cells["cname"].Style.SelectionBackColor = Color.SpringGreen;
            }

            if ((decimal)grdRequestGoods.Rows[e.RowIndex].Cells["procent"].Value<0)
            {
                grdRequestGoods.Rows[e.RowIndex].Cells["procent"].Style.BackColor =
                grdRequestGoods.Rows[e.RowIndex].Cells["procent"].Style.SelectionBackColor = Color.Bisque;
            }

            if (dtReqGoods.DefaultView[e.RowIndex]["ost2Shop"]!=DBNull.Value &&  decimal.Parse(dtReqGoods.DefaultView[e.RowIndex]["zakaz"].ToString()) > decimal.Parse(dtReqGoods.DefaultView[e.RowIndex]["ost2Shop"].ToString()))
            {
                grdRequestGoods.Rows[e.RowIndex].Cells["CauseOfDecline"].Style.BackColor =
                grdRequestGoods.Rows[e.RowIndex].Cells["CauseOfDecline"].Style.SelectionBackColor = panel1.BackColor;
            }

        }

        private int CheckSertificate(int rowIndex)
        {
            DataTable sertif = (id_dep == 6 ? Config.hCntAdd : Config.hCntMain).CheckSertificate(Convert.ToInt32(grdRequestGoods.Rows[rowIndex].Cells["id_tovar"].Value), Convert.ToInt32(grdRequestGoods.Rows[rowIndex].Cells["id_subject"].Value));
            if (sertif != null && sertif.Rows.Count > 0)
            {
                DateTime date_end = Convert.ToDateTime(sertif.Rows[0]["date_end"]).Date;
                if (Convert.ToDateTime(sertif.Rows[0]["date_end"]) <= DateTime.Today.Date)
                {
                    return 1;
                }
                else
                {
                    if (date_end <= DateTime.Today.Date.AddDays(days_sertificate))
                    {
                        return 2;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }

        private void btExcelCredit_Click(object sender, EventArgs e)
        {
            RequestPrint.Print.Print.printCredit(this.idTReq, id_dep);
        }

        private void btExcel_Click(object sender, EventArgs e)
        {
            Logging.StartFirstLevel(592);
            Logging.Comment("Выгрузить заявку в Excel");
            Logging.Comment("Заявка " + reqSettings["req_num"].ToString()
                            + (Convert.ToInt32(reqSettings["id_oper"]) == 2 ? " на возврат " : "")
                            + " от " + ((DateTime)reqSettings["req_date"]).ToString("dd.MM.yyyy")
                            + " выведена в Excel сотрудником в режиме " + UserSettings.User.StatusCode
                            + ", ФИО: " + UserSettings.User.FullUsername);
            Logging.Comment("Окно: " + reqSettings["porthole"].ToString());
            Logging.Comment("Отдел: " + reqSettings["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
            Logging.Comment("Тип кредитования: " + reqSettings["name_credit"].ToString().Trim() + ", ID: " + reqSettings["credit_type"].ToString());
            Logging.Comment("Поставщик: " + reqSettings["post_name"].ToString().Trim() + ", ID: " + reqSettings["id_post"].ToString());
            Logging.Comment("Тип заявки: " + reqSettings["op_name"].ToString().Trim() + ", ID " + reqSettings["id_oper"].ToString());
            Logging.Comment("ЮЛ: " + reqSettings["ul"].ToString().Trim() + ", ID: " + reqSettings["ntypeorg"].ToString());
            Logging.Comment("Примечание: " + reqSettings["cprimech"].ToString().Trim());
            Logging.Comment("Менеджер: " + reqSettings["man_name"].ToString().Trim() + ", ID: " + reqSettings["id_man"].ToString());

            if ((int)reqSettings["id_oper"] == 3)
            {
                Logging.Comment("Тип бонуса: " + cbBonusType.Text.Trim() + ", ID: " + cbBonusType.SelectedValue.ToString());
                if ((int)cbBonusType.SelectedValue == 5)
                {
                    Logging.Comment("П_Н: " + tbPN.Text);
                    Logging.Comment("Т_Б: " + tbTB.Text);
                }
            }

            if (UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР")
            {
                if (Config.hCntMain.NedoUser())
                {
                    RequestPrint.Print.Print.printNedoUserExcel(this.idTReq, id_dep);
                }
                else
                {
                    RequestPrint.Print.Print.printKntExcel(this.idTReq, id_dep);
                }
            }
            else
            {
                RequestPrint.Print.Print.printOverExcel(this.idTReq, id_dep);
            }

            Logging.Comment("Завершение операции \"Выгрузить заявку в Excel\"");
            Logging.StopFirstLevel();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            Logging.StartFirstLevel(335);
            Logging.Comment("Печать заявки");
            Logging.Comment("Заявка " + reqSettings["req_num"].ToString()
                            + " от " + ((DateTime)reqSettings["req_date"]).ToString("dd.MM.yyyy")
                            + " выведена на печать сотрудником в режиме " + UserSettings.User.StatusCode
                            + ", ФИО: " + UserSettings.User.FullUsername);
            Logging.Comment("Окно: " + reqSettings["porthole"].ToString());
            Logging.Comment("Отдел: " + reqSettings["dep_name"].ToString().Trim() + ", ID: " + id_dep.ToString());
            Logging.Comment("Тип кредитования: " + reqSettings["name_credit"].ToString().Trim() + ", ID: " + reqSettings["credit_type"].ToString());
            Logging.Comment("Поставщик: " + reqSettings["post_name"].ToString().Trim() + ", ID: " + reqSettings["id_post"].ToString());
            Logging.Comment("Тип заявки: " + reqSettings["op_name"].ToString().Trim() + ", ID " + reqSettings["id_oper"].ToString());
            Logging.Comment("ЮЛ: " + reqSettings["ul"].ToString().Trim() + ", ID: " + reqSettings["ntypeorg"].ToString());
            Logging.Comment("Примечание: " + reqSettings["cprimech"].ToString().Trim());
            Logging.Comment("Менеджер: " + reqSettings["man_name"].ToString().Trim() + ", ID: " + reqSettings["id_man"].ToString());

            if ((int)reqSettings["id_oper"] == 3)
            {
                Logging.Comment("Тип бонуса: " + cbBonusType.Text.Trim() + ", ID: " + cbBonusType.SelectedValue.ToString());
                if ((int)cbBonusType.SelectedValue == 5)
                {
                    Logging.Comment("П_Н: " + tbPN.Text);
                    Logging.Comment("Т_Б: " + tbTB.Text);
                }
            }

            if (UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР")
            {
                
                if (Config.hCntMain.NedoUser())
                {
                    RequestPrint.Print.Print.printNedoUserCrystal(this.idTReq, id_dep);
                }
                else
                {
                    RequestPrint.Print.Print.printKntCrystal(this.idTReq, id_dep);
                }
            }
            else
            {
                RequestPrint.Print.Print.showReport(this.idTReq, id_dep);
            }

            Logging.Comment("Завершение операции \"Печать заявки\"");
            Logging.StopFirstLevel();
        }

        // Получение данных для основного грида
        private void bgwEditReq_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dateToRCena = DateTime.Now;
            try
            {
                DoOnUIThread(delegate()
                {
                    dateToRCena = dtpDateOut.Value;
                });
            }
            catch
            {
                dateToRCena = DateTime.Now;
            }

            if (restoreAutoSaved)
            {
                dtReqGoods = (id_dep != 6
                                ? Config.hCntMain.GetAutoRequestBody()
                                : Config.hCntAdd.GetAutoRequestBody());
            }
            else
            {
                dtReqGoods = (id_dep != 6
                                ? Config.hCntMain.GetRequestBody(idTReq, isCopy, dateToRCena)
                                : Config.hCntAdd.GetRequestBody(idTReq, isCopy, dateToRCena));
            }

            if (dtReqGoods != null)
            {

                if (!dtReqGoods.Columns.Contains("ost2Shop"))
                {
                    dtReqGoods.Columns.Add("ost2Shop", typeof(decimal));
                    dtReqGoods.AcceptChanges();
                }



                var query = from row in dtReqGoods.AsEnumerable()
                            group row by row.Field<string>("ean") into sales
                            orderby sales.Key
                            select new
                            {
                                Name = sales.Key,
                                CountOfClients = sales.Count()
                            };

                string strEanSend = "";
                foreach (var str in query)
                {
                    strEanSend += "," + str.Name;
                }

                if (strEanSend.Length > 0) strEanSend = strEanSend.Remove(0, 1);

                DataTable dtOstTo3Shop = Config.hCntShop2.getOstTo3Shop(strEanSend);



                foreach (DataRow r in dtReqGoods.Rows)
                {
                    if (isWeInOut)
                    {
                        DataRow[] row = dtOstTo3Shop.Select(String.Format("ean  = '{0}'", r["ean"].ToString()));
                        if (row.Count() > 0)
                        {
                            r["ost2Shop"] = row[0]["netto"];
                        }
                        else
                        {
                            r["ost2Shop"] = 0;
                        }

                        if (decimal.Parse(r["zakaz"].ToString()) > decimal.Parse(r["ost2Shop"].ToString()))
                        {
                            r["CauseOfDecline"] = "Ограничение по текущему остатку онлайн";
                        }
                    }
                }


                dtReqGoods.AcceptChanges();
                dtOldReqData = dtReqGoods.Copy();
            }
        }

        private void bgwEditReq_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {   
            //Если режим копирования, проверяем ограничения, выводим сообщения
            if(isCopy)
            {
                id_dep = (int)cbDep.SelectedValue;
                bool tuChecked = (id_dep == 6 ? Config.hCntAdd.CheckReqToCopy(id_dep, idTReq) : Config.hCntMain.CheckReqToCopy(id_dep, idTReq));

                if (tuChecked)
                {
                    MessageBox.Show("В заявку добавлены товары,\nна которые у пользователя\nесть права!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (dtReqGoods.Select("id_unit = 1").Count() > 0
                    && dtReqGoods.Select("id_unit = 2").Count() > 0)
                {
                    MessageBox.Show("В заявке присутствуют\nштучные и весовые товары!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (MessageBox.Show("Обновить содержимое заявки с учетом последних изменений", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable dtUpdatedPrices = (id_dep == 6 ? Config.hCntAdd.GetNewPrice(idTReq) : Config.hCntMain.GetNewPrice(idTReq));

                    foreach (DataRow drUpdPr in dtUpdatedPrices.Rows)
                    {
                        if (dtReqGoods.Select("id_tovar = " + drUpdPr["id_tovar"].ToString()).Count() > 0)
                        {
                            dtReqGoods.Select("id_tovar = " + drUpdPr["id_tovar"].ToString())[0]["rcena"] = drUpdPr["rcena"];
                        }
                    }
                    dtReqGoods.AcceptChanges();
                }

                if (curIdType != 2 && curIdType != 8)
                {
                    DataTable dtReqProhGoods = Config.hCntMain.GetProhToCopy(idTReq);
                    if (dtReqProhGoods != null && dtReqProhGoods.Rows.Count > 0)
                    {
                        frmProhMessage frmPM = new frmProhMessage(dtReqProhGoods);
                        frmPM.ShowDialog();

                        dtReqGoods.Select("id_tovar in (" + Config.GetStringFromRow(dtReqProhGoods, "id_tovar") + ")");
                        foreach (DataRow drGood in dtReqGoods.Select("id_tovar in (" + Config.GetStringFromRow(dtReqProhGoods, "id_tovar") + ")"))
                        {
                            dtReqGoods.Rows.Remove(drGood);
                        }

                        dtReqGoods.AcceptChanges();
                    }
                }
            }

            Config.ChangeFormEnabled(this, true);
            this.pbEditReq.Visible = false;
            SetGrdSrc();
        }

        private void getRequestToDgvComp()
        {
            //Если режим копирования, проверяем ограничения, выводим сообщения
            if(isCopy)
            {
                id_dep = (int)cbDep.SelectedValue;
                bool tuChecked = (id_dep == 6 ? Config.hCntAdd.CheckReqToCopy(id_dep, idTReq) : Config.hCntMain.CheckReqToCopy(id_dep, idTReq));

                if (tuChecked)
                {
                    MessageBox.Show("В заявку добавлены товары,\nна которые у пользователя\nесть права!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (dtReqGoods.Select("id_unit = 1").Count() > 0
                    && dtReqGoods.Select("id_unit = 2").Count() > 0)
                {
                    MessageBox.Show("В заявке присутствуют\nштучные и весовые товары!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (MessageBox.Show("Обновить содержимое заявки с учетом последних изменений", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable dtUpdatedPrices = (id_dep == 6 ? Config.hCntAdd.GetNewPrice(idTReq) : Config.hCntMain.GetNewPrice(idTReq));

                    foreach (DataRow drUpdPr in dtUpdatedPrices.Rows)
                    {
                        if (dtReqGoods.Select("id_tovar = " + drUpdPr["id_tovar"].ToString()).Count() > 0)
                        {
                            dtReqGoods.Select("id_tovar = " + drUpdPr["id_tovar"].ToString())[0]["rcena"] = drUpdPr["rcena"];
                        }
                    }
                    dtReqGoods.AcceptChanges();
                }

                if (curIdType != 2 && curIdType != 8)
                {
                    DataTable dtReqProhGoods = Config.hCntMain.GetProhToCopy(idTReq);
                    if (dtReqProhGoods != null && dtReqProhGoods.Rows.Count > 0)
                    {
                        frmProhMessage frmPM = new frmProhMessage(dtReqProhGoods);
                        frmPM.ShowDialog();

                        dtReqGoods.Select("id_tovar in (" + Config.GetStringFromRow(dtReqProhGoods, "id_tovar") + ")");
                        foreach (DataRow drGood in dtReqGoods.Select("id_tovar in (" + Config.GetStringFromRow(dtReqProhGoods, "id_tovar") + ")"))
                        {
                            dtReqGoods.Rows.Remove(drGood);
                        }

                        dtReqGoods.AcceptChanges();
                    }
                }
            }

            Config.ChangeFormEnabled(this, true);
            this.pbEditReq.Visible = false;
            SetGrdSrc();
        }

        /// <summary>
        /// Присвоение источника гриду
        /// </summary>
        private void SetGrdSrc()
        {
            bsReqGoods.DataSource = dtReqGoods;
            grdRequestGoods.AutoGenerateColumns = false;
            grdRequestGoods.DataSource = bsReqGoods;
            SetControlsEnabled();
        }

        private void frmEditRequest_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (idTRequests.Count > 0)
            {
                foreach (int ids in idTRequests)
                {
                    Config.openedRequests.Remove(ids);
                }
            }
            else
            {
                Config.openedRequests.Remove(idTReq);
            }

            if(((Main)this.MdiParent).isFormOpened("frmRequests"))
                ((Main)this.MdiParent).SetTab("", "frmRequests", null, null);
            
        }

        private void tbPost_MouseClick(object sender, MouseEventArgs e)
        {
            Posts.frmPosts frmPosts = new Posts.frmPosts((int)cbDep.SelectedValue);
            frmPosts.ShowDialog();
            //reqIdPost = frmPosts.PostId;
            //tbPost.Text = frmPosts.PostName;
            //GetUl(id_dep, dtpDateOut.Value.Date, false);
           

            SetPost(frmPosts.PostId, frmPosts.PostName);
        }

        private void dtpDateOut_Validated(object sender, EventArgs e)
        {
            //if (Mode == 2 && dtpDateOut.Value.Date < Config.curDate.Date)
            //{
            //    dtpDateOut.MinDate =
            //        dtpDateOut.Value = Config.curDate;
            //}
            GetUl(id_dep, dtpDateOut.Value.Date, false);
        }

        private void tbPN_Validated(object sender, EventArgs e)
        {
            tbTB.Text = (100 - Decimal.Parse(tbPN.Text.Replace(" ","0"))).ToString();
        }

        private void tbTB_Validated(object sender, EventArgs e)
        {
            tbPN.Text = (100 - Decimal.Parse(tbTB.Text.Replace(" ", "0"))).ToString();
        }
         
        private void tbPN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '.')
                e.KeyChar = ',';

            e.Handled = (!"0123456789,.\b".Contains(e.KeyChar)
                        || ((e.KeyChar == ',' && (tbPN.Text.Contains(',') || tbPN.Text.Length == 0 || tbPN.Text.Length > 2))
                        || (!",\b".Contains(e.KeyChar) && ((!tbPN.Text.Contains(',') && tbPN.Text.Length == 2))))
                            && tbPN.SelectionLength != tbPN.Text.Length);
        }

        private void tbPN_Validating(object sender, CancelEventArgs e)
        {
            decimal d;
            if (!decimal.TryParse(tbPN.Text, out d) && tbPN.Text.Length != 0)
            {
                e.Cancel = true;
            }
            else
            {
                if (d > 99.99m)
                    d = 99.99m;
                if (d < 0)
                    d = 0;

                tbPN.Text = d.ToString("#0.00");
            }
        }

        private void tbPN_Validated_1(object sender, EventArgs e)
        {
            decimal val =  Decimal.Parse(tbPN.Text);

            if (val == 99.99m)
            {
                tbTB.Text = "00.00";
            }
            else
            {
                if (val == 0)
                {
                    tbTB.Text = "99.99";
                }
                else
                {
                    tbTB.Text = (100 - Decimal.Parse(tbPN.Text)).ToString();
                }
            }
        }

        private void tbTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';

            e.Handled = (!"0123456789,.\b".Contains(e.KeyChar)
                        || ((e.KeyChar == ',' && (tbTB.Text.Contains(',') || tbTB.Text.Length == 0 || tbTB.Text.Length > 2))
                        || (!",\b".Contains(e.KeyChar) && ((!tbTB.Text.Contains(',') && tbTB.Text.Length == 2))))
                            && tbTB.SelectionLength != tbTB.Text.Length);
        }

        private void tbTB_Validated_1(object sender, EventArgs e)
        {
            decimal val = Decimal.Parse(tbTB.Text);

            if (val == 99.99m)
            {
                tbPN.Text = "00.00";
            }
            else
            {
                if (val == 0)
                {
                    tbPN.Text = "99.99";
                }
                else
                {
                    tbPN.Text = (100 - Decimal.Parse(tbTB.Text)).ToString();
                }
            }
        }

        private void tbTB_Validating(object sender, CancelEventArgs e)
        {
            decimal d;
            if (!decimal.TryParse(tbTB.Text, out d) && tbTB.Text.Length != 0)
            {
                e.Cancel = true;
            }
            else
            {
                if (d > 99.99m)
                    d = 99.99m;
                if (d < 0)
                    d = 0;

                tbTB.Text = d.ToString("#0.00");
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            CheckAndSave();
        }

        /// <summary>
        /// Проверка и сохранение заявки
        /// </summary>
        private void CheckAndSave()
        {
            viewDataWeInOut();

            if (isWeInOut)
            {

                


                var query = from row in dtReqGoods.AsEnumerable()
                            group row by row.Field<string>("ean") into sales
                            orderby sales.Key
                            select new
                            {
                                Name = sales.Key,
                                CountOfClients = sales.Count()
                            };


                bool isDoubleEan = false;
                string doubleEan = "";

                foreach (var rr in query)
                {
                    if (rr.CountOfClients > 1)
                    {
                        doubleEan += "," + rr.Name;
                        isDoubleEan = true;
                    }
                }



                if (isDoubleEan)
                {
                    doubleEan = doubleEan.Remove(0, 1);
                    MessageBox.Show("EAN '" + doubleEan + "' дублируется в заявке!", "Ввод EAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataRow r in dtReqGoods.Rows)
                {
                    object sumTovar = 0;
                    if (Config.dtSelectedTovar != null && Config.dtSelectedTovar.Rows.Count > 0)
                        sumTovar = Config.dtSelectedTovar.Compute("SUM(Netto)", "id_tovar = " + r["id_tovar"].ToString());

                    if (sumTovar == DBNull.Value) sumTovar = 0;

                    if (decimal.Parse(r["zakaz"].ToString()) != decimal.Parse(sumTovar.ToString()))
                    {
                        MessageBox.Show("Нельзя сохранить данную заявку, пока не набрано\nколичество товаров на форме \"Выбор товара для\nзаявки\", равное количеству товаров в заявке.", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                bool isAlarmWeInOut = false;
             
                foreach (DataRow r in dtReqGoods.Rows)
                {
                    if (!Config.checkStatusTovarInBases(r["ean"].ToString(), id_dep))
                    {                        
                        MessageBox.Show("Введённый EAN товара отсутствует\nв базе данных!", "Ввод EAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //return;
                        isAlarmWeInOut = true;
                        break;
                    }
                }

                if (isAlarmWeInOut)
                {
                    int countFailEan = 0;
                    foreach (DataRow r in dtReqGoods.Rows)
                    {
                        if (!Config.checkStatusTovarInBases(r["ean"].ToString(), id_dep))
                            countFailEan++;
                    }

                    if (DialogResult.No == MessageBox.Show("Удалить из заказа товары\nотсутствующие в БД?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                        return;

                    if (dtReqGoods.Rows.Count == countFailEan)
                    {
                        MessageBox.Show("Все указанные в заявке\nтовары отсутсвуют в БД, заявка не будет\nсохранена!"
                            , "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

            }
            
            int nstatus = 0;
            int rCheckResult = 0;

            #region проверка на ввод данных
            if (reqIdPost == 0 || tbPost.Text.Trim().Length == 0)
            {
                MessageBox.Show("Выберите поставщика, на которого составляется заявка!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbType.SelectedValue == null || int.Parse(cbType.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Выберите тип заявки!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;  
            }

            if ((int)cbType.SelectedValue == 3 &&
                (int)cbBonusType.SelectedValue == 0)
            {
                MessageBox.Show("Выберите тип бонуса\nдля сохраняемой заявки!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;  
            }

            if (cbBonusType.SelectedValue != null
                && (int)cbBonusType.SelectedValue == 5
                && ((tbPN.Text.Trim().Length == 0 || tbTB.Text.Trim().Length == 0)
                || (decimal.Parse(tbPN.Text.Trim()) == 0 && decimal.Parse(tbTB.Text.Trim()) == 0))) 
            {
                MessageBox.Show("Не введен процент\nнедостачи или товарного\nбонуса!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (cbUL.SelectedValue == null || int.Parse(cbUL.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Для сохранения необходимо выбрать ЮЛ!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbWindow.SelectedValue == null || cbWindow.SelectedIndex == -1)
            {
                MessageBox.Show("Для сохранения необходимо выбрать окно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #endregion

            if (dtReqGoods.Select("procent < 0").Count() > 0)
            {
                if (MessageBox.Show("В заявке присутствуют товары\nс отрицательным %\nПродолжить сохранение?", "Сохранение заявки.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

            if (dtReqGoods.Select().Sum(x => x.Field<decimal>("sum")) == 0)
            {
                MessageBox.Show("Заявка не может быть сохранена, т.к. суммарный заказ равен нулю!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
                         
             DataRow[] draProhGoods = (id_dep != 6 ? Config.hCntMain.GetProhToCopy(idTReq)
                                                  : Config.hCntAdd.GetProhToCopy(idTReq)).Select("id_tovar in (" + Config.GetStringFromRow(dtReqGoods, "id_tovar") + ")");

            if (draProhGoods != null && draProhGoods.Count() > 0)
            {
                string pGoodsStr = "";

                for (int i = 0; i < draProhGoods.Count(); i++)
                {
                    pGoodsStr += (i + 1).ToString() + ". " + draProhGoods[i]["ean"].ToString().Trim().PadLeft(13) + " " + draProhGoods[i]["cName"].ToString().Trim() + "\n";
                }

                if (MessageBox.Show("В заявке присутствуют товары\nнаходящиеся в справочнике\nзапрещенных к добавлению в заявки\nтоваров:\n" + pGoodsStr + "Вы хотите сохранить заявку?", "Сохранение заявки.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            if (autoSave)
            {
                autoSaveTimer.Stop();
            }

            //Для МН - передача заявки
            if (UserSettings.User.StatusCode == "МН" /*|| UserSettings.User.StatusCode == "ДМН"*/)
                if (isCopy || Mode == 1 || (Mode == 2 && new int[2] { 0, 3 }.Contains(int.Parse(reqSettings["nstatus"].ToString()))))
                {
                    frmSendRequest frmSend = new frmSendRequest();
                    frmSend.ShowDialog();
                    DialogResult dResult = frmSend.DialogResult;

                    if (dResult != DialogResult.Cancel)
                    {
                        nstatus = (dResult == DialogResult.No ? 0 : 2);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    nstatus = int.Parse(reqSettings["nstatus"].ToString());
                }

            foreach (DataRow drReqGoods in dtReqGoods.Rows)
            {
                drReqGoods["CauseOfDecline"] = "";
            }

            if ((int)cbType.SelectedValue == 1
                && (Config.hCntMain.GetSettings("ldep").Select("value = '" + id_dep + "'").Count() != 0
                && Config.hCntMain.GetSettings("Bpst").Select("value = '" + reqIdPost.ToString() + "'").Count() == 0)
                && ((UserSettings.User.StatusCode != "МН" /*&& UserSettings.User.StatusCode != "ДМН"*/) || nstatus != 0))
            {
                string idToDelete;
                rCheckResult = Config.checkRestriction(ref dtReqGoods, id_dep, idTReq, dtpDateOut.Value.Date, false, out idToDelete);
                //checkRestriction();

                if (rCheckResult == 2)
                {
                    if (MessageBox.Show("В заявке есть товары с количеством заказа\nпревышающим разрешенное количество!\nУдалить эти товары из заявки?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataRow drDel in dtReqGoods.Select("idReq IN (" + idToDelete + ") AND LEN(TRIM(CauseOfDecline)) <> 0"))
                        {
                            if ((int)drDel["idReq"] != 0 && !isCopy)
                            {
                                deletedIdReq.Add((int)drDel["idReq"]);
                            }

                            dtReqGoods.Rows.Remove(drDel);
                        }

                        if (dtReqGoods.Rows.Count == 0)
                        {
                            MessageBox.Show("Заявка не может быть сохранена, т.к. суммарный заказ равен нулю!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                grdRequestGoods.Refresh();
            }

            if (grdRequestGoods.Rows.Count == 0)
            {
                curRow = null;
                SetGoodParameters();
                SetControlsEnabled();
            }

            if(rCheckResult != 2)
                SaveRequest(nstatus, rCheckResult);           
        }

        /// <summary>
        /// Сохранение заявки на сервер
        /// </summary>
        /// <param name="nstatus">статус заявки</param>
        /// <param name="checkResult">результат проверки ограничений</param>
        private void SaveRequest(int nstatus, int checkResult)
        {           
            int creditPeriod = 0;
            int maxIdUnit = 0;
            int minIdUnit = 0;
            int idUnit;
            ArrayList goodSubjNotCheck = new ArrayList();

            Config.curDate = Config.hCntMain.GetCurDate(false);
            DataTable dtCountry = Config.hCntMain.GetSubjectList();
            DataTable dtUnit = Config.hCntMain.GetUnitList();
            DataTable dtNds = Config.hCntMain.GetNdsList();
            DataTable dtTu = id_dep != 6 ? Config.hCntMain.GetTU(id_dep) : Config.hCntAdd.GetTU(id_dep);
            DataTable dtInv = id_dep != 6 ? Config.hCntMain.GetInvGrp(id_dep) : Config.hCntAdd.GetInvGrp(id_dep);
            DataTable dtStatuses = Config.hCntMain.GetReqStatuses();
            
            DateTime? BeginOfPeriod;
            DateTime? EndOfPeriod;
            DateTime periodDateTime;
            
            decimal? PlanRealiz;
            decimal outPlan;

            bool logAsNewGood = false; //Логировать товар как при добавлении
            bool valChanges = false;
            if(Mode == 2 && !isCopy)
                valChanges = isValuesChanged(true);

            int idCreditType = (new int [2] { 1, 2 }.Contains((int)cbCreditType.SelectedValue) ? 5 : 1);

            //Определяем nstatus
            if(checkResult == 1)
                nstatus = 6;
            else
            {
                if (UserSettings.User.StatusCode == "КД"
                    || UserSettings.User.StatusCode == "РКВ"
                    || UserSettings.User.StatusCode == "ДМН")
                {
                    if (MessageBox.Show("Вы хотите подтвердить сохраненную заявку?", "Сообщение.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        nstatus = 2;
                    }
                    else
                    {
                        if (UserSettings.User.StatusCode == "КД")
                        {
                            if (new int[2] { 3, 4 }.Contains((int)cbCreditType.SelectedValue))
                            {
                                nstatus = 8;
                            }
                            else
                            {
                                nstatus = 1;
                            }
                        }

                        if (UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "ДМН")
                        {
                            if (dtReqGoods.Select("nprimech = 1").Count() > 0 ||
                                new int[2] { 3, 4 }.Contains((int)cbCreditType.SelectedValue) ||
                                dtReqGoods.Select("BeginOfPeriod <> ''").Count() > 0 ||
                                dtReqGoods.Select("EndOfPeriod <> ''").Count() > 0 ||
                                dtReqGoods.Select("PlanRealiz <> -1").Count() > 0 ||
                                (id_dep != 6 ? Config.hCntMain.IsContainsSaleGoods(Config.GetStringFromRow(dtReqGoods, "id_tovar"), dtpDateOut.Value.Date)
                                             : Config.hCntAdd.IsContainsSaleGoods(Config.GetStringFromRow(dtReqGoods, "id_tovar"), dtpDateOut.Value.Date)))
                            {
                                nstatus = 7;
                            }
                            else
                            {
                                nstatus = 1;
                            }
                        }
                    }

                }


                if ((UserSettings.User.StatusCode == "МН")
                    && nstatus == 6)
                {
                    nstatus = 2;
                }   
            }

            if (Config.hCntMain.GetSettings("div").Select("value = '" + id_dep + "'").Count() > 0
                && nstatus != 0
                && nstatus != 6
                && (int)cbType.SelectedValue != 2
                && (int)cbType.SelectedValue != 3)
            {
                maxIdUnit = 2;
                minIdUnit = 1;
            }

            DataRow[] goodsToAdd;

            if (isCopy)
            {
                int srcStat = int.Parse(reqSettings["nstatus"].ToString());

                Logging.StartFirstLevel(593);
                Logging.Comment("Копирование заявки");
                Logging.Comment("Данные копируемой заявки");
                Logging.Comment("Номер заявки: " + reqSettings["req_num"].ToString());
                Logging.Comment("Дата заявки: " + ((DateTime)reqSettings["req_date"]).ToString("dd.MM.yyyy"));
                Logging.Comment("Статус заявки: " + (dtStatuses.Select("id = " + srcStat.ToString())[0]["cName"].ToString()));
                Logging.Comment("Отдел: " + reqSettings["dep_name"].ToString() + ", ID: " + reqSettings["id_dep"].ToString());
                Logging.Comment("Дата копирования: " + Config.curDate.ToString("dd.MM.yyyy"));
                Logging.Comment("ФИО менеджера: " + UserSettings.User.FullUsername);
                //Logging.Comment("Завершение операции \"Копирование заявки\"");
            }

            //Сохранение на сервер
            for (idUnit = minIdUnit; idUnit <= maxIdUnit; idUnit++)
            {
                for (int npp = 0; npp < dtReqGoods.Rows.Count; npp++ )
                {
                    DataRow[] drExistsGoods = dtOldReqData.Select("id_tovar = " + dtReqGoods.Rows[npp]["id_tovar"].ToString() + " AND npp = " + dtReqGoods.Rows[npp]["npp"].ToString() + " AND npp <> 0");
                    if (drExistsGoods != null && drExistsGoods.Count() > 0)
                    {
                        drExistsGoods[0]["npp"] = decimal.Parse((npp + 1).ToString());
                    }
                    dtReqGoods.Rows[npp]["npp"] = decimal.Parse((npp + 1).ToString());
                }
                //Если указано в настройках - разбиваем накладную на вес/шт
                goodsToAdd = (idUnit == 0 ? dtReqGoods.Select("zakaz > 0", "npp") : dtReqGoods.Select("id_unit = " + idUnit.ToString() + " AND zakaz > 0", "npp"));

                if (goodsToAdd.Count() != 0)
                {
                    decimal Deficit = 0;
                    decimal Goods = 0;

                    if (cbBonusType.SelectedValue != null && (int)cbBonusType.SelectedValue == 5)
                    {
                        Decimal.TryParse(tbPN.Text, out Deficit);
                        Decimal.TryParse(tbTB.Text, out Goods);
                    }

                    if (id_dep != 6)
                    {
                        idTReq = Config.hCntMain.SetRequestHead((isCopy ? 0 : idTReq), id_dep, dtpDateOut.Value.Date, reqIdPost, (Mode == 1 || isCopy ? UserSettings.User.Id :(int)reqSettings["id_man"]), 
                                                        nstatus, (int)cbUL.SelectedValue, tbPrimech.Text, (int)cbType.SelectedValue,
                                                        (int)cbWindow.SelectedValue, (cbBonusType.Visible ? (int)cbBonusType.SelectedValue : 1), idUnit,
                                                        (int)cbCreditType.SelectedValue, (int.TryParse(tbTerm.Text, out creditPeriod) ? creditPeriod : 0),
                                                        Deficit, Goods, Config.GetStringFromArray(deletedIdReq));

                    }
                    else
                    {
                        idTReq = Config.hCntAdd.SetRequestHead((isCopy ? 0 : idTReq), id_dep, dtpDateOut.Value.Date, reqIdPost, (Mode == 1 || isCopy ? UserSettings.User.Id : (int)reqSettings["id_man"]),
                                                        nstatus, (int)cbUL.SelectedValue, tbPrimech.Text, (int)cbType.SelectedValue,
                                                        (int)cbWindow.SelectedValue, (cbBonusType.Visible ? (int)cbBonusType.SelectedValue : 1), idUnit,
                                                        (int)cbCreditType.SelectedValue, (int.TryParse(tbTerm.Text, out creditPeriod) ? creditPeriod : 0),
                                                        Deficit, Goods, Config.GetStringFromArray(deletedIdReq));

                    }

                    

                    if (Mode == 1)
                    {
                        if (isCopy)
                        {
                            //Logging.StartFirstLevel(593);
                            Logging.Comment("Данные сохраняемой заявки");
                        }
                        else
                        {
                            Logging.StartFirstLevel(97);
                            Logging.Comment("Создание заявки");
                        }
                        Logging.Comment("Номер: " + idTReq.ToString() + ", Дата выдачи:" + dtpDateOut.Value.ToString("dd.MM.yyyy") + ", Окно: " + cbWindow.Text.Trim());
                        Logging.Comment("Отдел: " + cbDep.Text + ", ID: " + cbDep.SelectedValue.ToString());
                        Logging.Comment("Тип кредитования: " + cbCreditType.Text + ", ID: " + cbCreditType.SelectedValue.ToString());
                        Logging.Comment("Поставщик: " + tbPost.Text.Trim() + ", ID: " + reqIdPost.ToString());
                        Logging.Comment("Тип заявки: " + cbType.Text.Trim() + ", ID " + cbType.SelectedValue.ToString());
                        Logging.Comment("ЮЛ: " + cbUL.Text + ", ID: " + cbUL.SelectedValue.ToString());
                        Logging.Comment("Примечание: " + tbPrimech.Text);
                        Logging.Comment("Менеджер: " + tbMan.Text + ", ID: " + UserSettings.User.Id);
                        Logging.Comment("Статус заявки: " + (nstatus == 1 ? "Подтверждена" : dtStatuses.Select("id = " + nstatus.ToString())[0]["cName"].ToString()));

                        if (UserSettings.User.StatusCode == "МН" && nstatus != 0)
                        {
                            Logging.Comment("Дата передачи РКВ: " + Config.curDate.ToString("dd.MM.yyyy"));
                        }

                        if ((int)cbType.SelectedValue == 3)
                        {
                            Logging.Comment("Тип бонуса: " + cbBonusType.Text + ", ID: " + cbBonusType.SelectedValue.ToString());
                            if ((int)cbBonusType.SelectedValue == 5)
                            {
                                Logging.Comment("П_Н: " + tbPN.Text);
                                Logging.Comment("Т_Б: " + tbTB.Text);
                            }
                        }
                    }

                    if (Mode == 2 && !isCopy && (valChanges || nstatus != int.Parse(dtOldReqSettings["nstatus"].ToString())))
                    {
                        Logging.StartFirstLevel(98);
                        Logging.Comment("Редактирование заявки");
                        Logging.Comment("Номер: " + idTReq.ToString() + ", Отдел: " + cbDep.Text + ", ID отдела: " + cbDep.SelectedValue.ToString() + ", Дата выдачи: " + dtpDateOut.Value.ToString("dd.MM.yyyy"));
                        Logging.VariableChange("Дата выдачи", dtpDateOut.Value.ToString("dd.MM.yyyy"), ((DateTime)dtOldReqSettings["req_date"]).ToString("dd.MM.yyyy"));
                        Logging.VariableChange("Окно", cbWindow.Text.Trim(), ((DataTable)cbWindow.DataSource).Select("porthole = " + dtOldReqSettings["porthole"].ToString())[0]["cname"].ToString().Trim());
                        Logging.VariableChange("Тип кредитования", cbCreditType.Text.Trim(), ((DataTable)cbCreditType.DataSource).Select("id = " + dtOldReqSettings["credit_type"].ToString())[0]["cname"].ToString().Trim());
                        Logging.VariableChange("ID кредитования", cbCreditType.SelectedValue.ToString().Trim(), dtOldReqSettings["credit_type"].ToString().Trim());
                        Logging.VariableChange("Поставщик", tbPost.Text.Trim(), dtOldReqSettings["post_name"].ToString().Trim());
                        Logging.VariableChange("ID поставщика", reqIdPost.ToString().Trim(), dtOldReqSettings["id_post"].ToString().Trim());
                        Logging.VariableChange("Тип заявки", cbType.Text.Trim(), ((DataTable)cbType.DataSource).Select("id = " + dtOldReqSettings["id_oper"].ToString())[0]["sname"].ToString().Trim()); /*cbType.Text.Trim(), dtOldReqSettings["op_name"].ToString().Trim());*/
                        Logging.VariableChange("ID типа заявки", cbType.SelectedValue.ToString(), dtOldReqSettings["id_oper"].ToString());
                        Logging.VariableChange("ЮЛ", cbUL.Text.Trim(), dtOldReqSettings["ul"].ToString().Trim());
                        Logging.VariableChange("ntypeorg", cbUL.SelectedValue.ToString(), dtOldReqSettings["ntypeorg"].ToString());
                        Logging.VariableChange("Примечание", tbPrimech.Text.Trim(), dtOldReqSettings["cprimech"].ToString().Trim());

                        if ((int)cbType.SelectedValue == 3)
                        {
                            if ((int)dtOldReqSettings["id_oper"] != 3)
                            {
                                Logging.Comment("Тип бонуса: " + cbBonusType.Text + ", ID: " + cbBonusType.SelectedValue.ToString());
                                if ((int)cbBonusType.SelectedValue == 5)
                                {
                                    Logging.Comment("П_Н: " + tbPN.Text);
                                    Logging.Comment("Т_Б: " + tbTB.Text);
                                }
                            }
                            else
                            {
                                Logging.VariableChange("Тип бонуса", cbBonusType.Text.Trim(), ((DataTable)cbBonusType.DataSource).Select("id = " + dtOldReqSettings["id_TypeBonus"].ToString())[0]["cName"].ToString().Trim());
                                Logging.VariableChange("ID", cbBonusType.SelectedValue.ToString(), dtOldReqSettings["id_TypeBonus"].ToString());
                                if ((int)cbBonusType.SelectedValue == 5)
                                {
                                    if ((int)dtOldReqSettings["id_TypeBonus"] != 5)
                                    {
                                        Logging.Comment("П_Н: " + tbPN.Text);
                                        Logging.Comment("Т_Б: " + tbTB.Text);
                                    }
                                    else
                                    {
                                        Logging.VariableChange("П_Н", tbPN.Text.Trim(), oldPN.ToString().Trim());
                                        Logging.VariableChange("Т_Б", tbTB.Text.Trim(), oldTB.ToString().Trim());
                                    }
                                }
                            }
                        }

                        Logging.VariableChange("Статус заявки: ", (dtStatuses.Select("id = " + (nstatus == 1 ? "12" : nstatus.ToString()))[0]["cName"].ToString()),
                                                                (dtStatuses.Select("id = " + dtOldReqSettings["nstatus"].ToString())[0]["cName"].ToString()));
                    }

                    

                    //if (isCopy && idTRequests.Count == 0)
                    //{
                    //    idTRequests.Add(idTReq);
                    //}

                    foreach (DataRow drGood in goodsToAdd)
                    {

                        if ((isWeInOut && Config.checkStatusTovarInBases(drGood["ean"].ToString(), id_dep)) || !isWeInOut)
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
                            if (//tbPlanRealiz.Visible &&
                                decimal.TryParse(drGood["PlanRealiz"].ToString(), out outPlan))
                            {
                                if (outPlan != -1)
                                    PlanRealiz = outPlan;
                            }

                            if ((int)drGood["id_tovar"] == 0)
                            {
                                DataTable newGood = (id_dep != 6 ? Config.hCntMain.AddNewGood((string)drGood["cname"], (string)drGood["ean"], (int)drGood["id_grp1"], (int)drGood["id_grp2"], (int)cbUL.SelectedValue, (decimal)drGood["brutto"])
                                                                 : Config.hCntAdd.AddNewGood((string)drGood["cname"], (string)drGood["ean"], (int)drGood["id_grp1"], (int)drGood["id_grp2"], (int)cbUL.SelectedValue, (decimal)drGood["brutto"]));
                                drGood["id_tovar"] = newGood.Rows[0]["id"];
                            }

                            if (Convert.ToInt32(drGood["id_tovar"]) != 0 && Convert.ToInt32(drGood["nprimech"]) == 1)
                            {
                                if (id_dep != 6)
                                    Config.hCntMain.UpdateNewGoodNtypeorg(Convert.ToInt32(drGood["id_tovar"]), Convert.ToInt32(cbUL.SelectedValue));
                                else
                                    Config.hCntAdd.UpdateNewGoodNtypeorg(Convert.ToInt32(drGood["id_tovar"]), Convert.ToInt32(cbUL.SelectedValue));
                            }

                            if (id_dep != 6)
                            {
                                Config.hCntMain.SetRequestBody((isCopy ? 0 : (int)drGood["idReq"]), idTReq, (int)drGood["id_tovar"], (int)drGood["id_unit"], decimal.Parse(drGood["Tara"].ToString()), (decimal)drGood["zakaz"],
                                                                (decimal)drGood["zcena"], (decimal)drGood["rcena"], (decimal)drGood["zcenabnds"], (int)drGood["nprimech"],
                                                                (string)drGood["cprimech"].ToString().Replace("НТ ", "").Replace("НЦ ", ""), (int)drGood["id_subject"], BeginOfPeriod, EndOfPeriod, (string)drGood["CauseOfDecline"],
                                                                (string)drGood["PeriodOfStorage"], PlanRealiz, "", idCreditType, (string)drGood["ean"], (decimal)drGood["ShelfSpace"], (bool)drGood["isTransparent"]);
                            }
                            else
                            {
                                Config.hCntAdd.SetRequestBody((isCopy ? 0 : (int)drGood["idReq"]), idTReq, (int)drGood["id_tovar"], (int)drGood["id_unit"], decimal.Parse(drGood["Tara"].ToString()), (decimal)drGood["zakaz"],
                                                                (decimal)drGood["zcena"], (decimal)drGood["rcena"], (decimal)drGood["zcenabnds"], (int)drGood["nprimech"],
                                                                (string)drGood["cprimech"].ToString().Replace("НТ ", "").Replace("НЦ ", ""), (int)drGood["id_subject"], BeginOfPeriod, EndOfPeriod, (string)drGood["CauseOfDecline"],
                                                                (string)drGood["PeriodOfStorage"], PlanRealiz, "", idCreditType, (string)drGood["ean"], (decimal)drGood["ShelfSpace"], (bool)drGood["isTransparent"]);
                            }

                            //Сохранение страны/субъекта
                            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                                 && (int)drGood["nprimech"] != 1)
                            {
                                if ((int)drGood["id_subject"] != (int)drGood["spr_subject"] && !goodSubjNotCheck.Contains((int)drGood["id_tovar"]))
                                {
                                    if (id_dep != 6)
                                        Config.hCntMain.ChangeSubject((int)drGood["id_subject"], (int)drGood["id_tovar"]);
                                    else
                                        Config.hCntAdd.ChangeSubject((int)drGood["id_subject"], (int)drGood["id_tovar"]);

                                    goodSubjNotCheck.Add((int)drGood["id_tovar"]);
                                }
                            }


                            if (Mode == 2 && !isCopy && valChanges)
                            {
                                DataRow[] drExistGood = dtOldReqData.Select("id_tovar = " + drGood["id_tovar"].ToString() + " AND npp = " + drGood["npp"].ToString() + " AND npp <> 0");
                                if (drExistGood != null && drExistGood.Count() != 0)
                                {
                                    drGood.SetField<int>("limitType", 0);
                                    if (!drExistGood.First().ItemArray.SequenceEqual(drGood.ItemArray))
                                    {
                                        Logging.Comment("Отредактированный товар");
                                        Logging.Comment("ID товара: " + drGood["id_tovar"].ToString() + ", EAN: " + drGood["ean"].ToString().Trim() + ", Товар: " + drGood["cname"].ToString().Trim());
                                        Logging.Comment("Новый товар: " + ((int)drGood["nprimech"] == 1 ? "Да" : "Нет"));
                                        Logging.VariableChange("Наименование товара", drGood["cname"].ToString(), drExistGood[0]["cname"].ToString());
                                        Logging.VariableChange("Страна/субъект", dtCountry.Select("id = " + drGood["id_subject"].ToString())[0]["cname"].ToString().Trim(), dtCountry.Select("id = " + drExistGood[0]["id_subject"].ToString())[0]["cname"].ToString().Trim());
                                        Logging.VariableChange("ID страны/субъекта", drGood["id_subject"].ToString(), drExistGood[0]["id_subject"].ToString());
                                        Logging.VariableChange("Ед. изм.", dtUnit.Select("id = " + drGood["id_unit"].ToString())[0]["cunit"].ToString().Trim(), dtUnit.Select("id = " + drExistGood[0]["id_unit"].ToString())[0]["cunit"].ToString().Trim());
                                        Logging.VariableChange("ID Ед. изм.", drGood["id_unit"].ToString(), drExistGood[0]["id_unit"].ToString());
                                        Logging.VariableChange("НДС", dtNds.Select("id = " + drGood["id_nds"].ToString())[0]["nds"].ToString().Trim(), dtNds.Select("id = " + drExistGood[0]["id_nds"].ToString())[0]["nds"].ToString().Trim());
                                        Logging.VariableChange("ID НДС", drGood["id_nds"].ToString(), drExistGood[0]["id_nds"].ToString());
                                        Logging.VariableChange("Т/У группа", dtTu.Select("id = " + drGood["id_grp1"].ToString())[0]["cname"].ToString().Trim(), dtTu.Select("id = " + drExistGood[0]["id_grp1"].ToString())[0]["cname"].ToString().Trim());
                                        Logging.VariableChange("ID Т/У группы", drGood["id_grp1"].ToString(), drExistGood[0]["id_grp1"].ToString());
                                        Logging.VariableChange("Инв. группа", dtInv.Select("id = " + drGood["id_grp2"].ToString())[0]["cname"].ToString().Trim(), dtInv.Select("id = " + drExistGood[0]["id_grp2"].ToString())[0]["cname"].ToString().Trim());
                                        Logging.VariableChange("ID Инв. группы", drGood["id_grp2"].ToString(), drExistGood[0]["id_grp2"].ToString());
                                        Logging.VariableChange("Затарка", drGood["Zatar"].ToString(), ((int)drGood["Tara"] == 0 ? 0 : decimal.Round((decimal)drExistGood[0]["zakaz"] / (int)drGood["Tara"], 0)));
                                        Logging.VariableChange("Тара", drGood["Tara"].ToString(), drExistGood[0]["Tara"].ToString());
                                        Logging.VariableChange("Заказ", drGood["zakaz"].ToString(), drExistGood[0]["zakaz"].ToString());
                                        Logging.VariableChange("Цена зак. с ндс", drGood["zcena"].ToString(), drExistGood[0]["zcena"].ToString());
                                        Logging.VariableChange("Цена зак. без ндс", drGood["zcenabnds"].ToString(), drExistGood[0]["zcenabnds"].ToString());
                                        Logging.VariableChange("Цена продажи", drGood["rcena"].ToString(), drExistGood[0]["rcena"].ToString());
                                        Logging.VariableChange("Срок хранения", drGood["PeriodOfStorage"].ToString().Trim(), drExistGood[0]["PeriodOfStorage"].ToString().Trim());
                                        Logging.VariableChange("Полочное пр-во", drGood["ShelfSpace"].ToString(), drExistGood[0]["ShelfSpace"].ToString());
                                        Logging.VariableChange("Примечание", drGood["cprimech"].ToString().Trim().Replace("НТ ", "").Replace("НЦ ", ""), drExistGood[0]["cprimech"].ToString().Trim().Replace("НТ ", "").Replace("НЦ ", ""));
                                        Logging.VariableChange("Ограничение", drGood["CauseOfDecline"].ToString().Trim(), drExistGood[0]["CauseOfDecline"].ToString().Trim());
                                        Logging.VariableChange("% наценки", ((decimal)drGood["procent"]).ToString("#0.00"), ((decimal)drExistGood[0]["procent"]).ToString("#0.00"));

                                        decimal br = 0;
                                        decimal.TryParse(drGood["brutto"].ToString(), out br);
                                        decimal oldBr = 0;
                                        decimal.TryParse(drExistGood[0]["brutto"].ToString(), out oldBr);

                                        Logging.VariableChange("Брутто", br.ToString("#0.000"), oldBr.ToString("#0.000"));

                                        if ((int)drGood["nprimech"] == 1)
                                        {
                                            DataTable newGood = (id_dep != 6 ? Config.hCntMain.EditNewGood((int)drGood["id_tovar"], (decimal)drGood["brutto"])
                                                                     : Config.hCntAdd.EditNewGood((int)drGood["id_tovar"], (decimal)drGood["brutto"]));
                                        }
                                    }
                                }
                                else
                                {
                                    Logging.Comment("Добавленный в заявку товар");
                                    logAsNewGood = true;
                                }
                            }

                            if (Mode == 1 || logAsNewGood)
                            {
                                Logging.Comment("ID товара: " + drGood["id_tovar"].ToString() + ", EAN: " + drGood["ean"].ToString().Trim() + ", Товар: " + drGood["cname"].ToString().Trim());
                                Logging.Comment("Новый товар: " + ((int)drGood["nprimech"] == 1 ? "Да" : "Нет"));
                                Logging.Comment("Страна/субъект: " + dtCountry.Select("id = " + drGood["id_subject"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + drGood["id_subject"].ToString());
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
                                decimal br = 0;
                                decimal.TryParse(drGood["brutto"].ToString(), out br);
                                Logging.Comment("Брутто: " + br.ToString("#0.000"));
                                logAsNewGood = false;
                            }

                        }
                    }

                    foreach (int id_request in deletedIdReq)
                    {
                        if (id_request != 0)
                        {
                            DataRow[] drDelGoodInfo = dtOldReqData.Select("idReq = " + id_request.ToString());
                            if (drDelGoodInfo != null && drDelGoodInfo.Count() != 0)
                            {
                                Logging.Comment("Удаленый из заявки товар");
                                Logging.Comment("ID товара: " + drDelGoodInfo[0]["id_tovar"].ToString() + ", EAN: " + drDelGoodInfo[0]["ean"].ToString().Trim() + ", Товар: " + drDelGoodInfo[0]["cname"].ToString().Trim());
                                Logging.Comment("Новый товар: " + ((int)drDelGoodInfo[0]["nprimech"] == 1 ? "Да" : "Нет"));
                                Logging.Comment("Страна/субъект: " + dtCountry.Select("id = " + drDelGoodInfo[0]["id_subject"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + drDelGoodInfo[0]["id_subject"].ToString());
                                Logging.Comment("Ед. изм.: " + dtUnit.Select("id = " + drDelGoodInfo[0]["id_unit"].ToString())[0]["cunit"].ToString().Trim() + ", ID: " + drDelGoodInfo[0]["id_unit"].ToString());
                                Logging.Comment("НДС: " + dtNds.Select("id = " + drDelGoodInfo[0]["id_nds"].ToString())[0]["nds"].ToString().Trim() + ", ID: " + drDelGoodInfo[0]["id_nds"].ToString());
                                Logging.Comment("Т/У группа: " + dtTu.Select("id = " + drDelGoodInfo[0]["id_grp1"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + drDelGoodInfo[0]["id_grp1"].ToString());
                                Logging.Comment("Инв. группа: " + dtInv.Select("id = " + drDelGoodInfo[0]["id_grp2"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + drDelGoodInfo[0]["id_grp2"].ToString());
                                Logging.Comment("Затарка: " + drDelGoodInfo[0]["Zatar"].ToString() + ", Тара: " + drDelGoodInfo[0]["Tara"].ToString());
                                Logging.Comment("Заказ: " + drDelGoodInfo[0]["zakaz"].ToString());
                                Logging.Comment("Цена зак.: с ндс: " + drDelGoodInfo[0]["zcena"].ToString() + ", без ндс: " + drDelGoodInfo[0]["zcenabnds"].ToString());
                                Logging.Comment("Цена продажи: " + drDelGoodInfo[0]["rcena"].ToString() + ", Срок хранения: " + drDelGoodInfo[0]["PeriodOfStorage"].ToString());
                                Logging.Comment("Полочное пр-во: " + drDelGoodInfo[0]["ShelfSpace"].ToString() + ", Примечание: " + drDelGoodInfo[0]["cprimech"].ToString());
                                Logging.Comment("Ограничение: " + drDelGoodInfo[0]["CauseOfDecline"].ToString());
                                Logging.Comment("% наценки: " + ((decimal)drDelGoodInfo[0]["procent"]).ToString("#0.00"));
                                decimal br = 0;
                                decimal.TryParse(drDelGoodInfo[0]["brutto"].ToString(), out br);
                                Logging.Comment("Брутто: " + br.ToString("#0.000"));
                            }
                        }
                    }

                    idTRequests.Add(idTReq);

                    //Сохранение товаров к товару
                    //idTReq
                    if (isWeInOut)
                    {
                        Config.saveTovarSelected(idTReq);
                    }

                    //Разложение нашей заяки на приход отгрузку
                    if (nstatus == 1)
                    {
                        String status = UserSettings.User.StatusCode;
                        reqIdOper = (int)cbType.SelectedValue;
                        if (reqIdOper == 1)
                        {

                            if (status == "КД" || status == "ДМН" || status == "РКВ")
                            {
                                if (isWeInOut)
                                {
                                    createSubRequest();
                                }
                            }
                        }
                    }

                    idTReq = 0;

                    if (Mode == 1)
                    {
                        Logging.Comment("Завершение операции " + (isCopy ? "\"Копирование " : "\"Создание ") + "заявки\"");
                        Logging.StopFirstLevel();
                    }

                    if (Mode == 2 && !isCopy && valChanges)
                    {
                        Logging.Comment("Завершение операции \"Редактирование заявки\"");
                        Logging.StopFirstLevel();
                    }

                    if (Mode == 1 && autoSave)
                    {
                        Main.StartLogClearAutoSaveTables();
                        if (id_dep != 6)
                        {
                            Config.hCntMain.ClearAutoSaveTables(autoSaveTreqId);
                        }
                        else
                        {
                            Config.hCntAdd.ClearAutoSaveTables(autoSaveTreqId);
                        }
                        Main.StopLogClearAutoSaveTables();
                    }
                }

                
            }

            if (nstatus != 0)
            {
                for (int i = 0; i < idTRequests.Count; i++)
                {
                    if (nstatus != 6 && !isWeInOut
                        && MessageBox.Show("Хотите распечатать заявку?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        break;
                    }

                    Logging.StartFirstLevel(335);
                    Logging.Comment("Печать заявки");
                    Logging.Comment("Заявка " + idTRequests[i].ToString()
                                    + " от " + dtpDateOut.Value.ToString("dd.MM.yyyy")
                                    + " выведена на печать сотрудником в режиме " + UserSettings.User.StatusCode
                                    + ", ФИО: " + UserSettings.User.FullUsername);
                    Logging.Comment("Окно: " + cbWindow.SelectedValue.ToString());
                    Logging.Comment("Отдел: " + cbDep.Text + ", ID: " + cbDep.SelectedValue.ToString());
                    Logging.Comment("Тип кредитования: " + cbCreditType.Text + ", ID: " + cbCreditType.SelectedValue.ToString());
                    Logging.Comment("Поставщик: " + tbPost.Text.Trim() + ", ID: " + reqIdPost.ToString());
                    Logging.Comment("Тип заявки: " + cbType.Text.Trim() + ", ID " + cbType.SelectedValue.ToString());
                    Logging.Comment("ЮЛ: " + cbUL.Text + ", ID: " + cbUL.SelectedValue.ToString());
                    Logging.Comment("Примечание: " + tbPrimech.Text);
                    Logging.Comment("Менеджер: " + tbMan.Text + ", ID: " + (Mode == 1 || isCopy ? UserSettings.User.Id : (int)reqSettings["id_man"]));

                    if ((int)cbType.SelectedValue == 3)
                    {
                        Logging.Comment("Тип бонуса: " + cbBonusType.Text + ", ID: " + cbBonusType.SelectedValue.ToString());
                        if ((int)cbBonusType.SelectedValue == 5)
                        {
                            Logging.Comment("П_Н: " + tbPN.Text);
                            Logging.Comment("Т_Б: " + tbTB.Text);
                        }
                    }
                    
                    RequestPrint.Print.Print.showReport((int)idTRequests[i], id_dep);
                    Logging.Comment("Завершение операции \"Печать заявки\"");
                    Logging.StopFirstLevel();
                }
            }

            //Ставим флаг режима -1, чтобы не выводить сообщение об изменениях на форме
            Mode = -1;
            this.Close();
        }

        private void btNewGood_Click(object sender, EventArgs e)
        {
            viewDataWeInOut();
            //Добавляем новый товар в заявку
            frmEditGoods frmGood;
            if (Mode == 1 && !isCopy)
            {
                if(cbType.SelectedValue == null) 
                {
                    MessageBox.Show("Необходимо выбрать тип заявки!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmGood = new frmEditGoods(0, (int)cbType.SelectedValue, 0, (int)cbDep.SelectedValue, GetNewEans(), Mode, -1, GetAddedEans(), checkSertificate);
            }
            else
            {
                frmGood = new frmEditGoods(0, (int)cbType.SelectedValue, 0, (int)cbDep.SelectedValue, /*GetNewTovarCount()*/ GetNewEans(), Mode, (int)reqSettings["id_unit"], GetAddedEans(), checkSertificate);
            }
            frmGood.goodParameters = dtReqGoods.NewRow();
            frmGood.setWeInOut(isWeInOut, dtpDateOut.Value, idTReq);
            frmGood.ShowDialog();

            if (frmGood.DialogResult == DialogResult.OK)
            {
                if (autoSave)
                {
                    if (!frmGood.goodParameters.Table.Columns.Contains("id_autoreq"))
                    {
                        frmGood.goodParameters.Table.Columns.Add("id_autoreq",typeof(int));
                    }
                    if (!dtReqGoods.Columns.Contains("id_autoreq"))
                    {
                        dtReqGoods.Columns.Add("id_autoreq", typeof(int));
                        dtReqGoods.AcceptChanges();
                    }
                    frmGood.goodParameters["id_autoreq"] = 0;
                    AutoSaveRequestBody(frmGood.goodParameters, sender, e);
                }
                //frmGood.goodParameters["cname"] = "Бряда";

                dtReqGoods.LoadDataRow(frmGood.goodParameters.ItemArray, true);               
                dtReqGoods.AcceptChanges();
                grdRequestGoods.Refresh();
                SetControlsEnabled();
            }
        }

        private void AutoSaveRequestBody(DataRow goodRow, object sender, EventArgs e)
        {
            if (autoSaveTreqId == 0)
            {
                AutoSaveRequest(sender, e);
            }

            if (Convert.ToInt32(goodRow["id_tovar"]) == 0)
            {
                DataTable newGood = (id_dep != 6 ? Config.hCntMain.AddNewGood((string)goodRow["cname"], (string)goodRow["ean"], (int)goodRow["id_grp1"], (int)goodRow["id_grp2"], 0, (decimal)goodRow["brutto"])
                                                 : Config.hCntAdd.AddNewGood((string)goodRow["cname"], (string)goodRow["ean"], (int)goodRow["id_grp1"], (int)goodRow["id_grp2"], 0, (decimal)goodRow["brutto"]));
                goodRow["id_tovar"] = newGood.Rows[0]["id"];
            }

            if (goodRow.Table.Columns.Contains("id_autoreq"))
            {
                if (goodRow["id_autoreq"] == DBNull.Value)
                    goodRow["id_autoreq"] = 0;

                goodRow["id_autoreq"] = (id_dep != 6 ? Config.hCntMain : Config.hCntAdd).SetAutoRequestBody(Convert.ToInt32(goodRow["id_autoreq"]), autoSaveTreqId, Convert.ToInt32(goodRow["id_tovar"]), Convert.ToInt32(goodRow["id_unit"]), Convert.ToDecimal(goodRow["Tara"]), Convert.ToDecimal(goodRow["zakaz"]), Convert.ToDecimal(goodRow["zcena"]), Convert.ToDecimal(goodRow["rcena"]), Convert.ToDecimal(goodRow["zcenabnds"]), Convert.ToInt32(goodRow["nprimech"]), goodRow["cprimech"].ToString(), Convert.ToInt32(goodRow["id_subject"]), goodRow["PeriodOfStorage"].ToString(), Convert.ToBoolean(goodRow["isTransparent"]), Convert.ToDecimal(goodRow["ShelfSpace"]));
            }
        }

        /// <summary>
        /// Получение еанов добавленных товаров
        /// </summary>
        /// <returns></returns>
        private string[] GetAddedEans()
        {
            if (dtReqGoods == null)
                return new string[0] { };

            return dtReqGoods.Select().AsEnumerable().Select(row => row.Field<string>("ean")).ToArray();
        }

        private void btExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Кол-во новых товаров
        /// </summary>
        /// <returns></returns>
        private int GetNewTovarCount()
        {
            return dtReqGoods.Select("LEN(TRIM(ean)) = 5 AND idReq = 0").Count();
        }

        /// <summary>
        /// Максимальный еан нового добавленного товара
        /// </summary>
        /// <returns></returns>
        private string GetNewEans()
        {
            if (dtReqGoods == null || dtReqGoods.Rows.Count == 0)
                return "";

            DataRow[] addedEans = dtReqGoods.Select("LEN(TRIM(ean)) = 5 AND idReq = 0");
            return Config.GetStringFromRow(addedEans, "ean").Replace("-1","");
            //if(addedEans.Count() == 0)
            //    return 0;
            //else
            //    return addedEans.AsEnumerable().Select(row => row.Field<string>("ean")).ToArray().Max(elem => int.Parse(elem.Trim()));


        }

        private void btEditGood_Click(object sender, EventArgs e)
        {
            EditGood(sender, e);
        }

        private void EditGood(object sender, EventArgs e)
        {
            viewDataWeInOut();
            //Редактирование товара в заявке
            frmEditGoods frmGood = new frmEditGoods(1, 0, (int)curRow["id_tovar"], (int)cbDep.SelectedValue, GetNewEans(), Mode, (Mode == 1 && !isCopy ? -1 : (int)reqSettings["id_unit"]), GetAddedEans(), checkSertificate);
            frmGood.goodParameters = curRow;
            frmGood.setWeInOut(isWeInOut, dtpDateOut.Value, idTReq);
            DialogResult result = frmGood.ShowDialog();
            curRow = frmGood.goodParameters;
            dtReqGoods.AcceptChanges();
            grdRequestGoods.Refresh();

            if (result == DialogResult.OK && autoSave)
            {
                AutoSaveRequestBody(frmGood.goodParameters, sender, e);
            }
        }

        private void btDelGood_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранный товар из заявки?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                DelGoods(curRow);
        }

        /// <summary>
        /// Удаление строки с товаром
        /// </summary>
        /// <param name="deletedRow">строка с товаром</param>
        public void DelGoods(DataRow deletedRow)
        {
            int idRequest = (int)deletedRow["idReq"];
            //запоминаем удаленные товары
            if (idRequest != 0 && !isCopy)
            {
                deletedIdReq.Add(idRequest);
            }

            if (autoSave && deletedRow.Table.Columns.Contains("id_autoreq"))
            {
                if (deletedRow["id_autoreq"] == DBNull.Value)
                    deletedRow["id_autoreq"] = 0;

                autoSaveDeletedIdReq.Add(Convert.ToInt32(deletedRow["id_autoreq"]));
            }

            dtReqGoods.Rows.Remove(deletedRow);
            grdRequestGoods.Refresh();
            SetControlsEnabled();

            if (grdRequestGoods.Rows.Count == 0)
            {                    
                curRow = null;
                SetGoodParameters();
            }
        }

        /// <summary>
        /// Были ли внесены изменения пользователем
        /// </summary>
        /// <returns></returns>
        private bool isValuesChanged(bool checkDate)
        {
            if ((checkDate && dtpDateOut.Value.Date != ((DateTime)reqSettings["req_date"]).Date) ||
                    reqIdPost != (int)reqSettings["id_post"] ||
                    (int)cbType.SelectedValue != (int)reqSettings["id_oper"] ||
                    tbMan.Text != reqSettings["man_name"].ToString() ||
                    (int)cbCreditType.SelectedValue != (int)reqSettings["credit_type"] ||
                    tbTerm.Text != ((int)reqSettings["CreditPeriod"] != 0 ? reqSettings["CreditPeriod"].ToString() : "") ||
                    lbRBSixPrimech.Text.Trim() != reqSettings["statComment"].ToString().Trim() ||
                    tbPrimech.Text.Trim() != reqSettings["cprimech"].ToString().Trim() ||
                    (cbWindow.SelectedValue != null && (int)cbWindow.SelectedValue != int.Parse(reqSettings["porthole"].ToString())) ||
                    (cbUL.SelectedValue != null && (int)cbUL.SelectedValue != int.Parse(reqSettings["ntypeorg"].ToString())) ||
                    (cbBonusType.SelectedValue != null && ((int)cbBonusType.SelectedValue != (int)reqSettings["id_TypeBonus"]
                                                            || ((int)cbBonusType.SelectedValue == 5 && (decimal.Parse(tbPN.Text) != oldPN ||
                                                                                                    decimal.Parse(tbTB.Text) != oldTB)))) ||
                   isTablesDif(dtReqGoods,dtOldReqData))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// проверка таблиц на различия
        /// </summary>
        /// <param name="dtOne">Сверяемая таблица</param>
        /// <param name="dtTwo">Сверяемая таблица</param>
        /// <returns></returns>
        private bool isTablesDif(DataTable dtOne, DataTable dtTwo)
        {
            if (dtOne == null || dtTwo == null)
                return false;

            if (dtOne.Rows.Count != dtTwo.Rows.Count)
                return true;

            int equalRows = 0;
            foreach (DataRow row1 in dtOne.Rows)
            {
                foreach (DataRow row2 in dtTwo.Rows)
                {
                    var array1 = row1.ItemArray;
                    var array2 = row2.ItemArray;

                    if (array1.SequenceEqual(array2))
                    {
                        equalRows++;
                    }
                }
            }

            if (equalRows != dtOne.Rows.Count)
            {
                return true;
            }

            return false;
        }

        private void frmEditRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.None;
            bool cancel = false;
            if ((Mode == 2 && isValuesChanged(false)) || (Mode == 1 && grdRequestGoods.Rows.Count == 0))
            {
                if (MessageBox.Show("Вы уверены, что хотите отменить изменения в текущей заявке?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    cancel = true;
                    this.DialogResult = DialogResult.No;
                }
            }

            if (Mode == 1 && grdRequestGoods.Rows.Count != 0)
            {
                if ((MessageBox.Show("Ваша заявка находится в процессе заполнения!\nПри выходе из формы добавленные товары будут удалены! Хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
                {
                    e.Cancel = true;
                    cancel = true;
                    this.DialogResult = DialogResult.No;
                }
            }

            if (Mode == 1 && autoSave && !cancel)
            {
                autoSaveTimer.Stop();
                Main.StartLogClearAutoSaveTables();
                if (id_dep != 6)
                {
                    Config.hCntMain.ClearAutoSaveTables(autoSaveTreqId);
                }
                else
                {
                    Config.hCntAdd.ClearAutoSaveTables(autoSaveTreqId);
                }
                Main.StopLogClearAutoSaveTables();
            }

            if (Mode != 0 && !e.Cancel)
            {
                Config.linkToCurrentRequest = null;
                Config.currentEan = "";
            }

            if(this.DialogResult == DialogResult.None)
            {
                Mode = -1;
            }
        }

        /// <summary>
        /// Добавление товара по двойному клику на гриде формы список товаров
        /// </summary>
        /// <param name="ean"></param>
        /// <returns></returns>
        public bool AddGoodsFromGoodList(string ean, bool isWeInOut)
        {
            frmEditGoods frmGood = new frmEditGoods(ean, (int)cbDep.SelectedValue, Mode, (Mode == 1 ? -1 : (int)reqSettings["id_unit"]), GetAddedEans(), curIdType, checkSertificate);
            frmGood.setWeInOut(isWeInOut, dtpDateOut.Value, 0);
            frmGood.ShowDialog();

            if (frmGood.DialogResult == DialogResult.OK)
            {
                if (autoSave)
                {
                    frmGood.goodParameters.Table.Columns.Add("id_autoreq");
                    if (!dtReqGoods.Columns.Contains("id_autoreq"))
                    {
                        dtReqGoods.Columns.Add("id_autoreq");
                    }
                    frmGood.goodParameters["id_autoreq"] = 0;
                    AutoSaveRequestBody(frmGood.goodParameters, null, null);
                }
                
                dtReqGoods.LoadDataRow(frmGood.goodParameters.ItemArray, true);
                dtReqGoods.AcceptChanges();
                grdRequestGoods.Refresh();

                if (cbType.SelectedValue == null && curIdType != 0)
                {
                    cbType.SelectedValue = curIdType;
                }

                SetControlsEnabled();
                return true;
            }
            else
            {
                if(cbType.SelectedValue == null)
                    curIdType = 0;
            }

            return false;
        }

        /// <summary>
        /// Добавление товаров по кнопке добавить все товары на форме список товаров
        /// </summary>
        /// <param name="AddedGoods"></param>
        public void AddGoodsMassive(DataTable addedGoods)
        {
            while (bgwEditReq.IsBusy)
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }

            DataRow[] notExistsGoods = addedGoods.Select("id_tovar not in (" + Config.GetStringFromRow(dtReqGoods, "id_tovar") + ")");
            DataRow[] existsGoods = addedGoods.Select("id_tovar in (" + Config.GetStringFromRow(dtReqGoods, "id_tovar") + ")");

            DataTable dtNotExGoodsInf = id_dep != 6 ? Config.hCntMain.GetMassGoodsInformation(Config.GetStringFromRow(notExistsGoods, "id_tovar"), id_dep, reqIdPost)
                                                    : Config.hCntAdd.GetMassGoodsInformation(Config.GetStringFromRow(notExistsGoods, "id_tovar"), id_dep, reqIdPost);

            foreach (DataRow drToAdd in dtNotExGoodsInf.Rows)
            {
                if (autoSave)
                {
                    if (!drToAdd.Table.Columns.Contains("id_autoreq"))
                    {
                        drToAdd.Table.Columns.Add("id_autoreq", typeof(int));
                    }
                    
                    if (dtReqGoods != null && !dtReqGoods.Columns.Contains("id_autoreq"))
                    {
                        dtReqGoods.Columns.Add("id_autoreq", typeof(int));
                    }
                    drToAdd["id_autoreq"] = 0;
                    AutoSaveRequestBody(drToAdd, null, null);
                }
                
                dtReqGoods.LoadDataRow(drToAdd.ItemArray, true);
            }
            
            if (existsGoods != null && existsGoods.Count() > 0
                && MessageBox.Show("Среди добавляемых товаров присутствуют товары из заявки.\nДобавить эти товары?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataTable dtExGoodsInf = id_dep != 6 ? Config.hCntMain.GetMassGoodsInformation(Config.GetStringFromRow(existsGoods, "id_tovar"), id_dep, reqIdPost)
                                                   : Config.hCntAdd.GetMassGoodsInformation(Config.GetStringFromRow(existsGoods, "id_tovar"), id_dep, reqIdPost);

                foreach (DataRow drToAdd in dtExGoodsInf.Rows)
                {
                    if (autoSave)
                    {
                        if (!drToAdd.Table.Columns.Contains("id_autoreq"))
                        {
                            drToAdd.Table.Columns.Add("id_autoreq", typeof(int));
                        }
                        if (!dtReqGoods.Columns.Contains("id_autoreq"))
                        {
                            dtReqGoods.Columns.Add("id_autoreq", typeof(int));
                        }
                        drToAdd["id_autoreq"] = 0;
                        AutoSaveRequestBody(drToAdd, null, null);
                    }
                    
                    dtReqGoods.LoadDataRow(drToAdd.ItemArray, true);
                }
            }

            dtReqGoods.AcceptChanges();
            grdRequestGoods.Refresh();

            if (cbType.SelectedValue == null && curIdType != 0)
            {
                cbType.SelectedValue = curIdType;
            }

            SetControlsEnabled();
        }

        private void grdRequestGoods_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex != -1 && Mode != 0)
            {
                EditGood(sender, e);
            }
        }

        private void ttLegend_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Фон и граница
            e.DrawBackground();
            e.DrawBorder();

            // Текст и легенда
            using (StringFormat sf = new StringFormat())
            {
                //Формат текста
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;

                //Список
                SolidBrush rectBrush = new SolidBrush(Color.FromArgb(255, 76, 237));                
                e.Graphics.DrawString("ограничение на инв. группу", e.Font, Brushes.Black, 30, 20, sf);
                e.Graphics.FillRectangle(rectBrush, 5, 10, 20, 20);
                e.Graphics.DrawRectangle(Pens.Black, 5, 10, 20, 20);
                
                rectBrush = new SolidBrush(Color.FromArgb(0, 167, 14));
                e.Graphics.DrawString("ограничение на группу пересорта", e.Font, Brushes.Black, 30, 50, sf);
                e.Graphics.FillRectangle(rectBrush, 5, 40, 20, 20);
                e.Graphics.DrawRectangle(Pens.Black, 5, 40, 20, 20);

                rectBrush = new SolidBrush(Color.FromArgb(89, 177, 255));
                e.Graphics.DrawString("ограничение на товар распродажи", e.Font, Brushes.Black, 30, 80, sf);
                e.Graphics.FillRectangle(rectBrush, 5, 70, 20, 20);
                e.Graphics.DrawRectangle(Pens.Black, 5, 70, 20, 20);

                rectBrush = new SolidBrush(Color.FromArgb(255, 255, 83));
                e.Graphics.DrawString("новый товар", e.Font, Brushes.Black, 30, 110, sf);
                e.Graphics.FillRectangle(rectBrush, 5, 100, 20, 20);
                e.Graphics.DrawRectangle(Pens.Black, 5, 100, 20, 20);

                rectBrush = new SolidBrush(Color.FromArgb(255, 139, 38));
                e.Graphics.DrawString("аварийный товар", e.Font, Brushes.Black, 30, 140, sf);
                e.Graphics.FillRectangle(rectBrush, 5, 130, 20, 20);
                e.Graphics.DrawRectangle(Pens.Black, 5, 130, 20, 20);

                rectBrush = new SolidBrush(Color.FromArgb(143, 109, 182));
                e.Graphics.DrawString("разовые ограничения на инв. группу", e.Font, Brushes.Black, 30, 170, sf);
                e.Graphics.FillRectangle(rectBrush, 5, 160, 20, 20);
                e.Graphics.DrawRectangle(Pens.Black, 5, 160, 20, 20);
            }
        }

        private void ttLegend_Popup(object sender, PopupEventArgs e)
        {            
            e.ToolTipSize = new Size(240, 190);
        }

        private void grdRequestGoods_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grdRequestGoods.Columns["CauseOfDecline"].Index)
            {
                ttLegend.Hide(btDelGood);
            }

            ttGoodName.Hide(this);
        }

        private void grdRequestGoods_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
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

        private void cbType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int type;
            
            if (cbType.SelectedValue != null && int.TryParse(cbType.SelectedValue.ToString(), out type))
            {
                if (curIdType != 0)
                {
                    if (((type == 1 || type == 3) && curIdType == 2) ||
                        (type == 2 && (curIdType == 1 || curIdType == 3)))

                    {
                        if (MessageBox.Show("Вы действительно хотите поменять тип заявки на " + (type == 1 ? "приход" : type == 2 ? "ВОЗВРАТ" : "бонус") + "?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            type = curIdType;
                            cbType.SelectedValue = curIdType;                                
                        }
                    }
                }

                curIdType = type;
            }
            SetControlsEnabled();
            GetUl(id_dep, dtpDateOut.Value.Date, false);
        }

        public void SetPost(int id_post, string namePost)
        {
            this.reqIdPost = id_post;
            tbPost.Text = namePost;
            viewDataWeInOut();
            GetUl(id_dep, dtpDateOut.Value.Date, false);
        }

        public void GetPost(ref int id_post, ref string namePost)
        {
            id_post = reqIdPost;
            namePost = tbPost.Text.Trim();
        }

        private void grdRequestGoods_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
                Clipboard.SetText(grdRequestGoods.CurrentCell.Value.ToString().Trim()); 
            }
        }

        //NEW 27.07.2017

        private void viewDataWeInOut()
        {             
            DataTable dtTmp = Config.hCntMain.getAllSettingsWeInOut(2, "prhp");

            //Console.WriteLine(dtTmp.Select("Convert(value, 'System.Int32') = " + reqIdPost).Count());

            isWeInOut = (dtTmp != null && dtTmp.Rows.Count != 0 && dtTmp.Select("Convert(value, 'System.Int32') = " + reqIdPost).Count() != 0);
            btSelectTovar.Visible = isWeInOut;
            btSelectTovar.Enabled = isWeInOut;
        }

        private void btSelectTovar_Click(object sender, EventArgs e)
        {
            if (curRow != null)
            {
                sWeInOut.frmSelectTovar frm = new sWeInOut.frmSelectTovar();
                frm.setDataToForm(
                    (int)curRow["id_tovar"],
                    curRow["ean"].ToString().Trim(),
                    curRow["cname"].ToString().Trim(),
                    curRow["zakaz"].ToString(),
                    idTReq,
                    (int)cbDep.SelectedValue);
                frm.ShowDialog();
            }
        }

        private void createSubRequest()
        {
            DataTable dtMainSingleSub = new DataTable();
            dtMainSingleSub.Columns.Add("idTRequest", typeof(Int32));
            dtMainSingleSub.Columns.Add("idTRequestPrihod", typeof(Int32));
            dtMainSingleSub.Columns.Add("idTRequestOtgruz", typeof(Int32));
            dtMainSingleSub.AcceptChanges();

            //DataRow reqInfo = Config.dtRequests.DefaultView.ToTable().Rows[indexRequest];
            int id = idTReq;
            int id_dep = (int)cbDep.SelectedValue;
            DataTable dtReqGoods = (id_dep != 6
                               ? Config.hCntMain.GetRequestBody(id, false, (DateTime)dtpDateOut.Value.Date)
                               : Config.hCntAdd.GetRequestBody(id, false, (DateTime)dtpDateOut.Value.Date));

            DataTable dtListTovarWeInOut = Config.hCntMain.getWeInOutTovarList(0, DateTime.Now, DateTime.Now, "", "", id, 4);

            string strIdPrihod = "";
            foreach (DataRow r in dtListTovarWeInOut.Rows)
            {
                strIdPrihod += "," + r["id_prihod"].ToString();
            }
            strIdPrihod = strIdPrihod.Remove(0, 1);

            DataTable dtPostIdAndNtypeOrg = Config.hCntShop2.getWeInOutIdPostNtypeOrgList(strIdPrihod, 0, 0, 1);
            foreach (DataRow r in dtPostIdAndNtypeOrg.Rows)
            {
                DataTable dtTmp = Config.hCntMain.getWeInOutIdPostNtypeOrgList("", int.Parse(r["id_post"].ToString()), int.Parse(r["ntypeorg"].ToString()), 2);
                if (dtTmp == null || dtTmp.Rows.Count == 0 || dtTmp.Rows[0]["isExists"].ToString().Equals("0"))
                    MessageBox.Show("Сохранение накладной невозможно, не найден поставщик!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(r["id_post"].ToString());
            }

            DataTable dtTovar2Shop = Config.hCntShop2.getWeInOutIdPostNtypeOrgList(strIdPrihod, 0, 0, 3);

            // Получение getWeInOutIdPostNtypeOrgList для ЮЛ указанных в родительской заявке
            DataTable dtPostOtgruzNew = Config.hCntMain.getWeInOutIdPostNtypeOrgList(strIdPrihod, 0, (int)cbUL.SelectedValue, 4);
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
                DataTable dtInIdNewRequest = Config.hCntMain.setSubShopData(id_dep, int.Parse(dtPostPrihodNew.Rows[0]["id_post"].ToString()), DateTime.Parse(dtpDateOut.Value.Date.ToString()), (Mode == 1 || isCopy ? UserSettings.User.Id : (int)reqSettings["id_man"]), (int)cbUL.SelectedValue, (int)cbWindow.SelectedValue, 1, id);

                if (dtInIdNewRequest == null || dtInIdNewRequest.Rows.Count == 0)
                    return;

                idNewTreqSubShop = int.Parse(dtInIdNewRequest.Rows[0]["id"].ToString());
                listNewIdTrequestSubShop += "," + idNewTreqSubShop.ToString();
                dtMainSingleSub.Rows[dtMainSingleSub.Rows.Count - 1]["idTRequestPrihod"] = idNewTreqSubShop;

                // Создание заголовка для доп. коннекта
                DataTable dtInIdOtgruzRequest = Config.hCntShop2.setSubShopData(id_dep, int.Parse(dtPostOtgruzNew.Rows[0]["id_post"].ToString()), DateTime.Parse(dtpDateOut.Value.Date.ToString()), (Mode == 1 || isCopy ? UserSettings.User.Id : (int)reqSettings["id_man"]), int.Parse(r["ntypeorg"].ToString()), (int)cbWindow.SelectedValue, 2, idNewTreqSubShop);

                if (dtInIdOtgruzRequest == null || dtInIdOtgruzRequest.Rows.Count == 0)
                    return;

                idNewOtgruzTReq = int.Parse(dtInIdOtgruzRequest.Rows[0]["id"].ToString());
                dtMainSingleSub.Rows[dtMainSingleSub.Rows.Count - 1]["idTRequestOtgruz"] = idNewOtgruzTReq;

                //

                DataRow[] rowFind = dtTovar2Shop.Select(string.Format("ntypeorg = {0} AND id_post={1}", int.Parse(r["ntypeorg"].ToString()), int.Parse(r["id_post"].ToString())));
                foreach (DataRow rAdd in rowFind)
                {
                    DataRow[] rBusyGoods = dtListTovarWeInOut.Select(string.Format("id_prihod = {0}", rAdd["id"].ToString()));
                    if (rBusyGoods.Count() > 0)
                    {
                        DataRow[] rRequestTovar = dtReqGoods.Select(string.Format("id_tovar = {0}", rBusyGoods[0]["id_tovar"].ToString()));
                        if (rRequestTovar.Count() > 0)
                        {
                            decimal zcenNDSNew = 0;

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
                                DateTime.Parse(dtpDateOut.Value.Date.ToString())
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
                               DateTime.Parse(dtpDateOut.Value.Date.ToString())
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
    }

    
}
