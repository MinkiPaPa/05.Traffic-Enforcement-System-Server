using TRA.iTOPS.Windows.Set.MainApp;
using TRA.iTOPS.Contracts.Common;
using TRA.iTOPS.Contracts.Session;
using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TRA.iTOPS.Win
{
    public partial class ImportNatisDownForm : Form
    {
        #region # Declare Variable [ public, private, const ..]
        private bool bDone = false;
        private string sCurDownFile = string.Empty;
        private string sNatisFileName = string.Empty;
        int NatisHeaderSize = (1 + 2 + 8);


        /// <summary>
        /// Notice 발급 관련
        private string EasyPayID;
        private int FileGenNum = 0;
        int A_MIN = 0, A_MAX = 0, A_CURR = 0, A_ATTR = 0;
        int B_MIN = 0, B_MAX = 0, B_CURR = 0, B_ATTR = 0;
        int C_MIN = 0, C_MAX = 0, C_CURR = 0, C_ATTR = 0;
        int D_MIN = 0, D_MAX = 0, D_CURR = 0, D_ATTR = 0;
        int nA = 0, nB = 0, nC = 0, nD = 0;

        int iSucessNatice = 0; 

        [DllImport("TRA.iTOP.MFCDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int GetEasyPayNumberCheckDigit(int m_EasyPayID, int nNA, int nNB, int nNC);
        /// </summary>



        class NatisVal
        {
            public int index;
            public int Length;
            public string Val;
        }

        ArrayList aryNatis = new ArrayList();
        #endregion


        public ImportNatisDownForm()
        {
            InitializeComponent();
        }

        #region #EVENT
        private void NatisImportDownForm_Load(object sender, EventArgs e)
        {
            Chk_SameVehicle.Checked = true;
            
            Chk_TRANID.Checked = true;
            Chk_WaitingDownStatus.Checked = true;
            Chk_SequenceNum.Checked = true;

            Btn_ImportFile.Enabled = false;
            Lbl_FilePath.Text = "";

            Lst_History.View = View.Details;
            Lst_History.FullRowSelect = true;
            Lst_History.GridLines = true;

            // 리스트뷰 컬럼 추가 
            Lst_History.Columns.Add("Date", 140);
            Lst_History.Columns.Add("Action", 120);
            Lst_History.Columns.Add("Commnet", 120);
            Lst_History.Columns.Add("Status", 100);

            History_update(ITOPS_State.ITOPS_STATE_NATIS_DOWN);

        }


        private void Btn_DownFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Multiselect = false;  // Multiselect 못하게
            openFileDlg.DefaultExt = "out";
            openFileDlg.Filter = "Natis out Files (*.out)|0*.out;*.out";
            openFileDlg.ShowDialog();
            if (openFileDlg.FileName.Length > 0)
            {
                FileInfo fi = new FileInfo(openFileDlg.FileName);
                Lbl_FilePath.Text = fi.FullName;
                sCurDownFile = fi.FullName;
                sNatisFileName = fi.Name;

                if (fi.Exists != true)
                {
                    Trace("There is no file");
                    return;
                }

                Btn_ImportFile.Enabled = true;
            }

        }

        private void Btn_ImportFile_Click(object sender, EventArgs e)
        {
            //notice Numbering을 위한 사전 작업 
            NumberingPrepare();

            Trace("Start to import a NaTIS out file ...");
            string rLine;
            StreamReader sr = new StreamReader(sCurDownFile);

            int nL = 1;
            
            while ((rLine = sr.ReadLine()) != null)
            {
                ProcessNatis(rLine, nL);
                nL++;
            }

            if(iSucessNatice > 0)
            {
                int nNumStart = SetNoticeCurr(iSucessNatice);  // 다음을 위해 현재 Notice 정보를 업데이트
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
        private void KeepNoticeNumber()
        {
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
            }
        }


        private int SetNoticeCurr(int nMany)
        {
            int ntotal = 0;

            int TheValue1 = 0;

            if (nMany > 0)
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

                //TheValue1 = ((B_CURR + nMany) % (B_MAX - B_MIN + 1));
                TheValue1 = (B_CURR % (B_MAX - B_MIN + 1)); // nMany 를 넣을 필요가 없다. 
                UpdateCurr(1, TheValue1);

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


        private void ProcessNatis(string rLine, int nL)
        {
            string sCarInfo = rLine.Substring(0, 3);

            // 555 인경우에만 차량정보임
            if (sCarInfo == "501" || sCarInfo == "599")
                return;

            string TranID = rLine.Substring(3,8); // 8자리

            string sBody = rLine.Substring(NatisHeaderSize, rLine.Length - NatisHeaderSize);
            int noffset = 0, nlen = -1, nIndex = -1;

            string sLength = string.Empty;
            string sVal = string.Empty;
            string sIndex = string.Empty;
            string strlog = string.Empty;
            int nrow = 0;
            while (noffset < sBody.Length) 
            {
                sIndex = sBody.Substring(noffset, 3);


                //nIndex = Convert.ToInt32(sIndex);
                if(!Int32.TryParse(sIndex, out nIndex))
                {
                    strlog = string.Format("{0} Line, {1} Row : {2} Parse Error~!", nL, nrow, sIndex);
                    Trace(strlog, false);

                    //MessageBox.Show(sIndex, "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                if (nIndex == 999) // 엔딩값 999 
                    break;
                    
                noffset += 3;

                sLength = sBody.Substring(noffset, 2);
                noffset += 2;

                //nlen = Convert.ToInt32(sLength);
                if (!Int32.TryParse(sLength, out nlen))
                {
                    strlog = string.Format("{0} Line, {1} Row: {2} Parse Error~!", nL, nrow, sLength);
                    Trace(strlog, false);

                    //MessageBox.Show(sLength, "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                }


                sVal = sBody.Substring(noffset, nlen);
                noffset += nlen;

                NatisVal na = new NatisVal
                {
                    index = nIndex,
                    Length = nlen,
                    Val = sVal
                };

                aryNatis.Add(na);
                nrow++;
            }

            //DataSet ds = SelectCaseDown(aryNatis, TranID);
            //if(ds.Tables[0].Rows.Count > 0)
           // {
                 UpdateByNatisInfo(aryNatis, TranID, nL);
            //}

            aryNatis.Clear();
            
            // History 추가
            InsertHistory(ITOPS_State.ITOPS_STATE_NATIS_DOWN, 0, sNatisFileName + ".out", Convert.ToString(nrow));

            strlog = string.Format("Done {0} Line Importing.. [{1} cases]", nL, nrow);
            Trace(strlog, false);

            bDone = true;

        }


        private int NumberingPrepare()
        {
            int nNumStart = 0;
            DataSet ds = SelectEasyPayID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                EasyPayID = ds.Tables[0].Rows[0]["RECVID"].ToString();
                FileGenNum = Convert.ToInt32(ds.Tables[0].Rows[0]["FILE_GEN"]);
            }

            KeepNoticeNumber();

            return nNumStart;
        }

        private void UpdateByNatisInfo(ArrayList aryNatis, string TranID, int nLine)
        {
            string carnum           = string.Empty;
            string CODE_STATUS      = string.Empty;
            string CODE_STATUS_AUX  = string.Empty;
            string LAST_NATIS_DN    = string.Empty;
            string N001_ANSWER      = string.Empty;
            string N004_V_USAGE     = string.Empty;
            string N005_V_CDATE     = string.Empty;
            string N006_V_RQUAL     = string.Empty;
            string N007_V_CDATE     = string.Empty;
            string N008_V_STATE     = string.Empty;
            string N009_V_CDATE     = string.Empty;
            string N010_V_DESC      = string.Empty;
            string N011_V_CATEGORY  = string.Empty;
            string N012_X_CCNUM     = string.Empty;
            string N013_X_RAUTH     = string.Empty;
            string N014_P_IDTYPE    = string.Empty;
            string N015_P_IDNUM     = string.Empty;
            string N016_P_NAME      = string.Empty;
            string N017_P_INITIAL   = string.Empty;
            string N018_P_NOSO      = string.Empty;
            string N019_P_ADDR1     = string.Empty;
            string N020_P_ADDR2     = string.Empty;
            string N021_P_ADDR3     = string.Empty;
            string N022_P_ADDR4     = string.Empty;
            string N023_P_ADDR5     = string.Empty;
            string N024_P_ACODE     = string.Empty;
            string N025_P_STRT1     = string.Empty;
            string N026_P_STRT2     = string.Empty;
            string N027_P_STRT3     = string.Empty;
            string N028_P_STRT4     = string.Empty;
            string N029_P_NATURE    = string.Empty;
            string N030_P_SCODE     = string.Empty;
            string N031_PX_IDTYPE   = string.Empty;
            string N032_PX_NAME     = string.Empty;
            string N033_PX_INITIAL  = string.Empty;
            string N034_V_MAKE      = string.Empty;
            string N035_V_MODEL     = string.Empty;
            string N036_X_PCCNUM    = string.Empty;
            string N037_P_CDATE     = string.Empty;
            string N041_V_TYPE      = string.Empty;
            string N042_V_USAGE     = string.Empty;
            string N043_V_COLOR     = string.Empty;
            string N053_PX_IDNUM    = string.Empty;
            string N061_V_REGNUM    = string.Empty;
            string N062_D_LICCODE   = string.Empty;
            string N063_D_LICISSUE  = string.Empty;
            string N064_D_AGE       = string.Empty;
            string N065_TX_NUM      = string.Empty;
            string N066_X_VIN       = string.Empty;
            string N067_V_ENGIN     = string.Empty;

            
            foreach (NatisVal na in aryNatis)
            {
                switch(na.index)
                {
                    case 3:
                        carnum = na.Val;
                        break;
                    case 4:
                        N004_V_USAGE = na.Val;
                        break;
                    case 5:
                        N005_V_CDATE = na.Val;
                        break;
                    case 6:
                        N006_V_RQUAL = na.Val;
                        break;
                    case 7:
                        N007_V_CDATE = na.Val;
                        break;
                    case 8:
                        N008_V_STATE = na.Val;
                        break;
                    case 9:
                        N009_V_CDATE = na.Val;
                        break;
                    case 10:
                        N010_V_DESC = na.Val;
                        break;
                    case 11:
                        N011_V_CATEGORY = na.Val;
                        break;
                    case 12:
                        N012_X_CCNUM = na.Val;
                        break;
                    case 13:
                        N013_X_RAUTH = na.Val;
                        break;
                    case 14:
                        N014_P_IDTYPE = na.Val;
                        break;
                    case 15:
                        N015_P_IDNUM = na.Val;
                        break;
                    case 16:
                        N016_P_NAME = na.Val;
                        break;
                    case 17:
                        N017_P_INITIAL = na.Val;
                        break;
                    case 18:
                        N018_P_NOSO = na.Val;
                        break;
                    case 19:
                        N019_P_ADDR1 = na.Val;
                        break;
                    case 20:
                        N020_P_ADDR2 = na.Val;
                        break;
                    case 21:
                        N021_P_ADDR3 = na.Val;
                        break;
                    case 22:
                        N022_P_ADDR4 = na.Val;
                        break;
                    case 23:
                        N023_P_ADDR5 = na.Val;
                        break;
                    case 24:
                        N024_P_ACODE = na.Val;
                        break;
                    case 25:
                        N025_P_STRT1 = na.Val;
                        break;
                    case 26:
                        N026_P_STRT2 = na.Val;
                        break;
                    case 27:
                        N027_P_STRT3 = na.Val;
                        break;
                    case 28:
                        N028_P_STRT4 = na.Val;
                        break;
                    case 29:
                        N029_P_NATURE = na.Val;
                        break;
                    case 30:
                        N030_P_SCODE = na.Val;
                        break;
                    case 31:
                        N031_PX_IDTYPE = na.Val;
                        break;
                    case 32:
                        N032_PX_NAME = na.Val;
                        break;
                    case 33:
                        N033_PX_INITIAL = na.Val;
                        break;
                    case 34:
                        N034_V_MAKE = na.Val;  // carMake (name + code)
                        break;
                    case 35:
                        N035_V_MODEL = na.Val;
                        break;
                    case 36:
                        N036_X_PCCNUM = na.Val;
                        break;
                    case 37:
                        N037_P_CDATE = na.Val;
                        break;
                    case 41:
                        N041_V_TYPE = na.Val;  // carType (name + code)
                        break;
                    case 42:
                        N042_V_USAGE = na.Val;
                        break;
                    case 53:
                        N053_PX_IDNUM = na.Val;
                        break;
                    case 61:
                        N061_V_REGNUM = na.Val;
                        break;
                    case 62:
                        N062_D_LICCODE = na.Val;
                        break;
                    case 63:
                        N063_D_LICISSUE = na.Val;
                        break;
                    case 64:
                        N064_D_AGE = na.Val;
                        break;
                    case 65:
                        N065_TX_NUM = na.Val;  // NoticeNum
                        break;
                    case 66:
                        N066_X_VIN = na.Val;
                        break;
                    case 67:
                        N067_V_ENGIN = na.Val;
                        break;

                }
                
            }

            //MessageBox.Show(carnum +"    "+ N065_TX_NUM);
            string log = string.Empty;
            //log = string.Format("try Import Notice = {0}, TranID = {1}", carnum, N065_TX_NUM);
            //Trace(log);

            NatisBiz natisDown = new NatisBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CARNUM",         carnum);
            req.Parameters.Add("N000_TRANID",    TranID);
            req.Parameters.Add("N004_V_USAGE",   N004_V_USAGE);
            req.Parameters.Add("N005_V_CDATE",   N005_V_CDATE);
            req.Parameters.Add("N006_V_RQUAL",   N006_V_RQUAL);
            req.Parameters.Add("N007_V_CDATE",   N007_V_CDATE);
            req.Parameters.Add("N008_V_STATE",   N008_V_STATE);
            req.Parameters.Add("N009_V_CDATE",   N009_V_CDATE);
            req.Parameters.Add("N010_V_DESC",    N010_V_DESC);
            req.Parameters.Add("N011_V_CATEGORY",N011_V_CATEGORY);
            req.Parameters.Add("N012_X_CCNUM",   N012_X_CCNUM);
            req.Parameters.Add("N013_X_RAUTH",   N013_X_RAUTH);
            req.Parameters.Add("N014_P_IDTYPE",  N014_P_IDTYPE);
            req.Parameters.Add("N015_P_IDNUM",   N015_P_IDNUM);
            req.Parameters.Add("N016_P_NAME",    N016_P_NAME);
            req.Parameters.Add("N017_P_INITIAL", N017_P_INITIAL);
            req.Parameters.Add("N018_P_NOSO",    N018_P_NOSO);
            req.Parameters.Add("N019_P_ADDR1",   N019_P_ADDR1);
            req.Parameters.Add("N020_P_ADDR2",   N020_P_ADDR2);
            req.Parameters.Add("N021_P_ADDR3",   N021_P_ADDR3);
            req.Parameters.Add("N022_P_ADDR4",   N022_P_ADDR4);
            req.Parameters.Add("N023_P_ADDR5",   N023_P_ADDR5);
            req.Parameters.Add("N024_P_ACODE",   N024_P_ACODE);
            req.Parameters.Add("N025_P_STRT1",   N025_P_STRT1);
            req.Parameters.Add("N026_P_STRT2",   N026_P_STRT2);
            req.Parameters.Add("N027_P_STRT3",   N027_P_STRT3);
            req.Parameters.Add("N028_P_STRT4",   N028_P_STRT4);
            req.Parameters.Add("N029_P_NATURE",  N029_P_NATURE);
            req.Parameters.Add("N030_P_SCODE",   N030_P_SCODE);
            req.Parameters.Add("N031_PX_IDTYPE", N031_PX_IDTYPE);
            req.Parameters.Add("N032_PX_NAME",   N032_PX_NAME);
            req.Parameters.Add("N033_PX_INITIAL",N033_PX_INITIAL);
            req.Parameters.Add("N034_V_MAKE",    N034_V_MAKE);
            req.Parameters.Add("N035_V_MODEL",   N035_V_MODEL);
            req.Parameters.Add("N036_X_PCCNUM",  N036_X_PCCNUM);
            req.Parameters.Add("N037_P_CDATE",   N037_P_CDATE);
            req.Parameters.Add("N041_V_TYPE",    N041_V_TYPE);
            req.Parameters.Add("N042_V_USAGE",   N042_V_USAGE);
            req.Parameters.Add("N043_V_COLOR",   N043_V_COLOR);
            req.Parameters.Add("N053_PX_IDNUM",  N053_PX_IDNUM);
            req.Parameters.Add("N061_V_REGNUM",  N061_V_REGNUM);
            req.Parameters.Add("N062_D_LICCODE", N062_D_LICCODE);
            req.Parameters.Add("N063_D_LICISSUE",N063_D_LICISSUE);
            req.Parameters.Add("N064_D_AGE",     N064_D_AGE);
            req.Parameters.Add("N065_TX_NUM",    N065_TX_NUM);
            req.Parameters.Add("N066_X_VIN",     N066_X_VIN);
            req.Parameters.Add("N067_V_ENGIN",   N067_V_ENGIN);

            //DB Query
            TTOPSReply reply = natisDown.UpdateByNatisInfo(req, Chk_SameVehicle.Checked, Chk_SequenceNum.Checked, Chk_TRANID.Checked, Chk_WaitingDownStatus.Checked);
            
            if (reply.ResultCode.Equals("OK"))
            {
                log = string.Format("success Import Notice = {0}, TranID = {1}", N065_TX_NUM, TranID);
                Trace(log);


                // Natis 업데이트 성공하는 시점에 NoticeNumber 와 easyPay 번호 생성
                string sNumber = string.Empty; 
                string sEasyNum = string.Empty;
                int nChk = 0;

                //NoticeNumber 생성
                sNumber = GetNoticeNumberSetNext();

                int nEasyPayID = Convert.ToInt32(EasyPayID);

                // mfc dll 호출
                nChk = GetEasyPayNumberCheckDigit(nEasyPayID, nA, nB, nC);

                sEasyNum = string.Format("9{0}{1:00}{2:00000}{3:000}{4}", EasyPayID, nA, nB, nC, nChk);
                //sEasyNum.Format("9%s%02d%05d%03d%d",
                //    m_sEasyPayID, nA, nB, nC, nChk);

                UpdateNoticeNumberState(sNumber, sEasyNum, nA, nB, nC, nD, ITOPS_State.ITOPS_STATE_NATIS_DOWN, carnum, TranID, N065_TX_NUM);

                iSucessNatice++;
            }
            else
            {
                log = string.Format("[line {0}, {1}] fail to update the Natis information (tx_num = {2}, TranID = {3})", nLine, carnum, N065_TX_NUM, TranID);
                Trace(log, true);
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
        private void Trace(string message, bool bwrite = false)
        {

            string curTime = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            Edt_Log.AppendText(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " : " + message + Environment.NewLine);

            if(bwrite)
            {
                using (StreamWriter outputFile = new StreamWriter(sCurDownFile + ".log", true))
                {
                    outputFile.WriteLine(message);
                }

            }


            this.ActiveControl = Edt_Log;
            Edt_Log.Focus();
            Edt_Log.Update();
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

        private bool UpdateNoticeNumberState(string sNumber, string sEasyPayNumber, int nA, int nB, int nC, int nD, int nstat, string carnum, string TranID, string N065_TX_NUM)
        {

            NatisBiz natisBiz = new NatisBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("NOTICE_NUM", sNumber);
            req.Parameters.Add("PAY_ACCOUNT", sEasyPayNumber);
            req.Parameters.Add("NOTICE_NUM_A", nA);
            req.Parameters.Add("NOTICE_NUM_B", nB);
            req.Parameters.Add("NOTICE_NUM_C", nC);
            req.Parameters.Add("NOTICE_NUM_D", nD);
            req.Parameters.Add("CARNUM", carnum);
            req.Parameters.Add("N000_TRANID", TranID);

            //DB Query
            TTOPSReply reply = natisBiz.UpdateNoticeNumberState(req, Chk_SameVehicle.Checked, Chk_SequenceNum.Checked, Chk_TRANID.Checked, Chk_WaitingDownStatus.Checked, N065_TX_NUM);

            return true;
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

        /*
        private DataSet SelectCaseDown(ArrayList aryNatis, string TranID)
        {
            string carnum = string.Empty;
            string noticenum = string.Empty;

            foreach (NatisVal na in aryNatis)
            {
                switch (na.index)
                {
                    case 3:
                        carnum = na.Val;
                        break;
                    case 65:
                        noticenum = na.Val;
                        break;
                }
            }


           NatisBiz natisDown = new NatisBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CARNUM", carnum);
            req.Parameters.Add("NOTICE_NUM", noticenum);
            req.Parameters.Add("N000_TRANID", TranID);

            //DB Query
            TTOPSReply reply = natisDown.SelectCaseDown(req, Chk_SameVehicle.Checked, Chk_TRANID.Checked, Chk_WaitingDownStatus.Checked);

            return reply.ResultSet;
        }
        */

        private DataSet SelectEasyPayID()
        {
            NumberingBiz numberingBiz = new NumberingBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = numberingBiz.SelectEasyPayID(req);

            return reply.ResultSet;

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
        #endregion

    }
}
