using TRA.iTOPS.Contracts.Diagnostics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRA.iTOPS.Diagnostics.LogDispatchers
{
    public class FileWriter: ITbosLogWriter
    {
        /// <summary>
        /// 로그를 작성한다.
        /// </summary>
        /// <param name="o">로그 객체</param>
        /// <param name="messageType">로그 작성 형식:STRING/JSON</param>
        public void WriteActionLog(ExchangeActionMessage actionMsg)
        {
            // ExchangeActionMessage actionMsg = o as ExchangeActionMessage;

            // 로깅
            string strFileName = Path.Combine(this.GetLogDirectory(actionMsg.ActionLogType), $"iTOPS_{actionMsg.ActionLogType.ToString()}_{DateTime.Now.ToString("yyyyMMdd_HH")}.log");
            string strLog = actionMsg.ToJSON();
            WriteToFile(strLog, strFileName);
        }

        /// <summary>
        /// 로그 파일을 작성할 경로를 반환 반다.
        /// </summary>
        /// <param name="logType">Action/ERR</param>
        /// <returns></returns>
        private string GetLogDirectory(LogType logType)
        {
            string strDirName = ConfigurationManager.AppSettings["LogPath"];

            if (string.IsNullOrEmpty(strDirName))
                strDirName = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"iTOPS\Log");

            strDirName = Path.Combine(strDirName, logType.ToString());
            strDirName = Path.Combine(strDirName, DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(strDirName))
                Directory.CreateDirectory(strDirName);

            return strDirName;
        }
        /// <summary>
        /// log 내용을 파일에 작성한다.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="fileName"></param>
        private async void WriteToFile(string buffer, string fileName)
        {
            Encoding encoding = Encoding.UTF8;

            using (FileStream oStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter oWriter = new StreamWriter(oStream))
                {
                    await oWriter.WriteAsync(buffer);
                    oWriter.Flush();
                }
            }
        }
    }
}
