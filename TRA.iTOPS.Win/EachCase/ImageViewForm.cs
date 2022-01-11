using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRA.iTOPS.Win.EachCase
{
    public partial class ImageViewForm : Form
    {
        private string ImgFile = string.Empty;
        public string CurImgFile
        {
            get { return ImgFile; }
            set { ImgFile = value; }
        }

        //private Point LastPoint;
        //private List<Point> pointList;
        //private List<List<Point>> curveList;
        private Bitmap img;

        private double ratio = 1F;
        private Point imgPoint;
        private Rectangle imgRect;
        private Point clickPoint;


        public ImageViewForm()
        {
            InitializeComponent();
        }

        #region #Event

        private void ImageViewForm_Load(object sender, EventArgs e)
        {
            //pointList = new List<Point>();
            //curveList = new List<List<Point>>();

            this.PictureBox1.MouseWheel
                += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseWheel);


            img = new Bitmap(ImgFile);

            //pictureBox1.Load(img);

            imgPoint = new Point(PictureBox1.Width / 2, PictureBox1.Height / 2);
            imgRect = new Rectangle(0, 0, img.Width, img.Height);
            ratio = 1.0;
            clickPoint = imgPoint;

            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = imgRect.Width - PictureBox1.Width;

            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = imgRect.Height - PictureBox1.Height;


            PictureBox1.Invalidate();
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                ratio *= 1.4F;
                if (ratio > 100.0) ratio = 100.0;

            }
            else if (lines < 0)
            {
                ratio *= 0.7F;
                if (ratio < 1) ratio = 1;
            }


            imgRect.Width = (int)Math.Round(img.Width * ratio);
            imgRect.Height = (int)Math.Round(img.Height * ratio);
            imgRect.X = (int)Math.Round(pb.Width / 2 - imgPoint.X * ratio);
            imgRect.Y = (int)Math.Round(pb.Height / 2 - imgPoint.Y * ratio);


            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = imgRect.Width - pb.Width;
            if (hScrollBar1.Minimum >= hScrollBar1.Maximum)
            {
                hScrollBar1.Value = hScrollBar1.Maximum;
            }
            else
            {
                hScrollBar1.Value = imgRect.X * (-1);
            }

            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = imgRect.Height - pb.Height;
            if (vScrollBar1.Minimum >= vScrollBar1.Maximum)
            {
                vScrollBar1.Value = vScrollBar1.Maximum;
            }
            else
            {
                vScrollBar1.Value = imgRect.Y * (-1);
            }

            PictureBox1.Invalidate();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (img != null)
            {
                e.Graphics.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                e.Graphics.DrawImage(img, imgRect);
                PictureBox1.Focus();
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickPoint = new Point(e.X, e.Y);
            }

            PictureBox1.Invalidate();

        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) //이미지 이동(추가)
            {
                //imgRect.X = imgRect.X + (e.X - clickPoint.X);//이렇게 하면 이미지스크롤 속도가 너무 빠르므로 마우스 움직임에 나누기 5 정도 해준다.
                imgRect.X = imgRect.X + (int)Math.Round((double)(e.X - clickPoint.X) / (double)5);
                //imgRect.X = imgRect.X + (int)Math.Round((double)(e.X - clickPoint.X));
                if (imgRect.X >= 0) imgRect.X = 0;
                if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - PictureBox1.Width)) imgRect.X = -(imgRect.Width - PictureBox1.Width);
                imgRect.Y = imgRect.Y + (int)Math.Round((double)(e.Y - clickPoint.Y) / (double)5);
                //imgRect.Y = imgRect.Y + (int)Math.Round((double)(e.Y - clickPoint.Y));
                if (imgRect.Y >= 0) imgRect.Y = 0;
                if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - PictureBox1.Height)) imgRect.Y = -(imgRect.Height - PictureBox1.Height);

                hScrollBar1.Value = Math.Abs(imgRect.X);
                vScrollBar1.Value = Math.Abs(imgRect.Y);
            }

            PictureBox1.Invalidate();

        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickPoint = new Point(e.X, e.Y);
            }
            PictureBox1.Invalidate();

        }

        private void PictureBox1_Resize(object sender, EventArgs e)
        {
            PictureBox1.Invalidate();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            imgRect.Y = vScrollBar1.Value * (-1);

            PictureBox1.Invalidate();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            imgRect.X = hScrollBar1.Value * (-1);

            PictureBox1.Invalidate();
        }
        #endregion

    }
}
