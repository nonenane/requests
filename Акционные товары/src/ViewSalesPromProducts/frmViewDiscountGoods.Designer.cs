namespace ViewSalesPromProducts
{
    partial class frmViewDiscountGoods
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOtdel = new System.Windows.Forms.ComboBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.tbSearchName = new System.Windows.Forms.TextBox();
            this.tbSearchCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEditor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDateEdit = new System.Windows.Forms.TextBox();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPriceRealK21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPriceDiscountK21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPriceRealX14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPriceDiscountX14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Отдел";
            // 
            // cmbOtdel
            // 
            this.cmbOtdel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOtdel.FormattingEnabled = true;
            this.cmbOtdel.Location = new System.Drawing.Point(61, 9);
            this.cmbOtdel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOtdel.Name = "cmbOtdel";
            this.cmbOtdel.Size = new System.Drawing.Size(189, 21);
            this.cmbOtdel.TabIndex = 21;
            this.cmbOtdel.SelectionChangeCommitted += new System.EventHandler(this.cmbOtdel_SelectionChangeCommitted);
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
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDeps,
            this.cEan,
            this.cName,
            this.cPriceRealK21,
            this.cPriceDiscountK21,
            this.cPriceRealX14,
            this.cPriceDiscountX14});
            this.dgvMain.Location = new System.Drawing.Point(12, 89);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowHeadersWidth = 62;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1112, 494);
            this.dgvMain.TabIndex = 18;
            this.dgvMain.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvMain_ColumnWidthChanged);
            this.dgvMain.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMain_RowPostPaint);
            this.dgvMain.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvMain_RowPrePaint);
            this.dgvMain.SelectionChanged += new System.EventHandler(this.dgvMain_SelectionChanged);
            // 
            // tbSearchName
            // 
            this.tbSearchName.Location = new System.Drawing.Point(143, 63);
            this.tbSearchName.MaxLength = 250;
            this.tbSearchName.Name = "tbSearchName";
            this.tbSearchName.Size = new System.Drawing.Size(332, 20);
            this.tbSearchName.TabIndex = 24;
            this.tbSearchName.TextChanged += new System.EventHandler(this.tbSearchCode_TextChanged);
            // 
            // tbSearchCode
            // 
            this.tbSearchCode.Location = new System.Drawing.Point(22, 63);
            this.tbSearchCode.MaxLength = 13;
            this.tbSearchCode.Name = "tbSearchCode";
            this.tbSearchCode.Size = new System.Drawing.Size(115, 20);
            this.tbSearchCode.TabIndex = 23;
            this.tbSearchCode.TextChanged += new System.EventHandler(this.tbSearchCode_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 596);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Редактировал";
            // 
            // tbEditor
            // 
            this.tbEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbEditor.Location = new System.Drawing.Point(163, 592);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.ReadOnly = true;
            this.tbEditor.Size = new System.Drawing.Size(191, 20);
            this.tbEditor.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 622);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Дата редактирования";
            // 
            // tbDateEdit
            // 
            this.tbDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDateEdit.Location = new System.Drawing.Point(163, 618);
            this.tbDateEdit.Name = "tbDateEdit";
            this.tbDateEdit.ReadOnly = true;
            this.tbDateEdit.Size = new System.Drawing.Size(191, 20);
            this.tbDateEdit.TabIndex = 26;
            // 
            // cDeps
            // 
            this.cDeps.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cDeps.DataPropertyName = "nameDep";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cDeps.DefaultCellStyle = dataGridViewCellStyle20;
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.MinimumWidth = 110;
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            this.cDeps.Width = 110;
            // 
            // cEan
            // 
            this.cEan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cEan.DataPropertyName = "ean";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cEan.DefaultCellStyle = dataGridViewCellStyle21;
            this.cEan.HeaderText = "EAN";
            this.cEan.MinimumWidth = 120;
            this.cEan.Name = "cEan";
            this.cEan.ReadOnly = true;
            this.cEan.Width = 120;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cName.DefaultCellStyle = dataGridViewCellStyle22;
            this.cName.FillWeight = 338F;
            this.cName.HeaderText = "Наименование";
            this.cName.MinimumWidth = 150;
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cPriceRealK21
            // 
            this.cPriceRealK21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cPriceRealK21.DataPropertyName = "PriceRealK21";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Format = "N2";
            dataGridViewCellStyle23.NullValue = null;
            this.cPriceRealK21.DefaultCellStyle = dataGridViewCellStyle23;
            this.cPriceRealK21.HeaderText = "Цена без ограничения на К21";
            this.cPriceRealK21.MinimumWidth = 90;
            this.cPriceRealK21.Name = "cPriceRealK21";
            this.cPriceRealK21.ReadOnly = true;
            this.cPriceRealK21.Width = 90;
            // 
            // cPriceDiscountK21
            // 
            this.cPriceDiscountK21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cPriceDiscountK21.DataPropertyName = "PriceDiscountK21";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle24.Format = "N2";
            this.cPriceDiscountK21.DefaultCellStyle = dataGridViewCellStyle24;
            this.cPriceDiscountK21.HeaderText = "Акционная цена товара на К21";
            this.cPriceDiscountK21.MinimumWidth = 90;
            this.cPriceDiscountK21.Name = "cPriceDiscountK21";
            this.cPriceDiscountK21.ReadOnly = true;
            this.cPriceDiscountK21.Width = 90;
            // 
            // cPriceRealX14
            // 
            this.cPriceRealX14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cPriceRealX14.DataPropertyName = "PriceRealX14";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle25.Format = "N2";
            this.cPriceRealX14.DefaultCellStyle = dataGridViewCellStyle25;
            this.cPriceRealX14.HeaderText = "Цена без ограничения на Х14";
            this.cPriceRealX14.MinimumWidth = 90;
            this.cPriceRealX14.Name = "cPriceRealX14";
            this.cPriceRealX14.ReadOnly = true;
            this.cPriceRealX14.Width = 90;
            // 
            // cPriceDiscountX14
            // 
            this.cPriceDiscountX14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cPriceDiscountX14.DataPropertyName = "PriceDiscountX14";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Format = "N2";
            this.cPriceDiscountX14.DefaultCellStyle = dataGridViewCellStyle26;
            this.cPriceDiscountX14.HeaderText = "Акционная цена товара на Х14";
            this.cPriceDiscountX14.MinimumWidth = 90;
            this.cPriceDiscountX14.Name = "cPriceDiscountX14";
            this.cPriceDiscountX14.ReadOnly = true;
            this.cPriceDiscountX14.Width = 90;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.Image = global::ViewSalesPromProducts.Properties.Resources.klpq_25111;
            this.button4.Location = new System.Drawing.Point(850, 610);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 35);
            this.button4.TabIndex = 27;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.Image = global::ViewSalesPromProducts.Properties.Resources.compfile_1551;
            this.button3.Location = new System.Drawing.Point(919, 611);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 35);
            this.button3.TabIndex = 27;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Image = global::ViewSalesPromProducts.Properties.Resources.edit_1761;
            this.button2.Location = new System.Drawing.Point(960, 611);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 35);
            this.button2.TabIndex = 27;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Image = global::ViewSalesPromProducts.Properties.Resources.editdelete_3805;
            this.button1.Location = new System.Drawing.Point(1001, 610);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 27;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.reload_8055;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpdate.Location = new System.Drawing.Point(1089, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(35, 35);
            this.btnUpdate.TabIndex = 20;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::ViewSalesPromProducts.Properties.Resources.exit_8633;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Location = new System.Drawing.Point(1089, 610);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 19;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmViewDiscountGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 657);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbDateEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbEditor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSearchName);
            this.Controls.Add(this.tbSearchCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbOtdel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "frmViewDiscountGoods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmViewDiscountGoods";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewDiscountGoods_FormClosing);
            this.Load += new System.EventHandler(this.frmViewDiscountGoods_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbOtdel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.TextBox tbSearchName;
        private System.Windows.Forms.TextBox tbSearchCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbEditor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDateEdit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPriceRealK21;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPriceDiscountK21;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPriceRealX14;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPriceDiscountX14;
    }
}