using TRA.iTOPS.Contracts.Diagnostics;
using TRA.iTOPS.Service.Core.DBClient;
using TRA.iTOPS.Service.Core.ParamItem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Diagnostics;

namespace TRA.iTOPS.Service.Core
{
    public abstract partial class BizBase : ContextBase
    {
        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        public BizBase()
        {
        }


        #region FillDataSet
        /// <summary>
        /// SqlConnection에 대한 SQL 문을 실행하고 영향을 받는 데이터 셋을 반환합니다. 
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        protected DataSet FillDataSet(QueryParam queryParam, string connectionStringName)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            DataSet dsResult = null;
            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            try
            {
                //dsResult = Odbc.FillDataSet(queryParam, connectionStringName);
                dsResult = SQLClient.FillDataSet(queryParam, connectionStringName);  // MS SQL용

            }
            catch (Exception ex)
            {
                ExchangeActionMessage actionMsg = new ExchangeActionMessage();
                actionMsg.ActionLogType = LogType.ERR;
                actionMsg.RequestTime = strBeginDate;
                actionMsg.ResponseTime = DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
                actionMsg.Message = ex.ToString();
                WriteToLog(actionMsg);

                throw;
            }

            return dsResult;
        }

        protected int ExecuteNonQuery(QueryParam queryParam, string connectionStringName)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            int iResult = 0;
            try
            {
                //iResult = Odbc.ExecuteNonQuery(queryParam, connectionStringName);
                iResult = SQLClient.ExecuteNonQuery(queryParam, connectionStringName); //// MS SQL용
            }
            catch (Exception ex)
            {
                ExchangeActionMessage actionMsg = new ExchangeActionMessage();
                actionMsg.ActionLogType = LogType.ERR;
                actionMsg.RequestTime = strBeginDate;
                actionMsg.ResponseTime = DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
                actionMsg.Message = ex.Message.ToString();
                WriteToLog(actionMsg);

                throw;
            }

            return iResult;
        }
        protected object ExecuteScalar(QueryParam queryParam, string connectionStringName)
        {

            Stopwatch stopwatch = Stopwatch.StartNew();
            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            object result = null;
            try
            {
                //result = Odbc.ExecuteScalar(queryParam, connectionStringName);
                result = SQLClient.ExecuteScalar(queryParam, connectionStringName); // MS SQL용
            }
            catch (Exception ex)
            {
                ExchangeActionMessage actionMsg = new ExchangeActionMessage();
                actionMsg.ActionLogType = LogType.ERR;
                actionMsg.RequestTime = strBeginDate;
                actionMsg.ResponseTime = DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
                actionMsg.Message = ex.ToString();
                WriteToLog(actionMsg);

                throw;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 로그를 작성 합니다.
        /// </summary>
        /// <param name="actionMsg"></param>
        private static void WriteToLog(ExchangeActionMessage actionMsg)
        {
            try
            {
                TRA.iTOPS.Diagnostics.LogDispatchers.FileWriter fs = new Diagnostics.LogDispatchers.FileWriter();
                fs.WriteActionLog(actionMsg);
            }
            catch { }
        }

        /// <summary>
        /// TMon Request를 Odbc 파라메터로 변환
        /// </summary>
        /// <param name="parameters">Request 파라메터</param>
        /// <returns></returns>
        public static OdbcParameter[] ConvertOdbcParam(Dictionary<string, object> parameters)
        {
            int iSize = parameters.Count;
            OdbcParameter[] arrParam = new OdbcParameter[iSize];
            int i = 0;
            foreach (KeyValuePair<string, object> item in parameters)
            {
                arrParam[i] = new OdbcParameter();
                arrParam[i].ParameterName = "@" + item.Key;
                arrParam[i].Value = item.Value;
                i++;
            }

            return arrParam;

        }

        public static SqlParameter[] ConvertsqlParam(Dictionary<string, object> parameters)
        {
            int iSize = parameters.Count;
            SqlParameter[] arrParam = new SqlParameter[iSize];
            int i = 0;
            foreach (KeyValuePair<string, object> item in parameters)
            {
                arrParam[i] = new SqlParameter();
                arrParam[i].ParameterName = "@" + item.Key;
                arrParam[i].Value = item.Value;
                i++;
            }

            return arrParam;

        }

    }
}
