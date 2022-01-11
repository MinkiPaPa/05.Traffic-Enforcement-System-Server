using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRA.iTOPS.Contracts.Session
{
    /// <summary>
    /// 사용자 정보 세션
    /// </summary>
    public class UserInfo
    {
        private static UserInfo _userInfo = null;
        /// <summary>
        /// Singleton 패턴
        /// </summary>
        public static UserInfo Current
        {
            get
            {
                if (_userInfo == null)
                    _userInfo = new UserInfo();

                return _userInfo;
            }
        }

        private string _loginID = string.Empty;
        /// <summary>
        /// 로그인 사용자 ID 
        /// </summary>
        public string LoginID
        {
            get
            {
                return _loginID;
            }
            set
            {
                _loginID = value;
            }
        }

        private string _level = string.Empty;
        /// <summary>
        /// 로그인 사용자 Level  
        /// </summary>
        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

    }
}
