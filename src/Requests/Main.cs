using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;

namespace Requests
{
    public partial class Main : Form
    {
        Form currentForm;

        public Main()
        {
            InitializeComponent();
        }

        //Запрет изменения размеров формы по верхней и правой границе
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //WM_NCHITTEST
                    var result = (HitTest)m.Result.ToInt32();
                    if (result == HitTest.Left || result == HitTest.Top
                        || result == HitTest.TopLeft || result == HitTest.TopRight
                        || result == HitTest.BottomLeft)
                        m.Result = new IntPtr((int)HitTest.Caption);

                    break;
            }
        }
        enum HitTest
        {
            Caption = 2,
            Transparent = -1,
            Nowhere = 0,
            Client = 1,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17,
            Border = 18
        }

        private void Main_Load(object sender, EventArgs e)
        {
            tslMainConnect.Text = ConnectionSettings.GetServer() + " " + ConnectionSettings.GetDatabase();
            tslAddConnect.Text = ConnectionSettings.GetServer("2") + " " + ConnectionSettings.GetDatabase("2");
            tslAdd2Connect.Text = ConnectionSettings.GetServer("3") + " " + ConnectionSettings.GetDatabase("3");
            this.Text = ConnectionSettings.ProgramName + ", " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername + " - " + Config.hCntMain.GetStoreName();

            if (Config.hCntMain.NedoUser())
            {
                Logging.StartFirstLevel(93);
                Logging.Comment("Пользователь в режиме Просмотр с ограниченными правами");
                Logging.Comment("Отображать колонку \"Цена зак.\": НЕТ. ");
                Logging.StopFirstLevel();

                Config.hCntMain.SetProperties("show zcena", "0", 0);
            }

            Config.SetDoubleBuffered(tcMain);

            tsSvod.Visible = (UserSettings.User.StatusCode != "МН" && UserSettings.User.StatusCode != "ДМН");
            tsReserv.Visible = (UserSettings.User.StatusCode == "КД" || UserSettings.User.StatusCode == "РКВ");
            tsSetSup.Visible = (UserSettings.User.StatusCode == "КД" || UserSettings.User.StatusCode == "РКВ");
            tsSetLabels.Visible =
                tsSetProh.Visible =
                tsSetSale.Visible = 
                tsSetZatar.Visible =
                LabelsSticksToolStripMenuItem.Visible = (UserSettings.User.StatusCode != "КНТ" && UserSettings.User.StatusCode != "ПР");
            tsmiManagerGoods.Visible = UserSettings.User.StatusCode != "МН";
            tsZakaznik.Visible = (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН" || UserSettings.User.StatusCode == "РКВ") && Config.hCntMain.DepartmentInSettings("depz");
            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН" || UserSettings.User.StatusCode == "РКВ" || UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "КД") && Config.hCntMain.DepartmentInSettings("spis"))
            {
                tsSpis.Visible = true;
                ProcessService.StartStatusViewer("StatusViewer", Application.StartupPath);
            }
            //tsSetBonus.Visible = (UserSettings.User.StatusCode == "КД");
            Config.curDate = Config.hCntMain.GetCurDate(false);
            Config.dtProperties = Config.hCntMain.GetProperties();
            Config.openedRequests = new System.Collections.ArrayList();
            Config.currentEan = "";

            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && (UserSettings.User.IdDepartment != 6 ? Config.hCntMain.GetTU(UserSettings.User.IdDepartment) : Config.hCntAdd.GetTU(UserSettings.User.IdDepartment)).Select("id <> 0").Count() == 0)
            {
                MessageBox.Show("Вы не имеете прав для\nдоступа к группам товаров.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return;
            }

            InputLanguage.CurrentInputLanguage = GetInputLanguageByName("ru");

            if (ConnectionSettings.GetServer().Trim() == @"192.168.5.50\ISI_SQL"
                || ConnectionSettings.GetServer().Trim() == @"192.168.5.50")
            {
                Timer tmrCheckVer = new Timer();
                tmrCheckVer.Interval = 1800000;
                tmrCheckVer.Tick += new EventHandler(tmrCheckVer_Tick);
                tmrCheckVer.Start();
            }

            if (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
            {
                DataTable dtAuto = UserSettings.User.IdDepartment != 6 ? Config.hCntMain.AutoSavedRequestsExists() : Config.hCntAdd.AutoSavedRequestsExists();
                if (dtAuto != null && dtAuto.Rows.Count > 0)
                {
                    LoadAuto(dtAuto, false);
                    LoadAuto(dtAuto, true);
                }
            }

            //Проверка существования периодов в property_list
            Config.hCntMain.setCurPeriod();
        }

        private void LoadAuto(DataTable dt, bool pereoc)
        {
            DataRow[] rows = dt.Select(pereoc ? "id_operand = 4" : "id_operand <> 4");
            if (rows.Length > 0)
            {
                int autoSavedRequestId = Convert.ToInt32(rows[0]["id"]);
                if (autoSavedRequestId != 0)
                {
                    if (MessageBox.Show("У Вас имеется автоматически сохранённая " + (pereoc ? "переоценка" : "заявка") + ". \n Желаете продолжить с ней работу? \n\n В случае нажатия \"Нет\" автосохранённая " + (pereoc ? "переоценка" : "заявка") + " будет удалена.", "Восстановление автосохранённой заявки", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!pereoc)
                        {
                            SetTab("Заявки", "frmRequests", new object[] { true }, null);
                            SetTab("Создание заявки.", "frmEditRequest", new object[] { 1, 0, false, true, autoSavedRequestId }, 0);
                        }
                        else
                        {
                            SetTab("Переоценка. Создание.", "frmPereoc", new object[] { 2, 0, UserSettings.User.IdDepartment, DateTime.Now, true, autoSavedRequestId }, 0);
                        }
                    }
                    else
                    {
                        StartLogClearAutoSaveTables();
                        if (UserSettings.User.IdDepartment != 6)
                        {
                            Config.hCntMain.ClearAutoSaveTables(autoSavedRequestId);
                        }
                        else
                        {
                            Config.hCntAdd.ClearAutoSaveTables(autoSavedRequestId);
                        }
                        StopLogClearAutoSaveTables();
                    }
                }
            }
        }

        public static void StartLogClearAutoSaveTables()
        {
            Logging.StartFirstLevel(1015);
            Logging.Comment("Начало удаления автосохр.заявки");

            DataTable dt = UserSettings.User.IdDepartment != 6 ? Config.hCntMain.GetAutoRequestHead() : Config.hCntAdd.GetAutoRequestHead();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Logging.Comment("id автосохр.заявки = " + row["id"].ToString() + ", id отдела = " + row["id_Otdel"].ToString() + ", дата заявки = " + row["DateRequest"].ToString() + ", id поставщика = " + row["id_Post"].ToString() + ", ntypeorg = " + row["ntypeorg"].ToString() + ", cprimech = " + row["cprimech"].ToString() + ", id_operand = " + row["id_operand"].ToString() + ", porthole = " + row["porthole"].ToString() + ", id_TypeBonus = " + row["id_TypeBonus"].ToString() + ", название поставщика = " + row["post_name"].ToString());
            }

            DataTable dtBody = UserSettings.User.IdDepartment != 6 ? Config.hCntMain.GetAutoRequestBody() : Config.hCntAdd.GetAutoRequestBody();
            if (dtBody != null && dtBody.Rows.Count > 0)
            {
                DataRow row = dtBody.Rows[0];
                Logging.Comment("id_tovar = " + row["id_Tovar"].ToString() + ", ean = " + row["ean"].ToString() + ", cname = " + row["cname"].ToString() + ", zcena = " + row["Zcena"].ToString() + ", rcena = " + row["Rcena"].ToString() + ", zakaz = " + row["zakaz"].ToString() + ", id_subject = " + row["id_subject"].ToString());
            }
        }

        public static void StopLogClearAutoSaveTables()
        {
            Logging.Comment("Конец удаления автосохр.заявки");
            Logging.StopFirstLevel();
        }

        private void tmrCheckVer_Tick(object sender, EventArgs e)
        {
            byte[] fileServHash = GetMD5Hash(@"\\192.168.5.65\AppAuth\Requests\Requests.exe");
            byte[] fileLocalHash = GetMD5Hash(Application.StartupPath + @"\Requests.exe");

            if (!CompareHashCodes(fileServHash, fileLocalHash))
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;

                this.Activate();
                MessageBox.Show("Данная версия ПО \"Заявки\" устарела. Перезайдите в программу!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Сравнивает 2 хеша
        /// </summary>
        /// <param name="hastOne">Хеш для сравнения</param>
        /// <param name="hashTwo">Хеш для сравнения</param>
        /// <returns></returns>
        private bool CompareHashCodes(byte[] hastOne, byte[] hashTwo)
        {
            int i = 0;
            while ((i < hastOne.Length) && (hastOne[i] == hashTwo[i]))
            {
                i += 1;
            }
            if (i == hastOne.Length)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Получение md5 хеша файла
        /// </summary>
        /// <param name="filename">Путь к файлу</param>
        /// <returns></returns>
        private byte[] GetMD5Hash(string filename)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        /// <summary>
        /// Добавление закладки(если закладка уже есть - выбор закладки)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTab(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Tag != null)
            {
                SetTab(((ToolStripMenuItem)sender).Text, ((ToolStripMenuItem)sender).Tag.ToString(),null, null);
            }
        }

        public void SetTab(string tabText, string tag, object[] args, int? frmId)
        {
            string formName = tag; //Название формы
            if (frmId != null && !tag.Contains(frmId.ToString()))
                tag += frmId.ToString(); //Тэг формы для поиска

            //Выбор существующей формы
            foreach (TabPage tpag in tcMain.TabPages)
            {
                if (tpag.Tag.ToString() == tag)
                {
                    tcMain.SelectedTab = tpag;
                    SelectMdiChild(tag);
                    return;
                }
            }
            tcMain.TabPages.Add(tabText);
            tcMain.SelectedTab = tcMain.TabPages[tcMain.TabPages.Count - 1];

            //Добавление новой формы
            string BaseType = (formName.IndexOf('.') == -1 ? "Requests." : "") + 
                formName;
            tcMain.SelectedTab.Tag = tag;
            Form frm;
            if (args == null)
            {
                frm = GetInstance<Form>(BaseType);
            }
            else
            {
                frm = GetInstance<Form>(BaseType, args);
                if (frmId != null)
                {
                    frm.Name = frm.Name + frmId.ToString();
                }
            }

            frm.Tag = tag;
            frm.MdiParent = this;
            //frm.ControlBox = false;
            //frm.ShowIcon = false;
            //frm.Text = string.Empty;

            frm.FormBorderStyle = FormBorderStyle.None;
            frm.WindowState = FormWindowState.Normal;
            currentForm = frm;
            SetChildDimensions(frm);
            frm.Show();
            //currentForm = frm;
            //SetChildDimensions(frm);
            frm.FormClosed += new FormClosedEventHandler(mdiClosedByButton);

            //Устанавливаем двойную буфферизацию для гридов
            foreach (Control con in frm.Controls)
            {
                if (con is DataGridView)
                {
                    Config.SetDoubleBuffered(con);
                }
            }
        }

        private void mdiClosedByButton(object sender, FormClosedEventArgs e)
        {
            CloseTab(((Form)sender).Tag.ToString());
        }

        private void tsExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///// <summary>
        ///// Добавление дочерней MDI формы
        ///// </summary>
        ///// <param name="frm">Форма для добавления</param>
        //private void AddChlid(Form frm)
        //{
        //    frm.MdiParent = this;
        //    frm.Show();
        //    frm.WindowState = FormWindowState.Maximized;
        //}

        /// <summary>
        /// Выбор текущей дочерней формы (переход по вкладкам)
        /// </summary>
        /// <param name="frmName">Название формы</param>
        private void SelectMdiChild(string frmName)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == frmName || (frm.Tag != null && frm.Tag.ToString() == frmName))
                {
                    frm.Show();
                    currentForm = frm;
                    SetChildDimensions(frm);
                    if (frm.Name == "frmRequests")
                    {
                        ((frmRequests)frm).GetGridRequests();
                    }
                }
                else
                {
                    frm.Size =
                    frm.MaximumSize =
                    frm.MinimumSize = new Size(1, 1);
                    frm.Location = new Point(0, 0);
                }
            }
        }

        /// <summary>
        /// Получение типа по названию
        /// </summary>
        /// <typeparam name="T">Базовый тип</typeparam>
        /// <param name="type">Название типа</param>
        /// <returns></returns>
        private T GetInstance<T>(string type)
        {
            string AssemblyName = type.Substring(0, type.IndexOf("."));
            return (T)Activator.CreateInstance(Type.GetType(type + ", " + AssemblyName));
        }

        /// <summary>
        /// Получение типа по названию
        /// </summary>
        /// <typeparam name="T">Базовый тип</typeparam>
        /// <param name="type">Название типа</param>
        ///  <param name="type">Аргументы </param>
        /// <returns></returns>
        private T GetInstance<T>(string type, object[] args)
        {
            string AssemblyName = type.Substring(0, type.IndexOf("."));
            return (T)Activator.CreateInstance(Type.GetType(type + ", " + AssemblyName), args);
        }

        private void tcMain_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control.Tag != null)
            {
                if (!RemoveMdiChild(e.Control.Tag.ToString()))
                {
                    tcMain.TabPages.Insert(tcMain.TabPages.IndexOf((TabPage)e.Control), (TabPage)e.Control);
                }
            }
        }

        /// <summary>
        /// Закрытие формы по зарытию вкладки
        /// </summary>
        /// <param name="frmName">Название формы</param>
        private bool RemoveMdiChild(string frmName)
        {
            bool retValue = true;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == frmName || (frm.Tag != null && frm.Tag.ToString() == frmName))
                {
                    frm.Close();
                    if (frm.DialogResult == DialogResult.No)
                    {
                        retValue = false;
                    }
                    break;
                }

            }

            if (this.MdiChildren.Count() == 0)
            {
                currentForm = null;
            }

            return retValue;
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab != null && tcMain.SelectedTab.Tag != null)
            {
                SelectMdiChild(tcMain.SelectedTab.Tag.ToString());
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() != 0 && currentForm != null)
            {
                SetChildDimensions(currentForm);
            }
        }

        private void SetChildDimensions(Form frm)
        {
            frm.StartPosition = FormStartPosition.Manual;
            frm.Size =
            frm.MinimumSize =
            frm.MaximumSize = new Size((this.ClientSize.Width != 0 ? this.ClientSize.Width - 4 : 2)
                                        , (this.ClientSize.Height != 0 ? this.ClientSize.Height - 72 : 2));
            frm.Location = new Point(0, 0);

            foreach (Form child in this.MdiChildren)
            {
                if (child != frm)
                {
                    child.Hide(); 
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall
                && MessageBox.Show("Вы уверены что хотите закрыть\nпрограмму?", "Запрос на выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                {
                    DataTable dtAuto = UserSettings.User.IdDepartment != 6 ? Config.hCntMain.AutoSavedRequestsExists() : Config.hCntAdd.AutoSavedRequestsExists();
                    if (dtAuto != null && dtAuto.Rows.Count > 0)
                    {
                        StartLogClearAutoSaveTables();
                        if (UserSettings.User.IdDepartment != 6)
                        {
                           Config.hCntMain.ClearAutoSaveTables(Convert.ToInt32(dtAuto.Rows[0]["id"]));
                        }
                        else
                        {
                            Config.hCntAdd.ClearAutoSaveTables(Convert.ToInt32(dtAuto.Rows[0]["id"]));
                        }
                        Main.StopLogClearAutoSaveTables();
                    }
                }

                if (ProcessService.ProcessIsRunning("StatusViewer") && MessageBox.Show("У вас запущена программа отслеживания отчётов списания. Закрыть?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProcessService.StopProcess("StatusViewer");
                }
            }
        }

        public void CloseTab(string tag)
        {
            foreach (TabPage tpMain in tcMain.TabPages)
            {
                if (tpMain.Tag.ToString() == tag)
                {
                    tcMain.TabPages.Remove(tpMain);
                    break;
                }
            }
        }

        private void tsSetSup_Click(object sender, EventArgs e)
        {

            SetTab("Поставщики", "Posts.frmPosts", null, null);
        }

        private void GeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings.frmSettings frmGSettings = new GeneralSettings.frmSettings();
            frmGSettings.ShowDialog();
            Config.dtProperties = Config.hCntMain.GetProperties();
        }

        private void ZakazLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestSettings.MainForm frmRSettings = new RequestSettings.MainForm();
            frmRSettings.ShowDialog();
            Config.dtProperties = Config.hCntMain.GetProperties();
        }

        private void LabelsSticksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsStiksAndPrice.mainForm frmSPSettings = new SettingsStiksAndPrice.mainForm();
            frmSPSettings.ShowDialog();
            Config.dtProperties = Config.hCntMain.GetProperties();
        }

        public bool isFormOpened(string tag)
        {
            string formName = tag; //Название формы
            
            //Выбор существующей формы
            foreach (TabPage tpag in tcMain.TabPages)
            {
                if (tpag.Tag.ToString() == tag)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Выбор языка из установленых
        /// </summary>
        /// <param name="inputName">язык</param>
        /// <returns></returns>
        public static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))
                {
                    return lang;
                }
            }
            return null;
        }

        private void tsmiManagerGoods_Click(object sender, EventArgs e)
        {
            Form frmManGoods = new frmManagersVsGoods();
            frmManGoods.ShowDialog();
        }

        private void tsZakazMain_Click(object sender, EventArgs e)
        {
            SetTab("Основной заказ", "frmMainZakaz", null, null);
            //frmMainZakaz frmZakaz = new frmMainZakaz();
            //frmZakaz.ShowDialog();
        }

        private void tsZakazSpecial_Click(object sender, EventArgs e)
        {
            SetTab("Дополнительный заказ", "frmDopZakaz", null, null);
            //frmDopZakaz frmZakaz = new frmDopZakaz();
            //frmZakaz.ShowDialog();
        }

        private void tsSpis_Click(object sender, EventArgs e)
        {
            SetTab("Обоснование списания", "SpisanieOFK.MainForm", null, null);
        }

        private void tsmiSettingsWeInOut_Click(object sender, EventArgs e)
        {
            sWeInOut.frmSettingsWeInOut frm = new sWeInOut.frmSettingsWeInOut();
            frm.ShowDialog();
        }

    }
}
