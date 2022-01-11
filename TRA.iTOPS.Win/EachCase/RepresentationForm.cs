using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;
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
using System.Runtime.InteropServices;
using Infragistics.Win.UltraWinListView;
using Infragistics.Win.UltraMessageBox;

namespace TRA.iTOPS.Win.EachCase
{
    public partial class RepresentationForm : Form
    {
        private DataRow _dr = null;

        private DataSet dsRep = null;

        //private string sRefer = string.Empty;
        int m_nWhichRepresent = -1;
        private bool bPersonUpdated = false;
        private bool bRepresentUpdated = false;

        public bool PersonUpdateState
        {
            get { return this.bPersonUpdated; }   // 여기서 얻은 값을 다른폼으로 전달목적
            set { this.bPersonUpdated = value; }  // 다른폼에서 전달받은값 쓰기
        }

        public bool RepresentUpdateState
        {
            get { return this.bRepresentUpdated; }   // 여기서 얻은 값을 다른폼으로 전달목적
            set { this.bRepresentUpdated = value; }  // 다른폼에서 전달받은값 쓰기
        }

        string[] RepresentDesc =
        {
            "reduce fine into",
            "cancel the case",
            "refuse represent",
            "change owner info"
        };

        string[] RepresentMethod =
        {
            "visit",
            "email",
            "phone",
            "others"
        };

        public struct ReceiptPrint
        {
            public long n64CUID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string p9_CarNum;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_NoticeNum;

            public long n6_Ref64;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string r_pDecisionWho;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string r_pDecisionDT;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string r_pDecisionWhere;
            public int r_nDecisionIs;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string r_pDecisionDocNum;

            public int r_nFineReduced;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string r_pIssuerWho;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string r_pIssuerPhone;

            public int r_nIssuerMethod;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string r_pIssuerClaim;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string r_pCapturer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string r_pDateTime;
        }


        public DataRow CaseDr
        {
            get { return _dr; }
            set { _dr = value; }
        }

        [DllImport("TRA.iTOP.MFCDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int IssuePrint_RePresentation(ref ITOPS_State.CasePrint PayPrint, ref ReceiptPrint receiptPrint);


        public RepresentationForm()
        {
            InitializeComponent();
        }


        private void RepresentationForm_Load(object sender, EventArgs e)
        {
            Rb_visit.Checked = true;
            Rb_Reduce.Checked = true;
            this.ActiveControl = Txt_ReduceFine;
            Txt_ReduceFine.Focus();

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
            //////////////////////////////////////////////
            Lv_Representation.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_Representation.ViewSettingsDetails.FullRowSelect = true;
            Lbl_DecisionDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            Get_defaults();
            Get_history();

            Btn_Print.Enabled = false;
            Btn_Change.Enabled = false;
        }


        private void Btn_Print_Click(object sender, EventArgs e)
        {
            if (m_nWhichRepresent < 0)
                return;


            DataRow drRep_temp = null;
            if (dsRep.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drRep in dsRep.Tables[0].Rows)
                {
                    if(m_nWhichRepresent == Convert.ToInt64(drRep["HRID"]) )
                    {
                        drRep_temp = drRep;
                        break;
                    }
                        
                }
            }

            if (drRep_temp != null)
                PrintRePresentation(drRep_temp);
                //ReportRePresentation(drRep_temp);
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            int nMethod = -1, nDecisionIs = -1;

            if (Rb_visit.Checked)
                nMethod = 0;
            else if (Rb_Email.Checked)
                nMethod = 1;
            else if (Rb_Phone.Checked)
                nMethod = 2;
            else if (Rb_Others.Checked)
                nMethod = 3;


            if (Rb_Reduce.Checked)
                 nDecisionIs = 0;
            else if (Rb_Cancel.Checked)
            {
                nDecisionIs = 1;
                Txt_ReduceFine.Text = "";
            }
            else if (Rb_Refuse.Checked)
            {
                nDecisionIs = 2;
                Txt_ReduceFine.Text = "";
            }            
            else if (Rb_Change.Checked)
            {
                nDecisionIs = 3;
                Txt_ReduceFine.Text = "";
            }              


            if (Insert_represent(nMethod, nDecisionIs))
            {
                if (nMethod == 0)
                {
                    UpDateFine2();
                }
                else if(nMethod == 1)
                {
                    UpDateStatusClose();
                }

                Set_defaults();
                Get_defaults();
                Get_history();
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void Get_history()
        {
            Lv_Representation.Items.Clear();

             dsRep = SelectRepresentHistoryInfo();

            if (dsRep.Tables[0].Rows.Count > 0)
            {
                UltraListViewItem item;
                string sAction = string.Empty;
                string sMethod = string.Empty;

                foreach (DataRow dr in dsRep.Tables[0].Rows)
                {
                    //sRefer = dr["OLD_REF_1"].ToString();

                    if(Convert.ToInt32(dr["DECISION_IS"]) == 0)
                        sAction = string.Format("{0} ({1})", RepresentDesc[Convert.ToInt32(dr["DECISION_IS"])], Convert.ToInt32(dr["FINE_REDUCED"]));
                    else
                        sAction = string.Format("{0}", RepresentDesc[Convert.ToInt32(dr["DECISION_IS"])]);

                        sMethod = RepresentMethod[Convert.ToInt32(dr["ISSUER_METHOD"])];

                    item = new UltraListViewItem(ConvertDBDay(dr["DECISION_WHEN"].ToString()), new Object[] { sAction, dr["ISSUER_NAME"].ToString(), sMethod, dr["ISSUER_CLAIM"].ToString(), dr["CAPTURER"].ToString() });
                    item.Key = dr["HRID"].ToString(); // HRID 를 키로 추가
                   
                    Lv_Representation.Items.Add(item);

                }

            }

        }

        private string ConvertDBDay(string OrgDate)
        {
            if (OrgDate == null || OrgDate == "")
                return "";

            DateTime dt = new DateTime();
            DateTime.TryParse(OrgDate, null, System.Globalization.DateTimeStyles.AssumeLocal, out dt);
            string ConDt = dt.ToString("yyyy-MM-dd");

            return ConDt;
        }


        private DataSet SelectRepresentHistoryInfo()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CUID", _dr["CUID"]);

            //DB Query
            TTOPSReply reply = eachBiz.SelectHistoryRepresent(req);

            return reply.ResultSet;
        }

        private bool UpDateFine2()
        {
            if (Txt_ReduceFine.Text == "")
            {
                Txt_ReduceFine.Focus();
                return false;
            }
                

            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("FINE2", Txt_ReduceFine.Value);
            req.Parameters.Add("CUID", _dr["CUID"]);

            //DB Query
            TTOPSReply reply = eachBiz.UpdateFine2(req);
            if (reply.ResultCode == "OK")
            {
                bRepresentUpdated = true;
                _dr["FINE2"] = Txt_ReduceFine.Value;
                return true;
            }
            else
                return false;

        }

        private bool UpDateStatusClose()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CODE_STATUS", ITOPS_State.ITOPS_STATE_CLOSED);
            req.Parameters.Add("CLOSE_CODE1", ITOPS_State.ITOPS_CLOSED_CANCEL);
            req.Parameters.Add("CLOSE_CODE2", 0);
            req.Parameters.Add("CUID", _dr["CUID"]);

            //DB Query
            TTOPSReply reply = eachBiz.UpdateStatusClose(req);
            if (reply.ResultCode == "OK")
            {
                bRepresentUpdated = true;

                _dr["CODE_STATUS"] = ITOPS_State.ITOPS_STATE_CLOSED;
                _dr["CLOSE_CODE1"] = ITOPS_State.ITOPS_CLOSED_CANCEL;
                _dr["CLOSE_CODE2"] = 0;

                return true;
            }else
                return false;

        }


        private bool Insert_represent(int nMethod, int nDecisionIs)
        {

            if(Txt_Dmaker.Text == "")
            {
                Txt_Dmaker.Focus();
                return false;
            }

            if (Txt_Dplace.Text == "")
            {
                Txt_Dplace.Focus();
                return false;
            }


            if(Rb_Reduce.Checked)
            {

                if (Txt_ReduceFine.Text == "")
                {
                    Txt_ReduceFine.Focus();
                    return false;
                }else if(Convert.ToInt32(Txt_ReduceFine.Value) > Convert.ToInt32(_dr["FINE"]))
                {
                    //MessageBox.Show("reduced fine is greater than original fine", "ITOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowUltraMessage("reduced fine is greater than original fine");
                    return false;
                }
            }
                

            if (Txt_IssuerName.Text == "")
            {
                Txt_IssuerName.Focus();
                return false;
            }

            if(Txt_Insist.Text == "")
            {
                Txt_Insist.Focus();
                return false;
            }


            if(Txt_Capturer.Text == "")
            {
                Txt_Capturer.Focus();
                return false;
            }

            if(AddRepresent(nMethod, nDecisionIs))
            {
                //MessageBox.Show("OK add new representation", "ITOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("OK add new representation");
                return true;
            }
            else
            {
                //MessageBox.Show("FAIL to add new representation", "ITOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("FAIL to add new representation");
                return false;
            }
        }


        private bool AddRepresent(int nMethod, int nDecisionIs)
        {

            EachCaseBiz ecBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            req.Parameters.Add("CUID", _dr["CUID"]);
            req.Parameters.Add("CARNUM", _dr["CARNUM"]);
            req.Parameters.Add("NOTICE_NUM", _dr["NOTICE_NUM"]);
            req.Parameters.Add("OLD_REF_1", 0);
            req.Parameters.Add("DECISION_WHO", Txt_Dmaker.Text);
            req.Parameters.Add("DECISION_WHEN", Lbl_DecisionDate.Text);
            req.Parameters.Add("DECISION_WHERE", Txt_Dplace.Text);
            req.Parameters.Add("DECISION_IS", nDecisionIs);
            req.Parameters.Add("DECISION_DOCNUM", Txt_DDocNum.Text);
            req.Parameters.Add("ISSUER_NAME", Txt_IssuerName.Text);
            req.Parameters.Add("ISSUER_PHONE", Txt_Phone.Text);
            req.Parameters.Add("ISSUER_METHOD", nMethod);
            req.Parameters.Add("ISSUER_CLAIM", Txt_Insist.Text);
            req.Parameters.Add("CAPTURER", Txt_Capturer.Text);
            req.Parameters.Add("FINE_REDUCED", Txt_ReduceFine.Text);


            TTOPSReply reply = ecBiz.InsertHistoryRepresent(req);

            if (reply.ResultCode == "OK")
                return true;
            else
                return false;

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

        private void Get_defaults()
        {

            string sDecision = AppConfig.GetAppConfig("DECISION_MAKER");

            if (sDecision == null) sDecision = "";
            Txt_Dmaker.Text = sDecision;

            sDecision = AppConfig.GetAppConfig("DECISION_PLACE");

            if (sDecision == null) sDecision = "";
            Txt_Dplace.Text = sDecision;

            Txt_Capturer.Text = ITOPS_State.UserID;

        }

        private void Set_defaults()
        {
            AppConfig.SetAppConfig("DECISION_MAKER", Txt_Dmaker.Text);
            AppConfig.SetAppConfig("DECISION_PLACE", Txt_Dplace.Text);

            //WritePrivateProfileString("REPRESENTATION", "DECISION_MAKER", Txt_Dmaker.Text, Directory.GetCurrentDirectory() + "\\iTOPS.ini");
            //WritePrivateProfileString("REPRESENTATION", "DECISION_PLACE", Txt_Dplace.Text, Directory.GetCurrentDirectory() + "\\iTOPS.ini");

        }

        private void Btn_Change_Click(object sender, EventArgs e)
        {
            PersonForm frm = new PersonForm
            {
                CaseDr = _dr
            };

            frm.ShowDialog();

            if (frm.UpdateState == true)
                bPersonUpdated = true;
            else
                bPersonUpdated = false;

        }

        private void ReportRePresentation(DataRow drRep)
        {

        }

        private void PrintRePresentation(DataRow drRep)
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


            ReceiptPrint rp = new ReceiptPrint();
            rp.n64CUID = Convert.ToInt64(drRep["CUID"]);
            rp.p9_CarNum = drRep["CARNUM"].ToString();
            rp.k_NoticeNum = drRep["NOTICE_NUM"].ToString();
            rp.n6_Ref64 = Convert.ToInt64(drRep["OLD_REF_1"]);

            rp.r_pDecisionWho = drRep["DECISION_WHO"].ToString();
            rp.r_pDecisionDT = ConvertDBDay(drRep["DECISION_WHEN"].ToString());
            rp.r_pDecisionWhere = drRep["DECISION_WHERE"].ToString();
            rp.r_nDecisionIs = Convert.ToInt16(drRep["DECISION_IS"]);
            rp.r_pDecisionDocNum = drRep["DECISION_DOCNUM"].ToString();
            rp.r_nFineReduced = Convert.ToInt32(drRep["FINE_REDUCED"]);
            rp.r_pIssuerWho = drRep["ISSUER_NAME"].ToString();
            rp.r_pIssuerPhone = drRep["ISSUER_PHONE"].ToString();
            rp.r_nIssuerMethod = Convert.ToInt16(drRep["ISSUER_METHOD"]);
            rp.r_pIssuerClaim = drRep["ISSUER_CLAIM"].ToString();
            rp.r_pCapturer = drRep["CAPTURER"].ToString();
            rp.r_pDateTime = ITOPS_State.ConvertDBDate(drRep["CTIME"].ToString());

            IssuePrint_RePresentation(ref np, ref rp);
        }

        private void Lv_Representation_ItemDoubleClick(object sender, ItemDoubleClickEventArgs e)
        {
                UltraListViewItem li = Lv_Representation.SelectedItems[0];

                UltraListViewItem item = new UltraListViewItem(li.Text, new Object[] { li.SubItems[0].Text });
                item.Key = li.Key;

                m_nWhichRepresent = Convert.ToInt32(item.Key);

                Btn_Print.Enabled = true;

        }

        private void Rb_Change_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Change.Checked == true)
                Btn_Change.Enabled = true;
            else
                Btn_Change.Enabled = false;
        }

        private void Rb_Reduce_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Reduce.Checked == true)
            {
                Txt_ReduceFine.Enabled = true;
                //this.ActiveControl = Txt_ReduceFine;
                Txt_ReduceFine.Focus();


            }
            else
                Txt_ReduceFine.Enabled = false;
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
    }
}
