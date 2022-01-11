using System;
using System.Data;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts.Common;

namespace TRA.iTOPS.Biz
{
    public class ReportPay : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";

        
        public TTOPSReply SelectReportCase(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();


            string swhere = string.Format(" PAY_TIME >= '{0}' and PAY_TIME <= '{1}' ", req.Parameters["PS_DT"].ToString(), req.Parameters["PE_DT"].ToString());


            if (req.Parameters["OS_DT"].ToString().Length > 0)
            {
                string swhere2 = string.Format(" and WHEN_DT >= '{0}' and WHEN_DT <= '{1}' ", req.Parameters["OS_DT"].ToString(), req.Parameters["OE_DT"].ToString());

                swhere += swhere2;
            }
          
            string sQuery = string.Empty;

                sQuery =
                    @"select ROW_NUMBER() OVER(ORDER BY cuid DESC) AS ROWNUM
                            , CUID, NOTICE_NUM, CARNUM, WHEN_DT, OFFICER,
                            (case when FINE2 > 0 then CONCAT(CONVERT(INT,FINE),'(',CONVERT(INT,FINE2),')')
	                                else CONVERT(nvarchar(20),CONVERT(INT,FINE)) end)as FINE_S
                            , CONVERT(INT,PAYED) as PAYED, 
                            PAY_DUEDT,PAYED_RECEIPT, N016_P_NAME
                           from CONTRAVENTIONS";

            if (!swhere.Equals(""))
            {
                sQuery += " where ";
                sQuery += swhere;
            }

            param.SQL = sQuery;

            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;
        }


       

    }
}
