using TRA.iTOPS.Service.Core;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts;
using System;
using System.Data;

namespace TRA.iTOPS.Biz
{
    public class SearchBiz : BizBase
    {
        private const String CONNECTION_NAME = "iTOPS";
        private string swhere = string.Empty;
        public TTOPSReply SelectSearchCase(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();


            swhere = string.Empty;

            swhere += Make_where("CARNUM", req.Parameters["CARNUM"].ToString(), 50);
            swhere += Make_where("NOTICE_NUM", req.Parameters["NOTICE_NUM"].ToString(), 50);
            swhere += Make_where("N015_P_IDNUM", req.Parameters["N015_P_IDNUM"].ToString(), 50);
            swhere += Make_where("N016_P_NAME", req.Parameters["N016_P_NAME"].ToString(), 50);
            swhere += Make_where("OFFICER", req.Parameters["OFFICER"].ToString(), 50);

            if (req.Parameters["S_DT"].ToString().Length > 0 && req.Parameters["E_DT"].ToString().Length > 0)
            {
                swhere += Make_where("WHEN_DT", req.Parameters["S_DT"].ToString(), 2);
                swhere += Make_where("WHEN_DT", req.Parameters["E_DT"].ToString(), -2);
            }


            string sQuery = string.Empty;

            if (Convert.ToUInt32(req.Parameters["MAX"]) > 0)
                sQuery = string.Format(
                    @"select top {0} CUID, ROW_NUMBER() OVER(ORDER BY cuid DESC) AS ROWNUM
                                        ,NOTICE_NUM, CARNUM, WHEN_DT, FINE, OFFICER, N016_P_NAME
                                        , (case when CODE_STATUS = 0 then 'wait for NATIS Up'
                                              when CODE_STATUS = 10 then 'wait for NATIS Down'
                                              when CODE_STATUS = 20 then 'wait for 1st-Notice'
                                              when CODE_STATUS = 29 then 'No NATIS data'
                                              when CODE_STATUS = 30 then '1st Noticed, wait for NBS or reminder'
                                              when CODE_STATUS = 40 then 'NBS, wait for Summon'
                                              when CODE_STATUS = 50 then 'wait for WOA'
                                              when CODE_STATUS = 60 then 'WOA, wait for Court roll'
                                              when CODE_STATUS = 90 then 'Court roll, wait for Court Decision'
                                              when CODE_STATUS = 99 then 'Closed'
	                                          else 'unknown'
                                        end)as STAUS_DESC
                                        from CONTRAVENTIONS ", Convert.ToUInt32(req.Parameters["MAX"]));
            else
                sQuery =
                    @"select CUID, ROW_NUMBER() OVER(ORDER BY cuid DESC) AS ROWNUM
                            ,NOTICE_NUM, CARNUM, WHEN_DT, FINE, OFFICER, N016_P_NAME
                            , (case when CODE_STATUS = 0 then 'wait for NATIS Up'
                                    when CODE_STATUS = 10 then 'wait for NATIS Down'
                                    when CODE_STATUS = 20 then 'wait for 1st-Notice'
                                    when CODE_STATUS = 29 then 'No NATIS data'
                                    when CODE_STATUS = 30 then '1st Noticed, wait for NBS or reminder'
                                    when CODE_STATUS = 40 then 'NBS, wait for Summon'
                                    when CODE_STATUS = 50 then 'wait for WOA'
                                    when CODE_STATUS = 60 then 'WOA, wait for Court roll'
                                    when CODE_STATUS = 90 then 'Court roll, wait for Court Decision'
                                    when CODE_STATUS = 99 then 'Closed'
	                                else 'unknown'
                            end)as STAUS_DESC
                            from CONTRAVENTIONS ";

            if(!swhere.Equals(""))
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



        private string Make_where(string key, string value, int nop)
        {
            if (value == null || value.Equals(""))
                return "";

            string sshort = string.Empty;
            switch (nop)
            {
                case -2:
                    sshort = string.Format(" ({0} <= '{1}')", key, value);
                    break;

                case 0:
                    sshort = string.Format(" ({0} = '{1}')", key, value);
                    break;

                case 2:
                    sshort = string.Format(" ({0} >= '{1}')", key, value);
                    break;


                case 10:
                    sshort = string.Format(" ({0} = {1})", key, nop);
                    break;

                case 50:
                    sshort = string.Format(" ({0} like '%{1}%')", key, value);
                    break;

            }

            if (!swhere.Equals(""))
                swhere = " AND ";

            swhere += sshort;

            return swhere;
        }

        public TTOPSReply SelectCaseInfo(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "SELECT * FROM CONTRAVENTIONS WHERE CUID = " + req.Parameters["CUID"].ToString();

            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;

        }

        public TTOPSReply SelectPersonInfo(TTOPSRequest req)
        {
            QueryParam param = new QueryParam();
            TTOPSReply reply = new TTOPSReply();

            param.SQL = "SELECT N015_P_IDNUM, N016_P_NAME, N019_P_ADDR1, N020_P_ADDR2, N021_P_ADDR3, N025_P_STRT1, N026_P_STRT2, N027_P_STRT3, N030_P_SCODE, N037_P_CDATE FROM CONTRAVENTIONS WHERE CUID = " + req.Parameters["CUID"].ToString();

            param.SqlCommandType = CommandType.Text;

            DataSet ds = this.FillDataSet(param, CONNECTION_NAME);

            reply.ResultSet = ds;

            return reply;

        }


    }
}
