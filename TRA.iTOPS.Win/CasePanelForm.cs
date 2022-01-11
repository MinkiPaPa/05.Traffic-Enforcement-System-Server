using TRA.iTOPS.Windows.Set.MainApp;
using TRA.iTOPS.Contracts.Common;
using TRA.iTOPS.Contracts.Session;
using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.IO;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Infragistics.Win;
using Infragistics.Win.UltraWinToolTip;
using Infragistics.Win.UltraWinListView;


namespace TRA.iTOPS.Win
{

    public partial class CasePanelForm : FormBase
    {
        private string _strCuid = string.Empty;
        private DataRow CaseDr = null;

        private string curImgFile = string.Empty;

        string[] RepresentDesc =
        {
            "reduce fine into",
            "cancel the case",
            "refuse represent",
            "change owner info"
        };

        public List<ReportCase> FReportCase;

        public class ReportCase
        {
            public string OffenceDate;
            public string Court;
            public string LocationCode;
            public string SpeedLimit;
            public string YourSpeed;
            public string CameraID;
            public string OfficerID;

        }


        public string StrCuid
        {
            get { return _strCuid; }
            set { _strCuid = value; }
        }

        [DllImport("TRA.iTOP.MFCDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int PrintCaseDetail(ref ITOPS_State.CasePrint PayPrint);


        public CasePanelForm()
        {
            InitializeComponent();
        }


        #region #EVENT
        /// <summary>
        /// 환경 설정 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CasePanelForm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("notice:" + _strNoticeNumber + "    cuid:" + _strCuid);
            ListInitCol();

            LoadCaseInfo();

            LoadToolTip();
        }


        private void Btn_NotiReprint_Click(object sender, EventArgs e)
        {
            EachCase.NoticeRePrintForm frm = new EachCase.NoticeRePrintForm()
            {
                CaseDr = CaseDr
            };

            frm.ShowDialog();

        }

        private void Btn_PrinterDetail_Click(object sender, EventArgs e)
        {
            //CaseReportDetail();
            PrintDetail();
        }

        private void Btn_EditRepresen_Click(object sender, EventArgs e)
        {
            EachCase.RepresentationForm frm = new EachCase.RepresentationForm
            {
                CaseDr = CaseDr
            };

            frm.ShowDialog();

            if (frm.PersonUpdateState == true)
            {
                ShowChange();
                ShowPersonChange();

                CaseDr = frm.CaseDr;
            }

            if(frm.RepresentUpdateState == true)
            {
                ShowRepresent();

                CaseDr = frm.CaseDr;
            }


        }

        private void Btn_EditPay_Click(object sender, EventArgs e)
        {
            EachCase.PayForm frm = new EachCase.PayForm
            {
                CaseDr = CaseDr
            };

            frm.ShowDialog();

            if (frm.UpdateState == true)
            {
                ShowChange();
                CaseDr = frm.CaseDr;
            }

        }

        private void Btn_Court_Click(object sender, EventArgs e)
        {
            EachCase.CourtDecisionForm frm = new EachCase.CourtDecisionForm
            {
                CaseDr = CaseDr
            };

            frm.ShowDialog();

            if (frm.UpdateState == true)
            {
                ShowChange();
                CaseDr = frm.CaseDr;
            }
                
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lv_Represent_DoubleClick(object sender, EventArgs e)
        {
            EachCase.RepresentationForm frm = new EachCase.RepresentationForm
            {
                CaseDr = CaseDr
            };

            frm.ShowDialog();

            if (frm.PersonUpdateState == true)
            {
                ShowChange();
                ShowPersonChange();

                CaseDr = frm.CaseDr;
            }

            if (frm.RepresentUpdateState == true)
            {
                ShowRepresent();

                CaseDr = frm.CaseDr;
            }

        }

        private void Lv_Pay_DoubleClick(object sender, EventArgs e)
        {
            EachCase.PayForm frm = new EachCase.PayForm
            {
                CaseDr = CaseDr
            };

            frm.ShowDialog();

            if (frm.UpdateState == true)
                ShowChange();

        }

        private void Lv_Print_DoubleClick(object sender, EventArgs e)
        {
            EachCase.CourtDecisionForm frm = new EachCase.CourtDecisionForm
            {
                CaseDr = CaseDr
            };

            frm.ShowDialog();

            if (frm.UpdateState == true)
                ShowChange();

        }


        private void Pb_car_DoubleClick(object sender, EventArgs e)
        {
            EachCase.ImageViewForm frm = new EachCase.ImageViewForm()
            {
                CurImgFile = curImgFile
            };

            frm.ShowDialog();
        }



        #endregion

        #region # METHOD
        private void LoadToolTip()
        {
            UltraToolTipInfo toolTipInfo = new UltraToolTipInfo("Double Click to Capture Pay Status", ToolTipImage.Info, "Pay Info", DefaultableBoolean.True);
            toolTipInfo.Appearance.BackColor = Color.White;
            toolTipInfo.Appearance.BackColor2 = Color.Chartreuse;
            toolTipInfo.Appearance.BackGradientStyle = GradientStyle.Circular;
            toolTipInfo.Appearance.ForeColor = Color.Black;
            this.ultraToolTipManager1.SetUltraToolTip(Lv_Pay, toolTipInfo);

            UltraToolTipInfo toolTipInfo2 = new UltraToolTipInfo("Double Click to Capture Represention", ToolTipImage.Info, "Represention Info", DefaultableBoolean.True);
            toolTipInfo2.Appearance.BackColor = Color.White;
            toolTipInfo2.Appearance.BackColor2 = Color.Chartreuse;
            toolTipInfo2.Appearance.BackGradientStyle = GradientStyle.Circular;
            toolTipInfo2.Appearance.ForeColor = Color.DarkBlue;
            this.ultraToolTipManager1.SetUltraToolTip(Lv_Represent, toolTipInfo2);


            UltraToolTipInfo toolTipInfo3 = new UltraToolTipInfo("Double Click to Capture Court Decison", ToolTipImage.Info, "Court Info", DefaultableBoolean.True);
            toolTipInfo3.Appearance.BackColor = Color.White;
            toolTipInfo3.Appearance.BackColor2 = Color.Chartreuse;
            toolTipInfo3.Appearance.BackGradientStyle = GradientStyle.Circular;
            toolTipInfo3.Appearance.ForeColor = Color.Brown;

            this.ultraToolTipManager1.SetUltraToolTip(Lv_Print, toolTipInfo3);

            UltraToolTipInfo toolTipInfo4 = new UltraToolTipInfo("Double Click to Car Image Zoom In/Out", ToolTipImage.Info, "Car View", DefaultableBoolean.True);
            toolTipInfo4.Appearance.BackColor = Color.White;
            toolTipInfo4.Appearance.BackColor2 = Color.Chartreuse;
            toolTipInfo4.Appearance.BackGradientStyle = GradientStyle.Circular;
            toolTipInfo4.Appearance.ForeColor = Color.DarkViolet;

            this.ultraToolTipManager1.SetUltraToolTip(Pb_car, toolTipInfo4);

        }

        private void ShowPersonChange()
        {
            DataSet ds = SelectPersonChangeInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Lv_owner.Items.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ShowOwner(dr);
                }

            }

        }

        private DataSet SelectPersonChangeInfo()
        {
            SearchBiz sBiz = new SearchBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CUID", CaseDr["CUID"]);

            //DB Query
            TTOPSReply reply = sBiz.SelectPersonInfo(req);

            return reply.ResultSet;
        }


        private void ListInitCol()
        {
            Lv_Offence.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_Offence.ViewSettingsDetails.FullRowSelect = true;

            Lv_Capture.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_Capture.ViewSettingsDetails.FullRowSelect = true;

            Lv_Represent.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_Represent.ViewSettingsDetails.FullRowSelect = true;

            Lv_Pay.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_Pay.ViewSettingsDetails.FullRowSelect = true;

            Lv_Print.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_Print.ViewSettingsDetails.FullRowSelect = true;

            Lv_owner.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_owner.ViewSettingsDetails.FullRowSelect = true;

            Lv_Change.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_Change.ViewSettingsDetails.FullRowSelect = true;

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


        private void LoadCaseInfo()
        {
            DataSet CaseDs = SelectCaseInfo();

            if (CaseDs.Tables[0].Rows.Count > 0)
            {
                CaseDr = CaseDs.Tables[0].Rows[0];

                ShowOffence(CaseDr);
                ShowCapture(CaseDr);
                ShowPrint(CaseDr);
                
                ShowPay(CaseDr);
                ShowOwner(CaseDr);

                ShowRepresent();
                ShowChange();

                //string strFtpFilePath = CaseDr["file_directory"].ToString() +"\\"+ CaseDr["file_name"].ToString();

                LoadImage(CaseDr["FILE_DIRECTORY"].ToString(), CaseDr["FILE_NAME"].ToString());
            }
        }

        private void LoadImage(string filePath, string filename)
        {
            string severFile = filePath + "\\" + filename;
            curImgFile = Directory.GetCurrentDirectory() + @"\CarImage\" + filePath + filename;

            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(curImgFile));
            if (di.Exists == false)
            {
                di.Create();
            }

            // 로컬에 있으면 그거 보여주고, 없으면 ftp 로 땡겨와서 보여줌
            if(!File.Exists(curImgFile))
            {
                string strConnectionStrings = ConfigurationManager.ConnectionStrings["iTOPS_FTP"].ConnectionString;

                // FTP userID 와 password 는 하드코딩으로 아이피만 기록
                //string ftpPath = "ftp://" + strConnectionStrings + "/" + severFile;
                //if (DownloadFTPFile(ftpPath, curImgFile, "trapeace", "@mbcisno1"))

                string[] ftpConn = strConnectionStrings.Split(';');
                string ftpPath = "ftp://" + ftpConn[0] + "/" + severFile;
                // 패스워드 복호화 추가(AES128의 key는 16자리)
                string DecPw = AESDecrypt128(ftpConn[2], "trapeace_ambc!za");

                if (DownloadFTPFile(ftpPath, curImgFile, ftpConn[1], DecPw))
                {
                    try
                    {
                        Pb_car.Load(curImgFile);
                        Pb_car.SizeMode = PictureBoxSizeMode.StretchImage;
                    }catch(Exception ex)
                    {
                        MessageBox.Show("LoadImage:" + ex.Message.ToString());
                    }
                }
            }else
            {
                try
                {
                    Pb_car.Load(curImgFile);
                    Pb_car.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("LoadImage:" + ex.Message.ToString());
                }
            }

        }

        #region 암복호화
        //AES128 암호화
        private String AESEncrypt128(String Input, String key)
        {

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(Input);
            byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }

        //AES128 복호화
        private String AESDecrypt128(String Input, String key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(Input);
            byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            return DecryptedData;
        }
        #endregion


        #region FTP 파일 다운로드하기 - DownloadFTPFile(sourceFileURI, targetFilePath, userID, password)

        /// <summary>
        /// FTP 파일 다운로드하기
        /// </summary>
        /// <param name="sourceFileURI">소스 파일 URI</param>
        /// <param name="targetFilePath">타겟 파일 경로</param>
        /// <param name="userID">사용자 ID</param>
        /// <param name="password">패스워드</param>
        /// <returns>처리 결과</returns>
        public bool DownloadFTPFile(string sourceFileURI, string targetFilePath, string userID, string password)
        {
            try
            {
                Uri sourceFileUri = new Uri(sourceFileURI);

                FtpWebRequest ftpWebRequest = WebRequest.Create(sourceFileUri) as FtpWebRequest;

                ftpWebRequest.Credentials = new NetworkCredential(userID, password);

                ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse ftpWebResponse = ftpWebRequest.GetResponse() as FtpWebResponse;

                Stream sourceStream = ftpWebResponse.GetResponseStream();
                FileStream targetFileStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write);

                byte[] bufferByteArray = new byte[1024];

                while (true)
                {
                    int byteCount = sourceStream.Read(bufferByteArray, 0, bufferByteArray.Length);

                    if (byteCount == 0)
                    {
                        break;
                    }

                    targetFileStream.Write(bufferByteArray, 0, byteCount);
                }

                targetFileStream.Close();

                sourceStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }

            return true;
        }

        #endregion


        private void ShowOwner(DataRow dr)
        {
            UltraListViewItem item;
            item = new UltraListViewItem("ID", new Object[] { dr["N015_P_IDNUM"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Name", new Object[] { dr["N016_P_NAME"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Addr1", new Object[] { dr["N019_P_ADDR1"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Addr2", new Object[] { dr["N020_P_ADDR2"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Addr3", new Object[] { dr["N021_P_ADDR3"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Postal 1", new Object[] { dr["N025_P_STRT1"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Postal 2", new Object[] { dr["N026_P_STRT2"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Postal 3", new Object[] { dr["N027_P_STRT3"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Postal Code", new Object[] { dr["N030_P_SCODE"].ToString() });
            Lv_owner.Items.Add(item);

            item = new UltraListViewItem("Addr Change Date", new Object[] { dr["N037_P_CDATE"].ToString() });
            Lv_owner.Items.Add(item);

        }


        private void ShowRepresent()
        {
            DataSet ds = SelectRepresentHistoryInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                UltraListViewItem item;
                string sAction = string.Empty;
                string sMethod = string.Empty;

                Lv_Represent.Items.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (Convert.ToInt32(dr["DECISION_IS"]) == 0)
                        sAction = string.Format("{0} ({1})", RepresentDesc[Convert.ToInt32(dr["DECISION_IS"])], Convert.ToInt32(dr["FINE_REDUCED"]));
                    else
                        sAction = string.Format("{0}", RepresentDesc[Convert.ToInt32(dr["DECISION_IS"])]);

                    item = new UltraListViewItem(ConvertDBDay(dr["DECISION_WHEN"].ToString()), new Object[] { sAction});
                    Lv_Represent.Items.Add(item);

                }

            }

        }

        private void ShowChange()
        {
            DataSet ds = SelectChangeHistoryInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Lv_Change.Items.Clear();

                UltraListViewItem item;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new UltraListViewItem(dr["SUBJECT"].ToString(), new Object[] { dr["BEFORE"].ToString(), dr["AFTER"].ToString(), dr["OPERATOR"].ToString(), ITOPS_State.ConvertDBDate(dr["CTIME"].ToString()) });
                    Lv_Change.Items.Add(item);

                }

            }

        }

        private DataSet SelectCaseInfo()
        {
            //DataSet DS = new DataSet();
            SearchBiz SearchBiz = new SearchBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CUID", _strCuid);

            //DB Query
            TTOPSReply reply = SearchBiz.SelectCaseInfo(req);

            return reply.ResultSet;
        }

        private DataSet SelectRepresentHistoryInfo()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CUID", CaseDr["CUID"]);

            //DB Query
            TTOPSReply reply = eachBiz.SelectHistoryRepresentCase(req);

            return reply.ResultSet;
        }
        private DataSet SelectChangeHistoryInfo()
        {
            EachCaseBiz eachBiz = new EachCaseBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            req.Parameters.Add("CUID", CaseDr["CUID"]);

            //DB Query
            TTOPSReply reply = eachBiz.SelectHistoryChangeCase(req);

            return reply.ResultSet;
        }

        private void ShowPrint(DataRow dr)
        {
            UltraListViewItem item;
            item = new UltraListViewItem("1st Notice", new Object[] { ITOPS_State.ConvertDBDate(dr["LAST_341"].ToString()) });
            Lv_Print.Items.Add(item);

            item = new UltraListViewItem("NBS", new Object[] { ITOPS_State.ConvertDBDate(dr["LAST_NBS"].ToString()) });
            Lv_Print.Items.Add(item);

            item = new UltraListViewItem("Summon", new Object[] { ITOPS_State.ConvertDBDate(dr["LAST_SUMMON"].ToString()) });
            Lv_Print.Items.Add(item);

            item = new UltraListViewItem("Court", new Object[] { ITOPS_State.ConvertDBDate(dr["LAST_COURT"].ToString()) });
            Lv_Print.Items.Add(item);

            item = new UltraListViewItem("Represent", new Object[] { ITOPS_State.ConvertDBDate(dr["LAST_REPRESENT"].ToString()) });
            Lv_Print.Items.Add(item);

        }

        private void ShowPay(DataRow dr)
        {
            UltraListViewItem item;
            item = new UltraListViewItem("Pay", new Object[] { string.Format("R {0}.00", Convert.ToInt32(dr["PAYED"])) });
            Lv_Pay.Items.Add(item);

            item = new UltraListViewItem("Due Date", new Object[] { ITOPS_State.ConvertDBDate(dr["PAY_DUEDT"].ToString()) });
            Lv_Pay.Items.Add(item);

            item = new UltraListViewItem("Receipt#", new Object[] { dr["PAYED_RECEIPT"].ToString() });
            Lv_Pay.Items.Add(item);

            item = new UltraListViewItem("Pay Point", new Object[] { dr["PAY_POINT"].ToString() });
            Lv_Pay.Items.Add(item);

            item = new UltraListViewItem("Comment", new Object[] { dr["PAY_INFO"].ToString() });
            Lv_Pay.Items.Add(item);

            item = new UltraListViewItem("Date", new Object[] { ConvertDBDay(dr["PAY_TIME"].ToString()) });
            Lv_Pay.Items.Add(item);

        }

        private void ShowCapture(DataRow dr)
        {
            UltraListViewItem item;
            item = new UltraListViewItem("Car number", new Object[] { dr["CARNUM"].ToString() });
            Lv_Capture.Items.Add(item);

            item = new UltraListViewItem("Car make", new Object[] { dr["CARMAKE"].ToString() });
            Lv_Capture.Items.Add(item);

            item = new UltraListViewItem("Car type", new Object[] { dr["CARTYPE"].ToString() });
            Lv_Capture.Items.Add(item);

            item = new UltraListViewItem("Notice num", new Object[] { dr["NOTICE_NUM"].ToString() });
            Lv_Capture.Items.Add(item);

            item = new UltraListViewItem("EasyPay num", new Object[] { dr["PAY_ACCOUNT"].ToString() });
            Lv_Capture.Items.Add(item);

            item = new UltraListViewItem("Date", new Object[] { ITOPS_State.ConvertDBDate(dr["WHEN_DT"].ToString()) });
            Lv_Capture.Items.Add(item);

        }

        private void ShowOffence(DataRow dr)
        {
            UltraListViewItem item;
            item = new UltraListViewItem("Speed Limit", new Object[] { dr["SPEED_REGAL"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("Speed Actual", new Object[] { dr["SPEED_IS"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("Court", new Object[] { dr["COURT"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("Location", new Object[] { dr["LOCATION"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("DISTANCE", new Object[] { dr["DISTANCE"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("Officer", new Object[] { dr["OFFICER"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("Camera", new Object[] { dr["DEVICE_SN"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("Fine (R)", new Object[] { dr["FINE"].ToString() });
            Lv_Offence.Items.Add(item);

            item = new UltraListViewItem("Date", new Object[] { ITOPS_State.ConvertDBDate(dr["WHEN_DT"].ToString()) });
            Lv_Offence.Items.Add(item);

        }
        
        //private void CaseReportDetail()
        //{
        //    FastReport.Report report = new FastReport.Report();

        //    //CreateBusinessObject();
        //    // register the business object
        //    report.RegisterData(FReportCase, "Case");

        //    // design the report
        //    report.Design();

        //    // free resources used by report
        //    report.Dispose();

        //    /*
        //    report.Load("report.frx");

        //    //report.RegisterData(CaseDr, "Array");

        //    // run the report
        //    report.Show();

        //    // free resources used by report
        //    report.Dispose();
        //    */
        //}


        private void PrintDetail()
        {
            ITOPS_State.CasePrint np = new ITOPS_State.CasePrint();

            np.NaP_NAME = CaseDr["N016_P_NAME"].ToString();
            np.NaP_INITIAL = CaseDr["N017_P_INITIAL"].ToString();
            np.NaX_NAME = CaseDr["N032_PX_NAME"].ToString();
            np.NaP_ADDR1 = CaseDr["N019_P_ADDR1"].ToString();
            np.NaP_ADDR2 = CaseDr["N020_P_ADDR2"].ToString();
            np.NaP_ADDR3 = CaseDr["N021_P_ADDR3"].ToString();
            np.NaP_ADDR4 = CaseDr["N022_P_ADDR4"].ToString();
            np.NaP_ADDR5 = CaseDr["N023_P_ADDR5"].ToString();
            np.NaP_ACODE = CaseDr["N032_PX_NAME"].ToString();

            np.k_NoticeNum = CaseDr["NOTICE_NUM"].ToString();
            np.p9_CarNum = CaseDr["CARNUM"].ToString();
            np.p2_WhenDT = ITOPS_State.ConvertDBDate(CaseDr["WHEN_DT"].ToString());

            np.p1_Street = CaseDr["STREET"].ToString();
            np.p1_Court = CaseDr["COURT"].ToString();
            np.p1_Location = CaseDr["LOCATION"].ToString();
            np.p1_Direction = CaseDr["DIRECTION"].ToString();
            np.p1_SpeedLaw = CaseDr["SPEED_REGAL"].ToString();
            np.p2_SpeedIs = CaseDr["SPEED_IS"].ToString();
            np.p3_OffenceCode = CaseDr["OFFENCE_CODE"].ToString();
            np.p1_Officer = CaseDr["OFFICER"].ToString();
            //np.p2_File1 = CaseDr["FILE_NAME"].ToString();
            np.p2_File1 = Directory.GetCurrentDirectory() + @"\CarImage\" + CaseDr["FILE_DIRECTORY"].ToString() + CaseDr["FILE_NAME"].ToString(); ;

            np.k_PayDueDate = ITOPS_State.ConvertDBDate(CaseDr["PAY_DUEDT"].ToString());
            np.p3_Fine = CaseDr["FINE"].ToString();
            np.k_Last341 = ITOPS_State.ConvertDBDate(CaseDr["LAST_341"].ToString());
            np.k_LastNBS = ITOPS_State.ConvertDBDate(CaseDr["LAST_NBS"].ToString());
            np.k_LastSummon = ITOPS_State.ConvertDBDate(CaseDr["LAST_SUMMON"].ToString());

            np.k_PayBillNum = CaseDr["PAYED_RECEIPT"].ToString();
            np.k_PayPoint = CaseDr["PAY_POINT"].ToString();
            np.k_PayTime = ITOPS_State.ConvertDBDate(CaseDr["PAY_TIME"].ToString());
            np.k_PayerPhone = CaseDr["PAYER_PHONE"].ToString();
            np.k_PayerName = CaseDr["PAYER_NAME"].ToString();
            np.k_PayCasher = CaseDr["PAY_CASHER"].ToString();

            np.NaP_IDNUM = CaseDr["N015_P_IDNUM"].ToString();
            np.p1_Device = CaseDr["DEVICE_SN"].ToString();
            np.p2_Distance = CaseDr["DISTANCE"].ToString();

            np.k_PayType = Convert.ToInt32(CaseDr["PAYED_TYPE"]);
            np.k_Payed = Convert.ToInt32(CaseDr["PAYED"]);
            np.k_Fine2 = Convert.ToInt32(CaseDr["FINE2"]);

            PrintCaseDetail(ref np);
        }

        #endregion

    }
}
