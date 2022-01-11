using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Session;
using TRA.iTOPS.Contracts.Common;
using TRA.iTOPS.Biz;
using System;
using System.Data;
using System.Windows.Forms;
using Infragistics.Win.UltraMessageBox;

namespace TRA.iTOPS.Windows.Set.MainApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            string sLoginID = string.Empty;
            sLoginID = AppConfig.GetAppConfig("LoginID");

            if(sLoginID == null)
                sLoginID = "";

            if(sLoginID.Length > 0)
            {
                txtUserName.Text = sLoginID;

                this.ActiveControl = txtPassword;
                txtPassword.Focus();
            }

        }


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                //MessageBox.Show("Please enter your user ID", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowUltraMessage("Please enter your user ID");
                return;
            }
            else if (txtPassword.Text.Trim() == "")
            {
                //MessageBox.Show("Please enter a password", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowUltraMessage("Please enter a password");
                return;
            }

            //로그인
            if (UserLogin())
            {
                //로그인 성공 시 로그인 실패 횟수를 초기화 한다.
                InitFailCount();

                //로그인 성공 시 로그인 정보를 app.config에 저장한다.

                AppConfig.SetAppConfig("LoginID", txtPassword.Text);

                ITOPS_State.UserID = txtUserName.Text;
                //MDIMainForm mdi = new MDIMainForm();
                //MDIMainForm mdi = new MDIMainForm(); //MDIMainFrom2 로 설정
                MainForm sdi = new MainForm(); //MDIMainFrom2 로 설정
                // MainForm mdi = new MainForm();
                //mdi.FormClosed += (s1, e1) =>
                //{
                //    Application.Exit();
                //};
                //mdi.Show();
                sdi.LoginForm = this;

                txtPassword.Text = "";

                sdi.Show();

                this.Hide();
            }
            else
            {
                //로그인 실패시 로그인 실패 카운트 업데이트
                UserLoginFail();
            }

        }
       
        private bool UserLogin()
        {
            
            //입력받은 ID로 유저정보를 조회한다.
            DataSet ds = SelectUserInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //로그인실패 횟수를 체크하여 5회이상 로그인 실패한경우 잠긴계정으로 취급하며 로그인할 수 없다.
                int intLoginCount = Convert.ToInt32(ds.Tables[0].Rows[0]["FAIL_CNT"]);

                if (intLoginCount > 4)
                {
                    //MessageBox.Show("Your account has been locked because your login failed more than 5 times. \r\nPlease contact the administrator.", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowUltraMessage("Your account has been locked because your login failed more than 5 times. \r\nPlease contact the administrator.");
                    return false;
                }

                //비밀번호체크
                if (txtPassword.Text != ds.Tables[0].Rows[0]["USER_PW"].ToString())
                {
                    //MessageBox.Show(string.Format("Missing user ID or wrong password. (Failures:{0})", intLoginCount + 1), "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowUltraMessage(string.Format("Missing user ID or wrong password. (Failures:{0})", intLoginCount + 1));
                    return false;
                }


                UserInfo.Current.LoginID = ds.Tables[0].Rows[0]["USER_ID"].ToString();
                UserInfo.Current.Level = ds.Tables[0].Rows[0]["LEVEL"].ToString();

                return true;
            }
            else
            {
                //MessageBox.Show("Missing user ID or wrong password.", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowUltraMessage("Missing user ID or wrong password.");
                return false;
            }
            
        }
        

        /// <summary>
        /// 아이디값 알아오기
        /// </summary>
        /// <returns></returns>
        private DataSet SelectUserInfo()
        {
            //DataSet DS = new DataSet();
            LoginBiz loginBiz = new LoginBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("USER_ID", txtUserName.Text);

            //DB Query
            TTOPSReply reply = loginBiz.SelectUserID(req);

            return reply.ResultSet;
        }
        
        private void UserLoginFail()
        {
            //DataSet DS = new DataSet();
            LoginBiz loginBiz = new LoginBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("USER_ID", txtUserName.Text);

            //DB Query
            TTOPSReply reply = loginBiz.UpdateFailCount(req);
        }

        private void InitFailCount()
        {
            //DataSet DS = new DataSet();
            LoginBiz loginBiz = new LoginBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("USER_ID", txtUserName.Text);

            //DB Query
            TTOPSReply reply = loginBiz.UpdateInitFailCount(req);
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnLogin_Click(sender, e);
            
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnLogin_Click(sender, e);

        }

        private DialogResult ShowUltraMessage(string message)
        {
            using (UltraMessageBoxInfo ultraMessageBoxInfo = new UltraMessageBoxInfo())
            {
                // Set the general display style
                ultraMessageBoxInfo.Style = MessageBoxStyle.Default;

                // Set the text for the Text, Caption, Header and Footer
                ultraMessageBoxInfo.Text = message;
                ultraMessageBoxInfo.Caption = "Login";
                //ultraMessageBoxInfo.Header = "Login";
                //ultraMessageBoxInfo.Footer = "Continuing without restarting can produce unpredictable behaviors.";
                ultraMessageBoxInfo.Footer = "iTOPS";

                // Specify which buttons should be used and which is the default button
                ultraMessageBoxInfo.Buttons = MessageBoxButtons.OK;
                ultraMessageBoxInfo.DefaultButton = MessageBoxDefaultButton.Button1;
                ultraMessageBoxInfo.ShowHelpButton = Infragistics.Win.DefaultableBoolean.False;

                // Display the OS Error Icon
                ultraMessageBoxInfo.Icon = MessageBoxIcon.Warning;

                // Disable the default sounds
                ultraMessageBoxInfo.EnableSounds = Infragistics.Win.DefaultableBoolean.True;


                // Show the UltraMessageBox
                return this.ultraMessageBoxManager1.ShowMessageBox(ultraMessageBoxInfo);
            }
        }

    }
}
