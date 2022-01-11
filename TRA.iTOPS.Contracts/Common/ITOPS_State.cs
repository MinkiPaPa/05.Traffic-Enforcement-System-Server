using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TRA.iTOPS.Contracts.Common
{
    public class ITOPS_State
    {
        //private string _strLoginID;              // Login ID

                                                 // 공통 상수값 선언
       public const int	ITOPS_STATE_INIT		    = 	0;
       public const int	ITOPS_STATE_NUMBER		    = 	1;
       public const int	ITOPS_CHECK_EASYPAY_UP	    = 	8;
       public const int ITOPS_CHECK_EASYPAY_DOWN    =   9;
       public const int	ITOPS_STATE_NATIS_UP	    = 	10;
       public const int	ITOPS_STATE_NATIS_DOWN	    = 	20;
       public const int	ITOPS_STATE_NATIS_ERROR	    = 	29;
       public const int	ITOPS_STATE_NOTICE		    = 	30;
       public const int	ITOPS_STATE_NBS			    = 	40;
       public const int	ITOPS_STATE_SUMMON		    = 	50;
       public const int	ITOPS_STATE_WOA			    = 	60;
       public const int	ITOPS_STATE_COURT		    = 	90;
       public const int ITOPS_STATE_CLOSED          =   99;

       public const int	ITOPS_CLOSED_PAID		    =   1;
       public const int	ITOPS_CLOSED_CANCEL		    =   2;
       public const int	ITOPS_CLOSED_COURT		    =   3;
       public const int	ITOPS_CLOSED_SYSTEM		    =   4;
       public const int ITOPS_CLOSED_NONATIS        =   5;

       public const int EASYPAY_CHANGED_DONE       =  -1;
       public const int EASYPAY_CHANGED_NONE       =  0;
       public const int EASYPAY_CHANGED_UPDATE     =  1;
       public const int EASYPAY_CHANGED_DELETE     =  2;

        public static string TheCaseStateDesc(int nState)
        {
            string StateDesc = string.Empty;

            switch (nState){
                    case ITOPS_STATE_NUMBER:
                         StateDesc = "Numbering";
                         break;
                    case ITOPS_CHECK_EASYPAY_UP:
                        StateDesc = "Easypay Up";
                        break;
                    case ITOPS_CHECK_EASYPAY_DOWN:
                        StateDesc = "Easypay Down";
                        break;
                    case ITOPS_STATE_NATIS_UP:
                        StateDesc = "made UP (NATIS)";
                        break;
                    case ITOPS_STATE_NATIS_DOWN:
                        StateDesc = "imported DOWN(NATIS)";
                        break;
                    case ITOPS_STATE_NOTICE:
                        StateDesc = "printed 1st Notice";
                        break;
                    case ITOPS_STATE_NBS:
                        StateDesc = "printed NBS";
                        break;
                    case ITOPS_STATE_SUMMON:
                        StateDesc = "printed Summon";
                        break;
                    case ITOPS_STATE_WOA:
                        StateDesc = "printed WOA";
                        break;
                    case ITOPS_STATE_COURT:
                        StateDesc = "printed Court roll";
                        break;
                    default:
                        StateDesc = "";
                        break;

            }

            return StateDesc;
        }

        public static string ConvertDBDate(string OrgDate)
        {
            if (OrgDate == null || OrgDate == "")
                return "";

            DateTime dt = new DateTime();
            DateTime.TryParse(OrgDate, null, System.Globalization.DateTimeStyles.AssumeLocal, out dt);
            string ConDt = dt.ToString("yyyy-MM-dd HH:mm:ss");

            return ConDt;
        }

        public static string UserID = string.Empty;

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct CasePrint
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string NaP_NAME;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string NaP_INITIAL;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string NaX_NAME;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string NaP_ADDR1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string NaP_ADDR2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string NaP_ADDR3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string NaP_ADDR4;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string NaP_ADDR5;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string NaP_ACODE;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_NoticeNum;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string p9_CarNum;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string p2_WhenDT;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string p1_Street;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string p1_Court;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string p1_Location;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string p1_Direction;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string p1_SpeedLaw;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string p2_SpeedIs;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string p3_OffenceCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string p1_Officer;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
            public string p2_File1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_PayDueDate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string p3_Fine;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_Last341;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_LastNBS;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_LastSummon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_LastWOA;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string k_PayBillNum;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string k_PayPoint;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_PayTime;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string k_PayerPhone;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string k_PayerName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string k_PayCasher;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string NaP_IDNUM;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string p1_Device;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string p2_Distance;

            public int k_PayType;

            public int k_Payed;

            public int k_Fine2;
        }
    }


}
