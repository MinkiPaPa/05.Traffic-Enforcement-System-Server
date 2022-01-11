using TRA.iTOPS.Windows.Set.MainApp;
using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using System;
using System.Data;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

namespace TRA.iTOPS.Win
{
    public partial class SearchForm : FormBase
    {
        private bool bDone = false;

        public SearchForm()
        {
            InitializeComponent();
        }

        #region #EVENT
        /// <summary>
        /// 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SearchForm_Load(object sender, EventArgs e)
        {
            Btn_Detail.Enabled = false;
            Edt_Max.Value = 1000;

            Dt_Start.Value = null;
            Dt_End.Value = null;

            Rb_Dontcare.Checked = true;


            this.ActiveControl = Btn_search;
            Btn_search.Focus();

            SetGridInit();

        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            DataSet ds = SelectSearchCase();

            Grd_Search.DataSource = ds.Tables[0];
            Grd_Search.DataBind();
            Grd_Search.UpdateData();

            if (Grd_Search.Rows.Count > 0)
            {
                Btn_Detail.Enabled = true;

                //Grd_Search.Rows[2].Activate();

                this.ActiveControl = Btn_Detail;
                Btn_Detail.Focus();
            }

        }


        private void Btn_Close_Click(object sender, EventArgs e)
        {
            if (bDone)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void Btn_Detail_Click(object sender, EventArgs e)
        {
            //if (Grd_Search.Selected.Rows.Count < 1)
            if (Grd_Search.ActiveRow.Activated == false)
            {
                MessageBox.Show("No data Selected.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowCasePanel();
        }

        private void Grd_Search_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            //if (Grd_Search.Selected.Rows.Count < 1)
            if (Grd_Search.ActiveRow.Activated == false)
            {
                MessageBox.Show("No data Selected.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowCasePanel();
        }

        private void Grd_Search_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Cells.Exists("FINE"))
                e.Row.Cells["FINE"].Value = Convert.ToSingle(e.Row.Cells["FINE"].Value).ToString("N2");  //  소숫점 2자리만 나오게

        }

        #endregion

        #region # METHOD
        private void SetGridInit()
        {
            Grd_Search.DataSource = null;

            Grd_Search.DisplayLayout.Bands[0].Columns.ClearUnbound();

            //그리드를 완전 초기화 한다.
            Grd_Search.ResetDisplayLayout();
            //그리드의 캡션을 숨긴다.
            Grd_Search.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

            //컬럼 넓이를 자동으로 조정한다
            Grd_Search.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            //그리드 클릭시 Row전체를 선택한게 한다.
            Grd_Search.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            //그리드 헤더와셀의 텍스트를 가운데로 정렬한다.
            Grd_Search.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Grd_Search.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            // 모두 텍스트 스타일로 나오도록(이거안하면, 날짜부분에 달력이 생김)
            Grd_Search.DisplayLayout.Bands[0].Override.CellDisplayStyle = CellDisplayStyle.FormattedText;

            // 홀수줄 바탕색
            //Grd_Search.DisplayLayout.Override.RowAlternateAppearance.BackColor =
            //    System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(247)), ((System.Byte)(251)));
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("CUID", "cuid");
            Grd_Search.DisplayLayout.Bands[0].Columns["CUID"].Hidden = true;
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("ROWNUM", "Order");
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("NOTICE_NUM", "Notice Number");
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("CARNUM", "Car Number");
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("WHEN_DT", "Offence Date");
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("FINE", "Fine");
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("OFFICER", "officer");
            //Grd_Search.DisplayLayout.Bands[0].Columns.Add("PAYED", "Payed");
            //Grd_Search.DisplayLayout.Bands[0].Columns.Add("PAY_TIME", "Pay Date");
            //Grd_Search.DisplayLayout.Bands[0].Columns.Add("PAYED_RECEIPT", "Receipt Num");
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("N016_P_NAME", "Owner");
            Grd_Search.DisplayLayout.Bands[0].Columns.Add("STAUS_DESC", "Status");

        }


        private void ShowCasePanel()
        {
            bDone = true;
            CasePanelForm frm = new CasePanelForm();
            frm.StrCuid = Grd_Search.ActiveRow.Cells["CUID"].Value.ToString();

            frm.ShowDialog();
        }


        private DataSet SelectSearchCase()
        {
            SearchBiz searchBiz = new SearchBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //int nMaxRecord = Convert.ToInt32(Edt_Max.Value);

            string startDate = string.Empty;
            string EndDate = string.Empty;

            DateTime dttemp;

            if (Dt_Start.Value != null)
            {
                dttemp = Dt_Start.DateTime;
                startDate = dttemp.ToString("yyyy-MM-dd");
            }

            if (Dt_End.Value != null)
            {
                dttemp = Dt_End.DateTime;
                EndDate = dttemp.ToString("yyyy-MM-dd 23:59:59");
            }

            req.Parameters.Add("CARNUM", Txt_Carnum.Text);
            req.Parameters.Add("N015_P_IDNUM", Txt_ID.Text);
            req.Parameters.Add("N016_P_NAME", Txt_Name.Text);
            req.Parameters.Add("NOTICE_NUM", Txt_NoticeNum.Text);
            req.Parameters.Add("OFFICER", Txt_Officer.Text);
            req.Parameters.Add("S_DT", startDate);
            req.Parameters.Add("E_DT", EndDate);
            req.Parameters.Add("MAX", Edt_Max.Value);

            //DB Query
            //TTOPSReply reply = searchBiz.SelectSearchCase(req, Txt_Carnum.Text, Txt_ID.Text, Txt_Name.Text, Txt_NoticeNum.Text, startDate, EndDate, nMaxRecord);
            TTOPSReply reply = searchBiz.SelectSearchCase(req);

            return reply.ResultSet;
        }
        #endregion

        private void Rb_Dontcare_CheckedChanged(object sender, EventArgs e)
        {
            if(Rb_Dontcare.Checked == true)
                OnConditionCaseState(0);
        }


        private void Rb_StillProgress_CheckedChanged(object sender, EventArgs e)
        {
            if(Rb_StillProgress.Checked == true)
                OnConditionCaseState(1);
        }

        private void RB_Closed_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Closed.Checked == true)
                OnConditionCaseState(2);

        }

        private void OnConditionCaseState(int iState)
        {
            if (iState == 1)
            {
                Chk_Courtroll.Enabled = true;
                Chk_DownNatis.Enabled = true;
                Chk_Initial.Enabled = true;
                Chk_NBS.Enabled = true;
                Chk_Notice.Enabled = true;
                Chk_Summon.Enabled = true;
                Chk_upNatis.Enabled = true;
                Chk_WOA.Enabled = true;

                Chk_CCourt.Enabled = false;
                Chk_CCancel.Enabled = false;
                Chk_CPaid.Enabled = false;

            }
            else if(iState == 0)
            {
                Chk_Courtroll.Enabled = false;
                Chk_DownNatis.Enabled = false;
                Chk_Initial.Enabled = false;
                Chk_NBS.Enabled = false;
                Chk_Notice.Enabled = false;
                Chk_Summon.Enabled = false;
                Chk_upNatis.Enabled = false;
                Chk_WOA.Enabled = false;

                Chk_CCourt.Enabled = false;
                Chk_CCancel.Enabled = false;
                Chk_CPaid.Enabled = false;

            }
            else if(iState == 2)
            {
                Chk_DownNatis.Enabled = false;
                Chk_Initial.Enabled = false;
                Chk_NBS.Enabled = false;
                Chk_Notice.Enabled = false;
                Chk_Summon.Enabled = false;
                Chk_upNatis.Enabled = false;
                Chk_WOA.Enabled = false;
                Chk_Courtroll.Enabled = false;

                Chk_CCourt.Enabled = true;
                Chk_CCancel.Enabled = true;
                Chk_CPaid.Enabled = true;

            }

        }

    }
}
