namespace ViewSalesPromProducts
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbSearchCode = new System.Windows.Forms.TextBox();
            this.tbSearchName = new System.Windows.Forms.TextBox();
            this.btnSettingProducts = new System.Windows.Forms.Button();
            this.btnPrintReport = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOtdel = new System.Windows.Forms.ComboBox();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.summa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.Location = new System.Drawing.Point(61, 32);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Size = new System.Drawing.Size(139, 20);
            this.dtpDateStart.TabIndex = 0;
            this.dtpDateStart.ValueChanged += new System.EventHandler(this.dtpDateStart_ValueChanged);
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.Location = new System.Drawing.Point(276, 32);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(139, 20);
            this.dtpDateEnd.TabIndex = 1;
            this.dtpDateEnd.ValueChanged += new System.EventHandler(this.dtpDateEnd_ValueChanged);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDeps,
            this.date,
            this.code,
            this.id_tovar,
            this.name,
            this.count,
            this.price,
            this.summa});
            this.dgvMain.Location = new System.Drawing.Point(12, 86);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowHeadersWidth = 62;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(841, 310);
            this.dgvMain.TabIndex = 4;
            this.dgvMain.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvMain_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Дата с";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Дата по";
            // 
            // tbSearchCode
            // 
            this.tbSearchCode.Location = new System.Drawing.Point(118, 60);
            this.tbSearchCode.Name = "tbSearchCode";
            this.tbSearchCode.Size = new System.Drawing.Size(115, 20);
            this.tbSearchCode.TabIndex = 11;
            this.toolTip1.SetToolTip(this.tbSearchCode, "поиск по коду товара");
            this.tbSearchCode.TextChanged += new System.EventHandler(this.tbSearchCode_TextChanged);
            this.tbSearchCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchCode_KeyPress);
            // 
            // tbSearchName
            // 
            this.tbSearchName.Location = new System.Drawing.Point(239, 60);
            this.tbSearchName.Name = "tbSearchName";
            this.tbSearchName.Size = new System.Drawing.Size(332, 20);
            this.tbSearchName.TabIndex = 12;
            this.toolTip1.SetToolTip(this.tbSearchName, "поиск по наменованию");
            this.tbSearchName.TextChanged += new System.EventHandler(this.tbSearchName_TextChanged);
            this.tbSearchName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchName_KeyPress);
            // 
            // btnSettingProducts
            // 
            this.btnSettingProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSettingProducts.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.Set;
            this.btnSettingProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSettingProducts.Location = new System.Drawing.Point(13, 410);
            this.btnSettingProducts.Name = "btnSettingProducts";
            this.btnSettingProducts.Size = new System.Drawing.Size(35, 35);
            this.btnSettingProducts.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnSettingProducts, "Настройки акционного товара");
            this.btnSettingProducts.UseVisualStyleBackColor = true;
            this.btnSettingProducts.Visible = false;
            this.btnSettingProducts.Click += new System.EventHandler(this.btnSettingProducts_Click);
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintReport.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.klpq_2511;
            this.btnPrintReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrintReport.Location = new System.Drawing.Point(61, 410);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(35, 35);
            this.btnPrintReport.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnPrintReport, "Выгрузка в Excel");
            this.btnPrintReport.UseVisualStyleBackColor = true;
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.reload_8055;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpdate.Location = new System.Drawing.Point(818, 17);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(35, 35);
            this.btnUpdate.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnUpdate, "Обновить");
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.exit_8633;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Location = new System.Drawing.Point(818, 410);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnClose, "Выход");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 451);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(872, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Отдел";
            // 
            // cmbOtdel
            // 
            this.cmbOtdel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOtdel.FormattingEnabled = true;
            this.cmbOtdel.Location = new System.Drawing.Point(61, 6);
            this.cmbOtdel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOtdel.Name = "cmbOtdel";
            this.cmbOtdel.Size = new System.Drawing.Size(189, 21);
            this.cmbOtdel.TabIndex = 16;
            this.cmbOtdel.SelectionChangeCommitted += new System.EventHandler(this.cmbOtdel_SelectionChangeCommitted);
            // 
            // cDeps
            // 
            this.cDeps.DataPropertyName = "nameDep";
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            // 
            // date
            // 
            this.date.DataPropertyName = "date";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.NullValue = null;
            this.date.DefaultCellStyle = dataGridViewCellStyle10;
            this.date.FillWeight = 90F;
            this.date.HeaderText = "Дата";
            this.date.MinimumWidth = 50;
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // code
            // 
            this.code.DataPropertyName = "code";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.code.DefaultCellStyle = dataGridViewCellStyle11;
            this.code.FillWeight = 110F;
            this.code.HeaderText = "Код товара";
            this.code.MinimumWidth = 50;
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // id_tovar
            // 
            this.id_tovar.DataPropertyName = "id_tovar";
            this.id_tovar.HeaderText = "id_tovar";
            this.id_tovar.Name = "id_tovar";
            this.id_tovar.ReadOnly = true;
            this.id_tovar.Visible = false;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.name.DefaultCellStyle = dataGridViewCellStyle12;
            this.name.FillWeight = 338F;
            this.name.HeaderText = "Наименование";
            this.name.MinimumWidth = 150;
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // count
            // 
            this.count.DataPropertyName = "count";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N3";
            dataGridViewCellStyle13.NullValue = null;
            this.count.DefaultCellStyle = dataGridViewCellStyle13;
            this.count.FillWeight = 70F;
            this.count.HeaderText = "Количество";
            this.count.MinimumWidth = 50;
            this.count.Name = "count";
            this.count.ReadOnly = true;
            // 
            // price
            // 
            this.price.DataPropertyName = "price";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.price.DefaultCellStyle = dataGridViewCellStyle14;
            this.price.FillWeight = 70F;
            this.price.HeaderText = "Цена продажи";
            this.price.MinimumWidth = 50;
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // summa
            // 
            this.summa.DataPropertyName = "summa";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = null;
            this.summa.DefaultCellStyle = dataGridViewCellStyle15;
            this.summa.FillWeight = 70F;
            this.summa.HeaderText = "Сумма";
            this.summa.Name = "summa";
            this.summa.ReadOnly = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 473);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbOtdel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbSearchName);
            this.Controls.Add(this.tbSearchCode);
            this.Controls.Add(this.btnSettingProducts);
            this.Controls.Add(this.btnPrintReport);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.dtpDateEnd);
            this.Controls.Add(this.dtpDateStart);
            this.MinimumSize = new System.Drawing.Size(880, 500);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр реализации акционного товара";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnPrintReport;
        private System.Windows.Forms.Button btnSettingProducts;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tbSearchCode;
        private System.Windows.Forms.TextBox tbSearchName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbOtdel;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn summa;
    }
}

