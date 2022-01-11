using TRA.iTOPS.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TRA.iTOPS.Win.EachCase
{
    public partial class NoticeRePrintForm : Form
    {
        private DataRow _dr = null;

        private string TiniFile  = Directory.GetCurrentDirectory() + "\\TPrint.ini";

        [DllImport("TRA.iTOP.MFCDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern public static int IssuePrint_Notices(ref  ITOPS_State.CasePrint NotiPrint,  string TiniFile, string sTitle, bool boptPicture, bool boptPrePaper, bool boptDBUpdate, string sToday,  string sDueDate);

        public DataRow CaseDr
        {
            get { return _dr; }
            set { _dr = value; }
        }

        public NoticeRePrintForm()
        {
            InitializeComponent();
        }

        #region #Event

        private void NoticeRePrintForm_Load(object sender, EventArgs e)
        {
            Get_environment();
            Txt_CarNum.Text = _dr["CARNUM"].ToString();
            Txt_NoticeNum.Text = _dr["NOTICE_NUM"].ToString();

            Txt_IniTPrint.Text = TiniFile;

            this.ActiveControl = Btn_Close;
            Btn_Close.Focus();

        }


        private void Btn_Print_Click(object sender, EventArgs e)
        {
            NoticeRePrint();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Method

        private void Get_environment()
        {
            Cmb_Title.Items.Add(1, "Notice Reprinting");
            Cmb_Title.Items.Add(2, "1st Notice");
            Cmb_Title.Items.Add(3, "Notice / Reminder");

            Cmb_Title.SelectedIndex = 0;

        }

        private void NoticeRePrint()
        {
            string sTitle = Cmb_Title.SelectedItem.DisplayText;
            bool boptPicture = Chk_Picture.Checked;
            bool boptPrePaper = Chk_PrintPaper.Checked;
            bool boptDBUpdate = Chk_DBUpdate.Checked;
            string sToday = DateTime.Now.ToString("yyyy-MM-dd");
            string sDueDate = DateTime.Now.ToString("yyyy-MM-dd");

            ITOPS_State.CasePrint np = new ITOPS_State.CasePrint();

            np.NaP_NAME = _dr["N016_P_NAME"].ToString();
            np.NaP_INITIAL = _dr["N017_P_INITIAL"].ToString();
            np.NaX_NAME = _dr["N032_PX_NAME"].ToString();
            np.NaP_ADDR1 = _dr["N019_P_ADDR1"].ToString();
            np.NaP_ADDR2 = _dr["N020_P_ADDR2"].ToString();
            np.NaP_ADDR3 = _dr["N021_P_ADDR3"].ToString();
            np.NaP_ADDR4 = _dr["N022_P_ADDR4"].ToString();
            np.NaP_ADDR5 = _dr["N023_P_ADDR5"].ToString();
            np.NaP_ACODE = _dr["N032_PX_NAME"].ToString();

            np.k_NoticeNum = _dr["NOTICE_NUM"].ToString();
            np.p9_CarNum = _dr["CARNUM"].ToString();
            np.p2_WhenDT = ITOPS_State.ConvertDBDate(_dr["WHEN_DT"].ToString());

            np.p1_Street = _dr["STREET"].ToString();
            np.p1_Court = _dr["COURT"].ToString();
            np.p1_Location = _dr["LOCATION"].ToString();
            np.p1_Direction = _dr["DIRECTION"].ToString();
            np.p1_SpeedLaw = _dr["SPEED_REGAL"].ToString();
            np.p2_SpeedIs = _dr["SPEED_IS"].ToString();
            np.p3_OffenceCode = _dr["OFFENCE_CODE"].ToString();
            np.p1_Officer = _dr["OFFICER"].ToString();

            np.p2_File1 = Directory.GetCurrentDirectory() + @"\CarImage\" + _dr["FILE_DIRECTORY"].ToString() + _dr["FILE_NAME"].ToString();

            np.k_PayDueDate = ITOPS_State.ConvertDBDate(_dr["PAY_DUEDT"].ToString());
            np.p3_Fine = _dr["FINE"].ToString();
            np.k_Last341 = ITOPS_State.ConvertDBDate(_dr["LAST_341"].ToString());
            np.k_LastNBS = ITOPS_State.ConvertDBDate(_dr["LAST_NBS"].ToString());
            np.k_LastSummon = ITOPS_State.ConvertDBDate(_dr["LAST_SUMMON"].ToString());

            np.k_PayBillNum = _dr["PAYED_RECEIPT"].ToString();
            np.k_PayPoint = _dr["PAY_POINT"].ToString();
            np.k_PayTime = ITOPS_State.ConvertDBDate(_dr["PAY_TIME"].ToString());
            np.k_PayerPhone = _dr["PAYER_PHONE"].ToString();
            np.k_PayerName = _dr["PAYER_NAME"].ToString();
            np.k_PayCasher = _dr["PAY_CASHER"].ToString();

            np.NaP_IDNUM = _dr["N015_P_IDNUM"].ToString();
            np.p1_Device = _dr["DEVICE_SN"].ToString();
            np.p2_Distance = _dr["DISTANCE"].ToString();

            np.k_PayType = Convert.ToInt32(_dr["PAYED_TYPE"]);
            np.k_Payed = Convert.ToInt32(_dr["PAYED"]);
            np.k_Fine2 = Convert.ToInt32(_dr["FINE2"]);

            IssuePrint_Notices(ref np, TiniFile, sTitle, boptPicture, boptPrePaper, boptDBUpdate, sToday, sDueDate);
        }
        #endregion
    }
}
