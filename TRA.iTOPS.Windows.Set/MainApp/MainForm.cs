using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;
using TRA.iTOPS.Biz;
using System;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

namespace TRA.iTOPS.Windows.Set.MainApp
{
    public partial class MainForm : Form
    {
        private bool bLogOut = false; 
        private Form FormLogIn;

        public Form LoginForm
        {
            get { return this.FormLogIn; }   // 여기서 얻은 값을 다른폼으로 전달목적
            set { this.FormLogIn = value; }  // 다른폼에서 전달받은값 쓰기
        }

        public MainForm()
        {
            InitializeComponent();
        }

        #region #EVENT
        private void MainForm_Load(object sender, EventArgs e)
        {
            bLogOut = false;

            Lbl_login.Text = "Login ID: " + ITOPS_State.UserID;

            //BtnIssueDoc.Enabled = false;

            StateInit();

            StateCheck();

        }

        private void BtnNumbering_Click(object sender, EventArgs e)
        {
            if (LoadForm("TRA.iTOPS.Win.NumberingForm,TRA.iTOPS.Win,5").ShowDialog() == DialogResult.OK)
            {
                StateCheck();
            }

        }

        private void BtnMakeNATIS_Click(object sender, EventArgs e)
        {

            if (LoadForm("TRA.iTOPS.Win.MakeNatisUpForm,TRA.iTOPS.Win,5").ShowDialog() == DialogResult.OK)
            {
                StateCheck();
            }

        }

        private void BtnImportNATIS_Click(object sender, EventArgs e)
        {
            if (LoadForm("TRA.iTOPS.Win.ImportNatisDownForm,TRA.iTOPS.Win,5").ShowDialog() == DialogResult.OK)
            {
                StateCheck();
            }

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (LoadForm("TRA.iTOPS.Win.SearchForm,TRA.iTOPS.Win,5").ShowDialog() == DialogResult.OK)
            {
                StateCheck();
            }
        }

        private void BtnIssueDoc_Click(object sender, EventArgs e)
        {
            LoadForm("TRA.iTOPS.Win.PrintDataForm,TRA.iTOPS.Win,5").ShowDialog();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            LoadForm("TRA.iTOPS.Win.ReportPayForm,TRA.iTOPS.Win,5").ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bLogOut = true;
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!bLogOut)
            {
                SystemClose frmClose = new SystemClose();
                if (frmClose.ShowDialog() != DialogResult.OK)
                    e.Cancel = true;
                else
                {
                    /*
                    reader.Dispose();
                    */
                    Application.ExitThread();
                }

            }

        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bLogOut)
            {
                FormLogIn.Show();
            }

        }
        #endregion


        #region #Method
        public Form LoadForm(string formType)
        {
            string[] arrFormType = formType.Split(',');
            Assembly assmbly = Assembly.LoadFile(System.IO.Path.Combine(Application.StartupPath, arrFormType[1] + ".dll"));
            Form form = assmbly.CreateInstance(arrFormType[0]) as Form;

            return form;
        }

        private void StateInit()
        {
            lbl_CourtRoll.Text = "0";
            lbl_DownNATIS.Text = "0";
            lbl_Initial.Text = "0";
            lbl_NBS.Text = "0";
            lbl_Notice.Text = "0";
            lbl_Numbering.Text = "0";
            lbl_Sumon.Text = "0";
            lbl_UpNATIS.Text = "0";
            lbl_WOA.Text = "0";
            lbl_ByCanceled.Text = "0";
            lbl_ByCourt.Text = "0";
            lbl_byNATIS.Text = "0";
            lbl_byPaid.Text = "0";
            lbl_BySystem.Text = "0";
            
        }

        private void StateCheck()
        {
            //ITOPS 상태를 조회한다.
            DataSet ds = SelectStateInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_Initial.Text = ds.Tables[0].Rows[0]["STATUS_INIT"].ToString();
                lbl_Numbering.Text = ds.Tables[0].Rows[0]["STATUS_NUMBER"].ToString();
                lbl_UpNATIS.Text = ds.Tables[0].Rows[0]["STATUS_NATIS_UP"].ToString();
                lbl_DownNATIS.Text = ds.Tables[0].Rows[0]["STATUS_NATIS_DOWN"].ToString();
                lbl_Notice.Text = ds.Tables[0].Rows[0]["STATUS_NOTICE"].ToString();
                lbl_NBS.Text = ds.Tables[0].Rows[0]["STATUS_NBS"].ToString();
                lbl_Sumon.Text = ds.Tables[0].Rows[0]["STATUS_SUMMON"].ToString();
                lbl_WOA.Text = ds.Tables[0].Rows[0]["STATUS_WOA"].ToString();
                lbl_CourtRoll.Text = ds.Tables[0].Rows[0]["STATUS_COURT"].ToString();
                lbl_byPaid.Text = ds.Tables[0].Rows[0]["STATUS_CLOSED_PAID"].ToString();
                lbl_ByCanceled.Text = ds.Tables[0].Rows[0]["STATUS_CLOSED_CANCEL"].ToString();
                lbl_ByCourt.Text = ds.Tables[0].Rows[0]["STATUS_CLOSED_COURT"].ToString();
                lbl_BySystem.Text = ds.Tables[0].Rows[0]["STATUS_CLOSED_SYSTEM"].ToString();
                lbl_byNATIS.Text = ds.Tables[0].Rows[0]["STATUS_CLOSED_NOTATIS"].ToString();

                int nNatisError = -1;
                if(!Int32.TryParse(ds.Tables[0].Rows[0]["STATUS_NATIS_ERROR"].ToString(), out nNatisError))
                {
                    nNatisError = 0;
                }
                    
                if (nNatisError > 0)
                    MessageBox.Show(string.Format("NATIS Error occurred : {0}", nNatisError), "Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private DataSet SelectStateInfo()
        {
            //DataSet DS = new DataSet();
            StateBiz loginBiz = new StateBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = loginBiz.SelectState(req);

            return reply.ResultSet;
        }

        #endregion

    }
}
