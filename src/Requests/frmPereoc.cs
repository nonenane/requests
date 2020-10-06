using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Collections;
using Nwuram.Framework.ToExcel;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.User;

namespace Requests
{
    public partial class frmPereoc : Form
    {
        DataTable dtPereocGoods;
        BindingSource bsPereoc = new BindingSource();

        DataTable oldPereocGoods = null;
        
        /// <summary>
        /// режим 0 - просмотр, 1  - редактирование, 2 - создание, 3 - копирование
        /// </summary>
        int mode = 2; 
        int idPereoc;
        int idDep;
        DataTable dtPereocReport;
        bool isPereoc = false;
        bool isSaving = false;
        bool hasDeletedRows = false;
        DateTime datePereoc;

        System.Timers.Timer autoSaveTimer;
        bool autoSave = false;
        int autoSaveTreqId = 0;
        bool restoreAutoSaved = false;
        ArrayList autoSaveDeletedIdReq = new ArrayList();

        /// <summary>
        /// Режим создания переоценки
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="id"></param>
        /// <param name="idDep"></param>
        public frmPereoc(int mode, int id, int idDep)
        {
            InitializeComponent();
            this.mode = mode;
            this.idPereoc = id;
            this.idDep = idDep;
        }

        public frmPereoc(int mode, int id, int idDep, DateTime datePereoc)
        {
            InitializeComponent();
            this.mode = mode;
            this.idPereoc = id;
            this.idDep = idDep;
            this.datePereoc = datePereoc;
        }

        public frmPereoc(int mode, int id, int idDep, DateTime datePereoc, bool restoreAutoSaved, int autoSaveTreqId)
        {
            InitializeComponent();
            this.mode = mode;
            this.idPereoc = id;
            this.idDep = idDep;
            this.datePereoc = datePereoc;
            this.restoreAutoSaved = restoreAutoSaved;
            this.autoSaveTreqId = autoSaveTreqId;
        }

        private void frmPereoc_Load(object sender, EventArgs e)
        {
            procLoad();
        }

        private void procLoad()
        {
            if (Config.hCntMain.NedoUser())
            {
                zcena.Visible = false;
            }

            if (mode == 2)
            {
                btExcel.Enabled = false;
                btPrint.Enabled = false;
                dtPereocGoods = Config.dtPereocGoods;
                if (restoreAutoSaved)
                {
                    dtPereocGoods = (idDep != 6 ? Config.hCntMain : Config.hCntAdd).GetAutoPereocBody(autoSaveTreqId);
                    restoreAutoSaved = false;
                }
                else
                {
                    SetPereocSource();
                }
            }
            else if (mode == 3)
            {
                btExcel.Enabled = false;
                btPrint.Enabled = false;
                CopyExistPereoc();
            }
            else
            {
                btExcel.Enabled = true;
                btPrint.Enabled = true;
                GetExistsPereoc();
            }

            if (mode == 1 || mode == 2 || mode == 3)
            {
                Config.curPereocFrmName = this.Name;
                Config.editPereocForms.Add(this);
            }

            gbTypes.Enabled = (mode != 0);
            newcena.ReadOnly = (mode == 0);

            if (mode == 2 && (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН"))
            {
                autoSave = idDep != 6 ? Config.hCntMain.GetProperties("autosave") : Config.hCntAdd.GetProperties("autosave");
                if (autoSave)
                {
                    autoSaveTimer = new System.Timers.Timer();
                    // Tell the timer what to do when it elapses
                    autoSaveTimer.Elapsed += new ElapsedEventHandler(AutoSaveRequest);
                    // Set it to go off every five seconds
                    DataTable autoSaveTime = idDep != 6 ? Config.hCntMain.GetProperties() : Config.hCntAdd.GetProperties();
                    DataRow[] autoSaveTimeRow = autoSaveTime.Select("id_val = 'autosavetime'");
                    if (autoSaveTimeRow.Length > 0)
                    {
                        //autoSaveTimer.Interval = 5000;
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
        }

        public void AutoSaveRequest(object sender, EventArgs e)
        {
            int ntypeorg = -1;
            string cprimech = "";
            int id_operand = -1;
            int porthole = -1;
            int id_typeBonus = -1;
            DateTime dateRequest = DateTime.Now;
            DoOnUIThread(delegate()
            {
                ntypeorg = 0;
                cprimech = "";
                id_operand = 4;
                porthole = 0;
                id_typeBonus = 0;
                dateRequest = DateTime.Now.Date;
            });
            autoSaveTreqId = idDep != 6 ? Config.hCntMain.SetAutoRequestHead(autoSaveTreqId, dateRequest, 0, ntypeorg, cprimech, id_operand, porthole, id_typeBonus, Config.GetStringFromArray(autoSaveDeletedIdReq))
                                         : Config.hCntAdd.SetAutoRequestHead(autoSaveTreqId, dateRequest, 0, ntypeorg, cprimech, id_operand, porthole, id_typeBonus, Config.GetStringFromArray(autoSaveDeletedIdReq));
            autoSaveDeletedIdReq.Clear();
            AutoSaveRequestBody(sender, e);
        }

        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }

        private void AutoSaveRequestBody(object sender, EventArgs e)
        {
            if (autoSaveTreqId == 0)
            {
                AutoSaveRequest(sender, e);
            }

            DataView dt = bsPereoc.DataSource as DataView;
            foreach (DataRow goodRow in dt.Table.Rows)
            {
                (idDep != 6 ? Config.hCntMain : Config.hCntAdd).SetAutoRequestBody(0, autoSaveTreqId, Convert.ToInt32(goodRow["id_tovar"]), 1, 0, Convert.ToDecimal(goodRow["ostOnDate"]), Convert.ToDecimal(goodRow["zcena"]), Convert.ToDecimal(goodRow["rcena"]), Convert.ToDecimal(goodRow["zcenabnds"]), 0, "", 0, "", false, 0);
            }
        }

        private void CopyExistPereoc()
        {
            dtPereocGoods = (idDep != 6 ? Config.hCntMain.CopyPereoc(idPereoc) : Config.hCntAdd.CopyPereoc(idPereoc));
            LogCopyPereoc();
            if (mode == 3)
            {
                Config.dtPereocGoods = dtPereocGoods;
            }
            if (dtPereocGoods.Rows.Count > 0)
            {
                isPereoc = rbPereoc.Checked = (int)dtPereocGoods.Rows[0]["cprimech"] == 1;
                rbDooc.Checked = (int)dtPereocGoods.Rows[0]["cprimech"] == 0;
            }
            if (mode == 3)
            {
                idPereoc = 0;
            }
            SetPereocSource();
            //SetButtonsEnabled();
        }

        private void LogCopyPereoc()
        {
            Logging.StartFirstLevel(960);
            Logging.Comment("Начало копирования акта переоценки");

            Logging.Comment("id акта переоценки = " + idPereoc.ToString() + ", дата переоценки = " + datePereoc.ToShortDateString());
            Logging.Comment("id отдела = " + idDep.ToString());

            foreach (DataRow row in dtPereocGoods.Rows)
            {
                Logging.Comment("id_tovar = " + row["id_tovar"].ToString() + ", ean = '" + row["ean"].ToString() + "', название = " + row["cName"].ToString() + ", остаток = " + row["ostOnDate"].ToString() + ", цена продажи = " + row["rcena"].ToString() + ", цена закупки = " + row["zcena"].ToString());
            }

            Logging.Comment("Конец копирования акта переоценки");
            Logging.StopFirstLevel();
        }

        private void GetExistsPereoc()
        {
            dtPereocGoods = (idDep != 6 ? Config.hCntMain.GetPereoc(idPereoc) : Config.hCntAdd.GetPereoc(idPereoc));
            if (mode == 1)
            {
                Config.dtPereocGoods = dtPereocGoods;
            }
            if (dtPereocGoods.Rows.Count > 0)
            {
                isPereoc = rbPereoc.Checked = (int)dtPereocGoods.Rows[0]["cprimech"] == 1;
                rbDooc.Checked = (int)dtPereocGoods.Rows[0]["cprimech"] == 0;
            }
            SetPereocSource();
            SetButtonsEnabled();
        }

        private void SetPereocSource()
        {
            AutoSaveChanges();
            bsPereoc.DataSource = dtPereocGoods.DefaultView;
            grdPereoc.AutoGenerateColumns = false;
            grdPereoc.DataSource = bsPereoc;
        }

        private void AutoSaveChanges()
        {
            if (oldPereocGoods != null)
            {
                IEnumerable<int> diff = oldPereocGoods.AsEnumerable().Except(dtPereocGoods.DefaultView.ToTable().AsEnumerable(), new GoodsRowComparer()).Select(r => Convert.ToInt32(r["id_tovar"]));
                foreach (int id_tovar in diff)
                {
                    if (!autoSaveDeletedIdReq.Contains(id_tovar))
                    {
                        autoSaveDeletedIdReq.Add(id_tovar);
                    }
                }
                oldPereocGoods = null;
            }
        }

        private void SetButtonsEnabled()
        {
            if (mode == 0)
            {
                btNewPereoc.Visible =
                        btDelPereoc.Visible =
                        btSave.Visible = false;
            }
            else
            {
                btDelPereoc.Enabled = (grdPereoc.Rows.Count > 0);
                btSave.Enabled = (dtPereocGoods.DefaultView.Count > 0 && dtPereocGoods.DefaultView.ToTable().Select("zcenabnds <> 0").Count() > 0);
                btExcel.Enabled =
                        btPrint.Enabled =
                                (dtPereocGoods.DefaultView.Count > 0
                                 && dtPereocGoods.DefaultView.ToTable().Select("zcenabnds <= 0").Count() != dtPereocGoods.DefaultView.Count);
                                 //&& (mode != 2));
            }
        }

        private void grdPereoc_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            SetButtonsEnabled();
        }

        private void grdPereoc_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            SetButtonsEnabled();
        }

        private void btDelPereoc_Click(object sender, EventArgs e)
        {
            DelGood();
        }

        private void DelGood()
        {
            DataRow curRow = dtPereocGoods.DefaultView.ToTable().Rows[grdPereoc.CurrentRow.Index];
            if (MessageBox.Show("Вы уверены, что хотите удалить этот товар из переоценок?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id_tovar = Convert.ToInt32(curRow["id_tovar"]);
                if (!autoSaveDeletedIdReq.Contains(id_tovar))
                {
                    autoSaveDeletedIdReq.Add(id_tovar);
                }

                hasDeletedRows = (hasDeletedRows || (mode == 1 && dtPereocGoods.DefaultView.ToTable().Rows[grdPereoc.CurrentRow.Index]["zbndsOld"] != DBNull.Value));
                dtPereocGoods.DefaultView.Delete(grdPereoc.CurrentRow.Index);
                dtPereocGoods.AcceptChanges();
                grdPereoc.Refresh();
            }
        }

        private bool isValuesChanged()
        {
            bool retValue = false;

            if (dtPereocGoods.Columns.IndexOf("zbndsOld") == -1
                    || dtPereocGoods.DefaultView.ToTable().Select("zcenabnds <> zbndsOld OR (zbndsOld is null)").Count() > 0
                    || isPereoc != rbPereoc.Checked
                    || hasDeletedRows)
            {
                retValue = true;
            }
            return retValue;
        }

        private void frmPereoc_Activated(object sender, EventArgs e)
        {
            SetPereocSource();
        }

        private void btNewPereoc_Click(object sender, EventArgs e)
        {
            if (bsPereoc.DataSource != null)
            {
                oldPereocGoods = (bsPereoc.DataSource as DataView).ToTable().Copy();
            }

            SetPereocFormValues();
            ((Main)this.MdiParent).SetTab("Список товаров", "frmGoods", null, null);
        }

        public void SetPereocFormValues()
        {
            Config.curPereocFrmName = this.Name;
            Config.dtPereocGoods = dtPereocGoods;
        }

        private void frmPereoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool showReqList = true;
            if (!isSaving && (mode != 0 && isValuesChanged() && MessageBox.Show("Закрыть форму без\nсохранения данных?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
            {
                e.Cancel = true;
                this.DialogResult = DialogResult.No;
                return;
            }

            grdPereoc.DataSource = null;
            dtPereocGoods.Rows.Clear();

            if (mode != 0)
            {
                Config.editPereocForms.Remove(this);

                if (Config.editPereocForms.Count > 0)
                {
                    ((frmPereoc)Config.editPereocForms[0]).SetPereocFormValues();
                    ((Main)this.MdiParent).SetTab("", ((frmPereoc)Config.editPereocForms[0]).Name, null, null);
                    showReqList = false;
                }
                else
                {
                    if (mode == 2 && autoSave)
                    {
                        autoSaveTimer.Stop();
                        if (idDep != 6)
                        {
                            Config.hCntMain.ClearAutoSaveTables(autoSaveTreqId);
                        }
                        else
                        {
                            Config.hCntAdd.ClearAutoSaveTables(autoSaveTreqId);
                        }
                    }
                    
                    //if (Config.dtPereocGoods != null
                    //    && Config.dtPereocGoods.Rows.Count > 0
                    //    && mode != 0)
                    //{
                    //Config.dtPereocGoods.Rows.Clear();
                    Config.dtPereocGoods = null;
                    Config.curPereocFrmName = null;
                    //}
                }
            }

            if (showReqList && ((Main)this.MdiParent).isFormOpened("frmRequests"))
                ((Main)this.MdiParent).SetTab("", "frmRequests", null, null);

            this.DialogResult = DialogResult.None;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            savePereoc();
        }

        private void savePereoc()
        {
            bool saveAsNewGood = false;
            Config.curDate = Config.hCntMain.GetCurDate(false);
            int idNewPereoc = -1;
            if (dtPereocGoods.DefaultView.ToTable().Select("zcenabnds <= 0").Count() != 0
                    && MessageBox.Show("Не во всех строках заполнено поле новой цены! Такие строки сохраняться не будут! Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Config.DatePereocSelected = false;

            if (mode != 1)
            {
                frmDatePereoc frmDate = new frmDatePereoc();
                frmDate.ShowDialog();
            }
            else
            {
                frmDatePereoc frmDate = new frmDatePereoc(datePereoc);
                frmDate.ShowDialog();
            }

            if (!Config.DatePereocSelected)
            {
                return;
            }

            int[] id_Deps = dtPereocGoods.DefaultView.ToTable().Select("zcenabnds > 0").Select(r => int.Parse(r["id_dep"].ToString())).ToArray();
            id_Deps = id_Deps.Distinct().ToArray();

            foreach (int id in id_Deps)
            {
                DataTable dtTemp = new DataTable();
                dtTemp = dtPereocGoods.Select("id_dep = " + id.ToString() + " AND zcenabnds > 0").CopyToDataTable();
                dtTemp.DefaultView.Sort = "ean asc";
                dtTemp = dtTemp.DefaultView.ToTable(true);

                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    if (id != 6)
                    {
                        idNewPereoc = Config.hCntMain.SetPereocHead((idDep == id ? idPereoc : 0), id, Config.DatePereoc.Date, (rbPereoc.Checked ? "1" : "0"));
                    }
                    else
                    {
                        idNewPereoc = Config.hCntAdd.SetPereocHead((idDep == id ? idPereoc : 0), id, Config.DatePereoc.Date, (rbPereoc.Checked ? "1" : "0"));
                    }

                    if (idNewPereoc == -1)
                    {
                        MessageBox.Show("Нет действующего ЮЛ!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    DataTable dtDeps = Config.hCntMain.GetDeps();
                    if (idPereoc == 0 || idDep != id)
                    {
                        Logging.StartFirstLevel(18);
                        Logging.Comment("Добавление нового акта переоценки");
                        Logging.Comment("Номер: " + idNewPereoc.ToString() + ", Тип: " + (rbPereoc.Checked ? "Переоценка" : "Дооценка"));
                        Logging.Comment("Дата создания: " + Config.curDate.ToString("dd.MM.yyyy") + ", Отдел: " + dtDeps.Select("id = " + id.ToString())[0]["name"].ToString() + ", Номер отдела: " + id.ToString());
                    }
                    else
                    {
                        if (isValuesChanged())
                        {
                            Logging.StartFirstLevel(27);
                            Logging.Comment("Редактирование акта переоценки");
                            Logging.Comment("Номер: " + idNewPereoc.ToString());
                            Logging.Comment("Дата создания: " + datePereoc.ToString() + ", Отдел: " + dtDeps.Select("id = " + id.ToString())[0]["name"].ToString() + ", Номер отдела: " + id.ToString());
                            Logging.VariableChange("Тип", (rbPereoc.Checked ? "Переоценка" : "Дооценка"), (isPereoc ? "Переоценка" : "Дооценка"));
                        }
                    }

                    for (int i = 0; dtTemp.Rows.Count > i; i++)
                    {
                        if (id != 6)
                        {
                            Config.hCntMain.SetPereocBody(idNewPereoc, (int)dtTemp.Rows[i]["idReq"], (int)dtTemp.Rows[i]["id_tovar"], (decimal)dtTemp.Rows[i]["ostOnDate"],
                                    ((dtTemp.Rows[i]["zcena"].ToString().Length == 0) ? 0 : (decimal)dtTemp.Rows[i]["zcena"]),
                                    (decimal)dtTemp.Rows[i]["rcena"], (decimal)dtTemp.Rows[i]["zcenabnds"]);

                        }
                        else
                        {
                            Config.hCntAdd.SetPereocBody(idNewPereoc, (int)dtTemp.Rows[i]["idReq"], (int)dtTemp.Rows[i]["id_tovar"], (decimal)dtTemp.Rows[i]["ostOnDate"],
                                    ((dtTemp.Rows[i]["zcena"].ToString().Length == 0) ? 0 : (decimal)dtTemp.Rows[i]["zcena"]),
                                    (decimal)dtTemp.Rows[i]["rcena"], (decimal)dtTemp.Rows[i]["zcenabnds"]);
                        }


                        if (idPereoc != 0 && idDep == id)
                        {
                            if (dtTemp.Rows[i]["npp"].GetType() != typeof(DBNull))
                            {
                                if (decimal.Parse(dtTemp.Rows[i]["zcenabnds"].ToString()) != decimal.Parse(dtTemp.Rows[i]["zbndsOld"].ToString()))
                                {
                                    Logging.Comment("Отредактированный  товар");
                                    Logging.Comment("ID товара: " + dtTemp.Rows[i]["id_tovar"].ToString() + ", EAN: " + dtTemp.Rows[i]["ean"].ToString().Trim() + ", Наименование товара: " + dtTemp.Rows[i]["cName"].ToString().Trim());
                                    Logging.Comment("Остаток: " + dtTemp.Rows[i]["ostOnDate"].ToString() + ", Цена продажи: " + dtTemp.Rows[i]["rcena"].ToString() + ", Цена закупки: " + dtTemp.Rows[i]["zcena"].ToString());
                                    Logging.VariableChange("Новая цена", dtTemp.Rows[i]["zcenabnds"].ToString(), dtTemp.Rows[i]["zbndsOld"].ToString());
                                }
                            }
                            else
                            {
                                Logging.Comment("Добавленный в акт товар");
                                saveAsNewGood = true;
                            }
                        }

                        if (idPereoc == 0 || saveAsNewGood || idDep != id)
                        {
                            Logging.Comment("ID товара: " + dtTemp.Rows[i]["id_tovar"].ToString() + ", EAN: " + dtTemp.Rows[i]["ean"].ToString().Trim() + ", Наименование товара: " + dtTemp.Rows[i]["cName"].ToString().Trim());
                            Logging.Comment("Остаток: " + dtTemp.Rows[i]["ostOnDate"].ToString() + ", Цена продажи: " + dtTemp.Rows[i]["rcena"].ToString() + ", Цена закупки: " + dtTemp.Rows[i]["zcena"].ToString());
                            Logging.Comment("Новая цена: " + dtTemp.Rows[i]["zcenabnds"].ToString());
                            saveAsNewGood = false;
                        }
                    }

                    if (id != 6)
                    {
                        Config.hCntMain.DelPereocBody(idNewPereoc, Config.GetStringFromDT(dtTemp, "id_tovar"));
                    }
                    else
                    {
                        Config.hCntAdd.DelPereocBody(idNewPereoc, Config.GetStringFromDT(dtTemp, "id_tovar"));
                    }

                    if (idPereoc == 0)
                    {
                        Logging.Comment("Завершение операции \"Добавление нового акта переоценки\"");
                    }
                    else
                    {
                        Logging.Comment("Завершение операции \"Редактирование акта переоценки\"");
                    }
                    Logging.StopFirstLevel();
                }
            }


            if (mode == 2 && autoSave)
            {
                if (idDep != 6)
                {
                    Config.hCntMain.ClearAutoSaveTables(autoSaveTreqId);
                }
                else
                {
                    Config.hCntAdd.ClearAutoSaveTables(autoSaveTreqId);
                }
            }

            MessageBox.Show("Акт переоценки успешно сохранен", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            isSaving = true;
            ((Main)this.MdiParent).CloseTab(this.Tag.ToString());
        }

        private void grdPereoc_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdPereoc.Columns[e.ColumnIndex] == newcena)
            {
                decimal val = 0;
                if (decimal.TryParse(grdPereoc[e.ColumnIndex, e.RowIndex].Value.ToString(), out val))
                {
                    if (val > 9999999.99m)
                    {
                        grdPereoc[e.ColumnIndex, e.RowIndex].Value = 9999999.99m;
                    }
                }
                else
                {
                    grdPereoc[e.ColumnIndex, e.RowIndex].Value = 0.00m;
                }
                SetButtonsEnabled();
            }
        }

        private void btExcel_Click(object sender, EventArgs e)
        {
            PrintToExcel();
        }

        private void PrintToExcel()
        {
            bool NedoUser = Config.hCntMain.NedoUser();

            if (dtPereocGoods.DefaultView.ToTable().Select("zcenabnds <= 0").Count() > 0
                    && MessageBox.Show("Не во всех строках заполнено поле новой цены!\nТакие строки печататься не будут! Продолжить?",
                                                         "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int[] id_Deps = dtPereocGoods.DefaultView.ToTable().Select("zcenabnds > 0").Select(r => int.Parse(r["id_dep"].ToString())).ToArray();
            id_Deps = id_Deps.Distinct().ToArray();

            foreach (int id in id_Deps)
            {
                //Генерируем таблицу для выгрузки в отчет
                GenerateData(rbPereoc.Checked, true, true, id);

                if (NedoUser)
                {
                    dtPereocReport.Columns.Remove("zcena");
                    dtPereocReport.AcceptChanges();
                }

                int rowCount = dtPereocReport.Rows.Count;
                int colCount = dtPereocReport.Columns.Count;

                //SaveFileDialog sfdReport = new SaveFileDialog();
                //sfdReport.FileName = "акт переоценки " + id.ToString() + " отдел.";
                //sfdReport.Title = "Выгрузка в Excel";
                //sfdReport.Filter = "Файлы Excel | *.xls";
                //sfdReport.DefaultExt = "xls";
                //sfdReport.InitialDirectory = Application.StartupPath;
                //if (sfdReport.ShowDialog() != DialogResult.OK)
                //{
                //    return;
                //}

                //HandmadeReport rep = new HandmadeReport();
                Nwuram.Framework.ToExcelNew.ExcelUnLoad rep = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

                //Шапка
                rep.AddSingleValue("Акт " + (rbPereoc.Checked ? "переоценки" : "дооценки"), 2, 2);
                rep.SetFontBold(2, 2, 2, 2);
                rep.SetFontSize(2, 2, 2, 2, 12);

                rep.AddSingleValue("Период действия цены".PadRight(60, '_'), 4, 1);
                rep.AddSingleValue("Срочность".PadRight(60, '_'), 6, 1);
                rep.AddSingleValue("Планируемая дата переоценки".PadRight(60, '_'), 8, 1);

                int i = (rbPereoc.Checked ? 0 : 1);
                //Загоговок
                rep.SetRowHeight(10, 1, 10, 1, 25);

                if (rbPereoc.Checked)
                {
                    rep.SetColumnWidth(1, 1, 1, 1, 2);
                }
                rep.SetColumnWidth(1, 2 - i, 1, 2 - i, 3);

                rep.AddSingleValue("Наименование товара", 10, 3 - i);
                rep.SetColumnWidth(1, 3 - i, 1, 3 - i, 30);

                rep.AddSingleValue("EAN", 10, 4 - i);
                rep.SetColumnWidth(1, 4 - i, 1, 4 - i, 13);

                rep.AddSingleValue("Кол-во товара", 10, 5 - i);
                rep.SetColumnWidth(1, 5 - i, 1, 5 - i, 8);

                rep.AddSingleValue("Старая цена", 10, 6 - i);
                rep.SetColumnWidth(1, 6 - i, 1, 6 - i, 8);

                if (NedoUser)
                {
                    rep.AddSingleValue("Новая цена", 10, 7 - i);
                    rep.SetColumnWidth(1, 7 - i, 1, 7 - i, 15);

                    rep.SetWrapText(10, 1, 10, colCount);

                    rep.SetFontBold(10, 1, 10, colCount);
                    rep.SetCellAlignmentToCenter(10, 1, 10, colCount);

                    rep.AddSingleValue("Тех. менеджер".PadRight(36, '_'), rowCount + 12, 1);
                    rep.AddSingleValue("Менеджер".PadRight(33, '_'), rowCount + 12, 5 - i);

                    rep.AddSingleValue("Переоценщик".PadRight(36, '_'), rowCount + 14, 1);
                    rep.AddSingleValue("Руководитель отдела".PadRight(35, '_'), rowCount + 14, 5 - i);

                    rep.AddSingleValue("Мат. отв. лицо".PadRight(38, '_'), rowCount + 16, 1);
                    rep.AddSingleValue("Коммерческий директор".PadRight(34, '_'), rowCount + 16, 5 - i);

                    rep.AddSingleValue("Дата".PadRight(36, '_'), rowCount + 18, 1);
                    rep.AddSingleValue("Разрешено к перерасчету товара".PadRight(37, '_'), rowCount + 20, 1);
                    rep.AddSingleValue("Разрешено к отправке на кассы".PadRight(37, '_'), rowCount + 22, 1);

                    rep.SetFormat(11, 4 - i, rowCount + 10, 4 - i, "#############");
                    rep.SetFormat(11, 5 - i, rowCount + 10, 5 - i, "0,000");
                    rep.SetFormat(11, 6 - i, rowCount + 10, 6 - i, "0,00");
                    rep.SetFormat(11, 7 - i, rowCount + 10, 7 - i, "0,00");
                    rep.AddMultiValue(dtPereocReport, 11, 1);

                    if (rbPereoc.Checked)
                    {
                        rep.SetCellAlignmentToCenter(11, 1, rowCount + 10, 1);
                    }
                    rep.SetCellAlignmentToRight(11, 2 - i, rowCount + 10, 2 - i);
                    rep.SetCellAlignmentToCenter(11, 4 - i, rowCount + 10, 4 - i);
                    rep.SetCellAlignmentToRight(11, 5 - i, rowCount + 10, colCount);

                    rep.SetBorders(10, 1, rowCount + 10, colCount);
                }
                else
                {
                    rep.AddSingleValue("Цена пост.", 10, 7 - i);
                    rep.SetColumnWidth(1, 7 - i, 1, 7 - i, 8);

                    rep.AddSingleValue("Новая цена", 10, 8 - i);
                    rep.SetColumnWidth(1, 8 - i, 1, 8 - i, 15);

                    rep.SetWrapText(10, 1, 10, colCount);

                    rep.SetFontBold(10, 1, 10, colCount);
                    rep.SetCellAlignmentToCenter(10, 1, 10, colCount);

                    rep.AddSingleValue("Тех. менеджер".PadRight(36, '_'), rowCount + 12, 1);
                    rep.AddSingleValue("Менеджер".PadRight(33, '_'), rowCount + 12, 5 - i);

                    rep.AddSingleValue("Переоценщик".PadRight(36, '_'), rowCount + 14, 1);
                    rep.AddSingleValue("Руководитель отдела".PadRight(35, '_'), rowCount + 14, 5 - i);

                    rep.AddSingleValue("Мат. отв. лицо".PadRight(38, '_'), rowCount + 16, 1);
                    rep.AddSingleValue("Коммерческий директор".PadRight(34, '_'), rowCount + 16, 5 - i);

                    rep.AddSingleValue("Дата".PadRight(36, '_'), rowCount + 18, 1);
                    rep.AddSingleValue("Разрешено к перерасчету товара".PadRight(37, '_'), rowCount + 20, 1);
                    rep.AddSingleValue("Разрешено к отправке на кассы".PadRight(37, '_'), rowCount + 22, 1);

                    rep.SetFormat(11, 4 - i, rowCount + 10, 4 - i, "#############");
                    rep.SetFormat(11, 5 - i, rowCount + 10, 5 - i, "0,000");
                    rep.SetFormat(11, 6 - i, rowCount + 10, 6 - i, "0,00");
                    //rep.SetFormat(11, 7 - i, rowCount + 10, 7 - i, "0,0000");
                    rep.SetFormat(11, 8 - i, rowCount + 10, 8 - i, "0,00");

                    foreach (DataRow row in dtPereocReport.Rows)
                    {
                        row["zcena"] = Math.Round(Convert.ToDecimal(row["zcena"]), 4);
                    }

                    rep.AddMultiValue(dtPereocReport, 11, 1);

                    if (rbPereoc.Checked)
                    {
                        rep.SetCellAlignmentToCenter(11, 1, rowCount + 10, 1);
                    }
                    rep.SetCellAlignmentToRight(11, 2 - i, rowCount + 10, 2 - i);
                    rep.SetCellAlignmentToCenter(11, 4 - i, rowCount + 10, 4 - i);
                    rep.SetCellAlignmentToRight(11, 5 - i, rowCount + 10, colCount);

                    rep.SetBorders(10, 1, rowCount + 10, colCount);
                }


                //if (System.IO.File.Exists(sfdReport.FileName))
                //{
                //    try
                //    {
                //        System.IO.File.Delete(sfdReport.FileName);
                //    }
                //    catch (IOException)
                //    {
                //        MessageBox.Show("Невозможно сохранить файл " + sfdReport.FileName + "\n т.к. он занят другим приложением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }
                //}
                //rep.SaveToFile(sfdReport.FileName);
                rep.Show();
            }
        }

        private void PrintToCr()
        {
            if (dtPereocGoods.DefaultView.ToTable().Select("zcenabnds <= 0").Count() > 0
                    && MessageBox.Show("Не во всех строках заполнено поле новой цены!\nТакие строки печататься не будут! Продолжить?",
                                                         "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int[] id_Deps = dtPereocGoods.DefaultView.ToTable().Select("zcenabnds > 0").Select(r => int.Parse(r["id_dep"].ToString())).ToArray();
            id_Deps = id_Deps.Distinct().ToArray();

            foreach (int id in id_Deps)
            {
                GenerateData(rbPereoc.Checked, false, true, id);

                frmReport repPereoc = new frmReport(rbPereoc.Checked, dtPereocReport);
                repPereoc.ShowDialog();
            }
        }

        private void GenerateData(bool isPereoc, bool isExcel, bool addNum, int id_dep)
        {
            //DataTable dtTmpPereocGoods = dtPereocGoods.DefaultView.ToTable();
            DataTable dtTmpPereocGoods = dtPereocGoods;
            if (!dtTmpPereocGoods.Columns.Contains("npp"))
                dtTmpPereocGoods.Columns.Add("npp", typeof(int)).SetOrdinal(0);

            for (int i = 0; i < dtTmpPereocGoods.Rows.Count; i++)
            {
                dtTmpPereocGoods.Rows[i]["npp"] = i + 1;
            }
            dtTmpPereocGoods.AcceptChanges();
            dtPereocReport = dtTmpPereocGoods.DefaultView.ToTable(false, new string[8] { "npp", "cName", "ean", "ostOnDate", "rcena", "zcena", "zcenabnds", "id_dep" }).Select("zcenabnds > 0 AND id_dep = " + id_dep.ToString(), "npp ASC").CopyToDataTable();
            dtPereocReport.Columns.Remove("npp");
            dtPereocReport.Columns.Remove("id_dep");

            if (addNum)
            {
                dtPereocReport.Columns.Add("number", typeof(int));
                dtPereocReport.Columns["number"].SetOrdinal(0);
            }

            if (isPereoc && isExcel)
            {
                dtPereocReport.Columns.Add("choose", typeof(string));
                dtPereocReport.Columns["choose"].SetOrdinal(0);
            }

            bool printCol = (Config.dtProperties.Select("id_user = " + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                                                                                                         + " AND id_val = 'pprz' AND id_prog = " + Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram()
                                                                                                                         + " AND val = '1'").Count() > 0);

            dtPereocReport.DefaultView.Sort = "ean asc";
            dtPereocReport = dtPereocReport.DefaultView.ToTable(true);
            for (int i = 0; i < dtPereocReport.Rows.Count; i++)
            {
                if (isPereoc && isExcel)
                    dtPereocReport.Rows[i]["choose"] = "V";

                if (addNum)
                    dtPereocReport.Rows[i]["number"] = i + 1;

                if (!printCol)
                    dtPereocReport.Rows[i]["ostOnDate"] = DBNull.Value;
            }

            //dtPereocReport.DefaultView.Sort = "ean asc";
            //dtPereocReport = dtPereocReport.DefaultView.ToTable(true);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            PrintToCr();
        }

        private void grdPereoc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введено недопустимое значение!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            dtPereocGoods.Rows[e.RowIndex][grdPereoc.Columns[e.ColumnIndex].DataPropertyName] = 0.00;
            grdPereoc.Refresh();
        }

        private void grdPereoc_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                if (grdPereoc.CurrentCell.OwningColumn.DisplayIndex == newcena.Index)
                {
                    e.Control.KeyPress -= Newcena_KeyPress;
                    e.Control.KeyPress += Newcena_KeyPress;
                }
            }
        }

        void Newcena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (!"1234567890.,\b".Contains(e.KeyChar) ||
                    (((TextBox)sender).Text.Contains(".") && e.KeyChar == '.') ||
                    (((TextBox)sender).Text.Contains(",") && e.KeyChar == ','))
            {
                e.Handled = true;
            }

        }

        private void frmPereoc_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mode != 2)
            {
                Config.openedRequests.Remove(idPereoc);
            }
            mode = 0;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdPereoc_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void grdPereoc_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            grdPereoc.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
            //grdPereoc.Rows[e.RowIndex].DefaultCellStyle.BackColor;
        }
    }
}
