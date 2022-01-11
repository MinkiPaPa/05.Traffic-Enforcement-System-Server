// UxIni.cpp: implementation of the CUxIni class.
//
//////////////////////////////////////////////////////////////////////

#include "StdAfx.h"
#include "UxIni.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CUxIni::CUxIni(CString sapp) : m_sApp(sapp)
{
	//SetAppName();

	m_sSection = "DEFAULT";
}

CUxIni::~CUxIni()
{
}

void CUxIni::SetAppName()
{
	char drive[_MAX_DRIVE];
	char dir[_MAX_DIR];
	char fname[_MAX_FNAME];
	char ext[_MAX_EXT];
	char path[MAX_PATH]; 

	if (m_sApp.IsEmpty()) {
		GetModuleFileName(NULL, path, MAX_PATH);
	}
	else {
         GetCurrentDirectory(MAX_PATH, path);
	
		 _splitpath((LPCSTR) m_sApp, drive, dir, fname, ext );
		 strcat(path, "\\");
		 strcat(path, fname);
		 strcat(path, ext);
	}

	_splitpath(path, drive, dir, fname, ext );

	m_sApp = drive;
	m_sApp += dir;
	m_sApp += fname;

#ifdef _DEBUG_
	const char* p = (LPCSTR) m_sApp;
#endif

	if ((stricmp(ext, ".exe")) == 0)
		m_sApp += ".ini";
	else
		m_sApp += ext;
}

void CUxIni::SetSection(CString s)
{
	m_sSection = s;
}

CString CUxIni::GetString(CString sKey, CString sdefault, CString ssection)
{
	if (ssection != "") 
		m_sSection = ssection;

	char pAnswer[512];
	GetPrivateProfileString((LPCSTR) m_sSection, (LPCSTR) sKey, (LPCSTR) sdefault, 
		pAnswer, 512, (LPCSTR) m_sApp);

	return CString(pAnswer);
}

int	CUxIni::GetInt(CString sKey, int ndefault, CString ssection)
{
	if (ssection != "") 
		m_sSection = ssection;

	UINT nval = GetPrivateProfileInt((LPCSTR) m_sSection, (LPCSTR) sKey, ndefault, 
		(LPCSTR) m_sApp);

	return (int) nval;
}

void CUxIni::SetString(CString sKey, CString svalue, CString ssection)
{
	if (ssection != "") 
		m_sSection = ssection;

	WritePrivateProfileString((LPCSTR) m_sSection, (LPCSTR) sKey, (LPCSTR) svalue, (LPCSTR) m_sApp);
}

/**
DWORD GetPrivateProfileString(
  LPCTSTR lpAppName,        // section name
  LPCTSTR lpKeyName,        // key name
  LPCTSTR lpDefault,        // default CString
  LPTSTR lpReturnedString,  // destination buffer
  DWORD nSize,              // size of destination buffer
  LPCTSTR lpFileName        // initialization file name
);
BOOL WritePrivateProfileString(
  LPCTSTR lpAppName,  // section name
  LPCTSTR lpKeyName,  // key name
  LPCTSTR lpString,   // CString to add
  LPCTSTR lpFileName  // initialization file
);
***/
