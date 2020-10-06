namespace Requests
{
    partial class frmProhMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProhMessage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pbMessage = new System.Windows.Forms.PictureBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.grdReqProh = new System.Windows.Forms.DataGridView();
            this.ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReqProh)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMessage
            // 
            this.pbMessage.ErrorImage = null;
            this.pbMessage.Image = ((System.Drawing.Image)(resources.GetObject("pbMessage.Image")));
            this.pbMessage.Location = new System.Drawing.Point(12, 12);
            this.pbMessage.Name = "pbMessage";
            this.pbMessage.Size = new System.Drawing.Size(100, 100);
            this.pbMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMessage.TabIndex = 0;
            this.pbMessage.TabStop = false;
            // 
            // lbMessage
            // 
            this.lbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMessage.Location = new System.Drawing.Point(118, 12);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(304, 100);
            this.lbMessage.TabIndex = 1;
            this.lbMessage.Text = "Перечисленные ниже товары присутствуют \r\nв списке запрещенных товаров для \r\nдобав" +
                "ления в заявку.\r\n\r\nДобавление указанных товаров в заявку\r\nневозможно.\r\n";
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(171, 420);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 24);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // grdReqProh
            // 
            this.grdReqProh.AllowUserToAddRows = false;
            this.grdReqProh.AllowUserToDeleteRows = false;
            this.grdReqProh.AllowUserToResizeRows = false;
            this.grdReqProh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdReqProh.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdReqProh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdReqProh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReqProh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ean,
            this.cname});
            this.grdReqProh.Location = new System.Drawing.Point(12, 118);
            this.grdReqProh.Name = "grdReqProh";
            this.grdReqProh.ReadOnly = true;
            this.grdReqProh.RowHeadersVisible = false;
            this.grdReqProh.Size = new System.Drawing.Size(410, 296);
            this.grdReqProh.TabIndex = 3;
            // 
            // ean
            // 
            this.ean.DataPropertyName = "ean";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ean.DefaultCellStyle = dataGridViewCellStyle2;
            this.ean.FillWeight = 30F;
            this.ean.HeaderText = "EAN";
            this.ean.Name = "ean";
            this.ean.ReadOnly = true;
            // 
            // cname
            // 
            this.cname.DataPropertyName = "cName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cname.DefaultCellStyle = dataGridViewCellStyle3;
            this.cname.HeaderText = "Наименование товара";
            this.cname.Name = "cname";
            this.cname.ReadOnly = true;
            // 
            // frmProhMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 451);
            this.ControlBox = false;
            this.Controls.Add(this.grdReqProh);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.pbMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProhMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление товара в заявку";
            this.Load += new System.EventHandler(this.frmProhMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReqProh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMessage;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.DataGridView grdReqProh;
        private System.Windows.Forms.DataGridViewTextBoxColumn ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn cname;
    }
}