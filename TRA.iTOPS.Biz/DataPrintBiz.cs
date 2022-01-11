using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;
using System;
using System.Data;

namespace TRA.iTOPS.Biz
{
    public class DataPrintBiz : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";
        private string swhere = string.Empty;


        public TTOPSReply SelectPrintCaseInfo(TTOPSRequest req, bool bIgnore_prev, bool bReprint)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();
            swhere = string.Empty;

            int nWhichDoc = Convert.ToInt16(req.Parameters["WHICHDOC"]);
            //string pToday = DateTime.Now.ToString("yyyy-MM-dd");
            string pToday = req.Parameters["TODAY_DATE"].ToString();
            int nDiffDays = Convert.ToInt32(req.Parameters["PENDINGDAY"]);
            string pOffenceDate = req.Parameters["OFFENCEDATE"].ToString();

            int nNNA = Convert.ToInt16(req.Parameters["NNA"]);
            int nNNB_1 = Convert.ToInt16(req.Parameters["NNB1"]);
            int nNNB_2 = Convert.ToInt16(req.Parameters["NNB2"]);

            if (bIgnore_prev == false)
            {
                switch (nWhichDoc)
                {
                    case 0: // 1st Notice 
                        get_printQuery_Notice1st(bReprint);
                        break;
                    case 1: // Notice reminder
                        get_printQuery_NoticeNext(pToday, nDiffDays, bReprint);
                        break;
                    case 2: // NBS 
                        get_printQuery_NBS(pToday, nDiffDays, bReprint);
                        break;
                    case 3: // Summon 
                        get_printQuery_Summon(pToday, nDiffDays, bReprint);
                        break;
                    case 4: // WOA 
                        get_printQuery_WOA(pToday, nDiffDays, bReprint);
                        break;
                    case 5: // Court Roll 
                        get_printQuery_CourtRoll(pToday, nDiffDays, bReprint);
                        break;
                    default:
                        
                        break;
                }
            }



            string swhere_offence_date = string.Empty;
            string swhere_reprint = string.Empty;

            if(!pOffenceDate.Equals(""))
            {
                swhere_offence_date = string.Format(" {0} DateDiff(day, WHEN_DT, '{1}') = 0 ", (swhere.Equals("") ? "" : " and "), pOffenceDate);
                swhere += swhere_offence_date;
            }

            if (bReprint)
            {
                if(nNNA > 0)
                {
                    swhere_reprint = string.Format("%s (NOTICE_NUM_A = %d and NOTICE_NUM_B >= %d and  NOTICE_NUM_B <= %d) ", (swhere.Equals("") ? "" : " and "), nNNA, nNNB_1, nNNB_2);
                    swhere += swhere_reprint;
                }
            }

            string sQuery = @"SELECT CARNUM as CarNumber, NOTICE_NUM as NoticeNumber, N016_P_NAME as Owner, N017_P_INITIAL as Initial, N015_P_IDNUM as ID, N032_PX_NAME as ProxyName 
                             , N019_P_ADDR1 as Address1, N020_P_ADDR2 as Address2, N021_P_ADDR3 as Address3, N022_P_ADDR4 as Address4, N023_P_ADDR5 as Address5
                             , FILE_NAME as ImageFile, WHEN_DT as OffenceDate, STREET as Street, COURT as Court, LOCATION as Location, DIRECTION as Direction
                             , SPEED_REGAL as SpeedRegal, SPEED_IS as SpeedIs, OFFENCE_CODE as OffenceCode, OFFICER as Officer, PAY_DUEDT as PayDuedt
                             , FINE as Fine, FINE2 as Fine2, LAST_341 as Last341, LAST_NBS as LastNBS, LAST_SUMMON as LastSummon, DEVICE_SN DeviceNumber, DISTANCE as Distance
                             , FILE_DIRECTORY 
                              FROM CONTRAVENTIONS where N016_P_NAME is not NULL and ";


            sQuery += swhere;
            sQuery += " order by CUID desc";

            param.SQL = sQuery;
            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;

        }

        void get_printQuery_Notice1st(bool breprint)
        {
            swhere = string.Format(" CODE_STATUS = {0} and CODE_STATUS_AUX = 0", (breprint ? ITOPS_State.ITOPS_STATE_NOTICE : ITOPS_State.ITOPS_STATE_NATIS_DOWN));
        }

        void get_printQuery_NoticeNext(string pTheDay, int nDiffDays, bool breprint)
        {
            swhere = string.Format(" CODE_STATUS = {0} and DateDiff(day, LAST_341, '{1}') >= {2}", ITOPS_State.ITOPS_STATE_NOTICE, pTheDay, nDiffDays);
        }

        void get_printQuery_NBS(string pTheDay, int nDiffDays, bool breprint)
        {
            swhere = string.Format(" CODE_STATUS = {0} and DateDiff(day, LAST_341, '{1}') >= {2}", 
                (breprint ? ITOPS_State.ITOPS_STATE_NBS : ITOPS_State.ITOPS_STATE_NOTICE), pTheDay, nDiffDays);
        }

        void get_printQuery_Summon(string pTheDay, int nDiffDays, bool breprint)
        {
            swhere = string.Format(" (CODE_STATUS = {0} and DateDiff(day, LAST_NBS, '{1}') >= {2}) or (CODE_STATUS = 10 and CODE_STATUS_AUX > 0)", 
                (breprint ? ITOPS_State.ITOPS_STATE_SUMMON : ITOPS_State.ITOPS_STATE_NBS), pTheDay, nDiffDays);
        }

        void get_printQuery_WOA(string pTheDay, int nDiffDays, bool breprint)
        {
            swhere = string.Format(" CODE_STATUS = {0} and DateDiff(day, LAST_SUMMON, '{1}') >= {2}", 
                (breprint ? ITOPS_State.ITOPS_STATE_NOTICE : ITOPS_State.ITOPS_STATE_NATIS_DOWN), pTheDay, nDiffDays);
        }

        void get_printQuery_CourtRoll(string pTheDay, int nDiffDays, bool breprint)
        {
            swhere = string.Format(" CODE_STATUS = {0} and  DateDiff(day, LAST_WOA, '{1}') >= {2}", 
                (breprint ? ITOPS_State.ITOPS_STATE_NOTICE : ITOPS_State.ITOPS_STATE_NATIS_DOWN), pTheDay, nDiffDays);
        }


    }
}
