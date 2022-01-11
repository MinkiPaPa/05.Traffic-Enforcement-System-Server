#pragma once

//#include "Import_MDB.h"
#include "PredefineInfo.h"

/**
class CIamTPrint
{
public:
	CIamTPrint(void);
	virtual ~CIamTPrint(void);

protected:
	ARR_XCASES		m_arrXCases;
	CString			m_sPrintFile;
	CPredefineInfo	m_preinfo;

public:

protected:
	void			clear_memory();

public:
	void			InitTPrint();
	CPredefineInfo& GetPreinfo()    { return m_preinfo;  };

	void			ClearAll();
	ARR_XCASES &	GetArrXCase()	{ return m_arrXCases; };
	LPCSTR			GetPrintFile()  { return (LPCSTR) m_sPrintFile; };

	int				GetPrintData(LPCSTR pfilename);

};

extern CIamTPrint	TheTPrinter;
**/

