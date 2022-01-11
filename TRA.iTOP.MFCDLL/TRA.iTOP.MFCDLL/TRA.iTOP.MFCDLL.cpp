// TRA.iTOP.MFCDLL.cpp : DLL의 초기화 루틴을 정의합니다.
//

#include "stdafx.h"

#include "TRA.iTOP.MFCDLL.h"
#include "IssuePrint_Notices.h"
#include "Print_Represent.h"
#include "Issue_Receipt.h"
#include "Print_CaseDetail.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif
/*
//
//TODO: 이 DLL이 MFC DLL에 대해 동적으로 링크되어 있는 경우
//		MFC로 호출되는 이 DLL에서 내보내지는 모든 함수의
//		시작 부분에 AFX_MANAGE_STATE 매크로가
//		들어 있어야 합니다.
//
//		예:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// 일반적인 함수 본문은 여기에 옵니다.
//		}
//
//		이 매크로는 MFC로 호출하기 전에
//		각 함수에 반드시 들어 있어야 합니다.
//		즉, 매크로는 함수의 첫 번째 문이어야 하며
//		개체 변수의 생성자가 MFC DLL로
//		호출할 수 있으므로 개체 변수가 선언되기 전에
//		나와야 합니다.
//
//		자세한 내용은
//		MFC Technical Note 33 및 58을 참조하십시오.
//

// CTRAiTOPMFCDLLApp

BEGIN_MESSAGE_MAP(CTRAiTOPMFCDLLApp, CWinApp)
END_MESSAGE_MAP()


// CTRAiTOPMFCDLLApp 생성

CTRAiTOPMFCDLLApp::CTRAiTOPMFCDLLApp()
{
	// TODO: 여기에 생성 코드를 추가합니다.
	// InitInstance에 모든 중요한 초기화 작업을 배치합니다.
}


// 유일한 CTRAiTOPMFCDLLApp 개체입니다.

CTRAiTOPMFCDLLApp theApp;


// CTRAiTOPMFCDLLApp 초기화

BOOL CTRAiTOPMFCDLLApp::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}
*/

InfoList_Row	TheRow_PayDetailInfo[] = {
	{ "Pay",            NULL },
	{ "Due Date",       NULL },
	{ "Receipt#",       NULL },
	{ "Pay Point",      NULL },
	{ "Comment",        NULL },
	{ "Date",           NULL }
};

InfoList_Row	TheRow_LastPrintInfo[] = {
	{ "1st Notice",            NULL },
	{ "NBS",                   NULL },
	{ "Summon",                NULL },
	{ "Court",                 NULL },
	{ "Represent",             NULL }
};

InfoList_Row	TheRow_OwnerInfo[] = {
	{ "ID",                NULL },
	{ "Name",              NULL },
	{ "Addr 1",            NULL },
	{ "Addr 2",            NULL },
	{ "Addr 3",            NULL },
	{ "Addr Code",         NULL },
	{ "Postal 1",          NULL },
	{ "Postal 2",          NULL },
	{ "Postal 3",          NULL },
	{ "Postal Code",       NULL },
	{ "Addr Change Date",  NULL }
};

InfoList_Row	TheRow_Changes[] = {
	{ "Object",         NULL },
	{ "Before",			NULL },
	{ "After",			NULL },
	{ "who",			NULL },
	{ "when",           NULL }
};

void print_my_data(int nPage, printCase *pcase, CIssuePrint_Notices* pdev, bool m_bOptPageNum, char* _iniFile, char* m_sTitle, bool m_bOptPicture, bool m_bOptPrePaper, bool m_bOptDBUpdate, char* m_sToday, char* m_sDueDate);


int GetEasyPayNumberCheckDigit(int m_EasyPayID, int nNA, int nNB, int nNC)
{
	int nRRRR = m_EasyPayID;

	char pNUMBER[20];
	memset(pNUMBER, 0, 20);

	sprintf_s(pNUMBER, 20, "%04d%02d%05d%03d", nRRRR, nNA, nNB, nNC);

	char pLuhn[20];
	memset(pLuhn, 0, 20);

	int nLimit = strlen(pNUMBER);

	bool bOdd = true;
	for (int nX = nLimit - 1; nX >= 0; nX--, bOdd = !bOdd) {
		pLuhn[nX] = (pNUMBER[nX] - '0');

		if (bOdd) {
			pLuhn[nX] *= 2;
			if (pLuhn[nX] > 9)
				pLuhn[nX] = pLuhn[nX] - 9;
		}
	}

	int nsum = 0;
	for (int nX = 0; nX < nLimit; nX++) {
		nsum += pLuhn[nX];
	}


	int nanswer = (nsum + 9) / 10 * 10;
	nanswer = nanswer - nsum;

	return nanswer;

}


int IssuePrint_Notices(printCase* pcase, char* _iniFile, char* m_sTitle, bool m_bOptPicture, bool m_bOptPrePaper, bool m_bOptDBUpdate, char* m_sToday, char* m_sDueDate)
{
	//AfxMessageBox(CString(pNoti->k_NoticeNum));
	
	//////////////////////////////////////////////////
	// print !
	CIssuePrint_Notices		issue_print;

	if (issue_print.SafePrinter() == false) {
		MessageBox(NULL, _T("No Printer installed"), _T("ERROR"), 0);
		return 0;
	}

	CPredefineInfo	m_preinfo;
	m_preinfo.load_ini(CString(_iniFile));

	issue_print.SetDocName("Reprint Notice");
	issue_print.SetInfo(&m_preinfo);
	issue_print.SetDrawBorder(false);

	issue_print.GetPage()->ClearAllRegion();

	issue_print.IssueOne(0, pcase,
		m_sTitle,
		m_bOptPicture,
		false, // m_bOptPageNum, 
		m_bOptPrePaper,
		true, // m_bOptDueDate, 
		(LPCSTR)m_sDueDate,
		(LPCSTR)m_sToday,
		m_bOptDBUpdate
	);

	//
	return 0;
}

void IssuePrint_Data_list(Data_list *list, int npage, bool m_bOptPageNum, char* _iniFile, char* m_sTitle, bool m_bOptPicture, bool m_bOptPrePaper, bool m_bOptDBUpdate, char* m_sToday, char* m_sDueDate)
{
	if (!list) {
		printf("[ERROR] no list\n");
		return;
	}

	CIssuePrint_Notices		issue_print;

	if (issue_print.SafePrinter() == false) {
		MessageBox(NULL, "No Printer installed", "ERROR", 0);
		return;
	}

	issue_print.SetDrawBorder(FALSE);

	// ini 오류 수정 ////////////
	CPredefineInfo	m_preinfo;
	m_preinfo.load_ini(CString(_iniFile));
	issue_print.SetInfo(&m_preinfo);
	//////////////////////////////////

	for (int n = 0; n < list->len; n++) {
		print_my_data(n, list->data_list[n], &issue_print, m_bOptPageNum, _iniFile, m_sTitle, m_bOptPicture, m_bOptPrePaper, m_bOptDBUpdate, m_sToday, m_sDueDate);
	}

}

void print_my_data(int nPage, printCase *pcase, CIssuePrint_Notices* pdev, bool m_bOptPageNum, char* _iniFile, char* m_sTitle, bool m_bOptPicture, bool m_bOptPrePaper, bool m_bOptDBUpdate, char* m_sToday, char* m_sDueDate)
{

	pdev->GetPage()->ClearAllRegion();


	pdev->IssueOne(nPage, pcase, (LPCSTR)m_sTitle,
		m_bOptPicture, m_bOptPageNum, m_bOptPrePaper, m_bOptDBUpdate,
		(LPCSTR)m_sDueDate, (LPCSTR)m_sToday, false);

}


int IssuePrint_RePresentation(printCase* pcase, ONE_REPRESENT* prepresent)
{
	//AfxMessageBox(CString(pNoti->k_NoticeNum));

	//////////////////////////////////////////////////
	// print !
	
	//CPrintRepresent	prt_it(m_hWnd, PRINT_INDIRECT);
	CPrintRepresent	prt_it(NULL, PRINT_INDIRECT);

	if (prt_it.Print_init()) {
		//ONE_CASE* pcase = TBosIs.GetCurrCase();
		//ARR_REPRESENT &	arrHistory = TBosIs.GetRepresentArr();
		//ONE_REPRESENT* prepresent = arrHistory.at(m_nWhichRepresent);

		if (pcase != NULL && prepresent != NULL)
			prt_it.Print_content(pcase, prepresent);
	}
	prt_it.Print_fini();
	//
	return 0;
}

int IssuePrint_Pay(printCase* pcase)
{
	if (pcase) {
		CIssue_Receipt  prt_it(pcase);
		prt_it.PrintReceipt();
	}

	return 0;
}

int IssuePrint_Pay_text(printCase* pcase)
{
	if (pcase) {
		CIssue_Receipt  prt_it(pcase);
		prt_it.PrintReceipt_text();  // 열전사 프린트용
	}

	return 0;
}


int	PrintCaseDetail(printCase* pcase)
{
	CPrintCaseDetail prt_it(NULL, PRINT_INDIRECT);
	if (prt_it.Print_init()) {
		prt_it.Print_content(pcase,
			TheRow_PayDetailInfo, 6,
			TheRow_LastPrintInfo, 5,
			TheRow_OwnerInfo, 11,
			TheRow_Changes, 5);
	}
	prt_it.Print_fini();

	return 0;
}
/*
/////////////////// 테스트 용  ///////////////////////////////////////////
void OnTest1(void)
{
	//기본형
}

int intOnTest2(int intA)
{
	//입출력 숫자형

	++intA; //입력받은 숫자에 +1

	return intA;
}

char* strOnTest3(char *strTemp)
{
	//입출력 문자열형

	static char strTemp2[128] = { 0, };	//임시저장용 문자열
	sprintf_s(strTemp2, "%s strOnTest3 에서 리턴", strTemp);	//문자열 합치기

	return strTemp2;
}

void OnTest4(typeTest *testTemp)
{
	//입력 구조체형(포인터 출력가능)

	testTemp->byteTest[0] = 1;
	testTemp->intTest = testTemp->intTest + 2;
	sprintf_s(testTemp->strTest, "%s OnTest4에서 포인터", testTemp->strTest);
	testTemp->uintTest[0] = 1;
}

void OnTest5(int *intTemp)
{
	//입출력 배열형(포인터 출력 가능)
	for (int i = 0; i < 10; ++i)
	{
		intTemp[i] = intTemp[i] + i;
	}
}
//////////////////////////////////////////////////////////////
*/