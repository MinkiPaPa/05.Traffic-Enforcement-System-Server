#include "StdAfx.h"
#include "IssuePrint_Notices.h"
//#include "IamTBos.h"

char NameMonths[][20] = 
{
	"January", 
	"February", 
	"March", 
	"April", 
	"May", 
	"June", 
	"July", 
	"August", 
	"September", 
	"October", 
	"November", 
	"December"
};

CIssuePrint_Notices::CIssuePrint_Notices(void)
{
	m_sDirImageBase = _T("");
}

CIssuePrint_Notices::~CIssuePrint_Notices(void)
{
}

void CIssuePrint_Notices::SetDocName(LPCSTR pfilename)
{
#ifdef _TPRINT_ONLY
	m_sDirImageBase = pfilename;

	CString sdocname = m_sDocName;
	int npos = m_sDirImageBase.ReverseFind('\\');
	if (npos >= 0) {
		sdocname += m_sDirImageBase.Mid(npos, m_sDirImageBase.GetLength()-npos);
	}
	m_sDocName = sdocname;

	npos = m_sDirImageBase.ReverseFind('.');
	sdocname = m_sDirImageBase.Left(npos);
	m_sDirImageBase = sdocname + _T(".image");
#else
	//m_sDirImageBase = CTBosEnv::m_sZDrive;
	m_sDirImageBase = _T("Z");
	m_sDocName = m_sDocName + CString(pfilename);

#endif

}

void CIssuePrint_Notices::IssueOne(int nth, XONE_CASE* pcase, LPCSTR ptitle, 
			bool bPhoto, bool bPgNum, bool bPrePaper, bool bDueDate, 
			LPCSTR pDueDate, LPCSTR pToday, bool bShowPrintHistory)
{
	CDC* pDC = GetDC();
	pDC->StartPage();

	// use the preprinted paper 
	if (bPrePaper == false) {
		print_form();
	}

	// title 
	CPage *	page = GetPage();
	m_baseX = C2I(0.25 * 3.8);
	m_baseY = C2I(0.0);
	if (ptitle) {
		CPrintRegion * pregion = page->CreateRegion(C2I_Y(0.0), C2I_X(0.0), C2I_Y(1.0), C2I_X(17.5));
		if (pregion)
			page->Print(pregion, C2I(0.0),  C2I(0.0), TEXT_CENTER, 15,  ptitle);
	}
	m_baseY = C2I(1.5);

	// photo 
	if (bPhoto) {
/* 원본 
#ifdef _TPRINT_ONLY
		CString sfilename = m_sDirImageBase + pcase->p2_File1;
#else
		//CString sfilename = _T("C:\\TEMP\\car_tbos.jpg");
#endif
*/
		CString sfilename = pcase->p2_File1;  // 수정

		if (sfilename.GetLength() > 1)
		{
			CImage  image4print;
			//CRect   rectDraw(440, 60, (440 + 220), (60 + 120)); // 원본
			CRect   rectDraw(420, 60, (420 + 220), (60 + 120)); // 수정

			image4print.Destroy();
			if (image4print.Load((LPCSTR)sfilename) == S_OK) {
				image4print.Draw(pDC->m_hDC, rectDraw);
			}

		}

	}

	// case content ... 
	print_case_content(pcase, bDueDate, pDueDate, pToday, bShowPrintHistory);


	// page number (for verification)
	if (bPgNum) {
		CPrintRegion * pregion = page->CreateRegion(C2I_Y(26.0), C2I_X(17.0), C2I_Y(26.5), C2I_X(18.5));
		CString spagenum;
		spagenum.Format("%3d", nth+1);
		if (pregion) {
			page->Print(pregion, C2I(0.1),  C2I(0.1), TEXT_RIGHT, 10, (LPCSTR) spagenum);
		}
	}

	pDC->EndPage();
}

void CIssuePrint_Notices::print_case_content(XONE_CASE* pcase, bool bDueDate, 
								LPCSTR pDueDate, LPCSTR pToday, bool bshowHistory)
{
	print_case_address(pcase);
	print_case_numbers(pcase);
	print_case_contravention(pcase);
	print_case_payduedate(pcase, bDueDate, pDueDate);
	print_case_court(pcase);
	print_case_fine(pcase);
	print_case_enquries(pcase);
	print_case_issueinfo(pcase);

	print_case_printtoday(pcase, pToday);

	if (bshowHistory) 
		print_case_issuehistory(pcase);
}

void CIssuePrint_Notices::print_case_address(XONE_CASE* pcase)
{
	CPage *	page = GetPage();

	// Get Name 
	CString sname = _T("");
	{
		CString sname_full = pcase->NaP_NAME;
		CString sname_ini  = pcase->NaP_INITIAL;
		CString sproxy_name = pcase->NaX_NAME;

		if (sproxy_name.IsEmpty()) {
			sname.Format(_T("%s %s"), sname_full, sname_ini);
		}
		else {
			sname.Format(_T("%s %s (%s)"), sname_full, sname_ini, sproxy_name);
		}
	}

	// Get Address  
	CString saddress = _T("");
	{
		CString saddr4 = pcase->NaP_ADDR4;
		CString saddr5 = pcase->NaP_ADDR5;
	
		saddress.Format("%s\n%s\n%s\n%s%s%s", 
			pcase->NaP_ADDR1, pcase->NaP_ADDR2, pcase->NaP_ADDR3, 
			(saddr4.IsEmpty() ? "" : (saddr4+"\n")), 
			(saddr5.IsEmpty() ? "" : (saddr5+"\n")), 
			pcase->NaP_ACODE);
	}

	//CPrintRegion * pregionName = page->CreateRegion(C2I_Y(1.0), C2I_X(1.0), C2I_Y(2.0), C2I_X(7.5));  // 원본
	CPrintRegion * pregionName = page->CreateRegion(C2I_Y(0.7), C2I_X(1.0), C2I_Y(2.0), C2I_X(7.5));  // 수정
	if (pregionName == NULL) 
		return;
	if (m_bDrawBorder)
		pregionName->DrawBorder();

	//CPrintRegion * pregionAddr = page->CreateRegion(C2I_Y(2.0), C2I_X(1.0), C2I_Y(4.3), C2I_X(7.5)); // 원본
	CPrintRegion * pregionAddr = page->CreateRegion(C2I_Y(1.7), C2I_X(1.0), C2I_Y(4.3), C2I_X(7.5));  // 수정
	if (pregionAddr == NULL) 
		return;
	if (m_bDrawBorder)
		pregionAddr->DrawBorder();

	page->Print(pregionName, C2I(0.1),  C2I(0.1), TEXT_LEFT, 10,  sname);
	page->Print(pregionAddr, C2I(0.1),  C2I(0.1), TEXT_LEFT, 10,  saddress);
}

void CIssuePrint_Notices::print_case_numbers(XONE_CASE* pcase)
{
	CPage *	page = GetPage();
	CPrintRegion * pregionNum = page->CreateRegion(C2I_Y(2.0), C2I_X(13.0), C2I_Y(4.4), C2I_X(17.5));
	if (pregionNum == NULL) 
		return;
	if (m_bDrawBorder)
		pregionNum->DrawBorder();

	//CPrintRegion * pregionCar = page->CreateRegion(C2I_Y(3.8), C2I_X(8.5), C2I_Y(4.4), C2I_X(12.0));
	CPrintRegion * pregionCar = page->CreateRegion(C2I_Y(3.8), C2I_X(8.0), C2I_Y(4.4), C2I_X(12.0));
	if (pregionCar == NULL) 
		return;
	if (m_bDrawBorder)
		pregionCar->DrawBorder();


	CString snumber = _T("");
	//snumber.Format(_T("%s\n(%I64d)\n\n%s"), 
	//	pcase->k_NoticeNum, pcase->n6_Ref64, pcase->p9_CarNum);
	//snumber.Format(_T("%s\n \n\n%s"), 
	//	pcase->k_NoticeNum, pcase->p9_CarNum);
	snumber.Format(_T("%s"), pcase->k_NoticeNum);
	page->Print(pregionNum, C2I(0.1),  C2I(0.1), TEXT_CENTER|TEXT_BOLD, 12,  snumber);

	snumber.Format(_T("%s"), pcase->p9_CarNum);
	page->Print(pregionCar, C2I(0.1),  C2I(0.1), TEXT_CENTER|TEXT_BOLD, 12,  snumber);

}

void CIssuePrint_Notices::print_case_contravention(XONE_CASE* pcase)
{
	CString sline;
	CPage *	page = GetPage();

	// offence description 
	CPrintRegion * pregionOffenceDesc = page->CreateRegion(C2I_Y(6.0), C2I_X(0.7), C2I_Y(6.9), C2I_X(17.5));
	if (pregionOffenceDesc == NULL) 
		return;
	if (m_bDrawBorder)
		pregionOffenceDesc->DrawBorder();

	// offence description 
	//CString scomment1 = "          " 
	//	"Art./Sect. 58(1) gelees met/read with Art./Sect.  "
	//	"69(1), 73, 89 (1)  & 89(6) van Wet/of Act 93 van 1996 & Reg. "
	//	"284-291 & Bylae/Schedule 1 van die Nas.  Padverkeersregulasies/of "
	//	"the Nat.  Road Traffic Regulations 2000";
	//CString scomment1 = "          " 
	//	"Art./Sect. 59(4)(a) gelees met/read with Art./"
	//	"Sect. 59(1)(c)  69(2)  73  89(1) & 89(3) van Wet/of Act 93 van/of 1996 "
	//	"en/and Reg. 292(c) van die Nas. Padverkeersregulasies/of the Nat. Road"
	//	"Traffic Regulations 2000";
	CString scomment1 = "          " 
		"The accussed is guilty of contravcning Art/Sect 59(4)(b) "
		"gelees met/read with Art/Sect 69(1), 73, 89(1) and Reg 292(a) of "
		"the National Road Traffic Act 93 of 1996.";

	page->Print(pregionOffenceDesc, C2I(0.1),  C2I(0.1), TEXT_LEFT, 8,  scomment1);

	// offence date 
	CPrintRegion * pregionOffenceDate = page->CreateRegion(C2I_Y(7.5), C2I_X(4.0), C2I_Y(8.0), C2I_X(9.0));
	if (pregionOffenceDate == NULL) 
		return;
	if (m_bDrawBorder)
		pregionOffenceDate->DrawBorder();
	page->Print(pregionOffenceDate, C2I(0.1),  C2I(0.1), TEXT_CENTER | TEXT_BOLD, 8,  pcase->p2_WhenDT);


	// offence location  
	CPrintRegion * pregionOffencePlace = page->CreateRegion(C2I_Y(7.2), C2I_X(11.0), C2I_Y(8.0), C2I_X(17.8));
	if (pregionOffencePlace == NULL) 
		return;
	if (m_bDrawBorder)
		pregionOffencePlace->DrawBorder();

	sline = pcase->p1_Street;
	if (sline.IsEmpty()) {
		sline = pcase->p1_Court;
		sline += _T(" - ");
		sline += pcase->p1_Location;
	}
	if (strcmp(pcase->p1_Direction, "") != 0) {
		sline += _T(" (");
		sline += pcase->p1_Direction;
		sline += _T(")");
	}
	page->Print(pregionOffencePlace, C2I(0.1),  C2I(0.1), TEXT_LEFT|TEXT_VCENTER, 8,  sline);

	// offence  desc
	CPrintRegion * pregionOffenceDetail = page->CreateRegion(C2I_Y(8.5), C2I_X(0.7), C2I_Y(10.5), C2I_X(17.5));
	if (pregionOffenceDetail == NULL) 
		return;
	if (m_bDrawBorder)
		pregionOffenceDetail->DrawBorder();

	CString scomment2, scomment3;
	scomment2.Format(" As bestuurder van motorvoertuig %s die snelheidsgrens "
		"van %s kilometer per uur, soos op die voorgeskrewe wyse op 'n "
		"padverkeersteken aangetoon was, oorskry het deurdat hy of sy teen 'n"
		"snelheid van ten minste %s,0 kilometer per uur gery het", 
		pcase->p9_CarNum, pcase->p1_SpeedLaw, pcase->p2_SpeedIs);

	scomment3.Format(" As the driver of motor vehicle %s exceeded the "
		"speed limit of %s kilometers per hour, as was indicated in "
		"the prescribed manner on a road traffic sign, in that he or "
		"she drove at a speed of at least %s,0 kilometres per hour", 
		pcase->p9_CarNum, pcase->p1_SpeedLaw, pcase->p2_SpeedIs);

	CString soffence_is = scomment2;
	soffence_is += _T("\n\n");
	soffence_is += scomment3;

	page->Print(pregionOffenceDetail, C2I(0.1),  C2I(0.1), TEXT_LEFT, 8,  soffence_is);

	// code of court and officer  
/****
	CPrintRegion * pregionCodes = page->CreateRegion(C2I_Y(10.5), C2I_X(13.0), C2I_Y(11.9), C2I_X(17.0));
	if (pregionCodes == NULL) 
		return;
	if (m_bDrawBorder)
		pregionCodes->DrawBorder();

	sline.Format(_T("Court\t: %d\nOfficer\t: %s\nOffence\t: %s"), 
		pcase->k_CodeCourt,pcase->p1_Officer, pcase->p3_OffenceCode);
	page->Print(pregionCodes, C2I(0.1),  C2I(0.1), TEXT_LEFT|TEXT_EXPANDTABS, 8,  sline);
****/

	CPrintRegion * pregionOffenceCode = page->CreateRegion(C2I_Y(10.5), C2I_X(15.0), C2I_Y(11.3), C2I_X(17.5));
	if (pregionOffenceCode == NULL) 
		return;
	if (m_bDrawBorder)
		pregionOffenceCode->DrawBorder();
	page->Print(pregionOffenceCode, C2I(0.25),  C2I(0.1), TEXT_LEFT, 8,  pcase->p3_OffenceCode);

	CPrintRegion * pregionPoliceCode = page->CreateRegion(C2I_Y(11.3), C2I_X(13.0), C2I_Y(11.8), C2I_X(17.0));
	if (pregionPoliceCode == NULL) 
		return;
	if (m_bDrawBorder)
		pregionPoliceCode->DrawBorder();
	page->Print(pregionPoliceCode, C2I(0.1),  C2I(0.1), TEXT_LEFT, 8,  pcase->p1_Officer);
}

void CIssuePrint_Notices::print_case_payduedate(XONE_CASE* pcase, bool bDueDate, LPCSTR pDueDate)
{
	CPage *	page = GetPage();
	CPrintRegion * pregionDue = page->CreateRegion(C2I_Y(12.6), C2I_X(2.0), C2I_Y(13.4), C2I_X(8.5));
	if (pregionDue == NULL) 
		return;
	if (m_bDrawBorder)
		pregionDue->DrawBorder();

	CString sduedate = (bDueDate ? pDueDate : pcase->k_PayDueDate);

	if (sduedate.IsEmpty() == true)
		return;

	int npos = sduedate.Find(_T("00:00:00"));
	if (npos > 0) {
		sduedate = sduedate.Left(npos);
	}
	CString syear  = sduedate.Left(4);
	CString smonth = sduedate.Mid(5, 2);
	CString sday   = sduedate.Mid(8, 2);

	int nmonth = atoi(smonth);
	nmonth--;

	CString sDueFinalDate;
	if (nmonth < 12) {
		sDueFinalDate.Format(_T("%s %s %s"), 
			sday, NameMonths[nmonth], syear);
	}

	page->Print(pregionDue, C2I(0.1),  C2I(0.1), TEXT_CENTER, 10,  sDueFinalDate);

}

void CIssuePrint_Notices::print_case_court(XONE_CASE* pcase)
{
	CPage *	page = GetPage();
	CPrintRegion * pregionCourt = page->CreateRegion(C2I_Y(12.6), C2I_X(9.5), C2I_Y(13.4), C2I_X(15.5));
	if (pregionCourt == NULL) 
		return;
	if (m_bDrawBorder)
		pregionCourt->DrawBorder();

	page->Print(pregionCourt, C2I(0.1),  C2I(0.1), TEXT_CENTER, 10,  pcase->p1_Court);
}

void CIssuePrint_Notices::print_case_fine(XONE_CASE* pcase)
{
	CPage *	page = GetPage();
	CPrintRegion * pregionFine = page->CreateRegion(C2I_Y(14.0), C2I_X(3.5), C2I_Y(14.5), C2I_X(5.5));
	if (pregionFine == NULL) 
		return;
	if (m_bDrawBorder)
		pregionFine->DrawBorder();

	page->Print(pregionFine, C2I(0.1),  C2I(0.1), TEXT_CENTER|TEXT_BOLD, 10,  pcase->p3_Fine);
}

void CIssuePrint_Notices::print_case_enquries(XONE_CASE* pcase)
{
	CPage *	page = GetPage();
	CPrintRegion * pregionPhones = page->CreateRegion(C2I_Y(15.0), C2I_X(0.5), C2I_Y(16.4), C2I_X(8.0));
	if (pregionPhones == NULL) 
		return;
	if (m_bDrawBorder)
		pregionPhones->DrawBorder();

	CString sphones;
	//sphones.Format("Enquries :\tTel  (028) 713 8070\n"
	//			   "\t\tFax  (028) 713 8074\n"
	//			   "\t\t(email: traffic@reversedale.co.za)");

	//CPredefineInfo& info = TheTPrinter.GetPreinfo();
	
	sphones.Format("Enquries :\tTel  %s\n"
				   "\t\tFax  %s\n", 
				   m_pInfo->m_sEnq_1, m_pInfo->m_sEnq_2);
	if (m_pInfo->m_sEnq_3.IsEmpty() == FALSE) {
		sphones += _T("\t\temail : ");
		sphones += m_pInfo->m_sEnq_3;
	}
				   
	// 
	page->Print(pregionPhones, C2I(0.1),  C2I(0.1), TEXT_LEFT|TEXT_EXPANDTABS, 7,  sphones);

	//
	CPrintRegion * pregionAdds = page->CreateRegion(C2I_Y(14.6), C2I_X(9.0), C2I_Y(16.4), C2I_X(17.5));
	if (pregionAdds == NULL) 
		return;
	if (m_bDrawBorder)
		pregionAdds->DrawBorder();
	
	CString sadds = _T("");
	sadds += m_pInfo->m_sEnq_Addr_1;	sadds += _T("\n");
	sadds += m_pInfo->m_sEnq_Addr_2;	sadds += _T("\n");
	sadds += m_pInfo->m_sEnq_Addr_3;	sadds += _T("\n");
	sadds += m_pInfo->m_sEnq_Addr_4;	sadds += _T("\n");
	sadds += m_pInfo->m_sEnq_Addr_5;

	page->Print(pregionAdds, C2I(0.1),  C2I(0.1), TEXT_LEFT|TEXT_EXPANDTABS, 7,  sadds);

}

void CIssuePrint_Notices::print_case_issueinfo(XONE_CASE* pcase)
{
	CPage *	page = GetPage();

	CPrintRegion * pregionIssuer = page->CreateRegion(C2I_Y(17.0), C2I_X(2.0), C2I_Y(18.6), C2I_X(8.5));
	if (pregionIssuer == NULL) 
		return;
	if (m_bDrawBorder)
		pregionIssuer->DrawBorder();

	//\\CPredefineInfo& info = TheTPrinter.GetPreinfo();

	CString sissuer = _T("");
	sissuer += m_pInfo->m_sIssuer_1;   sissuer += _T("\n");
	sissuer += m_pInfo->m_sIssuer_2;   sissuer += _T("\n");
	sissuer += m_pInfo->m_sIssuer_3;   sissuer += _T("\n");
	sissuer += m_pInfo->m_sIssuer_4;   sissuer += _T("\n");
	sissuer += m_pInfo->m_sIssuer_5;

	page->Print(pregionIssuer, C2I(0.1),  C2I(0.1), TEXT_LEFT|TEXT_EXPANDTABS, 7,  sissuer);
}

void CIssuePrint_Notices::print_case_printtoday(XONE_CASE* pcase, LPCSTR pdate)
{
	CPage *	page = GetPage();

	CPrintRegion * pregionToday = page->CreateRegion(C2I_Y(16.6), C2I_X(11.5), C2I_Y(17.0), C2I_X(17.5));
	if (pregionToday == NULL) 
		return;
	if (m_bDrawBorder)
		pregionToday->DrawBorder();

	CString stoday = pdate;
	page->Print(pregionToday, C2I(0.05),  C2I(0.1), TEXT_LEFT, 8,  stoday);
}

void CIssuePrint_Notices::print_case_issuehistory(XONE_CASE* pcase)
{
	CPage *	page = GetPage();

	CPrintRegion * pregionHistory = page->CreateRegion(C2I_Y(17.05), C2I_X(9.5), C2I_Y(18.5), C2I_X(17.5));
	if (pregionHistory == NULL) 
		return;
	if (m_bDrawBorder)
		pregionHistory->DrawBorder();

	CString sbuff;
	CString shistory = _T("");
	{
		if (strcmp(pcase->k_Last341, "") != 0)  {
			shistory += _T("last Notice issue Date : \t\t");
			//shistory += pcase->k_Last341;
			sbuff = pcase->k_Last341;
			shistory += sbuff.Left(10);
			shistory += _T("\n");
		}
		if (strcmp(pcase->k_LastNBS, "") != 0)  {
			shistory += _T("last NBS    issue Date : \t\t");
			//shistory += pcase->k_LastNBS;
			sbuff = pcase->k_LastNBS;
			shistory += sbuff.Left(10);
			shistory += _T("\n");
		}
		if (strcmp(pcase->k_LastSummon, "") != 0)  {
			shistory += _T("last Summon issue Date : \t");
			//shistory += pcase->k_LastSummon;
			sbuff = pcase->k_LastSummon;
			shistory += sbuff.Left(10);
			shistory += _T("\n");
		}
		if (strcmp(pcase->k_LastWOA, "") != 0)  {
			shistory += _T("last  WOA   issue Date : \t\t");
			//shistory += pcase->k_LastWOA;
			sbuff = pcase->k_LastWOA;
			shistory += sbuff.Left(10);
			shistory += _T("\n");
		}
	}

	page->Print(pregionHistory, C2I(0.05),  C2I(0.1), TEXT_LEFT|TEXT_EXPANDTABS, 8,  shistory);
}
