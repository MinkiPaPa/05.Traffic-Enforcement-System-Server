#include "StdAfx.h"
#include "Print_CaseDetail.h"
//#include "IamTBos.h"

CPrintCaseDetail::CPrintCaseDetail(HWND hwnd, int nmode) : CPrint_TBos(hwnd, nmode)
{
}

CPrintCaseDetail::~CPrintCaseDetail(void)
{
}

bool CPrintCaseDetail::Print_content(ONE_CASE* pcase, 
							InfoList_Row* pPayRows,   int nPayRows, 
							InfoList_Row* pPrintRows, int nPrintRows, 
							InfoList_Row* pOwner,     int nOwnerRows,
							InfoList_Row* pChanges,   int nChangeRows)
{
	init_resolution();

	//////////////////////////////////////////////////////////////////////////////
	int nx = 100;
	int ny = (m_nYmax * 1) / 16;
	CString sline;

	Print_CaseInfo(pcase, _T("CASE DETAIL"));

	//////////////////////////////////////////////////////////////////////////////
	// 
	nx = 100;
	ny = (m_nYmax * 8) / 16 - 200;
	sline = _T("Owner Information");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET);

	sline.Format("NAME and ID : %s (%s)", pcase->NaP_NAME, pcase->NaP_IDNUM);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	ny += (LINE_OFFSET);

	sline.Format("Address : %s", pcase->NaP_ADDR1);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());	ny += (LINE_OFFSET);
	sline.Format("          %s", pcase->NaP_ADDR2);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());	ny += (LINE_OFFSET);
	sline.Format("          %s", pcase->NaP_ADDR3);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());	ny += (LINE_OFFSET);
	sline.Format("          %s", pcase->NaP_ACODE);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());	ny += (LINE_OFFSET);

	ny += (LINE_OFFSET);
	// 
	nx = 100;
	sline = _T("Pay status");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET);
	for (int i=0; i<nPayRows; i++) {
		sline.Format("%s : %s", pPayRows[i].pCol, pPayRows[i].pValue);
		TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());	ny += (LINE_OFFSET);
	}
	ny += (LINE_OFFSET);

	nx = 100;
	sline = _T("Printing status");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET);
	for (int i=0; i<nPrintRows; i++) {
		sline.Format("%s : %s", pPrintRows[i].pCol, pPrintRows[i].pValue);
		TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());	ny += (LINE_OFFSET);
	}
	ny += (LINE_OFFSET);

	/*
	// Change History 
	ARR_CHANGES & arr = TBosIs.GetChangesArr();
	if (arr.size() == 0) 
		return true;

	nx = 100;
	sline = _T("Change History");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET);
	for (unsigned int i=0; i<arr.size(); i++) {
		ONE_CHANGE* pTheChange = (ONE_CHANGE*) arr.at(i);
		sline.Format("[%s] PREV: %s -> AFTER: %s (%s, %s)",
			pTheChange->hc_Subject, 
			pTheChange->hc_Before,
			pTheChange->hc_After,
			pTheChange->hc_Operator,
			pTheChange->hc_DateTime);

		TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
		ny += (LINE_OFFSET);
	}
	*/

	return true;
}

