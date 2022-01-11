using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Infragistics.Win.UltraWinGrid;

namespace TRA.iTOPS.Win.DataPrint
{
    public partial class PrintDataOptionForm : Form
    {

        private List<UltraGridRow> _checkList = null;
        bool _bDBUpdate = false;
        private bool _bUpdated = false;


        private string TiniFile = Directory.GetCurrentDirectory() + "\\TPrint.ini";

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct MyDataList
        {
            //public int Len;
            //public IntPtr List; // MyData pointer list (MyData **data_list)
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            //public ITOPS_State.CasePrint[] Printdata;
            //public MyData[] Printdata;

            public int Len;
            public IntPtr List; // MyData pointer list (MyData **data_list)

        }

        [DllImport("TRA.iTOP.MFCDLL.dll",  CallingConvention = CallingConvention.Cdecl)]
        public extern static void IssuePrint_Data_list(ref MyDataList list, int nPage, bool boptPageNumber, string TiniFile, string sTitle, bool boptPicture, bool boptPrePaper, bool boptDBUpdate, string sToday, string sDueDate);

        //[DllImport("TRA.iTOP.MFCDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        //extern public static int IssuePrints_Data(ref ITOPS_State.CasePrint NotiPrint, int nPage,int nTotalPage, string TiniFile, string sTitle, bool boptPicture, bool boptPrePaper, bool boptDBUpdate, string sToday, string sDueDate);


        public List<UltraGridRow> CheckList
        {
            get { return _checkList; }
            set { _checkList = value; }
        }

        public bool bDBUpdate
        {
            get { return _bDBUpdate; }
            set { _bDBUpdate = value; }
        }


        public bool UpdateState
        {
            get { return this._bUpdated; }   // 여기서 얻은 값을 다른폼으로 전달목적
            set { this._bUpdated = value; }  // 다른폼에서 전달받은값 쓰기
        }


        public PrintDataOptionForm()
        {
            InitializeComponent();
        }


        private void PrintDataOptionForm_Load(object sender, EventArgs e)
        {
            Cmb_Title.Items.Add(001, "1st Notice");
            Cmb_Title.Items.Add(002, "1st Notice / Reminder");

            Cmb_Title.SelectedIndex = 0;

        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            
            if(Chk_Picture.Checked == true)
            {
                Thread t = new Thread(new ThreadStart(StartSplashForm));

                t.Start();

                DownLoadCarImages();

                t.Abort();

                /*
                PrintSplashForm frm = new PrintSplashForm()
                {
                    CheckList = _checkList
                };

                frm.ShowDialog();
                */
            }

            DoDataPrint();

            _bUpdated = true;

            //Close();
        }

        private void StartSplashForm()
        {
            Application.Run(new PrintSplashForm());
        }



        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_PreInfo_Click(object sender, EventArgs e)
        {
            PrintPreInfoForm frm = new PrintPreInfoForm();

            frm.ShowDialog();
        }


        private void DoDataPrint()
        {
            string Imagepath = Directory.GetCurrentDirectory() + @"\CarImage\";
            /*
            IntPtr ptr;
            MyData[] data = {
            new MyData() { Name = "aaa", Age = 10 },
            new MyData() { Name = "bbb", Age = 20 },
            new MyData() { Name = "ccc", Age = 30 }
            };

            MyDataList list = new MyDataList();
            list.Len = data.Length;

            // 포인터의 크기 * 배열 길이만큼 Alloc
            list.List = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * data.Length);

            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    // MyData 마샬링
                    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data[0]));
                    Marshal.StructureToPtr(data[i], ptr, false);

                    // list[포인터크기 * 배열 index] 위치에 포인터 복사
                    Marshal.StructureToPtr(ptr, (IntPtr)((int)list.List + Marshal.SizeOf<IntPtr>() * i), false);
                }

                MyLib.PrintMyDataList(ref list);
            }
            catch (Exception e)
            {
                Console.Write("Error: " + e.Message);
            }
            */

            List<string> pitems = new List<string>();

            int nPage = 0;
            decimal fineTemp;

            string sTitle = Cmb_Title.SelectedItem.DisplayText;
            bool boptPageNumber = Chk_PageNumber.Checked;
            bool boptPicture = Chk_Picture.Checked;
            bool boptPrePaper = Chk_PrintPaper.Checked;
            bool boptDBUpdate = _bDBUpdate;
            string sToday = DateTime.Now.ToString("yyyy-MM-dd");
            string sDueDate = DateTime.Now.ToString("yyyy-MM-dd");

            MyDataList list = new MyDataList();
            //ITOPS_State.CasePrint np = new ITOPS_State.CasePrint();
            ITOPS_State.CasePrint np = new ITOPS_State.CasePrint();
            IntPtr ptr = IntPtr.Zero;

            // 포인터의 크기 * 배열 길이만큼 Alloc
            list.List = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * _checkList.Count);
            list.Len = _checkList.Count;

            foreach (UltraGridRow checkRow in _checkList)
            {
                pitems.Clear();

                for (int i = 0; i < checkRow.Cells.Count; i++)
                {
                    pitems.Add(checkRow.Cells[i].Value.ToString());
                }

                np.p9_CarNum = pitems[0];
                np.k_NoticeNum = pitems[1];
                np.NaP_NAME = pitems[2];
                np.NaP_INITIAL = pitems[3];
                np.NaP_IDNUM = pitems[4];
                np.NaX_NAME = pitems[5];
                np.NaP_ADDR1 = pitems[6];
                np.NaP_ADDR2 = pitems[7];
                np.NaP_ADDR3 = pitems[8];
                np.NaP_ADDR4 = pitems[9];
                np.NaP_ADDR5 = pitems[10];
                np.p2_File1 = Imagepath + pitems[29] + pitems[11];
                np.p2_WhenDT = ITOPS_State.ConvertDBDate(pitems[12]);
                np.p1_Street = pitems[13];
                np.p1_Court = pitems[14];
                np.p1_Location = pitems[15];
                np.p1_Direction = pitems[16];

                np.p1_SpeedLaw = pitems[17];
                np.p2_SpeedIs = pitems[18];
                np.p3_OffenceCode = pitems[19];
                np.p1_Officer = pitems[20];

                np.k_PayDueDate = ITOPS_State.ConvertDBDate(pitems[21]);
                np.p3_Fine = pitems[22];

                fineTemp = Convert.ToDecimal(pitems[23]);
                np.k_Fine2 = Convert.ToInt32(fineTemp);
                //np.k_Fine2 = ToInt32.Parse(pitems[23]);

                np.k_Last341 = ITOPS_State.ConvertDBDate(pitems[24]);
                np.k_LastNBS = ITOPS_State.ConvertDBDate(pitems[25]);
                np.k_LastSummon = ITOPS_State.ConvertDBDate(pitems[26]);

                np.p1_Device = pitems[27];
                np.p2_Distance = pitems[28];

                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(np));
                Marshal.StructureToPtr(np, ptr, false);

                // list[포인터크기 * 배열 index] 위치에 포인터 복사
                Marshal.StructureToPtr(ptr, (IntPtr)((int)list.List + Marshal.SizeOf<IntPtr>() * nPage), false);


                nPage++;
            }

            IssuePrint_Data_list(ref list, nPage, boptPageNumber, TiniFile, sTitle, boptPicture, boptPrePaper, boptDBUpdate, sToday, sDueDate);
        }

        private void DownLoadCarImages()
        {
            string Imagepath = Directory.GetCurrentDirectory() + @"\CarImage\";

            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(Imagepath));
            if (di.Exists == false)
            {
                di.Create();
            }

            string strConnectionStrings = ConfigurationManager.ConnectionStrings["iTOPS_FTP"].ConnectionString;

            // FTP userID 와 password 는 하드코딩으로 아이피만 기록
            //string ftpPath = "ftp://" + strConnectionStrings + "/" + severFile;
            //if (DownloadFTPFile(ftpPath, curImgFile, "trapeace", "@mbcisno1"))

            string[] ftpConn = strConnectionStrings.Split(';');
            string ftpPath = "ftp://" + ftpConn[0] + "/";
            //  패스워드 복호화 추가(AES128의 key는 16자리)
            string DecPw = AESDecrypt128(ftpConn[2], "trapeace_ambc!za");


            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpPath);
            ftpRequest.Credentials = new NetworkCredential(ftpConn[1], DecPw);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            //FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();

            List<string> directories = new List<string>();
            foreach (UltraGridRow checkRow in _checkList)
            {
                // 11번째가 이미지 파일정보 
                // 디렉토리 정보를 29번째로 추가
                directories.Add(checkRow.Cells[29].Value.ToString() + checkRow.Cells[11].Value.ToString());

                if (Directory.Exists(Imagepath + checkRow.Cells[29].Value.ToString()) == false)
                {
                    Directory.CreateDirectory(Imagepath + checkRow.Cells[29].Value.ToString());
                }

            }

            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential(ftpConn[1], DecPw);

                for (int i = 0; i <= directories.Count - 1; i++)
                {
                    if (directories[i].Contains("."))
                    {

                        string path = ftpPath + "/" + directories[i].ToString();
                        string trnsfrpth = Directory.GetCurrentDirectory() + @"\CarImage\" + directories[i].ToString();

                        if (!File.Exists(trnsfrpth))
                        {
                            //Lbl_info.Text = directories[i].ToString();
                            ftpClient.DownloadFile(path, trnsfrpth);
                        }

                    }
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

    }
}
