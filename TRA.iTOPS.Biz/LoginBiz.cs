using System;
using System.Data;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;

namespace TRA.iTOPS.Biz
{
    public class LoginBiz : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";

        public TTOPSReply SelectUserID(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "Select * FROM USERS Where USER_ID = @USER_ID AND ALLOWED = 1";
            param.SqlCommandType = CommandType.Text;
            //param.OdbcParams = ConvertOdbcParam(request.Parameters);
            param.sqlParams = ConvertsqlParam(request.Parameters);

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }

        public TTOPSReply UpdateFailCount(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "UPDATE USERS SET FAIL_CNT = FAIL_CNT + 1 WHERE USER_ID = @USER_ID";
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

        public TTOPSReply UpdateInitFailCount(TTOPSRequest request)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "UPDATE USERS SET FAIL_CNT = 0 WHERE USER_ID = @USER_ID";
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

    }
}
