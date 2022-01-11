using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infragistics.Win.UltraMessageBox;


namespace TRA.TBOS.Windows.Core.MainApp
{
    /// <summary>
    /// SKI.TMS 공통 Base Form 클래스
    /// </summary>
    public partial class FormBase : Form
    {
        private Infragistics.Win.UltraWinForm.UltraFormManager _ultraFormManager1 = null;
        private const string MESSAGE_FOOTER = "Traffic BackOffice System";
        //protected MDIMainForm MDI = null;
        //protected MDIMainForm2 MDI = null;

        #region 생성자
        /// <summary>
        /// 생성자
        /// </summary>
        public FormBase()
        {
            InitializeComponent();
            this.InitailzeForm();
        }

        /// <summary>
        /// 초기화 할 지 여부
        /// </summary>
        /// <param name="IsInit">false이면 InitializeComponent()를 호출 하지 않는다. </param>
        public FormBase(bool IsInit)
        {
            if (IsInit)
                this.InitializeComponent();

            this.InitailzeForm();
        }
        #endregion

        /// <summary>
        /// FormBase 공통 초기화
        /// </summary>
        private void InitailzeForm()
        {
            this._ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this._ultraFormManager1.Form = this;

        }

        public string MenuId { get; set; }

        #region 메세지 박스
        /// <summary>
        /// 공통 메세지 박스 정의
        /// </summary>
        /// <param name="messageType">MessageType이 Custom인 경우 MessageBoxIcon, MessageBoxButtons 값을 직접 설정 할 수 있습니다. </param>
        /// <param name="message"></param>
        /// <param name="headerMessage"></param>
        /// <param name="messageBoxIcon"></param>
        /// <param name="messageBoxButtons"></param>
        /// <returns></returns>
        /// <example>
        /// DialogResult dlgResult = this.ShowMesage(MessageType.Inform, "방가");
        /// </example>
        public DialogResult ShowMesage(MessageType messageType, string message,
            string headerMessage = "",
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information,
            MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK,
            IContainer container = null)
        {
            if (container == null)
                container = this.components;

            UltraMessageBoxManager ultraMessageBoxManager1 = new UltraMessageBoxManager();
            ultraMessageBoxManager1.ContainingControl = this;

            DialogResult dlgResult = DialogResult.None;
            using (UltraMessageBoxInfo ultraMessageBoxInfo = this.GetDefaultMessagBoxInfo())
            {
                //ultraMessageBoxInfo.TextFormatted = message;
                ultraMessageBoxInfo.Text = message;
                ultraMessageBoxInfo.HeaderFormatted = headerMessage;

                // 메세지박스 타입에 따른 설정
                // Specify which buttons should be used and which is the default button
                ultraMessageBoxInfo.Icon = messageBoxIcon;
                ultraMessageBoxInfo.Buttons = messageBoxButtons;
                ultraMessageBoxInfo.Caption = messageType.ToString();

                // Display the OS Error Icon
                switch (messageType)
                {
                    case MessageType.Confirm:
                        ultraMessageBoxInfo.Icon = MessageBoxIcon.Question;
                        ultraMessageBoxInfo.Buttons = MessageBoxButtons.YesNo;
                        ultraMessageBoxInfo.DefaultButton = MessageBoxDefaultButton.Button2;
                        break;
                    case MessageType.Inform:
                        ultraMessageBoxInfo.Icon = MessageBoxIcon.Information;
                        ultraMessageBoxInfo.Buttons = MessageBoxButtons.OK;
                        break;
                    case MessageType.Warning:
                        ultraMessageBoxInfo.Icon = MessageBoxIcon.Warning;
                        ultraMessageBoxInfo.Buttons = MessageBoxButtons.OK;
                        break;
                    case MessageType.Error:
                        ultraMessageBoxInfo.Icon = MessageBoxIcon.Error;
                        ultraMessageBoxInfo.Buttons = MessageBoxButtons.OK;
                        break;
                }

                // Show the UltraMessageBox
                dlgResult = ultraMessageBoxManager1.ShowMessageBox(ultraMessageBoxInfo);
            }

            return dlgResult;
        }

        /// <summary>
        /// UltraMessageBoxManager 개체를 생성 후 반환 
        /// </summary>
        /// <returns></returns>
        protected UltraMessageBoxManager GetUltraMessageBoxManager()
        {
            UltraMessageBoxManager ultraMessageBoxManager1 = new UltraMessageBoxManager(this.components);
            ultraMessageBoxManager1.ContainingControl = this;

            return ultraMessageBoxManager1;
        }

        /// <summary>
        /// 기본 MessagBoxInfo 정보를 생성 후 반환 </br>
        /// DialogResult dlgResult = ultraMessageBoxManager1.ShowMessageBox(ultraMessageBoxInfo); 로 호출
        /// </summary>
        /// <returns></returns>
        private UltraMessageBoxInfo GetDefaultMessageBox()
        {
            UltraMessageBoxInfo ultraMessageBoxInfo = new UltraMessageBoxInfo();
            // 기본설정
            ultraMessageBoxInfo.FooterFormatted = MESSAGE_FOOTER;
            ultraMessageBoxInfo.Style = MessageBoxStyle.Standard;
            ultraMessageBoxInfo.EnableSounds = Infragistics.Win.DefaultableBoolean.False;
            ultraMessageBoxInfo.ShowHelpButton = Infragistics.Win.DefaultableBoolean.False;
            ultraMessageBoxInfo.MinimumWidth = 400;

            // 메세지박스 타입에 따른 설정
            // Specify which buttons should be used and which is the default button
            ultraMessageBoxInfo.Icon = MessageBoxIcon.Information;
            ultraMessageBoxInfo.Buttons = MessageBoxButtons.OK;

            return ultraMessageBoxInfo;
        }

        /// <summary>
        /// 사용자 커스터마이징 지원
        /// </summary>
        /// <returns></returns>
        protected UltraMessageBoxInfo GetDefaultMessagBoxInfo()
        {
            return GetDefaultMessageBox();
        }

        #endregion

        private void FormBase_Load(object sender, EventArgs e)
        {
            //MDI = this.MdiParent as MDIMainForm;
            //MDI = this.MdiParent as MDIMainForm2; // 
        }
    }




}
