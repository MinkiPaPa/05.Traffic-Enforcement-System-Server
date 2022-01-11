using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //class 추가


namespace TRA.iTOPS.Win.DataPrint
{
    public partial class PrintPreInfoForm : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                                        int size, string filePath);

        private string TiniFile = Directory.GetCurrentDirectory() + "\\TPrint.ini";

        public PrintPreInfoForm()
        {
            InitializeComponent();
        }

        private void PrintPreInfoForm_Load(object sender, EventArgs e)
        {
            StringBuilder retTemp = new StringBuilder();

            //ini 읽기
            GetPrivateProfileString("ENQURY", "ENQ_1", "", retTemp, 100, TiniFile);
            Txt_Tel.Text = retTemp.ToString();

            GetPrivateProfileString("ENQURY", "ENQ_2", "", retTemp, 100, TiniFile);
            Txt_Fax.Text = retTemp.ToString();

            GetPrivateProfileString("ENQURY", "ENQ_3", "", retTemp, 100, TiniFile);
            Txt_Email.Text = retTemp.ToString();

            GetPrivateProfileString("ENQURY", "ENQ_ADDR_1", "", retTemp, 300, TiniFile);
            Txt_Addr0.Text = retTemp.ToString();

            GetPrivateProfileString("ENQURY", "ENQ_ADDR_2", "", retTemp, 300, TiniFile);
            Txt_Addr1.Text = retTemp.ToString();

            GetPrivateProfileString("ENQURY", "ENQ_ADDR_3", "", retTemp, 300, TiniFile);
            Txt_Addr2.Text = retTemp.ToString();

            GetPrivateProfileString("ENQURY", "ENQ_ADDR_4", "", retTemp, 300, TiniFile);
            Txt_Addr3.Text = retTemp.ToString();

            GetPrivateProfileString("ENQURY", "ENQ_ADDR_5", "", retTemp, 300, TiniFile);
            Txt_AddrCode.Text = retTemp.ToString();

            GetPrivateProfileString("ISSUER", "Line_1", "", retTemp, 300, TiniFile);
            Txt_Line0.Text = retTemp.ToString();

            GetPrivateProfileString("ISSUER", "Line_2", "", retTemp, 300, TiniFile);
            Txt_Line1.Text = retTemp.ToString();

            GetPrivateProfileString("ISSUER", "Line_3", "", retTemp, 300, TiniFile);
            Txt_Line2.Text = retTemp.ToString();

            GetPrivateProfileString("ISSUER", "Line_4", "", retTemp, 300, TiniFile);
            Txt_Line3.Text = retTemp.ToString();

            GetPrivateProfileString("ISSUER", "Line_5", "", retTemp, 300, TiniFile);
            Txt_Line4.Text = retTemp.ToString();


        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("ENQURY", "ENQ_1", Txt_Tel.Text, TiniFile);
            WritePrivateProfileString("ENQURY", "ENQ_2", Txt_Fax.Text, TiniFile);
            WritePrivateProfileString("ENQURY", "ENQ_3", Txt_Email.Text, TiniFile);
            WritePrivateProfileString("ENQURY", "ENQ_ADDR_1", Txt_Addr0.Text, TiniFile);
            WritePrivateProfileString("ENQURY", "ENQ_ADDR_2", Txt_Addr1.Text, TiniFile);
            WritePrivateProfileString("ENQURY", "ENQ_ADDR_3", Txt_Addr2.Text, TiniFile);
            WritePrivateProfileString("ENQURY", "ENQ_ADDR_4", Txt_Addr3.Text, TiniFile);
            WritePrivateProfileString("ENQURY", "ENQ_ADDR_5", Txt_AddrCode.Text, TiniFile);
            WritePrivateProfileString("ISSUER", "Line_1", Txt_Line0.Text, TiniFile);
            WritePrivateProfileString("ISSUER", "Line_2", Txt_Line1.Text, TiniFile);
            WritePrivateProfileString("ISSUER", "Line_3", Txt_Line2.Text, TiniFile);
            WritePrivateProfileString("ISSUER", "Line_4", Txt_Line3.Text, TiniFile);
            WritePrivateProfileString("ISSUER", "Line_5", Txt_Line4.Text, TiniFile);

            Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
