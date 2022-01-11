// UxIni.h: interface for the CUxIni class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(__UTIL_INI_FILE_OPERATON__)
#define __UTIL_INI_FILE_OPERATON__

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CUxIni  
{
public:
	CUxIni(CString sapp = "");
	virtual ~CUxIni();

	void	SetAppName();

protected:
	CString			m_sApp;
	CString			m_sSection;

public:
	void	SetSection(CString s);
	CString	GetString(CString sKey, CString sdefault = "", CString ssection = "");
	int		GetInt(CString sKey, int ndefault = 0, CString ssection = "");
	void	SetString(CString sKey, CString svalue, CString ssection = "");

	void	SetIniName(LPCSTR pini)  { m_sApp = pini; };
};

#endif // !defined(__UTIL_INI_FILE_OPERATON__)
