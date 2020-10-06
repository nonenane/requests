namespace Requests
{
    partial class frmPereoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPereoc));
            this.gbTypes = new System.Windows.Forms.GroupBox();
            this.rbDooc = new System.Windows.Forms.RadioButton();
            this.rbPereoc = new System.Windows.Forms.RadioButton();
            this.grdPereoc = new System.Windows.Forms.DataGridView();
            this.id_dep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ostOnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rcena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zcena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newcena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btSave = new System.Windows.Forms.Button();
            this.btNewPereoc = new System.Windows.Forms.Button();
            this.btDelPereoc = new System.Windows.Forms.Button();
            this.btExcel = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.ttPereoc = new System.Windows.Forms.ToolTip(this.components);
            this.btExit = new System.Windows.Forms.Button();
            this.gbTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPereoc)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTypes
            // 
            this.gbTypes.Controls.Add(this.rbDooc);
            this.gbTypes.Controls.Add(this.rbPereoc);
            this.gbTypes.Location = new System.Drawing.Point(12, 3);
            this.gbTypes.Name = "gbTypes";
            this.gbTypes.Size = new System.Drawing.Size(180, 49);
            this.gbTypes.TabIndex = 0;
            this.gbTypes.TabStop = false;
            // 
            // rbDooc
            // 
            this.rbDooc.AutoSize = true;
            this.rbDooc.Location = new System.Drawing.Point(99, 19);
            this.rbDooc.Name = "rbDooc";
            this.rbDooc.Size = new System.Drawing.Size(76, 17);
            this.rbDooc.TabIndex = 38;
            this.rbDooc.Text = "Дооценка";
            this.rbDooc.UseVisualStyleBackColor = true;
            // 
            // rbPereoc
            // 
            this.rbPereoc.AutoSize = true;
            this.rbPereoc.Checked = true;
            this.rbPereoc.Location = new System.Drawing.Point(6, 19);
            this.rbPereoc.Name = "rbPereoc";
            this.rbPereoc.Size = new System.Drawing.Size(87, 17);
            this.rbPereoc.TabIndex = 38;
            this.rbPereoc.TabStop = true;
            this.rbPereoc.Text = "Переоценка";
            this.rbPereoc.UseVisualStyleBackColor = true;
            // 
            // grdPereoc
            // 
            this.grdPereoc.AllowUserToAddRows = false;
            this.grdPereoc.AllowUserToDeleteRows = false;
            this.grdPereoc.AllowUserToOrderColumns = true;
            this.grdPereoc.AllowUserToResizeRows = false;
            this.grdPereoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPereoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdPereoc.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPereoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdPereoc.ColumnHeadersHeight = 40;
            this.grdPereoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdPereoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_dep,
            this.cName,
            this.ean,
            this.ostOnDate,
            this.rcena,
            this.zcena,
            this.newcena});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPereoc.DefaultCellStyle = dataGridViewCellStyle9;
            this.grdPereoc.Location = new System.Drawing.Point(12, 58);
            this.grdPereoc.Name = "grdPereoc";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPereoc.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.grdPereoc.RowHeadersVisible = false;
            this.grdPereoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPereoc.Size = new System.Drawing.Size(747, 458);
            this.grdPereoc.TabIndex = 1;
            this.grdPereoc.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.grdPereoc_RowPrePaint);
            this.grdPereoc.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdPereoc_RowPostPaint);
            this.grdPereoc.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grdPereoc_RowsAdded);
            this.grdPereoc.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPereoc_CellEndEdit);
            this.grdPereoc.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdPereoc_EditingControlShowing);
            this.grdPereoc.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdPereoc_DataError);
            this.grdPereoc.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grdPereoc_RowsRemoved);
            // 
            // id_dep
            // 
            this.id_dep.DataPropertyName = "id_dep";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.id_dep.DefaultCellStyle = dataGridViewCellStyle2;
            this.id_dep.FillWeight = 50F;
            this.id_dep.HeaderText = "№ отдела";
            this.id_dep.Name = "id_dep";
            this.id_dep.ReadOnly = true;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cName.DefaultCellStyle = dataGridViewCellStyle3;
            this.cName.FillWeight = 180F;
            this.cName.HeaderText = "Наименование товара";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // ean
            // 
            this.ean.DataPropertyName = "ean";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ean.DefaultCellStyle = dataGridViewCellStyle4;
            this.ean.FillWeight = 90F;
            this.ean.HeaderText = "Код товара";
            this.ean.Name = "ean";
            this.ean.ReadOnly = true;
            // 
            // ostOnDate
            // 
            this.ostOnDate.DataPropertyName = "ostOnDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N3";
            this.ostOnDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.ostOnDate.HeaderText = "Остаток";
            this.ostOnDate.Name = "ostOnDate";
            this.ostOnDate.ReadOnly = true;
            // 
            // rcena
            // 
            this.rcena.DataPropertyName = "rcena";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            this.rcena.DefaultCellStyle = dataGridViewCellStyle6;
            this.rcena.HeaderText = "Цена продажи";
            this.rcena.Name = "rcena";
            this.rcena.ReadOnly = true;
            // 
            // zcena
            // 
            this.zcena.DataPropertyName = "zcena";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N4";
            this.zcena.DefaultCellStyle = dataGridViewCellStyle7;
            this.zcena.HeaderText = "Цена закупки";
            this.zcena.Name = "zcena";
            this.zcena.ReadOnly = true;
            // 
            // newcena
            // 
            this.newcena.DataPropertyName = "zcenabnds";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            this.newcena.DefaultCellStyle = dataGridViewCellStyle8;
            this.newcena.HeaderText = "Новая цена";
            this.newcena.Name = "newcena";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSave.BackgroundImage")));
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btSave.Location = new System.Drawing.Point(641, 522);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(25, 25);
            this.btSave.TabIndex = 35;
            this.ttPereoc.SetToolTip(this.btSave, "Сохранить");
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btNewPereoc
            // 
            this.btNewPereoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btNewPereoc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btNewPereoc.BackgroundImage")));
            this.btNewPereoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btNewPereoc.Location = new System.Drawing.Point(579, 522);
            this.btNewPereoc.Name = "btNewPereoc";
            this.btNewPereoc.Size = new System.Drawing.Size(25, 25);
            this.btNewPereoc.TabIndex = 36;
            this.ttPereoc.SetToolTip(this.btNewPereoc, "Добавить");
            this.btNewPereoc.UseVisualStyleBackColor = true;
            this.btNewPereoc.Click += new System.EventHandler(this.btNewPereoc_Click);
            // 
            // btDelPereoc
            // 
            this.btDelPereoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelPereoc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btDelPereoc.BackgroundImage")));
            this.btDelPereoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btDelPereoc.Location = new System.Drawing.Point(610, 522);
            this.btDelPereoc.Name = "btDelPereoc";
            this.btDelPereoc.Size = new System.Drawing.Size(25, 25);
            this.btDelPereoc.TabIndex = 37;
            this.ttPereoc.SetToolTip(this.btDelPereoc, "Удалить");
            this.btDelPereoc.UseVisualStyleBackColor = true;
            this.btDelPereoc.Click += new System.EventHandler(this.btDelPereoc_Click);
            // 
            // btExcel
            // 
            this.btExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btExcel.Image = ((System.Drawing.Image)(resources.GetObject("btExcel.Image")));
            this.btExcel.Location = new System.Drawing.Point(672, 522);
            this.btExcel.Name = "btExcel";
            this.btExcel.Size = new System.Drawing.Size(25, 25);
            this.btExcel.TabIndex = 34;
            this.ttPereoc.SetToolTip(this.btExcel, "Выгрузить данные в Excel");
            this.btExcel.UseVisualStyleBackColor = true;
            this.btExcel.Click += new System.EventHandler(this.btExcel_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btPrint.Image = ((System.Drawing.Image)(resources.GetObject("btPrint.Image")));
            this.btPrint.Location = new System.Drawing.Point(703, 522);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(25, 25);
            this.btPrint.TabIndex = 33;
            this.ttPereoc.SetToolTip(this.btPrint, "Печать");
            this.btPrint.UseVisualStyleBackColor = false;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.BackColor = System.Drawing.SystemColors.Control;
            this.btExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btExit.BackgroundImage")));
            this.btExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExit.Location = new System.Drawing.Point(734, 522);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(25, 25);
            this.btExit.TabIndex = 33;
            this.ttPereoc.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = false;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // frmPereoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 559);
            this.ControlBox = false;
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btNewPereoc);
            this.Controls.Add(this.btDelPereoc);
            this.Controls.Add(this.btExcel);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.grdPereoc);
            this.Controls.Add(this.gbTypes);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPereoc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmPereoc_Load);
            this.Activated += new System.EventHandler(this.frmPereoc_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPereoc_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPereoc_FormClosing);
            this.gbTypes.ResumeLayout(false);
            this.gbTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPereoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTypes;
        private System.Windows.Forms.DataGridView grdPereoc;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btNewPereoc;
        private System.Windows.Forms.Button btDelPereoc;
        private System.Windows.Forms.Button btExcel;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.RadioButton rbDooc;
        private System.Windows.Forms.RadioButton rbPereoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_dep;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn ostOnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn rcena;
        private System.Windows.Forms.DataGridViewTextBoxColumn zcena;
        private System.Windows.Forms.DataGridViewTextBoxColumn newcena;
        private System.Windows.Forms.ToolTip ttPereoc;
        private System.Windows.Forms.Button btExit;
    }
}