namespace Requests
{
    partial class frmAddFileName
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
					this.tbPrimech = new System.Windows.Forms.TextBox();
					this.btCancel = new System.Windows.Forms.Button();
					this.btOK = new System.Windows.Forms.Button();
					this.SuspendLayout();
					// 
					// tbPrimech
					// 
					this.tbPrimech.Location = new System.Drawing.Point(12, 12);
					this.tbPrimech.Name = "tbPrimech";
					this.tbPrimech.Size = new System.Drawing.Size(273, 20);
					this.tbPrimech.TabIndex = 3;
					// 
					// btCancel
					// 
					this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.btCancel.Location = new System.Drawing.Point(210, 38);
					this.btCancel.Name = "btCancel";
					this.btCancel.Size = new System.Drawing.Size(75, 23);
					this.btCancel.TabIndex = 5;
					this.btCancel.Text = "Отмена";
					this.btCancel.UseVisualStyleBackColor = true;
					this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
					// 
					// btOK
					// 
					this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
					this.btOK.Location = new System.Drawing.Point(129, 38);
					this.btOK.Name = "btOK";
					this.btOK.Size = new System.Drawing.Size(75, 23);
					this.btOK.TabIndex = 4;
					this.btOK.Text = "OK";
					this.btOK.UseVisualStyleBackColor = true;
					this.btOK.Click += new System.EventHandler(this.btOK_Click);
					// 
					// frmAddFileName
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(288, 69);
					this.ControlBox = false;
					this.Controls.Add(this.btCancel);
					this.Controls.Add(this.btOK);
					this.Controls.Add(this.tbPrimech);
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
					this.MaximizeBox = false;
					this.MinimizeBox = false;
					this.Name = "frmAddFileName";
					this.ShowIcon = false;
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "Ввод наименования изображения";
					this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddFileName_FormClosing);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPrimech;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
    }
}