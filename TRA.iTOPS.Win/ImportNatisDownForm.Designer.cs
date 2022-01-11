namespace TRA.iTOPS.Win
{
    partial class ImportNatisDownForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportNatisDownForm));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Lst_History = new System.Windows.Forms.ListView();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.Edt_Log = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Lbl_FilePath = new Infragistics.Win.Misc.UltraLabel();
            this.Btn_ImportFile = new Infragistics.Win.Misc.UltraButton();
            this.Btn_DownFile = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
            this.Chk_SequenceNum = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.Chk_WaitingDownStatus = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.Chk_TRANID = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.Chk_SameVehicle = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.Rb_TCS = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Rb_DownCITAS = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Rb_DownNATIS = new Infragistics.Win.UltraWinEditors.UltraRadioButton();
            this.Btn_Close = new Infragistics.Win.Misc.UltraButton();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Log)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).BeginInit();
            this.ultraGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_SequenceNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_WaitingDownStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_TRANID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_SameVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).BeginInit();
            this.ultraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_TCS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_DownCITAS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_DownNATIS)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(5, 5);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(901, 447);
            this.panel3.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Lst_History);
            this.panel2.Controls.Add(this.ultraLabel6);
            this.panel2.Controls.Add(this.Edt_Log);
            this.panel2.Controls.Add(this.ultraLabel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(453, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(429, 435);
            this.panel2.TabIndex = 4;
            // 
            // Lst_History
            // 
            this.Lst_History.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lst_History.HideSelection = false;
            this.Lst_History.Location = new System.Drawing.Point(12, 236);
            this.Lst_History.Name = "Lst_History";
            this.Lst_History.Size = new System.Drawing.Size(404, 186);
            this.Lst_History.TabIndex = 18;
            this.Lst_History.UseCompatibleStateImageBehavior = false;
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.AutoSize = true;
            this.ultraLabel6.Location = new System.Drawing.Point(12, 214);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(167, 15);
            this.ultraLabel6.TabIndex = 17;
            this.ultraLabel6.Text = "History of up and down files ..";
            // 
            // Edt_Log
            // 
            this.Edt_Log.Location = new System.Drawing.Point(14, 24);
            this.Edt_Log.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Edt_Log.Multiline = true;
            this.Edt_Log.Name = "Edt_Log";
            this.Edt_Log.Scrollbars = System.Windows.Forms.ScrollBars.Both;
            this.Edt_Log.Size = new System.Drawing.Size(402, 166);
            this.Edt_Log.TabIndex = 15;
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.AutoSize = true;
            this.ultraLabel2.Location = new System.Drawing.Point(18, 4);
            this.ultraLabel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(90, 15);
            this.ultraLabel2.TabIndex = 14;
            this.ultraLabel2.Text = "Processing Log";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Lbl_FilePath);
            this.panel1.Controls.Add(this.Btn_ImportFile);
            this.panel1.Controls.Add(this.Btn_DownFile);
            this.panel1.Controls.Add(this.ultraLabel5);
            this.panel1.Controls.Add(this.ultraGroupBox4);
            this.panel1.Controls.Add(this.ultraGroupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.panel1.Size = new System.Drawing.Size(448, 435);
            this.panel1.TabIndex = 3;
            // 
            // Lbl_FilePath
            // 
            this.Lbl_FilePath.AutoSize = true;
            this.Lbl_FilePath.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.Lbl_FilePath.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_FilePath.Location = new System.Drawing.Point(9, 276);
            this.Lbl_FilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Lbl_FilePath.Name = "Lbl_FilePath";
            this.Lbl_FilePath.Padding = new System.Drawing.Size(3, 3);
            this.Lbl_FilePath.Size = new System.Drawing.Size(117, 27);
            this.Lbl_FilePath.TabIndex = 7;
            this.Lbl_FilePath.Text = "select down file";
            // 
            // Btn_ImportFile
            // 
            this.Btn_ImportFile.Location = new System.Drawing.Point(224, 324);
            this.Btn_ImportFile.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Btn_ImportFile.Name = "Btn_ImportFile";
            this.Btn_ImportFile.Size = new System.Drawing.Size(140, 48);
            this.Btn_ImportFile.TabIndex = 6;
            this.Btn_ImportFile.Text = "Import Down File";
            this.Btn_ImportFile.Click += new System.EventHandler(this.Btn_ImportFile_Click);
            // 
            // Btn_DownFile
            // 
            this.Btn_DownFile.Location = new System.Drawing.Point(39, 324);
            this.Btn_DownFile.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Btn_DownFile.Name = "Btn_DownFile";
            this.Btn_DownFile.Size = new System.Drawing.Size(140, 48);
            this.Btn_DownFile.TabIndex = 5;
            this.Btn_DownFile.Text = "Look for Down File..";
            this.Btn_DownFile.Click += new System.EventHandler(this.Btn_DownFile_Click);
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.Location = new System.Drawing.Point(11, 252);
            this.ultraLabel5.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(113, 16);
            this.ultraLabel5.TabIndex = 4;
            this.ultraLabel5.Text = "Down FileName";
            // 
            // ultraGroupBox4
            // 
            this.ultraGroupBox4.Controls.Add(this.Chk_SequenceNum);
            this.ultraGroupBox4.Controls.Add(this.ultraLabel4);
            this.ultraGroupBox4.Controls.Add(this.Chk_WaitingDownStatus);
            this.ultraGroupBox4.Controls.Add(this.Chk_TRANID);
            this.ultraGroupBox4.Controls.Add(this.Chk_SameVehicle);
            this.ultraGroupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox4.Location = new System.Drawing.Point(9, 78);
            this.ultraGroupBox4.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ultraGroupBox4.Name = "ultraGroupBox4";
            this.ultraGroupBox4.Size = new System.Drawing.Size(430, 166);
            this.ultraGroupBox4.TabIndex = 2;
            this.ultraGroupBox4.Text = "Update Method";
            // 
            // Chk_SequenceNum
            // 
            this.Chk_SequenceNum.Location = new System.Drawing.Point(201, 72);
            this.Chk_SequenceNum.Name = "Chk_SequenceNum";
            this.Chk_SequenceNum.Size = new System.Drawing.Size(154, 20);
            this.Chk_SequenceNum.TabIndex = 5;
            this.Chk_SequenceNum.Text = "Same Sequence Num";
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.AutoSize = true;
            this.ultraLabel4.Location = new System.Drawing.Point(22, 114);
            this.ultraLabel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(290, 15);
            this.ultraLabel4.TabIndex = 4;
            this.ultraLabel4.Text = "All check is normal, uncheck for only test or experts.";
            // 
            // Chk_WaitingDownStatus
            // 
            this.Chk_WaitingDownStatus.AutoSize = true;
            this.Chk_WaitingDownStatus.Location = new System.Drawing.Point(201, 29);
            this.Chk_WaitingDownStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Chk_WaitingDownStatus.Name = "Chk_WaitingDownStatus";
            this.Chk_WaitingDownStatus.Size = new System.Drawing.Size(141, 18);
            this.Chk_WaitingDownStatus.TabIndex = 3;
            this.Chk_WaitingDownStatus.Text = "Waiting Down Status";
            // 
            // Chk_TRANID
            // 
            this.Chk_TRANID.AutoSize = true;
            this.Chk_TRANID.Location = new System.Drawing.Point(22, 72);
            this.Chk_TRANID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Chk_TRANID.Name = "Chk_TRANID";
            this.Chk_TRANID.Size = new System.Drawing.Size(107, 18);
            this.Chk_TRANID.TabIndex = 2;
            this.Chk_TRANID.Text = "Same TRAN ID";
            // 
            // Chk_SameVehicle
            // 
            this.Chk_SameVehicle.AutoSize = true;
            this.Chk_SameVehicle.Location = new System.Drawing.Point(23, 29);
            this.Chk_SameVehicle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Chk_SameVehicle.Name = "Chk_SameVehicle";
            this.Chk_SameVehicle.Size = new System.Drawing.Size(130, 18);
            this.Chk_SameVehicle.TabIndex = 0;
            this.Chk_SameVehicle.Text = "Same Vehicle Num";
            // 
            // ultraGroupBox3
            // 
            this.ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularDoubleSolid;
            this.ultraGroupBox3.Controls.Add(this.Rb_TCS);
            this.ultraGroupBox3.Controls.Add(this.Rb_DownCITAS);
            this.ultraGroupBox3.Controls.Add(this.Rb_DownNATIS);
            this.ultraGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox3.Location = new System.Drawing.Point(9, 12);
            this.ultraGroupBox3.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.ultraGroupBox3.Name = "ultraGroupBox3";
            this.ultraGroupBox3.Size = new System.Drawing.Size(430, 66);
            this.ultraGroupBox3.TabIndex = 1;
            this.ultraGroupBox3.Text = "Import Down File Format";
            // 
            // Rb_TCS
            // 
            this.Rb_TCS.AutoSize = true;
            this.Rb_TCS.Enabled = false;
            this.Rb_TCS.Location = new System.Drawing.Point(296, 28);
            this.Rb_TCS.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Rb_TCS.Name = "Rb_TCS";
            this.Rb_TCS.Size = new System.Drawing.Size(46, 18);
            this.Rb_TCS.TabIndex = 2;
            this.Rb_TCS.Text = "TCS";
            this.Rb_TCS.Visible = false;
            // 
            // Rb_DownCITAS
            // 
            this.Rb_DownCITAS.AutoSize = true;
            this.Rb_DownCITAS.Enabled = false;
            this.Rb_DownCITAS.Location = new System.Drawing.Point(156, 28);
            this.Rb_DownCITAS.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Rb_DownCITAS.Name = "Rb_DownCITAS";
            this.Rb_DownCITAS.Size = new System.Drawing.Size(70, 18);
            this.Rb_DownCITAS.TabIndex = 1;
            this.Rb_DownCITAS.Text = "CIVITAS";
            this.Rb_DownCITAS.Visible = false;
            // 
            // Rb_DownNATIS
            // 
            this.Rb_DownNATIS.AutoSize = true;
            this.Rb_DownNATIS.Location = new System.Drawing.Point(22, 28);
            this.Rb_DownNATIS.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Rb_DownNATIS.Name = "Rb_DownNATIS";
            this.Rb_DownNATIS.Size = new System.Drawing.Size(58, 18);
            this.Rb_DownNATIS.TabIndex = 0;
            this.Rb_DownNATIS.Text = "NATIS";
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(389, 479);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(133, 38);
            this.Btn_Close.TabIndex = 6;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // ImportNatisDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 536);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ImportNatisDownForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Natis Down";
            this.Load += new System.EventHandler(this.NatisImportDownForm_Load);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Log)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).EndInit();
            this.ultraGroupBox4.ResumeLayout(false);
            this.ultraGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_SequenceNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_WaitingDownStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_TRANID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_SameVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).EndInit();
            this.ultraGroupBox3.ResumeLayout(false);
            this.ultraGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_TCS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_DownCITAS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rb_DownNATIS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView Lst_History;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Edt_Log;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.Misc.UltraLabel Lbl_FilePath;
        private Infragistics.Win.Misc.UltraButton Btn_ImportFile;
        private Infragistics.Win.Misc.UltraButton Btn_DownFile;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_WaitingDownStatus;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_TRANID;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_SameVehicle;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox3;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_TCS;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_DownCITAS;
        private Infragistics.Win.UltraWinEditors.UltraRadioButton Rb_DownNATIS;
        private Infragistics.Win.Misc.UltraButton Btn_Close;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_SequenceNum;
    }
}