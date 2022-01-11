using TRA.iTOPS.Contracts.Diagnostics;

namespace TRA.iTOPS.Diagnostics
{
    public interface ITbosLogWriter
    {
        /// <summary>
        /// Action 로그를 기록 합니다.
        /// </summary>
        /// <param name="o"></param>
        void WriteActionLog(ExchangeActionMessage o);
    }
}
