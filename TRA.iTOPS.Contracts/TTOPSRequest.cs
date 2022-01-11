using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Collections;

namespace TRA.iTOPS.Contracts
{
    [DataContract]
    public class TTOPSRequest
    {

        // ToString() 시 사용하는 구분자 (COLUMN DELIMETER)
        public static string BASIC_DELIMETER = Convert.ToString((char)5);

        // DELIMETER
        public static string BASIC_DELIMETER2 = Convert.ToString((char)4);

        private Dictionary<string, object> _parameters = null;

        /// <summary>
        /// Request Parameter를 컬렉션에 추가 합니다.
        /// </summary>
        [DataMember]
        public Dictionary<string, object> Parameters
        {
            get
            {
                if (this._parameters == null)
                    this._parameters = new Dictionary<string, object>();

                return this._parameters;
            }
            set
            {
                this._parameters = value;
            }
        }

        public new string ToString()
        {
            string strResult = string.Empty;
            string strFormat = "{0}:{1}{2}";

            if (_parameters == null)
                return "";

            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (KeyValuePair<string, object> item in _parameters)
            {
                if (i == _parameters.Count)
                    sb.AppendFormat(strFormat, item.Key, item.Value.ToString(), "");
                else
                    sb.AppendFormat(strFormat, item.Key, item.Value.ToString(), BASIC_DELIMETER);

                i += 1;
            }

            return sb.ToString();
        }
    }
}
