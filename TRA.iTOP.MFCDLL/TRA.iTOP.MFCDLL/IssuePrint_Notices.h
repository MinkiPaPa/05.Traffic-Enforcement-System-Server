#pragma once

#include "IssuePrint_Form.h"
#include "PredefineInfo.h"

//#include "data_Case.h"
#include "TRA.iTOP.MFCDLL.h"

/**
//////////////////////////////////////////////////////////////////////////////////
typedef struct _case_record_x : public ONE_CASE
{
	int			x_nType;
	char		x_pIssueDate[SZ_DATETIME];
	char		x_pPrintDate[SZ_DATETIME];
	bool		x_bChecked;
} XONE_CASE;
**/
//typedef vector <XONE_CASE*>				ARR_XCASES;
//typedef ARR_XCASES::iterator			THE_XCASE;

//typedef ONE_CASE  XONE_CASE;
typedef printCase  XONE_CASE;

class CIssuePrint_Notices : public CIssuePrint_Form
{
public:
	CIssuePrint_Notices(void);
	virtual ~CIssuePrint_Notices(void);

protected:
	CString			m_sDirImageBase;
	bool			m_bDrawBorder;
	CPredefineInfo*	m_pInfo;

protected:
	void	print_case_content(XONE_CASE* pcase, bool bDueDate, LPCSTR pDueDate, LPCSTR pToday, bool bshowHistory);

	void	print_case_address(XONE_CASE* pcase);
	void	print_case_numbers(XONE_CASE* pcase);
	void	print_case_contravention(XONE_CASE* pcase);
	void	print_case_payduedate(XONE_CASE* pcase, bool bDueDate, LPCSTR pDueDate);
	void	print_case_court(XONE_CASE* pcase);
	void	print_case_fine(XONE_CASE* pcase);
	void	print_case_enquries(XONE_CASE* pcase);
	void	print_case_issueinfo(XONE_CASE* pcase);
	void	print_case_printtoday(XONE_CASE* pcase, LPCSTR pdate);
	void	print_case_issuehistory(XONE_CASE* pcase);


public:
	void	SetInfo(CPredefineInfo* p)  { m_pInfo = p; };
	void	SetDocName(LPCSTR pfilename);
	void	SetDrawBorder(bool b)    { m_bDrawBorder = b;  };

	void	IssueOne(int nth, XONE_CASE* pcase, 
		LPCSTR ptitle, 
		bool bPhoto, 
		bool bPgNum, 
		bool bPrePaper, 
		bool bDueDate, 
		LPCSTR pDueDate, 
		LPCSTR pToday,
		bool bShowPrintHistory);

};
