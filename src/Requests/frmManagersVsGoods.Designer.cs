namespace Requests
{
    partial class frmManagersVsGoods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagersVsGoods));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbManager = new System.Windows.Forms.Label();
            this.cbManager = new System.Windows.Forms.ComboBox();
            this.lbTU = new System.Windows.Forms.Label();
            this.cbTU = new System.Windows.Forms.ComboBox();
            this.lbInv = new System.Windows.Forms.Label();
            this.cbInv = new System.Windows.Forms.ComboBox();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.dgvManagerGoods = new System.Windows.Forms.DataGridView();
            this.btAdd = new System.Windows.Forms.Button();
            this.btAddAll = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btDeleteAll = new System.Windows.Forms.Button();
            this.ttManagersVsGoods = new System.Windows.Forms.ToolTip(this.components);
            this.lbDepartment = new System.Windows.Forms.Label();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eanMan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnameMan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tovarMan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbEan = new System.Windows.Forms.TextBox();
            this.tbCname = new System.Windows.Forms.TextBox();
            this.tbEanMan = new System.Windows.Forms.TextBox();
            this.tbCnameMan = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManagerGoods)).BeginInit();
            this.SuspendLayout();
            // 
            // lbManager
            // 
            this.lbManager.AutoSize = true;
            this.lbManager.Location = new System.Drawing.Point(183, 15);
            this.lbManager.Name = "lbManager";
            this.lbManager.Size = new System.Drawing.Size(63, 13);
            this.lbManager.TabIndex = 0;
            this.lbManager.Text = "Менеджер:";
            // 
            // cbManager
            // 
            this.cbManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbManager.FormattingEnabled = true;
            this.cbManager.Location = new System.Drawing.Point(252, 12);
            this.cbManager.Name = "cbManager";
            this.cbManager.Size = new System.Drawing.Size(121, 21);
            this.cbManager.TabIndex = 1;
            this.cbManager.SelectionChangeCommitted += new System.EventHandler(this.cbManager_SelectionChangeCommitted);
            // 
            // lbTU
            // 
            this.lbTU.AutoSize = true;
            this.lbTU.Location = new System.Drawing.Point(379, 15);
            this.lbTU.Name = "lbTU";
            this.lbTU.Size = new System.Drawing.Size(62, 13);
            this.lbTU.TabIndex = 2;
            this.lbTU.Text = "ТУ группа:";
            // 
            // cbTU
            // 
            this.cbTU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTU.FormattingEnabled = true;
            this.cbTU.Location = new System.Drawing.Point(447, 12);
            this.cbTU.Name = "cbTU";
            this.cbTU.Size = new System.Drawing.Size(121, 21);
            this.cbTU.TabIndex = 1;
            this.cbTU.SelectionChangeCommitted += new System.EventHandler(this.cbTU_SelectionChangeCommitted);
            // 
            // lbInv
            // 
            this.lbInv.AutoSize = true;
            this.lbInv.Location = new System.Drawing.Point(574, 15);
            this.lbInv.Name = "lbInv";
            this.lbInv.Size = new System.Drawing.Size(70, 13);
            this.lbInv.TabIndex = 2;
            this.lbInv.Text = "Инв. группа:";
            // 
            // cbInv
            // 
            this.cbInv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInv.FormattingEnabled = true;
            this.cbInv.Location = new System.Drawing.Point(650, 12);
            this.cbInv.Name = "cbInv";
            this.cbInv.Size = new System.Drawing.Size(121, 21);
            this.cbInv.TabIndex = 1;
            this.cbInv.SelectionChangeCommitted += new System.EventHandler(this.cbInv_SelectionChangeCommitted);
            // 
            // dgvGoods
            // 
            this.dgvGoods.AllowUserToAddRows = false;
            this.dgvGoods.AllowUserToDeleteRows = false;
            this.dgvGoods.AllowUserToResizeColumns = false;
            this.dgvGoods.AllowUserToResizeRows = false;
            this.dgvGoods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGoods.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGoods.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ean,
            this.cname,
            this.id_tovar});
            this.dgvGoods.Location = new System.Drawing.Point(12, 65);
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.ReadOnly = true;
            this.dgvGoods.RowHeadersVisible = false;
            this.dgvGoods.Size = new System.Drawing.Size(347, 348);
            this.dgvGoods.TabIndex = 3;
            // 
            // dgvManagerGoods
            // 
            this.dgvManagerGoods.AllowUserToAddRows = false;
            this.dgvManagerGoods.AllowUserToDeleteRows = false;
            this.dgvManagerGoods.AllowUserToResizeColumns = false;
            this.dgvManagerGoods.AllowUserToResizeRows = false;
            this.dgvManagerGoods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvManagerGoods.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvManagerGoods.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvManagerGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvManagerGoods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eanMan,
            this.cnameMan,
            this.id_tovarMan});
            this.dgvManagerGoods.Location = new System.Drawing.Point(424, 65);
            this.dgvManagerGoods.Name = "dgvManagerGoods";
            this.dgvManagerGoods.ReadOnly = true;
            this.dgvManagerGoods.RowHeadersVisible = false;
            this.dgvManagerGoods.Size = new System.Drawing.Size(347, 348);
            this.dgvManagerGoods.TabIndex = 3;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(374, 182);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(35, 35);
            this.btAdd.TabIndex = 4;
            this.btAdd.Text = ">";
            this.ttManagersVsGoods.SetToolTip(this.btAdd, "Добавить товар");
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btAddAll
            // 
            this.btAddAll.Location = new System.Drawing.Point(374, 65);
            this.btAddAll.Name = "btAddAll";
            this.btAddAll.Size = new System.Drawing.Size(35, 35);
            this.btAddAll.TabIndex = 4;
            this.btAddAll.Text = ">>";
            this.ttManagersVsGoods.SetToolTip(this.btAddAll, "Добавить все товары");
            this.btAddAll.UseVisualStyleBackColor = true;
            this.btAddAll.Click += new System.EventHandler(this.btAddAll_Click);
            // 
            // btExit
            // 
            this.btExit.Image = ((System.Drawing.Image)(resources.GetObject("btExit.Image")));
            this.btExit.Location = new System.Drawing.Point(736, 419);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(35, 35);
            this.btExit.TabIndex = 4;
            this.ttManagersVsGoods.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(374, 240);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(35, 35);
            this.btDelete.TabIndex = 4;
            this.btDelete.Text = "<";
            this.ttManagersVsGoods.SetToolTip(this.btDelete, "Удалить товар");
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btDeleteAll
            // 
            this.btDeleteAll.Location = new System.Drawing.Point(374, 378);
            this.btDeleteAll.Name = "btDeleteAll";
            this.btDeleteAll.Size = new System.Drawing.Size(35, 35);
            this.btDeleteAll.TabIndex = 4;
            this.btDeleteAll.Text = "<<";
            this.ttManagersVsGoods.SetToolTip(this.btDeleteAll, "Удалить все товары");
            this.btDeleteAll.UseVisualStyleBackColor = true;
            this.btDeleteAll.Click += new System.EventHandler(this.btDeleteAll_Click);
            // 
            // lbDepartment
            // 
            this.lbDepartment.AutoSize = true;
            this.lbDepartment.Location = new System.Drawing.Point(9, 15);
            this.lbDepartment.Name = "lbDepartment";
            this.lbDepartment.Size = new System.Drawing.Size(41, 13);
            this.lbDepartment.TabIndex = 0;
            this.lbDepartment.Text = "Отдел:";
            // 
            // cbDepartment
            // 
            this.cbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Location = new System.Drawing.Point(56, 12);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Size = new System.Drawing.Size(121, 21);
            this.cbDepartment.TabIndex = 1;
            // 
            // ean
            // 
            this.ean.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ean.DataPropertyName = "ean";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ean.DefaultCellStyle = dataGridViewCellStyle2;
            this.ean.HeaderText = "EAN";
            this.ean.Name = "ean";
            this.ean.ReadOnly = true;
            this.ean.Width = 110;
            // 
            // cname
            // 
            this.cname.DataPropertyName = "cname";
            this.cname.HeaderText = "Наименование";
            this.cname.Name = "cname";
            this.cname.ReadOnly = true;
            // 
            // id_tovar
            // 
            this.id_tovar.DataPropertyName = "id_tovar";
            this.id_tovar.HeaderText = "id_tovar";
            this.id_tovar.Name = "id_tovar";
            this.id_tovar.ReadOnly = true;
            this.id_tovar.Visible = false;
            // 
            // eanMan
            // 
            this.eanMan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.eanMan.DataPropertyName = "ean";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.eanMan.DefaultCellStyle = dataGridViewCellStyle4;
            this.eanMan.HeaderText = "EAN";
            this.eanMan.Name = "eanMan";
            this.eanMan.ReadOnly = true;
            this.eanMan.Width = 110;
            // 
            // cnameMan
            // 
            this.cnameMan.DataPropertyName = "cname";
            this.cnameMan.HeaderText = "Наименование";
            this.cnameMan.Name = "cnameMan";
            this.cnameMan.ReadOnly = true;
            // 
            // id_tovarMan
            // 
            this.id_tovarMan.DataPropertyName = "id_tovar";
            this.id_tovarMan.HeaderText = "id_tovarMan";
            this.id_tovarMan.Name = "id_tovarMan";
            this.id_tovarMan.ReadOnly = true;
            this.id_tovarMan.Visible = false;
            // 
            // tbEan
            // 
            this.tbEan.Location = new System.Drawing.Point(12, 39);
            this.tbEan.MaxLength = 13;
            this.tbEan.Name = "tbEan";
            this.tbEan.Size = new System.Drawing.Size(109, 20);
            this.tbEan.TabIndex = 5;
            this.tbEan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbEan.TextChanged += new System.EventHandler(this.tbEan_TextChanged);
            this.tbEan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEan_KeyPress);
            // 
            // tbCname
            // 
            this.tbCname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbCname.Location = new System.Drawing.Point(127, 39);
            this.tbCname.Name = "tbCname";
            this.tbCname.Size = new System.Drawing.Size(232, 20);
            this.tbCname.TabIndex = 5;
            this.tbCname.TextChanged += new System.EventHandler(this.tbEan_TextChanged);
            this.tbCname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCname_KeyPress);
            // 
            // tbEanMan
            // 
            this.tbEanMan.Location = new System.Drawing.Point(424, 39);
            this.tbEanMan.MaxLength = 13;
            this.tbEanMan.Name = "tbEanMan";
            this.tbEanMan.Size = new System.Drawing.Size(109, 20);
            this.tbEanMan.TabIndex = 5;
            this.tbEanMan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbEanMan.TextChanged += new System.EventHandler(this.tbEan_TextChanged);
            this.tbEanMan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEan_KeyPress);
            // 
            // tbCnameMan
            // 
            this.tbCnameMan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbCnameMan.Location = new System.Drawing.Point(539, 39);
            this.tbCnameMan.Name = "tbCnameMan";
            this.tbCnameMan.Size = new System.Drawing.Size(232, 20);
            this.tbCnameMan.TabIndex = 5;
            this.tbCnameMan.TextChanged += new System.EventHandler(this.tbEan_TextChanged);
            this.tbCnameMan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCname_KeyPress);
            // 
            // frmManagersVsGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 463);
            this.ControlBox = false;
            this.Controls.Add(this.tbCnameMan);
            this.Controls.Add(this.tbCname);
            this.Controls.Add(this.tbEanMan);
            this.Controls.Add(this.tbEan);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btDeleteAll);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btAddAll);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.dgvManagerGoods);
            this.Controls.Add(this.dgvGoods);
            this.Controls.Add(this.lbInv);
            this.Controls.Add(this.lbTU);
            this.Controls.Add(this.cbInv);
            this.Controls.Add(this.cbTU);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.lbDepartment);
            this.Controls.Add(this.cbManager);
            this.Controls.Add(this.lbManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManagersVsGoods";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Товары менеджеров";
            this.Load += new System.EventHandler(this.frmManagersVsGoods_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManagerGoods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbManager;
        private System.Windows.Forms.ComboBox cbManager;
        private System.Windows.Forms.Label lbTU;
        private System.Windows.Forms.ComboBox cbTU;
        private System.Windows.Forms.Label lbInv;
        private System.Windows.Forms.ComboBox cbInv;
        private System.Windows.Forms.DataGridView dgvGoods;
        private System.Windows.Forms.DataGridView dgvManagerGoods;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btAddAll;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btDeleteAll;
        private System.Windows.Forms.ToolTip ttManagersVsGoods;
        private System.Windows.Forms.Label lbDepartment;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn cname;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn eanMan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnameMan;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tovarMan;
        private System.Windows.Forms.TextBox tbEan;
        private System.Windows.Forms.TextBox tbCname;
        private System.Windows.Forms.TextBox tbEanMan;
        private System.Windows.Forms.TextBox tbCnameMan;
    }
}