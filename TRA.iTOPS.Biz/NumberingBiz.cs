using System;
using System.Data;
using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;

namespace TRA.iTOPS.Biz
{
    public class NumberingBiz : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";

        public TTOPSReply SelectCandidate(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "select * from CONTRAVENTIONS where PAY_ACCOUNT is NULL and NOTICE_NUM is NOT NULL order by WHEN_DT";
            param.SqlCommandType = CommandType.Text;
            param.sqlParams = ConvertsqlParam(request.Parameters);

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply SelectEasyPayID(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "select RECVID, FILE_GEN from NUMBER_EASYPAY";
            param.SqlCommandType = CommandType.Text;
            param.sqlParams = ConvertsqlParam(request.Parameters);

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply SelectNoticeNumber(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "select * from NUMBER_NOTICE";
            param.SqlCommandType = CommandType.Text;
            param.sqlParams = ConvertsqlParam(request.Parameters);

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply UpdateCurr(int nCurr, TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            if(nCurr == 0)
                param.SQL = "UPDATE NUMBER_NOTICE SET A_CURR = @CURR";
            else
                param.SQL = "UPDATE NUMBER_NOTICE SET B_CURR = @CURR";

            param.SqlCommandType = CommandType.Text;
            //param.OdbcParams = ConvertOdbcParam(request.Parameters);
            param.sqlParams = ConvertsqlParam(request.Parameters);

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
            {
                reply.ResultCode = "OK";
            }

            return reply;
        }

        public TTOPSReply InsertHistoryNoticeNumIssue(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"INSERT into HISTORY_NOTICENUM  
                        (NN_A, NN_B, NN_C, NN_D,
                        REQ_NUM, REQ_KEY, REQ_WHO)
                        values 
                         ('" + req.Parameters["NN_A"].ToString() + @"', '" + req.Parameters["NN_B"].ToString() + @"', '" + req.Parameters["NN_C"].ToString() + @"', '" + req.Parameters["NN_D"].ToString() + @"',
                          '" + req.Parameters["REQ_NUM"].ToString() + @"', '" + req.Parameters["REQ_KEY"].ToString() + @"', '" + req.Parameters["REQ_WHO"].ToString() + @"');";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";

            return reply;
        }


        public TTOPSReply UpdateNoticeNumberState(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET NOTICE_NUM          = '" + req.Parameters["NOTICE_NUM"].ToString() + @"'
                            , PAY_ACCOUNT         = '" + req.Parameters["PAY_ACCOUNT"].ToString() + @"'
                            , NOTICE_NUM_A         = '" + req.Parameters["NOTICE_NUM_A"].ToString() + @"'
                            , NOTICE_NUM_B       = '" + req.Parameters["NOTICE_NUM_B"].ToString() + @"'
                            , NOTICE_NUM_C = " + req.Parameters["NOTICE_NUM_C"].ToString() + @"
                            , NOTICE_NUM_D            = '" + req.Parameters["NOTICE_NUM_D"].ToString() + @"'
                            , CODE_STATUS         = '" + req.Parameters["CODE_STATUS"].ToString() + @"'
                       WHERE CUID = " + req.Parameters["CUID"].ToString() + " ";

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

    }
}
