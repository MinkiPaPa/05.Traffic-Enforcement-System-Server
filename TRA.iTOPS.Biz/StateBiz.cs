using System;
using System.Data;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts.Common;

namespace TRA.iTOPS.Biz
{
    public class StateBiz : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";

        public TTOPSReply SelectState(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"
            SELECT SUM(CASE A.CODE_STATUS
                        WHEN 0 THEN A.CNT
                        ELSE 0
                   END)  STATUS_INIT
                 , SUM(CASE A.CODE_STATUS
                        WHEN 1 THEN A.CNT
                        ELSE 0
                   END)  STATUS_NUMBER
                 , SUM(CASE A.CODE_STATUS
                        WHEN 10 THEN A.CNT
                        ELSE 0
                   END)  STATUS_NATIS_UP
                 , SUM(CASE A.CODE_STATUS
                        WHEN 20 THEN A.CNT
                        ELSE 0
                   END) STATUS_NATIS_DOWN
                 , SUM(CASE A.CODE_STATUS
                        WHEN 29 THEN A.CNT
                        ELSE 0
                   END) STATUS_NATIS_ERROR
                 , SUM(CASE A.CODE_STATUS
                        WHEN 30 THEN A.CNT
                        ELSE 0
                   END) STATUS_NOTICE
                 , SUM(CASE A.CODE_STATUS
                        WHEN 40 THEN A.CNT
                        ELSE 0
                   END) STATUS_NBS
                 , SUM(CASE A.CODE_STATUS
                        WHEN 50 THEN A.CNT
                        ELSE 0
                   END) STATUS_SUMMON
                 , SUM(CASE A.CODE_STATUS
                        WHEN 60 THEN A.CNT
                        ELSE 0
                   END) STATUS_WOA
                 , SUM(CASE A.CODE_STATUS
                        WHEN 90 THEN A.CNT
                        ELSE 0
                   END) STATUS_COURT
                 , SUM(CASE A.CODE_STATUS
                        WHEN 99 THEN CASE CLOSE_CODE1
			                              WHEN 1 THEN A.CNT
						                  ELSE        0
			                         END
			            ELSE 0
                   END) STATUS_CLOSED_PAID
                 , SUM(CASE A.CODE_STATUS
                        WHEN 99 THEN CASE CLOSE_CODE1
			                              WHEN 2 THEN A.CNT
						                  ELSE        0
			                         END
			            ELSE 0
                   END) STATUS_CLOSED_CANCEL
                 , SUM(CASE A.CODE_STATUS
                        WHEN 99 THEN CASE CLOSE_CODE1
			                              WHEN 3 THEN A.CNT
						                  ELSE        0
			                         END
			            ELSE 0
                   END) STATUS_CLOSED_COURT
                 , SUM(CASE A.CODE_STATUS
                        WHEN 99 THEN CASE CLOSE_CODE1
			                              WHEN 4 THEN A.CNT
						                  ELSE        0
			                         END
			            ELSE 0
                   END) STATUS_CLOSED_SYSTEM
                 , SUM(CASE A.CODE_STATUS
                        WHEN 99 THEN CASE CLOSE_CODE1
			                              WHEN 5 THEN A.CNT
						                  ELSE        0
			                         END
			            ELSE 0
                   END) STATUS_CLOSED_NOTATIS
              FROM (
                    SELECT CODE_STATUS, COUNT(*) CNT, CLOSE_CODE1
                      FROM CONTRAVENTIONS
                     GROUP BY CODE_STATUS, CLOSE_CODE1
                   ) A
                ";
            param.SqlCommandType = CommandType.Text;
            param.sqlParams = ConvertsqlParam(request.Parameters);

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply UpdateStatusChanged(TTOPSRequest req, int nCurr, int nNext, int nOpt, string pOptVal)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            string slastdate = string.Empty;

            switch (nNext)
            {
                case ITOPS_State.ITOPS_STATE_NATIS_UP:
                    slastdate = ", LAST_NATIS_UP = GetDate()";
                    break;
                case ITOPS_State.ITOPS_STATE_NATIS_DOWN:
                    slastdate = ", LAST_NATIS_DN = GetDate()";
                    break;
                case ITOPS_State.ITOPS_STATE_NOTICE:
                    slastdate = ", LAST_341 = GetDate()";
                    break;
                case ITOPS_State.ITOPS_STATE_NBS:
                    slastdate = ", LAST_NBS = GetDate()";
                    break;
                case ITOPS_State.ITOPS_STATE_SUMMON:
                    slastdate = ", LAST_SUMMON = GetDate()";
                    break;
                case ITOPS_State.ITOPS_STATE_WOA:
                    slastdate = ", LAST_WOA = GetDate()";
                    break;
                case ITOPS_State.ITOPS_STATE_COURT:
                    slastdate = ", LAST_COURT = GetDate()";
                    break;
                case ITOPS_State.ITOPS_STATE_CLOSED:  // CLOSED ..
                    break;
                default:
                    break;
            }


            string sOptValue = string.Empty;
            switch (nOpt)
            {
                case 1:
                    if (pOptVal != null)
                    {
                        if (pOptVal.Length <= 10)
                            sOptValue = string.Format(", PAY_DUEDT = '{0} 16:00:00'", pOptVal);
                        else
                            sOptValue = string.Format(", PAY_DUEDT = '%s'", pOptVal);
                    }
                    break;
                case 2:
                    if (pOptVal != null)
                    {
                        sOptValue = string.Format(", N000_TRANID = '{0}'", pOptVal);
                    }
                    break;
                case 0:
                default:
                    break;
            }

            string sql_Qry = string.Empty;
            sql_Qry = string.Format("UPDATE CONTRAVENTIONS set CODE_STATUS={0}, CODE_STATUS_AUX={1} {2} {3} where CUID = {4}"
                                    , nNext
                                    , (nCurr == nNext ? "CODE_STATUS_AUX + 1" : "0")
                                    , slastdate
                                    , sOptValue
                                    , Convert.ToInt64(req.Parameters["CUID"])
                                    );

            param.SQL = sql_Qry;

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
            {
                reply.ResultCode = "OK";
            }

            return reply;
        }

        public TTOPSReply SelectHistory(TTOPSRequest request, int nClassfy)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            string sWhere = string.Empty;


            if (ITOPS_State.ITOPS_STATE_NUMBER == nClassfy)
                sWhere = string.Format("where ACTION_CODE = {0} ", ITOPS_State.ITOPS_STATE_NUMBER);
            else if (ITOPS_State.ITOPS_CHECK_EASYPAY_UP == nClassfy || ITOPS_State.ITOPS_CHECK_EASYPAY_DOWN == nClassfy)
                sWhere = string.Format("where ACTION_CODE = {0} or ACTION_CODE = {1} ", ITOPS_State.ITOPS_CHECK_EASYPAY_UP, ITOPS_State.ITOPS_CHECK_EASYPAY_DOWN);
            else if (ITOPS_State.ITOPS_STATE_NATIS_UP == nClassfy || ITOPS_State.ITOPS_STATE_NATIS_DOWN == nClassfy)
                sWhere = string.Format("where ACTION_CODE = {0} or ACTION_CODE = {1} ", ITOPS_State.ITOPS_STATE_NATIS_UP, ITOPS_State.ITOPS_STATE_NATIS_DOWN);
            else if (ITOPS_State.ITOPS_STATE_NOTICE == nClassfy || ITOPS_State.ITOPS_STATE_NBS == nClassfy || ITOPS_State.ITOPS_STATE_SUMMON == nClassfy || ITOPS_State.ITOPS_STATE_WOA == nClassfy || ITOPS_State.ITOPS_STATE_COURT == nClassfy)
                sWhere = string.Format("where ACTION_CODE >= {0}", ITOPS_State.ITOPS_STATE_NOTICE);


            param.SQL = string.Format("select top 100 * from HISTORY_OPERATION {0} order by HCID desc; ", sWhere);
            param.SqlCommandType = CommandType.Text;
            param.sqlParams = ConvertsqlParam(request.Parameters);

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply InsertHistory(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"INSERT into HISTORY_OPERATION  
                        (ACTION_CODE, ACTION_CODE_AUX, COMMENT, COMMENT_AUX)
                        values 
                         (" + req.Parameters["ACTION_CODE"].ToString() + @", " + req.Parameters["ACTION_CODE_AUX"].ToString() + @", '" + req.Parameters["COMMENT"].ToString() + @"', '" + req.Parameters["COMMENT_AUX"].ToString() + @"'
                          );";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";

            return reply;
        }

    }
}
