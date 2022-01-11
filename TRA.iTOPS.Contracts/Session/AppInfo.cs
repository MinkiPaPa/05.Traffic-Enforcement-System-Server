using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRA.iTOPS.Contracts.Session
{
    public class AppInfo
    {
        private static AppInfo _appInfo = null;
        public static AppInfo Current
        {
            get
            {
                if (_appInfo == null)
                    _appInfo = new AppInfo();

                return _appInfo;
            }
        }

        public int ErrorRaiseCount { get; set; } = 0;

        public string[] StyleNames { get; set; }
    }
}
