namespace Requests
{
    partial class frmReportChoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportChoice));
            this.rbRequestList = new System.Windows.Forms.RadioButton();
            this.rbPostData = new System.Windows.Forms.RadioButton();
            this.btExit = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // rbRequestList
            // 
            this.rbRequestList.AutoSize = true;
            this.rbRequestList.Checked = true;
            this.rbRequestList.Location = new System.Drawing.Point(12, 12);
            this.rbRequestList.Name = "rbRequestList";
            this.rbRequestList.Size = new System.Drawing.Size(101, 17);
            this.rbRequestList.TabIndex = 0;
            this.rbRequestList.TabStop = true;
            this.rbRequestList.Text = "Список заявок";
            this.rbRequestList.UseVisualStyleBackColor = true;
            // 
            // rbPostData
            // 
            this.rbPostData.AutoSize = true;
            this.rbPostData.Location = new System.Drawing.Point(12, 35);
            this.rbPostData.Name = "rbPostData";
            this.rbPostData.Size = new System.Drawing.Size(154, 17);
            this.rbPostData.TabIndex = 1;
            this.rbPostData.Text = "Данные по поставщикам";
            this.rbPostData.UseVisualStyleBackColor = true;
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = ((System.Drawing.Image)(resources.GetObject("btExit.Image")));
            this.btExit.Location = new System.Drawing.Point(209, 28);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(25, 25);
            this.btExit.TabIndex = 5;
            this.ttMain.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = ((System.Drawing.Image)(resources.GetObject("btPrint.Image")));
            this.btPrint.Location = new System.Drawing.Point(178, 28);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(25, 25);
            this.btPrint.TabIndex = 6;
            this.ttMain.SetToolTip(this.btPrint, "Печать");
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // frmReportChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 61);
            this.ControlBox = false;
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.rbPostData);
            this.Controls.Add(this.rbRequestList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportChoice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор отчёта";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbRequestList;
        private System.Windows.Forms.RadioButton rbPostData;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.ToolTip ttMain;
    }
}