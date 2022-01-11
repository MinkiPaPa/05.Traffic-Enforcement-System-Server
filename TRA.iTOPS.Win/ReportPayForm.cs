using TRA.iTOPS.Windows.Set.MainApp;
using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraMessageBox;

//using Infragistics.Documents.Report;
//using Infragistics.Win.UltraWinGrid.DocumentExport;

namespace TRA.iTOPS.Win
{
    public partial class ReportPayForm : FormBase
    {
        public ReportPayForm()
        {
            InitializeComponent();
        }

        #region #EVENT
        /// <summary>
        /// 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ReportPayForm_Load(object sender, EventArgs e)
        {
            Btn_SaveFile.Enabled = false;
            Btn_CopyExcel.Enabled = false;

            SetGridInit();
        }


        private void Btn_GetList_Click(object sender, EventArgs e)
        {
            DataSet ds = SelectReportPay();

            if(ds.Tables[0].Rows.Count > 0)
            {
                Btn_SaveFile.Enabled = true;
                Btn_CopyExcel.Enabled = true;
            }

            Grd_Report.DataSource = ds.Tables[0];
            Grd_Report.DataBind();
            Grd_Report.UpdateData();

        }

        private void Btn_SaveFile_Click(object sender, EventArgs e)
        {
            if (Grd_Report.Rows.Count < 1)
                return;

            //저장 위치 결정.
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.InitialDirectory = System.Environment.CurrentDirectory;
            saveDlg.Filter = "csv (*.csv)|*.csv|txt (*txt)|*.txt|All files (*.*)|*.*";
            if (saveDlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //이어쓰기 모드로 스트림을 생성.
            //한줄씩 입력하기 위해서 이어쓰기(Append) 모드로 한다.
            FileStream fs = new FileStream(saveDlg.FileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            string sline = "\r\n,,Payment Report\r\n\r\n";
            sw.WriteLine(sline);

            
            sline = string.Format("PAY DATE :, {0},to, {1},,total case:, {2}", Convert.ToDateTime(Dt_Pfrom.Value.ToString()).ToString("yyyy-MM-dd"), Convert.ToDateTime(Dt_Pto.Value.ToString()).ToString("yyyy-MM-dd"), Grd_Report.Rows.Count);
            sw.WriteLine(sline);

            if(Chk_OffenceDate.Checked)
            {
                sline = string.Format("OFFENCE DATE :, {0},to, {1},,total case:, {2}", Convert.ToDateTime(Dt_Ofrom.Value.ToString()).ToString("yyyy-MM-dd"), Convert.ToDateTime(Dt_Oto.Value.ToString()).ToString("yyyy-MM-dd"), Grd_Report.Rows.Count);
                sw.WriteLine(sline);
            }else
            {
                sline = string.Format("\r\n");
                sw.WriteLine(sline);
            }

            sline = string.Format("Order,Notice Number,Car Number,Offence Date,Officer,Fine,Pay,Pay date,Receipt Num,Owner");
            sw.WriteLine(sline);

            int TotalPay = 0, TotalFine = 0;
            string strFine = string.Empty;
            for (int i = 0; i < Grd_Report.Rows.Count; i++)
            {
                sline = "";

                foreach (UltraGridCell cell in Grd_Report.Rows[i].Cells)
                {
                    if (!cell.Column.Hidden)
                    {
                         sline += string.Format("{0},", cell.Text);

                        if (cell.Column.Key == "PAYED")
                        {
                            TotalPay += Convert.ToInt32(cell.Text);
                        }

                        if (cell.Column.Key == "FINE_S")
                        {
                            strFine = cell.Text;

                            if(strFine.IndexOf("(") > 0)
                                strFine = strFine.Substring(strFine.IndexOf("(")+1, strFine.IndexOf(")") - strFine.IndexOf("(")-1);

                           TotalFine += Convert.ToInt32(strFine);
                        }

                    }
                }
                sw.WriteLine(sline);
            }

            sline = string.Format("\r\nTotal,,,,,{0},{1}", TotalFine, TotalPay);
            sw.WriteLine(sline);
          
            sw.Close();
            fs.Close();

            //MessageBox.Show(saveDlg.FileName);
            ShowUltraMessage(saveDlg.FileName);
        }


        private void Btn_CopyExcel_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void Chk_OffenceDate_CheckedChanged(object sender, EventArgs e)
        {
            if(Chk_OffenceDate.Checked == true)
            {
                Dt_Ofrom.Enabled = true;
                Dt_Oto.Enabled = true;
            }else
            {
                Dt_Ofrom.Enabled = false;
                Dt_Oto.Enabled = false;
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region #METHOD
        private void SetGridInit()
        {
            Grd_Report.DataSource = null;

            Grd_Report.DisplayLayout.Bands[0].Columns.ClearUnbound();

            //그리드를 완전 초기화 한다.
            Grd_Report.ResetDisplayLayout();
            //그리드의 캡션을 숨긴다.
            Grd_Report.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

            //컬럼 넓이를 자동으로 조정한다
            Grd_Report.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            //그리드 클릭시 Row전체를 선택한게 한다.
            Grd_Report.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            //그리드 헤더와셀의 텍스트를 가운데로 정렬한다.
            Grd_Report.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Grd_Report.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            // 모두 텍스트 스타일로 나오도록(이거안하면, 날짜부분에 달력이 생김)
            Grd_Report.DisplayLayout.Bands[0].Override.CellDisplayStyle = CellDisplayStyle.FormattedText;

            Grd_Report.DisplayLayout.Bands[0].Columns.Add("ROWNUM", "Order");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("NOTICE_NUM", "Notice Number");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("CARNUM", "Car Number");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("WHEN_DT", "Offence Date");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("OFFICER", "Officer");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("FINE_S", "Fine");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("PAYED", "Pay");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("PAY_DUEDT", "Pay Date");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("PAYED_RECEIPT", "Receipt Num");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("N016_P_NAME", "Owner");
            Grd_Report.DisplayLayout.Bands[0].Columns.Add("CUID", "cuid");
            Grd_Report.DisplayLayout.Bands[0].Columns["CUID"].Hidden = true;

        }
        private void Copy()
        {
            StringBuilder strText = new StringBuilder();
            string strRows = string.Empty;

            if (Grd_Report.Rows.Count > 0)
            {
                strRows = "";

                for (int i = 0; i < Grd_Report.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    if (!Grd_Report.DisplayLayout.Bands[0].Columns[i].Hidden)
                    {
                        if (strRows == "")
                            strRows = Grd_Report.DisplayLayout.Bands[0].Columns[i].Header.Caption;
                        else
                            strRows += string.Format("\t{0}", Grd_Report.DisplayLayout.Bands[0].Columns[i].Header.Caption);
                    }
                }

                strText.AppendLine(strRows);

                for (int i = 0; i < Grd_Report.Rows.Count; i++)
                {
                    strRows = "";

                    foreach (UltraGridCell cell in Grd_Report.Rows[i].Cells)
                    {
                        if (!cell.Column.Hidden)
                        {
                            if (strRows == "")
                                strRows = cell.Text;
                            else
                                strRows += string.Format("\t{0}", cell.Text);
                        }
                    }

                    strText.AppendLine(strRows);
                }

                Clipboard.SetText(strText.ToString());

                //MessageBox.Show("Data has been copied to the clipboard.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("Data has been copied to the clipboard.");
            }
            else
            {
                //MessageBox.Show("No data to copy.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowUltraMessage("No data to copy.");
            }
        }
        private DataSet SelectReportPay()
        {
            ReportPay ReportBiz = new ReportPay();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            string PstartDate = string.Empty;
            string PEndDate = string.Empty;

            string OstartDate = string.Empty;
            string OEndDate = string.Empty;


            if (Dt_Pfrom.Value != null)
                PstartDate = Convert.ToDateTime(Dt_Pfrom.Value.ToString()).ToString("yyyy-MM-dd");

            if (Dt_Pto.Value != null)
                PEndDate = Convert.ToDateTime(Dt_Pto.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");

            if (Chk_OffenceDate.Checked == true)
            {
                if (Dt_Ofrom.Value != null)
                    PstartDate = Convert.ToDateTime(Dt_Ofrom.Value.ToString()).ToString("yyyy-MM-dd");

                if (Dt_Oto.Value != null)
                    PEndDate = Convert.ToDateTime(Dt_Oto.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");
            }


            req.Parameters.Add("PS_DT", PstartDate);
            req.Parameters.Add("PE_DT", PEndDate);
            req.Parameters.Add("OS_DT", OstartDate);
            req.Parameters.Add("OE_DT", OEndDate);

            //DB Query
            TTOPSReply reply = ReportBiz.SelectReportCase(req);

            return reply.ResultSet;

        }

        private DialogResult ShowUltraMessage(string message, int opt = 0)
        {
            using (UltraMessageBoxInfo ultraMessageBoxInfo = new UltraMessageBoxInfo())
            {
                // Set the general display style
                ultraMessageBoxInfo.Style = MessageBoxStyle.Default;

                // Set the text for the Text, Caption, Header and Footer
                ultraMessageBoxInfo.Text = message;
                ultraMessageBoxInfo.Caption = this.Text;
                //ultraMessageBoxInfo.Footer = "Continuing without restarting can produce unpredictable behaviors.";
                ultraMessageBoxInfo.Footer = "iTOPS";

                // Specify which buttons should be used and which is the default button
                if (opt == 0)
                    ultraMessageBoxInfo.Buttons = MessageBoxButtons.OK;
                else
                    ultraMessageBoxInfo.Buttons = MessageBoxButtons.YesNo;

                ultraMessageBoxInfo.DefaultButton = MessageBoxDefaultButton.Button1;
                ultraMessageBoxInfo.ShowHelpButton = Infragistics.Win.DefaultableBoolean.False;

                // Display the OS Error Icon
                ultraMessageBoxInfo.Icon = MessageBoxIcon.Information;

                // Disable the default sounds
                ultraMessageBoxInfo.EnableSounds = Infragistics.Win.DefaultableBoolean.True;


                // Show the UltraMessageBox
                return this.ultraMessageBoxManager1.ShowMessageBox(ultraMessageBoxInfo);
            }
        }

        #endregion
    }
}
