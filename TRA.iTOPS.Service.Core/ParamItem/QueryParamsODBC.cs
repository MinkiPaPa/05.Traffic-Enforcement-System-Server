using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRA.iTOPS.Service.Core.ParamItem
{
    public class QueryParam
    {

        private string _sqlString = string.Empty;
        /// <summary>
        /// SQL문자열을 설정하거나 반환 합니다.
        /// </summary>
        public string SQL
        {
            get { return this._sqlString; }
            set { this._sqlString = value; }
        }

        public OdbcParameter[] OdbcParams { get; set;  }

        public SqlParameter[] sqlParams { get; set; }

        /// <summary>
        ///CommandType- Text만 지원중 
        ///프로시저 지원 하려면 OdbcClient 쪽 변경해야함
        /// </summary>
        public CommandType SqlCommandType { get; set; }

        private int _timeOut = 30;
        /// <summary>
        /// DB 쿼리 수행시간을 제한 합니다.<br />
        /// 기본값은 30초 입니다.
        /// </summary>
        public int TimeOut
        {
            get { return this._timeOut; }
            set { this._timeOut = value; }
        }
    }
}
