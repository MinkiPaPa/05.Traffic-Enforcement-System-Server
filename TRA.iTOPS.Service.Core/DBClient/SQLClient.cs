using System;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using TRA.iTOPS.Service.Core.ParamItem;
using TRA.iTOPS.Contracts.Diagnostics;

namespace TRA.iTOPS.Service.Core.DBClient
{
    public partial class SQLClient
    {


        /// <summary>
        ///   MS-SQL용
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
            SqlConnection sqlConnection = null;
            string strSqlStmt = string.Empty;
            SqlParameter[] sqlParams = null;

            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            long lngBeginTicks = System.DateTime.Now.Ticks;

            try
            {
                SqlCommand sqlCommand = null;
                try
                {
                    //string strConnectionString = GetConnectionString(connectionStringName);
                    // userID 와 password 는 하드코딩으로 아이피만 기록
                    //string strConnectionString = GetConnectionString(connectionStringName) + ",1433; uid = admin; pwd = @mbcisno1; database = TRAPEACE;";
                    // 패스워드 복호화 추가 및 수정(AES128의 key는 16자리)
                    string[] DBConn = GetConnectionString(connectionStringName).Split(';');
                    string strConnectionString = DBConn[0] + ";" + DBConn[1] + ";" + AESDecrypt128(DBConn[2], "trapeace_ambc!za") + ";" + DBConn[3] +";";

                    sqlConnection = new SqlConnection(strConnectionString);
                    sqlConnection.Open();

                    sqlParams = queryParam.sqlParams;

                    string strSQL = queryParam.SQL; 
                    //string strSQL = GenerateSQLStat(queryParam);

                    sqlCommand = new SqlCommand(strSQL, sqlConnection);
                    sqlCommand.CommandType = queryParam.SqlCommandType;
                    sqlCommand.CommandTimeout = queryParam.TimeOut;

                    if (sqlParams != null)
                    {
                        foreach (SqlParameter param in sqlParams)
                        {
                            sqlCommand.Parameters.Add(param);
                        }
                    }

                    dsResult = new DataSet();
                    dsResult.Locale = CultureInfo.InvariantCulture;
                    using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDa.Fill(dsResult);
                    }
                }
                finally
                {
                    SQLClient.GetExecutionSqlStatement(sqlCommand, true, strBeginDate, lngBeginTicks);
                }
            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
                    sqlConnection.Dispose();
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
            SqlConnection sqlConnection = null;
            string strSqlStmt = string.Empty;
            SqlParameter[] sqlParams = null;
            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");

            try
            {
                long lngBeginTicks = System.DateTime.Now.Ticks;
                //string strConnectionString = GetConnectionString(connectionStringName);
                // userID 와 password 는 하드코딩으로 아이피만 기록
                //string strConnectionString = GetConnectionString(connectionStringName) + ",1433; uid = admin; pwd = @mbcisno1; database = TRAPEACE;";
                // 패스워드 복호화 추가 및 수정(AES128의 key는 16자리)
                string[] DBConn = GetConnectionString(connectionStringName).Split(';');
                string strConnectionString = DBConn[0] + ";" + DBConn[1] + ";" + AESDecrypt128(DBConn[2], "trapeace_ambc!za") + ";" + DBConn[3] + ";";

                sqlConnection = new SqlConnection(strConnectionString);
                sqlConnection.Open();

                sqlParams = queryParam.sqlParams;
                string strSQL = queryParam.SQL; 
                //string strSQL = GenerateSQLStat(queryParam);
                SqlCommand sqlCommand = null;
                try
                {
                    sqlCommand = new SqlCommand(strSQL, sqlConnection);
                    sqlCommand.CommandType = queryParam.SqlCommandType;
                    sqlCommand.CommandTimeout = queryParam.TimeOut;
                    if (sqlParams != null)
                    {
                        foreach (SqlParameter param in sqlParams)
                        {
                            sqlCommand.Parameters.Add(param);
                        }
                    }
                    iResult = sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    SQLClient.GetExecutionSqlStatement(sqlCommand, true, strBeginDate, lngBeginTicks);
                }
            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
                    sqlConnection.Dispose();
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
            SqlConnection sqlConnection = null;
            string strSqlStmt = string.Empty;
            SqlParameter[] odbcParams = null;

            string strBeginDate = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            long lngBeginTicks = System.DateTime.Now.Ticks;

            try
            {
                //string strConnectionString = GetConnectionString(connectionStringName);
                // userID 와 password 는 하드코딩으로 아이피만 기록
                //string strConnectionString = GetConnectionString(connectionStringName) + ",1433; uid = admin; pwd = @mbcisno1; database = TRAPEACE;";
                // 패스워드 복호화 추가 및 수정 (AES128의 key는 16자리)
                string[] DBConn = GetConnectionString(connectionStringName).Split(';');
                string strConnectionString = DBConn[0] + ";" + DBConn[1] + ";" + AESDecrypt128(DBConn[2], "trapeace_ambc!za") + ";" + DBConn[3] + ";";

                sqlConnection = new SqlConnection(strConnectionString);
                sqlConnection.Open();

                odbcParams = queryParam.sqlParams;

                string strSQL = queryParam.SQL; 
                //string strSQL = GenerateSQLStat(queryParam);
                SqlCommand sqlCommand = null;
                try
                {
                    sqlCommand = new SqlCommand(strSQL, sqlConnection);
                    sqlCommand.CommandType = queryParam.SqlCommandType;
                    sqlCommand.CommandTimeout = queryParam.TimeOut;
                    if (odbcParams != null)
                    {
                        foreach (SqlParameter param in odbcParams)
                        {
                            sqlCommand.Parameters.Add(param);
                        }
                    }
                    oResult = sqlCommand.ExecuteScalar();
                }
                finally
                {
                    SQLClient.GetExecutionSqlStatement(sqlCommand, true, strBeginDate, lngBeginTicks);
                }
            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
                    sqlConnection.Dispose();
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
                    strSQL = GenerateSQLStatBySP(queryParam.SQL, queryParam.sqlParams);
                    break;
                case CommandType.Text:
                    strSQL = GenerateSQLStatByText(queryParam.SQL, queryParam.sqlParams);
                    break;
            }
            return strSQL;
        }

        /// <summary>
        /// Stored Procedure용 SQL문을 작성 합니다.
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private static string GenerateSQLStatBySP(string sp, SqlParameter[] sqlParams)
        {
            string strSQL = "{ ? = CALL {0} {1} {2} {3}}";

            if (sqlParams == null) return string.Empty;
            if (sqlParams.Length == 1)
            {
                strSQL = "{0} ? = CALL {1} {2}";
                strSQL = string.Format(strSQL, "{", sp, "}");
            }
            else if (sqlParams.Length > 1)
            {
                strSQL = "{0} ? = CALL {1} ( {2} ){3}";
                string strQuestion = string.Empty;

                for (int i = 0; i < sqlParams.Length; i++)
                {
                    if (i > 0)
                    {
                        if (i == sqlParams.Length - 1)
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
        private static string GenerateSQLStatByText(string sp, SqlParameter[] sqlParams)
        {
            string strParamName = string.Empty;
            string strSQL = string.Empty;

            strSQL = sp.ToLower();

            if (sqlParams == null) return strSQL;
            if (sqlParams.Length <= 0) return sp;

            if (sqlParams != null && sqlParams.Length > 0)
            {
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    //6.24일 변경
                    //strParamName = odbcParams[i].ParameterName.Remove(odbcParams[i].ParameterName.IndexOf("__"), 3);
                    //strParamName = odbcParams[i].ParameterName.Substring(0, odbcParams[i].ParameterName.IndexOf("__"));

                    strParamName = sqlParams[i].ParameterName;
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
        private static void GetExecutionSqlStatement(SqlCommand cmd, bool markEndLine, string beginDate, long beginTicks)
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
                            oBuilder.Replace(cmd.Parameters[iElemCnt].ParameterName, GetsqlCommandValueToString(cmd.Parameters[iElemCnt].SqlDbType, cmd.Parameters[iElemCnt].Value));
                        }
                    }
                    else
                    {
                        for (int iElemCnt = 0; iElemCnt < cmd.Parameters.Count; iElemCnt++)
                        {
                            oBuilder.Append("\r\n");
                            oBuilder.Append(cmd.Parameters[iElemCnt].ParameterName);
                            oBuilder.Append(" = ");
                            oBuilder.Append(GetsqlCommandValueToString(cmd.Parameters[iElemCnt].SqlDbType, cmd.Parameters[iElemCnt].Value));
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
        private static string GetsqlCommandValueToString(SqlDbType tp, object parameterValue)
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
                        case SqlDbType.Char:
                        case SqlDbType.VarChar:
                        case SqlDbType.NChar:
                        case SqlDbType.NVarChar:
                        case SqlDbType.NText:
                        case SqlDbType.Text:
                            strReturn = string.Concat("'", parameterValue.ToString(), "'");
                            break;
                        case SqlDbType.Image:
                            strReturn = "<Blob>";
                            break;
                        case SqlDbType.Binary:
                            strReturn = "<Binary>";
                            break;
                        case SqlDbType.VarBinary:
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

        #region 암복호화
        //AES128 암호화
        private String AESEncrypt128(String Input, String key)
        {

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(Input);
            byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }

        //AES128 복호화
        private static String AESDecrypt128(String Input, String key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(Input);
            byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            return DecryptedData;
        }
        #endregion


    }
}

