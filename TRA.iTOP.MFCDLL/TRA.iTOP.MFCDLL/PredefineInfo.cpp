#include "StdAfx.h"
#include "PredefineInfo.h"
#include "UxIni.h"
//#include "IamTBos.h"

CPredefineInfo::CPredefineInfo(void)
{
}

CPredefineInfo::~CPredefineInfo(void)
{
}

void CPredefineInfo::load_ini(LPCSTR pinifile)
{
	CUxIni  ini;
	ini.SetIniName(pinifile);

	//ini.SetAppName();

	m_sEnq_1 = ini.GetString("ENQ_1", "", "ENQURY");
	m_sEnq_2 = ini.GetString("ENQ_2", "", "ENQURY");
	m_sEnq_3 = ini.GetString("ENQ_3", "", "ENQURY");
	m_sEnq_Addr_1 = ini.GetString("ENQ_ADDR_1", "", "ENQURY");
	m_sEnq_Addr_2 = ini.GetString("ENQ_ADDR_2", "", "ENQURY");
	m_sEnq_Addr_3 = ini.GetString("ENQ_ADDR_3", "", "ENQURY");
	m_sEnq_Addr_4 = ini.GetString("ENQ_ADDR_4", "", "ENQURY");
	m_sEnq_Addr_5 = ini.GetString("ENQ_ADDR_5", "", "ENQURY");
	m_sIssuer_1 = ini.GetString("Line_1", "", "ISSUER");
	m_sIssuer_2 = ini.GetString("Line_2", "", "ISSUER");
	m_sIssuer_3 = ini.GetString("Line_3", "", "ISSUER");
	m_sIssuer_4 = ini.GetString("Line_4", "", "ISSUER");
	m_sIssuer_5 = ini.GetString("Line_5", "", "ISSUER");

}

void CPredefineInfo::save_ini(LPCSTR pinifile)
{
	CUxIni  ini;

	//ini.SetAppName();
	ini.SetIniName(pinifile);

	ini.SetString("ENQ_1", m_sEnq_1, "ENQURY");
	ini.SetString("ENQ_2", m_sEnq_2, "ENQURY");
	ini.SetString("ENQ_3", m_sEnq_3, "ENQURY");
	ini.SetString("ENQ_ADDR_1", m_sEnq_Addr_1, "ENQURY");
	ini.SetString("ENQ_ADDR_2", m_sEnq_Addr_2, "ENQURY");
	ini.SetString("ENQ_ADDR_3", m_sEnq_Addr_3, "ENQURY");
	ini.SetString("ENQ_ADDR_4", m_sEnq_Addr_4, "ENQURY");
	ini.SetString("ENQ_ADDR_5", m_sEnq_Addr_5, "ENQURY");
	ini.SetString("Line_1", m_sIssuer_1, "ISSUER");
	ini.SetString("Line_2", m_sIssuer_2, "ISSUER");
	ini.SetString("Line_3", m_sIssuer_3, "ISSUER");
	ini.SetString("Line_4", m_sIssuer_4, "ISSUER");
	ini.SetString("Line_5", m_sIssuer_5, "ISSUER");

}
