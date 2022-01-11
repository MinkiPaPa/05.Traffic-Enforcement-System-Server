namespace TRA.iTOPS.Win
{
    partial class ReportPayForm
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton4 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportPayForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Grd_Report = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_CopyExcel = new Infragistics.Win.Misc.UltraButton();
            this.Btn_Close = new Infragistics.Win.Misc.UltraButton();
            this.Btn_SaveFile = new Infragistics.Win.Misc.UltraButton();
            this.Btn_GetList = new Infragistics.Win.Misc.UltraButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.Dt_Oto = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.Dt_Ofrom = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.Dt_Pto = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.Dt_Pfrom = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.Chk_OffenceDate = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.Chk_PayDate = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraMessageBoxManager1 = new Infragistics.Win.UltraMessageBox.UltraMessageBoxManager(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grd_Report)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Oto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Ofrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Pto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Pfrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_OffenceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_PayDate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(8, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.panel1.Size = new System.Drawing.Size(1069, 560);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Grd_Report);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 171);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.panel3.Size = new System.Drawing.Size(1047, 375);
            this.panel3.TabIndex = 7;
            // 
            // Grd_Report
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.Grd_Report.DisplayLayout.Appearance = appearance1;
            this.Grd_Report.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.Grd_Report.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.Grd_Report.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Grd_Report.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.Grd_Report.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Grd_Report.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.Grd_Report.DisplayLayout.MaxColScrollRegions = 1;
            this.Grd_Report.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Grd_Report.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Grd_Report.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.Grd_Report.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.Grd_Report.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.Grd_Report.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.Grd_Report.DisplayLayout.Override.CellAppearance = appearance8;
            this.Grd_Report.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.Grd_Report.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.Grd_Report.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.Grd_Report.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.Grd_Report.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.Grd_Report.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.Grd_Report.DisplayLayout.Override.RowAppearance = appearance11;
            this.Grd_Report.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Grd_Report.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.Grd_Report.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.Grd_Report.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.Grd_Report.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.Grd_Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grd_Report.Location = new System.Drawing.Point(10, 12);
            this.Grd_Report.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Grd_Report.Name = "Grd_Report";
            this.Grd_Report.Size = new System.Drawing.Size(1027, 351);
            this.Grd_Report.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_CopyExcel);
            this.panel2.Controls.Add(this.Btn_Close);
            this.panel2.Controls.Add(this.Btn_SaveFile);
            this.panel2.Controls.Add(this.Btn_GetList);
            this.panel2.Controls.Add(this.ultraGroupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1047, 159);
            this.panel2.TabIndex = 6;
            // 
            // Btn_CopyExcel
            // 
            this.Btn_CopyExcel.Location = new System.Drawing.Point(677, 31);
            this.Btn_CopyExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_CopyExcel.Name = "Btn_CopyExcel";
            this.Btn_CopyExcel.Size = new System.Drawing.Size(103, 52);
            this.Btn_CopyExcel.TabIndex = 9;
            this.Btn_CopyExcel.Text = "Copy to Excel";
            this.Btn_CopyExcel.Click += new System.EventHandler(this.Btn_CopyExcel_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(786, 31);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(103, 52);
            this.Btn_Close.TabIndex = 8;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_SaveFile
            // 
            this.Btn_SaveFile.Location = new System.Drawing.Point(568, 31);
            this.Btn_SaveFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_SaveFile.Name = "Btn_SaveFile";
            this.Btn_SaveFile.Size = new System.Drawing.Size(103, 52);
            this.Btn_SaveFile.TabIndex = 7;
            this.Btn_SaveFile.Text = "Save File";
            this.Btn_SaveFile.Click += new System.EventHandler(this.Btn_SaveFile_Click);
            // 
            // Btn_GetList
            // 
            this.Btn_GetList.Location = new System.Drawing.Point(459, 31);
            this.Btn_GetList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Btn_GetList.Name = "Btn_GetList";
            this.Btn_GetList.Size = new System.Drawing.Size(103, 52);
            this.Btn_GetList.TabIndex = 6;
            this.Btn_GetList.Text = "Get List";
            this.Btn_GetList.Click += new System.EventHandler(this.Btn_GetList_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.ultraLabel2);
            this.ultraGroupBox1.Controls.Add(this.Dt_Oto);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel1);
            this.ultraGroupBox1.Controls.Add(this.Dt_Ofrom);
            this.ultraGroupBox1.Controls.Add(this.Dt_Pto);
            this.ultraGroupBox1.Controls.Add(this.Dt_Pfrom);
            this.ultraGroupBox1.Controls.Add(this.Chk_OffenceDate);
            this.ultraGroupBox1.Controls.Add(this.Chk_PayDate);
            this.ultraGroupBox1.Location = new System.Drawing.Point(19, 19);
            this.ultraGroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(405, 126);
            this.ultraGroupBox1.TabIndex = 5;
            this.ultraGroupBox1.Text = "Fetch Condition";
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.AutoSize = true;
            this.ultraLabel2.Location = new System.Drawing.Point(240, 79);
            this.ultraLabel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(12, 15);
            this.ultraLabel2.TabIndex = 2;
            this.ultraLabel2.Text = "~";
            // 
            // Dt_Oto
            // 
            this.Dt_Oto.DateButtons.Add(dateButton1);
            this.Dt_Oto.Enabled = false;
            this.Dt_Oto.Location = new System.Drawing.Point(264, 75);
            this.Dt_Oto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Dt_Oto.Name = "Dt_Oto";
            this.Dt_Oto.NonAutoSizeHeight = 21;
            this.Dt_Oto.Size = new System.Drawing.Size(102, 21);
            this.Dt_Oto.TabIndex = 5;
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(240, 36);
            this.ultraLabel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(12, 15);
            this.ultraLabel1.TabIndex = 1;
            this.ultraLabel1.Text = "~";
            // 
            // Dt_Ofrom
            // 
            this.Dt_Ofrom.DateButtons.Add(dateButton2);
            this.Dt_Ofrom.Enabled = false;
            this.Dt_Ofrom.Location = new System.Drawing.Point(129, 75);
            this.Dt_Ofrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Dt_Ofrom.Name = "Dt_Ofrom";
            this.Dt_Ofrom.NonAutoSizeHeight = 21;
            this.Dt_Ofrom.Size = new System.Drawing.Size(102, 21);
            this.Dt_Ofrom.TabIndex = 4;
            // 
            // Dt_Pto
            // 
            this.Dt_Pto.DateButtons.Add(dateButton3);
            this.Dt_Pto.Location = new System.Drawing.Point(264, 30);
            this.Dt_Pto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Dt_Pto.Name = "Dt_Pto";
            this.Dt_Pto.NonAutoSizeHeight = 21;
            this.Dt_Pto.Size = new System.Drawing.Size(102, 21);
            this.Dt_Pto.TabIndex = 3;
            // 
            // Dt_Pfrom
            // 
            this.Dt_Pfrom.DateButtons.Add(dateButton4);
            this.Dt_Pfrom.Location = new System.Drawing.Point(129, 30);
            this.Dt_Pfrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Dt_Pfrom.Name = "Dt_Pfrom";
            this.Dt_Pfrom.NonAutoSizeHeight = 21;
            this.Dt_Pfrom.Size = new System.Drawing.Size(102, 21);
            this.Dt_Pfrom.TabIndex = 2;
            // 
            // Chk_OffenceDate
            // 
            this.Chk_OffenceDate.AutoSize = true;
            this.Chk_OffenceDate.Location = new System.Drawing.Point(15, 79);
            this.Chk_OffenceDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Chk_OffenceDate.Name = "Chk_OffenceDate";
            this.Chk_OffenceDate.Size = new System.Drawing.Size(99, 18);
            this.Chk_OffenceDate.TabIndex = 1;
            this.Chk_OffenceDate.Text = "Offence Date";
            this.Chk_OffenceDate.CheckedChanged += new System.EventHandler(this.Chk_OffenceDate_CheckedChanged);
            // 
            // Chk_PayDate
            // 
            this.Chk_PayDate.AutoSize = true;
            this.Chk_PayDate.Checked = true;
            this.Chk_PayDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_PayDate.Enabled = false;
            this.Chk_PayDate.Location = new System.Drawing.Point(15, 35);
            this.Chk_PayDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Chk_PayDate.Name = "Chk_PayDate";
            this.Chk_PayDate.Size = new System.Drawing.Size(102, 18);
            this.Chk_PayDate.TabIndex = 0;
            this.Chk_PayDate.Text = "Payment Date";
            // 
            // ultraMessageBoxManager1
            // 
            this.ultraMessageBoxManager1.ContainingControl = this;
            // 
            // ReportPayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 600);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReportPayForm";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report-Pay";
            this.Load += new System.EventHandler(this.ReportPayForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grd_Report)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Oto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Ofrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Pto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dt_Pfrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_OffenceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_PayDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.UltraWinGrid.UltraGrid Grd_Report;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.Misc.UltraButton Btn_CopyExcel;
        private Infragistics.Win.Misc.UltraButton Btn_Close;
        private Infragistics.Win.Misc.UltraButton Btn_SaveFile;
        private Infragistics.Win.Misc.UltraButton Btn_GetList;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo Dt_Oto;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo Dt_Ofrom;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo Dt_Pto;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo Dt_Pfrom;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_OffenceDate;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor Chk_PayDate;
        private Infragistics.Win.UltraMessageBox.UltraMessageBoxManager ultraMessageBoxManager1;
    }
}