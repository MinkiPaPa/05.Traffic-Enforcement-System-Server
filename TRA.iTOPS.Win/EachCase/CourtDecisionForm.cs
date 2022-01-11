using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win.UltraWinListView;
using Infragistics.Win.UltraMessageBox;

namespace TRA.iTOPS.Win.EachCase
{
    public partial class CourtDecisionForm : Form
    {
        private DataRow _dr = null;
        private bool bUpdated = false;

        public bool UpdateState
        {
            get { return this.bUpdated;}   // 여기서 얻은 값을 다른폼으로 전달목적
            set { this.bUpdated = value;}  // 다른폼에서 전달받은값 쓰기
        }

        public DataRow CaseDr
        {
            get { return _dr; }
            set { _dr = value; }
        }

        public CourtDecisionForm()
        {
            InitializeComponent();
        }

        #region #EVENT
        private void CourtDecisionForm_Load(object sender, EventArgs e)
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

            Cmb_Decision.Items.Add(001, "001 SPOT FINE PAID");
            Cmb_Decision.Items.Add(002, "002 SPOT FINE PAID AS REDUCED BY STATE PROSECUTOR");
            Cmb_Decision.Items.Add(003, "003 SPOT FINE PAID AS REDUCED BY OTHER OFFICER");
            Cmb_Decision.Items.Add(004, "004 SPOT FINE PAID - CERTAIN PORTION TO BE REFUNDED");
            Cmb_Decision.Items.Add(005, "005 A.G. PAID");
            Cmb_Decision.Items.Add(006, "006 A.G PAID AS ALTERED BY THE STATE PROSECUTOR");
            Cmb_Decision.Items.Add(008, "008 ACCUSED FOUND GUILTY");
            Cmb_Decision.Items.Add(009, "009 ACCUSED FOUND NOT GUILT");
            Cmb_Decision.Items.Add(010, "010 NOLLE PROSEQUI");
            Cmb_Decision.Items.Add(011, "011 CEASE PROSECUTION - ACCUSED UNDERTERMINABLE");
            Cmb_Decision.Items.Add(015, "015 REMOVED FROM ROLL - TO RE-SUMMONS");
            Cmb_Decision.Items.Add(016, "016 CASE NOT ENROLLED - TO RE-SUMMONS");
            Cmb_Decision.Items.Add(017, "017 CASE ADJOURNED");
            Cmb_Decision.Items.Add(019, "019 PAYMENT OF A.G. SET ASIDE - REFUND AND RE-SUMMONS");
            Cmb_Decision.Items.Add(020, "020 CEASE PROSECUTION - POSSIBLE FORCING");
            Cmb_Decision.Items.Add(021, "021 CEASE PROSECUTION - FIRST NOTICE TOO LATE");
            Cmb_Decision.Items.Add(022, "022 REMOVED FROM ROLL");
            Cmb_Decision.Items.Add(045, "045 SECOND WARRANT OF ARREST ISSUED");
            Cmb_Decision.Items.Add(047, "047 PROCESS CANCELLED - ADMINISTRATIVE ERROR");
            Cmb_Decision.Items.Add(048, "048 DOCUMENT DESTROYED OR LOST");
            Cmb_Decision.Items.Add(049, "049 DOCUMENT CANCELLED");
            Cmb_Decision.Items.Add(050, "050 DOCUMENT PRESENTED - CONTINUE WITH ALTERNATIVE CHARGE");
            Cmb_Decision.Items.Add(051, "051 PROSECUTION IMPOSSIBLE - OFFICER ERROR");

            Cmb_Decision.SelectedIndex = -1;
            //Cmb_Decision.ReadOnly = true;

            Txt_FineBefore.Text = _dr["FINE"].ToString();
            Txt_FineBefore.Enabled = false;

            if (Convert.ToInt32(_dr["CLOSE_CODE2"]) > 0)
            {
                //Cmb_Decision.SelectedItem.DataValue = Convert.ToInt32(_dr["CLOSE_CODE2"]);
                Cmb_Decision.SelectedIndex = Convert.ToInt32(_dr["CLOSE_CODE2"]);
            }


        }

        private void Btn_UpdateDecision_Click(object sender, EventArgs e)
        {
            if(Cmb_Decision.SelectedIndex == -1)
            {
                ShowUltraMessage("Select Choose a Decision");
                return;
            }
                

            if (UpDateStatusClose())
                //MessageBox.Show("OK Update Court information", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("OK Update Court information");
            else
                //MessageBox.Show("Fail to Update Court information", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("Fail to Update Court information");
        }

        private void Btn_UpdateFine_Click(object sender, EventArgs e)
        {
            if (Txt_FineAfter.Text == "0" || Txt_FineAfter.Text == "")
            {
                this.ActiveControl = Txt_FineAfter;
                Txt_FineAfter.Focus();
                return;
            }


            if (UpDateFine())
            {
                string sprev = string.Format("The fine was {0}", Convert.ToInt32(Txt_FineBefore.Value));
                string snext = string.Format("The fine is {0}", Convert.ToInt32(Txt_FineAfter.Value));

                AddHistoryChange("FINE", sprev, snext);

                bUpdated = true;

                _dr["FINE"] = Txt_FineAfter.Value;

                //MessageBox.Show("OK - Updated", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("OK - Updated");

            }
            else
                //MessageBox.Show("FAIL to Update Fine Adjustment", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("FAIL to Update Fine Adjustment");

        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region #METHOD

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


        private bool UpDateStatusClose()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CODE_STATUS", ITOPS_State.ITOPS_STATE_CLOSED);
            req.Parameters.Add("CLOSE_CODE1", ITOPS_State.ITOPS_CLOSED_COURT);
            //req.Parameters.Add("CLOSE_CODE2", Cmb_Decision.SelectedItem.DataValue);
            req.Parameters.Add("CLOSE_CODE2", Cmb_Decision.SelectedIndex);
            req.Parameters.Add("CUID", _dr["CUID"]);

            //DB Query
            TTOPSReply reply = eachBiz.UpdateStatusClose(req);
            if (reply.ResultCode == "OK")
            {
                bUpdated = true;

                _dr["CODE_STATUS"] = ITOPS_State.ITOPS_STATE_CLOSED;
                _dr["CLOSE_CODE1"] = ITOPS_State.ITOPS_CLOSED_COURT;
                _dr["CLOSE_CODE2"] = Cmb_Decision.SelectedIndex;

                return true;
            }
            else
                return false;

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

        private bool UpDateFine()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("FINE", Txt_FineAfter.Value);
            req.Parameters.Add("CUID", _dr["CUID"]);

            //DB Query
            TTOPSReply reply = eachBiz.UpdateFine(req);
            if (reply.ResultCode == "OK")
                return true;
            else
                return false;

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
