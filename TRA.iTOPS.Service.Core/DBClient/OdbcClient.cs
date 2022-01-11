using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Globalization;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts.Diagnostics;

namespace TRA.iTOPS.Service.Core.DBClient
{
    /// <summary>
    /// ODBC 연결을 지원 합니다.
    /// </summary>
    public partial class Odbc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        /// <example>
        /// <code></code>
        /// </example>
        public static DataSet FillDataSet(QueryParam queryParam, string connectionStringName)
        {
            DataSet dsResult = null;
            OdbcConnection odbcConnection = null;
            string strSqlStmt = string.Empty;
            OdbcParameter[] odbcParams = null;

            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            long lngBeginTicks = System.DateTime.Now.Ticks;

            try
            {
                OdbcCommand odbcCommand = null;
                try
                {
                    string strConnectionString = GetConnectionString(connectionStringName);
                    odbcConnection = new OdbcConnection(strConnectionString);
                    odbcConnection.Open();

                    odbcParams = queryParam.OdbcParams;

                    //string strSQL = queryParam.SQL; 
                    string strSQL = GenerateSQLStat(queryParam);

                    odbcCommand = new OdbcCommand(strSQL, odbcConnection);
                    odbcCommand.CommandType = queryParam.SqlCommandType;
                    odbcCommand.CommandTimeout = queryParam.TimeOut;

                    if (odbcParams != null)
                    {
                        foreach (OdbcParameter param in odbcParams)
                        {
                            odbcCommand.Parameters.Add(param);
                        }
                    }

                    dsResult = new DataSet();
                    dsResult.Locale = CultureInfo.InvariantCulture;
                    using (OdbcDataAdapter odbcDa = new OdbcDataAdapter(odbcCommand))
                    {
                        odbcDa.Fill(dsResult);
                    }
                }
                finally
                {
                    Odbc.GetExecutionSqlStatement(odbcCommand, true, strBeginDate, lngBeginTicks);
                }
            }
            finally
            {
                if (odbcConnection != null)
                {
                    if (odbcConnection.State == ConnectionState.Open) odbcConnection.Close();
                    odbcConnection.Dispose();
                }
            }
            return dsResult;
        }


        /// <summary>
        /// 쿼리문을 실행하고 영향받은 행 수를 반환 합니다.
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        /// <example>
        /// <code></code>
        /// </example>
        public static int ExecuteNonQuery(QueryParam queryParam, string connectionStringName)
        {
            int iResult = -1;
            OdbcConnection odbcConnection = null;
            string strSqlStmt = string.Empty;
            OdbcParameter[] odbcParams = null;
            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");

            try
            {
                long lngBeginTicks = System.DateTime.Now.Ticks;
                string strConnectionString = GetConnectionString(connectionStringName);
                odbcConnection = new OdbcConnection(strConnectionString);
                odbcConnection.Open();

                odbcParams = queryParam.OdbcParams;
                //string strSQL = queryParam.SQL; 
                string strSQL = GenerateSQLStat(queryParam);
                OdbcCommand odbcCommand = null;
                try
                {
                    odbcCommand = new OdbcCommand(strSQL, odbcConnection);
                    odbcCommand.CommandType = queryParam.SqlCommandType;
                    odbcCommand.CommandTimeout = queryParam.TimeOut;
                    if (odbcParams != null)
                    {
                        foreach (OdbcParameter param in odbcParams)
                        {
                            odbcCommand.Parameters.Add(param);
                        }
                    }
                    iResult = odbcCommand.ExecuteNonQuery();
                }
                finally
                {
                    Odbc.GetExecutionSqlStatement(odbcCommand, true, strBeginDate, lngBeginTicks);
                }
            }
            finally
            {
                if (odbcConnection != null)
                {
                    if (odbcConnection.State == ConnectionState.Open) odbcConnection.Close();
                    odbcConnection.Dispose();
                }
            }
            return iResult;
        }

        /// <summary>
        /// 쿼리를 실행하고 첫번째 행 첫번째 필드의 데이타를 반환 합니다.
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <example>
        /// <code></code>
        /// </example>
        public static object ExecuteScalar(QueryParam queryParam, string connectionStringName)
        {
            object oResult = null;
            OdbcConnection odbcConnection = null;
            string strSqlStmt = string.Empty;
            OdbcParameter[] odbcParams = null;

            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            long lngBeginTicks = System.DateTime.Now.Ticks;

            try
            {
                string strConnectionString = GetConnectionString(connectionStringName);

                odbcConnection = new OdbcConnection(strConnectionString);
                odbcConnection.Open();

                odbcParams = queryParam.OdbcParams;

                //string strSQL = queryParam.SQL; 
                string strSQL = GenerateSQLStat(queryParam);
                OdbcCommand odbcCommand = null;
                try
                {
                    odbcCommand = new OdbcCommand(strSQL, odbcConnection);
                    odbcCommand.CommandType = queryParam.SqlCommandType;
                    odbcCommand.CommandTimeout = queryParam.TimeOut;
                    if (odbcParams != null)
                    {
                        foreach (OdbcParameter param in odbcParams)
                        {
                            odbcCommand.Parameters.Add(param);
                        }
                    }
                    oResult = odbcCommand.ExecuteScalar();
                }
                finally
                {
                    Odbc.GetExecutionSqlStatement(odbcCommand, true, strBeginDate, lngBeginTicks);
                }
            }
            finally
            {
                if (odbcConnection != null)
                {
                    if (odbcConnection.State == ConnectionState.Open) odbcConnection.Close();
                    odbcConnection.Dispose();
                }
            }
            return oResult;
        }
        private static string GenerateSQLStat(QueryParam queryParam)
        {
            string strSQL = string.Empty;
            switch (queryParam.SqlCommandType)
            {
                case CommandType.StoredProcedure:
                    strSQL = GenerateSQLStatBySP(queryParam.SQL, queryParam.OdbcParams);
                    break;
                case CommandType.Text:
                    strSQL = GenerateSQLStatByText(queryParam.SQL, queryParam.OdbcParams);
                    break;
            }
            return strSQL;
        }

        /// <summary>
        /// Stored Procedure용 SQL문을 작성 합니다.
        /// </summary>
        /// <param name="odbcParams"></param>
        /// <returns></returns>
        private static string GenerateSQLStatBySP(string sp, OdbcParameter[] odbcParams)
        {
            string strSQL = "{ ? = CALL {0} {1} {2} {3}}";

            if (odbcParams == null) return string.Empty;
            if (odbcParams.Length == 1)
            {
                strSQL = "{0} ? = CALL {1} {2}";
                strSQL = string.Format(strSQL, "{", sp, "}");
            }
            else if (odbcParams.Length > 1)
            {
                strSQL = "{0} ? = CALL {1} ( {2} ){3}";
                string strQuestion = string.Empty;

                for (int i = 0; i < odbcParams.Length; i++)
                {
                    if (i > 0)
                    {
                        if (i == odbcParams.Length - 1)
                        {
                            strQuestion += "?";
                        }
                        else
                        {
                            strQuestion += "?,";
                        }
                    }
                }
                strSQL = string.Format(strSQL, "{", sp, strQuestion, "}");
            }
            return strSQL;
        }

        /// <summary>
        /// Text용 SQL문을 작성 합니다.
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="odbcParams"></param>
        /// <param name="dbServerType"></param>
        /// <returns></returns>
        private static string GenerateSQLStatByText(string sp, OdbcParameter[] odbcParams)
        {
            string strParamName = string.Empty;
            string strSQL = string.Empty;

            strSQL = sp.ToLower();

            if (odbcParams == null) return strSQL;
            if (odbcParams.Length <= 0) return sp;

            if (odbcParams != null && odbcParams.Length > 0)
            {
                for (int i = 0; i < odbcParams.Length; i++)
                {
                    //6.24일 변경
                    //strParamName = odbcParams[i].ParameterName.Remove(odbcParams[i].ParameterName.IndexOf("__"), 3);
                    //strParamName = odbcParams[i].ParameterName.Substring(0, odbcParams[i].ParameterName.IndexOf("__"));

                    strParamName = odbcParams[i].ParameterName;
                    strParamName = strParamName.ToLower();
                    strSQL = GetCompleteSqlStmt(strParamName, strSQL);
                }
            }
            return strSQL;
        }

        private static string GetCompleteSqlStmt(string pRstr, string pSql)
        {
            string tmp = string.Empty;
            bool bfind = false;
            int findPos = 0;
            int endPos = 0;

            do
            {
                if (endPos == 0)
                {
                    findPos = pSql.IndexOf(pRstr);
                }
                else
                {
                    findPos = pSql.IndexOf(pRstr, endPos - (pRstr.Length - 1));
                }

                if (findPos >= 0)
                {
                    bfind = true;
                    endPos = findPos + pRstr.Length;

                    if (endPos < pSql.Length)
                    {
                        //endPos += 1;
                        if (!((pSql[endPos] >= 97 && pSql[endPos] <= 122) || (pSql[endPos] >= 65 && pSql[endPos] <= 90) ||
                         (pSql[endPos] == 95) || (pSql[endPos] >= 48 && pSql[endPos] <= 57)))
                        {
                            pSql = pSql.Remove(findPos, pRstr.Length);
                            pSql = pSql.Insert(findPos, "?");
                        }
                    }
                    else
                    {
                        //2010.08.24 15:53 버그패치 From.김희택
                        pSql = pSql.Remove(findPos, pRstr.Length);
                        pSql = pSql.Insert(findPos, "?");
                    }
                }
                else
                {
                    bfind = false;
                }
            } while (bfind);

            return pSql;
        }

        /// <summary>
        /// 실행된 SQL구분을 분석하여 파일에 저장 합니다.
        /// </summary>
        private static void GetExecutionSqlStatement(OdbcCommand cmd, bool markEndLine, string beginDate, long beginTicks)
        {
            bool isSqlLog = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsSQLLog"]);
            if (!isSqlLog) return;

            string strContents = string.Empty;
            ExchangeActionMessage actionMsg = new ExchangeActionMessage();
            actionMsg.RequestTime = beginDate;
            actionMsg.ResponseTime = DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            try
            {
                System.Text.StringBuilder oBuilder = new System.Text.StringBuilder();

                oBuilder.Append(System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz"));
                oBuilder.Append("\r\n");
                oBuilder.Append(cmd.CommandText);

                if (cmd.Parameters.Count > 0)
                {
                    if (cmd.CommandType == CommandType.Text)
                    {
                        for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                        {
                            oBuilder.Replace(cmd.Parameters[iElemCnt].ParameterName, GetOdbcCommandValueToString(cmd.Parameters[iElemCnt].OdbcType, cmd.Parameters[iElemCnt].Value));
                        }
                    }
                    else
                    {
                        for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                        {
                            oBuilder.Append("\r\n");
                            oBuilder.Append(cmd.Parameters[iElemCnt].ParameterName);
                            oBuilder.Append(" = ");
                            oBuilder.Append(GetOdbcCommandValueToString(cmd.Parameters[iElemCnt].OdbcType, cmd.Parameters[iElemCnt].Value));
                        }
                    }
                }
                if (markEndLine) oBuilder.Append("\r\n-------------------------------------------------------------\r\n");
                strContents = oBuilder.ToString();

                actionMsg.ActionLogType = LogType.SQL;
                actionMsg.Message = strContents;

                WriteToLog(actionMsg);
            }
            catch (Exception ex)
            {
                actionMsg.ActionLogType = LogType.ERR;
                actionMsg.Message = ex.ToString();

                WriteToLog(actionMsg);
            }
        }

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
        /// SQL Parameter를 변환합니다.
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        private static string GetOdbcCommandValueToString(OdbcType tp, object parameterValue)
        {
            string strReturn = "NULL";

            if (parameterValue != null)
            {
                if (parameterValue == System.DBNull.Value)
                    strReturn = "[NULL]";
                else
                {
                    switch (tp)
                    {
                        case OdbcType.Char:
                        case OdbcType.VarChar:
                        case OdbcType.NChar:
                        case OdbcType.NVarChar:
                        case OdbcType.NText:
                        case OdbcType.Text:
                            strReturn = string.Concat("'", parameterValue.ToString(), "'");
                            break;
                        case OdbcType.Image:
                            strReturn = "<Blob>";
                            break;
                        case OdbcType.Binary:
                            strReturn = "<Binary>";
                            break;
                        case OdbcType.VarBinary:
                            strReturn = "<VarBinary>";
                            break;
                        default:
                            strReturn = parameterValue.ToString();
                            break;
                    }
                }
            }
            return strReturn;
        }
        private static string GetConnectionString(string connectionStringName)
        {
            string strConnctionString = string.Empty;

            strConnctionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            return strConnctionString;


        }
    }
}
