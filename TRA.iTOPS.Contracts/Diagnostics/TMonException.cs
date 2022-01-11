using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRA.iTOPS.Contracts.Diagnostics
{
    [DataContract]
    [Serializable]
    public class TMonException : Exception, ISerializable
    {
        public TMonException() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorDetail"></param>
        /// <param name="errorMessageType"></param>
        /// <param name="loginID"></param>
        /// <param name="computerName"></param>
        public TMonException(string errorCode, string errorMessage, string errorDetail, ExceptionType errorMessageType,
            string loginID, string computerName)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.ErrorDetail = errorDetail;
            this.ErrorMessageType = errorMessageType;
            this.LoginID = loginID;
            this.ComputerName = computerName;
        }
        public TMonException(SerializationInfo info, StreamingContext context)
        {
            _errorCode = (string)info.GetValue("ErrorCode", typeof(string));
            _errorMessage = (string)info.GetValue("ErrorMessage", typeof(string));
            _errorDetail = (string)info.GetValue("ErrorDetail", typeof(string));
            _messageType = (ExceptionType)info.GetValue("ErrorMessageType", typeof(ExceptionType));
            _loginID = (string)info.GetValue("LoginID", typeof(string));
            _computerName = (string)info.GetValue("ComputerName", typeof(string));
        }

        private string _errorCode = "iTOPS:none";
        [DataMember]
        public string ErrorCode
        {

            get { return _errorCode; }
            set { _errorCode = value; }
        }

        private string _errorMessage = string.Empty;
        [DataMember]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        private string _errorDetail = string.Empty;
        [DataMember]
        public string ErrorDetail
        {
            get { return _errorDetail; }
            set { _errorDetail = value; }
        }

        private ExceptionType _messageType = ExceptionType.Error;
        /// <summary>
        /// 오류 메시지 타입
        /// </summary>
        [DataMember]
        public ExceptionType ErrorMessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        private string _loginID = string.Empty;
        [DataMember]
        public string LoginID
        {
            get { return _loginID; }
            set { _loginID = value; }
        }

        private string _computerName = string.Empty;
        /// <summary>
        /// 컴퓨터 이름
        /// </summary>
        public string ComputerName
        {
            get
            {
                if (string.IsNullOrEmpty(_computerName))
                {
                    _computerName = SystemInformation.ComputerName;
                }
                return _computerName;
            }
            set { _computerName = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ExceptionType : {this.ErrorMessageType}");
            sb.AppendLine($"LoginID : {this.LoginID}");
            sb.AppendLine($"ComputerName : {this.ComputerName}");
            sb.AppendLine($"ErrorCode : {this.ErrorCode}");
            sb.AppendLine($"ErrorMessage : {this.ErrorMessage}");
            sb.AppendLine($"ErrorDetail : {this.ErrorDetail}");

            return sb.ToString();

        }

        public string ToFormattedString()
        {
            string strToString = this.ToString();

            strToString = strToString.Replace("\r\n", "<br/>");

            return strToString;
        }

        public ExchangeActionMessage ToExchangeActionMessage()
        {
            ExchangeActionMessage actionMsg = new ExchangeActionMessage();
            actionMsg.ActionLogType = LogType.ERR;
            actionMsg.ResponseTime = DateTime.Now.ToString("yyyyMMddHHmmss.fffzz");
            actionMsg.Message = this.ToString();

            return actionMsg;
        }
    }
}
