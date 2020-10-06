namespace Requests.sWeInOut
{
    partial class frmSelectTovar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectTovar));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbEan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rb22 = new System.Windows.Forms.RadioButton();
            this.rb11 = new System.Windows.Forms.RadioButton();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDeps = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tbZakaz = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbInRequest = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLostRequest = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btClose = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chbUsed = new System.Windows.Forms.CheckBox();
            this.tbFTTN = new System.Windows.Forms.TextBox();
            this.tbFName = new System.Windows.Forms.TextBox();
            this.btAutoCount = new System.Windows.Forms.Button();
            this.cbAddedN = new System.Windows.Forms.CheckBox();
            this.cDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNumer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTTN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cRequestCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tbEan
            // 
            this.tbEan.Location = new System.Drawing.Point(58, 23);
            this.tbEan.Name = "tbEan";
            this.tbEan.ReadOnly = true;
            this.tbEan.Size = new System.Drawing.Size(148, 20);
            this.tbEan.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Товар:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(212, 23);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(473, 20);
            this.tbName.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rb22);
            this.groupBox4.Controls.Add(this.rb11);
            this.groupBox4.Location = new System.Drawing.Point(826, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(104, 42);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            // 
            // rb22
            // 
            this.rb22.AutoSize = true;
            this.rb22.Location = new System.Drawing.Point(51, 15);
            this.rb22.Name = "rb22";
            this.rb22.Size = new System.Drawing.Size(37, 17);
            this.rb22.TabIndex = 0;
            this.rb22.Text = "22";
            this.rb22.UseVisualStyleBackColor = true;
            this.rb22.Click += new System.EventHandler(this.rb11_Click);
            // 
            // rb11
            // 
            this.rb11.AutoSize = true;
            this.rb11.Checked = true;
            this.rb11.Location = new System.Drawing.Point(8, 15);
            this.rb11.Name = "rb11";
            this.rb11.Size = new System.Drawing.Size(37, 17);
            this.rb11.TabIndex = 0;
            this.rb11.TabStop = true;
            this.rb11.Text = "11";
            this.rb11.UseVisualStyleBackColor = true;
            this.rb11.Click += new System.EventHandler(this.rb11_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(71, 50);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(92, 20);
            this.dtpStart.TabIndex = 42;
            this.dtpStart.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(212, 50);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(92, 20);
            this.dtpEnd.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "по";
            // 
            // cbDeps
            // 
            this.cbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeps.FormattingEnabled = true;
            this.cbDeps.Location = new System.Drawing.Point(503, 50);
            this.cbDeps.Name = "cbDeps";
            this.cbDeps.Size = new System.Drawing.Size(182, 21);
            this.cbDeps.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(456, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Отдел:";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDate,
            this.cNumer,
            this.cTTN,
            this.cUl,
            this.cPostName,
            this.cCount,
            this.cPrice,
            this.cV,
            this.cRequestCount});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.Location = new System.Drawing.Point(14, 102);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.Size = new System.Drawing.Size(900, 248);
            this.dgvData.TabIndex = 44;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellEndEdit);
            this.dgvData.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvData_CellValidating);
            this.dgvData.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvData_EditingControlShowing);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            this.dgvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvData_KeyDown);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox3.Location = new System.Drawing.Point(17, 389);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(289, 50);
            this.textBox3.TabIndex = 0;
            // 
            // tbZakaz
            // 
            this.tbZakaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbZakaz.Location = new System.Drawing.Point(393, 355);
            this.tbZakaz.Name = "tbZakaz";
            this.tbZakaz.ReadOnly = true;
            this.tbZakaz.Size = new System.Drawing.Size(148, 20);
            this.tbZakaz.TabIndex = 0;
            this.tbZakaz.Text = "0,00";
            this.tbZakaz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(331, 359);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "В заявке:";
            // 
            // tbInRequest
            // 
            this.tbInRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInRequest.Location = new System.Drawing.Point(765, 355);
            this.tbInRequest.Name = "tbInRequest";
            this.tbInRequest.ReadOnly = true;
            this.tbInRequest.Size = new System.Drawing.Size(148, 20);
            this.tbInRequest.TabIndex = 0;
            this.tbInRequest.Text = "0,00";
            this.tbInRequest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(671, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Итого в заявке:";
            // 
            // tbLostRequest
            // 
            this.tbLostRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLostRequest.Location = new System.Drawing.Point(765, 381);
            this.tbLostRequest.Name = "tbLostRequest";
            this.tbLostRequest.ReadOnly = true;
            this.tbLostRequest.Size = new System.Drawing.Size(148, 20);
            this.tbLostRequest.TabIndex = 0;
            this.tbLostRequest.Text = "0,00";
            this.tbLostRequest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(658, 384);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Недобор в заявку:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 407);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = " - товар использован";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(322, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(19, 19);
            this.panel2.TabIndex = 45;
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackColor = System.Drawing.SystemColors.Control;
            this.btClose.BackgroundImage = global::Requests.Properties.Resources.pict_close;
            this.btClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(878, 407);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(35, 35);
            this.btClose.TabIndex = 47;
            this.toolTip1.SetToolTip(this.btClose, "Выход");
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.BackColor = System.Drawing.SystemColors.Control;
            this.btSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSave.BackgroundImage")));
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btSave.Location = new System.Drawing.Point(837, 407);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(35, 35);
            this.btSave.TabIndex = 46;
            this.toolTip1.SetToolTip(this.btSave, "Сохранить");
            this.btSave.UseVisualStyleBackColor = false;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Период с";
            // 
            // chbUsed
            // 
            this.chbUsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbUsed.AutoSize = true;
            this.chbUsed.Location = new System.Drawing.Point(17, 350);
            this.chbUsed.Name = "chbUsed";
            this.chbUsed.Size = new System.Drawing.Size(124, 17);
            this.chbUsed.TabIndex = 48;
            this.chbUsed.Text = "товар использован";
            this.chbUsed.UseVisualStyleBackColor = true;
            this.chbUsed.Click += new System.EventHandler(this.chbUsed_Click);
            // 
            // tbFTTN
            // 
            this.tbFTTN.Location = new System.Drawing.Point(156, 76);
            this.tbFTTN.MaxLength = 50;
            this.tbFTTN.Name = "tbFTTN";
            this.tbFTTN.Size = new System.Drawing.Size(148, 20);
            this.tbFTTN.TabIndex = 0;
            this.tbFTTN.TextChanged += new System.EventHandler(this.tbFTTN_TextChanged);
            // 
            // tbFName
            // 
            this.tbFName.Location = new System.Drawing.Point(310, 76);
            this.tbFName.MaxLength = 120;
            this.tbFName.Name = "tbFName";
            this.tbFName.Size = new System.Drawing.Size(148, 20);
            this.tbFName.TabIndex = 0;
            this.tbFName.TextChanged += new System.EventHandler(this.tbFTTN_TextChanged);
            // 
            // btAutoCount
            // 
            this.btAutoCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAutoCount.BackColor = System.Drawing.SystemColors.Control;
            this.btAutoCount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btAutoCount.Image = global::Requests.Properties.Resources.copy_8372;
            this.btAutoCount.Location = new System.Drawing.Point(796, 407);
            this.btAutoCount.Name = "btAutoCount";
            this.btAutoCount.Size = new System.Drawing.Size(35, 35);
            this.btAutoCount.TabIndex = 46;
            this.btAutoCount.UseVisualStyleBackColor = false;
            this.btAutoCount.Click += new System.EventHandler(this.btAutoCount_Click);
            // 
            // cbAddedN
            // 
            this.cbAddedN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAddedN.AutoSize = true;
            this.cbAddedN.Location = new System.Drawing.Point(17, 366);
            this.cbAddedN.Name = "cbAddedN";
            this.cbAddedN.Size = new System.Drawing.Size(204, 17);
            this.cbAddedN.TabIndex = 48;
            this.cbAddedN.Text = "отобразить выбранные накладные";
            this.cbAddedN.UseVisualStyleBackColor = true;
            this.cbAddedN.CheckedChanged += new System.EventHandler(this.cbAddedN_CheckedChanged);
            this.cbAddedN.Click += new System.EventHandler(this.chbUsed_Click);
            // 
            // cDate
            // 
            this.cDate.DataPropertyName = "dprihod";
            this.cDate.HeaderText = "Дата";
            this.cDate.Name = "cDate";
            this.cDate.ReadOnly = true;
            this.cDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cNumer
            // 
            this.cNumer.DataPropertyName = "id_trequest";
            this.cNumer.HeaderText = "Номер заявки";
            this.cNumer.Name = "cNumer";
            this.cNumer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cTTN
            // 
            this.cTTN.DataPropertyName = "ttn";
            this.cTTN.HeaderText = "ТТН";
            this.cTTN.Name = "cTTN";
            this.cTTN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cUl
            // 
            this.cUl.DataPropertyName = "Abbriviation";
            this.cUl.HeaderText = "ЮЛ";
            this.cUl.Name = "cUl";
            this.cUl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cPostName
            // 
            this.cPostName.DataPropertyName = "cname";
            this.cPostName.HeaderText = "Поставщик";
            this.cPostName.Name = "cPostName";
            this.cPostName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cCount
            // 
            this.cCount.DataPropertyName = "netto";
            this.cCount.HeaderText = "Кол-во шт/кг";
            this.cCount.Name = "cCount";
            this.cCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cPrice
            // 
            this.cPrice.DataPropertyName = "zcena";
            this.cPrice.HeaderText = "Цена закупки";
            this.cPrice.Name = "cPrice";
            this.cPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cV
            // 
            this.cV.DataPropertyName = "isV";
            this.cV.FalseValue = "False";
            this.cV.HeaderText = "V";
            this.cV.IndeterminateValue = "False";
            this.cV.Name = "cV";
            this.cV.ReadOnly = true;
            this.cV.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cV.TrueValue = "True";
            // 
            // cRequestCount
            // 
            this.cRequestCount.DataPropertyName = "NettoReq";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            this.cRequestCount.DefaultCellStyle = dataGridViewCellStyle2;
            this.cRequestCount.HeaderText = "В заявку шт/кг";
            this.cRequestCount.MaxInputLength = 13;
            this.cRequestCount.Name = "cRequestCount";
            this.cRequestCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmSelectTovar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 443);
            this.ControlBox = false;
            this.Controls.Add(this.cbAddedN);
            this.Controls.Add(this.chbUsed);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btAutoCount);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.cbDeps);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.tbLostRequest);
            this.Controls.Add(this.tbInRequest);
            this.Controls.Add(this.tbFName);
            this.Controls.Add(this.tbFTTN);
            this.Controls.Add(this.tbZakaz);
            this.Controls.Add(this.tbEan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectTovar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор товара для заявки";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectTovar_FormClosing);
            this.Load += new System.EventHandler(this.frmSelectTovar_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbEan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rb22;
        private System.Windows.Forms.RadioButton rb11;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDeps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox tbZakaz;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbInRequest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbLostRequest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbUsed;
        private System.Windows.Forms.TextBox tbFTTN;
        private System.Windows.Forms.TextBox tbFName;
        private System.Windows.Forms.Button btAutoCount;
        private System.Windows.Forms.CheckBox cbAddedN;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumer;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTTN;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUl;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPrice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cV;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRequestCount;
    }
}