#include "StdAfx.h"
#include "IamTPrint.h"

/**
CIamTPrint::CIamTPrint(void)
{
}

CIamTPrint::~CIamTPrint(void)
{
}

void CIamTPrint::InitTPrint()
{
	m_preinfo.load_ini();
}

void CIamTPrint::ClearAll()
{
	clear_memory();
}

void CIamTPrint::clear_memory()
{
	for (THE_XCASE it = m_arrXCases.begin(); it != m_arrXCases.end(); it++) 
	{
		delete *it;
	}
	m_arrXCases.clear();
}

int	CIamTPrint::GetPrintData(LPCSTR pfilename)
{
	clear_memory();

	m_sPrintFile = pfilename;

	// 
	CImportMDB		mdber;
	mdber.Open(pfilename);
	int ncount = mdber.GetData(m_arrXCases);

	return ncount;
}
***/

