using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using TRA.TBOS.Windows.Set.Splash;
using TRA.iTOPS.Contracts.Diagnostics;
using TRA.iTOPS.Contracts.Session;
using TRA.iTOPS.Windows.Set.MainApp;

namespace TRA.iTOPS.Launcher
{
    static partial class Program
    {

        //private static string APP_MUTEX_GUID = "TBOS1317-0D24-4C0D-86FE-AE269D1B15DA";
        //private static string PRODUCT_NAME = string.Empty;
        private static int COMPUTER_NAME_MAX_LENGTH = 15;

        private static void StartUpCheck(string[] args)
        {
            /*
            // Configuration에서 APP_MUTEX_GUID값을 가져옴
            if (ConfigurationManager.AppSettings["APP_MUTEX_GUID"] != null)
            {
                APP_MUTEX_GUID = ConfigurationManager.AppSettings["APP_MUTEX_GUID"].Trim();
            }

            // VISTA 이상에서만 확인
            if (Environment.OSVersion.Version.Major >= 6)
            {
                if (IsAdministrator() == false)
                {
                    try
                    {
                        ProcessStartInfo procInfo = new ProcessStartInfo();
                        procInfo.UseShellExecute = true;
                        procInfo.FileName = Application.ExecutablePath;
                        procInfo.WorkingDirectory = Environment.CurrentDirectory;
                        procInfo.Verb = "runas";
                        if (args.Length > 0)
                            procInfo.Arguments = args[0];

                        Process.Start(procInfo);
                    }
                    catch (Exception ex)
                    {
                        // TODO 공통 에러 메세지로 변경
                        MessageBox.Show(ex.Message.ToString());
                    }
                    return;
                }
            }

            if (ConfigurationManager.AppSettings["PRODUCT_NAME"] != null)
                PRODUCT_NAME = ConfigurationManager.AppSettings["PRODUCT_NAME"].Trim();
            */
            Mutex mutex = null;
            bool isNew = false;

            //mutex = new Mutex(true, APP_MUTEX_GUID, out isNew);
            mutex = new Mutex(true, "iTOPS_Server", out isNew);


            if (isNew)
            {
                // 어플리케이션 전역 레벨에서 에러를 Catch하기 위해 이벤트 핸들러를 Attach 시킨다.
                SetExceptionHandler();

                //SplashManager<SplashForm>.Show();
                //SplashManager<SplashForm>.SetMessage("iTOPS를 시작 하는 중 입니다.");
                // AutoUpdateCheck();

                string strLoginType = ConfigurationManager.AppSettings["LOGIN_FORM_TYPE"];
                string[] arrLoginType = strLoginType.Split(',');
                Assembly assmbly = Assembly.LoadFile(System.IO.Path.Combine(Application.StartupPath, arrLoginType[1] + ".dll"));
                Form loginForm = assmbly.CreateInstance(arrLoginType[0]) as Form;
                Application.Run(loginForm);
            }
            else
            {
                MessageBox.Show("The program is running." , "iTOPS Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        /// <summary>
        /// 관리자 권한 체크
        /// </summary>
        /// <returns></returns>
        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }

        /// <summary>
        /// 에러 핸들러
        /// </summary>
        private static void SetExceptionHandler()
        {
            Application.ThreadException += (sender1, e1) =>
            {
                ApplicationErrorHandler(e1.Exception);
            };
            AppDomain.CurrentDomain.UnhandledException += (sender2, e2) =>
            {
                ApplicationErrorHandler((Exception)e2.ExceptionObject);
            };
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        }

        /// <summary>
        /// Error 공통 처리 Dispatcher 
        /// </summary>
        /// <param name="ex"></param>
        private static void ApplicationErrorHandler(Exception ex)
        {
            try
            {
                TMonException tmEx = null;
                bool isTmException = false;
                if (ex is TMonException)
                {
                    isTmException = true;
                    tmEx = (TMonException)ex;
                }
                else
                {
                    // TMonException이 있는지 찾기
                    var innerException = ex.InnerException;
                    while (null != innerException)
                    {
                        if (innerException is TMonException)
                        {
                            isTmException = true;
                            tmEx = (TMonException)innerException;
                            break;
                        }
                        innerException = innerException.InnerException;
                    }
                }

                // TMonException 아니면 TMonException 만든다.
                if (isTmException == false)
                {
                    string strComputerName = System.Windows.Forms.SystemInformation.ComputerName;
                    if (strComputerName.Length > COMPUTER_NAME_MAX_LENGTH)
                        strComputerName = strComputerName.Substring(0, COMPUTER_NAME_MAX_LENGTH);
                    else
                        strComputerName = strComputerName.PadRight(COMPUTER_NAME_MAX_LENGTH, '0');

                    tmEx = new TMonException(
                        "iTOPS Server:Windows",
                        ex.Message,
                        ex.ToString(),
                        ExceptionType.Error,
                        TRA.iTOPS.Contracts.Session.UserInfo.Current.LoginID,
                        string.Format("{0}.{1}", System.DateTime.Now.ToString("yyyyMMddHHmmssfff"), strComputerName)
                        );
                }

                // Error Dialog
                ShowErrorDialogBox(tmEx);
            }
            finally
            {
                // ExitApplication();
            }
        }

        private static void ShowErrorDialogBox(TMonException tmEx)
        {
            // 연속 Error Count 증가
            if(tmEx.ErrorMessageType == ExceptionType.Error)
                TRA.iTOPS.Contracts.Session.AppInfo.Current.ErrorRaiseCount += 1;


            // 로깅
            bool isErrorLog = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsErrorLog"]);
            if (isErrorLog)
            {
                try
                {
                    TRA.iTOPS.Diagnostics.LogDispatchers.FileWriter fs = new TRA.iTOPS.Diagnostics.LogDispatchers.FileWriter();
                    fs.WriteActionLog(tmEx.ToExchangeActionMessage());
                }
                catch { }
            }

            //Form mdiForm = Application.OpenForms.Cast<Form>().Where(a => a.Name.Equals("MDIMainForm")).ToList().FirstOrDefault();
            Form mdiForm = Application.OpenForms.Cast<Form>().Where(a => a.Name.Equals("MainForm")).ToList().FirstOrDefault();
            FormBase formBase = new FormBase(false);
            // TODO 에러 메세지 대화상자 처리
            switch (tmEx.ErrorMessageType)
            {
                case ExceptionType.Error:
                    formBase.ShowMesage(MessageType.Error,
                        tmEx.ToFormattedString(),
                        "Appliation Error", MessageBoxIcon.Error, MessageBoxButtons.OK, null);
                    break;
                case ExceptionType.Warning:
                    formBase.ShowMesage(MessageType.Warning,
                        tmEx.Message,
                        "Appliation Warning", MessageBoxIcon.Warning, MessageBoxButtons.OK, null);
                    break;
                case ExceptionType.Inform:
                    formBase.ShowMesage(MessageType.Inform,
                        tmEx.Message,
                        "Appliation Inform", MessageBoxIcon.Information, MessageBoxButtons.OK, null);
                    break;
                case ExceptionType.Debug:
                    formBase.ShowMesage(MessageType.Inform,
                        tmEx.Message,
                        "Appliation Debug", MessageBoxIcon.Hand, MessageBoxButtons.OK, null);
                    break;
            }


            // 5회 연속 에러 발생 시 프로그램 종료
            if (AppInfo.Current.ErrorRaiseCount >= 5)
            {
                // TODO 공통 메세지 대화상자로 변경
                DialogResult dlgResult = MessageBox.Show("Error occurred 5 consecutive times.\r\nDo you want to kill the program?", "Confirm !", MessageBoxButtons.YesNo);
                if (dlgResult == DialogResult.Yes)
                {
                    // 프로그램 종료
                    ExitApplication();
                }
                else
                {
                    // ErrorRaiseCount 초기화
                    AppInfo.Current.ErrorRaiseCount = 0;
                }
            }
        }

        /// <summary>
        /// 프로그램 종료
        /// </summary>
        private static void ExitApplication()
        {
            try
            {
                try
                {
                    foreach (Form frm in Application.OpenForms)
                    {
                        frm.Close();
                    }
                }
                catch { }
                Application.Exit();
            }
            finally
            {
            }
        }
        
    }
}
