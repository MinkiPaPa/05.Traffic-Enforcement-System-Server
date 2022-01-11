namespace TRA.iTOPS.Win.DataPrint
{
    partial class PrintSplashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintSplashForm));
            this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.ultraActivityIndicator1 = new Infragistics.Win.UltraActivityIndicator.UltraActivityIndicator();
            this.SuspendLayout();
            // 
            // ultraPictureBox1
            // 
            this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
            this.ultraPictureBox1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded4;
            this.ultraPictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraPictureBox1.Image = ((object)(resources.GetObject("ultraPictureBox1.Image")));
            this.ultraPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraPictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraPictureBox1.Name = "ultraPictureBox1";
            this.ultraPictureBox1.Size = new System.Drawing.Size(375, 146);
            this.ultraPictureBox1.TabIndex = 0;
            // 
            // ultraActivityIndicator1
            // 
            this.ultraActivityIndicator1.AnimationEnabled = true;
            this.ultraActivityIndicator1.AnimationEnabledText = "Downloading Car Images..";
            this.ultraActivityIndicator1.AnimationSpeed = 50;
            this.ultraActivityIndicator1.BorderStyle = Infragistics.Win.UIElementBorderStyle.TwoColor;
            this.ultraActivityIndicator1.CausesValidation = true;
            this.ultraActivityIndicator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraActivityIndicator1.Location = new System.Drawing.Point(0, 146);
            this.ultraActivityIndicator1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ultraActivityIndicator1.Name = "ultraActivityIndicator1";
            this.ultraActivityIndicator1.Size = new System.Drawing.Size(375, 49);
            this.ultraActivityIndicator1.TabIndex = 4;
            this.ultraActivityIndicator1.TabStop = true;
            this.ultraActivityIndicator1.Text = "Prepare Downloading";
            // 
            // PrintSplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 200);
            this.Controls.Add(this.ultraActivityIndicator1);
            this.Controls.Add(this.ultraPictureBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PrintSplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintSplashForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraPictureBox ultraPictureBox1;
        private Infragistics.Win.UltraActivityIndicator.UltraActivityIndicator ultraActivityIndicator1;
    }
}