#include "StdAfx.h"
#include "IssuePrint_Form.h"

///////////////////////////////////////////////////////////////////////////////

CString sA_UL_Title = "NO. VAN EERSTE\nKENNISGEWING\nINGEVOLGE ART. 341\nVAN DIE STRAF -\nPROSESWET 1977";
CString sA_UR_Title = "NO. OF FIRST NOTICE\nISSUED IN TERMS OF\nSEC. 341 OF THE\nCRIMINAL PROCEDURE\nACT 1977";

CString sB_U_Title = 
	"NEEM KENNIS DAT DIE BOETE VIR DIE ONDERGENOEMDE OORTREDING OP 'N KENNISGEWING INGEVOLGE "
	"ARTIKEL 341 VAN DIE STRAFPROSESWET, 1977, NOG NIE BETAAL IS NIE EN VOOR OF OP DIE "
	"ONDERGENOEMDE BETAALDATUM BETAAL MOET WORD OM DAGVAARDING TE VERMY. TAKE NOTE THAT A NOTICE "
	"IN TERMS OF SECTION 341 OF THE CRIMINAL PROCEDURES ACT, 1977, WAS ISSUED FOR THE OFFICE "
	"MENTIONED BELOW AND THAT THE FINE MUST BE PAID ON OR BEFORE THE PAYMENT DATE MENTIONED BELOW "
	"TO AVOID BEING SUMMONSED. ";

CString sB_U_Title2 = "DAT die beskuldigde skuldig is aan die oortreding van";
CString sB_U_Title3 = "IN THAT the accused is guilty of contravening";

CString sB_U_Title4 = "DEURDAT die beskuldige op of omtrent\nIN THAT the accused upon or about";
CString sB_U_Title5 = "te / naby / op\nat / near / on\n'n plek openbare pad in die ondergemelde distrik, "
	"wederregtetelik\na place / public road in the district mentioned below, wrongfully";


CString sD_U_TitleL = "PLEK WAAR BOETE BETAAL KAN WORD TEN EINDE DAGVAARDING\nTE VERMY";
CString sD_U_TitleR = "PLACE WHERE FINE MAY BE PAID TO AVOID \nSUMMONS";
CString sD_U_TitleM1 = "n Boete en bedrae van\nA fine of\nSlegs by:\nOnly at";
CString sD_U_TitleM2 = "kan betaal word, in welke geval betaling moet geskied voor of op bogenoemde betaaldatum."
	"\nmay be paid, in which case payment must be made on or before abovementioned payment date. ";

CString sG_L_Title = "BELANGRIKE INLIGTING VIR DIE OORTREDER\n(SYNDE DIE EIENAAR/BESTUURDER)";
CString sG_R_Title = "IMPORTANT INFORMATION TO THE OFFENDER\n(BEING THE OWNER/DRIVER)";

CString sG_L_Content = 
"1.	NB: DIE BOETE KAN NIE BY 'N POLISIE KANTOOR OF 'N HOF BETAAL WORD NIE.\n"
"2.	WAARSKUWING: DIE BETALING VAN DIE BEGENOEMDE BOETE MOET VOOR OF OP DIE "
   "BOGENOEMDE BETAALDATUM GESKIED TEN EINDE DAGVAARDING TE VERMY. \n"
"3.	VOORWAARDE VIR BETALING VAN BOETES: \n"
"	MET BETALING VAN DIE BOETE:-\n"
"\t3.1.	MOET HIERDIE DOKUMENT DIE BETALING VERGESEL. \n"
"\t3.2.	SLEGS KONTANT, 'N POSORDER, 'N POSWISSEL OF 'N 	BANKGEWAARBORGDE TJEK SAL AANVAAR WORD.	\n"
"\t3.3.	MOET POSORDERS, POSWISSELS OF TJEKS AAN DIE BETROKKE OWERHEID UITGEMAAK WORD. \n"
"\t3.4.	SAL U NIE LATER IN DIE HOF HOEF TE VERSKYN NIE.	\n"
"4.	BETALING PER POS MOET BOGENOEMDE ADRES VOOR DIE\n" 
"\t BETAALDATUM, SOOS AANGEDUI IN AFDELING C, BEREIK.\n"
"5.	IGNOREER HIERDIE KENNISGEWING INDIEN DIE BOETE ALREEDS BETAAL IS.";

CString sG_R_Content = 
"1.	NB: THE FINE CANNOT BE PAID AT A POLICE STATION OR A COURT.	\n"
"2.	WARNING: THE ABOVEMENTIONED FINE MUST BE PAID ON OR BEFORE THE ABOVEMENTIONED"
" PAYMENT DATE TO AVOID BEING SUMMONSED. \n"
"3.	CONDITIONS FOR PAYMENT OF FINE:		\n"
"	WHEN PAYING THE FINE:-		\n"
"\t3.1.	THE DOCUMENTS IS TO ACCOMPANY SUCH PAYMENT \n"
"\t3.2.	ONLY CASH, A MONEY ORDER, A POSTAL ORDER OR A BANK GAURANTEED CHEQUE WILL BE ACCEPTED. \n"
"\t3.3.	POSTAL ORDERS, MONEY ORDERS OR CHEQUES MUST BE MADE PAYABLE TO THE RELEVANT AUTHORITY.  \n"
"\t3.4.	YOU NEED NOT LATER APPEAR IN COURT. \n"
"4.	PAYMENT BY POST MUST REACH THE ABOVEMENTIONED ADDRESS BEFORE THE PAYMENT DATE MENTIONED IN SECTION C. \n"
"5.	PLEASE IGNORE THIS NOTIFICATION IF THE FINE HAS ALREADY BEEN PAID.  \n";

///////////////////////////////////////////////////////////////////////////////

RECT_REGION_CENTI	NBS_AREA_A[] =
{
	{  0.0,	 0.0,	12.5,	4.5 },
	{ 12.5,	 0.0,	18.0,	1.5 },
	{ 12.5,	 1.5,	18.0,	4.5 }
};

RECT_REGION_CENTI	NBS_AREA_B[] = 
{
	{  0.0,	 0.0,	18.0,	7.5 }
};

RECT_REGION_CENTI	NBS_AREA_C[] = 
{
	{  0.0,	 0.0,	18.0,	1.5 },
	{  0.0,	 0.0,	 1.2,	1.5 },
	{  1.2,	 0.0,	 9.0,	1.5 },
	{  9.0,	 0.0,	16.8,	1.5 },
	{ 16.8,	 0.0,	18.0,	1.5 }
};

RECT_REGION_CENTI	NBS_AREA_D[] = 
{
	{  0.0,	 0.0,	18.0,	3.0 }
};

RECT_REGION_CENTI	NBS_AREA_E[] = 
{
	{  0.0,	 0.0,	18.0,	2.2 }
};

RECT_REGION_CENTI	NBS_AREA_F[] = 
{
	{  0.0,	 0.0,	18.0,	7.0 }
};

///////////////////////////////////////////////////////////////////////////////

CIssuePrint_Form::CIssuePrint_Form(void)
{
}

CIssuePrint_Form::~CIssuePrint_Form(void)
{
}

void CIssuePrint_Form::PrintPage()
{
	CDC*     pDC   = GetDC();
	CPage *	 page = GetPage();

	pDC->StartPage();

	test_rotation();

	pDC->EndPage();
}

void CIssuePrint_Form::test_rotation()
{
	CPage *	 page = GetPage();

	double dinterval = 0.21;
	double dstartX = 15.0;
	CString sline;
	for (int i=0; i<30; i++) {
		sline.Format("%03d line 270 degree - start X = %.2f ... long text hahaha more more longer",
			i, dstartX);

		page->PrintRotatedText(C2I(9.0), C2I(dstartX), C2I(29.0), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE|TEXT_BOLD, 8, sline, 2700);

		dstartX += dinterval;
	}

	dinterval = 0.21;
	dstartX = 0.0;
	for (int i=0; i<30; i++) {
		sline.Format("%03d line 90 Degree - start X = %.2f ... long text hahaha more more longer",
			i, dstartX);

		page->PrintRotatedText(C2I(18.0), C2I(dstartX), C2I(29.0), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE|TEXT_BOLD, 8, sline, 900);

		dstartX += dinterval;
	}
}

void CIssuePrint_Form::PrintForm_NBS()
{
	CDC* pDC = GetDC();

	if (pDC == NULL) {
		return;
	}
	pDC->StartPage();
	
	print_form();

	pDC->EndPage();
}

void CIssuePrint_Form::print_form()
{
	nbs_sides();

	m_baseY = C2I(1.5);

	nbs_lines();
	nbs_area_A();
	nbs_area_A2();
	nbs_area_B();
	nbs_area_C();
	nbs_area_C2();
	nbs_area_D();
	nbs_area_E();
	nbs_area_F();
}

void CIssuePrint_Form::nbs_sides()
{
	double dinterval = 0.25;
	double dstartX = 0.0;
	double dstartY = 18.5;
	double dendY = 29.0;

	CString sline1 = "KENNISGEWING VOOR           NOTICE BEFORE";
	CString sline2 = "DAGVAARDING IN STRAFSAAK - SUMMONS IN CRIMINAL CASE";
	CString sline3 = "LANDDROSHOF         MAGISTRATE S COURT";

	m_pThePage->SetColor(COLOR_RED);

	/////////////////////////////////////////////////////////////////////////////////////
	// LEFT  side 
	m_pThePage->PrintRotatedText(C2I(dstartY), C2I(dstartX), C2I(dendY), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE|TEXT_BOLD, 8, sline1, 900);
	dstartX += dinterval;

	m_pThePage->PrintRotatedText(C2I(dstartY+1.0), C2I(dstartX), C2I(dendY), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE|TEXT_BOLD, 8, sline2, 900);
	dstartX += dinterval;

	m_pThePage->PrintRotatedText(C2I(dstartY-1.0), C2I(dstartX), C2I(dendY), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE, 8, sline3, 900);

	/////////////////////////////////////////////////////////////////////////////////////
	// RIGHT side 
	dstartX = 20.05;
	dstartY = 11.0;

	m_pThePage->PrintRotatedText(C2I(dstartY), C2I(dstartX), C2I(dendY), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE|TEXT_BOLD, 8, sline1, 2700);
	dstartX -= dinterval;

	m_pThePage->PrintRotatedText(C2I(dstartY-1.0), C2I(dstartX), C2I(dendY), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE|TEXT_BOLD, 8, sline2, 2700);
	dstartX -= dinterval;

	m_pThePage->PrintRotatedText(C2I(dstartY+1.0), C2I(dstartX), C2I(dendY), C2I(dstartX+dinterval), 
			TEXT_NOCLIP|TEXT_SINGLELINE, 8, sline3, 2700);

	// post processing ... 
	m_baseX = C2I(dinterval*3.8);

}

void CIssuePrint_Form::nbs_lines()
{
	m_pThePage->SetColor(COLOR_BLACK);

	m_pThePage->Line(
		C2I_Y(NBS_AREA_A[0].y1),  C2I_X(NBS_AREA_A[0].x1),  
		C2I_Y(NBS_AREA_A[0].y2),  C2I_X(NBS_AREA_A[0].x1));
	m_pThePage->Line(
		C2I_Y(NBS_AREA_A[1].y1),  C2I_X(NBS_AREA_A[1].x1),  
		C2I_Y(NBS_AREA_A[2].y2),  C2I_X(NBS_AREA_A[1].x1));
	m_pThePage->Line(
		C2I_Y(NBS_AREA_A[1].y1),  C2I_X(NBS_AREA_A[1].x2),  
		C2I_Y(NBS_AREA_A[2].y2),  C2I_X(NBS_AREA_A[1].x2));
	m_pThePage->Line(
		C2I_Y(NBS_AREA_A[2].y1),  C2I_X(NBS_AREA_A[2].x1),  
		C2I_Y(NBS_AREA_A[2].y1),  C2I_X(NBS_AREA_A[2].x2));

	// 맨위 라인 그리기 추가 [정병영]
	m_pThePage->Line(C2I_Y(0.0), C2I_X(0.0), C2I_Y(0.0), C2I_X(18.0));
	
	m_pThePage->Line(C2I_Y(4.5),  C2I_X(0.0),  C2I_Y(25.0),  C2I_X(0.0));
	m_pThePage->Line(C2I_Y(4.5),  C2I_X(18.0), C2I_Y(25.7),  C2I_X(18.0));

}

void CIssuePrint_Form::nbs_sub_title(CPrintRegion *pregion, LPCSTR ptitle)
{
	CPage* page = m_pThePage;

	CPrintRegion * pBox = page->CreateSubRegion(pregion, C2I(0.0), C2I(0.0), C2I(0.65), C2I(0.5));
	//pBox->DrawBorder();
	page->Line(pregion, C2I(0.0),   C2I(0.5),  C2I(0.65),  C2I(0.5), 1);
	page->Line(pregion, C2I(0.65),  C2I(0.0),  C2I(0.65),  C2I(0.5), 1);

	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_BOLD, 14,  ptitle);
}

void CIssuePrint_Form::nbs_area_A()
{
	CPage* page = m_pThePage;

	CPrintRegion * region_A[3];  // sizeof(NBS_AREA_A)/sizeof(RECT_REGION_CENTI)
	for (int i=0; i<3; i++) 
	{
		region_A[i] = page->CreateRegion(
			C2I_Y(NBS_AREA_A[i].y1), C2I_X(NBS_AREA_A[i].x1), 
			C2I_Y(NBS_AREA_A[i].y2), C2I_X(NBS_AREA_A[i].x2));
	}
	nbs_sub_title(region_A[0], "A");

	page->Print(region_A[0], C2I(0.05),  C2I(0.8), TEXT_NORMAL, 7,  "AAN\nTO");
	page->Print(region_A[1], C2I(0.05),  C2I(0.3), TEXT_NORMAL, 6, sA_UL_Title);
	page->Print(region_A[1], C2I(0.05),  C2I(3.0), TEXT_NORMAL, 6, sA_UR_Title);

	page->Line(region_A[1], C2I(0.1),  C2I(2.7),  C2I(1.3),  C2I(2.5), 1);

	// post processing ... 
	m_baseY += C2I(NBS_AREA_A[0].y2);
}

void CIssuePrint_Form::nbs_area_A2()
{
}

void CIssuePrint_Form::nbs_area_B()
{
	CPage* page = m_pThePage;

	CPrintRegion * region_B[1];  // sizeof(NBS_AREA_B)/sizeof(RECT_REGION_CENTI)
	for (int i=0; i<1; i++) 
	{
		region_B[i] = page->CreateRegion(
			C2I_Y(NBS_AREA_B[i].y1), C2I_X(NBS_AREA_B[i].x1), 
			C2I_Y(NBS_AREA_B[i].y2), C2I_X(NBS_AREA_B[i].x2));
	}
	region_B[0]->DrawBorder();

	nbs_sub_title(region_B[0], "B");

	double row = 0.0;
	row = page->Print(region_B[0], C2I(0.05),  C2I(0.8), TEXT_NORMAL, 6, sB_U_Title);
	row = page->Print(region_B[0],  row, C2I(0.8), TEXT_NORMAL, 6, sB_U_Title2);
	row = page->Print(region_B[0],  row, C2I(0.8), TEXT_NORMAL, 6, sB_U_Title3);

	page->Print(region_B[0],  row+C2I(1.5), C2I(0.8), TEXT_NORMAL, 6, sB_U_Title4);
	page->Print(region_B[0],  row+C2I(1.5), C2I(9.8), TEXT_NORMAL, 6, sB_U_Title5);
	page->Print(region_B[0],  row+C2I(4.7), C2I(14.0), TEXT_NORMAL, 6, "Kode\nCode");
	page->Print(region_B[0],  C2I(6.8), C2I(12.0), TEXT_NORMAL, 6, "Inspno.\nInsp. No.");

	// post processing ... 
	m_baseY += C2I(NBS_AREA_B[0].y2);
}

void CIssuePrint_Form::nbs_area_C()
{
	CPage* page = m_pThePage;

	page->SetColor(COLOR_RED);

	CPrintRegion * region_C[5];  // sizeof(NBS_AREA_C)/sizeof(RECT_REGION_CENTI)
	for (int i=0; i<5; i++) 
	{
		region_C[i] = page->CreateRegion(
			C2I_Y(NBS_AREA_C[i].y1), C2I_X(NBS_AREA_C[i].x1), 
			C2I_Y(NBS_AREA_C[i].y2), C2I_X(NBS_AREA_C[i].x2));
	}
	
	region_C[1]->DrawBorder();
	region_C[2]->DrawBorder();
	region_C[3]->DrawBorder();
	region_C[4]->DrawBorder();

	page->Line(region_C[1], C2I(0.0),  C2I(0.0),  C2I(0.75),  C2I(1.2), 1);
	page->Line(region_C[1], C2I(1.5),  C2I(0.0),  C2I(0.75),  C2I(1.2), 1);
	page->Line(region_C[4], C2I(0.0),  C2I(1.2),  C2I(0.75),  C2I(0.0), 1);
	page->Line(region_C[4], C2I(1.5),  C2I(1.2),  C2I(0.75),  C2I(0.0), 1);

	nbs_sub_title(region_C[0], "C");

	double row1 = page->Print(region_C[2],  C2I(0.15),  C2I(1.0), TEXT_CENTER, 8,  "BETAALDATUM / PAYMENT DATE");
	double row2 = page->Print(region_C[3],  C2I(0.15),  C2I(1.0), TEXT_CENTER, 8,  "LANDROSDISTRIK / MAGESTERIAL DISTRICT");

	// post processing ... 
	m_baseY += C2I(NBS_AREA_C[0].y2);
}

void CIssuePrint_Form::nbs_area_C2()
{
}

void CIssuePrint_Form::nbs_area_D()
{
	CPage* page = m_pThePage;

	CPrintRegion * region_D[1];  // sizeof(NBS_AREA_D)/sizeof(RECT_REGION_CENTI)
	for (int i=0; i<1; i++) 
	{
		region_D[i] = page->CreateRegion(
			C2I_Y(NBS_AREA_D[i].y1), C2I_X(NBS_AREA_D[i].x1), 
			C2I_Y(NBS_AREA_D[i].y2), C2I_X(NBS_AREA_D[i].x2));
	}
	region_D[0]->DrawBorder();

	nbs_sub_title(region_D[0], "D");

	double row = 0;
	row = page->Print(region_D[0],  C2I(0.05), C2I(0.8), TEXT_NORMAL, 6, sD_U_TitleL);
	row = page->Print(region_D[0],  C2I(0.05), C2I(9.2), TEXT_NORMAL, 6, sD_U_TitleR);
	page->Print(region_D[0],  row , C2I(0.8), TEXT_BOLD, 6, sD_U_TitleM1);
	page->Print(region_D[0],  row , C2I(5.8), TEXT_BOLD, 6, sD_U_TitleM2);
	page->Print(region_D[0],  row+C2I(0.1) , C2I(3.2), TEXT_BOLD, 10, "R");

	// post processing ... 
	m_baseY += C2I(NBS_AREA_D[0].y2);
}

void CIssuePrint_Form::nbs_area_E()
{
	CPage* page = m_pThePage;

	page->SetColor(COLOR_BLACK);

	CPrintRegion * region_E[1];  // sizeof(NBS_AREA_D)/sizeof(RECT_REGION_CENTI)
	for (int i=0; i<1; i++) 
	{
		region_E[i] = page->CreateRegion(
			C2I_Y(NBS_AREA_E[i].y1), C2I_X(NBS_AREA_E[i].x1), 
			C2I_Y(NBS_AREA_E[i].y2), C2I_X(NBS_AREA_E[i].x2));
	}
/**
	page->Line(region_E[0], 
			C2I(NBS_AREA_E[0].y1), C2I(NBS_AREA_E[0].x1), 
			C2I(NBS_AREA_E[0].y2), C2I(NBS_AREA_E[0].x1), 1);
	page->Line(region_E[0], 
			C2I(NBS_AREA_E[0].y1), C2I(NBS_AREA_E[0].x2), 
			C2I(NBS_AREA_E[0].y2), C2I(NBS_AREA_E[0].x2), 1);
**/
	nbs_sub_title(region_E[0], "E");

	double row = 0;
	row = page->Print(region_E[0],  C2I(0.05), C2I(0.8), TEXT_BOLD, 6, "Uitgereik deur");
	page->Print(region_E[0],  C2I(0.05), C2I(9.8), TEXT_BOLD, 6, "Datum / Date");
	page->Print(region_E[0],  row+C2I(0.8) , C2I(0.8), TEXT_BOLD, 6, "Issued by");

	// post processing ... 
	m_baseY += C2I(NBS_AREA_E[0].y2);
}

void CIssuePrint_Form::nbs_area_F()
{
	CPage* page = m_pThePage;

	page->SetColor(COLOR_BLACK);

	CPrintRegion * region_F[1];  // sizeof(NBS_AREA_D)/sizeof(RECT_REGION_CENTI)
	for (int i=0; i<1; i++) 
	{
		region_F[i] = page->CreateRegion(
			C2I_Y(NBS_AREA_F[i].y1), C2I_X(NBS_AREA_F[i].x1), 
			C2I_Y(NBS_AREA_F[i].y2), C2I_X(NBS_AREA_F[i].x2));
	}
	region_F[0]->DrawBorder();

	nbs_sub_title(region_F[0], "F");

	//
	CPrintRegion *RegionL = page->CreateRegion(C2I_Y(0.6),  C2I_X(1.0),   C2I_Y(6.8),  C2I_X(8.5));
	CPrintRegion *RegionR = page->CreateRegion(C2I_Y(0.6),  C2I_X(9.5),  C2I_Y(6.8),  C2I_X(17.5));

	page->SetColor(COLOR_RED);

	double row = 0;
	row = page->Print(RegionL,  C2I(0.0),  C2I(0.1), TEXT_BOLD|TEXT_CENTER, 7,  sG_L_Title);
	row = page->Print(RegionR,  C2I(0.0),  C2I(0.1), TEXT_BOLD|TEXT_CENTER, 7,  sG_R_Title);

	page->Print(RegionL,  row + C2I(0.2),  C2I(0.1), TEXT_NORMAL, 7,  sG_L_Content);
	page->Print(RegionR,  row + C2I(0.2),  C2I(0.1), TEXT_NORMAL, 7,  sG_R_Content);
	page->SetColor(COLOR_BLACK);
}

