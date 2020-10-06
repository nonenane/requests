namespace Requests
{
    partial class frmMainZakaz
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.grdBody = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.check = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCbIsSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.post_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fact_netto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtFactNetto = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.caliber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.zcena = new DevExpress.XtraGrid.Columns.GridColumn();
            this.subject_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.id_trequest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.id_tovar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.id_order = new DevExpress.XtraGrid.Columns.GridColumn();
            this.id_order_body = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.grdHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ean = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sred_rashod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ost_on_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.inventory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.diff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.plan_realiz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.zakaz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.perezatarka = new DevExpress.XtraGrid.Columns.GridColumn();
            this.perezatarka_zal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcena = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spisanie = new DevExpress.XtraGrid.Columns.GridColumn();
            this.id_grp3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCmbGrp3 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.zakaz_manager = new DevExpress.XtraGrid.Columns.GridColumn();
            this.id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nzatar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.orderBody = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmbTUGroups = new System.Windows.Forms.ComboBox();
            this.cmbSubGroups = new System.Windows.Forms.ComboBox();
            this.txtEAN = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPostName = new System.Windows.Forms.TextBox();
            this.btnClearName = new System.Windows.Forms.Button();
            this.btnClearPost = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCreateRequests = new System.Windows.Forms.Button();
            this.btnCreateActInv = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.txtInserter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDateInsert = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDateEdit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlHasZatar = new System.Windows.Forms.Panel();
            this.pnlSelected = new System.Windows.Forms.Panel();
            this.pnlHasRequest = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbSelected = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.btnMultiTU = new System.Windows.Forms.Button();
            this.ordersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmsRealiz = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmRealiz = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInventory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmInventory = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPosts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAddPost = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelPost = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSubjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSubjects = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwLoad = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbInv = new System.Windows.Forms.RadioButton();
            this.rbOst = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdBody)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCbIsSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFactNetto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbGrp3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBody)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).BeginInit();
            this.cmsRealiz.SuspendLayout();
            this.cmsInventory.SuspendLayout();
            this.cmsPosts.SuspendLayout();
            this.cmsSubjects.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdBody
            // 
            this.grdBody.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Maroon;
            this.grdBody.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.grdBody.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.check,
            this.post_name,
            this.fact_netto,
            this.caliber,
            this.zcena,
            this.subject_name,
            this.id_trequest,
            this.id_tovar,
            this.id_order,
            this.id_order_body});
            this.grdBody.GridControl = this.grdMain;
            this.grdBody.LevelIndent = 10;
            this.grdBody.Name = "grdBody";
            this.grdBody.OptionsMenu.EnableColumnMenu = false;
            this.grdBody.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grdBody.OptionsView.ShowGroupPanel = false;
            this.grdBody.OptionsView.ShowIndicator = false;
            this.grdBody.ViewCaption = "Поставщики";
            this.grdBody.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdBody_RowCellClick);
            this.grdBody.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdBody_RowCellStyle);
            this.grdBody.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grdBody_PopupMenuShowing);
            this.grdBody.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.grdBody_ShowingEditor);
            // 
            // check
            // 
            this.check.Caption = " ";
            this.check.ColumnEdit = this.repCbIsSelect;
            this.check.FieldName = "checked";
            this.check.Name = "check";
            this.check.Visible = true;
            this.check.VisibleIndex = 0;
            // 
            // repCbIsSelect
            // 
            this.repCbIsSelect.AutoHeight = false;
            this.repCbIsSelect.Name = "repCbIsSelect";
            // 
            // post_name
            // 
            this.post_name.Caption = "Поставщик";
            this.post_name.FieldName = "post_name";
            this.post_name.Name = "post_name";
            this.post_name.Visible = true;
            this.post_name.VisibleIndex = 1;
            // 
            // fact_netto
            // 
            this.fact_netto.Caption = "Факт. заказ";
            this.fact_netto.ColumnEdit = this.repTxtFactNetto;
            this.fact_netto.DisplayFormat.FormatString = "N3";
            this.fact_netto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fact_netto.FieldName = "fact_netto";
            this.fact_netto.Name = "fact_netto";
            this.fact_netto.Visible = true;
            this.fact_netto.VisibleIndex = 2;
            // 
            // repTxtFactNetto
            // 
            this.repTxtFactNetto.AutoHeight = false;
            this.repTxtFactNetto.DisplayFormat.FormatString = "N3";
            this.repTxtFactNetto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repTxtFactNetto.Name = "repTxtFactNetto";
            this.repTxtFactNetto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repTxtFactNetto_KeyPress);
            // 
            // caliber
            // 
            this.caliber.Caption = "Калибр";
            this.caliber.FieldName = "caliber";
            this.caliber.Name = "caliber";
            this.caliber.Visible = true;
            this.caliber.VisibleIndex = 3;
            // 
            // zcena
            // 
            this.zcena.Caption = "Цена закупки";
            this.zcena.DisplayFormat.FormatString = "N2";
            this.zcena.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.zcena.FieldName = "zcena";
            this.zcena.Name = "zcena";
            this.zcena.Visible = true;
            this.zcena.VisibleIndex = 4;
            // 
            // subject_name
            // 
            this.subject_name.Caption = "Страна/Субъект";
            this.subject_name.FieldName = "subject_name";
            this.subject_name.Name = "subject_name";
            this.subject_name.Visible = true;
            this.subject_name.VisibleIndex = 5;
            // 
            // id_trequest
            // 
            this.id_trequest.Caption = "№ заявки";
            this.id_trequest.FieldName = "id_trequest";
            this.id_trequest.Name = "id_trequest";
            this.id_trequest.Visible = true;
            this.id_trequest.VisibleIndex = 6;
            // 
            // id_tovar
            // 
            this.id_tovar.Caption = "id_tovar";
            this.id_tovar.FieldName = "id_tovar";
            this.id_tovar.Name = "id_tovar";
            // 
            // id_order
            // 
            this.id_order.Caption = "id_order";
            this.id_order.FieldName = "id_order";
            this.id_order.Name = "id_order";
            // 
            // id_order_body
            // 
            this.id_order_body.Caption = "id_order_body";
            this.id_order_body.FieldName = "id_order_body";
            this.id_order_body.Name = "id_order_body";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.Cursor = System.Windows.Forms.Cursors.Default;
            gridLevelNode1.LevelTemplate = this.grdBody;
            gridLevelNode1.RelationName = "grdBody";
            this.grdMain.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdMain.Location = new System.Drawing.Point(12, 101);
            this.grdMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grdMain.MainView = this.grdHeader;
            this.grdMain.Name = "grdMain";
            this.grdMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repCbIsSelect,
            this.repCmbGrp3,
            this.repTxtFactNetto});
            this.grdMain.Size = new System.Drawing.Size(943, 472);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdHeader,
            this.orderBody,
            this.grdBody});
            // 
            // grdHeader
            // 
            this.grdHeader.Appearance.DetailTip.BackColor = System.Drawing.Color.White;
            this.grdHeader.Appearance.DetailTip.Options.UseBackColor = true;
            this.grdHeader.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.grdHeader.Appearance.EvenRow.Options.UseBackColor = true;
            this.grdHeader.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdHeader.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdHeader.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grdHeader.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grdHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ean,
            this.cname,
            this.sred_rashod,
            this.ost_on_date,
            this.inventory,
            this.diff,
            this.plan_realiz,
            this.zakaz,
            this.perezatarka,
            this.perezatarka_zal,
            this.rcena,
            this.spisanie,
            this.id_grp3,
            this.zakaz_manager,
            this.id,
            this.nzatar});
            styleFormatCondition1.Column = this.inventory;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            this.grdHeader.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.grdHeader.GridControl = this.grdMain;
            this.grdHeader.LevelIndent = 100;
            this.grdHeader.Name = "grdHeader";
            this.grdHeader.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.grdHeader.OptionsCustomization.AllowFilter = false;
            this.grdHeader.OptionsDetail.AllowExpandEmptyDetails = true;
            this.grdHeader.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grdHeader.OptionsView.ShowDetailButtons = false;
            this.grdHeader.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grdHeader.OptionsView.ShowGroupPanel = false;
            this.grdHeader.OptionsView.ShowIndicator = false;
            this.grdHeader.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grdHeader_RowCellClick);
            this.grdHeader.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdHeader_RowCellStyle);
            this.grdHeader.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grdHeader_RowStyle);
            this.grdHeader.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grdHeader_PopupMenuShowing);
            this.grdHeader.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.grdHeader_ShowingEditor);
            this.grdHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grdHeader_FocusedRowChanged);
            this.grdHeader.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.grdHeader_FocusedColumnChanged);
            this.grdHeader.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grdHeader_CellValueChanged);
            this.grdHeader.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.grdHeader_MouseWheel);
            // 
            // ean
            // 
            this.ean.Caption = "EAN";
            this.ean.FieldName = "ean";
            this.ean.Name = "ean";
            this.ean.OptionsColumn.ReadOnly = true;
            this.ean.Visible = true;
            this.ean.VisibleIndex = 0;
            this.ean.Width = 77;
            // 
            // cname
            // 
            this.cname.Caption = "Наименование";
            this.cname.FieldName = "cname";
            this.cname.Name = "cname";
            this.cname.OptionsColumn.ReadOnly = true;
            this.cname.Visible = true;
            this.cname.VisibleIndex = 1;
            this.cname.Width = 90;
            // 
            // sred_rashod
            // 
            this.sred_rashod.Caption = "Средний расход";
            this.sred_rashod.DisplayFormat.FormatString = "N3";
            this.sred_rashod.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.sred_rashod.FieldName = "sred_rashod";
            this.sred_rashod.Name = "sred_rashod";
            this.sred_rashod.OptionsColumn.ReadOnly = true;
            this.sred_rashod.Visible = true;
            this.sred_rashod.VisibleIndex = 2;
            // 
            // ost_on_date
            // 
            this.ost_on_date.Caption = "Остаток на утро";
            this.ost_on_date.DisplayFormat.FormatString = "N3";
            this.ost_on_date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ost_on_date.FieldName = "ost_on_date";
            this.ost_on_date.Name = "ost_on_date";
            this.ost_on_date.OptionsColumn.ReadOnly = true;
            this.ost_on_date.Visible = true;
            this.ost_on_date.VisibleIndex = 3;
            // 
            // inventory
            // 
            this.inventory.Caption = "Переучёт";
            this.inventory.DisplayFormat.FormatString = "N3";
            this.inventory.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.inventory.FieldName = "inventory";
            this.inventory.Name = "inventory";
            this.inventory.Visible = true;
            this.inventory.VisibleIndex = 4;
            // 
            // diff
            // 
            this.diff.Caption = "Разница";
            this.diff.DisplayFormat.FormatString = "N0";
            this.diff.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.diff.FieldName = "diff";
            this.diff.Name = "diff";
            this.diff.OptionsColumn.ReadOnly = true;
            this.diff.Visible = true;
            this.diff.VisibleIndex = 5;
            // 
            // plan_realiz
            // 
            this.plan_realiz.Caption = "Плановая реализация";
            this.plan_realiz.DisplayFormat.FormatString = "N0";
            this.plan_realiz.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.plan_realiz.FieldName = "plan_realiz";
            this.plan_realiz.Name = "plan_realiz";
            this.plan_realiz.OptionsColumn.ReadOnly = true;
            this.plan_realiz.Visible = true;
            this.plan_realiz.VisibleIndex = 6;
            // 
            // zakaz
            // 
            this.zakaz.Caption = "Расчёт заказа";
            this.zakaz.DisplayFormat.FormatString = "N0";
            this.zakaz.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.zakaz.FieldName = "zakaz";
            this.zakaz.Name = "zakaz";
            this.zakaz.OptionsColumn.ReadOnly = true;
            this.zakaz.Visible = true;
            this.zakaz.VisibleIndex = 7;
            // 
            // perezatarka
            // 
            this.perezatarka.Caption = "Перезатарка";
            this.perezatarka.DisplayFormat.FormatString = "N3";
            this.perezatarka.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.perezatarka.FieldName = "perezatarka";
            this.perezatarka.Name = "perezatarka";
            this.perezatarka.OptionsColumn.ReadOnly = true;
            this.perezatarka.Visible = true;
            this.perezatarka.VisibleIndex = 8;
            // 
            // perezatarka_zal
            // 
            this.perezatarka_zal.Caption = "Перезатарка зал";
            this.perezatarka_zal.DisplayFormat.FormatString = "N3";
            this.perezatarka_zal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.perezatarka_zal.FieldName = "perezatarka_zal";
            this.perezatarka_zal.Name = "perezatarka_zal";
            this.perezatarka_zal.OptionsColumn.ReadOnly = true;
            this.perezatarka_zal.Visible = true;
            this.perezatarka_zal.VisibleIndex = 9;
            // 
            // rcena
            // 
            this.rcena.Caption = "Цена пр.";
            this.rcena.FieldName = "rcena";
            this.rcena.Name = "rcena";
            this.rcena.OptionsColumn.AllowEdit = false;
            this.rcena.Visible = true;
            this.rcena.VisibleIndex = 11;
            // 
            // spisanie
            // 
            this.spisanie.Caption = "Списание";
            this.spisanie.FieldName = "spisanie";
            this.spisanie.Name = "spisanie";
            this.spisanie.OptionsColumn.AllowEdit = false;
            this.spisanie.Visible = true;
            this.spisanie.VisibleIndex = 12;
            // 
            // id_grp3
            // 
            this.id_grp3.Caption = "Вид";
            this.id_grp3.ColumnEdit = this.repCmbGrp3;
            this.id_grp3.FieldName = "id_grp3";
            this.id_grp3.Name = "id_grp3";
            this.id_grp3.Visible = true;
            this.id_grp3.VisibleIndex = 13;
            this.id_grp3.Width = 92;
            // 
            // repCmbGrp3
            // 
            this.repCmbGrp3.AutoHeight = false;
            this.repCmbGrp3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbGrp3.DisplayMember = "cname";
            this.repCmbGrp3.Name = "repCmbGrp3";
            this.repCmbGrp3.PopupFormSize = new System.Drawing.Size(30, 30);
            this.repCmbGrp3.ShowFooter = false;
            this.repCmbGrp3.ShowPopupShadow = false;
            this.repCmbGrp3.ValueMember = "id";
            this.repCmbGrp3.View = this.repositoryItemGridLookUpEdit1View;
            this.repCmbGrp3.EditValueChanged += new System.EventHandler(this.repCmbGrp3_EditValueChanged);
            this.repCmbGrp3.Leave += new System.EventHandler(this.repCmbGrp3_Leave);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.FieldName = "cname";
            this.gridColumn2.MinWidth = 10;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 10;
            // 
            // zakaz_manager
            // 
            this.zakaz_manager.Caption = "Заказ менеджера";
            this.zakaz_manager.DisplayFormat.FormatString = "N0";
            this.zakaz_manager.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.zakaz_manager.FieldName = "zakaz_manager";
            this.zakaz_manager.Name = "zakaz_manager";
            this.zakaz_manager.Visible = true;
            this.zakaz_manager.VisibleIndex = 10;
            // 
            // id
            // 
            this.id.Caption = "id";
            this.id.FieldName = "id";
            this.id.Name = "id";
            // 
            // nzatar
            // 
            this.nzatar.Caption = "nzatar";
            this.nzatar.Name = "nzatar";
            // 
            // orderBody
            // 
            this.orderBody.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.orderBody.GridControl = this.grdMain;
            this.orderBody.Name = "orderBody";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ТУ группа:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(642, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Подгруппа:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "EAN:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(253, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Наименование товара:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(309, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Поставщик:";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(54, 13);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(92, 20);
            this.dtpDate.TabIndex = 7;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // cmbTUGroups
            // 
            this.cmbTUGroups.DisplayMember = "cname";
            this.cmbTUGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTUGroups.FormattingEnabled = true;
            this.cmbTUGroups.Location = new System.Drawing.Point(324, 8);
            this.cmbTUGroups.Name = "cmbTUGroups";
            this.cmbTUGroups.Size = new System.Drawing.Size(121, 21);
            this.cmbTUGroups.TabIndex = 8;
            this.cmbTUGroups.ValueMember = "id";
            this.cmbTUGroups.SelectedValueChanged += new System.EventHandler(this.cmbTUGroups_SelectedValueChanged);
            // 
            // cmbSubGroups
            // 
            this.cmbSubGroups.DisplayMember = "cname";
            this.cmbSubGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubGroups.FormattingEnabled = true;
            this.cmbSubGroups.Location = new System.Drawing.Point(712, 8);
            this.cmbSubGroups.Name = "cmbSubGroups";
            this.cmbSubGroups.Size = new System.Drawing.Size(121, 21);
            this.cmbSubGroups.TabIndex = 9;
            this.cmbSubGroups.ValueMember = "id";
            this.cmbSubGroups.SelectedValueChanged += new System.EventHandler(this.cmbSubGroups_SelectedValueChanged);
            // 
            // txtEAN
            // 
            this.txtEAN.Location = new System.Drawing.Point(54, 39);
            this.txtEAN.MaxLength = 13;
            this.txtEAN.Name = "txtEAN";
            this.txtEAN.Size = new System.Drawing.Size(109, 20);
            this.txtEAN.TabIndex = 10;
            this.toolTips.SetToolTip(this.txtEAN, "Поиск по EAN");
            this.txtEAN.TextChanged += new System.EventHandler(this.txtEAN_TextChanged);
            this.txtEAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEAN_KeyPress);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(383, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(534, 20);
            this.txtName.TabIndex = 11;
            this.toolTips.SetToolTip(this.txtName, "Поиск по наименованию товара");
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // txtPostName
            // 
            this.txtPostName.Location = new System.Drawing.Point(383, 67);
            this.txtPostName.Name = "txtPostName";
            this.txtPostName.Size = new System.Drawing.Size(534, 20);
            this.txtPostName.TabIndex = 12;
            this.toolTips.SetToolTip(this.txtPostName, "Поиск по поставщику");
            this.txtPostName.Click += new System.EventHandler(this.txtPostName_Click);
            // 
            // btnClearName
            // 
            this.btnClearName.Image = global::Requests.Properties.Resources.delete;
            this.btnClearName.Location = new System.Drawing.Point(923, 33);
            this.btnClearName.Name = "btnClearName";
            this.btnClearName.Size = new System.Drawing.Size(26, 26);
            this.btnClearName.TabIndex = 13;
            this.toolTips.SetToolTip(this.btnClearName, "Очистить фильтр");
            this.btnClearName.UseVisualStyleBackColor = true;
            this.btnClearName.Click += new System.EventHandler(this.btnClearName_Click);
            // 
            // btnClearPost
            // 
            this.btnClearPost.Image = global::Requests.Properties.Resources.delete;
            this.btnClearPost.Location = new System.Drawing.Point(923, 64);
            this.btnClearPost.Name = "btnClearPost";
            this.btnClearPost.Size = new System.Drawing.Size(26, 26);
            this.btnClearPost.TabIndex = 14;
            this.toolTips.SetToolTip(this.btnClearPost, "Очистить фильтр");
            this.btnClearPost.UseVisualStyleBackColor = true;
            this.btnClearPost.Click += new System.EventHandler(this.btnClearPost_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = global::Requests.Properties.Resources.pict_close;
            this.btnExit.Location = new System.Drawing.Point(923, 623);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 15;
            this.toolTips.SetToolTip(this.btnExit, "Выход");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnToExcel
            // 
            this.btnToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToExcel.Image = global::Requests.Properties.Resources.pict_excel;
            this.btnToExcel.Location = new System.Drawing.Point(923, 579);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(32, 32);
            this.btnToExcel.TabIndex = 16;
            this.toolTips.SetToolTip(this.btnToExcel, "Выгрузить в Excel");
            this.btnToExcel.UseVisualStyleBackColor = true;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::Requests.Properties.Resources.pict_save;
            this.btnSave.Location = new System.Drawing.Point(885, 579);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(32, 32);
            this.btnSave.TabIndex = 17;
            this.toolTips.SetToolTip(this.btnSave, "Сохранить");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCreateRequests
            // 
            this.btnCreateRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateRequests.Image = global::Requests.Properties.Resources.copy_8101;
            this.btnCreateRequests.Location = new System.Drawing.Point(847, 579);
            this.btnCreateRequests.Name = "btnCreateRequests";
            this.btnCreateRequests.Size = new System.Drawing.Size(32, 32);
            this.btnCreateRequests.TabIndex = 18;
            this.toolTips.SetToolTip(this.btnCreateRequests, "Создать заявки");
            this.btnCreateRequests.UseVisualStyleBackColor = true;
            this.btnCreateRequests.Click += new System.EventHandler(this.btnCreateRequests_Click);
            // 
            // btnCreateActInv
            // 
            this.btnCreateActInv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateActInv.Image = global::Requests.Properties.Resources.pencil;
            this.btnCreateActInv.Location = new System.Drawing.Point(809, 579);
            this.btnCreateActInv.Name = "btnCreateActInv";
            this.btnCreateActInv.Size = new System.Drawing.Size(32, 32);
            this.btnCreateActInv.TabIndex = 19;
            this.toolTips.SetToolTip(this.btnCreateActInv, "Создать акт инвентаризации");
            this.btnCreateActInv.UseVisualStyleBackColor = true;
            this.btnCreateActInv.Click += new System.EventHandler(this.btnCreateActInv_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Image = global::Requests.Properties.Resources.pict_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(12, 579);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 20;
            this.toolTips.SetToolTip(this.btnRefresh, "Обновить");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSettings.Image = global::Requests.Properties.Resources.setting_tools;
            this.btnSettings.Location = new System.Drawing.Point(50, 579);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(32, 32);
            this.btnSettings.TabIndex = 21;
            this.toolTips.SetToolTip(this.btnSettings, "Настройки");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // txtInserter
            // 
            this.txtInserter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInserter.Location = new System.Drawing.Point(228, 579);
            this.txtInserter.Name = "txtInserter";
            this.txtInserter.ReadOnly = true;
            this.txtInserter.Size = new System.Drawing.Size(128, 20);
            this.txtInserter.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(169, 582);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Добавил:";
            // 
            // txtEditor
            // 
            this.txtEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtEditor.Location = new System.Drawing.Point(464, 579);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.ReadOnly = true;
            this.txtEditor.Size = new System.Drawing.Size(128, 20);
            this.txtEditor.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(376, 582);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Редактировал:";
            // 
            // txtDateInsert
            // 
            this.txtDateInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDateInsert.Location = new System.Drawing.Point(228, 601);
            this.txtDateInsert.Name = "txtDateInsert";
            this.txtDateInsert.ReadOnly = true;
            this.txtDateInsert.Size = new System.Drawing.Size(128, 20);
            this.txtDateInsert.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(169, 604);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Дата доб.";
            // 
            // txtDateEdit
            // 
            this.txtDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDateEdit.Location = new System.Drawing.Point(464, 601);
            this.txtDateEdit.Name = "txtDateEdit";
            this.txtDateEdit.ReadOnly = true;
            this.txtDateEdit.Size = new System.Drawing.Size(128, 20);
            this.txtDateEdit.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(401, 604);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Дата ред.";
            // 
            // pnlHasZatar
            // 
            this.pnlHasZatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlHasZatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pnlHasZatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHasZatar.Location = new System.Drawing.Point(14, 644);
            this.pnlHasZatar.Name = "pnlHasZatar";
            this.pnlHasZatar.Size = new System.Drawing.Size(15, 15);
            this.pnlHasZatar.TabIndex = 30;
            // 
            // pnlSelected
            // 
            this.pnlSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(120)))));
            this.pnlSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelected.Location = new System.Drawing.Point(14, 623);
            this.pnlSelected.Name = "pnlSelected";
            this.pnlSelected.Size = new System.Drawing.Size(15, 15);
            this.pnlSelected.TabIndex = 31;
            // 
            // pnlHasRequest
            // 
            this.pnlHasRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlHasRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlHasRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHasRequest.Location = new System.Drawing.Point(115, 644);
            this.pnlHasRequest.Name = "pnlHasRequest";
            this.pnlHasRequest.Size = new System.Drawing.Size(15, 15);
            this.pnlHasRequest.TabIndex = 32;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 646);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "есть затарка";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(136, 646);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "заявка создана";
            // 
            // cbSelected
            // 
            this.cbSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSelected.AutoSize = true;
            this.cbSelected.Location = new System.Drawing.Point(35, 623);
            this.cbSelected.Name = "cbSelected";
            this.cbSelected.Size = new System.Drawing.Size(154, 17);
            this.cbSelected.TabIndex = 35;
            this.cbSelected.Text = "подтверждённые заказы";
            this.cbSelected.UseVisualStyleBackColor = true;
            this.cbSelected.CheckedChanged += new System.EventHandler(this.cbSelected_CheckedChanged);
            // 
            // btnMultiTU
            // 
            this.btnMultiTU.Location = new System.Drawing.Point(451, 7);
            this.btnMultiTU.Name = "btnMultiTU";
            this.btnMultiTU.Size = new System.Drawing.Size(32, 23);
            this.btnMultiTU.TabIndex = 36;
            this.btnMultiTU.Text = "...";
            this.toolTips.SetToolTip(this.btnMultiTU, "Множественный выбор ТУ групп");
            this.btnMultiTU.UseVisualStyleBackColor = true;
            this.btnMultiTU.Click += new System.EventHandler(this.btnMultiTU_Click);
            // 
            // cmsRealiz
            // 
            this.cmsRealiz.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRealiz});
            this.cmsRealiz.Name = "cmsRealiz";
            this.cmsRealiz.Size = new System.Drawing.Size(196, 26);
            this.cmsRealiz.Text = "Реализация по товару";
            // 
            // tsmRealiz
            // 
            this.tsmRealiz.Name = "tsmRealiz";
            this.tsmRealiz.Size = new System.Drawing.Size(195, 22);
            this.tsmRealiz.Text = "Реализация по товару";
            this.tsmRealiz.Click += new System.EventHandler(this.tsmRealiz_Click);
            // 
            // cmsInventory
            // 
            this.cmsInventory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmInventory});
            this.cmsInventory.Name = "cmsInventory";
            this.cmsInventory.Size = new System.Drawing.Size(127, 26);
            // 
            // tsmInventory
            // 
            this.tsmInventory.Name = "tsmInventory";
            this.tsmInventory.Size = new System.Drawing.Size(126, 22);
            this.tsmInventory.Text = "Переучёт";
            this.tsmInventory.Click += new System.EventHandler(this.tsmInventory_Click);
            // 
            // cmsPosts
            // 
            this.cmsPosts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddPost,
            this.tsmDelPost});
            this.cmsPosts.Name = "cmsPosts";
            this.cmsPosts.Size = new System.Drawing.Size(197, 48);
            // 
            // tsmAddPost
            // 
            this.tsmAddPost.Name = "tsmAddPost";
            this.tsmAddPost.Size = new System.Drawing.Size(196, 22);
            this.tsmAddPost.Text = "Добавить поставщика";
            this.tsmAddPost.Click += new System.EventHandler(this.tsmAddPost_Click);
            // 
            // tsmDelPost
            // 
            this.tsmDelPost.Name = "tsmDelPost";
            this.tsmDelPost.Size = new System.Drawing.Size(196, 22);
            this.tsmDelPost.Text = "Удалить поставщика";
            this.tsmDelPost.Click += new System.EventHandler(this.tsmDelPost_Click);
            // 
            // cmsSubjects
            // 
            this.cmsSubjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSubjects});
            this.cmsSubjects.Name = "cmsSubjects";
            this.cmsSubjects.Size = new System.Drawing.Size(210, 26);
            // 
            // tsmSubjects
            // 
            this.tsmSubjects.Name = "tsmSubjects";
            this.tsmSubjects.Size = new System.Drawing.Size(209, 22);
            this.tsmSubjects.Text = "Выбрать страну/субъект";
            this.tsmSubjects.Click += new System.EventHandler(this.tsmSubjects_Click);
            // 
            // bgwLoad
            // 
            this.bgwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoad_DoWork);
            this.bgwLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoad_RunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbInv);
            this.groupBox1.Controls.Add(this.rbOst);
            this.groupBox1.Location = new System.Drawing.Point(81, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 36);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // rbInv
            // 
            this.rbInv.AutoSize = true;
            this.rbInv.Location = new System.Drawing.Point(110, 13);
            this.rbInv.Name = "rbInv";
            this.rbInv.Size = new System.Drawing.Size(75, 17);
            this.rbInv.TabIndex = 0;
            this.rbInv.Text = "переучёту";
            this.rbInv.UseVisualStyleBackColor = true;
            this.rbInv.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // rbOst
            // 
            this.rbOst.AutoSize = true;
            this.rbOst.Checked = true;
            this.rbOst.Location = new System.Drawing.Point(19, 13);
            this.rbOst.Name = "rbOst";
            this.rbOst.Size = new System.Drawing.Size(73, 17);
            this.rbOst.TabIndex = 0;
            this.rbOst.TabStop = true;
            this.rbOst.Text = "остаткам";
            this.rbOst.UseVisualStyleBackColor = true;
            this.rbOst.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "Расчёт по :";
            // 
            // frmMainZakaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 668);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnMultiTU);
            this.Controls.Add(this.cbSelected);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pnlHasRequest);
            this.Controls.Add(this.pnlSelected);
            this.Controls.Add(this.pnlHasZatar);
            this.Controls.Add(this.txtDateEdit);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDateInsert);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEditor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtInserter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCreateActInv);
            this.Controls.Add(this.btnCreateRequests);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnToExcel);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClearPost);
            this.Controls.Add(this.btnClearName);
            this.Controls.Add(this.txtPostName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtEAN);
            this.Controls.Add(this.cmbSubGroups);
            this.Controls.Add(this.cmbTUGroups);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdMain);
            this.Name = "frmMainZakaz";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Основной заказ";
            this.Load += new System.EventHandler(this.frmMainZakaz_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCbIsSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtFactNetto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbGrp3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).EndInit();
            this.cmsRealiz.ResumeLayout(false);
            this.cmsInventory.ResumeLayout(false);
            this.cmsPosts.ResumeLayout(false);
            this.cmsSubjects.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grdHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cmbTUGroups;
        private System.Windows.Forms.ComboBox cmbSubGroups;
        private System.Windows.Forms.TextBox txtEAN;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPostName;
        private System.Windows.Forms.Button btnClearName;
        private System.Windows.Forms.Button btnClearPost;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnToExcel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCreateRequests;
        private System.Windows.Forms.Button btnCreateActInv;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.TextBox txtInserter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDateInsert;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDateEdit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlHasZatar;
        private System.Windows.Forms.Panel pnlSelected;
        private System.Windows.Forms.Panel pnlHasRequest;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbSelected;
        private System.Windows.Forms.ToolTip toolTips;
        private DevExpress.XtraGrid.Views.Grid.GridView grdBody;
        private DevExpress.XtraGrid.Columns.GridColumn check;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repCbIsSelect;
        private DevExpress.XtraGrid.Columns.GridColumn post_name;
        private DevExpress.XtraGrid.Columns.GridColumn fact_netto;
        private DevExpress.XtraGrid.Columns.GridColumn caliber;
        private DevExpress.XtraGrid.Columns.GridColumn zcena;
        private DevExpress.XtraGrid.Columns.GridColumn subject_name;
        private DevExpress.XtraGrid.Columns.GridColumn id_trequest;
        private DevExpress.XtraGrid.Columns.GridColumn ean;
        private DevExpress.XtraGrid.Columns.GridColumn cname;
        private DevExpress.XtraGrid.Columns.GridColumn sred_rashod;
        private DevExpress.XtraGrid.Columns.GridColumn ost_on_date;
        private DevExpress.XtraGrid.Columns.GridColumn inventory;
        private DevExpress.XtraGrid.Columns.GridColumn diff;
        private DevExpress.XtraGrid.Columns.GridColumn plan_realiz;
        private DevExpress.XtraGrid.Columns.GridColumn zakaz;
        private DevExpress.XtraGrid.Columns.GridColumn perezatarka;
        private DevExpress.XtraGrid.Columns.GridColumn perezatarka_zal;
        private DevExpress.XtraGrid.Columns.GridColumn id_grp3;
        private DevExpress.XtraGrid.Columns.GridColumn zakaz_manager;
        private System.Windows.Forms.BindingSource ordersBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn id_tovar;
        private DevExpress.XtraGrid.Columns.GridColumn id;
        private DevExpress.XtraGrid.Views.Grid.GridView orderBody;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn id_order;
        private DevExpress.XtraGrid.Columns.GridColumn id_order_body;
        private System.Windows.Forms.Button btnMultiTU;
        private System.Windows.Forms.ContextMenuStrip cmsRealiz;
        private System.Windows.Forms.ToolStripMenuItem tsmRealiz;
        private System.Windows.Forms.ContextMenuStrip cmsInventory;
        private System.Windows.Forms.ToolStripMenuItem tsmInventory;
        private System.Windows.Forms.ContextMenuStrip cmsPosts;
        private System.Windows.Forms.ToolStripMenuItem tsmAddPost;
        private System.Windows.Forms.ToolStripMenuItem tsmDelPost;
        private System.Windows.Forms.ContextMenuStrip cmsSubjects;
        private System.Windows.Forms.ToolStripMenuItem tsmSubjects;
        private DevExpress.XtraGrid.Columns.GridColumn nzatar;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repCmbGrp3;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.ComponentModel.BackgroundWorker bgwLoad;
        private DevExpress.XtraGrid.Columns.GridColumn rcena;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtFactNetto;
        private DevExpress.XtraGrid.Columns.GridColumn spisanie;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton rbInv;
        private System.Windows.Forms.RadioButton rbOst;
    }
}