using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRA.iTOPS.Contracts.Diagnostics
{
    /// <summary>
    /// 에러메시지 타입을 정의 합니다.
    /// </summary>
    public enum ExceptionType
    {
        Inform,
        Warning,
        Error,
        Debug
    }
}
