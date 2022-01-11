#include "StdAfx.h"
#include "Print_PayReceipt.h"
//#include "TBosCommonLook.h"

CPrint_PayReceipt::CPrint_PayReceipt(HWND hwnd, int nmode) : CPrint_TBos(hwnd, nmode)
{
}

CPrint_PayReceipt::~CPrint_PayReceipt(void)
{
}

bool CPrint_PayReceipt::Print_content(ONE_CASE* pcase)
{
	init_resolution();

		//////////////////////////////////////////////////////////////////////////////
	int nx = 100;
	int ny = (m_nYmax * 1) / 16;
	CString sline;

	Print_CaseInfo(pcase, _T("PAY RECEIPT"));

	//////////////////////////////////////////////////////////////////////////////
	// 
	nx = 100;
	ny = (m_nYmax * 8) / 16 + 100;
	sline = _T("Pay information");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET*2);

	nx = 1500;
	sline = _T("PROVINCIAL TRAFFIC");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	ny += (LINE_OFFSET*2);

	nx = 500;
	sline.Format("RECEIPT NUMBER       : %s", pcase->k_PayBillNum);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	ny += (LINE_OFFSET*2);

	sline = _T("RECEIVED IN TERMS OF CRIMINAL PROCEDURE ACT OF ACT51 OF 1977");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	//sline.Format("SURNAME              : %s", pcase->p_pNameSur);
	//TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format("NAME/NAAM            : %s", pcase->NaP_NAME); // p_pName1st
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format("ADDRESS/ADRES        : %s", pcase->NaP_ADDR1); // p_pAddr_1
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	sline.Format("                       %s", pcase->NaP_ADDR2);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	sline.Format("                       %s", pcase->NaP_ADDR3);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	sline.Format("                       %s", pcase->NaP_ACODE); // p_pAddr_Code
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	ny += LINE_OFFSET;

	sline.Format("PAYER NAME/NAAM      : %s", pcase->k_PayerName);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	sline.Format("PAYER PHONE NUMBER   : %s", pcase->k_PayerPhone);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format("FINE / BOETE         :         ............. R   %s.00", pcase->p3_Fine);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	if (pcase->k_Fine2 > 0) { 
		sline.Format( _T("  reduced into   :         ............. R %5d.00"), pcase->k_Fine2);
		TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	}

#ifdef _MINIMIZE_LINES
	sline.Format("RECEIVED THE TOTAL AMOUNT OF");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	sline.Format("                               ............. R %5d.00", pcase->k_Payed);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
#else
	sline.Format("RECEIVED THE TOTAL AMOUNT OF ............... R %5d.00", pcase->k_Payed);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
#endif
	sline.Format("PAY METHOD           : %s", ThePayMethods[pcase->k_PayType].pMethod);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format("PAY DATE             : %s", pcase->k_PayTime);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format("PAY POINT            : %s", pcase->k_PayPoint);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format("Casher               : %s", pcase->k_PayCasher);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	return true;
}

