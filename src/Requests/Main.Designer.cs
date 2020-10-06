namespace Requests
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ssConnections = new System.Windows.Forms.StatusStrip();
            this.tslMainConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslAddConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslAdd2Connect = new System.Windows.Forms.ToolStripStatusLabel();
            this.ms_Main = new System.Windows.Forms.MenuStrip();
            this.tsRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSvod = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSprav = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetGoods = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetSup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetZatar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetProh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetSale = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetBonus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSpis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsReserv = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.GeneralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZakazLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelsSticksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManagerGoods = new System.Windows.Forms.ToolStripMenuItem();
            this.tsZakaznik = new System.Windows.Forms.ToolStripMenuItem();
            this.tsZakazMain = new System.Windows.Forms.ToolStripMenuItem();
            this.tsZakazSpecial = new System.Windows.Forms.ToolStripMenuItem();
            this.tsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMain = new Requests.TabControlEx();
            this.tsmiSettingsWeInOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ssConnections.SuspendLayout();
            this.ms_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssConnections
            // 
            this.ssConnections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslMainConnect,
            this.tslAddConnect,
            this.tslAdd2Connect});
            this.ssConnections.Location = new System.Drawing.Point(0, 642);
            this.ssConnections.Name = "ssConnections";
            this.ssConnections.Size = new System.Drawing.Size(975, 22);
            this.ssConnections.TabIndex = 0;
            this.ssConnections.Text = "statusStrip1";
            // 
            // tslMainConnect
            // 
            this.tslMainConnect.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslMainConnect.Name = "tslMainConnect";
            this.tslMainConnect.Size = new System.Drawing.Size(4, 17);
            // 
            // tslAddConnect
            // 
            this.tslAddConnect.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tslAddConnect.Name = "tslAddConnect";
            this.tslAddConnect.Size = new System.Drawing.Size(4, 17);
            // 
            // tslAdd2Connect
            // 
            this.tslAdd2Connect.Name = "tslAdd2Connect";
            this.tslAdd2Connect.Size = new System.Drawing.Size(0, 17);
            // 
            // ms_Main
            // 
            this.ms_Main.AllowMerge = false;
            this.ms_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRequest,
            this.tsSvod,
            this.tsSprav,
            this.tsSpis,
            this.tsReserv,
            this.tsSettings,
            this.tsZakaznik,
            this.tsExit});
            this.ms_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ms_Main.Location = new System.Drawing.Point(0, 0);
            this.ms_Main.Name = "ms_Main";
            this.ms_Main.Size = new System.Drawing.Size(975, 24);
            this.ms_Main.TabIndex = 1;
            this.ms_Main.Text = "menuStrip1";
            // 
            // tsRequest
            // 
            this.tsRequest.Name = "tsRequest";
            this.tsRequest.Size = new System.Drawing.Size(57, 20);
            this.tsRequest.Tag = "frmRequests";
            this.tsRequest.Text = "Заявки";
            this.tsRequest.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSvod
            // 
            this.tsSvod.Name = "tsSvod";
            this.tsSvod.Size = new System.Drawing.Size(58, 20);
            this.tsSvod.Tag = "Svodka.frmSvodka";
            this.tsSvod.Text = "Сводка";
            this.tsSvod.Visible = false;
            this.tsSvod.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSprav
            // 
            this.tsSprav.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSetGoods,
            this.tsSetSup,
            this.tsSetZatar,
            this.tsSetLabels,
            this.tsSetProh,
            this.tsSetSale,
            this.tsSetBonus});
            this.tsSprav.Name = "tsSprav";
            this.tsSprav.Size = new System.Drawing.Size(94, 20);
            this.tsSprav.Text = "Справочники";
            // 
            // tsSetGoods
            // 
            this.tsSetGoods.Name = "tsSetGoods";
            this.tsSetGoods.Size = new System.Drawing.Size(200, 22);
            this.tsSetGoods.Tag = "frmGoods";
            this.tsSetGoods.Text = "Список товаров";
            this.tsSetGoods.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSetSup
            // 
            this.tsSetSup.Name = "tsSetSup";
            this.tsSetSup.Size = new System.Drawing.Size(200, 22);
            this.tsSetSup.Text = "Поставщики";
            this.tsSetSup.Visible = false;
            this.tsSetSup.Click += new System.EventHandler(this.tsSetSup_Click);
            // 
            // tsSetZatar
            // 
            this.tsSetZatar.Name = "tsSetZatar";
            this.tsSetZatar.Size = new System.Drawing.Size(200, 22);
            this.tsSetZatar.Tag = "request_zatr.MainForm";
            this.tsSetZatar.Text = "Затарка";
            this.tsSetZatar.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSetLabels
            // 
            this.tsSetLabels.Name = "tsSetLabels";
            this.tsSetLabels.Size = new System.Drawing.Size(200, 22);
            this.tsSetLabels.Tag = "Stikers.MainForm";
            this.tsSetLabels.Text = "Этикетки";
            this.tsSetLabels.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSetProh
            // 
            this.tsSetProh.Name = "tsSetProh";
            this.tsSetProh.Size = new System.Drawing.Size(200, 22);
            this.tsSetProh.Tag = "ForbiddenGoods.frmForbiddenGoods";
            this.tsSetProh.Text = "Запрещенные товары";
            this.tsSetProh.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSetSale
            // 
            this.tsSetSale.Name = "tsSetSale";
            this.tsSetSale.Size = new System.Drawing.Size(200, 22);
            this.tsSetSale.Tag = "SaleGoods.frmSaleGoods";
            this.tsSetSale.Text = "Распродажные товары";
            this.tsSetSale.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSetBonus
            // 
            this.tsSetBonus.Name = "tsSetBonus";
            this.tsSetBonus.Size = new System.Drawing.Size(200, 22);
            this.tsSetBonus.Tag = "Bonus.frmMain";
            this.tsSetBonus.Text = "Бонус";
            this.tsSetBonus.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSpis
            // 
            this.tsSpis.Name = "tsSpis";
            this.tsSpis.Size = new System.Drawing.Size(126, 20);
            this.tsSpis.Text = "Обоснование спис.";
            this.tsSpis.Click += new System.EventHandler(this.tsSpis_Click);
            // 
            // tsReserv
            // 
            this.tsReserv.Name = "tsReserv";
            this.tsReserv.Size = new System.Drawing.Size(65, 20);
            this.tsReserv.Tag = "RequestReservs.mainForm";
            this.tsReserv.Text = "Резервы";
            this.tsReserv.Visible = false;
            this.tsReserv.Click += new System.EventHandler(this.AddTab);
            // 
            // tsSettings
            // 
            this.tsSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeneralToolStripMenuItem,
            this.ZakazLimitToolStripMenuItem,
            this.LabelsSticksToolStripMenuItem,
            this.tsmiManagerGoods,
            this.tsmiSettingsWeInOut});
            this.tsSettings.Name = "tsSettings";
            this.tsSettings.Size = new System.Drawing.Size(79, 20);
            this.tsSettings.Text = "Настройки";
            this.tsSettings.Click += new System.EventHandler(this.AddTab);
            // 
            // GeneralToolStripMenuItem
            // 
            this.GeneralToolStripMenuItem.Name = "GeneralToolStripMenuItem";
            this.GeneralToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.GeneralToolStripMenuItem.Text = "Общие";
            this.GeneralToolStripMenuItem.Click += new System.EventHandler(this.GeneralToolStripMenuItem_Click);
            // 
            // ZakazLimitToolStripMenuItem
            // 
            this.ZakazLimitToolStripMenuItem.Name = "ZakazLimitToolStripMenuItem";
            this.ZakazLimitToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.ZakazLimitToolStripMenuItem.Text = "Ограничения заказа";
            this.ZakazLimitToolStripMenuItem.Click += new System.EventHandler(this.ZakazLimitToolStripMenuItem_Click);
            // 
            // LabelsSticksToolStripMenuItem
            // 
            this.LabelsSticksToolStripMenuItem.Name = "LabelsSticksToolStripMenuItem";
            this.LabelsSticksToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.LabelsSticksToolStripMenuItem.Text = "Ценники и этикетки";
            this.LabelsSticksToolStripMenuItem.Click += new System.EventHandler(this.LabelsSticksToolStripMenuItem_Click);
            // 
            // tsmiManagerGoods
            // 
            this.tsmiManagerGoods.Name = "tsmiManagerGoods";
            this.tsmiManagerGoods.Size = new System.Drawing.Size(237, 22);
            this.tsmiManagerGoods.Text = "Товары менеджеров";
            this.tsmiManagerGoods.Click += new System.EventHandler(this.tsmiManagerGoods_Click);
            // 
            // tsZakaznik
            // 
            this.tsZakaznik.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsZakazMain,
            this.tsZakazSpecial});
            this.tsZakaznik.Name = "tsZakaznik";
            this.tsZakaznik.Size = new System.Drawing.Size(69, 20);
            this.tsZakaznik.Text = "Заказник";
            // 
            // tsZakazMain
            // 
            this.tsZakazMain.Name = "tsZakazMain";
            this.tsZakazMain.Size = new System.Drawing.Size(202, 22);
            this.tsZakazMain.Text = "Основной заказ";
            this.tsZakazMain.Click += new System.EventHandler(this.tsZakazMain_Click);
            // 
            // tsZakazSpecial
            // 
            this.tsZakazSpecial.Name = "tsZakazSpecial";
            this.tsZakazSpecial.Size = new System.Drawing.Size(202, 22);
            this.tsZakazSpecial.Text = "Дополнительный заказ";
            this.tsZakazSpecial.Click += new System.EventHandler(this.tsZakazSpecial_Click);
            // 
            // tsExit
            // 
            this.tsExit.Name = "tsExit";
            this.tsExit.Size = new System.Drawing.Size(53, 20);
            this.tsExit.Text = "Выход";
            this.tsExit.Click += new System.EventHandler(this.tsExit_Click);
            // 
            // tcMain
            // 
            this.tcMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tcMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcMain.ItemSize = new System.Drawing.Size(0, 18);
            this.tcMain.Location = new System.Drawing.Point(0, 622);
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(10, 0);
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(975, 20);
            this.tcMain.TabIndex = 2;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            this.tcMain.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.tcMain_ControlRemoved);
            // 
            // tsmiSettingsWeInOut
            // 
            this.tsmiSettingsWeInOut.Name = "tsmiSettingsWeInOut";
            this.tsmiSettingsWeInOut.Size = new System.Drawing.Size(237, 22);
            this.tsmiSettingsWeInOut.Text = "\"Наших\" приходов\\возвратов";
            this.tsmiSettingsWeInOut.Click += new System.EventHandler(this.tsmiSettingsWeInOut_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 664);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.ssConnections);
            this.Controls.Add(this.ms_Main);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.ms_Main;
            this.MinimumSize = new System.Drawing.Size(975, 650);
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.ssConnections.ResumeLayout(false);
            this.ssConnections.PerformLayout();
            this.ms_Main.ResumeLayout(false);
            this.ms_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssConnections;
        private System.Windows.Forms.MenuStrip ms_Main;
        private System.Windows.Forms.ToolStripMenuItem tsRequest;
        private System.Windows.Forms.ToolStripMenuItem tsSvod;
        private System.Windows.Forms.ToolStripMenuItem tsReserv;
        private System.Windows.Forms.ToolStripMenuItem tsSprav;
        private System.Windows.Forms.ToolStripMenuItem tsSetGoods;
        private System.Windows.Forms.ToolStripMenuItem tsSetSup;
        private System.Windows.Forms.ToolStripMenuItem tsSetZatar;
        private System.Windows.Forms.ToolStripMenuItem tsSetLabels;
        private System.Windows.Forms.ToolStripMenuItem tsSetProh;
        private System.Windows.Forms.ToolStripMenuItem tsSetSale;
        private System.Windows.Forms.ToolStripMenuItem tsSettings;
        private System.Windows.Forms.ToolStripMenuItem tsExit;
        //private System.Windows.Forms.TabControl tabControl1;
        private TabControlEx tcMain;
        private System.Windows.Forms.ToolStripStatusLabel tslMainConnect;
        private System.Windows.Forms.ToolStripStatusLabel tslAddConnect;
        private System.Windows.Forms.ToolStripMenuItem GeneralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZakazLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LabelsSticksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsSetBonus;
        private System.Windows.Forms.ToolStripMenuItem tsmiManagerGoods;
        private System.Windows.Forms.ToolStripMenuItem tsZakaznik;
        private System.Windows.Forms.ToolStripMenuItem tsZakazMain;
        private System.Windows.Forms.ToolStripMenuItem tsZakazSpecial;
        private System.Windows.Forms.ToolStripMenuItem tsSpis;
        private System.Windows.Forms.ToolStripStatusLabel tslAdd2Connect;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettingsWeInOut;
    }
}

