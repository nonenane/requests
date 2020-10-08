namespace ViewSalesPromProducts
{
    partial class frmSettingProducts
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOne = new System.Windows.Forms.DataGridView();
            this.id_tovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameTovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isRezerv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbOtdel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTYGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbinvGroup = new System.Windows.Forms.ComboBox();
            this.tbSearchEan = new System.Windows.Forms.TextBox();
            this.tbSearchName = new System.Windows.Forms.TextBox();
            this.tbSearchEan2 = new System.Windows.Forms.TextBox();
            this.tbSearchName2 = new System.Windows.Forms.TextBox();
            this.dgvTwo = new System.Windows.Forms.DataGridView();
            this._id_tovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._NameTovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._isRezerv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTwo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOne
            // 
            this.dgvOne.AllowUserToAddRows = false;
            this.dgvOne.AllowUserToDeleteRows = false;
            this.dgvOne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvOne.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvOne.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOne.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOne.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_tovar,
            this.ean,
            this.NameTovar,
            this.isRezerv});
            this.dgvOne.GridColor = System.Drawing.Color.Gray;
            this.dgvOne.Location = new System.Drawing.Point(16, 18);
            this.dgvOne.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOne.Name = "dgvOne";
            this.dgvOne.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOne.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOne.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvOne.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOne.RowTemplate.Height = 24;
            this.dgvOne.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOne.Size = new System.Drawing.Size(282, 506);
            this.dgvOne.TabIndex = 12;
            this.dgvOne.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvOne_RowPrePaint);
            // 
            // id_tovar
            // 
            this.id_tovar.DataPropertyName = "id_tovar";
            this.id_tovar.HeaderText = "id";
            this.id_tovar.Name = "id_tovar";
            this.id_tovar.ReadOnly = true;
            this.id_tovar.Visible = false;
            // 
            // ean
            // 
            this.ean.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ean.DataPropertyName = "ean";
            this.ean.HeaderText = "EAN";
            this.ean.MinimumWidth = 10;
            this.ean.Name = "ean";
            this.ean.ReadOnly = true;
            // 
            // NameTovar
            // 
            this.NameTovar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameTovar.DataPropertyName = "NameTovar";
            this.NameTovar.HeaderText = "Наименование";
            this.NameTovar.Name = "NameTovar";
            this.NameTovar.ReadOnly = true;
            // 
            // isRezerv
            // 
            this.isRezerv.DataPropertyName = "isRezerv";
            this.isRezerv.HeaderText = "isRezerv";
            this.isRezerv.Name = "isRezerv";
            this.isRezerv.ReadOnly = true;
            this.isRezerv.Visible = false;
            // 
            // cmbOtdel
            // 
            this.cmbOtdel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOtdel.FormattingEnabled = true;
            this.cmbOtdel.Location = new System.Drawing.Point(119, 13);
            this.cmbOtdel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOtdel.Name = "cmbOtdel";
            this.cmbOtdel.Size = new System.Drawing.Size(189, 21);
            this.cmbOtdel.TabIndex = 14;
            this.cmbOtdel.SelectedIndexChanged += new System.EventHandler(this.cmbOtdel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Отдел";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = " Т/У группа";
            // 
            // cmbTYGroup
            // 
            this.cmbTYGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTYGroup.FormattingEnabled = true;
            this.cmbTYGroup.Location = new System.Drawing.Point(119, 48);
            this.cmbTYGroup.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTYGroup.Name = "cmbTYGroup";
            this.cmbTYGroup.Size = new System.Drawing.Size(189, 21);
            this.cmbTYGroup.TabIndex = 17;
            this.cmbTYGroup.SelectedIndexChanged += new System.EventHandler(this.cmbTYGroup_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Инв.группа";
            // 
            // cmbinvGroup
            // 
            this.cmbinvGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbinvGroup.FormattingEnabled = true;
            this.cmbinvGroup.Location = new System.Drawing.Point(119, 81);
            this.cmbinvGroup.Margin = new System.Windows.Forms.Padding(2);
            this.cmbinvGroup.Name = "cmbinvGroup";
            this.cmbinvGroup.Size = new System.Drawing.Size(189, 21);
            this.cmbinvGroup.TabIndex = 19;
            this.cmbinvGroup.SelectedIndexChanged += new System.EventHandler(this.cmbinvGroup_SelectedIndexChanged);
            // 
            // tbSearchEan
            // 
            this.tbSearchEan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchEan.Location = new System.Drawing.Point(27, 113);
            this.tbSearchEan.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearchEan.Name = "tbSearchEan";
            this.tbSearchEan.Size = new System.Drawing.Size(104, 20);
            this.tbSearchEan.TabIndex = 20;
            this.tbSearchEan.TextChanged += new System.EventHandler(this.tbSearchEan_TextChanged);
            this.tbSearchEan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchEan_KeyPress);
            // 
            // tbSearchName
            // 
            this.tbSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchName.Location = new System.Drawing.Point(135, 113);
            this.tbSearchName.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearchName.Name = "tbSearchName";
            this.tbSearchName.Size = new System.Drawing.Size(174, 20);
            this.tbSearchName.TabIndex = 21;
            this.tbSearchName.TextChanged += new System.EventHandler(this.tbSearchName_TextChanged);
            this.tbSearchName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchName_KeyPress);
            // 
            // tbSearchEan2
            // 
            this.tbSearchEan2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchEan2.Location = new System.Drawing.Point(415, 113);
            this.tbSearchEan2.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearchEan2.Name = "tbSearchEan2";
            this.tbSearchEan2.Size = new System.Drawing.Size(103, 20);
            this.tbSearchEan2.TabIndex = 22;
            this.tbSearchEan2.TextChanged += new System.EventHandler(this.tbSearchEan2_TextChanged);
            this.tbSearchEan2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchEan2_KeyPress);
            // 
            // tbSearchName2
            // 
            this.tbSearchName2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchName2.Location = new System.Drawing.Point(520, 113);
            this.tbSearchName2.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearchName2.Name = "tbSearchName2";
            this.tbSearchName2.Size = new System.Drawing.Size(186, 20);
            this.tbSearchName2.TabIndex = 23;
            this.tbSearchName2.TextChanged += new System.EventHandler(this.tbSearchName2_TextChanged);
            this.tbSearchName2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchName2_KeyPress);
            // 
            // dgvTwo
            // 
            this.dgvTwo.AllowUserToAddRows = false;
            this.dgvTwo.AllowUserToDeleteRows = false;
            this.dgvTwo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTwo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvTwo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTwo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTwo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._id_tovar,
            this._ean,
            this._NameTovar,
            this._isRezerv});
            this.dgvTwo.GridColor = System.Drawing.Color.Gray;
            this.dgvTwo.Location = new System.Drawing.Point(21, 18);
            this.dgvTwo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTwo.Name = "dgvTwo";
            this.dgvTwo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTwo.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTwo.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvTwo.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTwo.RowTemplate.Height = 24;
            this.dgvTwo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTwo.Size = new System.Drawing.Size(293, 506);
            this.dgvTwo.TabIndex = 24;
            this.dgvTwo.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvTwo_RowPrePaint);
            // 
            // _id_tovar
            // 
            this._id_tovar.DataPropertyName = "id_tovar";
            this._id_tovar.HeaderText = "id";
            this._id_tovar.Name = "_id_tovar";
            this._id_tovar.ReadOnly = true;
            this._id_tovar.Visible = false;
            // 
            // _ean
            // 
            this._ean.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._ean.DataPropertyName = "ean";
            this._ean.HeaderText = "EAN";
            this._ean.MinimumWidth = 10;
            this._ean.Name = "_ean";
            this._ean.ReadOnly = true;
            // 
            // _NameTovar
            // 
            this._NameTovar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._NameTovar.DataPropertyName = "NameTovar";
            this._NameTovar.HeaderText = "Наименование";
            this._NameTovar.Name = "_NameTovar";
            this._NameTovar.ReadOnly = true;
            // 
            // _isRezerv
            // 
            this._isRezerv.DataPropertyName = "isRezerv";
            this._isRezerv.HeaderText = "isRezerv";
            this._isRezerv.Name = "_isRezerv";
            this._isRezerv.ReadOnly = true;
            this._isRezerv.Visible = false;
            // 
            // btnAddAll
            // 
            this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAll.Location = new System.Drawing.Point(336, 212);
            this.btnAddAll.MaximumSize = new System.Drawing.Size(40, 35);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(40, 35);
            this.btnAddAll.TabIndex = 25;
            this.btnAddAll.Text = ">>";
            this.toolTip1.SetToolTip(this.btnAddAll, "Добавить все");
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(336, 264);
            this.btnAdd.MaximumSize = new System.Drawing.Size(40, 35);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 35);
            this.btnAdd.TabIndex = 26;
            this.btnAdd.Text = ">";
            this.toolTip1.SetToolTip(this.btnAdd, "Добавить");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(337, 339);
            this.btnDelete.MaximumSize = new System.Drawing.Size(40, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(40, 35);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.Text = "<";
            this.toolTip1.SetToolTip(this.btnDelete, "Удалить");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAll.Location = new System.Drawing.Point(337, 384);
            this.btnDeleteAll.MaximumSize = new System.Drawing.Size(40, 35);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(40, 35);
            this.btnDeleteAll.TabIndex = 28;
            this.btnDeleteAll.Text = "<<";
            this.toolTip1.SetToolTip(this.btnDeleteAll, "Удалить все");
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 705);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 22);
            this.statusStrip1.TabIndex = 32;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(14, 674);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(19, 18);
            this.panel1.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 677);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Резервные товары";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvOne);
            this.groupBox1.Location = new System.Drawing.Point(11, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 531);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Все товары";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvTwo);
            this.groupBox2.Location = new System.Drawing.Point(392, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 531);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Справочник акционных товаров";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.pict_close;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(678, 670);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 35);
            this.btnExit.TabIndex = 31;
            this.toolTip1.SetToolTip(this.btnExit, "Выход");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.save_edit;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(627, 670);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 35);
            this.btnSave.TabIndex = 30;
            this.toolTip1.SetToolTip(this.btnSave, "Сохранить");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.klpq_2511;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.Location = new System.Drawing.Point(575, 670);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(35, 35);
            this.btnPrint.TabIndex = 29;
            this.toolTip1.SetToolTip(this.btnPrint, "Печать");
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmSettingProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 727);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.tbSearchName2);
            this.Controls.Add(this.tbSearchEan2);
            this.Controls.Add(this.tbSearchName);
            this.Controls.Add(this.tbSearchEan);
            this.Controls.Add(this.cmbinvGroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTYGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbOtdel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(750, 766);
            this.MinimumSize = new System.Drawing.Size(695, 702);
            this.Name = "frmSettingProducts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки акционного товара";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTwo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOne;
        private System.Windows.Forms.ComboBox cmbOtdel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTYGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbinvGroup;
        private System.Windows.Forms.TextBox tbSearchEan;
        private System.Windows.Forms.TextBox tbSearchName;
        private System.Windows.Forms.TextBox tbSearchEan2;
        private System.Windows.Forms.TextBox tbSearchName2;
        private System.Windows.Forms.DataGridView dgvTwo;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameTovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn isRezerv;
        private System.Windows.Forms.DataGridViewTextBoxColumn _id_tovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn _NameTovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn _isRezerv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}