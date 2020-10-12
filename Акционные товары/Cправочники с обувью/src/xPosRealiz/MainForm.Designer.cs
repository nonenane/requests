namespace xPosRealiz
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timerRealiz = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.rtbSprav = new System.Windows.Forms.RichTextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblSprav = new System.Windows.Forms.Label();
            this.bwSparv = new System.ComponentModel.BackgroundWorker();
            this.bwFullSprav = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRealiz
            // 
            this.timerRealiz.Interval = 1000;
            this.timerRealiz.Tick += new System.EventHandler(this.timerRealiz_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnResume);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.rtbSprav);
            this.panel1.Controls.Add(this.btnCreate);
            this.panel1.Controls.Add(this.lblSprav);
            this.panel1.Location = new System.Drawing.Point(6, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 478);
            this.panel1.TabIndex = 2;            
            // 
            // btnResume
            // 
            this.btnResume.Enabled = false;
            this.btnResume.Location = new System.Drawing.Point(238, 36);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(81, 23);
            this.btnResume.TabIndex = 10;
            this.btnResume.Text = "Возобновить";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(143, 36);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "Остановить";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // rtbSprav
            // 
            this.rtbSprav.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbSprav.Location = new System.Drawing.Point(3, 74);
            this.rtbSprav.Name = "rtbSprav";
            this.rtbSprav.Size = new System.Drawing.Size(324, 390);
            this.rtbSprav.TabIndex = 8;
            this.rtbSprav.Text = "";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(15, 26);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(114, 42);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "Выгрузить полный справочник";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblSprav
            // 
            this.lblSprav.AutoSize = true;
            this.lblSprav.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSprav.Location = new System.Drawing.Point(71, 6);
            this.lblSprav.Name = "lblSprav";
            this.lblSprav.Size = new System.Drawing.Size(168, 17);
            this.lblSprav.TabIndex = 6;
            this.lblSprav.Text = "Справочник с обувью";            
            // 
            // bwSparv
            // 
            this.bwSparv.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSparv_DoWork);
            this.bwSparv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSparv_RunWorkerCompleted);
            // 
            // bwFullSprav
            // 
            this.bwFullSprav.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwFullSprav_DoWork);
            this.bwFullSprav.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwFullSprav_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 474);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(530, 10);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочники";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);            
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerRealiz;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSprav;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.RichTextBox rtbSprav;
        private System.ComponentModel.BackgroundWorker bwSparv;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnPause;
        private System.ComponentModel.BackgroundWorker bwFullSprav;

    }
}

