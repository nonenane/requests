namespace Requests
{
    partial class frmEditGoods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditGoods));
            this.pnEan = new System.Windows.Forms.Panel();
            this.tbEan = new System.Windows.Forms.TextBox();
            this.pnName = new System.Windows.Forms.Panel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.cbSubject = new System.Windows.Forms.ComboBox();
            this.lbEan = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbCountry = new System.Windows.Forms.Label();
            this.pnOst = new System.Windows.Forms.Panel();
            this.lbOst = new System.Windows.Forms.Label();
            this.lbDay = new System.Windows.Forms.Label();
            this.lbZapas = new System.Windows.Forms.Label();
            this.tbZapas = new System.Windows.Forms.TextBox();
            this.tbOst = new System.Windows.Forms.TextBox();
            this.tbShelfSpace = new System.Windows.Forms.TextBox();
            this.pnZcena = new System.Windows.Forms.Panel();
            this.tbZcenabnds = new System.Windows.Forms.TextBox();
            this.tbZcena = new System.Windows.Forms.TextBox();
            this.pnKol = new System.Windows.Forms.Panel();
            this.tbZatar = new System.Windows.Forms.TextBox();
            this.tbTara = new System.Windows.Forms.TextBox();
            this.tbZakaz = new System.Windows.Forms.TextBox();
            this.lbTara = new System.Windows.Forms.Label();
            this.lbZakaz = new System.Windows.Forms.Label();
            this.lbZatar = new System.Windows.Forms.Label();
            this.pnRcena = new System.Windows.Forms.Panel();
            this.tbRcena = new System.Windows.Forms.TextBox();
            this.lbBnds = new System.Windows.Forms.Label();
            this.lbWnds = new System.Windows.Forms.Label();
            this.lbZcena = new System.Windows.Forms.Label();
            this.lbRcena = new System.Windows.Forms.Label();
            this.lbZapas2 = new System.Windows.Forms.Label();
            this.tbZapas2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCzak = new System.Windows.Forms.TextBox();
            this.lbZakazCom = new System.Windows.Forms.Label();
            this.chkCreateLocalCode = new System.Windows.Forms.CheckBox();
            this.chkPlanRealizDay = new System.Windows.Forms.CheckBox();
            this.tbDayPlanReal = new System.Windows.Forms.TextBox();
            this.chkOtsechDays = new System.Windows.Forms.CheckBox();
            this.dtpOtsechStart = new System.Windows.Forms.DateTimePicker();
            this.dtpOtsechFinish = new System.Windows.Forms.DateTimePicker();
            this.lbOtsechFin = new System.Windows.Forms.Label();
            this.lbShelf = new System.Windows.Forms.Label();
            this.lbPeriodOfStorage = new System.Windows.Forms.Label();
            this.tbPeriodOfStorage = new System.Windows.Forms.TextBox();
            this.lbPrimech = new System.Windows.Forms.Label();
            this.tbPrimech = new System.Windows.Forms.TextBox();
            this.lbUnit = new System.Windows.Forms.Label();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.lbNds = new System.Windows.Forms.Label();
            this.cbNds = new System.Windows.Forms.ComboBox();
            this.lbTU = new System.Windows.Forms.Label();
            this.cbTU = new System.Windows.Forms.ComboBox();
            this.lbInvGrp = new System.Windows.Forms.Label();
            this.cbInvGrp = new System.Windows.Forms.ComboBox();
            this.ttEditGoods = new System.Windows.Forms.ToolTip(this.components);
            this.btReturn = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.lbPercent = new System.Windows.Forms.Label();
            this.tbPercent = new System.Windows.Forms.TextBox();
            this.tbBrutto = new System.Windows.Forms.TextBox();
            this.lbBrutto = new System.Windows.Forms.Label();
            this.chbIsTransparent = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbZapasM2 = new System.Windows.Forms.TextBox();
            this.tbOstM2 = new System.Windows.Forms.TextBox();
            this.btSelectTovar = new System.Windows.Forms.Button();
            this.pnEan.SuspendLayout();
            this.pnName.SuspendLayout();
            this.pnOst.SuspendLayout();
            this.pnZcena.SuspendLayout();
            this.pnKol.SuspendLayout();
            this.pnRcena.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnEan
            // 
            this.pnEan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnEan.Controls.Add(this.tbEan);
            this.pnEan.Location = new System.Drawing.Point(12, 12);
            this.pnEan.Name = "pnEan";
            this.pnEan.Size = new System.Drawing.Size(100, 33);
            this.pnEan.TabIndex = 1;
            // 
            // tbEan
            // 
            this.tbEan.Location = new System.Drawing.Point(3, 7);
            this.tbEan.MaxLength = 13;
            this.tbEan.Name = "tbEan";
            this.tbEan.Size = new System.Drawing.Size(90, 20);
            this.tbEan.TabIndex = 0;
            this.tbEan.TabStop = false;
            this.tbEan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbEan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbEan_MouseClick);
            this.tbEan.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbEan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEan_KeyPress);
            this.tbEan.Validated += new System.EventHandler(this.tbEan_Validated);
            // 
            // pnName
            // 
            this.pnName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnName.Controls.Add(this.tbName);
            this.pnName.Location = new System.Drawing.Point(111, 12);
            this.pnName.Name = "pnName";
            this.pnName.Size = new System.Drawing.Size(533, 33);
            this.pnName.TabIndex = 0;
            // 
            // tbName
            // 
            this.tbName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbName.Location = new System.Drawing.Point(6, 7);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(522, 20);
            this.tbName.TabIndex = 1;
            this.tbName.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // cbSubject
            // 
            this.cbSubject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSubject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubject.FormattingEnabled = true;
            this.cbSubject.Location = new System.Drawing.Point(119, 51);
            this.cbSubject.Name = "cbSubject";
            this.cbSubject.Size = new System.Drawing.Size(525, 21);
            this.cbSubject.TabIndex = 2;
            this.cbSubject.SelectionChangeCommitted += new System.EventHandler(this.cbSubject_SelectionChangeCommitted);
            // 
            // lbEan
            // 
            this.lbEan.AutoSize = true;
            this.lbEan.Location = new System.Drawing.Point(26, 4);
            this.lbEan.Name = "lbEan";
            this.lbEan.Size = new System.Drawing.Size(67, 13);
            this.lbEan.TabIndex = 0;
            this.lbEan.Text = "Код товара:";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(129, 4);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(124, 13);
            this.lbName.TabIndex = 3;
            this.lbName.Text = "Наименование товара:";
            // 
            // lbCountry
            // 
            this.lbCountry.AutoSize = true;
            this.lbCountry.Location = new System.Drawing.Point(19, 54);
            this.lbCountry.Name = "lbCountry";
            this.lbCountry.Size = new System.Drawing.Size(93, 13);
            this.lbCountry.TabIndex = 4;
            this.lbCountry.Text = "Страна/Субъект:";
            // 
            // pnOst
            // 
            this.pnOst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnOst.Controls.Add(this.lbOst);
            this.pnOst.Controls.Add(this.lbDay);
            this.pnOst.Controls.Add(this.lbZapas);
            this.pnOst.Controls.Add(this.tbZapas);
            this.pnOst.Controls.Add(this.tbOst);
            this.pnOst.Location = new System.Drawing.Point(12, 91);
            this.pnOst.Name = "pnOst";
            this.pnOst.Size = new System.Drawing.Size(137, 57);
            this.pnOst.TabIndex = 3;
            // 
            // lbOst
            // 
            this.lbOst.AutoSize = true;
            this.lbOst.Location = new System.Drawing.Point(3, 6);
            this.lbOst.Name = "lbOst";
            this.lbOst.Size = new System.Drawing.Size(52, 13);
            this.lbOst.TabIndex = 0;
            this.lbOst.Text = "Тек. ост.";
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Location = new System.Drawing.Point(101, 32);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(31, 13);
            this.lbDay.TabIndex = 7;
            this.lbDay.Text = "дней";
            // 
            // lbZapas
            // 
            this.lbZapas.AutoSize = true;
            this.lbZapas.Location = new System.Drawing.Point(17, 32);
            this.lbZapas.Name = "lbZapas";
            this.lbZapas.Size = new System.Drawing.Size(38, 13);
            this.lbZapas.TabIndex = 7;
            this.lbZapas.Text = "Запас";
            // 
            // tbZapas
            // 
            this.tbZapas.Location = new System.Drawing.Point(61, 29);
            this.tbZapas.MaxLength = 3;
            this.tbZapas.Name = "tbZapas";
            this.tbZapas.Size = new System.Drawing.Size(38, 20);
            this.tbZapas.TabIndex = 1;
            this.tbZapas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbZapas.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbZapas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbZapas_KeyPress);
            this.tbZapas.Validating += new System.ComponentModel.CancelEventHandler(this.tbZapas_Validating);
            this.tbZapas.Validated += new System.EventHandler(this.tbZapas_Validated);
            // 
            // tbOst
            // 
            this.tbOst.Location = new System.Drawing.Point(61, 3);
            this.tbOst.MaxLength = 11;
            this.tbOst.Name = "tbOst";
            this.tbOst.Size = new System.Drawing.Size(71, 20);
            this.tbOst.TabIndex = 1;
            this.tbOst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbOst.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbOst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOst_KeyPress);
            this.tbOst.Validating += new System.ComponentModel.CancelEventHandler(this.tbOst_Validating);
            // 
            // tbShelfSpace
            // 
            this.tbShelfSpace.Location = new System.Drawing.Point(148, 207);
            this.tbShelfSpace.Name = "tbShelfSpace";
            this.tbShelfSpace.Size = new System.Drawing.Size(63, 20);
            this.tbShelfSpace.TabIndex = 15;
            this.tbShelfSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbShelfSpace.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbShelfSpace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbShelfSpace_KeyPress);
            this.tbShelfSpace.Validating += new System.ComponentModel.CancelEventHandler(this.tbShelfSpace_Validating);
            // 
            // pnZcena
            // 
            this.pnZcena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnZcena.Controls.Add(this.tbZcenabnds);
            this.pnZcena.Controls.Add(this.tbZcena);
            this.pnZcena.Location = new System.Drawing.Point(445, 112);
            this.pnZcena.Name = "pnZcena";
            this.pnZcena.Size = new System.Drawing.Size(140, 36);
            this.pnZcena.TabIndex = 5;
            // 
            // tbZcenabnds
            // 
            this.tbZcenabnds.Location = new System.Drawing.Point(3, 8);
            this.tbZcenabnds.MaxLength = 12;
            this.tbZcenabnds.Name = "tbZcenabnds";
            this.tbZcenabnds.Size = new System.Drawing.Size(63, 20);
            this.tbZcenabnds.TabIndex = 0;
            this.tbZcenabnds.Text = "0,0000";
            this.tbZcenabnds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbZcenabnds.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbZcenabnds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbZcenabnds_KeyPress);
            this.tbZcenabnds.Validating += new System.ComponentModel.CancelEventHandler(this.tbZcenabnds_Validating);
            this.tbZcenabnds.Validated += new System.EventHandler(this.tbZcenabnds_Validated);
            // 
            // tbZcena
            // 
            this.tbZcena.Location = new System.Drawing.Point(72, 8);
            this.tbZcena.MaxLength = 12;
            this.tbZcena.Name = "tbZcena";
            this.tbZcena.Size = new System.Drawing.Size(63, 20);
            this.tbZcena.TabIndex = 1;
            this.tbZcena.Text = "0,0000";
            this.tbZcena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbZcena.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbZcena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbZcena_KeyPress);
            this.tbZcena.Validating += new System.ComponentModel.CancelEventHandler(this.tbZcena_Validating);
            this.tbZcena.Validated += new System.EventHandler(this.tbZcena_Validated);
            // 
            // pnKol
            // 
            this.pnKol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnKol.Controls.Add(this.tbZatar);
            this.pnKol.Controls.Add(this.tbTara);
            this.pnKol.Controls.Add(this.tbZakaz);
            this.pnKol.Controls.Add(this.lbTara);
            this.pnKol.Controls.Add(this.lbZakaz);
            this.pnKol.Controls.Add(this.lbZatar);
            this.pnKol.Location = new System.Drawing.Point(307, 82);
            this.pnKol.Name = "pnKol";
            this.pnKol.Size = new System.Drawing.Size(136, 66);
            this.pnKol.TabIndex = 4;
            // 
            // tbZatar
            // 
            this.tbZatar.Location = new System.Drawing.Point(71, 3);
            this.tbZatar.MaxLength = 8;
            this.tbZatar.Name = "tbZatar";
            this.tbZatar.Size = new System.Drawing.Size(56, 20);
            this.tbZatar.TabIndex = 0;
            this.tbZatar.Text = "0";
            this.tbZatar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbZatar.TextChanged += new System.EventHandler(this.tbZatar_TextChanged);
            this.tbZatar.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbZatar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbZatar_KeyPress);
            this.tbZatar.Leave += new System.EventHandler(this.tbZatar_Leave);
            this.tbZatar.Validating += new System.ComponentModel.CancelEventHandler(this.tbZatar_Validating);
            this.tbZatar.Validated += new System.EventHandler(this.tbZatar_Validated);
            // 
            // tbTara
            // 
            this.tbTara.Location = new System.Drawing.Point(3, 38);
            this.tbTara.MaxLength = 4;
            this.tbTara.Name = "tbTara";
            this.tbTara.Size = new System.Drawing.Size(50, 20);
            this.tbTara.TabIndex = 1;
            this.tbTara.Text = "0";
            this.tbTara.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTara.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbTara.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTara_KeyPress);
            this.tbTara.Validating += new System.ComponentModel.CancelEventHandler(this.tbTara_Validating);
            this.tbTara.Validated += new System.EventHandler(this.tbTara_Validated);
            // 
            // tbZakaz
            // 
            this.tbZakaz.Location = new System.Drawing.Point(59, 38);
            this.tbZakaz.Name = "tbZakaz";
            this.tbZakaz.Size = new System.Drawing.Size(68, 20);
            this.tbZakaz.TabIndex = 2;
            this.tbZakaz.Text = "0";
            this.tbZakaz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbZakaz.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbZakaz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbZakaz_KeyPress);
            this.tbZakaz.Leave += new System.EventHandler(this.tbZakaz_Leave);
            this.tbZakaz.Validating += new System.ComponentModel.CancelEventHandler(this.tbZakaz_Validating);
            this.tbZakaz.Validated += new System.EventHandler(this.tbZakaz_Validated);
            // 
            // lbTara
            // 
            this.lbTara.AutoSize = true;
            this.lbTara.Location = new System.Drawing.Point(6, 22);
            this.lbTara.Name = "lbTara";
            this.lbTara.Size = new System.Drawing.Size(32, 13);
            this.lbTara.TabIndex = 7;
            this.lbTara.Text = "Тара";
            // 
            // lbZakaz
            // 
            this.lbZakaz.AutoSize = true;
            this.lbZakaz.Location = new System.Drawing.Point(74, 23);
            this.lbZakaz.Name = "lbZakaz";
            this.lbZakaz.Size = new System.Drawing.Size(38, 13);
            this.lbZakaz.TabIndex = 7;
            this.lbZakaz.Text = "Заказ";
            // 
            // lbZatar
            // 
            this.lbZatar.AutoSize = true;
            this.lbZatar.Location = new System.Drawing.Point(6, 6);
            this.lbZatar.Name = "lbZatar";
            this.lbZatar.Size = new System.Drawing.Size(49, 13);
            this.lbZatar.TabIndex = 7;
            this.lbZatar.Text = "Затарка";
            // 
            // pnRcena
            // 
            this.pnRcena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnRcena.Controls.Add(this.tbRcena);
            this.pnRcena.Location = new System.Drawing.Point(584, 112);
            this.pnRcena.Name = "pnRcena";
            this.pnRcena.Size = new System.Drawing.Size(60, 36);
            this.pnRcena.TabIndex = 6;
            // 
            // tbRcena
            // 
            this.tbRcena.Location = new System.Drawing.Point(3, 8);
            this.tbRcena.Name = "tbRcena";
            this.tbRcena.Size = new System.Drawing.Size(52, 20);
            this.tbRcena.TabIndex = 0;
            this.tbRcena.Text = "0,00";
            this.tbRcena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRcena.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbRcena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRcena_KeyPress);
            this.tbRcena.Validating += new System.ComponentModel.CancelEventHandler(this.tbRcena_Validating);
            this.tbRcena.Validated += new System.EventHandler(this.tbRcena_Validated);
            // 
            // lbBnds
            // 
            this.lbBnds.AutoSize = true;
            this.lbBnds.Location = new System.Drawing.Point(455, 105);
            this.lbBnds.Name = "lbBnds";
            this.lbBnds.Size = new System.Drawing.Size(52, 13);
            this.lbBnds.TabIndex = 7;
            this.lbBnds.Text = "без НДС";
            // 
            // lbWnds
            // 
            this.lbWnds.AutoSize = true;
            this.lbWnds.Location = new System.Drawing.Point(530, 105);
            this.lbWnds.Name = "lbWnds";
            this.lbWnds.Size = new System.Drawing.Size(40, 13);
            this.lbWnds.TabIndex = 7;
            this.lbWnds.Text = "с НДС";
            // 
            // lbZcena
            // 
            this.lbZcena.AutoSize = true;
            this.lbZcena.Location = new System.Drawing.Point(476, 89);
            this.lbZcena.Name = "lbZcena";
            this.lbZcena.Size = new System.Drawing.Size(77, 13);
            this.lbZcena.TabIndex = 7;
            this.lbZcena.Text = "Цена закупки";
            // 
            // lbRcena
            // 
            this.lbRcena.AutoSize = true;
            this.lbRcena.Location = new System.Drawing.Point(589, 105);
            this.lbRcena.Name = "lbRcena";
            this.lbRcena.Size = new System.Drawing.Size(51, 13);
            this.lbRcena.TabIndex = 5;
            this.lbRcena.Text = "Цена пр.";
            // 
            // lbZapas2
            // 
            this.lbZapas2.AutoSize = true;
            this.lbZapas2.Location = new System.Drawing.Point(24, 158);
            this.lbZapas2.Name = "lbZapas2";
            this.lbZapas2.Size = new System.Drawing.Size(44, 13);
            this.lbZapas2.TabIndex = 7;
            this.lbZapas2.Text = "Запас2";
            // 
            // tbZapas2
            // 
            this.tbZapas2.Enabled = false;
            this.tbZapas2.Location = new System.Drawing.Point(74, 155);
            this.tbZapas2.MaxLength = 3;
            this.tbZapas2.Name = "tbZapas2";
            this.tbZapas2.Size = new System.Drawing.Size(38, 20);
            this.tbZapas2.TabIndex = 7;
            this.tbZapas2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbZapas2.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "дней";
            // 
            // tbCzak
            // 
            this.tbCzak.Enabled = false;
            this.tbCzak.Location = new System.Drawing.Point(229, 155);
            this.tbCzak.Name = "tbCzak";
            this.tbCzak.Size = new System.Drawing.Size(68, 20);
            this.tbCzak.TabIndex = 8;
            this.tbCzak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCzak.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            // 
            // lbZakazCom
            // 
            this.lbZakazCom.AutoSize = true;
            this.lbZakazCom.Location = new System.Drawing.Point(158, 158);
            this.lbZakazCom.Name = "lbZakazCom";
            this.lbZakazCom.Size = new System.Drawing.Size(65, 13);
            this.lbZakazCom.TabIndex = 7;
            this.lbZakazCom.Text = "Заказ общ.";
            // 
            // chkCreateLocalCode
            // 
            this.chkCreateLocalCode.AutoSize = true;
            this.chkCreateLocalCode.Location = new System.Drawing.Point(418, 157);
            this.chkCreateLocalCode.Name = "chkCreateLocalCode";
            this.chkCreateLocalCode.Size = new System.Drawing.Size(167, 17);
            this.chkCreateLocalCode.TabIndex = 9;
            this.chkCreateLocalCode.Text = "Формировать местный код";
            this.chkCreateLocalCode.UseVisualStyleBackColor = true;
            this.chkCreateLocalCode.CheckedChanged += new System.EventHandler(this.chkCreateLocalCode_CheckedChanged);
            // 
            // chkPlanRealizDay
            // 
            this.chkPlanRealizDay.AutoSize = true;
            this.chkPlanRealizDay.Location = new System.Drawing.Point(16, 183);
            this.chkPlanRealizDay.Name = "chkPlanRealizDay";
            this.chkPlanRealizDay.Size = new System.Drawing.Size(133, 17);
            this.chkPlanRealizDay.TabIndex = 10;
            this.chkPlanRealizDay.Text = "План. реализ. в день";
            this.chkPlanRealizDay.UseVisualStyleBackColor = true;
            this.chkPlanRealizDay.CheckedChanged += new System.EventHandler(this.chkPlanRealizDAy_CheckedChanged);
            // 
            // tbDayPlanReal
            // 
            this.tbDayPlanReal.Enabled = false;
            this.tbDayPlanReal.Location = new System.Drawing.Point(148, 181);
            this.tbDayPlanReal.Name = "tbDayPlanReal";
            this.tbDayPlanReal.Size = new System.Drawing.Size(52, 20);
            this.tbDayPlanReal.TabIndex = 11;
            this.tbDayPlanReal.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbDayPlanReal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDayPlanReal_KeyPress);
            this.tbDayPlanReal.Validating += new System.ComponentModel.CancelEventHandler(this.tbDayPlanReal_Validating);
            this.tbDayPlanReal.Validated += new System.EventHandler(this.tbDayPlanReal_Validated);
            // 
            // chkOtsechDays
            // 
            this.chkOtsechDays.AutoSize = true;
            this.chkOtsechDays.Location = new System.Drawing.Point(302, 183);
            this.chkOtsechDays.Name = "chkOtsechDays";
            this.chkOtsechDays.Size = new System.Drawing.Size(116, 17);
            this.chkOtsechDays.TabIndex = 12;
            this.chkOtsechDays.Text = "Период отсечки с";
            this.chkOtsechDays.UseVisualStyleBackColor = true;
            this.chkOtsechDays.CheckedChanged += new System.EventHandler(this.chkOtsechDays_CheckedChanged);
            // 
            // dtpOtsechStart
            // 
            this.dtpOtsechStart.Enabled = false;
            this.dtpOtsechStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOtsechStart.Location = new System.Drawing.Point(418, 183);
            this.dtpOtsechStart.Name = "dtpOtsechStart";
            this.dtpOtsechStart.Size = new System.Drawing.Size(98, 20);
            this.dtpOtsechStart.TabIndex = 13;
            this.dtpOtsechStart.Validated += new System.EventHandler(this.dtpOtsechStart_Validated);
            // 
            // dtpOtsechFinish
            // 
            this.dtpOtsechFinish.Enabled = false;
            this.dtpOtsechFinish.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOtsechFinish.Location = new System.Drawing.Point(542, 183);
            this.dtpOtsechFinish.Name = "dtpOtsechFinish";
            this.dtpOtsechFinish.Size = new System.Drawing.Size(98, 20);
            this.dtpOtsechFinish.TabIndex = 14;
            this.dtpOtsechFinish.Validated += new System.EventHandler(this.dtpOtsechFinish_Validated);
            // 
            // lbOtsechFin
            // 
            this.lbOtsechFin.AutoSize = true;
            this.lbOtsechFin.Location = new System.Drawing.Point(520, 184);
            this.lbOtsechFin.Name = "lbOtsechFin";
            this.lbOtsechFin.Size = new System.Drawing.Size(19, 13);
            this.lbOtsechFin.TabIndex = 7;
            this.lbOtsechFin.Text = "по";
            // 
            // lbShelf
            // 
            this.lbShelf.AutoSize = true;
            this.lbShelf.Location = new System.Drawing.Point(16, 210);
            this.lbShelf.Name = "lbShelf";
            this.lbShelf.Size = new System.Drawing.Size(132, 13);
            this.lbShelf.TabIndex = 7;
            this.lbShelf.Text = "Полочное пространство:";
            // 
            // lbPeriodOfStorage
            // 
            this.lbPeriodOfStorage.AutoSize = true;
            this.lbPeriodOfStorage.Location = new System.Drawing.Point(213, 210);
            this.lbPeriodOfStorage.Name = "lbPeriodOfStorage";
            this.lbPeriodOfStorage.Size = new System.Drawing.Size(61, 13);
            this.lbPeriodOfStorage.TabIndex = 7;
            this.lbPeriodOfStorage.Text = "Срок хран.";
            // 
            // tbPeriodOfStorage
            // 
            this.tbPeriodOfStorage.Location = new System.Drawing.Point(274, 207);
            this.tbPeriodOfStorage.Name = "tbPeriodOfStorage";
            this.tbPeriodOfStorage.Size = new System.Drawing.Size(366, 20);
            this.tbPeriodOfStorage.TabIndex = 16;
            this.tbPeriodOfStorage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPeriodOfStorage.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbPeriodOfStorage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPeriodOfStorage_KeyPress);
            // 
            // lbPrimech
            // 
            this.lbPrimech.AutoSize = true;
            this.lbPrimech.Location = new System.Drawing.Point(16, 236);
            this.lbPrimech.Name = "lbPrimech";
            this.lbPrimech.Size = new System.Drawing.Size(73, 13);
            this.lbPrimech.TabIndex = 7;
            this.lbPrimech.Text = "Примечание:";
            // 
            // tbPrimech
            // 
            this.tbPrimech.Location = new System.Drawing.Point(95, 233);
            this.tbPrimech.Name = "tbPrimech";
            this.tbPrimech.Size = new System.Drawing.Size(363, 20);
            this.tbPrimech.TabIndex = 17;
            this.tbPrimech.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbPrimech.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrimech_KeyPress);
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(464, 236);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(49, 13);
            this.lbUnit.TabIndex = 7;
            this.lbUnit.Text = "Ед. изм.";
            // 
            // cbUnit
            // 
            this.cbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Location = new System.Drawing.Point(513, 233);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(45, 21);
            this.cbUnit.TabIndex = 18;
            this.cbUnit.SelectionChangeCommitted += new System.EventHandler(this.cbUnit_SelectionChangeCommitted);
            this.cbUnit.SelectedValueChanged += new System.EventHandler(this.cbUnit_SelectedValueChanged);
            // 
            // lbNds
            // 
            this.lbNds.AutoSize = true;
            this.lbNds.Location = new System.Drawing.Point(563, 236);
            this.lbNds.Name = "lbNds";
            this.lbNds.Size = new System.Drawing.Size(34, 13);
            this.lbNds.TabIndex = 7;
            this.lbNds.Text = "НДС:";
            // 
            // cbNds
            // 
            this.cbNds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNds.FormattingEnabled = true;
            this.cbNds.Location = new System.Drawing.Point(599, 233);
            this.cbNds.Name = "cbNds";
            this.cbNds.Size = new System.Drawing.Size(45, 21);
            this.cbNds.TabIndex = 19;
            this.cbNds.SelectedValueChanged += new System.EventHandler(this.cbNds_SelectedValueChanged);
            // 
            // lbTU
            // 
            this.lbTU.AutoSize = true;
            this.lbTU.Location = new System.Drawing.Point(16, 262);
            this.lbTU.Name = "lbTU";
            this.lbTU.Size = new System.Drawing.Size(64, 13);
            this.lbTU.TabIndex = 7;
            this.lbTU.Text = "Т/У группа";
            // 
            // cbTU
            // 
            this.cbTU.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbTU.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTU.FormattingEnabled = true;
            this.cbTU.Location = new System.Drawing.Point(95, 259);
            this.cbTU.Name = "cbTU";
            this.cbTU.Size = new System.Drawing.Size(227, 21);
            this.cbTU.TabIndex = 20;
            this.cbTU.SelectionChangeCommitted += new System.EventHandler(this.cbTU_SelectionChangeCommitted);
            // 
            // lbInvGrp
            // 
            this.lbInvGrp.AutoSize = true;
            this.lbInvGrp.Location = new System.Drawing.Point(16, 289);
            this.lbInvGrp.Name = "lbInvGrp";
            this.lbInvGrp.Size = new System.Drawing.Size(67, 13);
            this.lbInvGrp.TabIndex = 7;
            this.lbInvGrp.Text = "Инв. группа";
            // 
            // cbInvGrp
            // 
            this.cbInvGrp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbInvGrp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbInvGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInvGrp.FormattingEnabled = true;
            this.cbInvGrp.Location = new System.Drawing.Point(95, 286);
            this.cbInvGrp.Name = "cbInvGrp";
            this.cbInvGrp.Size = new System.Drawing.Size(227, 21);
            this.cbInvGrp.TabIndex = 21;
            // 
            // btReturn
            // 
            this.btReturn.BackColor = System.Drawing.SystemColors.Control;
            this.btReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btReturn.BackgroundImage")));
            this.btReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btReturn.Location = new System.Drawing.Point(609, 278);
            this.btReturn.Name = "btReturn";
            this.btReturn.Size = new System.Drawing.Size(35, 35);
            this.btReturn.TabIndex = 25;
            this.ttEditGoods.SetToolTip(this.btReturn, "Выход");
            this.btReturn.UseVisualStyleBackColor = false;
            this.btReturn.Click += new System.EventHandler(this.btReturn_Click);
            // 
            // btSave
            // 
            this.btSave.BackColor = System.Drawing.SystemColors.Control;
            this.btSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSave.BackgroundImage")));
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btSave.Location = new System.Drawing.Point(574, 278);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(35, 35);
            this.btSave.TabIndex = 24;
            this.ttEditGoods.SetToolTip(this.btSave, "Сохранить");
            this.btSave.UseVisualStyleBackColor = false;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // lbPercent
            // 
            this.lbPercent.AutoSize = true;
            this.lbPercent.Location = new System.Drawing.Point(592, 158);
            this.lbPercent.Name = "lbPercent";
            this.lbPercent.Size = new System.Drawing.Size(15, 13);
            this.lbPercent.TabIndex = 7;
            this.lbPercent.Text = "%";
            // 
            // tbPercent
            // 
            this.tbPercent.Enabled = false;
            this.tbPercent.Location = new System.Drawing.Point(610, 155);
            this.tbPercent.Name = "tbPercent";
            this.tbPercent.Size = new System.Drawing.Size(34, 20);
            this.tbPercent.TabIndex = 23;
            this.tbPercent.Text = "0";
            this.tbPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbBrutto
            // 
            this.tbBrutto.Location = new System.Drawing.Point(375, 286);
            this.tbBrutto.Name = "tbBrutto";
            this.tbBrutto.Size = new System.Drawing.Size(82, 20);
            this.tbBrutto.TabIndex = 22;
            this.tbBrutto.Text = "0,000";
            this.tbBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbBrutto.Visible = false;
            this.tbBrutto.Enter += new System.EventHandler(this.SelectAllOnEnterTextbox);
            this.tbBrutto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBrutto_KeyPress);
            this.tbBrutto.Validating += new System.ComponentModel.CancelEventHandler(this.tbBrutto_Validating);
            // 
            // lbBrutto
            // 
            this.lbBrutto.AutoSize = true;
            this.lbBrutto.Location = new System.Drawing.Point(328, 289);
            this.lbBrutto.Name = "lbBrutto";
            this.lbBrutto.Size = new System.Drawing.Size(41, 13);
            this.lbBrutto.TabIndex = 7;
            this.lbBrutto.Text = "Брутто";
            this.lbBrutto.Visible = false;
            // 
            // chbIsTransparent
            // 
            this.chbIsTransparent.AutoSize = true;
            this.chbIsTransparent.Location = new System.Drawing.Point(331, 261);
            this.chbIsTransparent.Name = "chbIsTransparent";
            this.chbIsTransparent.Size = new System.Drawing.Size(137, 17);
            this.chbIsTransparent.TabIndex = 26;
            this.chbIsTransparent.Text = "Прозрачная упаковка";
            this.chbIsTransparent.UseVisualStyleBackColor = true;
            this.chbIsTransparent.CheckedChanged += new System.EventHandler(this.chbIsTransparent_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbZapasM2);
            this.panel1.Controls.Add(this.tbOstM2);
            this.panel1.Location = new System.Drawing.Point(151, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 57);
            this.panel1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Тек. ост. (M2)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "дней";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Запас (M2)";
            // 
            // tbZapasM2
            // 
            this.tbZapasM2.Enabled = false;
            this.tbZapasM2.Location = new System.Drawing.Point(74, 29);
            this.tbZapasM2.MaxLength = 3;
            this.tbZapasM2.Name = "tbZapasM2";
            this.tbZapasM2.Size = new System.Drawing.Size(38, 20);
            this.tbZapasM2.TabIndex = 1;
            this.tbZapasM2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbOstM2
            // 
            this.tbOstM2.Enabled = false;
            this.tbOstM2.Location = new System.Drawing.Point(74, 3);
            this.tbOstM2.MaxLength = 11;
            this.tbOstM2.Name = "tbOstM2";
            this.tbOstM2.Size = new System.Drawing.Size(71, 20);
            this.tbOstM2.TabIndex = 1;
            this.tbOstM2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btSelectTovar
            // 
            this.btSelectTovar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectTovar.Enabled = false;
            this.btSelectTovar.Image = global::Requests.Properties.Resources.copy_8372;
            this.btSelectTovar.Location = new System.Drawing.Point(533, 278);
            this.btSelectTovar.Name = "btSelectTovar";
            this.btSelectTovar.Size = new System.Drawing.Size(35, 35);
            this.btSelectTovar.TabIndex = 28;
            this.btSelectTovar.UseVisualStyleBackColor = true;
            this.btSelectTovar.Visible = false;
            this.btSelectTovar.Click += new System.EventHandler(this.btSelectTovar_Click);
            // 
            // frmEditGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 326);
            this.ControlBox = false;
            this.Controls.Add(this.btSelectTovar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chbIsTransparent);
            this.Controls.Add(this.lbBrutto);
            this.Controls.Add(this.tbBrutto);
            this.Controls.Add(this.tbPercent);
            this.Controls.Add(this.lbPercent);
            this.Controls.Add(this.tbDayPlanReal);
            this.Controls.Add(this.tbPrimech);
            this.Controls.Add(this.btReturn);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.cbNds);
            this.Controls.Add(this.cbInvGrp);
            this.Controls.Add(this.cbTU);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.lbOtsechFin);
            this.Controls.Add(this.dtpOtsechFinish);
            this.Controls.Add(this.dtpOtsechStart);
            this.Controls.Add(this.chkOtsechDays);
            this.Controls.Add(this.chkPlanRealizDay);
            this.Controls.Add(this.chkCreateLocalCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCzak);
            this.Controls.Add(this.lbBnds);
            this.Controls.Add(this.lbWnds);
            this.Controls.Add(this.tbZapas2);
            this.Controls.Add(this.lbZcena);
            this.Controls.Add(this.lbRcena);
            this.Controls.Add(this.lbZapas2);
            this.Controls.Add(this.lbZakazCom);
            this.Controls.Add(this.lbShelf);
            this.Controls.Add(this.lbPeriodOfStorage);
            this.Controls.Add(this.lbPrimech);
            this.Controls.Add(this.lbUnit);
            this.Controls.Add(this.lbNds);
            this.Controls.Add(this.lbTU);
            this.Controls.Add(this.lbInvGrp);
            this.Controls.Add(this.pnKol);
            this.Controls.Add(this.pnRcena);
            this.Controls.Add(this.pnZcena);
            this.Controls.Add(this.tbShelfSpace);
            this.Controls.Add(this.pnOst);
            this.Controls.Add(this.lbCountry);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbEan);
            this.Controls.Add(this.cbSubject);
            this.Controls.Add(this.pnName);
            this.Controls.Add(this.pnEan);
            this.Controls.Add(this.tbPeriodOfStorage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(564, 332);
            this.Name = "frmEditGoods";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение/добавление товара";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmEditGoods_Load);
            this.pnEan.ResumeLayout(false);
            this.pnEan.PerformLayout();
            this.pnName.ResumeLayout(false);
            this.pnName.PerformLayout();
            this.pnOst.ResumeLayout(false);
            this.pnOst.PerformLayout();
            this.pnZcena.ResumeLayout(false);
            this.pnZcena.PerformLayout();
            this.pnKol.ResumeLayout(false);
            this.pnKol.PerformLayout();
            this.pnRcena.ResumeLayout(false);
            this.pnRcena.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnEan;
        private System.Windows.Forms.Panel pnName;
        private System.Windows.Forms.TextBox tbEan;
        private System.Windows.Forms.ComboBox cbSubject;
        private System.Windows.Forms.Label lbEan;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbCountry;
        private System.Windows.Forms.Panel pnOst;
        private System.Windows.Forms.Label lbOst;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbZapas;
        private System.Windows.Forms.TextBox tbZapas;
        private System.Windows.Forms.TextBox tbOst;
        private System.Windows.Forms.TextBox tbShelfSpace;
        private System.Windows.Forms.Panel pnZcena;
        private System.Windows.Forms.Panel pnKol;
        private System.Windows.Forms.TextBox tbZatar;
        private System.Windows.Forms.TextBox tbTara;
        private System.Windows.Forms.TextBox tbZakaz;
        private System.Windows.Forms.Panel pnRcena;
        private System.Windows.Forms.Label lbZatar;
        private System.Windows.Forms.Label lbTara;
        private System.Windows.Forms.TextBox tbZcenabnds;
        private System.Windows.Forms.TextBox tbZcena;
        private System.Windows.Forms.Label lbZakaz;
        private System.Windows.Forms.Label lbBnds;
        private System.Windows.Forms.TextBox tbRcena;
        private System.Windows.Forms.Label lbWnds;
        private System.Windows.Forms.Label lbZcena;
        private System.Windows.Forms.Label lbRcena;
        private System.Windows.Forms.Label lbZapas2;
        private System.Windows.Forms.TextBox tbZapas2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCzak;
        private System.Windows.Forms.Label lbZakazCom;
        private System.Windows.Forms.CheckBox chkCreateLocalCode;
        private System.Windows.Forms.CheckBox chkPlanRealizDay;
        private System.Windows.Forms.TextBox tbDayPlanReal;
        private System.Windows.Forms.CheckBox chkOtsechDays;
        private System.Windows.Forms.DateTimePicker dtpOtsechStart;
        private System.Windows.Forms.DateTimePicker dtpOtsechFinish;
        private System.Windows.Forms.Label lbOtsechFin;
        private System.Windows.Forms.Label lbShelf;
        private System.Windows.Forms.Label lbPeriodOfStorage;
        private System.Windows.Forms.TextBox tbPeriodOfStorage;
        private System.Windows.Forms.Label lbPrimech;
        private System.Windows.Forms.TextBox tbPrimech;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.Label lbNds;
        private System.Windows.Forms.ComboBox cbNds;
        private System.Windows.Forms.Label lbTU;
        private System.Windows.Forms.ComboBox cbTU;
        private System.Windows.Forms.Label lbInvGrp;
        private System.Windows.Forms.ComboBox cbInvGrp;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btReturn;
        private System.Windows.Forms.ToolTip ttEditGoods;
        private System.Windows.Forms.Label lbPercent;
        private System.Windows.Forms.TextBox tbPercent;
        private System.Windows.Forms.TextBox tbBrutto;
        private System.Windows.Forms.Label lbBrutto;
        private System.Windows.Forms.CheckBox chbIsTransparent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbZapasM2;
        private System.Windows.Forms.TextBox tbOstM2;
        private System.Windows.Forms.Button btSelectTovar;

    }
}