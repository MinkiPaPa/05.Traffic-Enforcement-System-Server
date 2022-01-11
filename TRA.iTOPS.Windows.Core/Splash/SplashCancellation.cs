using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRA.TBOS.Windows.Core.Splash
{
    public partial class SplashCancellation : Form, ISplasher
    {
        private Color _backColor = Color.FromArgb(66, 51, 107);
        private PictureBox pictureBox1;
        private Label lblStatus;
        private Infragistics.Win.Misc.UltraButton btnCancel;
        private Point mousePoint;

        public SplashCancellation()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;

            this.Load += SplashCancellation_Load;
            this.MouseMove += SplashCancellation_MouseMove;
            this.MouseDown += SplashCancellation_MouseDown;
            
            btnCancel.Appearance = GetAppearance(false);
            btnCancel.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;

            btnCancel.MouseEnter += BtnCancel_MouseEnter;
            btnCancel.MouseLeave += BtnCancel_MouseLeave;
        }

        private void SplashCancellation_Load(object sender, EventArgs e)
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };

        }

        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.Appearance.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
        }

        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.Appearance.BackColorAlpha = Infragistics.Win.Alpha.Default;
        }

        private Infragistics.Win.Appearance GetAppearance(bool isHotTrack)
        {
            Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
            appearance.BackColor = _backColor;
            if (!isHotTrack)
                appearance.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            else
                appearance.BackColorAlpha = Infragistics.Win.Alpha.Default;
            appearance.BorderColor = Color.White;
            appearance.ForeColor = Color.White;
            appearance.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;

            return appearance;
        }

 
        public event ManualCancelHandler ManualCanceled;

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (ManualCanceled != null)
                ManualCanceled();
        }

        public void SetStatus(string message)
        {
            lblStatus.Text = message;
        }


        #region 폼 이동을 위한 이벤트
        /// <summary>
        /// 폼 이동을 위한 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashCancellation_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X), this.Top - (mousePoint.Y - e.Y));
            }

        }

        private void SplashCancellation_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        #endregion

        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TRA.TBOS.Windows.Core.Properties.Resources.loader;
            this.pictureBox1.Location = new System.Drawing.Point(150, 75);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 15);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStatus.Location = new System.Drawing.Point(4, 106);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(434, 30);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "loading...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCancel
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(51)))), ((int)(((byte)(107)))));
            appearance1.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            appearance1.BorderColor = System.Drawing.Color.White;
            appearance1.FontData.Name = "Microsoft Sans Serif";
            appearance1.FontData.SizeInPoints = 10F;
            appearance1.ForeColor = System.Drawing.Color.White;
            appearance1.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.btnCancel.Appearance = appearance1;
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(51)))), ((int)(((byte)(107)))));
            appearance2.ForeColor = System.Drawing.Color.White;
            appearance2.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.btnCancel.HotTrackAppearance = appearance2;
            this.btnCancel.Location = new System.Drawing.Point(152, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShowOutline = false;
            this.btnCancel.Size = new System.Drawing.Size(128, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // SplashCancellation
            // 
            this.BackgroundImage = global::TRA.TBOS.Windows.Core.Properties.Resources.loadBg;
            this.ClientSize = new System.Drawing.Size(440, 220);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashCancellation";
            this.Load += new System.EventHandler(this.SplashCancellation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

        }

    }
}
