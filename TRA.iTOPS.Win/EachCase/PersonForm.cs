using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infragistics.Win.UltraMessageBox;

namespace TRA.iTOPS.Win.EachCase
{
    public partial class PersonForm : Form
    {
        private DataRow _dr = null;

        private bool bUpdated = false;

        public bool UpdateState
        {
            get { return this.bUpdated; }   // 여기서 얻은 값을 다른폼으로 전달목적
            set { this.bUpdated = value; }  // 다른폼에서 전달받은값 쓰기
        }

        public DataRow CaseDr
        {
            get { return _dr; }
            set { _dr = value; }
        }

        public PersonForm()
        {
            InitializeComponent();
        }

        #region #Event
        private void PersonForm_Load(object sender, EventArgs e)
        {
            Txt_bID.Text        = _dr["N015_P_IDNUM"].ToString();
            Txt_bName.Text      = _dr["N016_P_NAME"].ToString();
            Txt_bInitial.Text   = _dr["N017_P_INITIAL"].ToString();

            Txt_xID.Text        = _dr["N053_PX_IDNUM"].ToString();
            Txt_xName.Text      = _dr["N032_PX_NAME"].ToString();
            Txt_xInitial.Text   = _dr["N033_PX_INITIAL"].ToString();
           
            Txt_pAddress1.Text  = _dr["N019_P_ADDR1"].ToString();
            Txt_pAddress2.Text  = _dr["N020_P_ADDR2"].ToString();
            Txt_pAddress3.Text  = _dr["N021_P_ADDR3"].ToString();
            Txt_pAddress4.Text  = _dr["N022_P_ADDR4"].ToString();
            Txt_pAddress5.Text  = _dr["N023_P_ADDR5"].ToString();
            Txt_pCode.Text      = _dr["N024_P_ACODE"].ToString();

            Txt_sAddress1.Text  = _dr["N025_P_STRT1"].ToString();
            Txt_sAddress2.Text  = _dr["N026_P_STRT2"].ToString();
            Txt_sAddress3.Text  = _dr["N027_P_STRT3"].ToString();
            Txt_sAddress4.Text  = _dr["N028_P_STRT4"].ToString();
            
            Txt_sCode.Text      = _dr["N030_P_SCODE"].ToString();

            this.ActiveControl = Btn_Cancel;
            Btn_Cancel.Focus();
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if(UpdatePerson())
            {
                string sprev = string.Format("ID: {0}, NAME: {1}, ADDR: {2},{3},{4},{5},{6},{7}",
                    _dr["N015_P_IDNUM"].ToString(), _dr["N016_P_NAME"].ToString(),
                    _dr["N019_P_ADDR1"].ToString(), _dr["N020_P_ADDR2"].ToString(),
                    _dr["N021_P_ADDR3"].ToString(), _dr["N022_P_ADDR4"].ToString(),
                    _dr["N023_P_ADDR5"].ToString(), _dr["N024_P_ACODE"].ToString());

                string sAfter = string.Format("ID: {0}, NAME: {1}, ADDR: {2},{3},{4},{5},{6},{7}",
                    Txt_bID.Text, Txt_bName.Text,
                    Txt_pAddress1.Text, Txt_pAddress2.Text,
                    Txt_pAddress3.Text, Txt_pAddress4.Text,
                    Txt_pAddress5.Text, Txt_pCode.Text);

                bUpdated = true;
                AddHistoryChange("PERSON", sprev, sAfter);
            }
            
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        #endregion


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

        private bool UpdatePerson()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("N015_P_IDNUM",      Txt_bID.Text      );
            req.Parameters.Add("N016_P_NAME",       Txt_bName.Text    );
            req.Parameters.Add("N017_P_INITIAL",    Txt_bInitial.Text );
            
            req.Parameters.Add("N053_PX_IDNUM",     Txt_xID.Text      );
            req.Parameters.Add("N032_PX_NAME",      Txt_xName.Text    );
            req.Parameters.Add("N033_PX_INITIAL",   Txt_xInitial.Text );
            
            req.Parameters.Add("N019_P_ADDR1",      Txt_pAddress1.Text);
            req.Parameters.Add("N020_P_ADDR2",      Txt_pAddress2.Text);
            req.Parameters.Add("N021_P_ADDR3",      Txt_pAddress3.Text);
            req.Parameters.Add("N022_P_ADDR4",      Txt_pAddress4.Text);
            req.Parameters.Add("N023_P_ADDR5",      Txt_pAddress5.Text);
            req.Parameters.Add("N024_P_ACODE",      Txt_pCode.Text    );
            
            req.Parameters.Add("N025_P_STRT1",      Txt_sAddress1.Text);
            req.Parameters.Add("N026_P_STRT2",      Txt_sAddress2.Text);
            req.Parameters.Add("N027_P_STRT3",      Txt_sAddress3.Text);
            req.Parameters.Add("N028_P_STRT4",      Txt_sAddress4.Text);
            req.Parameters.Add("N030_P_SCODE",      Txt_sCode.Text);
            req.Parameters.Add("CUID", _dr["CUID"]);    

            //DB Query
            TTOPSReply reply = eachBiz.UpdatePerson(req);
            if (reply.ResultCode == "OK")
            {
                //MessageBox.Show("OK - Updated", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("OK - Updated");
                return true;
            }else
            {
                //MessageBox.Show("FAIL to Update Offender information", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowUltraMessage("FAIL to Update Offender information");
                return false;
            }

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
