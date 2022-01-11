using TRA.iTOPS.Windows.Set.MainApp;
using TRA.iTOPS.Contracts.Common;
using TRA.iTOPS.Contracts.Session;
using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace TRA.iTOPS.Win
{

    public partial class MakeNatisUpForm : Form
    {

        private bool bDone = false;
        private string sCurUpFile = string.Empty;
        private string upFileName = string.Empty;
        private int nCondition = -1;


        public MakeNatisUpForm()
        {
            InitializeComponent();
        }

        #region #EVENT

        private void NatisUpDownForm_Load(object sender, EventArgs e)
        {
            Rb_FirstUp.Checked = true;

            SetNatisUpFile();

            Lst_History.View = View.Details;
            Lst_History.FullRowSelect = true;
            Lst_History.GridLines = true;

            // 리스트뷰 컬럼 추가 
            Lst_History.Columns.Add("Date", 140);
            Lst_History.Columns.Add("Action", 120);
            Lst_History.Columns.Add("Commnet", 120);
            Lst_History.Columns.Add("Status", 100);

            History_update(ITOPS_State.ITOPS_STATE_NATIS_UP);

        }

        private void Btn_MakeUpFile_Click(object sender, EventArgs e)
        {

            if (sCurUpFile == null)
                return;

            string strlog = string.Empty;

            FileInfo fi = new FileInfo(sCurUpFile);
            if (fi.Exists == true)
            {
                Trace("FAIL : There are already UpFiles");
                return;
            }

            if (Rb_FirstUp.Checked)
                nCondition = 0;
            else if (Rb_StillNotDown.Checked)
                nCondition = 1;
            else if (RB_Retry.Checked)
                nCondition = 2;

            DataSet ds = SelectCaseUp();

            int nTotal = ds.Tables[0].Rows.Count;
            if (nTotal > 0)
            {
                strlog = string.Format("there are {0} Data --> {1}", nTotal, upFileName + ".inp");
                Trace(strlog);

                strlog = string.Format("making Up file ... [{0} cases]", nTotal);
                Trace(strlog);

                BarProgress.Minimum = 0;
                BarProgress.Maximum = nTotal;
                BarProgress.Step = 1;

                string sline = string.Empty;
                string ConDt = string.Empty;
                //string NoticeNum = string.Empty;
                string scuid = string.Empty;
                long lcuid;
                int iTranID = Convert.ToInt32(upFileName); // 파일이름으로 TranID 생성 

                string sTranID = string.Format("{0:00000000}", iTranID); // TranID는 8자리

                DateTime dt = new DateTime();

                StreamWriter sw = new StreamWriter(sCurUpFile);

                // export to the file ...
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DateTime.TryParse(dr["WHEN_DT"].ToString(), null, System.Globalization.DateTimeStyles.AssumeLocal, out dt);
                    ConDt = dt.ToString("yyyyMMdd"); //년월일만필요(8자리)
                    //NoticeNum = dr["NOTICE_NUM"].ToString().Replace("/", "");
                    lcuid = Convert.ToInt64(dr["CUID"]);
                    scuid = string.Format("{0:00000000000000}", lcuid); // 14자리(뉴캐슬과 같이)

                    sline = string.Format("504{0}003{1:00}{2}060{3:00}{4}065{5:00}{6}99901",
                                            sTranID, // Tran ID
                                            dr["CARNUM"].ToString().Length,
                                            dr["CARNUM"].ToString(),
                                            ConDt.Length,
                                            ConDt,
                                            //NoticeNum.Length,
                                            //NoticeNum
                                            scuid.Length,
                                            scuid
                                            );

                    sw.WriteLine(sline);

                    // 상태 업데이트 
                    UpdateStatusChanged(Convert.ToInt64(dr["CUID"]), Convert.ToInt32(dr["CODE_STATUS"]), ITOPS_State.ITOPS_STATE_NATIS_UP, 2, sTranID);

                    BarProgress.PerformStep();
                }

                // 트랜정보에 대한 마감 처리 
                string traninfo = string.Empty;
                traninfo = "9999999999999";
                sline = string.Format("504{0}065{1:00}{2}99901", sTranID, traninfo.Length, traninfo);
                sw.WriteLine(sline);

                // 파일에 대한 종료처리
                sline = string.Format("599{0}900{1:00}{2}99901", sTranID, upFileName.Length, upFileName); // 
                sw.WriteLine(sline);

                sw.Close();


                // History 추가
                InsertHistory(ITOPS_State.ITOPS_STATE_NATIS_UP, 0, upFileName + ".inp", Convert.ToString(nTotal));

                // 모든 상태가 정상적이면 파일Seq 업데이트
                UpdateNatisUpFileSeq();

                strlog = string.Format("Done making Up file ... [{0} cases]", nTotal);
                Trace(strlog);

                bDone = true;
            }
            else
            {
                Trace("No Data to make Up file");
            }

        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            if (bDone)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;


            this.Close();

        }
        #endregion


        #region #METHOD

        private void SetNatisUpFile()
        {
            string curUpDir = Directory.GetCurrentDirectory() + @"\UpFile\";
            DirectoryInfo di = new DirectoryInfo(curUpDir);
            if (di.Exists != true)
                Directory.CreateDirectory(curUpDir);

            DataSet ds = SelectNatisUpFilename();
            if (ds.Tables[0].Rows.Count > 0)
            {
                upFileName = string.Format("{0}{1}{2:000}", ds.Tables[0].Rows[0]["FILE_PREFIX"].ToString(), ds.Tables[0].Rows[0]["FILE_AUTH"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[0]["FILE_SEQ"]));

                sCurUpFile = curUpDir + upFileName + ".inp";

                lbl_FilePath.Text = sCurUpFile;
            }

        }



        private void UpdateNatisUpFileSeq()
        {

            NatisBiz natisUp = new NatisBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = natisUp.UpdateNatisUpFileSeq(req);

        }

        private bool InsertHistory(int nStatus, int nAux, string Comment, string Comment2)
        {
            StateBiz stateBiz = new StateBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            req.Parameters.Add("ACTION_CODE", nStatus);
            req.Parameters.Add("ACTION_CODE_AUX", nAux);
            req.Parameters.Add("COMMENT", Comment);
            req.Parameters.Add("COMMENT_AUX", Comment2);

            TTOPSReply reply = stateBiz.InsertHistory(req);

            if (reply.ResultCode == "OK")
                return true;
            else
               return false;

        }

        private bool UpdateStatusChanged(Int64 cuid, int nCurr, int nNext, int nOpt, string nOptVal)
        {
            StateBiz stateBiz = new StateBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CUID", cuid);

            //DB Query
            TTOPSReply reply = stateBiz.UpdateStatusChanged(req, nCurr, nNext, nOpt, nOptVal);

            if (reply.ResultCode == "OK")
                return true;
            else
                return false;
        }

        private void NatisInterfaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bDone)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;

        }

        private void History_update(int ncase)
        {

            Lst_History.Items.Clear();

            DataSet ds = SelectHistoryInfo(ncase);

            String[] arr = new String[4];

            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    arr[0] = ITOPS_State.ConvertDBDate(dr["CTIME"].ToString());
                    arr[1] = ITOPS_State.TheCaseStateDesc(Convert.ToInt32(dr["ACTION_CODE"]));
                    arr[2] = dr["COMMENT"].ToString();
                    arr[3] = dr["COMMENT_AUX"].ToString();

                    ListViewItem lvt = new ListViewItem(arr);

                    Lst_History.Items.Add(lvt);

                }
            }
        }

        private void Trace(string message)
        {
            string curTime = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

            Edt_Log.AppendText(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " : " + message + "\r\n");
        }

        private DataSet SelectHistoryInfo(int ncase)
        {
            //DataSet DS = new DataSet();
            StateBiz stateBiz = new StateBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = stateBiz.SelectHistory(req, ncase);

            return reply.ResultSet;
        }

        private DataSet SelectNatisUpFilename()
        {
            NatisBiz natisUp = new NatisBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = natisUp.SelectNatisUpFilename(req);

            return reply.ResultSet;

        }

        private DataSet SelectCaseUp()
        {
            NatisBiz natisUp = new NatisBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            // 이건 나중에 force 와 Retry 구현할때 쓰자
            string sToday = string.Empty;
            string sComp = string.Empty;
            int nCompDays = 0;

            //DB Query
            TTOPSReply reply = natisUp.SelectCaseUp(req, nCondition, sToday, sComp, nCompDays);

            return reply.ResultSet;
        }

        #endregion
    }
}
