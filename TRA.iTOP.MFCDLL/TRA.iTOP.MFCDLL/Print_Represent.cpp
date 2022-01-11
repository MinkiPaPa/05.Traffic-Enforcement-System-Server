#include "StdAfx.h"
#include "Print_Represent.h"

DescribeStatus  TheRepresentDesc[] =
{
	{ 0,  _T("reduce fine into")                , _T("")  },
	{ 1,  _T("cancel the case")                 , _T("")  },
	{ 2,  _T("refuse represent")                , _T("")  },
	{ 3,  _T("change owner info")               , _T("")  },
};

DescribeStatus  TheRepresentMethod[] =
{
	{ 0,  _T("visit")                , _T("")  },
	{ 1,  _T("email")                , _T("")  },
	{ 2,  _T("phone")                , _T("")  },
	{ 3,  _T("others")               , _T("")  },
};


CPrintRepresent::CPrintRepresent(HWND hwnd, int nmode) : CPrint_TBos(hwnd, nmode)
{
}

CPrintRepresent::~CPrintRepresent(void)
{
}

bool CPrintRepresent::Print_content(ONE_CASE* pcase, ONE_REPRESENT* prepre)
{
	init_resolution();

	//////////////////////////////////////////////////////////////////////////////
	int nx = 100;
	int ny = (m_nYmax * 1) / 16;
	CString sline;

	Print_CaseInfo(pcase, _T("REPRESENTATION REPORT"));

	//////////////////////////////////////////////////////////////////////////////
	// 
	nx = 100;
	ny = (m_nYmax * 8) / 16 + 100;
	sline = _T("Representation is ...");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET*2);

	sline.Format( _T("Decison Maker    : %s"), prepre->r_pDecisionWho);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Decison Date     : %s"), prepre->r_pDecisionDT);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Decison Place    : %s"), prepre->r_pDecisionWhere);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	if (prepre->r_nDecisionIs == 0)
		sline.Format(_T("Decison Is       : %s (%d)"), 
			TheRepresentDesc[prepre->r_nDecisionIs].pAction, prepre->r_nFineReduced);
	else 
		sline.Format(_T("Decison Is       : %s"), 
			TheRepresentDesc[prepre->r_nDecisionIs].pAction);

	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Decison Doc Num. : %s"), prepre->r_pDecisionDocNum);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	ny += LINE_OFFSET;
	sline.Format( _T("Issuer Name      : %s"), prepre->r_pIssuerWho);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Issuer Phone     : %s"), prepre->r_pIssuerPhone);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Issue method     : %s"), 
		TheRepresentMethod[prepre->r_nIssuerMethod].pAction);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Issuer Claim is  : "), prepre->r_pIssuerClaim);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	CString sclaim = prepre->r_pIssuerClaim;
	int nclaim_length = sclaim.GetLength();

	for (int i=0; ; i++) {
		if ((i*80) > nclaim_length)
			break;
		sline = sclaim.Mid(i*80, nclaim_length-(i*80));
		TextOut(m_hPrtdc, nx+1000, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;
	}

	ny += LINE_OFFSET;
	sline.Format( _T("Capturer info    : %s"), prepre->r_pCapturer);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Capture  time    : %s"), prepre->r_pDateTime);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	return true;
}

