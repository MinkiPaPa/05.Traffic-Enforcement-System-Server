using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRA.iTOPS.Launcher
{
    static partial class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            Infragistics.Win.AppStyling.StyleManager.Load(
               TRA.iTOPS.Windows.Set.Utilities.GetEmbeddedResourceStream("TRA.iTOPS.Windows.Set.Styling.IG.isl"));
            //TRA.iTOPS.Windows.Set.Utilities.GetEmbeddedResourceStream("TRA.iTOPS.Windows.Set.Styling.TRA_iTOPS.isl"));
            

            //string[] arrFiles = System.IO.Directory.GetFiles(@"D:\TFS\Solution\MostiSoft-GTF-(GTFramework)\SKI.TMS\SKI.TMS.Launcher\Styling", "*.isl"); Office2007Silver
            //List<string> lstFiles = new List<string>();
            //foreach (string item in arrFiles)
            //{
            //    lstFiles.Add(System.IO.Path.GetFileNameWithoutExtension(item));
            //}
            //TRA.TBOS.Contracts.Session.AppInfo.Current.StyleNames = lstFiles.ToArray();

            StartUpCheck(args);

            //Application.Run(new Form1());

        }
    }
}
