using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing.Printing;
using Infragistics.Win.UltraWinListView;
using Infragistics.Win.UltraMessageBox;

namespace TRA.iTOPS.Win.EachCase
{
    public partial class PayForm : Form
    {
        private DataRow _dr = null;
        int nPayType = 0;
        int nRelation = 0;

        private bool bUpdated = false;

        public bool UpdateState
        {
            get { return this.bUpdated; }   // 여기서 얻은 값을 다른폼으로 전달목적
            set { this.bUpdated = value; }  // 다른폼에서 전달받은값 쓰기
        }


        [DllImport("TRA.iTOP.MFCDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int IssuePrint_Pay(ref ITOPS_State.CasePrint PayPrint );
        //extern public static int IssuePrint_Pay_text(ref ITOPS_State.CasePrint PayPrint );
        public DataRow CaseDr
        {
            get { return _dr; }
            set { _dr = value; }
        }

 
        public PayForm()
        {
            InitializeComponent();
        }

        #region #Event

        private void PayForm_Load(object sender, EventArgs e)
        {
            // Basic Infomation //////////////////////////
            Txt_OwnerName.Text = _dr["N016_P_NAME"].ToString();
            Txt_OffenceDate.Text = ITOPS_State.ConvertDBDate(_dr["WHEN_DT"].ToString());
            Txt_Fine1.Text = Convert.ToSingle(_dr["FINE"]).ToString("N2");  //  소숫점 2자리만 나오게
            Txt_Fine2.Text = Convert.ToSingle(_dr["FINE2"]).ToString("N2"); ;

            if (Convert.ToInt32(_dr["FINE2"]) == 0)
                Txt_Fine3.Text = Convert.ToSingle(_dr["FINE"]).ToString("N2");
            else
                Txt_Fine3.Text = Convert.ToSingle(_dr["FINE2"]).ToString("N2");

            ShowPayInfo();
            ///////////////////////////////////////////

            Txt_Casher.Text = ITOPS_State.UserID;
            Rb_Cash.Checked = true;
            Rb_rCash.Checked = true;

            string sPayPoint = AppConfig.GetAppConfig("PayPoint");

            // 디폴트값은 1 이다.
            if (sPayPoint == null)
                sPayPoint = "1";

            Txt_PayPoint.Text = sPayPoint;

            if (Enable_editables() == true)
                Set_current_info();

        }
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if (!PayUpdateState())
                return;

            if (Convert.ToInt32(_dr["PAYED"]) == 0)
            {
                UpdatePayinfo(ITOPS_State.ITOPS_CLOSED_PAID);
            }
            else
            {
                Update_pay_amount_only();
            }

            Enable_editables(true);

        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {

            PrintReceipt();
            /*
            //MessageBox.Show("For recipients", "ITOPS Cash Pay", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowUltraMessage("For recipients");

            PrintDocument doc = new PrintDocument();
            //doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            try
            {
                doc.PrinterSettings.PrinterName = "Generic / Text Only";
            }
            catch(Exception ex)
            {
                //MessageBox.Show("PrinterSettings.PrinterName:" + ex.Message.ToString());
                ShowUltraMessage("PrinterSettings.PrinterName:" + ex.Message.ToString());
                return;
            }
            

            // 열전사 프린터는 
            // 프린터 - 기본인쇄 설정 - 용지/품질 - 용지공급 -> "절단 용지"로 설정 
            //doc.PrinterSettings.PaperSources = PaperSourceKind.

            doc.PrintPage += new PrintPageEventHandler(PrintHandler);
            doc.Print();

            //MessageBox.Show("For Payer", "ITOPS Cash Pay", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowUltraMessage("For Payer");

            PrintDocument doc2 = new PrintDocument();
            //doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            doc2.PrinterSettings.PrinterName = "Generic / Text Only";
            doc2.PrintPage += new PrintPageEventHandler(PrintHandler2);
            doc2.Print();
            */
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region #Method

        private bool Enable_editables(bool bUpdate = false)
        {
            bool balready_paid = false;
            if (bUpdate)
                balready_paid = (Convert.ToInt32(Txt_Payed.Value) > 0);
            else
                balready_paid = (Convert.ToInt32(_dr["PAYED"]) > 0);


            Rb_Card.Enabled = !balready_paid;
            Rb_Cash.Enabled = !balready_paid;
            Rb_Cheque.Enabled = !balready_paid;
            Rb_Others.Enabled = !balready_paid;

            Txt_PayPoint.Enabled = !balready_paid;
            Txt_Casher.Enabled = !balready_paid;
            Cal_PayDate.Enabled = !balready_paid;

            Txt_name.Enabled = !balready_paid;
            Txt_Phone.Enabled = !balready_paid;

            Rb_rCard.Enabled = !balready_paid;
            Rb_rCash.Enabled = !balready_paid;
            Rb_rCheque.Enabled = !balready_paid;
            Rb_rOthers.Enabled = !balready_paid;

            Btn_Print.Enabled = balready_paid;

            return balready_paid;
        }

        private void Set_current_info()
        {
            // Pay Infomation
            int m_nPayMoney = Convert.ToInt32(_dr["PAYED"]);
            Txt_Payed.Text = m_nPayMoney.ToString();
            //Txt_Payed.Text = Convert.ToInt32(_dr["PAYED"]);
            
            nPayType = Convert.ToInt32(_dr["PAYED_TYPE"]);

            if (nPayType == 0)
                Rb_Cash.Checked = true;
            else if (nPayType == 1)
                Rb_Cheque.Checked = true;
            else if (nPayType == 2)
                Rb_Card.Checked = true;
            else if (nPayType == 3)
                Rb_Others.Checked = true;

            Txt_PayPoint.Text = _dr["PAY_POINT"].ToString();
            Txt_Casher.Text = _dr["PAY_CASHER"].ToString();

            //Cal_PayDate.Value = Convert.ToDateTime(_dr["PAY_TIME"].ToString());
            Cal_PayDate.Value = _dr["PAY_TIME"];

            // Payer Infomation
            Txt_name.Text = _dr["PAYER_NAME"].ToString();
            Txt_Phone.Text = _dr["PAYER_PHONE"].ToString();

             nRelation = Convert.ToInt32(_dr["PAYER_REL"]);

            if (nRelation == 0)
                Rb_rCash.Checked = true;
            else if (nRelation == 1)
                Rb_rCheque.Checked = true;
            else if (nRelation == 2)
                Rb_rCard.Checked = true;
            else if (nRelation == 3)
                Rb_rOthers.Checked = true;

            // Receipt Num
            Txt_Receipt.Text = _dr["PAYED_RECEIPT"].ToString();

            Txt_Payed.Focus();
        }

        private void ShowPayInfo()
        {
            Lv_BasicInfo.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_BasicInfo.ViewSettingsDetails.FullRowSelect = true;


            UltraListViewItem item;
            item = new UltraListViewItem("Car number", new Object[] { _dr["CARNUM"].ToString() });
            Lv_BasicInfo.Items.Add(item);

            //item = new UltraListViewItem("Ref number", new Object[] { _dr["OLD_REF_1"].ToString() });
            //Lv_PayInfo.Items.Add(item);

            item = new UltraListViewItem("Notice num", new Object[] { _dr["NOTICE_NUM"].ToString() });
            Lv_BasicInfo.Items.Add(item);

            item = new UltraListViewItem("Offence Date", new Object[] { ITOPS_State.ConvertDBDate(_dr["WHEN_DT"].ToString()) });
            Lv_BasicInfo.Items.Add(item);

        }


        private bool Update_pay_amount_only()
        {
            if(Txt_Payed.Text == "")
            {
                Txt_Payed.Focus();
                return false;
            }

            if(Convert.ToInt32(_dr["PAYED"]) == Convert.ToInt32(Txt_Payed.Value))
            {
                ShowUltraMessage("Do Not Change the pay amount");
                return false;
            }
                
            string sconfirm = string.Format("Change the pay amount\n\n"+
                                            "\t{0}  >>>>  {1}\n\nContinue?", Convert.ToInt32(_dr["PAYED"]), Convert.ToInt32(Txt_Payed.Value));

            //if (MessageBox.Show(sconfirm, "ITOPS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            //    return false;
            if(ShowUltraMessage(sconfirm, 1) != DialogResult.Yes)
               return false;

            if (UpDatePay2())
            {
                bUpdated = true;

                string sprev = string.Format("R {0}.00", Convert.ToInt32(_dr["PAYED"]));
                string snext = string.Format("R {0}.00", Convert.ToInt32(Txt_Payed.Value));

                AddHistoryChange("PAYMENT", sprev, snext);
                //MessageBox.Show("Success Change the Pay Amount", "ITOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("Success Change the Pay Amount");

                return true;
            }
            else
            {
                //MessageBox.Show("FAIL Change the Pay Amount", "ITOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("FAIL Change the Pay Amount");
                return false;

            }

        }

        private bool AddHistoryChange(string subject, string before, string after)
        {
            EachCaseBiz ecBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            req.Parameters.Add("CUID", _dr["CUID"]);
            req.Parameters.Add("CARNUM", _dr["CARNUM"]);
            req.Parameters.Add("NOTICE_NUM", _dr["NOTICE_NUM"]);
            req.Parameters.Add("SUBJECT", subject);
            req.Parameters.Add("BEFORE", before);
            req.Parameters.Add("AFTER", after);

            TTOPSReply reply = ecBiz.InsertHistoryChange(req);

            if (reply.ResultCode == "OK")
                return true;
            else
                return false;

        }

        private bool UpdatePayinfo(int nCode1)
        {

            int nfine_real = (Convert.ToInt32(_dr["FINE2"]) == 0 ? Convert.ToInt32(_dr["FINE"]) : Convert.ToInt32(_dr["FINE2"]));
            int nPayMoney = Convert.ToInt32(Txt_Payed.Value);


            if (nPayMoney != nfine_real)
            {
                string sCommnet = string.Format
                    ("Pay Amount is different from fine!\n\n" +
                    "The fine is R {0}, Payment is R {1}\n" +
                    "Will you continue to Pay?", nfine_real, nPayMoney);

                //if (MessageBox.Show(sCommnet, "Pay Of Each Case", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                if(ShowUltraMessage(sCommnet, 1) != DialogResult.Yes)
                {
                    Txt_Payed.Focus();
                    return false;
                }
            }


            DataSet ds = SelectPay_ReceiptNumber();

            if (ds.Tables[0].Rows.Count > 0)
            {
                string sReceiptNum = string.Format("{0:0000}-{1:000}-{2:00000000}-{3:00000}",
                    Convert.ToInt32(ds.Tables[0].Rows[0]["PAYSYSTEM"]),
                    Convert.ToInt32(ds.Tables[0].Rows[0]["PAYPOINT"]),
                    Convert.ToInt32(ds.Tables[0].Rows[0]["PAYDATE"]),
                    Convert.ToInt32(ds.Tables[0].Rows[0]["PAYSEQ"]));

                if (UpDatePay(sReceiptNum, nCode1))
                {

                    int iPaySeq = Convert.ToInt32(ds.Tables[0].Rows[0]["PAYSEQ"]);
                    iPaySeq++;
                    UpdateReceiptNumSeq(iPaySeq);

                    //MessageBox.Show("OK add Pay information", "ITOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowUltraMessage("OK add Pay information");

                    Txt_Receipt.Text = sReceiptNum;

                    bUpdated = true;

                    return true;

                }
                else
                {
                    //MessageBox.Show("FAIL to add Pay information", "ITOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowUltraMessage("FAIL to add Pay information");
                    return false;
                }

            }else
                return false;
        }

        private void UpdateReceiptNumSeq(int iPaySeq)
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("PAYSEQ", iPaySeq);

            //DB Query
            TTOPSReply reply = eachBiz.ReceiptNumSeq(req);

        }

        private bool UpDatePay(string sReceiptNum, int nCode1)
        {

            EachCaseBiz eachBiz = new EachCaseBiz();


            if (Rb_Cash.Checked == true)
                nPayType = 0;
            else if (Rb_Cheque.Checked == true)
                nPayType = 1;
            else if (Rb_Card.Checked == true )
                nPayType = 2;
            else if (Rb_Others.Checked == true )
                nPayType = 3;

            
            if (Rb_rCash.Checked == true)
                nRelation = 0;
            else if (Rb_rCheque.Checked == true)
                nRelation = 1;
            else if (Rb_rCard.Checked == true)
                nRelation = 2;
            else if (Rb_rOthers.Checked == true)
                nRelation = 3;


            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("PAYED", Txt_Payed.Text);
            req.Parameters.Add("PAYED_TYPE", nPayType);
            req.Parameters.Add("PAY_POINT", Txt_PayPoint.Text);
            req.Parameters.Add("PAY_CASHER", Txt_Casher.Text);
            req.Parameters.Add("PAY_TIME", System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
            req.Parameters.Add("PAYER_NAME", Txt_name.Text);
            req.Parameters.Add("PAYER_PHONE", Txt_Phone.Text);
            req.Parameters.Add("PAYER_REL", nRelation);
            req.Parameters.Add("PAYED_RECEIPT", sReceiptNum);
            req.Parameters.Add("CLOSE_CODE1", nCode1);
            req.Parameters.Add("CUID", _dr["CUID"].ToString());


            //DB Query
            TTOPSReply reply = eachBiz.UpdatePay(req);
            if (reply.ResultCode == "OK")
            {
                _dr["PAYED"] = Txt_Payed.Text;
                _dr["PAYED_TYPE"] = nPayType;
                _dr["PAY_POINT"] = Txt_PayPoint.Text;
                _dr["PAY_CASHER"] = Txt_Casher.Text;
                _dr["PAY_TIME"] = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                _dr["PAYER_NAME"] = Txt_name.Text;
                _dr["PAYER_PHONE"] = Txt_Phone.Text;
                _dr["PAYER_REL"] = nRelation;
                _dr["PAYED_RECEIPT"] = sReceiptNum;
                _dr["CLOSE_CODE1"] = nCode1;

                return true;
            }else
                return false;

        }

        private bool UpDatePay2()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("PAYED", Txt_Payed.Text);
            req.Parameters.Add("CUID", _dr["CUID"].ToString());

            //DB Query
            TTOPSReply reply = eachBiz.UpdatePay2(req);
            if (reply.ResultCode == "OK")
            {
                _dr["PAYED"] = Txt_Payed.Text;
                return true;
            }
            else
                return false;

        }

        private DataSet SelectPay_ReceiptNumber()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = eachBiz.SelectPayReceiptNumber(req);

            return reply.ResultSet;

        }

        private bool PayUpdateState()
        {
            if (Txt_PayPoint.Text == "")
            {
                this.ActiveControl = Txt_PayPoint;
                Txt_PayPoint.Focus();
                return false;
            }

            if (Txt_Payed.Text == "")
            {
                this.ActiveControl = Txt_Payed;
                Txt_Payed.Focus();
                return false;
            }

            if (Txt_name.Text == "")
            {
                this.ActiveControl = Txt_name;
                Txt_name.Focus();
                return false;
            }

            return true;

        }



        private void PrintHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            Font FontNormal = new Font("Verdana", 12);
            Graphics e = ppeArgs.Graphics;

            e.DrawString("For recipients", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 10, 125);
            e.DrawString("NEWCASTLE MUNICIPALITY TRAFFIC DEPARTMENT", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 30, 150);
            e.DrawString("Cash Receipt / Kontant Kwitansie ", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 175);
            e.DrawString("Date/Datum:" + ITOPS_State.ConvertDBDate(_dr["PAY_TIME"].ToString()), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 200);
            e.DrawString("Received from : "+ _dr["PAYER_NAME"].ToString() +"(" + _dr["PAYER_PHONE"].ToString() + ")", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 225);
            e.DrawString("Traffic Fine", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 250);
            e.DrawString("Received :" + _dr["PAYED"].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 275);
            e.DrawString("Notice Number : "+ _dr["NOTICE_NUM"].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 300);
            e.DrawString("Receipt Number : "+_dr["PAYED_RECEIPT"].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 325);
            e.DrawString("Signature : _____________________", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 350);
            e.DrawString("--------------------------", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 375);

        }

        private void PrintHandler2(object sender, PrintPageEventArgs ppeArgs)
        {
            Font FontNormal = new Font("Verdana", 12);
            Graphics e = ppeArgs.Graphics;

            e.DrawString("For Payer", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 10, 125);
            e.DrawString("NEWCASTLE MUNICIPALITY TRAFFIC DEPARTMENT", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 30, 150);
            e.DrawString("Cash Receipt / Kontant Kwitansie ", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 175);
            e.DrawString("Date/Datum:" + ITOPS_State.ConvertDBDate(_dr["PAY_TIME"].ToString()), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 200);
            e.DrawString("Received from : " + _dr["PAYER_NAME"].ToString() + "(" + _dr["PAYER_PHONE"].ToString() + ")", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 225);
            e.DrawString("Traffic Fine", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 250);
            e.DrawString("Received :" + _dr["PAYED"].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 275);
            e.DrawString("Notice Number : " + _dr["NOTICE_NUM"].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 300);
            e.DrawString("Receipt Number : " + _dr["PAYED_RECEIPT"].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 325);
            e.DrawString("Signature : _____________________", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 350);
            e.DrawString("--------------------------", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 375);

        }


        private void PrintReceipt()
        {
            ITOPS_State.CasePrint np = new ITOPS_State.CasePrint();

            np.NaP_NAME = _dr["N016_P_NAME"].ToString();
            np.NaP_INITIAL = _dr["N017_P_INITIAL"].ToString();
            np.NaX_NAME = _dr["N032_PX_NAME"].ToString();
            np.NaP_ADDR1 = _dr["N019_P_ADDR1"].ToString();
            np.NaP_ADDR2 = _dr["N020_P_ADDR2"].ToString();
            np.NaP_ADDR3 = _dr["N021_P_ADDR3"].ToString();
            np.NaP_ADDR4 = _dr["N022_P_ADDR4"].ToString();
            np.NaP_ADDR5 = _dr["N023_P_ADDR5"].ToString();
            np.NaP_ACODE = _dr["N032_PX_NAME"].ToString();

            np.k_NoticeNum = _dr["NOTICE_NUM"].ToString();
            np.p9_CarNum = _dr["CARNUM"].ToString();
            np.p2_WhenDT = ITOPS_State.ConvertDBDate(_dr["WHEN_DT"].ToString());

            np.p1_Street = _dr["STREET"].ToString();
            np.p1_Court = _dr["COURT"].ToString();
            np.p1_Location = _dr["LOCATION"].ToString();
            np.p1_Direction = _dr["DIRECTION"].ToString();
            np.p1_SpeedLaw = _dr["SPEED_REGAL"].ToString();
            np.p2_SpeedIs = _dr["SPEED_IS"].ToString();
            np.p3_OffenceCode = _dr["OFFENCE_CODE"].ToString();
            np.p1_Officer = _dr["OFFICER"].ToString();
            np.p2_File1 = _dr["FILE_NAME"].ToString();

            np.k_PayDueDate = ITOPS_State.ConvertDBDate(_dr["PAY_DUEDT"].ToString());
            np.p3_Fine = _dr["FINE"].ToString();
            np.k_Last341 = ITOPS_State.ConvertDBDate(_dr["LAST_341"].ToString());
            np.k_LastNBS = ITOPS_State.ConvertDBDate(_dr["LAST_NBS"].ToString());
            np.k_LastSummon = ITOPS_State.ConvertDBDate(_dr["LAST_SUMMON"].ToString());

            np.k_PayBillNum = _dr["PAYED_RECEIPT"].ToString();
            np.k_PayPoint = _dr["PAY_POINT"].ToString();
            np.k_PayTime = ITOPS_State.ConvertDBDate(_dr["PAY_TIME"].ToString());
            np.k_PayerPhone = _dr["PAYER_PHONE"].ToString();
            np.k_PayerName = _dr["PAYER_NAME"].ToString();
            np.k_PayCasher = _dr["PAY_CASHER"].ToString();

            np.NaP_IDNUM = _dr["N015_P_IDNUM"].ToString();
            np.p1_Device = _dr["DEVICE_SN"].ToString();
            np.p2_Distance = _dr["DISTANCE"].ToString();

            np.k_PayType = Convert.ToInt32(_dr["PAYED_TYPE"]);
            np.k_Payed = Convert.ToInt32(_dr["PAYED"]);
            np.k_Fine2 = Convert.ToInt32(_dr["FINE2"]);

            //IssuePrint_Pay_text(ref np);
            IssuePrint_Pay(ref np);
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
                if(opt == 0)
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
