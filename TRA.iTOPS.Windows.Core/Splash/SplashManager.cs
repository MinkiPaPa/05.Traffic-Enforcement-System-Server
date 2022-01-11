using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRA.TBOS.Windows.Core.Splash
{
    /// <summary>
    /// Splash 관리자
    /// </summary>
    /// <typeparam name="T">ISplasher를 구현하는 Form</typeparam>
    public class SplashManager<T> where T : ISplasher, new()
    {
        private static Thread _t;
        private static ISplasher _splash;
        private static ManualResetEventSlim _syncer = new ManualResetEventSlim(false);

        /// <summary>
        /// Splash Form을 시작합니다.
        /// </summary>
        /// <returns></returns>
        public static ISplasher Show()
        {
            _t = new Thread(() =>
            {
                if (_splash == null)
                {
                    _splash = new T();

                    Form form = _splash as Form;
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.HandleCreated += Form_HandleCreated;


                    Application.Run(form);
                }
                else
                {
                    Form form = _splash as Form;
                    form.Invoke((Action)(() =>
                    {
                        form.TopMost = true;
                        form.Show();
                        //form.BringToFront();
                    }));
                }
            });
            _t.IsBackground = true;
            _t.SetApartmentState(ApartmentState.STA);
            _t.Start();

            // 핸들생성까지 동기 Wait
            _syncer.Wait();

            return _splash;
        }

        private static void Form_HandleCreated(object sender, EventArgs e)
        {
            // 핸들생성 이후 지속
            _syncer.Set();
        }

        /// <summary>
        /// Splash Form에 메시지를 전송합니다.
        /// </summary>
        /// <param name="message"></param>
        public static void SetMessage(string message)
        {
            if (_splash == null) return;

            Form form = _splash as Form;
            form.Invoke((Action)(() => 
            {
                //form.BringToFront();
                form.TopMost = true;
                form.BringToFront();
                _splash.SetStatus(message);
            }));
        }

        /// <summary>
        /// Splash Form을 종료합니다.
        /// </summary>
        public static void Close()
        {
            if (_splash == null) return;

            Form form = _splash as Form;
            form.Invoke((Action)(() =>
            {
                //form.Close();
                form.Hide();
                _t = null;
            }));
        }
    }
}
