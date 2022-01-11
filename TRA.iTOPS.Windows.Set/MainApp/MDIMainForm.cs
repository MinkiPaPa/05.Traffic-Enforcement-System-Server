using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;

namespace TRA.iTOPS.Windows.Set.MainApp
{
    public partial class MDIMainForm : Form
    {
        private ArrayList formList = new ArrayList();
        //string strDBDSN = string.Empty;

        public MDIMainForm()
        {
            InitializeComponent();
            ultraTabbedMdiManager1.MdiParent = this;
            Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager();
            ultraFormManager1.Form = this;
        }


        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemClose frmClose = new SystemClose();
            if (frmClose.ShowDialog() == DialogResult.OK)
            {
                //this.Close();
                Application.ExitThread();
            }
            
        }


        private void MDIMainForm_Load(object sender, EventArgs e)
        {

            //유저정보 조회
            GetUserInfo();

            LoadForm("TRA.iTOPS.Win.MainView.MainViewForm,TRA.iTOPS.Win,5");
        }

        
        public Form LoadForm(string formType)
        {
            string[] arrFormType = formType.Split(',');
            Assembly assmbly = Assembly.LoadFile(System.IO.Path.Combine(Application.StartupPath, arrFormType[1] + ".dll"));
            Form form = assmbly.CreateInstance(arrFormType[0]) as Form;

            form.FormClosed += new FormClosedEventHandler(Form_Closed);
            formList.Add(arrFormType[0]);
            form.MdiParent = this;
            ((FormBase)form).MenuId = arrFormType[2];
            form.Show();

            return form;
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            Form frm = (Form)sender;
            formList.Remove(frm.ToString().Split(',').GetValue(0).ToString());
        }


        public void GetUserInfo()
        {
            //MDIMainBiz oBiz = new MDIMainBiz();

            //TMonRequest req = new TMonRequest();

            //string strUserId = TMSRegistry.gbGetRegValueSearchInKey(TMSRegistry.Sect1Subkey, TMSRegistry.Sect1Login);
            //req.Parameters.Add("USER_ID", strUserId);
            //req.Parameters.Add("USER_NAME", strUserId);

            //TMonReply reply = oBiz.GetUserInfo(req);

            //DataSet ds = reply.ResultSet;

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    lblUserInfo.Text = ds.Tables[0].Rows[0]["User_Name"].ToString() + "(" + ds.Tables[0].Rows[0]["PRIVILEGE_NM"].ToString() + ")";
            //}

            lblUserInfo.Text = "Test User (No : 122333)";

        }

        private void Section341ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string myform = "TRA.iTOPS.Win.Capturing.Section341Form";
            bool bFormExist = false;

            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                //이미 열려있는 메뉴 선택시 해당 탭 활성화
                if (myform.Equals(Application.OpenForms[i].ToString().Split(',').GetValue(0)))
                {
                    Application.OpenForms[i].Activate();
                    bFormExist = true;
                    break;
                }

            }

            if (!bFormExist)
                LoadForm("TRA.iTOPS.Win.Capturing.Section341Form,TRA.iTOPS.Win,5");

        }

        private void MDIMainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void captureCaseResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string myform = "TRA.iTOPS.Win.Capturing.Court.CaptureCaseResultsForm2";
            bool bFormExist = false;

            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                //이미 열려있는 메뉴 선택시 해당 탭 활성화
                if (myform.Equals(Application.OpenForms[i].ToString().Split(',').GetValue(0)))
                {
                    Application.OpenForms[i].Activate();
                    bFormExist = true;
                    break;
                }

            }

            if (!bFormExist)
                LoadForm("TRA.iTOPS.Win.Capturing.Court.CaptureCaseResultsForm2,TRA.iTOPS.Win,5");


        }
    }
}
