#include "StdAfx.h"
#include "PrintBase.h"


CPrintBase::CPrintBase(HWND hwnd, int nmode) : m_hOwnerWnd(hwnd), m_nMode(nmode) 
{
	m_hPrtdc = NULL;
}

CPrintBase::~CPrintBase()
{
}

bool CPrintBase::print_init_direct()
{
	char	pNamePrinter[100];
	DWORD	nNameSize = 0;
	memset(pNamePrinter, 0, 100);

	GetDefaultPrinter(NULL, &nNameSize);
	if (GetDefaultPrinter(pNamePrinter, &nNameSize) == FALSE)
		return false;

	m_hPrtdc = CreateDC(NULL, pNamePrinter, NULL, NULL);

	return (m_hPrtdc != NULL);
}

bool CPrintBase::print_init_indirect()
{
	// Get the printer information and make DC ... 
	PRINTDLG	pd;
	memset(&pd, 0, sizeof(PRINTDLG));
	pd.lStructSize = sizeof(PRINTDLG);
	pd.Flags = PD_RETURNDC;
	pd.hwndOwner = m_hOwnerWnd;
	pd.nFromPage = 1;
	pd.nToPage = 1;
	pd.nMinPage = 1;
	pd.nMaxPage = 1;
	pd.nCopies = 1;

	PrintDlg(&pd);

	m_hPrtdc = pd.hDC;

	return (m_hPrtdc != NULL);
}

bool CPrintBase::Print_init()
{
	bool bok = true;
	if (m_nMode == PRINT_DIRECT)  {
		bok = print_init_direct();
	}
	else {
		bok = print_init_indirect();
	}

	if (bok == false) {
		return false;
	}

	// start PRINTING ... 
	DOCINFO		doc;
	doc.cbSize = sizeof(DOCINFO);
	doc.lpszDocName = "TEST IMAGE ...";
	doc.lpszOutput  = NULL;
	doc.lpszDatatype = NULL;
	doc.fwType = 0;

	if (StartDoc(m_hPrtdc, &doc) <=0 ) {
		DeleteDC(m_hPrtdc);
		return false;
	}
	if (StartPage(m_hPrtdc) <= 0) {
		DeleteDC(m_hPrtdc);
		return false;
	}

	return bok;
}

bool CPrintBase::Print_fini()
{
	// end PRINTiNG ... 
	if (EndPage(m_hPrtdc) > 0)  {
		EndDoc(m_hPrtdc);
	}

	DeleteDC(m_hPrtdc);
	return true;
}

