using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRA.TBOS.Windows.Core.Splash;


namespace TRA.TBOS.Windows.Core.MainApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           // SplashManager<SplashForm>.Close();

            this.CenterToScreen();

        }


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter your user ID", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a password", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //로그인
            if (UserLogin())
            {
                //로그인 성공 시 로그인 실패 횟수를 초기화 한다.
                //InitFailCount();

                //로그인 성공 시 로그인 정보를 레지스트리에 저장한다.
                //TMSRegistry.gbSetRegValue(TMSRegistry.Sect1Subkey, TMSRegistry.Sect1Login, txtUserName.Text);

                //MDIMainForm mdi = new MDIMainForm();
                MDIMainForm mdi = new MDIMainForm(); //MDIMainFrom2 로 설정
                // MainForm mdi = new MainForm();
                //mdi.FormClosed += (s1, e1) =>
                //{
                //    Application.Exit();
                //};
                mdi.Show();

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
            /*
            //입력받은 ID로 유저정보를 조회한다.
            DataSet ds = selectUserInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //로그인실패 횟수를 체크하여 5회이상 로그인 실패한경우 잠긴계정으로 취급하며 로그인할 수 없다.
                int intLoginCount = Convert.ToInt32(ds.Tables[0].Rows[0]["LOGIN_FALSE_COUNT"]);

                if (intLoginCount > 4)
                {
                    MessageBox.Show("Your account has been locked because your login failed more than 5 times. \r\nPlease contact the administrator.", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //비밀번호체크
                if (txtPassword.Text != ds.Tables[0].Rows[0]["USER_PWD"].ToString())
                {
                    MessageBox.Show(string.Format("Missing user ID or wrong password. (Failures:{0})", intLoginCount + 1), "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                UserInfo.Current.LoginID = ds.Tables[0].Rows[0]["USER_ID"].ToString();
                UserInfo.Current.Privilege = ds.Tables[0].Rows[0]["PRIVILEGE"].ToString();

                return true;
            }
            else
            {
                MessageBox.Show("Missing user ID or wrong password.", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }*/

            return true;
        }

        private void UserLoginFail()
        {
            /*
            //DataSet DS = new DataSet();
            LoginBiz loginBiz = new LoginBiz();

            //파라미터 객체 생성
            TMonRequest req = new TMonRequest();
            req.Parameters.Add("USER_ID", txtUserName.Text);

            //DB Query
            TMonReply reply = loginBiz.UpdateFailCount(req);
            */
        }

        private void InitFailCount()
        {
            /*
            //DataSet DS = new DataSet();
            LoginBiz loginBiz = new LoginBiz();

            //파라미터 객체 생성
            TMonRequest req = new TMonRequest();
            req.Parameters.Add("USER_ID", txtUserName.Text);

            //DB Query
            TMonReply reply = loginBiz.UpdateInitFailCount(req);
            */
        }
    }
}
