using System;
using System.Data;
using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;

namespace TRA.iTOPS.Biz
{
    public class NatisBiz : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";
        private string swhere = string.Empty;

        public TTOPSReply SelectNatisUpFilename(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();


            param.SQL = string.Format("SELECT * FROM NUMBER_NATIS");
            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply SelectCaseUp(TTOPSRequest request, int nCondition, string pToday, string pComp, int nCompDays)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            swhere = string.Empty;

            switch (nCondition)
            {
                case 0: // first up 
                    swhere = string.Format(" where CODE_STATUS = {0}", ITOPS_State.ITOPS_STATE_INIT);
                    break;
                case 1: // force 
                    swhere = string.Format(" where CODE_STATUS = {0} and DateDiff(day, LAST_NATIS_UP, '{1}') {2} {3}",
                        ITOPS_State.ITOPS_STATE_NATIS_UP,
                        pToday, pComp, nCompDays);
                    break;
                case 2: // retry
                    swhere = string.Format(" where CODE_STATUS = %d ", // " where CODE_STATUS = %d and N001_ANSWER is not NULL",
                        ITOPS_State.ITOPS_STATE_NATIS_ERROR);
                    break;
                default:
                    break;
            }

            string sQuery = "SELECT CUID, CARNUM, NOTICE_NUM, CODE_STATUS, WHEN_DT FROM CONTRAVENTIONS";

            sQuery += swhere;
            sQuery += " order by CUID;";


            param.SQL = sQuery;
            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply UpdateNatisUpFileSeq(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "UPDATE NUMBER_NATIS set FILE_SEQ = FILE_SEQ + 1";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
            {
                reply.ResultCode = "OK";
            }

            return reply;
        }
        /*
        public TTOPSReply SelectCaseDown(TTOPSRequest req, bool bUpNatisSameVehicle,  bool bUpNatisSameTranID, bool bUpNatisWaitingStatus)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            swhere = string.Empty;

            if (bUpNatisSameVehicle)
                swhere += Make_where("CARNUM", req.Parameters["CARNUM"].ToString(), 0);

            //if (bUpNatisSameNotice)
            //    swhere += Make_where("NOTICE_NUM", req.Parameters["NOTICE_NUM"].ToString(), 0);

            if (bUpNatisSameTranID)
                swhere += Make_where("N000_TRANID", req.Parameters["N000_TRANID"].ToString(), 0);

            if (bUpNatisWaitingStatus)
                swhere += Make_where("CODE_STATUS", "",  ITOPS_State.ITOPS_STATE_NATIS_UP);

            string sQuery;
            if (swhere.Equals(""))
                sQuery = string.Format("select top 1 * from CONTRAVENTIONS where CODE_STATUS = {0}", ITOPS_State.ITOPS_STATE_NATIS_UP);
            else
                sQuery = "select NOTICE_NUM from CONTRAVENTIONS where" + swhere;

            param.SQL = sQuery;
            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }
        */

        private string Make_where(string key, string value, long nop)
        {
            string sshort = string.Empty;

            if(nop < 0)
                sshort = string.Format(" ({0} = '{1}')", key, value);
            else
                sshort = string.Format(" ({0} = {1})", key, nop);

            if(!swhere.Equals(""))
                swhere = " AND ";

            swhere += sshort;

            return swhere;
        }


        public TTOPSReply UpdateByNatisInfo(TTOPSRequest req, bool bUpNatisSameVehicle, bool bUpNatisSameCuid, bool bUpNatisSameTranID, bool bUpNatisWaitingStatus)
        //public TTOPSReply UpdateByNatisInfo(TTOPSRequest req, bool bUpNatisSameVehicle, bool bUpNatisSameTranID, bool bUpNatisWaitingStatus)
        {

            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            swhere = string.Empty;

            //string NoticeNumber = MakeNoticeNumber(req.Parameters["N065_TX_NUM"].ToString());


            if (bUpNatisSameVehicle)
                swhere += Make_where("CARNUM", req.Parameters["CARNUM"].ToString(), -1);

            if (bUpNatisSameCuid)
            {
                long icuid = Convert.ToInt64(req.Parameters["N065_TX_NUM"]);
                swhere += Make_where("CUID", "", icuid);

            }

            if (bUpNatisSameTranID)
                swhere += Make_where("N000_TRANID", req.Parameters["N000_TRANID"].ToString(), -1);

            if (bUpNatisWaitingStatus)
                swhere += Make_where("CODE_STATUS", "", ITOPS_State.ITOPS_STATE_NATIS_UP);

            string curTime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET CODE_STATUS      = 20
                            , CODE_STATUS_AUX  = 0
                            , LAST_NATIS_DN    = '" + curTime + @"'
                            , N001_ANSWER      = ''
                            , N004_V_USAGE     = '" + req.Parameters["N004_V_USAGE"].ToString() + @"'
                            , N005_V_CDATE     = '" + req.Parameters["N005_V_CDATE"].ToString() + @"'
                            , N006_V_RQUAL     = '" + req.Parameters["N006_V_RQUAL"].ToString() + @"'
                            , N007_V_CDATE     = '" + req.Parameters["N007_V_CDATE"].ToString() + @"'
                            , N008_V_STATE     = '" + req.Parameters["N008_V_STATE"].ToString() + @"'
                            , N009_V_CDATE     = '" + req.Parameters["N009_V_CDATE"].ToString() + @"'
                            , N010_V_DESC      = '" + req.Parameters["N010_V_DESC"].ToString() + @"'
                            , N011_V_CATEGORY  = '" + req.Parameters["N011_V_CATEGORY"].ToString() + @"'
                            , N012_X_CCNUM     = '" + req.Parameters["N012_X_CCNUM"].ToString() + @"'
                            , N013_X_RAUTH     = '" + req.Parameters["N013_X_RAUTH"].ToString() + @"'
                            , N014_P_IDTYPE    = '" + req.Parameters["N014_P_IDTYPE"].ToString() + @"'
                            , N015_P_IDNUM     = '" + req.Parameters["N015_P_IDNUM"].ToString() + @"'
                            , N016_P_NAME      = '" + req.Parameters["N016_P_NAME"].ToString() + @"'
                            , N017_P_INITIAL   = '" + req.Parameters["N017_P_INITIAL"].ToString() + @"'
                            , N018_P_NOSO      = '" + req.Parameters["N018_P_NOSO"].ToString() + @"'
                            , N019_P_ADDR1     = '" + req.Parameters["N019_P_ADDR1"].ToString() + @"'
                            , N020_P_ADDR2     = '" + req.Parameters["N020_P_ADDR2"].ToString() + @"'
                            , N021_P_ADDR3     = '" + req.Parameters["N021_P_ADDR3"].ToString() + @"'
                            , N022_P_ADDR4     = '" + req.Parameters["N022_P_ADDR4"].ToString() + @"'
                            , N023_P_ADDR5     = '" + req.Parameters["N023_P_ADDR5"].ToString() + @"'
                            , N024_P_ACODE     = '" + req.Parameters["N024_P_ACODE"].ToString() + @"'
                            , N025_P_STRT1     = '" + req.Parameters["N025_P_STRT1"].ToString() + @"'
                            , N026_P_STRT2     = '" + req.Parameters["N026_P_STRT2"].ToString() + @"'
                            , N027_P_STRT3     = '" + req.Parameters["N027_P_STRT3"].ToString() + @"'
                            , N028_P_STRT4     = '" + req.Parameters["N028_P_STRT4"].ToString() + @"'
                            , N029_P_NATURE    = '" + req.Parameters["N029_P_NATURE"].ToString() + @"'
                            , N030_P_SCODE     = '" + req.Parameters["N030_P_SCODE"].ToString() + @"'
                            , N031_PX_IDTYPE   = '" + req.Parameters["N031_PX_IDTYPE"].ToString() + @"'
                            , N032_PX_NAME     = '" + req.Parameters["N032_PX_NAME"].ToString() + @"'
                            , N033_PX_INITIAL  = '" + req.Parameters["N033_PX_INITIAL"].ToString() + @"'
                            , N034_V_MAKE      = '" + req.Parameters["N034_V_MAKE"].ToString() + @"'
                            , N035_V_MODEL     = '" + req.Parameters["N035_V_MODEL"].ToString() + @"'
                            , N036_X_PCCNUM    = '" + req.Parameters["N036_X_PCCNUM"].ToString() + @"'
                            , N037_P_CDATE     = '" + req.Parameters["N037_P_CDATE"].ToString() + @"'
                            , N041_V_TYPE      = '" + req.Parameters["N041_V_TYPE"].ToString() + @"'
                            , N042_V_USAGE     = '" + req.Parameters["N042_V_USAGE"].ToString() + @"'
                            , N043_V_COLOR     = '" + req.Parameters["N043_V_COLOR"].ToString() + @"'
                            , N053_PX_IDNUM    = '" + req.Parameters["N053_PX_IDNUM"].ToString() + @"'
                            , N061_V_REGNUM    = '" + req.Parameters["N061_V_REGNUM"].ToString() + @"'
                            , N062_D_LICCODE   = '" + req.Parameters["N062_D_LICCODE"].ToString() + @"'
                            , N063_D_LICISSUE  = '" + req.Parameters["N063_D_LICISSUE"].ToString() + @"'
                            , N064_D_AGE       = '" + req.Parameters["N064_D_AGE"].ToString() + @"'
                            , N065_TX_NUM      = '" + req.Parameters["N065_TX_NUM"].ToString() + @"'
                            , N066_X_VIN       = '" + req.Parameters["N066_X_VIN"].ToString() + @"'
                            , N067_V_ENGIN     = '" + req.Parameters["N067_V_ENGIN"].ToString() + @"'
                           WHERE " + swhere;

            //param.SQL = "UPDATE NUMBER_NOTICE SET B_CURR = @CURR";

            param.SqlCommandType = CommandType.Text;
            //param.OdbcParams = ConvertOdbcParam(request.Parameters);
            //param.sqlParams = ConvertsqlParam(req.Parameters);

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }

        public TTOPSReply UpdateNoticeNumberState(TTOPSRequest req, bool bUpNatisSameVehicle, bool bUpNatisSameCuid, bool bUpNatisSameTranID, bool bUpNatisWaitingStatus, string tx_num)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            swhere = string.Empty;


            if (bUpNatisSameVehicle)
                swhere += Make_where("CARNUM", req.Parameters["CARNUM"].ToString(), -1);

            if (bUpNatisSameCuid)
            {
                long icuid = Convert.ToInt64(tx_num);
                swhere += Make_where("CUID", "", icuid);

            }

            if (bUpNatisSameTranID)
                swhere += Make_where("N000_TRANID", req.Parameters["N000_TRANID"].ToString(), -1);

            //if (bUpNatisWaitingStatus)
            //    swhere += Make_where("CODE_STATUS", "", ITOPS_State.ITOPS_STATE_NATIS_UP);

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET NOTICE_NUM          = '" + req.Parameters["NOTICE_NUM"].ToString() + @"'
                            , PAY_ACCOUNT         = '" + req.Parameters["PAY_ACCOUNT"].ToString() + @"'
                            , NOTICE_NUM_A         = '" + req.Parameters["NOTICE_NUM_A"].ToString() + @"'
                            , NOTICE_NUM_B       = '" + req.Parameters["NOTICE_NUM_B"].ToString() + @"'
                            , NOTICE_NUM_C = " + req.Parameters["NOTICE_NUM_C"].ToString() + @"
                            , NOTICE_NUM_D            = '" + req.Parameters["NOTICE_NUM_D"].ToString() + @"'
                       WHERE " + swhere;

            //param.SQL = "UPDATE NUMBER_NOTICE SET B_CURR = @CURR";

            param.SqlCommandType = CommandType.Text;
            //param.OdbcParams = ConvertOdbcParam(request.Parameters);
            //param.sqlParams = ConvertsqlParam(req.Parameters);

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
            {
                reply.ResultCode = "OK";
            }

            return reply;
        }


        private string MakeNoticeNumber(string txNum)
        {
            string noticenumber = string.Empty;

            noticenumber = txNum.Insert(2, "/");
            noticenumber = noticenumber.Insert(8, "/");
            noticenumber = noticenumber.Insert(12, "/");

            return noticenumber;
            
        }

    }
}
