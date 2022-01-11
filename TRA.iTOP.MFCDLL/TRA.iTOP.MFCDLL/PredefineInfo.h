#pragma once

class CPredefineInfo
{
public:
	CPredefineInfo(void);
	virtual ~CPredefineInfo(void);

public:
	CString				m_sEnq_1;
	CString				m_sEnq_2;
	CString				m_sEnq_3;
	CString				m_sEnq_Addr_1;
	CString				m_sEnq_Addr_2;
	CString				m_sEnq_Addr_3;
	CString				m_sEnq_Addr_4;
	CString				m_sEnq_Addr_5;
	CString				m_sIssuer_1;
	CString				m_sIssuer_2;
	CString				m_sIssuer_3;
	CString				m_sIssuer_4;
	CString				m_sIssuer_5;

public:
	void	load_ini(LPCSTR pinifile);
	void	save_ini(LPCSTR pinifile);

};
