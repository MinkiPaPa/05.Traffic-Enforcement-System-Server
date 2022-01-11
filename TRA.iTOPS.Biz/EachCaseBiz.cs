using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;

using System;
using System.Data;

namespace TRA.iTOPS.Biz
{
    public class EachCaseBiz : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";

        //private int EASYPAY_CHANGED_DONE = -1;
        //private int EASYPAY_CHANGED_NONE = 0;
        //private int EASYPAY_CHANGED_UPDATE = 1;
        //private int EASYPAY_CHANGED_DELETE = 2;

        public TTOPSReply SelectPayReceiptNumber(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "select top 1 * from NUMBER_RECEIPT order by NRID desc";
            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;

        }


        public TTOPSReply UpdatePay(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET FLAG_EASYPAY_CHANGED = " + ITOPS_State.EASYPAY_CHANGED_UPDATE + @"
                            , PAYED         = '" + req.Parameters["PAYED"].ToString() + @"'
                            , PAYED_TYPE    = '" + req.Parameters["PAYED_TYPE"].ToString() + @"'
                            , PAY_POINT     = '" + req.Parameters["PAY_POINT"].ToString() + @"'
                            , PAY_CASHER    = '" + req.Parameters["PAY_CASHER"].ToString() + @"'
                            , PAY_TIME      = '" + req.Parameters["PAY_TIME"].ToString() + @"'
                            , PAYER_NAME    = '" + req.Parameters["PAYER_NAME"].ToString() + @"'
                            , PAYER_PHONE   = '" + req.Parameters["PAYER_PHONE"].ToString() + @"'
                            , PAYER_REL     = '" + req.Parameters["PAYER_REL"].ToString() + @"'
                            , PAYED_RECEIPT = '" + req.Parameters["PAYED_RECEIPT"].ToString() + @"'
                            , CODE_STATUS   = " + ITOPS_State.ITOPS_STATE_CLOSED + @"
                            , CODE_STATUS_AUX = " + 0 + @"
                            , LAST_CLOSED   = GetDate()
                            , CLOSE_CODE1   = " + req.Parameters["CLOSE_CODE1"].ToString() + @"
                            , CLOSE_CODE2   = " + 0 + @"
                            , CLOSE_CODE3   = " + 0 + @"
                       WHERE CUID = " + req.Parameters["CUID"].ToString() + " ";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }

        public TTOPSReply UpdatePay2(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET FLAG_EASYPAY_CHANGED = " + ITOPS_State.EASYPAY_CHANGED_UPDATE + @"
                            , PAYED         = '" + req.Parameters["PAYED"].ToString() + @"'
                       WHERE CUID = " + req.Parameters["CUID"].ToString() + " ";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }

        public TTOPSReply ReceiptNumSeq(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE NUMBER_RECEIPT 
                          SET PAYSEQ = "+ req.Parameters["PAYSEQ"].ToString() + @"
                       WHERE NRID = "+ 1 +" ";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }



        public TTOPSReply UpdateStatusClose(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET FLAG_EASYPAY_CHANGED = " + ITOPS_State.EASYPAY_CHANGED_DELETE + @"
                            , CODE_STATUS         = " + req.Parameters["CODE_STATUS"].ToString() + @"
                            , CODE_STATUS_AUX     = " + 0 + @"
                            , LAST_CLOSED         = GetDate()
                            , CLOSE_CODE1         = " + req.Parameters["CLOSE_CODE1"].ToString() + @"
                            , CLOSE_CODE2         = " + req.Parameters["CLOSE_CODE2"].ToString() + @"
                       WHERE CUID = " + req.Parameters["CUID"].ToString() + " ";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }

        public TTOPSReply UpdateFine(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET FLAG_EASYPAY_CHANGED = " + ITOPS_State.EASYPAY_CHANGED_UPDATE + @"
                            , FINE         = " + req.Parameters["FINE"].ToString() + @"
                       WHERE CUID = " + req.Parameters["CUID"].ToString() + " ";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }

        public TTOPSReply UpdateFine2(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE CONTRAVENTIONS 
                          SET FLAG_EASYPAY_CHANGED = " + ITOPS_State.EASYPAY_CHANGED_UPDATE + @"
                            , FINE2         = " + req.Parameters["FINE2"].ToString() + @"
                       WHERE CUID = " + req.Parameters["CUID"].ToString() + " ";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }

        public TTOPSReply InsertHistoryChange(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"INSERT into HISTORY_CHANGE  
                        (CUID, CARNUM, NOTICE_NUM, SUBJECT, BEFORE, AFTER, OPERATOR)
                        values 
                         (" + req.Parameters["CUID"].ToString() + @", '" + req.Parameters["CARNUM"].ToString() + @"', '" + req.Parameters["NOTICE_NUM"].ToString() + @"'
                    , '" + req.Parameters["SUBJECT"].ToString() + @"', '" + req.Parameters["BEFORE"].ToString() + @"', '" + req.Parameters["AFTER"].ToString() + @"', '" + ITOPS_State.UserID + @"'
                          );";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }

        public TTOPSReply SelectHistoryRepresent(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"select top 100 * from HISTORY_REPRESENTATION 
                        where CUID = " + req.Parameters["CUID"].ToString() + @" order by HRID desc;";

            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;

        }

        public TTOPSReply SelectHistoryRepresentCase(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"select top 100 DECISION_WHEN, DECISION_IS, FINE_REDUCED from HISTORY_REPRESENTATION 
                        where CUID = " + req.Parameters["CUID"].ToString() + @" order by HRID desc;";

            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;

        }

        public TTOPSReply SelectHistoryChangeCase(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"select SUBJECT, BEFORE, AFTER, OPERATOR, CTIME from HISTORY_CHANGE 
                        where CUID = " + req.Parameters["CUID"].ToString() + @" order by HCID desc;";

            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;

        }


        public TTOPSReply InsertHistoryRepresent(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"INSERT into HISTORY_REPRESENTATION  
                        (CUID, CARNUM, NOTICE_NUM, OLD_REF_1,
                        DECISION_WHO, DECISION_WHEN, DECISION_WHERE, DECISION_IS, DECISION_DOCNUM,
                        ISSUER_NAME, ISSUER_PHONE, ISSUER_METHOD, ISSUER_CLAIM,
                        CAPTURER, FINE_REDUCED)
                        values 
                         (" + req.Parameters["CUID"].ToString() + @", '" + req.Parameters["CARNUM"].ToString() + @"', '" + req.Parameters["NOTICE_NUM"].ToString() + @"'
                    , " + req.Parameters["OLD_REF_1"].ToString() + @", '" + req.Parameters["DECISION_WHO"].ToString() + @"', '" + req.Parameters["DECISION_WHEN"].ToString() + @"', '" + req.Parameters["DECISION_WHERE"].ToString() + @"'
                    , " + req.Parameters["DECISION_IS"].ToString() + @", '" + req.Parameters["DECISION_DOCNUM"].ToString() + @"', '" + req.Parameters["ISSUER_NAME"].ToString() + @"', '" + req.Parameters["ISSUER_PHONE"].ToString() + @"'
                    , " + req.Parameters["ISSUER_METHOD"].ToString() + @", '" + req.Parameters["ISSUER_CLAIM"].ToString() + @"', '" + ITOPS_State.UserID + @"', '" + req.Parameters["FINE_REDUCED"].ToString() + @"'
                          );";

            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }
        public TTOPSReply UpdatePerson(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = @"UPDATE CONTRAVENTIONS set 
                        N015_P_IDNUM = '" + req.Parameters["N015_P_IDNUM"].ToString() + @"',   N016_P_NAME = '" + req.Parameters["N016_P_NAME"].ToString() + @"',  N017_P_INITIAL = '" + req.Parameters["N017_P_INITIAL"].ToString() + @"',
                        N053_PX_IDNUM = '" + req.Parameters["N053_PX_IDNUM"].ToString() + @"', N032_PX_NAME = '" + req.Parameters["N032_PX_NAME"].ToString() + @"', N033_PX_INITIAL = '" + req.Parameters["N033_PX_INITIAL"].ToString() + @"',
                        N019_P_ADDR1 = '" + req.Parameters["N019_P_ADDR1"].ToString() + @"', N020_P_ADDR2 = '" + req.Parameters["N020_P_ADDR2"].ToString() + @"', N021_P_ADDR3 = '" + req.Parameters["N021_P_ADDR3"].ToString() + @"',
                        N022_P_ADDR4 = '" + req.Parameters["N022_P_ADDR4"].ToString() + @"', N023_P_ADDR5 = '" + req.Parameters["N023_P_ADDR5"].ToString() + @"', N024_P_ACODE = '" + req.Parameters["N024_P_ACODE"].ToString() + @"',
                        N025_P_STRT1 = '" + req.Parameters["N025_P_STRT1"].ToString() + @"', N026_P_STRT2 = '" + req.Parameters["N026_P_STRT2"].ToString() + @"', N027_P_STRT3 = '" + req.Parameters["N027_P_STRT3"].ToString() + @"',
                        N028_P_STRT4 = '" + req.Parameters["N028_P_STRT4"].ToString() + @"', N030_P_SCODE = '" + req.Parameters["N030_P_SCODE"].ToString() + @"'
                        where CUID = " + req.Parameters["CUID"].ToString() + @"";


            param.SqlCommandType = CommandType.Text;

            int iReuslt = this.ExecuteNonQuery(param, CONNECTION_NAME);

            if (iReuslt > 0)
                reply.ResultCode = "OK";
            else
                reply.ResultCode = "";

            return reply;
        }
    }
}
