#include "StdAfx.h"
#include "Print_TBos.h"
//#include "encrypt.h"
//#include "IamTBos.h"

CPrint_TBos::CPrint_TBos(HWND hwnd, int nmode) : CPrintBase(hwnd, nmode)
{
	m_nXmax = 0;
	m_nYmax = 0;
}

CPrint_TBos::~CPrint_TBos(void)
{
}

void CPrint_TBos::print_image_there(LPCSTR pfilename, CRect rectDraw)
{
	/*
	CString sfile_jpg;
	sfile_jpg.Format(_T("%s\\%s"), "Z", pfilename);

	CString stemp_tofile = _T("C:\\TEMP\\tbos_print.jpg");
	CopyFile(sfile_jpg, stemp_tofile, FALSE);
	*/

	// 정병영 이미지 경로 수정  ///////////////////////////////////////////////////////
	//TCHAR path[_MAX_PATH];
	//GetModuleFileName(NULL, path, sizeof path);

	//CString strPath = path;
	//int i = strPath.ReverseFind('\\');//실행 파일 이름을 지우기 위해서 왼쪽에 있는 '/'를 찾는다.
	//strPath = strPath.Left(i);//뒤에 있는 현재 실행 파일 이름을 지운다.
	//
	//CString stemp_tofile = strPath + _T("\\CarImage\\") + pfilename;
	
	// c# 에서 경로를 통째로 넘겨주는 걸로 수정
	CString stemp_tofile = pfilename;


	 //AfxMessageBox(strPath);
	////////////////////////////////////////////////////////////////////////////	

	//CEncrypt enc;
	//enc.endecryption((char*) (LPCSTR) stemp_tofile, DECRYPTION_MODE, (char*) (LPCSTR) stemp_tofile);

	CImage  image4print;
	image4print.Destroy();

	if (image4print.Load((LPCSTR) stemp_tofile) == S_OK) {
		image4print.Draw(m_hPrtdc, rectDraw);
	}
	else {
		::Rectangle(m_hPrtdc, rectDraw.left, rectDraw.top, rectDraw.right, rectDraw.bottom);

		CString snoimage = _T("No Image : ");
		::TextOut(m_hPrtdc, 
			rectDraw.left + 300, (rectDraw.top+rectDraw.bottom) / 2, 
			snoimage, snoimage.GetLength());
	}

	DeleteFile(stemp_tofile);
}

void CPrint_TBos::init_resolution()
{
	if (m_nXmax == 0)
		m_nXmax = GetDeviceCaps(m_hPrtdc, HORZRES);
	if (m_nYmax == 0)
		m_nYmax = GetDeviceCaps(m_hPrtdc, VERTRES);
}

bool CPrint_TBos::Print_CaseInfo(ONE_CASE* pcase, LPCSTR ptitle)
{
	if (pcase == NULL) 
		return false;

	init_resolution();

	//////////////////////////////////////////////////////////////////////////////
	// text (the detail of contravention ...) 
	int nx = 100;
	int ny = (m_nYmax * 1) / 16;

	CString sline;

	nx = 2700;
	ny = (m_nYmax * 2) / 16;
	int nimageX = (m_nXmax-100) - nx;
	int nimageY = (nimageX * 3) / 4;
	print_image_there(pcase->p2_File1, CRect(nx, ny, (nx+nimageX), (ny+nimageY) ));

	///////////////////////////////////////////////////////////////////////////
	//nx = 1500;
	nx = 1700;
	ny = (m_nYmax * 1) / 16;
	sline = ptitle;
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());

	nx = 100;
	ny = (m_nYmax * 15) / 16;
	sline = _T("printed by iTOPS");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());

	nx = (m_nXmax * 3) / 4 - 200;
	ny = (m_nYmax * 15) / 16;
	SYSTEMTIME timeNow;
	GetLocalTime(&timeNow);
	sline.Format(_T("Print date : %04d-%02d-%02d"), timeNow.wYear, timeNow.wMonth,  timeNow.wDay);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());

	///////////////////////////////////////////////////////////////////////////
	nx = 100;
	ny = (m_nYmax * 2) / 16;
	sline = _T("Contravention Details");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET*2);

	// 
	sline.Format( _T("Offence Date     : %s"), pcase->p2_WhenDT);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Court            : %s"), pcase->p1_Court);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Location Code    : %s"), pcase->p1_Location);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Speed Limit      : %s Km/h"), pcase->p1_SpeedLaw);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Your Speed       : %s Km/h"), pcase->p2_SpeedIs);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Distance         : %s (m)"), pcase->p2_Distance);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Camer ID         : %s"), pcase->p1_Device);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Officer ID       : %s"), pcase->p1_Officer);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	///////////////////////////////////////////////////////////////////////////
	nx = 100;
	ny = (m_nYmax * 5) / 16;
	sline = _T("Captured Information");
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());
	nx = 500;  ny += (LINE_OFFSET*2);

	sline.Format( _T("Car Number       : %s"), pcase->p9_CarNum);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Notice Number    : %s"), pcase->k_NoticeNum);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	//sline.Format( _T("Ref. Number      : %I64d"), pcase->n6_Ref64);
	//TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Fine (initial)   : %s.00 (Rands)"), pcase->p3_Fine);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	if (pcase->k_Fine2 == 0) { 
		sline.Format( _T("Fine (reduced)   : Not reduced"));
	}
	else {
		sline.Format( _T("Fine (reduced)   : %d.00 (Rands)"), pcase->k_Fine2);
	}
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	sline.Format( _T("Offence Code     : %s"), pcase->p3_OffenceCode);
	TextOut(m_hPrtdc, nx, ny, (LPCSTR) sline, sline.GetLength());  ny += LINE_OFFSET;

	return true;
}

