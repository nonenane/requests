namespace Requests
{
    partial class frmSendRequest
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
            this.label1 = new System.Windows.Forms.Label();
            this.btRemain = new System.Windows.Forms.Button();
            this.btSend = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сохранить заявку для дальнейшего редактирования без\r\nпередачи руководителю?\r\n\r\nОт" +
                "мена - возврат к основной форме.\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btRemain
            // 
            this.btRemain.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btRemain.Location = new System.Drawing.Point(12, 72);
            this.btRemain.Name = "btRemain";
            this.btRemain.Size = new System.Drawing.Size(142, 23);
            this.btRemain.TabIndex = 1;
            this.btRemain.Text = "Сохранить без передачи";
            this.btRemain.UseVisualStyleBackColor = true;
            // 
            // btSend
            // 
            this.btSend.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btSend.Location = new System.Drawing.Point(160, 72);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(139, 23);
            this.btSend.TabIndex = 2;
            this.btSend.Text = "Передать руководителю";
            this.btSend.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(305, 72);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(78, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // frmSendRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 107);
            this.ControlBox = false;
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.btRemain);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSendRequest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запрос на сохранение";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRemain;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Button btCancel;
    }
}