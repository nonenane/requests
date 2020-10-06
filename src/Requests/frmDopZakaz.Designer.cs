namespace Requests
{
    partial class frmDopZakaz
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMultiTU = new System.Windows.Forms.Button();
            this.cmbTUGroups = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSubGroups = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEAN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearName = new System.Windows.Forms.Button();
            this.dgvDopZakaz = new System.Windows.Forms.DataGridView();
            this.id_tovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sred_rashod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ost_on_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.current_realiz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.realiz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fact_netto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dop_zakaz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subgroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_grp1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_grp3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbNeedZakaz = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDopZakaz)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(56, 12);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(92, 20);
            this.dtpDate.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Дата:";
            // 
            // btnMultiTU
            // 
            this.btnMultiTU.Location = new System.Drawing.Point(386, 11);
            this.btnMultiTU.Name = "btnMultiTU";
            this.btnMultiTU.Size = new System.Drawing.Size(32, 23);
            this.btnMultiTU.TabIndex = 39;
            this.btnMultiTU.Text = "...";
            this.toolTips.SetToolTip(this.btnMultiTU, "Выбор ТУ групп");
            this.btnMultiTU.UseVisualStyleBackColor = true;
            this.btnMultiTU.Click += new System.EventHandler(this.btnMultiTU_Click);
            // 
            // cmbTUGroups
            // 
            this.cmbTUGroups.DisplayMember = "cname";
            this.cmbTUGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTUGroups.FormattingEnabled = true;
            this.cmbTUGroups.Location = new System.Drawing.Point(259, 12);
            this.cmbTUGroups.Name = "cmbTUGroups";
            this.cmbTUGroups.Size = new System.Drawing.Size(121, 21);
            this.cmbTUGroups.TabIndex = 38;
            this.cmbTUGroups.ValueMember = "id";
            this.cmbTUGroups.SelectedValueChanged += new System.EventHandler(this.cmbTUGroups_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "ТУ группа:";
            // 
            // cmbSubGroups
            // 
            this.cmbSubGroups.DisplayMember = "cname";
            this.cmbSubGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubGroups.FormattingEnabled = true;
            this.cmbSubGroups.Location = new System.Drawing.Point(706, 15);
            this.cmbSubGroups.Name = "cmbSubGroups";
            this.cmbSubGroups.Size = new System.Drawing.Size(121, 21);
            this.cmbSubGroups.TabIndex = 41;
            this.cmbSubGroups.ValueMember = "id";
            this.cmbSubGroups.SelectedValueChanged += new System.EventHandler(this.cmbSubGroups_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(636, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Подгруппа:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(293, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(534, 20);
            this.txtName.TabIndex = 45;
            this.toolTips.SetToolTip(this.txtName, "Поиск по наименованию товара");
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // txtEAN
            // 
            this.txtEAN.Location = new System.Drawing.Point(52, 41);
            this.txtEAN.Name = "txtEAN";
            this.txtEAN.Size = new System.Drawing.Size(109, 20);
            this.txtEAN.TabIndex = 44;
            this.toolTips.SetToolTip(this.txtEAN, "Поиск по EAN");
            this.txtEAN.TextChanged += new System.EventHandler(this.txtEAN_TextChanged);
            this.txtEAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEAN_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(167, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Наименование товара:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "EAN:";
            // 
            // btnClearName
            // 
            this.btnClearName.Image = global::Requests.Properties.Resources.delete;
            this.btnClearName.Location = new System.Drawing.Point(833, 35);
            this.btnClearName.Name = "btnClearName";
            this.btnClearName.Size = new System.Drawing.Size(26, 26);
            this.btnClearName.TabIndex = 46;
            this.toolTips.SetToolTip(this.btnClearName, "Очистить фильтр");
            this.btnClearName.UseVisualStyleBackColor = true;
            this.btnClearName.Click += new System.EventHandler(this.btnClearName_Click);
            // 
            // dgvDopZakaz
            // 
            this.dgvDopZakaz.AllowUserToAddRows = false;
            this.dgvDopZakaz.AllowUserToDeleteRows = false;
            this.dgvDopZakaz.AllowUserToResizeRows = false;
            this.dgvDopZakaz.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDopZakaz.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDopZakaz.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDopZakaz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDopZakaz.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_tovar,
            this.ean,
            this.cname,
            this.sred_rashod,
            this.ost_on_date,
            this.inventory,
            this.current_realiz,
            this.realiz,
            this.fact_netto,
            this.dop_zakaz,
            this.subgroup,
            this.id_grp1,
            this.id_grp3});
            this.dgvDopZakaz.Location = new System.Drawing.Point(17, 67);
            this.dgvDopZakaz.Name = "dgvDopZakaz";
            this.dgvDopZakaz.ReadOnly = true;
            this.dgvDopZakaz.RowHeadersVisible = false;
            this.dgvDopZakaz.Size = new System.Drawing.Size(839, 453);
            this.dgvDopZakaz.TabIndex = 47;
            // 
            // id_tovar
            // 
            this.id_tovar.DataPropertyName = "id_tovar";
            this.id_tovar.HeaderText = "id_tovar";
            this.id_tovar.Name = "id_tovar";
            this.id_tovar.ReadOnly = true;
            this.id_tovar.Visible = false;
            // 
            // ean
            // 
            this.ean.DataPropertyName = "ean";
            this.ean.HeaderText = "EAN";
            this.ean.Name = "ean";
            this.ean.ReadOnly = true;
            // 
            // cname
            // 
            this.cname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cname.DataPropertyName = "cname";
            this.cname.HeaderText = "Наименование";
            this.cname.Name = "cname";
            this.cname.ReadOnly = true;
            this.cname.Width = 120;
            // 
            // sred_rashod
            // 
            this.sred_rashod.DataPropertyName = "sred_rashod";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N3";
            this.sred_rashod.DefaultCellStyle = dataGridViewCellStyle2;
            this.sred_rashod.HeaderText = "Средний расход";
            this.sred_rashod.Name = "sred_rashod";
            this.sred_rashod.ReadOnly = true;
            // 
            // ost_on_date
            // 
            this.ost_on_date.DataPropertyName = "ost_on_date";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N3";
            this.ost_on_date.DefaultCellStyle = dataGridViewCellStyle3;
            this.ost_on_date.HeaderText = "Остаток на утро";
            this.ost_on_date.Name = "ost_on_date";
            this.ost_on_date.ReadOnly = true;
            // 
            // inventory
            // 
            this.inventory.DataPropertyName = "inventory";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N3";
            this.inventory.DefaultCellStyle = dataGridViewCellStyle4;
            this.inventory.HeaderText = "Переучёт";
            this.inventory.Name = "inventory";
            this.inventory.ReadOnly = true;
            // 
            // current_realiz
            // 
            this.current_realiz.DataPropertyName = "current_realiz";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N3";
            this.current_realiz.DefaultCellStyle = dataGridViewCellStyle5;
            this.current_realiz.HeaderText = "Текущая реализация";
            this.current_realiz.Name = "current_realiz";
            this.current_realiz.ReadOnly = true;
            // 
            // realiz
            // 
            this.realiz.DataPropertyName = "realiz";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N3";
            this.realiz.DefaultCellStyle = dataGridViewCellStyle6;
            this.realiz.HeaderText = "Примерная реализация";
            this.realiz.Name = "realiz";
            this.realiz.ReadOnly = true;
            // 
            // fact_netto
            // 
            this.fact_netto.DataPropertyName = "fact_netto";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N3";
            this.fact_netto.DefaultCellStyle = dataGridViewCellStyle7;
            this.fact_netto.HeaderText = "Фактический заказ";
            this.fact_netto.Name = "fact_netto";
            this.fact_netto.ReadOnly = true;
            // 
            // dop_zakaz
            // 
            this.dop_zakaz.DataPropertyName = "dop_zakaz";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N3";
            this.dop_zakaz.DefaultCellStyle = dataGridViewCellStyle8;
            this.dop_zakaz.HeaderText = "Доп. заказ";
            this.dop_zakaz.Name = "dop_zakaz";
            this.dop_zakaz.ReadOnly = true;
            // 
            // subgroup
            // 
            this.subgroup.DataPropertyName = "subgroup";
            this.subgroup.HeaderText = "Вид";
            this.subgroup.Name = "subgroup";
            this.subgroup.ReadOnly = true;
            // 
            // id_grp1
            // 
            this.id_grp1.DataPropertyName = "id_grp1";
            this.id_grp1.HeaderText = "id_grp1";
            this.id_grp1.Name = "id_grp1";
            this.id_grp1.ReadOnly = true;
            this.id_grp1.Visible = false;
            // 
            // id_grp3
            // 
            this.id_grp3.DataPropertyName = "id_grp3";
            this.id_grp3.HeaderText = "id_grp3";
            this.id_grp3.Name = "id_grp3";
            this.id_grp3.ReadOnly = true;
            this.id_grp3.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Requests.Properties.Resources.pict_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(11, 526);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 48;
            this.toolTips.SetToolTip(this.btnRefresh, "Обновить");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnToExcel
            // 
            this.btnToExcel.Image = global::Requests.Properties.Resources.pict_excel;
            this.btnToExcel.Location = new System.Drawing.Point(786, 526);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(32, 32);
            this.btnToExcel.TabIndex = 49;
            this.toolTips.SetToolTip(this.btnToExcel, "Печать");
            this.btnToExcel.UseVisualStyleBackColor = true;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::Requests.Properties.Resources.pict_close;
            this.btnExit.Location = new System.Drawing.Point(824, 526);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 50;
            this.toolTips.SetToolTip(this.btnExit, "Выход");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbNeedZakaz
            // 
            this.cbNeedZakaz.AutoSize = true;
            this.cbNeedZakaz.Location = new System.Drawing.Point(49, 535);
            this.cbNeedZakaz.Name = "cbNeedZakaz";
            this.cbNeedZakaz.Size = new System.Drawing.Size(149, 17);
            this.cbNeedZakaz.TabIndex = 51;
            this.cbNeedZakaz.Text = "необходимо дозаказать";
            this.cbNeedZakaz.UseVisualStyleBackColor = true;
            this.cbNeedZakaz.CheckedChanged += new System.EventHandler(this.cbNeedZakaz_CheckedChanged);
            // 
            // frmDopZakaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 578);
            this.ControlBox = false;
            this.Controls.Add(this.cbNeedZakaz);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnToExcel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvDopZakaz);
            this.Controls.Add(this.btnClearName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtEAN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSubGroups);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnMultiTU);
            this.Controls.Add(this.cmbTUGroups);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDopZakaz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дополнительный заказ";
            this.Load += new System.EventHandler(this.frmDopZakaz_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDopZakaz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMultiTU;
        private System.Windows.Forms.ComboBox cmbTUGroups;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSubGroups;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClearName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEAN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvDopZakaz;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnToExcel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox cbNeedZakaz;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn cname;
        private System.Windows.Forms.DataGridViewTextBoxColumn sred_rashod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ost_on_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventory;
        private System.Windows.Forms.DataGridViewTextBoxColumn current_realiz;
        private System.Windows.Forms.DataGridViewTextBoxColumn realiz;
        private System.Windows.Forms.DataGridViewTextBoxColumn fact_netto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dop_zakaz;
        private System.Windows.Forms.DataGridViewTextBoxColumn subgroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_grp1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_grp3;
    }
}