using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.User;

namespace Requests
{
    public partial class frmEditGoods : Form
    {
        /// <summary>
        /// 0 - Добавление нового, 1 - изменение товара из заявки, 2 - добавление сущ. товара, 3 - добавление товара с формы список товаров
        /// </summary>
        int mode;
        int idOperReq;
        int id_tovar;
        int id_dep;
        decimal oldZatar;
        bool Spr_isTransparent = false;
        string newAddedEans;
        
        /// <summary>
        /// режим открытия заявки 1 - создание, 2 - редактирование
        /// </summary>
        int reqMode; 

        /// <summary>
        /// тип заявки(шт/вес)
        /// </summary>
        int reqIdUnit; 
        string[] addedEans;
        decimal oldRcena;
        int oldSubject;
        DataTable dtAllTuGrps;        

        public DataRow goodParameters { get; set; }

        /// <summary>
        /// true - искать товар в j_newtovar, если не нашли в s_tovar
        /// </summary>
        bool? isNewTovar = null;
        bool load = false;

        bool checkSertificate = false;

        #region Инициализация формы и заполнение контролов

        public frmEditGoods(int mode, int idOperand, int id_tovar, int id_dep, string newAddedEans, int reqMode, int reqIdUnit, string[] addedEans, bool checkSertificate)
        {
            InitializeComponent();
            this.mode = mode;
            this.idOperReq = idOperand;
            this.id_tovar = id_tovar;
            this.id_dep = id_dep;
            this.newAddedEans = newAddedEans;
            this.reqMode = reqMode;
            this.reqIdUnit = reqIdUnit;
            this.addedEans = addedEans;
            this.checkSertificate = checkSertificate;
        }

        //Добавление товара из формы список товаров
        public frmEditGoods(string ean, int id_dep, int reqMode, int reqIdUnit, string[] addedEans, int idOperand, bool checkSertificate)
        {
            InitializeComponent();
            this.mode = 3;
            this.id_dep = id_dep;
            this.reqMode = reqMode;
            this.reqIdUnit = reqIdUnit;
            this.addedEans = addedEans;
            this.tbEan.Text = ean.Trim();
            this.idOperReq = idOperand;
            this.checkSertificate = checkSertificate;
            isNewTovar = false;
        }

        private void frmEditGoods_Load(object sender, EventArgs e)
        {
            load = true;            

            dtAllTuGrps = id_dep != 6 ? Config.hCntMain.GetTU(id_dep, (isNewTovar.HasValue && isNewTovar.Value), false)
                                        : Config.hCntAdd.GetTU(id_dep, (isNewTovar.HasValue && isNewTovar.Value), false);
            if (mode == 1)
            {
                isNewTovar = (((int)goodParameters["nprimech"]) == 1);
            }

            GetUnitList();
            GetNdsList();
            GetTU();
            GetSubjectList();
            if (mode == 1)
                SetGoodParameters();

            if (UserSettings.User.StatusCode != "КД"
                && UserSettings.User.StatusCode != "РКВ"
                && UserSettings.User.StatusCode != "ДМН")
            {
                chkPlanRealizDay.Visible =
                    tbDayPlanReal.Visible =
                    chkOtsechDays.Visible =
                    dtpOtsechStart.Visible =
                    lbOtsechFin.Visible =
                    dtpOtsechFinish.Visible = false;
            }

            if (mode == 3)
            {
                FindGood();
                load = false;
                return;
            }

            SetControlsEnabled();
            SetControlsVisible();
            load = false;
        }

        private void SetGoodParameters()
        {
            if (goodParameters != null)
            {
                tbEan.Text = goodParameters["ean"].ToString().Trim();
                tbName.Text = goodParameters["cname"].ToString().Trim();
                cbSubject.SelectedValue = (int)goodParameters["id_subject"];
                oldSubject = (int)goodParameters["id_subject"];
                tbOst.Text = ((decimal)goodParameters["TekOst"]).ToString("#0.000");
                oldZatar = decimal.Parse(goodParameters["spr_zatar"].ToString());
                tbZakaz.Text = goodParameters["zakaz"].ToString();
                tbZcenabnds.Text = goodParameters["zcenabnds"].ToString();
                tbZcena.Text = goodParameters["zcena"].ToString();
                tbRcena.Text = goodParameters["rcena"].ToString();
                oldRcena = (decimal)goodParameters["rcena"];
                tbZapas.Text = goodParameters["zapas"].ToString();
                tbZapas2.Text = goodParameters["zapas2"].ToString();
                tbPrimech.Text = goodParameters["cprimech"].ToString().Replace("НТ ", "").Replace("НЦ ", "");
                tbCzak.Text = (id_dep != 6 ?
                                    Config.hCntMain.GetCommonOrder((int)goodParameters["id_tovar"], id_dep).ToString("0.000") :
                                    Config.hCntAdd.GetCommonOrder((int)goodParameters["id_tovar"], id_dep).ToString("0.000"));
                cbTU.SelectedValue = (int)goodParameters["id_grp1"];
                //if (mode == 1)
                GetInvGrp();
                cbInvGrp.SelectedValue = (int)goodParameters["id_grp2"];
                cbUnit.SelectedValue = (int)goodParameters["id_unit"];
                cbNds.SelectedValue = (int)goodParameters["id_nds"];
                Spr_isTransparent = (bool)goodParameters["spr_isTransparent"];
                chbIsTransparent.Checked = (bool)goodParameters["isTransparent"];

                if (UserSettings.User.StatusCode != "МН"
                    /*&& UserSettings.User.StatusCode != "ДМН"*/)
                {
                    if (mode == 1)
                    {
                        if ((goodParameters["BeginOfPeriod"].ToString().Length != 0
                            && goodParameters["EndOfPeriod"].ToString().Length != 0))
                        {
                            chkOtsechDays.Checked = true;
                            dtpOtsechStart.Value = DateTime.Parse(goodParameters["BeginOfPeriod"].ToString());
                            dtpOtsechFinish.Value = DateTime.Parse(goodParameters["EndOfPeriod"].ToString());
                        }
                        else
                        {
                            if (goodParameters["CauseOfDecline"].ToString().Contains("Товар с Ср.расход<0"))
                            {
                                chkOtsechDays.Checked = true;
                            }

                            DataTable dtRealDates = id_dep != 6 ? Config.hCntMain.GetGoodODates((int)goodParameters["id_tovar"])
                                                                   : Config.hCntAdd.GetGoodODates((int)goodParameters["id_tovar"]);
                            dtpOtsechStart.Value = DateTime.Parse(dtRealDates.Rows[0]["BeginOfPeriod"].ToString());
                            dtpOtsechFinish.Value = DateTime.Parse(dtRealDates.Rows[0]["EndOfPeriod"].ToString());
                        }

                        if (((decimal)goodParameters["PlanRealiz"]) != -1)
                        {
                            chkPlanRealizDay.Checked = true;
                            tbDayPlanReal.Text = goodParameters["PlanRealiz"].ToString();
                        }
                        else
                        {
                            if (goodParameters["CauseOfDecline"].ToString().Contains("Товар новый") || isNewTovar.Value)
                            {
                                chkPlanRealizDay.Checked = true;
                                chkPlanRealizDay.Enabled =
                                    chkOtsechDays.Enabled = false;
                            }
                            tbDayPlanReal.Text = "0,000";
                        }
                    }
                    else
                    {
                        if (!isNewTovar.Value)
                        {
                            dtpOtsechStart.Value = DateTime.Parse(goodParameters["BeginOfPeriod"].ToString());
                            dtpOtsechFinish.Value = DateTime.Parse(goodParameters["EndOfPeriod"].ToString());
                        }

                        tbDayPlanReal.Text = "0,000";
                    }

                    if (isNewTovar.Value)
                    {
                        chkOtsechDays.Enabled = false;
                    }
                }

                tbShelfSpace.Text = goodParameters["ShelfSpace"].ToString();
                tbPeriodOfStorage.Text = goodParameters["PeriodOfStorage"].ToString();
                tbBrutto.Text = goodParameters["brutto"].ToString();
                id_tovar = (int)goodParameters["id_tovar"];

                ValidateZakaz();
                ChexInputValue(tbZcenabnds, 6, 4);
                ChexInputValue(tbZcena, 6, 4);
                ChexInputValue(tbRcena, 8, 2);
                ChexInputValue(tbBrutto, 8, 3);

                calculPercent();
                SetControlsEnabled();

                //Start NEW 28.07.2017
                tbRcena.Enabled = !isWeInOut;

                if (isWeInOut)
                {
                    DataTable dtTmp = Config.hCntMain.getWeInOutTovarPrice(tbEan.Text.Trim(), dateRequest, 1);
                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                        tbZcena.Text = dtTmp.Rows[0]["zcena"].ToString();

                    dtTmp = Config.hCntMain.getWeInOutTovarPrice(tbEan.Text.Trim(), dateRequest, 2);
                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                        tbRcena.Text = dtTmp.Rows[0]["rcena"].ToString();
                    btSelectTovar.Enabled = true;
                }
                //End NEW 28.07.2017

                //CalculZcena(true);
            }

            DataTable dtOstZapas2 = Config.hCntShop2.GetTekOstAndZapas((isNewTovar.HasValue && isNewTovar.Value ? 0 : 1), tbEan.Text.Trim());
            if (dtOstZapas2 != null && dtOstZapas2.Rows.Count > 0)
            {
                tbOstM2.Text = ((decimal)dtOstZapas2.Rows[0]["TekOst"]).ToString("#0.000");
                tbZapasM2.Text = dtOstZapas2.Rows[0]["zapas"].ToString();
            }
        }

        /// <summary>
        /// Поиск товара
        /// </summary>
        private void FindGood()
        {

            if (isNewTovar.HasValue && !isNewTovar.Value && isWeInOut)
            {
                if (!Config.checkStatusTovarInBases(tbEan.Text.Trim(), id_dep))
                {
                    MessageBox.Show("Введённый EAN товара отсутствует\nв базе данных!", "Ввод EAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbEan.Text = "";
                    return;
                }
            }


            if (isNewTovar.HasValue && isNewTovar.Value)
            {
                if (tbEan.Text.Trim().Length == 4)
                {
                    MessageBox.Show("Код для весового товара\nформируется автоматически!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbEan.Text = "";
                    return;
                }
            }

            if (addedEans.Contains(tbEan.Text) && isWeInOut)
            {
                MessageBox.Show("Товар с таким EAN уже есть в заявке!", "Ввод EAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbEan.Text = "";
                return;
            }

            if (addedEans.Contains(tbEan.Text) && !isWeInOut
              && MessageBox.Show("Товар с кодом \"" + tbEan.Text.Trim() + "\" Уже был добавлен в заявку!\nХотите добавить еще один товар с таким кодом?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                tbEan.Text = "";
                return;
            }

            if ((id_dep != 6 ? Config.hCntMain.CheckNewGoodExists(tbEan.Text)
                              : Config.hCntAdd.CheckNewGoodExists(tbEan.Text))
                && (isNewTovar.HasValue && isNewTovar.Value))
            {
                if (MessageBox.Show("Код с таким товаром уже существует!\nВвести данные существующего кода?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    isNewTovar = false;
                }
                else
                {
                    tbEan.Text = "";
                    return;
                }
            }

            if (Config.hCntMain.GetSettings("div").Select("value = '" + id_dep.ToString() + "'").Count() > 0
                && reqMode == 2
                && !new int[2] { 2, 3 }.Contains(idOperReq))
            {
                if (tbEan.Text.Trim().Length == 4 && reqIdUnit == 2)
                {
                    tbEan.Text = "";
                    MessageBox.Show("Вы пытаетесь добавить весовой\nтовар в заявку с штучным товаром.\nДобавление товара невозможно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    closeFormOnAddGood();
                    return;
                }

                if (tbEan.Text.Trim().Length > 4 && reqIdUnit == 1)
                {
                    tbEan.Text = "";
                    MessageBox.Show("Вы пытаетесь добавить штучный\nтовар в заявку с весовым товаром.\nДобавление товара невозможно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    closeFormOnAddGood();
                    return;
                }
            }

            int idPostReq = 0;
            string namePostReq = "";
            if (Config.linkToCurrentRequest != null)
            {
                Config.linkToCurrentRequest.GetPost(ref idPostReq, ref namePostReq);
            }

            DataTable dtGoodInformation = (id_dep != 6 ? Config.hCntMain.GetGoodInformation((isNewTovar.HasValue && isNewTovar.Value ? 0 : 1), tbEan.Text.Trim(), id_dep, idOperReq, idPostReq)
                                                       : Config.hCntAdd.GetGoodInformation((isNewTovar.HasValue && isNewTovar.Value ? 0 : 1), tbEan.Text.Trim(), id_dep, idOperReq, idPostReq));

            if (dtGoodInformation.Columns.Contains("error_code"))
            {
                int code = (int)dtGoodInformation.Rows[0]["error_code"];

                switch (code)
                {
                    case 0:
                        if (isNewTovar.HasValue && !isNewTovar.Value)
                            MessageBox.Show("Товаров с таким кодом не найдено!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 1:
                        MessageBox.Show("Введенный код принадлежит другому отделу!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 2:
                        MessageBox.Show("Вы не имеете прав для\nдоступа к данному товару!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 3:
                        MessageBox.Show("Товар: " + dtGoodInformation.Rows[0]["cname"].ToString() + "\n присутствует в списке запрещенных товаров для\nдобавления в заявку\nДобавление указанного товара в заявку невозможно.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }

                //if(code != 0)
                if (isNewTovar.HasValue && !isNewTovar.Value)
                    tbEan.Text = "";
                closeFormOnAddGood();
                return;
            }

            // Временное (вечное =( ) решение если на чистой базе нет данных по приходам
            if (id_dep != 6 /*&& isWeInOut*/){
                DataTable dtGoodInformation2 = Config.hCntShop2.GetGoodInformation((isNewTovar.HasValue && isNewTovar.Value ? 0 : 1), tbEan.Text.Trim(), id_dep, idOperReq, idPostReq);

                if ((dtGoodInformation.Rows[0]["zcena"] == DBNull.Value || Convert.ToDecimal(dtGoodInformation.Rows[0]["zcena"].ToString()) == Convert.ToDecimal(0))
                    && (dtGoodInformation.Rows[0]["zcenabnds"] == DBNull.Value || Convert.ToDecimal(dtGoodInformation.Rows[0]["zcenabnds"].ToString()) == Convert.ToDecimal(0)))
                {
                    dtGoodInformation.Rows[0]["zcena"] = dtGoodInformation2.Rows[0]["zcena"];
                    dtGoodInformation.Rows[0]["zcenabnds"] = dtGoodInformation2.Rows[0]["zcenabnds"];
                    
                }

                if (dtGoodInformation.Rows[0]["Zatar"] == DBNull.Value || Convert.ToDecimal(dtGoodInformation.Rows[0]["Zatar"].ToString()) == Convert.ToDecimal(0))
                {
                    dtGoodInformation.Rows[0]["Zatar"] = dtGoodInformation2.Rows[0]["Zatar"];
                }
            }

            if (dtGoodInformation.Rows.Count > 0)
            {
                goodParameters = dtGoodInformation.Rows[0];

                if (dtAllTuGrps.Select("id = " + goodParameters["id_grp1"].ToString()).Count() == 0)
                {
                    MessageBox.Show("Данный товар не принадлежит Т/У группам пользователя.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbEan.Text = "";
                    closeFormOnAddGood();
                    return;
                }

                isNewTovar = (int)goodParameters["nprimech"] == 1;
                mode = 2;
                //(int)goodParameters["nprimech"] != 1 ? 0 : 3;
            }

            chkCreateLocalCode.Enabled = false;
            SetGoodParameters();

        }

        private void GetUnitList()
        {
            DataTable dtUnitList = Config.hCntMain.GetUnitList();
            cbUnit.DataSource = dtUnitList;
            cbUnit.DisplayMember = "cunit";
            cbUnit.ValueMember = "id";
            cbUnit.SelectedIndex = -1;
        }

        private void GetNdsList()
        {
            DataTable dtNdsList = Config.hCntMain.GetNdsList();
            cbNds.DataSource = dtNdsList;
            cbNds.DisplayMember = "nds";
            cbNds.ValueMember = "id";
            cbNds.SelectedIndex = -1;
        }

        private void GetTU()
        {
            DataTable dtTU = (id_dep != 6 ? Config.hCntMain.GetTU(id_dep, false, true)
                                          : Config.hCntAdd.GetTU(id_dep, false, true));
            cbTU.DataSource = dtTU;
            cbTU.DisplayMember = "cname";
            cbTU.ValueMember = "id";
        }

        private void GetSubjectList()
        {
            DataTable dtSubjects = Config.hCntMain.GetSubjectList();
            cbSubject.DataSource = dtSubjects;
            cbSubject.DisplayMember = "cname";
            cbSubject.ValueMember = "id";
        }

        private void GetInvGrp()
        {
            if (cbTU.SelectedValue == null || (int)cbTU.SelectedValue == 0)
            {
                cbInvGrp.DataSource = null;
            }
            else
            {
                DataTable dtInvGrp = (id_dep != 6 ? Config.hCntMain.GetInvGrp(id_dep, (int)cbTU.SelectedValue, (cbUnit.SelectedIndex == -1 ? 0 : (int)cbUnit.SelectedValue))
                                              : Config.hCntAdd.GetInvGrp(id_dep, (int)cbTU.SelectedValue, (cbUnit.SelectedIndex == -1 ? 0 : (int)cbUnit.SelectedValue)));
                cbInvGrp.DataSource = dtInvGrp;
                cbInvGrp.DisplayMember = "cname";
                cbInvGrp.ValueMember = "id";
            }
        }

        private void SetControlsEnabled()
        {
            tbEan.Enabled = (mode == 0);
            cbTU.Enabled =
            cbInvGrp.Enabled = (mode == 0 || (isNewTovar.HasValue && isNewTovar.Value) || (goodParameters != null && dtAllTuGrps.Select("id = " + goodParameters["id_grp1"].ToString())[0]["cname"].ToString().Trim().ToUpper() == "НОВЫЙ ТОВАР"));
            cbUnit.Enabled =
            cbNds.Enabled = (mode == 0 || (isNewTovar.HasValue && isNewTovar.Value));
            tbName.Enabled = (mode == 0 || (isNewTovar.HasValue && isNewTovar.Value));
            tbOst.Enabled =
            tbShelfSpace.Enabled = (isNewTovar.HasValue && !isNewTovar.Value);
            if (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
            {
                //tbZcenabnds.Enabled = ( 0 && Config.dtProperties.Select("TRIM(id_val) = 'using zcenabn' AND TRIM(val) = '1'").Count() > 0);
                tbZapas.Enabled = (Config.dtProperties.Select("TRIM(id_val) = 'using stock' AND TRIM(val) = '1'").Count() > 0);
            }

        }

        private void SetControlsVisible()
        {
            bool BruttoVis = false;
            bool isTransVis = false;

            int selVal;
            if (cbUnit.SelectedValue != null && int.TryParse(cbUnit.SelectedValue.ToString(), out selVal))
            {
                if (selVal == 2)
                {
                    isTransVis = true;

                    if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН" || UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "КД")
                        && (isNewTovar.HasValue)
                        && (isNewTovar.Value))
                    {
                        BruttoVis = true;
                    }
                }
            }

            tbBrutto.Visible
                    = lbBrutto.Visible
                    = BruttoVis;

            chbIsTransparent.Visible
                = isTransVis;
        }

        private void closeFormOnAddGood()
        {
            if (mode == 3)
            {
                // 2b
                //this.Dispose();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #endregion
        
        #region KeyPress

        private void tbEan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("0123456789\b".Contains(e.KeyChar) /*|| e.KeyChar == (char)13*/)
            {
                if (tbEan.Text.Length == 13 && e.KeyChar != '\b'  /*|| (e.KeyChar == (char)13 && tbEan.Text.Length >= 4)*/)
                {
                    e.Handled = true;
                    GoToNextControl((char)13);
                    SetControlsVisible();
                    //FindGood();
                }
            }
            else
            {
                e.Handled = true;
            }

            GoToNextControl(e.KeyChar);
        }

        private void tbOst_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = AllowCharInputDecimal(tbOst, 7, 3, e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbZatar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)cbUnit.SelectedValue == 1)
            {
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
                e.Handled = AllowCharInputDecimal(tbZatar, 4, 3, e.KeyChar);
            }
            else
            {
                e.Handled = !"0123456789\b".Contains(e.KeyChar);
            }

            GoToNextControl(e.KeyChar);
        }

        private void tbTara_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !"0123456789\b".Contains(e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbZakaz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)cbUnit.SelectedValue == 1)
            {
                if (e.KeyChar == '.')
                    e.KeyChar = ',';
                e.Handled = AllowCharInputDecimal(tbZakaz, 6, 3, e.KeyChar);
            }
            else
            {
                e.Handled = !"0123456789\b".Contains(e.KeyChar);
            }

            GoToNextControl(e.KeyChar);
        }

        private void tbZcenabnds_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = AllowCharInputDecimal(tbZcenabnds, 6, 4, e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbZcena_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = AllowCharInputDecimal(tbZcena, 6, 4, e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbRcena_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = AllowCharInputDecimal(tbRcena, 9, 2, e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbZapas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !"0123456789\b".Contains(e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            GoToNextControl(e.KeyChar);
        }

        private void tbDayPlanReal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = AllowCharInputDecimal(tbDayPlanReal, 8, 3, e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbShelfSpace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = AllowCharInputDecimal(tbShelfSpace, 7, 3, e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        private void tbPeriodOfStorage_KeyPress(object sender, KeyPressEventArgs e)
        {
            GoToNextControl(e.KeyChar);
        }

        private void tbPrimech_KeyPress(object sender, KeyPressEventArgs e)
        {
            GoToNextControl(e.KeyChar);
        }

        private void tbBrutto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = AllowCharInputDecimal(tbBrutto, 8, 3, e.KeyChar);
            GoToNextControl(e.KeyChar);
        }

        #endregion

        #region Validated

        private void tbZatar_Validated(object sender, EventArgs e)
        {            
            //calculZakaz(false);
            
            bool ChangeRequestOnTarZatarChanging = Config.hCntMain.GetProperties("change zakaz");
            bool ChangeTara = Config.hCntMain.GetProperties("tara");
            decimal Zkz = 0;
            decimal Tr = 0;
            
            decimal.TryParse(tbZakaz.Text, out Zkz);
            decimal.TryParse(tbTara.Text, out Tr);
            
            if (ChangeRequestOnTarZatarChanging)
            {
                if (ChangeTara)
                {
                    //если тара = 0, считаем тару
                    if ((Tr == 0) || (Zkz != 0))
                    {
                        recalcTara();
                    }
                    /*
                    //если заказ = 0, считаем заказ
                    if ((Tr != 0) && (Zkz == 0))
                    {
                        recalcZakaz();
                    }

                    //если и тара и заказ не равны нулю, считаем заказ
                    if ((Tr != 0) && (Zkz != 0))
                    {
                        recalcZakaz();
                    }

                    //если и тара и заказ равны нулю, ничего не считаем 
                    if ((Tr == 0) && (Zkz == 0))
                    {
                    }*/

                }
                else
                {
                    recalcZakaz();
                }                
            }            

            calculZapas2(true);
        }

        private void tbTara_Validated(object sender, EventArgs e)
        {
            //calculZakaz(false);            

            decimal Zakaz = 0;
            bool ChangeRequestOnTarZatarChanging = Config.hCntMain.GetProperties("change zakaz");

            if (ChangeRequestOnTarZatarChanging)
            {
                Zakaz = (decimal.Parse(tbZatar.Text) * int.Parse(tbTara.Text));
                tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));
            }

            calculZapas2(true);
        }

        private void tbZcenabnds_Validated(object sender, EventArgs e)
        {
            CalculZcena(false);
        }

        private void tbZcena_Validated(object sender, EventArgs e)
        {
            CalculZcena(true);
            calculPercent();
        }

        private void tbZapas_Validated(object sender, EventArgs e)
        {
            //calculZakaz(true);

            decimal Zakaz = 0;

            bool standartCalc = true;
            bool ChangeRequestOnTarZatarChanging = false;

            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && isNewTovar.HasValue)
            {
                DataRow[] drStock = Config.dtProperties.Select("id_val = 'using stock'");
                if (drStock.Count() > 0
                    && int.Parse(drStock[0]["val"].ToString()) == 1
                    && goodParameters != null
                    //&& fromZapas
                    )
                {
                    standartCalc = false;
                }
            }

            ChangeRequestOnTarZatarChanging = Config.hCntMain.GetProperties("change zakaz");

            if (standartCalc)
            {
                if (ChangeRequestOnTarZatarChanging)
                {
                    Zakaz = (decimal.Parse(tbZatar.Text) * int.Parse(tbTara.Text));
                    tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));
                }
            }
            else
            {
                if (ChangeRequestOnTarZatarChanging)
                {
                    decimal avgReal, zakazP, ost;
                    int zapas;
                    if (decimal.TryParse(goodParameters["AvgReal"].ToString(), out avgReal)
                        && decimal.TryParse(goodParameters["ZakazP"].ToString(), out zakazP)
                        && int.TryParse(tbZapas.Text.Trim(), out zapas)
                        && decimal.TryParse(tbOst.Text.Trim(), out ost))
                    {
                        Zakaz = zapas * avgReal
                            - zakazP - ost;
                    }
                    else
                    {
                        Zakaz = 0;
                    }
                    tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));

                    decimal taratara = (decimal.Parse(tbZatar.Text) == 0) ? 0 : Zakaz / decimal.Parse(tbZatar.Text);
                    tbTara.Text = Math.Round(taratara, 0).ToString("#0"); ;
                }
                else
                {
                    decimal avgReal, zakazP, ost;
                    int zapas;
                    if (decimal.TryParse(goodParameters["AvgReal"].ToString(), out avgReal)
                        && decimal.TryParse(goodParameters["ZakazP"].ToString(), out zakazP)
                        && int.TryParse(tbZapas.Text.Trim(), out zapas)
                        && decimal.TryParse(tbOst.Text.Trim(), out ost))
                    {
                        Zakaz = zapas * avgReal
                            - zakazP - ost;
                    }
                    else
                    {
                        Zakaz = 0;
                    }
                    tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));
                }
            }

            calculZapas2(true);
        }

        private void dtpOtsechStart_Validated(object sender, EventArgs e)
        {
            dtpOtsechFinish.Value = dtpOtsechStart.Value.AddDays(6);
            calculZapas2(false);
        }

        private void dtpOtsechFinish_Validated(object sender, EventArgs e)
        {
            dtpOtsechStart.Value = dtpOtsechFinish.Value.AddDays(-6);
            calculZapas2(false);
        }

        private void tbZakaz_Validated(object sender, EventArgs e)
        {
            bool ChangeTara = Config.hCntMain.GetProperties("tara");

            if (ChangeTara)
                recalcTara();

            calculZapas2(true);
        }

        /// <summary>
        /// Процедура перерасчета Тары
        /// </summary>
        private void recalcTara()
        {
            decimal temp1 = decimal.Parse(tbZakaz.Text);
            decimal temp2 = decimal.Parse(tbZatar.Text);

            if (temp2 == 0) { MessageBox.Show("Затарка равняется нулю!", "Ошибка!", MessageBoxButtons.OK); tbZatar.Focus(); tbZatar.BackColor = Color.Red; return; }
            //получаем результат деления заказа на затарку
            if (tbZatar.BackColor == Color.Red) tbZatar.BackColor = Color.White;
            decimal division = temp1 / temp2;
            
            //получаем целую часть от деления
            int result = (int)division;

            //получаем дробную часть
            decimal fractPart = division % 1;

            //если дробная часть >0 увеличиваем целую часть на 1
            if (fractPart > 0)
            {
                result++;
            }

            tbTara.Text = result.ToString();            
        }

        /// <summary>
        /// Процедура перерасчета Заказа
        /// </summary>
        private void recalcZakaz()
        {
            decimal Zakaz = 0;
            Zakaz = (decimal.Parse(tbZatar.Text) * int.Parse(tbTara.Text));
            tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));
        }

        private void tbDayPlanReal_Validated(object sender, EventArgs e)
        {
            calculZapas2(false);
        }

        private void tbEan_Validated(object sender, EventArgs e)
        {
            if (tbEan.Text.Trim().Length >= 4)
                FindGood();
            SetControlsVisible();
        }

        private void tbRcena_Validated(object sender, EventArgs e)
        {
            calculPercent();
        }

        #endregion

        #region Validating

        private void tbZakaz_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateZakaz();
        }

        private void tbOst_Validating(object sender, CancelEventArgs e)
        {
            decimal d;
            if (!decimal.TryParse(tbOst.Text, out d) && tbOst.Text.Length != 0)
            {
                e.Cancel = true;
            }
            else
            {
                if (d >= 10000000)
                    d = 9999999.999m;
                tbOst.Text = d.ToString("#0.000");
            }
        }

        private void tbZatar_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateZatar();
        }

        private void tbTara_Validating(object sender, CancelEventArgs e)
        {
            if (tbTara.Text.Trim().Length == 0)
            {
                tbTara.Text = "0";
            }
        }

        private void tbZcenabnds_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ChexInputValue(tbZcenabnds, 6, 4);
        }

        private void tbZcena_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ChexInputValue(tbZcena, 6, 4);
        }

        private void tbRcena_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ChexInputValue(tbRcena, 8, 2);
        }

        private void tbZapas_Validating(object sender, CancelEventArgs e)
        {
            if (tbZapas.Text.Length == 0)
            {
                tbZapas.Text = "0";
            }
        }

        private void tbDayPlanReal_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ChexInputValue(tbDayPlanReal, 8, 3);
        }

        private void tbShelfSpace_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ChexInputValue(tbShelfSpace, 7, 3);
        }

        private void tbBrutto_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ChexInputValue(tbBrutto, 8, 3);
        }

        #endregion

        #region Обработка изменений значений контролов

        private void cbTU_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetInvGrp();
        }

        private void cbUnit_SelectedValueChanged(object sender, EventArgs e)
        {
            int selVal;
            if (cbUnit.SelectedValue != null && int.TryParse(cbUnit.SelectedValue.ToString(), out selVal))
            {
                tbZatar.Enabled =
                    tbTara.Enabled = true;

                try
                {
                    string zatarVal = goodParameters["Zatar"].ToString().Trim().Length == 0 ? "0" : goodParameters["Zatar"].ToString();
                    string taraVal = goodParameters["Tara"].ToString().Trim().Length == 0 ? "0" : goodParameters["Tara"].ToString();
                    tbZatar.Text = ((goodParameters["idReq"].GetType() != typeof(System.DBNull) && (int)goodParameters["idReq"] != 0) )?
                        (int.Parse(taraVal) == 0 ? zatarVal : (decimal.Parse(tbZakaz.Text) / int.Parse(taraVal)).ToString())
                        : goodParameters["Zatar"].ToString().Trim().Length == 0 ? "0" : goodParameters["Zatar"].ToString();
                    tbTara.Text = taraVal;

                    chkCreateLocalCode.Enabled = (selVal == 2 && (!isNewTovar.HasValue || isNewTovar.Value) && mode == 0);

                    tbZakaz.Enabled = true;

                    string filter = (selVal == 2 ? "#0" : "#0.000");
                    tbZakaz.Text = decimal.Parse(tbZakaz.Text).ToString(filter);
                    tbZatar.Text = decimal.Parse(tbZatar.Text).ToString(filter);
                    tbZakaz.MaxLength = (selVal == 2 ? 6 : 10);
                }
                catch { }
            }
            else
            {
                tbZatar.Enabled =
                    tbTara.Enabled =
                    tbZakaz.Enabled =
                    chkCreateLocalCode.Enabled = false;
            }
            SetControlsVisible();

        }

        private void cbNds_SelectedValueChanged(object sender, EventArgs e)
        {
            int selVal;
            if (cbNds.SelectedValue != null && int.TryParse(cbNds.SelectedValue.ToString(), out selVal))
            {
                if (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                {
                    tbZcenabnds.Enabled = (Config.dtProperties.Select("TRIM(id_val) = 'using zcenabn' AND TRIM(val) = '1'").Count() > 0);
                }
                else
                {
                    tbZcenabnds.Enabled = true;
                }
                //tbZcena.Enabled = true;

                tbZcena.Enabled = !isWeInOut; //NEW 28.07.2017

                CalculZcena(true);
            }
            else
            {
                tbZcenabnds.Enabled =
                    tbZcena.Enabled = false;
            }
        }

        private void cbUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetInvGrp();
            if (cbUnit.SelectedValue != null)
            {
                if ((int)cbUnit.SelectedValue == 1)
                {
                    tbEan.Text = "";
                    tbEan.Enabled = false;
                }
                else
                {
                    tbEan.Enabled = true;
                }
            }
        }

        private void cbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
            //    && isNewTovar.HasValue && !isNewTovar.Value)
            //{
            //    DataRow[] drSameGoods = Config.linkToCurrentRequest.getGoodsData().Select("id_tovar = " + id_tovar.ToString() + " AND id_subject <> " + goodParameters["id_subject"].ToString()/*cbSubject.SelectedValue.ToString()*/);
            //    int selectedSubj = (int)cbSubject.SelectedValue;

            //    if (drSameGoods.Count() > 0)
            //    {
            //        if (MessageBox.Show("В заявке уже есть этот товар с другой страной/субъектом РФ!\nЗаменить страну/субъект у текущего товара?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //        {
            //            cbSubject.SelectedValue = (int)drSameGoods[0]["id_subject"];
            //        }
            //        else
            //        {
            //            foreach (DataRow drSame in drSameGoods)
            //            {
            //                drSame["id_subject"] = selectedSubj;
            //            }
            //            Config.linkToCurrentRequest.getGoodsData().AcceptChanges();
            //        }
            //    }

            //    if (oldSubject != 0 && oldSubject != selectedSubj)
            //    {
            //        if (MessageBox.Show("Значение поля Страна/Субъект изменилось!\nСохранить новое значение в справочник?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            if (id_dep != 6)
            //                Config.hCntMain.ChangeSubject(selectedSubj, id_tovar);
            //            else
            //                Config.hCntAdd.ChangeSubject(selectedSubj, id_tovar);

            //            UpdateValue("spr_subject", selectedSubj);
            //            oldSubject = selectedSubj;

            //            //if (drSameGoods.Count() > 0)
            //            //{
            //            //    foreach (DataRow drSame in drSameGoods)
            //            //    {
            //            //        drSame["spr_subject"] = selectedSubj;
            //            //    }
            //            //    Config.linkToCurrentRequest.getGoodsData().AcceptChanges();
            //            //}
            //        }
            //    }
            //}
        }

        private void chkPlanRealizDAy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlanRealizDay.Checked)
            {
                chkOtsechDays.Checked = false;
                tbDayPlanReal.Enabled = true;
                if (tbDayPlanReal.Text.Trim().Length == 0)
                {
                    tbDayPlanReal.Text = "0,000";
                }
            }
            else
            {
                tbDayPlanReal.Enabled = false;
                // tbDayPlanReal.Text = "";
            }
        }

        private void chkOtsechDays_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtsechDays.Checked)
            {
                chkPlanRealizDay.Checked = false;
                dtpOtsechStart.Enabled =
                    dtpOtsechFinish.Enabled = true;
            }
            else
            {
                dtpOtsechStart.Enabled =
                                    dtpOtsechFinish.Enabled = false;
            }
        }

        private void chkCreateLocalCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCreateLocalCode.Checked)
            {
                if (MessageBox.Show("Сформировать местный код?", "Запрос на формирование кода", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int curEan = (id_dep != 6 ? Config.hCntMain.GetNewEAN(id_dep, newAddedEans)
                                              : Config.hCntAdd.GetNewEAN(id_dep, newAddedEans));
                    tbEan.Text = (curEan).ToString();
                }
                else
                {
                    chkCreateLocalCode.Checked = false;
                }
            }

        }

        private void chbIsTransparent_CheckedChanged(object sender, EventArgs e)
        {
            if (!load)
            {
                if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                    && isNewTovar.HasValue && !isNewTovar.Value)
                {
                    if (Spr_isTransparent != chbIsTransparent.Checked)
                    {
                        if (MessageBox.Show("Значение признака упаковки изменилось!\nСохранить новое значение в справочник?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (id_dep != 6)
                                Config.hCntMain.ChangeTransparent(id_tovar, chbIsTransparent.Checked);
                            else
                                Config.hCntAdd.ChangeTransparent(id_tovar, chbIsTransparent.Checked);

                            UpdateValue("spr_isTransparent", (chbIsTransparent.Checked ? 1 : 0));
                            Spr_isTransparent = chbIsTransparent.Checked;
                        }
                    }
                }
            }
        }

        #endregion

        #region Сохранение

        private void btSave_Click(object sender, EventArgs e)
        {
            if (checkSave())
            {
                SaveGood();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool checkSave()
        {
            if (tbZcena.Text.Trim().Length == 0 || decimal.Parse(tbZcena.Text) == 0)
            {
                MessageBox.Show("Некорректно заполнено поле \"Цена закупки\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (tbRcena.Text.Trim().Length == 0 || decimal.Parse(tbRcena.Text) == 0)
            {
                MessageBox.Show("Некорректно заполнено поле \"Цена продажи\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            //if ((decimal.Parse(tbRcena.Text) - decimal.Parse(tbZcena.Text)) / decimal.Parse(tbZcena.Text) * 100 > 999
            //    || (decimal.Parse(tbZcena.Text) - decimal.Parse(tbRcena.Text)) / decimal.Parse(tbRcena.Text) * 100 > 999)
            //{
            //    MessageBox.Show("Внимание! Разница между ценой закупки и ценой реализации составляет больше 999%!", "Предупреждение", MessageBoxButtons.OK);
            //    return false;
            //}

            if (cbTU.SelectedValue == null || (int)cbTU.SelectedValue == 0)
            {
                MessageBox.Show("Некорректно заполнено поле \"Т/У группа\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (cbInvGrp.SelectedValue == null || (int)cbInvGrp.SelectedValue == 0)
            {
                MessageBox.Show("Некорректно заполнено поле \"Инв. группа\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (cbSubject.SelectedValue == null || (int)cbSubject.SelectedValue == 0)
            {
                MessageBox.Show("Не выбрана страна или субъект РФ!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Заполните поле \"Наименование товара\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (tbZakaz.Text.Trim().Length == 0 || decimal.Parse(tbZakaz.Text) <= 0)
            {
                MessageBox.Show("Некорректно заполнено поле \"Заказ\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (cbUnit.SelectedIndex == -1)
            {
                MessageBox.Show("Некорректно заполнено поле \"Ед. изм. товара\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (cbNds.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните поле \"НДС\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if ((int)cbUnit.SelectedValue == 2 && tbEan.Text.Trim().Length < 5)
            {
                MessageBox.Show("Некорректно заполнено поле \"Код товара\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if ((idOperReq == 1) || (idOperReq == 3))
            {
                decimal z = 0, t = 0;
                decimal.TryParse(tbZatar.Text, out z);
                decimal.TryParse(tbTara.Text, out t);
                if (z == 0 || t == 0)
                {
                    MessageBox.Show("Не заполнены поля \nТара или Затарка", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            DataTable dt = new DataTable();
            dt = Config.hCntMain.GetProperties();
            DataRow[] dr;
            bool MustDeclareBrutto = false;

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                dr = dt.Select("id_val like '%brutto%'");

                if (dr.Count() > 0)
                {
                    MustDeclareBrutto = (dr[0]["val"].ToString() == "1") ? true : false;
                }
            }

            if ((tbBrutto.Visible) && (MustDeclareBrutto) && (decimal.Parse(tbBrutto.Text) == 0))
            {
                if (MessageBox.Show("У товара не указан вес брутто. Вес \nзаявки будет рассчитан не полностью. \nСохранить товар без брутто?", "Запрос подтверждения действий", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    tbBrutto.Focus();
                    return false;
                }
            }

            if (isWeInOut)
            {
                object sumTovar = 0;
                if (Config.dtSelectedTovar != null && Config.dtSelectedTovar.Rows.Count > 0)
                    sumTovar = Config.dtSelectedTovar.Compute("SUM(Netto)", "id_tovar = " + id_tovar);

                if (sumTovar == DBNull.Value)
                    sumTovar = 0;

                if (decimal.Parse(tbZakaz.Text) != decimal.Parse(sumTovar.ToString()))
                {
                    MessageBox.Show("Нельзя сохранить данную заявку, пока не набрано\nколичество товаров на форме \"Выбор товара для\nзаявки\", равное количеству товаров в заявке.", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return true;
        }

        private void SaveGood()
        {
            if (checkSertificate && tbEan.Text.Trim().Length == 4)
            {
                DataTable dtSertif = (id_dep == 6 ? Config.hCntAdd : Config.hCntMain).CheckSertificate(id_tovar, Convert.ToInt32(cbSubject.SelectedValue));
                if (!(dtSertif != null && dtSertif.Rows.Count > 0 && Convert.ToDateTime(dtSertif.Rows[0]["date_end"]) > DateTime.Today))
                {
                    string message = "У введённого товара отсутствует действующий сертификат/декларация о соответствии по товару и стране производства.";
                    if (dtSertif != null && dtSertif.Rows.Count > 0)
                    {
                        message += "\n Последний действующий номер сертификата: \n № " + dtSertif.Rows[0]["name"].ToString() + "\n Дата окончания действия сертификата: " + Convert.ToDateTime(dtSertif.Rows[0]["date_end"]).ToShortDateString();
                    }
                    MessageBox.Show(message, "Добавление товара", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && isNewTovar.HasValue && !isNewTovar.Value)
            {
                int selectedSubj = (int)cbSubject.SelectedValue;
                UpdateValue("id_subject", selectedSubj);
                DataRow[] drSameGoods = Config.linkToCurrentRequest.getGoodsData().Select("id_tovar = " + id_tovar.ToString() + " AND id_subject <> " + selectedSubj.ToString());

                if (drSameGoods.Count() > 0)
                {
                    if (MessageBox.Show("В заявке уже есть этот товар с другой страной/субъектом РФ!\nЗаменить страну/субъект у текущего товара?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        cbSubject.SelectedValue = (int)drSameGoods[0]["id_subject"];
                    }
                    else
                    {
                        foreach (DataRow drSame in drSameGoods)
                        {
                            drSame["id_subject"] = selectedSubj;
                        }
                        Config.linkToCurrentRequest.getGoodsData().AcceptChanges();
                    }
                }
            }
            UpdateValue("ean", tbEan.Text.Trim());
            UpdateValue("cname", tbName.Text.Trim());
            UpdateValue("zcena", decimal.Parse(tbZcena.Text));
            UpdateValue("rcena", decimal.Parse(tbRcena.Text));
            UpdateValue("zcenabnds", decimal.Parse(tbZcenabnds.Text));
            UpdateValue("procent", decimal.Round((decimal.Parse(tbRcena.Text) - decimal.Parse(tbZcena.Text)) / decimal.Parse(tbZcena.Text) * 100, 0));
            UpdateValue("Tara", int.Parse(tbTara.Text));
            UpdateValue("zakaz", decimal.Parse(tbZakaz.Text));
            UpdateValue("sum", decimal.Parse(tbZakaz.Text) * decimal.Parse(tbZcena.Text));
            UpdateValue("cprimech", tbPrimech.Text.Trim());
            UpdateValue("CauseOfDecline", "");
            UpdateValue("nprimech", (!isNewTovar.HasValue || isNewTovar.Value) ? 1 : (oldRcena != (decimal)goodParameters["rcena"] ? 2 : 0));
            UpdateValue("id_tovar", id_tovar);
            UpdateValue("PeriodOfStorage", tbPeriodOfStorage.Text);
            UpdateValue("id_nds", (int)cbNds.SelectedValue);
            UpdateValue("Zatar", decimal.Parse(tbZatar.Text));
            //-------------------------------
            UpdateValue("PlanRealiz", (tbDayPlanReal.Text.Length == 0 || !chkPlanRealizDay.Checked || decimal.Parse(tbDayPlanReal.Text) == 0 ? -1 : decimal.Parse(tbDayPlanReal.Text)));
            UpdateValue("BeginOfPeriod", chkOtsechDays.Checked ? dtpOtsechStart.Value.ToString("dd.MM.yyyy") : "");
            UpdateValue("EndOfPeriod", chkOtsechDays.Checked ? dtpOtsechFinish.Value.ToString("dd.MM.yyyy") : "");

            UpdateValue("TekOst", tbOst.Text.Length == 0 ? 0 : decimal.Parse(tbOst.Text));
            UpdateValue("id_grp1", (int)cbTU.SelectedValue);
            UpdateValue("id_grp2", (int)cbInvGrp.SelectedValue);
            UpdateValue("id_unit", (int)cbUnit.SelectedValue);
            UpdateValue("id_subject", (int)cbSubject.SelectedValue);

            UpdateValue("limitType", 0);
            UpdateValue("zapas2", (tbZapas2.Text.Trim().Length == 0 ? 0 : int.Parse(tbZapas2.Text)));
            UpdateValue("ShelfSpace", (tbShelfSpace.Text.Trim().Length == 0 ? 0 : decimal.Parse(tbShelfSpace.Text)));
            UpdateValue("brutto", decimal.Parse(tbBrutto.Text));
            UpdateValue("isTransparent", (chbIsTransparent.Checked ? 1 : 0));
            
            

            if (mode == 0)
            {
                UpdateValue("idReq", 0);
                UpdateValue("zapas", 0);
                //UpdateValue("zapas2", 0);
                UpdateValue("CauseOfDeclineCredit", "");
                UpdateValue("id_CreditStatus", 5);
                UpdateValue("AvgReal", 0.00M);
                UpdateValue("ZakazP", 0.00M);
                UpdateValue("ZakazNP", 0.00M);
                UpdateValue("porthole", 0);
                UpdateValue("npp", 0);
                UpdateValue("spr_subject", 0);
                UpdateValue("spr_zatar", 0);
                UpdateValue("isProh", false);
                UpdateValue("spr_isTransparent", (chbIsTransparent.Checked ? 1 : 0));
            }
            //-----------
        }

        private void UpdateValue(string columnName, object newValue)
        {
            if (goodParameters != null)
            {
                goodParameters[columnName] = newValue;
            }
        }

        #endregion

        #region Расчет 

        private void calculZakaz(bool fromZapas)
        {
            decimal Zakaz = 0;
            
            bool standartCalc = true;
            bool ChangeRequestOnTarZatarChanging = false;

            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && isNewTovar.HasValue)
            {
                DataRow[] drStock = Config.dtProperties.Select("id_val = 'using stock'");
                if (drStock.Count() > 0
                    && int.Parse(drStock[0]["val"].ToString()) == 1
                    && goodParameters != null
                    && fromZapas)
                {
                    standartCalc = false;
                }
            }
            
            ChangeRequestOnTarZatarChanging = Config.hCntMain.GetProperties("change zakaz");
                        
            if (standartCalc)
            {
                if (ChangeRequestOnTarZatarChanging)
                {
                    Zakaz = (decimal.Parse(tbZatar.Text) * int.Parse(tbTara.Text));
                    tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));
                }
            }
            else
            {
                if (ChangeRequestOnTarZatarChanging)
                {
                    decimal avgReal, zakazP, ost;
                    int zapas;
                    if (decimal.TryParse(goodParameters["AvgReal"].ToString(), out avgReal)
                        && decimal.TryParse(goodParameters["ZakazP"].ToString(), out zakazP)
                        && int.TryParse(tbZapas.Text.Trim(), out zapas)
                        && decimal.TryParse(tbOst.Text.Trim(), out ost))
                    {
                        Zakaz = zapas * avgReal
                            - zakazP - ost;
                    }
                    else
                    {
                        Zakaz = 0;
                    }
                    tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));

                    decimal taratara = (decimal.Parse(tbZatar.Text) == 0) ? 0 : Zakaz / decimal.Parse(tbZatar.Text);
                    tbTara.Text = Math.Round(taratara, 0).ToString("#0"); ;
                }
                else
                {
                    decimal avgReal, zakazP, ost;
                    int zapas;
                    if (decimal.TryParse(goodParameters["AvgReal"].ToString(), out avgReal)
                        && decimal.TryParse(goodParameters["ZakazP"].ToString(), out zakazP)
                        && int.TryParse(tbZapas.Text.Trim(), out zapas)
                        && decimal.TryParse(tbOst.Text.Trim(), out ost))
                    {
                        Zakaz = zapas * avgReal
                            - zakazP - ost;
                    }
                    else
                    {
                        Zakaz = 0;
                    }
                    tbZakaz.Text = Zakaz.ToString((cbUnit.SelectedIndex != -1 && (int)cbUnit.SelectedValue == 2 ? "#0" : "#0.000"));
                }
            }

            calculZapas2(true);
        }

        private void CalculZcena(bool calcBnds)
        {
            if (calcBnds)
            {
                decimal Zcena;
                if (decimal.TryParse(tbZcena.Text, out Zcena))
                {
                    tbZcenabnds.Text = ((Zcena * 100) / (100 + int.Parse(cbNds.Text))).ToString("#0.0000");
                }
                else
                {
                    tbZcenabnds.Text = "0,0000";
                }
            }
            else
            {
                decimal Zcenabnds;
                if (decimal.TryParse(tbZcenabnds.Text, out Zcenabnds))
                {
                    tbZcena.Text = (Zcenabnds * (100 + int.Parse(cbNds.Text)) / 100).ToString("#0.0000");
                }
                else
                {
                    tbZcena.Text = "0,0000";
                }
                calculPercent();
            }

        }

        private void calculZapas2(bool fromZakaz)
        {
            if (fromZakaz)
            {
                if (goodParameters != null && goodParameters["id_tovar"] != System.DBNull.Value)
                {
                    decimal AvgReal = decimal.Parse(goodParameters["AvgReal"].ToString());

                    if (AvgReal == 0)
                    {
                        AvgReal = 1;
                    }
                    tbZapas2.Text = decimal.ToInt32((decimal.Parse(goodParameters["zapas2"].ToString()) - (decimal.Parse(goodParameters["zakaz"].ToString()) / AvgReal) + (decimal.Parse(tbZakaz.Text) / AvgReal))).ToString();
                }
            }
            else
            {
                tbZapas2.Text = (id_dep != 6 ? Config.hCntMain.GetZapas2(id_tovar, (chkPlanRealizDay.Checked ? decimal.Parse(tbDayPlanReal.Text.ToString()) : 0), dtpOtsechStart.Value.Date, dtpOtsechFinish.Value.Date)
                                            : Config.hCntAdd.GetZapas2(id_tovar, (chkPlanRealizDay.Checked ? decimal.Parse(tbDayPlanReal.Text.ToString()) : 0), dtpOtsechStart.Value.Date, dtpOtsechFinish.Value.Date)).ToString();
            }
        }
        
        private void calculPercent()
        {
            decimal z = decimal.Parse(tbZcena.Text);
            decimal r = decimal.Parse(tbRcena.Text);
            decimal p = 100;

            if (z != 0)
            {
                p = (r - z) / z * 100;
            }

            tbPercent.Text = p.ToString("#0");
        }

        private bool ValidateZatar()
        {
            decimal d;
            if (!decimal.TryParse(tbZatar.Text, out d) && tbZatar.Text.Length != 0)
            {
                return true;
            }
            else
            {
                string format = "#0";
                if ((int)cbUnit.SelectedValue == 1)
                {
                    format = "#0.000";

                    if (d >= 10000)
                    {
                        d = 9999.999m;
                    }
                }
                else
                {
                    if (d >= 10000)
                    {
                        d = 9999m;
                    }
                }
                tbZatar.Text = d.ToString(format);
            }

            return false;
        }

        private bool ValidateZakaz()
        {
            decimal d;
            if (!decimal.TryParse(tbZakaz.Text, out d) && tbZakaz.Text.Length != 0)
            {
                return true;
            }
            else
            {
                string format = "";
                if ((int)cbUnit.SelectedValue == 1)
                {
                    format = "#0.000";

                    if (d >= 1000000)
                    {
                        d = 999999.999m;
                    }
                }
                tbZakaz.Text = d.ToString(format);
            }
           // if (decimal.TryParse(tbZakaz.Text, out d) == 0) MessageBox.Show("dfghfdg");
            return false;
        }

        #endregion

        #region Прочее

        private void btReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbEan_MouseClick(object sender, MouseEventArgs e)
        {
            ///while(isNewTovar == null)
            if (isNewTovar == null && !isWeInOut)
            {
                if (MessageBox.Show("Вы хотите указать существующий код товара?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    isNewTovar = false;
                }
                else
                {
                    //if (reqIdOperand != 1 && reqIdOperand != 3)
                    //{
                    //    MessageBox.Show("Новый товар можно добавить\n" + "только в заявку с типом\n".PadLeft(30) + "\"Приход\" или \"Бонус\"".PadLeft(30), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //}
                    //else
                    //{
                    isNewTovar = true;
                    ///}
                }

                tbEan.Text = "";
            }
            else if (isNewTovar == null && isWeInOut)
            {
                isNewTovar = false;
                tbEan.Text = "";
            }
            SetControlsVisible();
        }

        private void SelectAllOnEnterTextbox(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                ((TextBox)sender).SelectAll();
            });
        }

        private bool AllowCharInputDecimal(TextBox tb, int intPlaces, int decPlaces, char inputChar)
        {
            return (!"0123456789,\b".Contains(inputChar)
                        ||
                        ((inputChar == ',' && (tb.Text.Contains(',') || tb.Text.Length == 0 || tb.Text.Length > intPlaces))
                        || (!",\b".Contains(inputChar) && (((tb.Text.IndexOf(',') == -1 ? tb.Text.Length : tb.Text.IndexOf(',')) == intPlaces && ((tb.SelectionStart + tb.SelectionLength) < (tb.Text.IndexOf(',') == -1 ? tb.MaxLength + 1 : tb.Text.IndexOf(','))))
                                                       || (tb.Text.IndexOf(',') != -1 && (tb.Text.Length - tb.Text.IndexOf(',')) == decPlaces + 1 && ((tb.SelectionStart + tb.SelectionLength) > tb.Text.IndexOf(',')))))
                            )
                            && tb.SelectionLength == 0 /*!= tb.Text.Length*/);
        }

        private void GoToNextControl(char inputChar)
        {
            if (inputChar == (char)13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void tbZatar_Leave(object sender, EventArgs e)
        {
            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && isNewTovar.HasValue && !isNewTovar.Value)
            {
                if (/*oldZatar != 0 &&*/ oldZatar != decimal.Parse(tbZatar.Text))
                {
                    if (MessageBox.Show("Значение затарки изменилось!\nСохранить новое значение в справочник?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (id_dep != 6)
                            Config.hCntMain.ChangeZatar(id_tovar, decimal.Parse(tbZatar.Text));
                        else
                            Config.hCntAdd.ChangeZatar(id_tovar, decimal.Parse(tbZatar.Text));

                        UpdateValue("spr_zatar", decimal.Parse(tbZatar.Text));
                        oldZatar = decimal.Parse(tbZatar.Text);
                    }
                }
            }

            
        }

        private bool ChexInputValue(TextBox tb, int intPlaces, int decimalPlaces)
        {
            decimal d;
            bool retValue = false; ;
            if (!decimal.TryParse(tb.Text, out d) && tb.Text.Length != 0)
            {
                retValue = true;
            }
            else
            {
                if (Double.Parse(d.ToString()) > Math.Pow(10, intPlaces))
                    d = decimal.Parse("".PadLeft(intPlaces, '9') + ',' + "".PadLeft(decimalPlaces, '9'));

                tb.Text = d.ToString("#0." + "".PadLeft(decimalPlaces, '0'));
            }

            return retValue;
        }

        #endregion

        private void tbZatar_TextChanged(object sender, EventArgs e)
        {

        }

        //NEW 27.07.2017

        private bool isWeInOut = false;
        private DateTime dateRequest;
        private int idTReq;
        public void setWeInOut(bool _value, DateTime _dateRequest, int idTReq)
        {
            this.isWeInOut = _value;
            this.dateRequest = _dateRequest;
            btSelectTovar.Visible = isWeInOut;
            this.idTReq = idTReq;
        }

        private void tbZakaz_Leave(object sender, EventArgs e)
        {
            if (isWeInOut)
            {
                if (MessageBox.Show("Вы хотите открыть форму\n\"Выбор товара для заявки\"?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sWeInOut.frmSelectTovar frm = new sWeInOut.frmSelectTovar();
                    frm.setDataToForm(id_tovar, tbEan.Text.Trim(), tbName.Text.Trim(), tbZakaz.Text.Trim().Length == 0 ? "0" : tbZakaz.Text.Trim(), idTReq, id_dep);
                    frm.ShowDialog();
                }
            }
        }

        private void btSelectTovar_Click(object sender, EventArgs e)
        {
            sWeInOut.frmSelectTovar frm = new sWeInOut.frmSelectTovar();
            frm.setDataToForm(id_tovar, tbEan.Text.Trim(), tbName.Text.Trim(), tbZakaz.Text.Trim().Length == 0 ? "0" : tbZakaz.Text.Trim(), idTReq, id_dep);
            frm.ShowDialog();
        }
    }
}
