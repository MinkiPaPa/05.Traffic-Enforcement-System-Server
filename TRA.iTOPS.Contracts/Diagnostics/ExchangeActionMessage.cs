using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace TRA.iTOPS.Contracts.Diagnostics
{
    /// <summary>
    /// TMS Client DB Request Log
    /// </summary>
    [DataContract]
    [Serializable]
    public class ExchangeActionMessage
    {
        /// <summary>
        /// 관리 지역(설정 지역)
        /// </summary>
        [DataMember]
        public string SettingAREA { get; set; }

        /// <summary>
        /// 설정 지역의 RTDB 명
        /// </summary>
        [DataMember]
        public string RTDBName { get; set; }

        /// <summary>
        /// 요청 BIZ 명
        /// </summary>
        [DataMember]
        public string RequestBiz { get; set; }

        /// <summary>
        /// 요청 파라메터
        /// </summary>
        [DataMember]
        public List<KeyValuePair<string, string>> Parameters { get; set; }

        /// <summary>
        /// 요청 쿼리문
        /// </summary>
        [DataMember]
        public string Query { get; set; }

        /// <summary>
        /// 요청 사용자
        /// </summary>
        [DataMember]
        public string RequestUser { get; set; }

        /// <summary>
        /// 요청 시간
        /// </summary>
        [DataMember]
        public string RequestTime { get; set; } = DateTime.Now.ToString("yyyyMMddhhmmsszzz");

        /// <summary>
        /// 응답 시간
        /// </summary>
        public string ResponseTime { get; set; } = DateTime.Now.ToString("yyyyMMddhhmmsszzz");

        /// <summary>
        /// 메시지
        /// </summary>
        [DataMember]
        public string Message { get; set; } = null;

        /// <summary>
        /// 오류 여부
        /// </summary>
        public bool IsError { get; set; } = false;

        public LogType ActionLogType { get; set; } = LogType.ACTION;

        /// <summary>
        /// 오류 내역을 문자열로 반환
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.AppendLine($"▶SettingAREA : {this.SettingAREA}");
            sbMsg.AppendLine($"▶RTDBName : {this.RTDBName}");
            sbMsg.AppendLine($"▶RequestBiz : {this.RequestBiz}");
            if (Parameters != null && Parameters.Count > 0)
                foreach (System.Collections.Generic.KeyValuePair<string, string> item in Parameters)
                    sbMsg.AppendLine($"▶Parameters : {item.Key}{", "}{item.Value}");
            else
                sbMsg.AppendLine($"▶Parameters : ");
            sbMsg.AppendLine($"▶Query : {this.Query}");
            sbMsg.AppendLine($"▶RequestUser : {this.RequestUser}");
            sbMsg.AppendLine($"▶Request Time : {this.RequestTime}");
            sbMsg.AppendLine($"▶Response Time : {this.ResponseTime}");
            sbMsg.AppendLine($"▶IsError : {this.IsError}");
            sbMsg.AppendLine($"▶ErrorMessage : {this.Message}");
            sbMsg.AppendLine("▶************************************************************************");
            return sbMsg.ToString();
        }

        public string ToJSON()
        {
            StringBuilder sbMsg = new StringBuilder();

            string strResult = new JavaScriptSerializer().Serialize(this).ToString();
            sbMsg.AppendLine($"▶ActionMessage :{ DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")}");
            sbMsg.AppendLine($"▶Result : {strResult}");

            return sbMsg.ToString();
        }
    }
}
