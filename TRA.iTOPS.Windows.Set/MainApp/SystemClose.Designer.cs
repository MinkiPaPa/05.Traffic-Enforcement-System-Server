namespace TRA.iTOPS.Windows.Set.MainApp
{
    partial class SystemClose
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
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            this.btnOK = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this._SystemClose_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._SystemClose_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._SystemClose_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._SystemClose_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraPanel1
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            appearance1.BorderColor = System.Drawing.Color.Gainsboro;
            appearance1.ForeColor = System.Drawing.Color.Black;
            this.ultraPanel1.Appearance = appearance1;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.btnCancel);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnOK);
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraLabel1);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanel1.ForeColor = System.Drawing.Color.White;
            this.ultraPanel1.Location = new System.Drawing.Point(8, 32);
            this.ultraPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(331, 167);
            this.ultraPanel1.TabIndex = 5;
            this.ultraPanel1.UseAppStyling = false;
            // 
            // btnCancel
            // 
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(116)))), ((int)(((byte)(120)))));
            appearance2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(105)))), ((int)(((byte)(108)))));
            appearance2.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Appearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(56)))), ((int)(((byte)(58)))));
            this.btnCancel.HotTrackAppearance = appearance3;
            this.btnCancel.Location = new System.Drawing.Point(169, 92);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "No";
            this.btnCancel.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            appearance4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(116)))), ((int)(((byte)(120)))));
            appearance4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(105)))), ((int)(((byte)(108)))));
            appearance4.ForeColor = System.Drawing.Color.White;
            this.btnOK.Appearance = appearance4;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(56)))), ((int)(((byte)(58)))));
            this.btnOK.HotTrackAppearance = appearance5;
            this.btnOK.Location = new System.Drawing.Point(83, 92);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 32);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Yes";
            this.btnOK.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ultraLabel1
            // 
            appearance6.ForeColor = System.Drawing.Color.White;
            this.ultraLabel1.Appearance = appearance6;
            this.ultraLabel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel1.Location = new System.Drawing.Point(99, 40);
            this.ultraLabel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(142, 21);
            this.ultraLabel1.TabIndex = 1;
            this.ultraLabel1.Text = "Do you want Quit ?";
            this.ultraLabel1.UseAppStyling = false;
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // _SystemClose_UltraFormManager_Dock_Area_Top
            // 
            this._SystemClose_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._SystemClose_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._SystemClose_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._SystemClose_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._SystemClose_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._SystemClose_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._SystemClose_UltraFormManager_Dock_Area_Top.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SystemClose_UltraFormManager_Dock_Area_Top.Name = "_SystemClose_UltraFormManager_Dock_Area_Top";
            this._SystemClose_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(347, 32);
            // 
            // _SystemClose_UltraFormManager_Dock_Area_Bottom
            // 
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 8;
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 199);
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.Name = "_SystemClose_UltraFormManager_Dock_Area_Bottom";
            this._SystemClose_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(347, 8);
            // 
            // _SystemClose_UltraFormManager_Dock_Area_Left
            // 
            this._SystemClose_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._SystemClose_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._SystemClose_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._SystemClose_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._SystemClose_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._SystemClose_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 8;
            this._SystemClose_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 32);
            this._SystemClose_UltraFormManager_Dock_Area_Left.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SystemClose_UltraFormManager_Dock_Area_Left.Name = "_SystemClose_UltraFormManager_Dock_Area_Left";
            this._SystemClose_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(8, 167);
            // 
            // _SystemClose_UltraFormManager_Dock_Area_Right
            // 
            this._SystemClose_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._SystemClose_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._SystemClose_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._SystemClose_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._SystemClose_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._SystemClose_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 8;
            this._SystemClose_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(339, 32);
            this._SystemClose_UltraFormManager_Dock_Area_Right.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SystemClose_UltraFormManager_Dock_Area_Right.Name = "_SystemClose_UltraFormManager_Dock_Area_Right";
            this._SystemClose_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(8, 167);
            // 
            // SystemClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 207);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this._SystemClose_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._SystemClose_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._SystemClose_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._SystemClose_UltraFormManager_Dock_Area_Bottom);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemClose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ITOPS Server Quit";
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraButton btnCancel;
        private Infragistics.Win.Misc.UltraButton btnOK;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _SystemClose_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _SystemClose_UltraFormManager_Dock_Area_Bottom;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _SystemClose_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _SystemClose_UltraFormManager_Dock_Area_Right;
    }
}