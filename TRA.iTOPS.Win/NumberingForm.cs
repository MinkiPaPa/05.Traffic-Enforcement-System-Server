using TRA.iTOPS.Windows.Set.MainApp;
using TRA.iTOPS.Contracts.Common;
using TRA.iTOPS.Contracts.Session;
using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TRA.iTOPS.Win
{
    public partial class NumberingForm : FormBase
    {
        private DataSet Candi_ds;
        private string smessage;
        private int nTotal = 0;

        private string EasyPayID;
        private int FileGenNum = 0;

        private bool bDone = false;

        int A_MIN = 0, A_MAX = 0, A_CURR = 0, A_ATTR = 0;
        int B_MIN = 0, B_MAX = 0, B_CURR = 0, B_ATTR = 0;
        int C_MIN = 0, C_MAX = 0, C_CURR = 0, C_ATTR = 0;
        int D_MIN = 0, D_MAX = 0, D_CURR = 0, D_ATTR = 0;
        int nA = 0, nB = 0, nC = 0, nD = 0;

        [DllImport("TRA.iTOP.MFCDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int GetEasyPayNumberCheckDigit(int m_EasyPayID, int nNA, int nNB, int nNC);


        public NumberingForm()
        {
            InitializeComponent();
        }

        #region #EVENT

        private void NumberingForm_Load(object sender, EventArgs e)
        {
            Numbering_init();

            Lst_History.View = View.Details;
            Lst_History.FullRowSelect = true;
            Lst_History.GridLines = true;

            // 리스트뷰 컬럼 추가 
            Lst_History.Columns.Add("Date", 140);
            Lst_History.Columns.Add("Action", 120);
            Lst_History.Columns.Add("Commnet", 120);
            Lst_History.Columns.Add("Status", 100);

            History_update(ITOPS_State.ITOPS_STATE_NUMBER);
            Trace("Waiting to Get candidates ...");
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            if (bDone)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;


            this.Close();
        }

        private void Btn_Get_Click(object sender, EventArgs e)
        {
            // DataSet 중복쿼리실행 방지
            if (Candi_ds != null)
                return;


            Candi_ds = SelectCandidata();
            nTotal = Candi_ds.Tables[0].Rows.Count;
            if (nTotal > 0)
            {
                Txt_CandiNumber.Text = Convert.ToString(Candi_ds.Tables[0].Rows.Count);

                Txt_OffenStartDate.Text = ITOPS_State.ConvertDBDate(Candi_ds.Tables[0].Rows[0]["WHEN_DT"].ToString());
                Txt_OffenEndDate.Text = ITOPS_State.ConvertDBDate(Candi_ds.Tables[0].Rows[nTotal - 1]["WHEN_DT"].ToString());

                Btn_Do.Enabled = true;
            }

            smessage = string.Format("{0} candidate(s)!", nTotal);
            Trace(smessage);
        }



        private void Btn_Do_Click(object sender, EventArgs e)
        {
            if (Candi_ds != null)
            {
                int nStartNum = NumberingPrepare(nTotal, ITOPS_State.UserID);

                Do_Numbering();

                Trace("Done");

                Candi_ds = null;

                Txt_OffenStartDate.Text = "";
                Txt_OffenEndDate.Text = "";
                Txt_CandiNumber.Text = "";

                string scomment = string.Format("start: {0}", nStartNum);
                string scomment2 = string.Format("count: {0}", nTotal);

                InsertHistory(ITOPS_State.ITOPS_STATE_NUMBER, 0, scomment, scomment2);

                History_update(ITOPS_State.ITOPS_STATE_NUMBER);

                Btn_Do.Enabled = false;
            }
        }
        #endregion

        #region #METHOD

        private void Numbering_init()
        {
            Candi_ds = null;

            EasyPayID = string.Empty;
            FileGenNum = 0;

            Edt_Log.Clear();
            Lst_History.Clear();

            bDone = false;
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

            Edt_Log.AppendText(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " : " + message);
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

            if (reply.ResultCode != "OK")
                return false;

            return true;

        }

        private void Do_Numbering()
        {
            
            string sNumber, sEasyNum;
            int nChk = 0;

            BarProgress.Minimum = 0;
            BarProgress.Maximum = nTotal;
            BarProgress.Step = 1;

            foreach (DataRow dr in Candi_ds.Tables[0].Rows)
            {
                //NoticeNumber 생성
                sNumber = GetNoticeNumberSetNext();

                int nEasyPayID = Convert.ToInt32(EasyPayID);

                // mfc dll 호출
                nChk = GetEasyPayNumberCheckDigit(nEasyPayID, nA, nB, nC);

                sEasyNum = string.Format("9{0}{1:00}{2:00000}{3:000}{4}", EasyPayID, nA, nB, nC, nChk);
                //sEasyNum.Format("9%s%02d%05d%03d%d",
                //    m_sEasyPayID, nA, nB, nC, nChk);

                UpdateNoticeNumberState(Convert.ToInt32(dr["CUID"]), sNumber, sEasyNum, nA, nB, nC, nD, ITOPS_State.ITOPS_STATE_NUMBER);

                BarProgress.PerformStep();

            }

            bDone = true;
        }


        private string GetNoticeNumberSetNext()
        {
            //  Notice Number 생성규칙
            //  20(A:Section 341 Notices) / 00001(B:Form Number) / 252(C:시 우편번호) / 000316(Check Digit Verification number :D = A + (2 x B) + C )

            // Get current value 
            nA = A_CURR;
            nB = B_CURR;
            nC = C_CURR;

            // TCS rule 
            nD = nA + (nB * 2) + nC;
            nD %= 1000000;

            // set for next usage 
            B_CURR++;
            if (B_CURR > B_MAX)
            {
                B_CURR = B_MIN;

                A_CURR++;
                if (A_CURR > A_MAX)
                {
                    A_CURR = A_MIN;
                }
            }

            string strNumber = string.Format("{0:00}/{1:00000}/{2:000}/{3:000000}", nA, nB, nC, nD);

            return strNumber;
        }

        
        //private int GetEasyPayNumberCheckDigit()
        //{
        //    int nRRRR = Convert.ToInt32(EasyPayID);

        //    string pNUMBER;
        //    pNUMBER = string.Format("{0:0000}{1:00}{2:00000}{3:000}", nRRRR, nA, nB, nC);
        //    //sprintf_s(pNUMBER, 20, "%04d%02d%05d%03d", nRRRR, nA, nB, nC);

        //    /*
        //    char pLuhn;

        //    int nLimit = pNUMBER.Length;
            
        //    bool bOdd = true;
        //    for (int nX = nLimit - 1; nX >= 0; nX--, bOdd = !bOdd)
        //    {
        //        pLuhn[nX] = (pNUMBER[nX] - '0');

        //        if (bOdd)
        //        {
        //            pLuhn[nX] *= 2;
        //            if (pLuhn[nX] > 9)
        //                pLuhn[nX] = pLuhn[nX] - 9;
        //        }
        //    }
            
        //    int nsum = 0;
        //    for (int nX = 0; nX < nLimit; nX++)
        //    {
        //        nsum += pLuhn[nX];
        //    }
        //    */
        //    int nsum = 0;
        //    int nanswer = (nsum + 9) / 10 * 10;
        //    nanswer = nanswer - nsum;

        //    return nanswer;

        //}

        private int NumberingPrepare(int nMany, string sWho)
        {
            int nNumStart = 0;
            DataSet ds = SelectEasyPayID();

            if(ds.Tables[0].Rows.Count > 0)
            {
                EasyPayID = ds.Tables[0].Rows[0]["RECVID"].ToString();
                FileGenNum = Convert.ToInt32(ds.Tables[0].Rows[0]["FILE_GEN"]);
            }

            nNumStart = KeepSetNoticeNumber(nMany);

            // histroy 는 나중에
            InsertHistoryNoticeNumIssue(nMany, sWho);

            return nNumStart;
        }

        private int KeepSetNoticeNumber(int nMany)
        {
            int ntotal = 0;

            int TheValue1 = 0;

            DataSet ds = SelectNoticeNumber();
            if (ds.Tables[0].Rows.Count > 0)
            {
                A_MIN = Convert.ToInt32(ds.Tables[0].Rows[0]["A_MIN"]);
                A_MAX = Convert.ToInt32(ds.Tables[0].Rows[0]["A_MAX"]);
                A_CURR = Convert.ToInt32(ds.Tables[0].Rows[0]["A_CURR"]);
                A_ATTR = Convert.ToInt32(ds.Tables[0].Rows[0]["A_ATTR"]);
                B_MIN = Convert.ToInt32(ds.Tables[0].Rows[0]["B_MIN"]);
                B_MAX = Convert.ToInt32(ds.Tables[0].Rows[0]["B_MAX"]);
                B_CURR = Convert.ToInt32(ds.Tables[0].Rows[0]["B_CURR"]);
                B_ATTR = Convert.ToInt32(ds.Tables[0].Rows[0]["B_ATTR"]);
                C_MIN = Convert.ToInt32(ds.Tables[0].Rows[0]["C_MIN"]);
                C_MAX = Convert.ToInt32(ds.Tables[0].Rows[0]["C_MAX"]);
                C_CURR = Convert.ToInt32(ds.Tables[0].Rows[0]["C_CURR"]);
                C_ATTR = Convert.ToInt32(ds.Tables[0].Rows[0]["C_ATTR"]);
                D_MIN = Convert.ToInt32(ds.Tables[0].Rows[0]["D_MIN"]);
                D_MAX = Convert.ToInt32(ds.Tables[0].Rows[0]["D_MAX"]);
                D_CURR = Convert.ToInt32(ds.Tables[0].Rows[0]["D_CURR"]);
                D_ATTR = Convert.ToInt32(ds.Tables[0].Rows[0]["D_ATTR"]);

                if(nMany > 0)
                {
                    int nOverflowB = (B_CURR + nMany) / (B_MAX - B_MIN + 1);
                    if (nOverflowB > 0)
                    {
                        int nNextA = A_CURR + nOverflowB;
                        if (nNextA > A_MAX)
                        {
                            nNextA = A_MIN;
                        }

                        TheValue1 = nNextA;
                        UpdateCurr(0, TheValue1);
                    }

                    TheValue1 = ((B_CURR + nMany) % (B_MAX - B_MIN + 1));
                    UpdateCurr(1, TheValue1);

                }
            }

            return (ntotal < 0 ? ntotal : (A_CURR * 100000 + B_CURR));
        }

        private void UpdateCurr(int nCurr, int nNext)
        {
            NumberingBiz numberingBiz = new NumberingBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CURR", nNext);

            //DB Query
            TTOPSReply reply = numberingBiz.UpdateCurr(nCurr, req);

        }


        private DataSet SelectNoticeNumber()
        {
            NumberingBiz numberingBiz = new NumberingBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = numberingBiz.SelectNoticeNumber(req);

            return reply.ResultSet;

        }

        private bool InsertHistoryNoticeNumIssue(int nMany, string pwho)
        {
            NumberingBiz numberingBiz = new NumberingBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            req.Parameters.Add("NN_A", A_CURR);
            req.Parameters.Add("NN_B", B_CURR);
            req.Parameters.Add("NN_C", C_CURR);
            req.Parameters.Add("NN_D", D_CURR);
            req.Parameters.Add("REQ_NUM", nMany);
            req.Parameters.Add("REQ_KEY", "");
            req.Parameters.Add("REQ_WHO", pwho);

            TTOPSReply reply = numberingBiz.InsertHistoryNoticeNumIssue(req);

            if (reply.ResultCode != "OK")
                return false;

            return true;
        }


        private bool UpdateNoticeNumberState(int cuid,string sNumber, string sEasyPayNumber, int nA, int nB, int nC, int nD, int nstat)
        {

            NumberingBiz numberingBiz = new NumberingBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("NOTICE_NUM", sNumber);
            req.Parameters.Add("PAY_ACCOUNT", sEasyPayNumber);
            req.Parameters.Add("NOTICE_NUM_A", nA);
            req.Parameters.Add("NOTICE_NUM_B", nB);
            req.Parameters.Add("NOTICE_NUM_C", nC);
            req.Parameters.Add("NOTICE_NUM_D", nD);
            req.Parameters.Add("CODE_STATUS", nstat);
            req.Parameters.Add("CUID", cuid);


            //DB Query
            TTOPSReply reply = numberingBiz.UpdateNoticeNumberState(req);


            return true;
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
        private DataSet SelectCandidata()
        {
            NumberingBiz numberingBiz = new NumberingBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = numberingBiz.SelectCandidate(req);

            return reply.ResultSet;

        }
        private DataSet SelectEasyPayID()
        {
            NumberingBiz numberingBiz = new NumberingBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = numberingBiz.SelectEasyPayID(req);

            return reply.ResultSet;

        }
        #endregion

    }
}
