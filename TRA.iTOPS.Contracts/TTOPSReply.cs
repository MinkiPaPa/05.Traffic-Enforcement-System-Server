using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TRA.iTOPS.Contracts
{
    public class TTOPSReply
    {
        /// <summary>
        /// 사용자 정의 ResultCode값을 설정 하거나 반환 합니다.
        /// </summary>
        [DataMember]
        public string ResultCode { get; set; }

        /// <summary>
        /// 사용자 정의 Result Message값을 설정 하거나 반환 합니다.
        /// </summary>
        [DataMember]
        public string ResultData { get; set; }

        /// <summary>
        /// 사용자 정의 DataSet 개체를 설정 하거나 반환 합니다.
        /// </summary>
        [DataMember]
        public DataSet ResultSet { get; set; }

        /// <summary>
        /// 사용자 정의 Dictionary 개체를 설정 하거나 반환 합니다.
        /// </summary>
        [DataMember]
        public Dictionary<string, object> ResultCollection { get; set; }


    }
}
