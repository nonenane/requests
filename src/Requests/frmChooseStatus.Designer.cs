namespace Requests
{
    partial class frmChooseStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChooseStatus));
            this.chkMan = new System.Windows.Forms.CheckBox();
            this.chkDraft = new System.Windows.Forms.CheckBox();
            this.chkInProcess = new System.Windows.Forms.CheckBox();
            this.chkDecline = new System.Windows.Forms.CheckBox();
            this.chkVozvr = new System.Windows.Forms.CheckBox();
            this.chkAccept = new System.Windows.Forms.CheckBox();
            this.chkNeedKDAccepr = new System.Windows.Forms.CheckBox();
            this.chkNeedRBSixAccept = new System.Windows.Forms.CheckBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.ttStatusFilter = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // chkMan
            // 
            this.chkMan.AutoSize = true;
            this.chkMan.Location = new System.Drawing.Point(12, 12);
            this.chkMan.Name = "chkMan";
            this.chkMan.Size = new System.Drawing.Size(92, 17);
            this.chkMan.TabIndex = 0;
            this.chkMan.Tag = "0";
            this.chkMan.Text = "у менеджера";
            this.chkMan.UseVisualStyleBackColor = true;
            this.chkMan.Visible = false;
            // 
            // chkDraft
            // 
            this.chkDraft.AutoSize = true;
            this.chkDraft.Location = new System.Drawing.Point(110, 12);
            this.chkDraft.Name = "chkDraft";
            this.chkDraft.Size = new System.Drawing.Size(73, 17);
            this.chkDraft.TabIndex = 1;
            this.chkDraft.Tag = "6";
            this.chkDraft.Text = "черновик";
            this.chkDraft.UseVisualStyleBackColor = true;
            // 
            // chkInProcess
            // 
            this.chkInProcess.AutoSize = true;
            this.chkInProcess.Location = new System.Drawing.Point(189, 12);
            this.chkInProcess.Name = "chkInProcess";
            this.chkInProcess.Size = new System.Drawing.Size(83, 17);
            this.chkInProcess.TabIndex = 2;
            this.chkInProcess.Tag = "2";
            this.chkInProcess.Text = "в процессе";
            this.chkInProcess.UseVisualStyleBackColor = true;
            // 
            // chkDecline
            // 
            this.chkDecline.AutoSize = true;
            this.chkDecline.Location = new System.Drawing.Point(12, 35);
            this.chkDecline.Name = "chkDecline";
            this.chkDecline.Size = new System.Drawing.Size(79, 17);
            this.chkDecline.TabIndex = 3;
            this.chkDecline.Tag = "3";
            this.chkDecline.Text = "отклонена";
            this.chkDecline.UseVisualStyleBackColor = true;
            // 
            // chkVozvr
            // 
            this.chkVozvr.AutoSize = true;
            this.chkVozvr.Location = new System.Drawing.Point(110, 35);
            this.chkVozvr.Name = "chkVozvr";
            this.chkVozvr.Size = new System.Drawing.Size(67, 17);
            this.chkVozvr.TabIndex = 4;
            this.chkVozvr.Tag = "11";
            this.chkVozvr.Text = "возврат";
            this.chkVozvr.UseVisualStyleBackColor = true;
            // 
            // chkAccept
            // 
            this.chkAccept.AutoSize = true;
            this.chkAccept.Location = new System.Drawing.Point(189, 35);
            this.chkAccept.Name = "chkAccept";
            this.chkAccept.Size = new System.Drawing.Size(99, 17);
            this.chkAccept.TabIndex = 5;
            this.chkAccept.Tag = "12";
            this.chkAccept.Text = "подтверждена";
            this.chkAccept.UseVisualStyleBackColor = true;
            // 
            // chkNeedKDAccepr
            // 
            this.chkNeedKDAccepr.AutoSize = true;
            this.chkNeedKDAccepr.Location = new System.Drawing.Point(12, 68);
            this.chkNeedKDAccepr.Name = "chkNeedKDAccepr";
            this.chkNeedKDAccepr.Size = new System.Drawing.Size(153, 17);
            this.chkNeedKDAccepr.TabIndex = 6;
            this.chkNeedKDAccepr.Tag = "7";
            this.chkNeedKDAccepr.Text = "треб. подтверждение КД";
            this.chkNeedKDAccepr.UseVisualStyleBackColor = true;
            // 
            // chkNeedRBSixAccept
            // 
            this.chkNeedRBSixAccept.AutoSize = true;
            this.chkNeedRBSixAccept.Location = new System.Drawing.Point(12, 91);
            this.chkNeedRBSixAccept.Name = "chkNeedRBSixAccept";
            this.chkNeedRBSixAccept.Size = new System.Drawing.Size(157, 17);
            this.chkNeedRBSixAccept.TabIndex = 7;
            this.chkNeedRBSixAccept.Tag = "8";
            this.chkNeedRBSixAccept.Text = "треб. подтверждение РБ6";
            this.chkNeedRBSixAccept.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSave.BackgroundImage")));
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btSave.Location = new System.Drawing.Point(241, 91);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(25, 25);
            this.btSave.TabIndex = 8;
            this.ttStatusFilter.SetToolTip(this.btSave, "Сохранить");
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btExit
            // 
            this.btExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btExit.BackgroundImage")));
            this.btExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExit.Location = new System.Drawing.Point(272, 91);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(25, 25);
            this.btExit.TabIndex = 9;
            this.ttStatusFilter.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // frmChooseStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 120);
            this.ControlBox = false;
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.chkNeedRBSixAccept);
            this.Controls.Add(this.chkNeedKDAccepr);
            this.Controls.Add(this.chkAccept);
            this.Controls.Add(this.chkVozvr);
            this.Controls.Add(this.chkDecline);
            this.Controls.Add(this.chkInProcess);
            this.Controls.Add(this.chkDraft);
            this.Controls.Add(this.chkMan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChooseStatus";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор статусов";
            this.Load += new System.EventHandler(this.frmChooseStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMan;
        private System.Windows.Forms.CheckBox chkDraft;
        private System.Windows.Forms.CheckBox chkInProcess;
        private System.Windows.Forms.CheckBox chkDecline;
        private System.Windows.Forms.CheckBox chkVozvr;
        private System.Windows.Forms.CheckBox chkAccept;
        private System.Windows.Forms.CheckBox chkNeedKDAccepr;
        private System.Windows.Forms.CheckBox chkNeedRBSixAccept;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.ToolTip ttStatusFilter;
    }
}