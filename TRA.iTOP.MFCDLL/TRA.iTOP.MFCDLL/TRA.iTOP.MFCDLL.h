// TRA.iTOP.MFCDLL.h : TRA.iTOP.MFCDLL DLL의 주 헤더 파일
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH에 대해 이 파일을 포함하기 전에 'stdafx.h'를 포함합니다."
#endif

#include "resource.h"		// 주 기호입니다.
#include <windows.h>
#include "PredefineInfo.h"

#define		SZ_PERSON_IDTYPE			4
#define		SZ_PERSON_SID				20
#define		SZ_PERSON_NAME				40
#define		SZ_PERSON_NAME_INI			4
#define		SZ_PERSON_NOSO				4
#define		SZ_PERSON_ADDR				40
#define		SZ_PERSON_CODE				8
#define		SZ_PERSON_PHONE				20
#define		SZ_PERSON_NATURE			4
#define		SZ_PERSON_CDATE				10
#define		SZ_INTERIM_CARNUM			16
#define		SZ_CASE_NOTICENUM			20
#define		SZ_INTERIM_WHENDT			20
#define		SZ_INTERIM_STREET			40
#define		SZ_INTERIM_COURT			20
#define		SZ_INTERIM_LOCATION			10
#define		SZ_INTERIM_DIRECTION		8
#define		SZ_INTERIM_SPEEDLAW			8
#define		SZ_INTERIM_SPEEDIS			8
#define		SZ_INTERIM_OFFENCECODE		16
#define		SZ_INTERIM_OFFICER			16
#define		SZ_CASE_PAY_DATE			20
#define		SZ_INTERIM_FINE				16
#define		SZ_CASE_PRINT_DATE			20
#define		SZ_CASE_PAY_NUM				40
#define		SZ_CASE_PAY_POINT			40
#define		SZ_CASE_PAYER_PHONE			20
#define		SZ_CASE_PAYER_NAME			40
#define		SZ_CASE_PAY_CAHSER			40
#define		SZ_INTERIM_FILE1			200


#define		SZ_DATETIME					20
#define		SZ_REPRESENT_DOCNUM		20
#define		SZ_REPRESENT_CLAIM		256

#define		SZ_INTERIM_DEVICE			10
#define		SZ_INTERIM_DISTANCE			8

typedef struct tTest
{
	char  strTest[128]; //문자열 128
	int   intTest;    //숫자형
	BYTE byteTest[64]; //바이트형 배열
	UINT  uintTest[4];  //유니트형 배열
} typeTest;

typedef struct tPrintCase
{
	char	NaP_NAME[SZ_PERSON_NAME];

	char	NaP_INITIAL[SZ_PERSON_NAME_INI];
	char	NaX_NAME[SZ_PERSON_NAME];

	char	NaP_ADDR1[SZ_PERSON_ADDR];
	char	NaP_ADDR2[SZ_PERSON_ADDR];
	char	NaP_ADDR3[SZ_PERSON_ADDR];
	char	NaP_ADDR4[SZ_PERSON_ADDR];
	char	NaP_ADDR5[SZ_PERSON_ADDR];
	char	NaP_ACODE[SZ_PERSON_CODE];

	char	k_NoticeNum[SZ_CASE_NOTICENUM];
	char	p9_CarNum[SZ_INTERIM_CARNUM];
	char	p2_WhenDT[SZ_INTERIM_WHENDT];
	char	p1_Street[SZ_INTERIM_STREET];
	char	p1_Court[SZ_INTERIM_COURT];
	char	p1_Location[SZ_INTERIM_LOCATION];
	char	p1_Direction[SZ_INTERIM_DIRECTION];
	char	p1_SpeedLaw[SZ_INTERIM_SPEEDLAW];
	char	p2_SpeedIs[SZ_INTERIM_SPEEDIS];
	char	p3_OffenceCode[SZ_INTERIM_OFFENCECODE];
	char	p1_Officer[SZ_INTERIM_OFFICER];
	char	p2_File1[SZ_INTERIM_FILE1];

	char	k_PayDueDate[SZ_CASE_PAY_DATE];
	char	p3_Fine[SZ_INTERIM_FINE];
	
	char	k_Last341[SZ_CASE_PRINT_DATE];
	char	k_LastNBS[SZ_CASE_PRINT_DATE];
	char	k_LastSummon[SZ_CASE_PRINT_DATE];
	char	k_LastWOA[SZ_CASE_PRINT_DATE];

	char	k_PayBillNum[SZ_CASE_PAY_NUM];
	char	k_PayPoint[SZ_CASE_PAY_POINT];
	char	k_PayTime[SZ_CASE_PAY_DATE];
	char	k_PayerPhone[SZ_CASE_PAYER_PHONE];
	char	k_PayerName[SZ_CASE_PAYER_NAME];
	char	k_PayCasher[SZ_CASE_PAY_CAHSER];

	// Print Case 에서 추가
	char	NaP_IDNUM[SZ_PERSON_SID];
	char	p1_Device[SZ_INTERIM_DEVICE];
	char	p2_Distance[SZ_INTERIM_DISTANCE];

	int		k_PayType;
	int		k_Payed;
	int		k_Fine2;

} printCase;


typedef struct tPrintReceipt
{

	char	k_NoticeNum[SZ_CASE_NOTICENUM];
	char	k_PayBillNum[SZ_CASE_PAY_NUM];
	char	k_PayPoint[SZ_CASE_PAY_POINT];
	char	k_PayTime[SZ_CASE_PAY_DATE];
	char	k_PayerPhone[SZ_CASE_PAYER_PHONE];
	char	k_PayerName[SZ_CASE_PAYER_NAME];
	char	k_PayCasher[SZ_CASE_PAY_CAHSER];

	int		k_PayType;
	int		k_Payed;

} printReceipt;


typedef struct _representation_record
{
	UINT64		n64CUID;
	char		p9_CarNum[SZ_INTERIM_CARNUM];
	char		k_NoticeNum[SZ_CASE_NOTICENUM];
	UINT64		n6_Ref64;

	char		r_pDecisionWho[SZ_CASE_PAY_CAHSER];
	char		r_pDecisionDT[SZ_DATETIME];
	char		r_pDecisionWhere[SZ_CASE_PAY_POINT];
	int			r_nDecisionIs;
	char		r_pDecisionDocNum[SZ_REPRESENT_DOCNUM];
	int			r_nFineReduced;

	char		r_pIssuerWho[SZ_CASE_PAY_CAHSER];
	char		r_pIssuerPhone[SZ_CASE_PAYER_PHONE];
	int			r_nIssuerMethod;
	char		r_pIssuerClaim[SZ_REPRESENT_CLAIM];

	char		r_pCapturer[SZ_CASE_PAY_CAHSER];
	char		r_pDateTime[SZ_DATETIME];

} ONE_REPRESENT;


typedef struct {
	int len;
	printCase **data_list;
}Data_list;



// CTRAiTOPMFCDLLApp
// 이 클래스 구현에 대해서는 TRA.iTOP.MFCDLL.cpp를 참조하세요.
//
extern "C" __declspec(dllexport) int	GetEasyPayNumberCheckDigit(int m_EasyPayID, int nNA, int nNB, int nNC);
extern "C" __declspec(dllexport) int	IssuePrint_Notices(printCase* pcase, char* _iniFile, char* m_sTitle, bool m_bOptPicture, bool m_bOptPrePaper, bool m_bOptDBUpdate, char* m_sToday, char* m_sDueDate);
extern "C" __declspec(dllexport) int	IssuePrint_RePresentation(printCase* pcase, ONE_REPRESENT* prepresent);
extern "C" __declspec(dllexport) int	IssuePrint_Pay(printCase* pcase);
extern "C" __declspec(dllexport) int	IssuePrint_Pay_text(printCase* pcase);
extern "C" __declspec(dllexport) int	PrintCaseDetail(printCase* pcase);
extern "C" __declspec(dllexport) void	IssuePrint_Data_list(Data_list *list, int npage, bool m_bOptPageNum, char* _iniFile, char* m_sTitle, bool m_bOptPicture, bool m_bOptPrePaper, bool m_bOptDBUpdate, char* m_sToday, char* m_sDueDate);
/*
class CTRAiTOPMFCDLLApp : public CWinApp
{
public:
	CTRAiTOPMFCDLLApp();


// 재정의입니다.
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
*/
