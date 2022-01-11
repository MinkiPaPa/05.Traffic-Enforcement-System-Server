namespace TRA.iTOPS.Win
{
    partial class NumberingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Lst_History = new System.Windows.Forms.ListView();
            this.Btn_Close = new Infragistics.Win.Misc.UltraButton();
            this.Btn_Do = new Infragistics.Win.Misc.UltraButton();
            this.BarProgress = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.Edt_Log = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.Txt_OffenEndDate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.Txt_OffenStartDate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.Txt_CandiNumber = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.Btn_Get = new Infragistics.Win.Misc.UltraButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.Chk_EasyPayNumber = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.Chk_NoticeNumber = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Log)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_OffenEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_OffenStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_CandiNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_EasyPayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_NoticeNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.ultraGroupBox2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.ultraGroupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(8, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 13, 10, 13);
            this.panel1.Size = new System.Drawing.Size(806, 530);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.Lst_History);
            this.panel4.Controls.Add(this.Btn_Close);
            this.panel4.Controls.Add(this.Btn_Do);
            this.panel4.Controls.Add(this.BarProgress);
            this.panel4.Controls.Add(this.Edt_Log);
            this.panel4.Controls.Add(this.ultraLabel4);
            this.panel4.Controls.Add(this.ultraLabel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 259);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(786, 255);
            this.panel4.TabIndex = 4;
            // 
            // Lst_History
            // 
            this.Lst_History.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lst_History.HideSelection = false;
            this.Lst_History.Location = new System.Drawing.Point(363, 40);
            this.Lst_History.Name = "Lst_History";
            this.Lst_History.Size = new System.Drawing.Size(420, 149);
            this.Lst_History.TabIndex = 7;
            this.Lst_History.UseCompatibleStateImageBehavior = false;
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(654, 195);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(99, 47);
            this.Btn_Close.TabIndex = 6;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_Do
            // 
            this.Btn_Do.Enabled = false;
            this.Btn_Do.Location = new System.Drawing.Point(526, 195);
            this.Btn_Do.Name = "Btn_Do";
            this.Btn_Do.Size = new System.Drawing.Size(106, 47);
            this.Btn_Do.TabIndex = 5;
            this.Btn_Do.Text = "Do";
            this.Btn_Do.Click += new System.EventHandler(this.Btn_Do_Click);
            // 
            // BarProgress
            // 
            this.BarProgress.Location = new System.Drawing.Point(27, 196);
            this.BarProgress.Name = "BarProgress";
            this.BarProgress.Size = new System.Drawing.Size(317, 23);
            this.BarProgress.TabIndex = 4;
            this.BarProgress.Text = "[Formatted]";
            // 
            // Edt_Log
            // 
            this.Edt_Log.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edt_Log.Location = new System.Drawing.Point(27, 38);
            this.Edt_Log.Multiline = true;
            this.Edt_Log.Name = "Edt_Log";
            this.Edt_Log.Size = new System.Drawing.Size(317, 151);
            this.Edt_Log.TabIndex = 2;
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.AutoSize = true;
            this.ultraLabel4.Location = new System.Drawing.Point(363, 11);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(46, 17);
            this.ultraLabel4.TabIndex = 1;
            this.ultraLabel4.Text = "History";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.AutoSize = true;
            this.ultraLabel3.Location = new System.Drawing.Point(29, 11);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(91, 17);
            this.ultraLabel3.TabIndex = 0;
            this.ultraLabel3.Text = "processing log";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 224);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(786, 35);
            this.panel3.TabIndex = 3;
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularDoubleSolid;
            this.ultraGroupBox2.Controls.Add(this.ultraLabel5);
            this.ultraGroupBox2.Controls.Add(this.Txt_OffenEndDate);
            this.ultraGroupBox2.Controls.Add(this.Txt_OffenStartDate);
            this.ultraGroupBox2.Controls.Add(this.Txt_CandiNumber);
            this.ultraGroupBox2.Controls.Add(this.ultraLabel2);
            this.ultraGroupBox2.Controls.Add(this.ultraLabel1);
            this.ultraGroupBox2.Controls.Add(this.Btn_Get);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox2.Location = new System.Drawing.Point(10, 112);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(786, 112);
            this.ultraGroupBox2.TabIndex = 2;
            this.ultraGroupBox2.Text = "Candidate to be Numbering";
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.Location = new System.Drawing.Point(603, 64);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(16, 23);
            this.ultraLabel5.TabIndex = 6;
            this.ultraLabel5.Text = "~";
            // 
            // Txt_OffenEndDate
            // 
            this.Txt_OffenEndDate.Location = new System.Drawing.Point(620, 64);
            this.Txt_OffenEndDate.Name = "Txt_OffenEndDate";
            this.Txt_OffenEndDate.ReadOnly = true;
            this.Txt_OffenEndDate.Size = new System.Drawing.Size(151, 24);
            this.Txt_OffenEndDate.TabIndex = 5;
            // 
            // Txt_OffenStartDate
            // 
            this.Txt_OffenStartDate.Location = new System.Drawing.Point(449, 64);
            this.Txt_OffenStartDate.Name = "Txt_OffenStartDate";
            this.Txt_OffenStartDate.ReadOnly = true;
            this.Txt_OffenStartDate.Size = new System.Drawing.Size(151, 24);
            this.Txt_OffenStartDate.TabIndex = 4;
            // 
            // Txt_CandiNumber
            // 
            this.Txt_CandiNumber.Location = new System.Drawing.Point(254, 64);
            this.Txt_CandiNumber.Name = "Txt_CandiNumber";
            this.Txt_CandiNumber.ReadOnly = true;
            this.Txt_CandiNumber.Size = new System.Drawing.Size(100, 24);
            this.Txt_CandiNumber.TabIndex = 3;
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.AutoSize = true;
            this.ultraLabel2.Location = new System.Drawing.Point(559, 32);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(84, 17);
            this.ultraLabel2.TabIndex = 2;
            this.ultraLabel2.Text = "Offence Date";
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(234, 32);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(166, 17);
            this.ultraLabel1.TabIndex = 1;
            this.ultraLabel1.Text = "Total Number of Candidate";
            // 
            // Btn_Get
            // 
            this.Btn_Get.Location = new System.Drawing.Point(13, 40);
            this.Btn_Get.Name = "Btn_Get";
            this.Btn_Get.Size = new System.Drawing.Size(165, 48);
            this.Btn_Get.TabIndex = 0;
            this.Btn_Get.Text = "Get";
            this.Btn_Get.Click += new System.EventHandler(this.Btn_Get_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 33);
            this.panel2.TabIndex = 1;
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularDoubleSolid;
            this.ultraGroupBox1.Controls.Add(this.Chk_EasyPayNumber);
            this.ultraGroupBox1.Controls.Add(this.Chk_NoticeNumber);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox1.Location = new System.Drawing.Point(10, 13);
            this.ultraGroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(786, 66);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "Numbering Options";
            // 
            // Chk_EasyPayNumber
            // 
            this.Chk_EasyPayNumber.AutoSize = true;
            this.Chk_EasyPayNumber.Enabled = false;
            this.Chk_EasyPayNumber.Location = new System.Drawing.Point(425, 31);
            this.Chk_EasyPayNumber.Name = "Chk_EasyPayNumber";
            this.Chk_EasyPayNumber.Size = new System.Drawing.Size(130, 20);
            this.Chk_EasyPayNumber.TabIndex = 1;
            this.Chk_EasyPayNumber.Text = "Easy Pay Number";
            // 
            // Chk_NoticeNumber
            // 
            this.Chk_NoticeNumber.AutoSize = true;
            this.Chk_NoticeNumber.Enabled = false;
            this.Chk_NoticeNumber.Location = new System.Drawing.Point(44, 31);
            this.Chk_NoticeNumber.Name = "Chk_NoticeNumber";
            this.Chk_NoticeNumber.Size = new System.Drawing.Size(111, 20);
            this.Chk_NoticeNumber.TabIndex = 0;
            this.Chk_NoticeNumber.Text = "Notice Number";
            // 
            // NumberingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 570);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NumberingForm";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Numbering";
            this.Load += new System.EventHandler(this.NumberingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Log)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_OffenEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_OffenStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_CandiNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_EasyPayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_NoticeNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Panel panel4;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Edt_Log;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Txt_OffenEndDate;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Txt_OffenStartDate;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Txt_CandiNumber;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraButton Btn_Get;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_EasyPayNumber;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_NoticeNumber;
        private Infragistics.Win.Misc.UltraButton Btn_Close;
        private Infragistics.Win.Misc.UltraButton Btn_Do;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar BarProgress;
        private System.Windows.Forms.ListView Lst_History;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
    }
}