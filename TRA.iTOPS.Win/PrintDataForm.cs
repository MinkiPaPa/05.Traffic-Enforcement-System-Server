using TRA.iTOPS.Biz;
using TRA.iTOPS.Contracts;
using TRA.iTOPS.Contracts.Common;
using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraMessageBox;
using Infragistics.Win.UltraWinListView;

namespace TRA.iTOPS.Win
{
    public partial class PrintDataForm : Form
    {
        public PrintDataForm()
        {
            InitializeComponent();
        }

        private void DataPrintForm_Load(object sender, EventArgs e)
        {
            Lv_History.ViewSettingsDetails.ImageSize = Size.Empty;
            Lv_History.ViewSettingsDetails.FullRowSelect = true;

            Rb_1stNotice.Checked = true;
            Chk_Duedate.Checked = true;

            Btn_Print.Enabled = false;

            Update_condition();

            History_update();

            SetGridInit();
        }

        private void History_update()
        {
            Lv_History.Items.Clear();

            DataSet ds = SelectHistoryInfo(ITOPS_State.ITOPS_STATE_NOTICE);

            if (ds.Tables[0].Rows.Count > 0)
            {
                UltraListViewItem item;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new UltraListViewItem(ITOPS_State.ConvertDBDate(dr["CTIME"].ToString()), new Object[] { dr["ACTION_CODE"].ToString(), dr["COMMENT"].ToString(), dr["ACTION_CODE_AUX"].ToString()});
                    Lv_History.Items.Add(item);
                }
            }
        }

        private DataSet SelectHistoryInfo(int ncase)
        {
            //DataSet DS = new DataSet();
            StateBiz stateBiz = new StateBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            //DB Query
            TTOPSReply reply = stateBiz.SelectHistory(req, ncase);

            return reply.ResultSet;
        }


        private DataSet SelectPrintCase()
        {
            DataPrintBiz printBiz = new DataPrintBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();
            int nWhichDoc = -1;

            if (Rb_1stNotice.Checked == true)
                nWhichDoc = 0;
            else if (Rb_NoticeReminder.Checked == true)
                nWhichDoc = 1;
            else if (Rb_NBS.Checked == true)
                nWhichDoc = 2;
            else if (Rb_Summon.Checked == true)
                nWhichDoc = 3;
            else if (Rb_WOA.Checked == true)
                nWhichDoc = 4;
            else if (Rb_CourtRoll.Checked == true)
                nWhichDoc = 5;

            //int nMaxRecord = Convert.ToInt32(Edt_Max.Value);
            string OffenceDate = string.Empty;
            if (Chk_OffenceDate.Checked == true)
            {
                OffenceDate = Convert.ToString(Dt_OffenceDate.Value);
            }


            req.Parameters.Add("WHICHDOC", nWhichDoc);
            req.Parameters.Add("TODAY_DATE", Dt_Today.Value);
            req.Parameters.Add("PENDINGDAY", Ud_days.Value);
            req.Parameters.Add("OFFENCEDATE", OffenceDate);
            req.Parameters.Add("NNA", Txt_NNA.Value);
            req.Parameters.Add("NNB1", Txt_NNB1.Value);
            req.Parameters.Add("NNB2", Txt_NNB2.Value);

            //DB Query
            TTOPSReply reply = printBiz.SelectPrintCaseInfo(req, Chk_Ignore_prev.Checked, Chk_Reprint.Checked);

            return reply.ResultSet;
        }


        private void Grd_PrintView_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            UltraGridBand band = layout.Bands[0];
            UltraGridOverride ov = layout.Override;

            string checkBoxColumnKey = "Select";
            if (false == band.Columns.Exists(checkBoxColumnKey))
            {
                UltraGridColumn column = band.Columns.Add(checkBoxColumnKey);
                column.DataType = typeof(bool);
                //column.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                //column.CellActivation = Activation.AllowEdit;

                column.Header.CheckBoxAlignment = HeaderCheckBoxAlignment.Left;
                column.Header.CheckBoxSynchronization = HeaderCheckBoxSynchronization.RowsCollection;
                column.Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
                column.Header.VisiblePosition = 0;
                
            }

            // 체크박스 컬럼만 빼고 나머지 컬럼은 편집할수 없도록 한다
            foreach (UltraGridColumn Datacolumn in band.Columns)
            {
                // Column 0 is the checkbox column, so leave it alone. 
                if (Datacolumn.Key == checkBoxColumnKey)
                    continue;

                Datacolumn.CellClickAction = CellClickAction.RowSelect;
                Datacolumn.CellActivation = Activation.NoEdit;
            }


        }

        private void Rb_1stNotice_CheckedChanged(object sender, EventArgs e)
        {
            Update_condition();
        }

        private void Rb_NoticeReminder_CheckedChanged(object sender, EventArgs e)
        {
            Update_condition();
        }

        private void Rb_NBS_CheckedChanged(object sender, EventArgs e)
        {
            Update_condition();
        }

        private void Rb_Summon_CheckedChanged(object sender, EventArgs e)
        {
            Update_condition();
        }

        private void Rb_WOA_CheckedChanged(object sender, EventArgs e)
        {
            Update_condition();
        }

        private void Rb_CourtRoll_CheckedChanged(object sender, EventArgs e)
        {
            Update_condition();
        }

        private void Chk_OffenceDate_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_OffenceDate.Checked == true)
            {
                Dt_OffenceDate.Enabled = true;
            }
            else
                Dt_OffenceDate.Enabled = false;

        }

        private void Chk_Reprint_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Reprint.Checked == true)
            {
                Chk_Ignore_prev.Enabled = true;
                Txt_NNA.Enabled = true;
                Txt_NNB1.Enabled = true;
                Txt_NNB2.Enabled = true;
                Lbl_NoticeNumber.Enabled = true;
                Lbl_comma.Enabled = true;
                Lbl_comma.Enabled = true;

            }
            else
            {
                Chk_Ignore_prev.Enabled = false;
                Txt_NNA.Enabled = false;
                Txt_NNB1.Enabled = false;
                Txt_NNB2.Enabled = false;
                Lbl_NoticeNumber.Enabled = false;
                Lbl_comma.Enabled = false;
                Lbl_comma.Enabled = false;

            }

        }

        private void Chk_Duedate_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Duedate.Checked == true)
                Dt_DueDate.Enabled = true;
            else
                Dt_DueDate.Enabled = false;

        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (!Chk_Duedate.Checked)
            {
                //if (MessageBox.Show("Due Date is not set\n\n Set it again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                if(ShowUltraMessage("Due Date is not set\n\n Set it again?", 1) == DialogResult.Yes)
                {
                    this.ActiveControl = Chk_Duedate;
                    Chk_Duedate.Focus();
                    return;
                }
            }

            DataSet ds = SelectPrintCase();

            Grd_PrintView.DataSource = ds.Tables[0];
            Grd_PrintView.DataBind();
            Grd_PrintView.UpdateData();

            if (Grd_PrintView.Rows.Count > 0)
            {
                Btn_Print.Enabled = true;
            }
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            if(Chk_Duedate.Checked == false )
            {
                if(ShowUltraMessage("Due Date is not set\n\n Set it again?", 1) == DialogResult.Yes)
                {
                    this.ActiveControl = Chk_Duedate;
                    Chk_Duedate.Focus();
                    return;
                }
            }

            List<UltraGridRow> checkList = GetCheckedRows(Grd_PrintView, "Select");

            if(checkList.Count > 0)
            {
                DataPrint.PrintDataOptionForm frm = new DataPrint.PrintDataOptionForm()
                {
                    CheckList = checkList,
                    bDBUpdate = Chk_Duedate.Checked
                };

                frm.ShowDialog();

                if(frm.UpdateState == true)
                {
                    InsertHistory(ITOPS_State.ITOPS_STATE_NOTICE, 1, "Print Notice", "OK");
                    History_update();
                }
                  
            }
            else
            {

                ShowUltraMessage("Please select items");
                //MessageBox.Show("Please select items", "iTOPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private bool InsertHistory(int nStatus, int nAux, string Comment, string Comment2)
        {
            StateBiz stateBiz = new StateBiz();

            //파라미터 객체 생성
            TTOPSRequest req = new TTOPSRequest();

            req.Parameters.Add("ACTION_CODE", nStatus);
            req.Parameters.Add("ACTION_CODE_AUX", nAux);
            req.Parameters.Add("COMMENT", Comment);
            req.Parameters.Add("COMMENT_AUX", Comment2);

            TTOPSReply reply = stateBiz.InsertHistory(req);

            if (reply.ResultCode == "OK")
                return true;
            else
                return false;

        }


        private List<UltraGridRow> GetCheckedRows(UltraGrid grid, string checkBoxColumnKey)
        {
            // Build a list of checked rows. 
            var checkedRows = new List<UltraGridRow>();

            // Get all of the data rows (ignoring grouping) that arenot filtered out. 
            var allDataRows = grid.Rows.GetFilteredInNonGroupByRows();

            // If a cell is in edit mode, then the Value property will returning the underlying
            // value and will not reflect pending changes on the screen that have not yet
            // been committed. So see if there's an ActiveCell in edit mode in the checkbox
            // column, because if there is, we will need to handle that differently. 
            var checkBoxColumn = grid.DisplayLayout.Bands[0].Columns[checkBoxColumnKey];
            var activeCell = grid.ActiveCell;
            bool hasCellInEditMode = null != activeCell &&
                0 == string.Compare(activeCell.Column.Key, checkBoxColumnKey, false) &&
                activeCell.IsInEditMode;

            foreach (var row in allDataRows)
            {
                bool isRowChecked;

                // If there's an active cell in edit mode in the checkbox column
                // we need to get it's checked state from the text, not the value, since
                // the value is still pending committment. 
                if (hasCellInEditMode &&
                    activeCell.Row == row)
                {
                    // Get the text of the cell and parse it to a bool. 
                    // Also, use GetCellText instead of row.Cells["checkBoxColumn"].Text
                    // because this is more efficient and doesn't force the creation 
                    // of the UltraGridCell object. 
                    isRowChecked = bool.Parse(row.GetCellText(checkBoxColumn));
                }
                else
                {
                    // GetCellValue instead of row.Cells["checkBoxColumn"].Value
                    // because this is more efficient and doesn't force the creation 
                    // of the UltraGridCell object. 
                    isRowChecked = (bool)row.GetCellValue(checkBoxColumnKey);
                }

                // If this row is checked, add it to the list. 
                if (isRowChecked)
                    checkedRows.Add(row);
            }


            // Return the list of all checked rows. 
            return checkedRows;
        }
        private void SetGridInit()
        {
            Grd_PrintView.DataSource = null;

            Grd_PrintView.DisplayLayout.Bands[0].Columns.ClearUnbound();

            //그리드를 완전 초기화 한다.
            Grd_PrintView.ResetDisplayLayout();

            //그리드의 캡션을 숨긴다.
            Grd_PrintView.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

            //그리드 헤더와셀의 텍스트를 가운데로 정렬한다.
            Grd_PrintView.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Grd_PrintView.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            // 파일 디렉토리는 나중에 이미지 파일 다운 받을때 필요
            Grd_PrintView.DisplayLayout.Bands[0].Columns.Add("FILE_DIRECTORY", "directory");
            Grd_PrintView.DisplayLayout.Bands[0].Columns["FILE_DIRECTORY"].Hidden = true;

        }

        private void Update_condition()
        {
            if (Rb_1stNotice.Checked == true)
            {
                Lbl_and.Visible = false;
                Dt_Today.Visible = false;
                Lbl_To.Visible = false;
                Txt_lastDate.Visible = false;
                Lbl_bigger.Visible = false;
                Ud_days.Visible = false;
                Lbl_Days.Visible = false;

                Lbl_Status.Text = "init(NATIS down)";
            }
            else
            {
                Lbl_and.Visible = true;
                Dt_Today.Visible = true;
                Lbl_To.Visible = true;
                Txt_lastDate.Visible = true;
                Lbl_bigger.Visible = true;
                Ud_days.Visible = true;
                Lbl_Days.Visible = true;

                if (Rb_NoticeReminder.Checked == true)
                {
                    Lbl_Status.Text = "Noticed";
                    Txt_lastDate.Text = "Last 1st Notice Issue Date";
                }
                else if (Rb_NBS.Checked == true)
                {
                    Lbl_Status.Text = "Noticed";
                    Txt_lastDate.Text = "Last Notice Issue Date";
                }
                else if (Rb_Summon.Checked == true)
                {
                    Lbl_Status.Text = "NBS issued";
                    Txt_lastDate.Text = "Last NBS Issue Date";
                }
                else if (Rb_WOA.Checked == true)
                {
                    Lbl_Status.Text = "Summon issued";
                    Txt_lastDate.Text = "Last Summon Issue Date";
                }
                else if (Rb_CourtRoll.Checked == true)
                {
                    Lbl_Status.Text = "WOA issued";
                    Txt_lastDate.Text = "Last WOA Issue Date";
                }

            }

        }

        private DialogResult ShowUltraMessage(string message, int opt = 0)
        {
            using (UltraMessageBoxInfo ultraMessageBoxInfo = new UltraMessageBoxInfo())
            {
                // Set the general display style
                ultraMessageBoxInfo.Style = MessageBoxStyle.Default;

                // Set the text for the Text, Caption, Header and Footer
                ultraMessageBoxInfo.Text = message;
                ultraMessageBoxInfo.Caption = this.Text;
                //ultraMessageBoxInfo.Header = "New Castle Municipality Traffic Department";
                ultraMessageBoxInfo.Footer = "iTOPS";

                // Specify which buttons should be used and which is the default button
                if(opt == 0)
                    ultraMessageBoxInfo.Buttons = MessageBoxButtons.OK;
                else
                    ultraMessageBoxInfo.Buttons = MessageBoxButtons.YesNo;

                ultraMessageBoxInfo.DefaultButton = MessageBoxDefaultButton.Button2;
                ultraMessageBoxInfo.ShowHelpButton = Infragistics.Win.DefaultableBoolean.False;

                // Display the OS Error Icon
                ultraMessageBoxInfo.Icon = MessageBoxIcon.Information;

                // Disable the default sounds
                ultraMessageBoxInfo.EnableSounds = Infragistics.Win.DefaultableBoolean.True;


                // Show the UltraMessageBox
                return this.ultraMessageBoxManager1.ShowMessageBox(ultraMessageBoxInfo);
            }
        }

    }
}
