#include "StdAfx.h"
#include "IssuePrint.h"

CIssuePrint::CIssuePrint(void) : m_pTheDC(NULL), m_pThePage(NULL)
{
	m_sDocName = _T("Tprint - ");  // default doc name 

	m_baseX = 0.0;
	m_baseY = 0.0;

	ready_print_doc();
}

CIssuePrint::~CIssuePrint(void)
{
	if (m_pTheDC && m_pTheDC->m_hDC) {
		m_pTheDC->EndDoc();
		m_pTheDC->Detach();
	}

	delete m_pTheDC;
	delete m_pThePage;
}

void CIssuePrint::ready_print_doc()
{
	CPrintDialog	printDlg(FALSE, PD_ALLPAGES);
	printDlg.m_pd.nCopies = 1;
	printDlg.GetDefaults();

	// 
	//m_pTheDC = new CDC(printDlg.GetPrinterDC());  // 주석처리 [정병영]
	m_pTheDC = new CDC();
	HDC hdc_printer = printDlg.GetPrinterDC();

	if (hdc_printer == NULL) 
		return;

	m_pTheDC->Attach(hdc_printer);

	m_pTheDC->StartDoc(m_sDocName);

	// 
	int nXmax = GetDeviceCaps(hdc_printer, HORZRES);
	int nYmax = GetDeviceCaps(hdc_printer, VERTRES);

	m_rectDraw.left = m_rectDraw.top = 0;
	m_rectDraw.right = nXmax;
	m_rectDraw.bottom = nYmax;

	m_pThePage = new CPage(m_rectDraw, (CWrapCDC*) m_pTheDC);


}

