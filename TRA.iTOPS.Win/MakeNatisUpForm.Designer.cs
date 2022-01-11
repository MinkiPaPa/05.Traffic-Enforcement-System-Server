namespace TRA.iTOPS.Win
{
    partial class MakeNatisUpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakeNatisUpForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Lst_History = new System.Windows.Forms.ListView();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.BarProgress = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.Edt_Log = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_FilePath = new Infragistics.Win.Misc.UltraLabel();
            this.Btn_MakeUpFile = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.Lbl_LastDay = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.Dt_Today = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.RB_Retry = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Rb_StillNotDown = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Rb_FirstUp = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.Rb_UpTCS = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Rb_UpCIVITAS = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Rb_UpNatis = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Btn_Close = new Infragistics.Win.Misc.UltraButton();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Log)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Today)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RB_Retry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_StillNotDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_FirstUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_UpTCS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_UpCIVITAS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_UpNatis)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9, 13, 9, 13);
            this.panel1.Size = new System.Drawing.Size(1000, 423);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Lst_History);
            this.panel3.Controls.Add(this.ultraLabel6);
            this.panel3.Controls.Add(this.BarProgress);
            this.panel3.Controls.Add(this.Edt_Log);
            this.panel3.Controls.Add(this.ultraLabel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(469, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(515, 395);
            this.panel3.TabIndex = 5;
            // 
            // Lst_History
            // 
            this.Lst_History.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lst_History.HideSelection = false;
            this.Lst_History.Location = new System.Drawing.Point(9, 215);
            this.Lst_History.Name = "Lst_History";
            this.Lst_History.Size = new System.Drawing.Size(491, 171);
            this.Lst_History.TabIndex = 18;
            this.Lst_History.UseCompatibleStateImageBehavior = false;
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Location = new System.Drawing.Point(9, 193);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(197, 23);
            this.ultraLabel6.TabIndex = 17;
            this.ultraLabel6.Text = "History of up and down files ..";
            // 
            // BarProgress
            // 
            this.BarProgress.Location = new System.Drawing.Point(11, 160);
            this.BarProgress.Name = "BarProgress";
            this.BarProgress.Size = new System.Drawing.Size(489, 31);
            this.BarProgress.TabIndex = 16;
            this.BarProgress.Text = "[Formatted]";
            // 
            // Edt_Log
            // 
            this.Edt_Log.Location = new System.Drawing.Point(11, 31);
            this.Edt_Log.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Edt_Log.Multiline = true;
            this.Edt_Log.Name = "Edt_Log";
            this.Edt_Log.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.Edt_Log.Size = new System.Drawing.Size(489, 126);
            this.Edt_Log.TabIndex = 15;
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(15, 11);
            this.ultraLabel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(121, 21);
            this.ultraLabel2.TabIndex = 14;
            this.ultraLabel2.Text = "Processing Log";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.lbl_FilePath);
            this.panel2.Controls.Add(this.Btn_MakeUpFile);
            this.panel2.Controls.Add(this.ultraLabel1);
            this.panel2.Controls.Add(this.ultraGroupBox2);
            this.panel2.Controls.Add(this.ultraGroupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(9, 13);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.panel2.Size = new System.Drawing.Size(460, 395);
            this.panel2.TabIndex = 0;
            // 
            // lbl_FilePath
            // 
            this.lbl_FilePath.AutoSize = true;
            this.lbl_FilePath.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
            this.lbl_FilePath.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FilePath.Location = new System.Drawing.Point(14, 263);
            this.lbl_FilePath.Name = "lbl_FilePath";
            this.lbl_FilePath.Padding = new System.Drawing.Size(3, 3);
            this.lbl_FilePath.Size = new System.Drawing.Size(66, 25);
            this.lbl_FilePath.TabIndex = 14;
            this.lbl_FilePath.Text = "FilePath";
            // 
            // Btn_MakeUpFile
            // 
            this.Btn_MakeUpFile.Location = new System.Drawing.Point(302, 296);
            this.Btn_MakeUpFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Btn_MakeUpFile.Name = "Btn_MakeUpFile";
            this.Btn_MakeUpFile.Size = new System.Drawing.Size(118, 38);
            this.Btn_MakeUpFile.TabIndex = 4;
            this.Btn_MakeUpFile.Text = "Make Up file";
            this.Btn_MakeUpFile.Click += new System.EventHandler(this.Btn_MakeUpFile_Click);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(14, 239);
            this.ultraLabel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(83, 17);
            this.ultraLabel1.TabIndex = 2;
            this.ultraLabel1.Text = "Up FileName";
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularDoubleSolid;
            this.ultraGroupBox2.Controls.Add(this.Lbl_LastDay);
            this.ultraGroupBox2.Controls.Add(this.ultraLabel3);
            this.ultraGroupBox2.Controls.Add(this.Dt_Today);
            this.ultraGroupBox2.Controls.Add(this.RB_Retry);
            this.ultraGroupBox2.Controls.Add(this.Rb_StillNotDown);
            this.ultraGroupBox2.Controls.Add(this.Rb_FirstUp);
            this.ultraGroupBox2.Location = new System.Drawing.Point(9, 80);
            this.ultraGroupBox2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(412, 114);
            this.ultraGroupBox2.TabIndex = 1;
            this.ultraGroupBox2.Text = "Up conditions";
            // 
            // Lbl_LastDay
            // 
            this.Lbl_LastDay.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
            this.Lbl_LastDay.Location = new System.Drawing.Point(139, 68);
            this.Lbl_LastDay.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Lbl_LastDay.Name = "Lbl_LastDay";
            this.Lbl_LastDay.Size = new System.Drawing.Size(100, 23);
            this.Lbl_LastDay.TabIndex = 5;
            this.Lbl_LastDay.Text = "last up Date";
            this.Lbl_LastDay.Visible = false;
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Location = new System.Drawing.Point(121, 69);
            this.ultraLabel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(16, 23);
            this.ultraLabel3.TabIndex = 4;
            this.ultraLabel3.Text = "~";
            this.ultraLabel3.Visible = false;
            // 
            // Dt_Today
            // 
            this.Dt_Today.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.Dt_Today.Location = new System.Drawing.Point(20, 68);
            this.Dt_Today.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Dt_Today.Name = "Dt_Today";
            this.Dt_Today.Size = new System.Drawing.Size(96, 24);
            this.Dt_Today.TabIndex = 3;
            this.Dt_Today.Visible = false;
            // 
            // RB_Retry
            // 
            this.RB_Retry.AutoSize = true;
            this.RB_Retry.Location = new System.Drawing.Point(247, 29);
            this.RB_Retry.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.RB_Retry.Name = "RB_Retry";
            this.RB_Retry.Size = new System.Drawing.Size(149, 20);
            this.RB_Retry.TabIndex = 2;
            this.RB_Retry.Text = "Retry for Bad Answer";
            // 
            // Rb_StillNotDown
            // 
            this.Rb_StillNotDown.AutoSize = true;
            this.Rb_StillNotDown.Location = new System.Drawing.Point(114, 30);
            this.Rb_StillNotDown.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Rb_StillNotDown.Name = "Rb_StillNotDown";
            this.Rb_StillNotDown.Size = new System.Drawing.Size(106, 20);
            this.Rb_StillNotDown.TabIndex = 1;
            this.Rb_StillNotDown.Text = "Still Not Down";
            // 
            // Rb_FirstUp
            // 
            this.Rb_FirstUp.AutoSize = true;
            this.Rb_FirstUp.Location = new System.Drawing.Point(16, 29);
            this.Rb_FirstUp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Rb_FirstUp.Name = "Rb_FirstUp";
            this.Rb_FirstUp.Size = new System.Drawing.Size(69, 20);
            this.Rb_FirstUp.TabIndex = 0;
            this.Rb_FirstUp.Text = "First Up";
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularDoubleSolid;
            this.ultraGroupBox1.Controls.Add(this.Rb_UpTCS);
            this.ultraGroupBox1.Controls.Add(this.Rb_UpCIVITAS);
            this.ultraGroupBox1.Controls.Add(this.Rb_UpNatis);
            this.ultraGroupBox1.Location = new System.Drawing.Point(9, 10);
            this.ultraGroupBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(412, 53);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "UpFile Format";
            // 
            // Rb_UpTCS
            // 
            this.Rb_UpTCS.AutoSize = true;
            this.Rb_UpTCS.Enabled = false;
            this.Rb_UpTCS.Location = new System.Drawing.Point(296, 22);
            this.Rb_UpTCS.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Rb_UpTCS.Name = "Rb_UpTCS";
            this.Rb_UpTCS.Size = new System.Drawing.Size(49, 20);
            this.Rb_UpTCS.TabIndex = 2;
            this.Rb_UpTCS.Text = "TCS";
            this.Rb_UpTCS.Visible = false;
            // 
            // Rb_UpCIVITAS
            // 
            this.Rb_UpCIVITAS.AutoSize = true;
            this.Rb_UpCIVITAS.Enabled = false;
            this.Rb_UpCIVITAS.Location = new System.Drawing.Point(156, 22);
            this.Rb_UpCIVITAS.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Rb_UpCIVITAS.Name = "Rb_UpCIVITAS";
            this.Rb_UpCIVITAS.Size = new System.Drawing.Size(74, 20);
            this.Rb_UpCIVITAS.TabIndex = 1;
            this.Rb_UpCIVITAS.Text = "CIVITAS";
            this.Rb_UpCIVITAS.Visible = false;
            // 
            // Rb_UpNatis
            // 
            this.Rb_UpNatis.AutoSize = true;
            this.Rb_UpNatis.Location = new System.Drawing.Point(22, 22);
            this.Rb_UpNatis.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Rb_UpNatis.Name = "Rb_UpNatis";
            this.Rb_UpNatis.Size = new System.Drawing.Size(61, 20);
            this.Rb_UpNatis.TabIndex = 0;
            this.Rb_UpNatis.Text = "NATIS";
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(407, 430);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(133, 38);
            this.Btn_Close.TabIndex = 5;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // MakeNatisUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 482);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "MakeNatisUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Make Natis Up";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NatisInterfaceForm_FormClosing);
            this.Load += new System.EventHandler(this.NatisUpDownForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Log)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Today)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RB_Retry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_StillNotDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_FirstUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_UpTCS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_UpCIVITAS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_UpNatis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraButton Btn_MakeUpFile;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton RB_Retry;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_StillNotDown;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_FirstUp;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_UpTCS;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_UpCIVITAS;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_UpNatis;
        private Infragistics.Win.Misc.UltraLabel Lbl_LastDay;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor Dt_Today;
        private Infragistics.Win.Misc.UltraLabel lbl_FilePath;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView Lst_History;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar BarProgress;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Edt_Log;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraButton Btn_Close;
    }
}