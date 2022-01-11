#include "StdAfx.h"
#include "Issue_Receipt.h"



CIssue_Receipt::CIssue_Receipt(printCase* pcase) : m_pCase(pcase)
{
}

CIssue_Receipt::~CIssue_Receipt(void)
{
}

void CIssue_Receipt::PrintReceipt()
{
	CDC*     pDC   = GetDC();
	CPage *	 page = GetPage();

	pDC->StartPage();

	////////////////////////////////////////////////////////////////////
	m_baseX = C2I(1.0);
	m_baseY = C2I(1.5);
	CPrintRegion * the_main = page->CreateRegion(
			C2I_Y(0.0),  C2I_X(0.0), C2I_Y(10.0), C2I_X(18.0));
	print_receipt(the_main);

	////////////////////////////////////////////////////////////////////
	page->Line(C2I(14.0), C2I(0.5), C2I(14.0),  C2I(18.0), 1, PEN_SOLID);
	//page->Line(C2I(14.1), C2I(0.5), C2I(14.1),  C2I(18.0), 1, PEN_DOT);
	//page->Line(C2I(14.2), C2I(0.5), C2I(14.2),  C2I(18.0), 1, PEN_DASH);
	//page->Line(C2I(14.3), C2I(0.5), C2I(14.3),  C2I(18.0), 1, PEN_THIN);
	//page->Line(C2I(14.4), C2I(0.5), C2I(14.4),  C2I(18.0), 1, PEN_THICK);
	//page->Line(C2I(14.5), C2I(0.5), C2I(14.5),  C2I(18.0), 1, PEN_DASHDOT);
	//page->Line(C2I(14.6), C2I(0.5), C2I(14.6),  C2I(18.0), 1, PEN_DASHDOTDOT);

	////////////////////////////////////////////////////////////////////
	m_baseX = C2I(1.0);
	m_baseY = C2I(16.0);
	CPrintRegion * the_main2 = page->CreateRegion(
			C2I_Y(0.0),  C2I_X(0.0), C2I_Y(10.0), C2I_X(18.0));
	print_receipt(the_main2);

	pDC->EndPage();
}

// 열전사 프린트용
void CIssue_Receipt::PrintReceipt_text()
{
	//CDC*     pDC = GetDC();
	//CPage *	 page = GetPage();

	//pDC->StartPage();

	////////////////////////////////////////////////////////////////////
	print_receipt_text();

	////////////////////////////////////////////////////////////////////
	//page->Line(C2I(14.0), C2I(0.5), C2I(14.0), C2I(18.0), 1, PEN_SOLID);
	//page->Line(C2I(14.6), C2I(0.5), C2I(14.6),  C2I(18.0), 1, PEN_DASHDOTDOT);

	////////////////////////////////////////////////////////////////////

	//print_receipt_text();

	//pDC->EndPage();
}

void CIssue_Receipt::print_receipt(CPrintRegion * the_main_area)
{
	//m_pThePage->SetColor(COLOR_RED);
	print_title(the_main_area);

	//m_pThePage->SetColor(COLOR_BLACK);

	print_unique_num(the_main_area);
	print_town(the_main_area);
	print_date(the_main_area);
	print_pay_method(the_main_area);

	print_offender_name(the_main_area);
	print_sun_of_amount(the_main_area);
	print_its_traffice_fine(the_main_area);
	print_unique_num2(the_main_area);
	print_signature(the_main_area);
	print_amount_box(the_main_area);
}

void CIssue_Receipt::print_receipt_text()
{
	CString snotice_num = _T("");   // "Notice Number is ...";
	CString sreceipt_num = _T("");  // "Receipt Number is ...";
	CString sDate = _T(""), sCasher = _T(""), sReceived = _T("");

	snotice_num.Format("Notice  number: %s", m_pCase->k_NoticeNum);
	sreceipt_num.Format("Receipt number: %s", m_pCase->k_PayBillNum);
	sDate.Format("Date/Datum: %s", m_pCase->k_PayTime);
	sCasher.Format("Received from: %s(%s)", m_pCase->k_PayerName, m_pCase->k_PayerPhone);
	sReceived.Format("Received : %d", m_pCase->k_Payed);

	CPage* page = m_pThePage;
	
	double row = page->Print(0.0, 0.0, TEXT_LEFT | TEXT_BOLD, 10, "NEWCASTLE MUNICIPALITY TRAFFIC DEPARTMENT");
	row = page->Print(row, 0.0, TEXT_LEFT | TEXT_ITALIC, 10, "Cash Receipt / Kontant Kwitansie ");
	row = page->Print(row, 0.0, TEXT_LEFT, 10, sDate);
	row = page->Print(row, 0.0, TEXT_LEFT, 10, sCasher);
	row = page->Print(row, 0.0, TEXT_LEFT, 10, "Traffic Fine");
	row = page->Print(row, 0.0, TEXT_LEFT, 10, sReceived);
	row = page->Print(row, 0.0, TEXT_LEFT, 10, snotice_num);
	row = page->Print(row, 0.0, TEXT_LEFT, 10, sreceipt_num);
	row = page->Print(row, 0.0, TEXT_LEFT, 10, "---------------------------------");


}

void CIssue_Receipt::print_title(CPrintRegion * the_area)
{
	//CString stitle1 = "GREATER TUBATSE MUNICIPALITY TRAFFIC DEPARTMENT   ";
	CString stitle1 = "NEW CASTLE MUNICIPALITY TRAFFIC DEPARTMENT   ";
	CString stitle2 = "Cash Receipt / Kontant Kwitansie ";

	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(0.0), C2I(0.0), C2I(1.0), C2I(18.0));
	//page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_LEFT|TEXT_BOLD, 14,  stitle1);
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_CENTER|TEXT_BOLD, 14,  stitle1);

	pBox = page->CreateSubRegion(the_area, 
		C2I(0.8), C2I(0.0), C2I(2.0), C2I(15.0));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_LEFT|TEXT_ITALIC, 10,  stitle2);

}

void CIssue_Receipt::print_unique_num(CPrintRegion * the_area)
{
	CString snotice_num = _T("");    // "Notice Number is ...";
	CString sreceipt_num = _T("");  // "Receipt Number is ...";

	snotice_num.Format ("Notice  number: %s", m_pCase->k_NoticeNum);
	sreceipt_num.Format("Receipt number: %s", m_pCase->k_PayBillNum);

	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(1.0), C2I(11.0), C2I(1.8), C2I(18.0));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_LEFT, 10,  snotice_num);

	pBox = page->CreateSubRegion(the_area, 
		C2I(1.5), C2I(11.0), C2I(2.0), C2I(18.0));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_LEFT, 10,  sreceipt_num);

}

void CIssue_Receipt::print_town(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(2.5), C2I(0.0), C2I(3.2), C2I(3.8));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 10,  "Town/Drop:  ");

	pBox = page->CreateSubRegion(the_area, 
		C2I(2.5), C2I(4.0), C2I(3.0), C2I(10.0));
	pBox->DrawBorder();
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_CENTER, 10,  m_pCase->k_PayPoint);

}

void CIssue_Receipt::print_date(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(2.5), C2I(11.0), C2I(3.2), C2I(14.0));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 10,  "Date/Datum:  ");

	CString sbuff  = m_pCase->k_PayTime;
	CString sToday = sbuff.Left(10);

	pBox = page->CreateSubRegion(the_area, 
		C2I(2.5), C2I(14.5), C2I(3.2), C2I(18.0));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_LEFT|TEXT_UNDERLINED, 10,  sToday);
}

void CIssue_Receipt::print_pay_method(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(3.5), C2I(0.0), C2I(4.0), C2I(3.8));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 10,  "Payment/Betaling:  ");

	// 
	pBox = page->CreateSubRegion(the_area, 
		C2I(3.5), C2I(4.0), C2I(4.0), C2I(6.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 10,  "Cash/Kontant:  ");

	pBox = page->CreateSubRegion(the_area, 
		C2I(3.5), C2I(6.5), C2I(4.0), C2I(7.0));
	if (m_pCase->k_PayType == 0) { // cash 
		pBox->DrawTitle("V");
	}
	else 
		pBox->DrawBorder();

	//
	pBox = page->CreateSubRegion(the_area, 
		C2I(3.5), C2I(7.0), C2I(4.0), C2I(9.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 10,  "Cheque/Tjek:  ");

	pBox = page->CreateSubRegion(the_area, 
		C2I(3.5), C2I(9.5), C2I(4.0), C2I(10.0));
	if (m_pCase->k_PayType == 1) { // cheque
		pBox->DrawTitle("V");
	}
	else 
		pBox->DrawBorder();

	//
	pBox = page->CreateSubRegion(the_area, 
		C2I(3.5), C2I(10.0), C2I(4.0), C2I(14.0));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 10,  "Postal Order or Others:  ");

	pBox = page->CreateSubRegion(the_area, 
		C2I(3.5), C2I(14.0), C2I(4.0), C2I(14.5));
	if (m_pCase->k_PayType >= 3) { // others  
		pBox->DrawTitle("V");
	}
	else 
		pBox->DrawBorder();
}

///////////////////////////////////////////////////////////////////////////////
//
//
//
///////////////////////////////////////////////////////////////////////////////

void CIssue_Receipt::print_offender_name(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(5.0), C2I(0.0), C2I(6.0), C2I(2.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 8,  "Received from:\nOntvang van:");

	page->Line(C2I_Y(5.8), C2I_X(2.6), C2I_Y(5.8), C2I_X(12.5), 1, PEN_DOT);

	CString sname;
	if (strcmp(m_pCase->k_PayerPhone, "") != 0)
		sname.Format("%s (%s)", m_pCase->k_PayerName, m_pCase->k_PayerPhone);
	else
		sname = m_pCase->k_PayerName;

	pBox = page->CreateSubRegion(the_area, 
		C2I(5.0), C2I(2.6), C2I(6.0), C2I(12.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_CENTER, 12,  sname);


}

void CIssue_Receipt::print_sun_of_amount(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(6.0), C2I(0.0), C2I(7.0), C2I(2.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 8,  "The sum of:\nDie bedrag van:");

	page->Line(C2I_Y(6.8), C2I_X(2.6), C2I_Y(6.8), C2I_X(12.5), 1, PEN_DOT);

	CString sPayed;
	sPayed.Format("R %d.00", m_pCase->k_Payed);
	pBox = page->CreateSubRegion(the_area, 
		C2I(6.0), C2I(2.6), C2I(7.0), C2I(12.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_CENTER, 12,  sPayed);
}

void CIssue_Receipt::print_its_traffice_fine(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(7.0), C2I(0.0), C2I(8.0), C2I(2.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 8,  "Sundry:\nDiverse:");

	page->Line(C2I_Y(7.8), C2I_X(2.6), C2I_Y(7.8), C2I_X(12.5), 1, PEN_DOT);

	CString sname = "Traffic Fine";
	pBox = page->CreateSubRegion(the_area, 
		C2I(7.0), C2I(2.6), C2I(8.0), C2I(12.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_CENTER, 12,  sname);
}

void CIssue_Receipt::print_unique_num2(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(8.0), C2I(0.0), C2I(9.0), C2I(2.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 8,  "Account Nr.:\nRekening No.:");

	page->Line(C2I_Y(8.8), C2I_X(2.6), C2I_Y(8.8), C2I_X(12.5), 1, PEN_DOT);

	CString sAccount;
	sAccount.Format("%s & %s", m_pCase->k_NoticeNum, m_pCase->k_PayBillNum);
	pBox = page->CreateSubRegion(the_area, 
		C2I(8.0), C2I(2.6), C2I(9.0), C2I(12.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_CENTER, 10,  sAccount);
}

void CIssue_Receipt::print_signature(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;
	CPrintRegion * pBox = NULL;

	pBox = page->CreateSubRegion(the_area, 
		C2I(9.0), C2I(0.0), C2I(10.0), C2I(2.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_RIGHT, 8,  "Singature:\nHandtekening");

	page->Line(C2I_Y(10.0), C2I_X(2.6), C2I_Y(10.0), C2I_X(12.5), 1, PEN_DOT);

	CString sCahser;
	sCahser.Format("casher: %s", m_pCase->k_PayCasher);
	pBox = page->CreateSubRegion(the_area, 
		C2I(9.0), C2I(3.6), C2I(10.0), C2I(12.5));
	page->Print(pBox,  C2I(0.03),  C2I(0.05), TEXT_LEFT, 12,  sCahser);
}

void CIssue_Receipt::print_amount_box(CPrintRegion * the_area)
{
	CPage* page = m_pThePage;

	CPrintRegion * pBoxR = page->CreateSubRegion(the_area, 
		C2I(5.0), C2I(13.0), C2I(6.0), C2I(16.0));
	pBoxR->DrawBorder();
	page->Print(pBoxR,  C2I(0.3),  C2I(0.0), TEXT_CENTER|TEXT_VCENTER, 8,  "R");

	CPrintRegion * pBoxC = page->CreateSubRegion(the_area, 
		C2I(5.0), C2I(16.0), C2I(6.0), C2I(18.0));
	pBoxC->DrawBorder();
	page->Print(pBoxC,  C2I(0.3),  C2I(0.0), TEXT_CENTER|TEXT_VCENTER, 8,  "C");

	////////////////////////////////////////////////////////////////////
	CPrintRegion * pBoxR2 = page->CreateSubRegion(the_area, 
		C2I(6.0), C2I(13.0), C2I(9.0), C2I(16.0));
	pBoxR2->DrawBorder();
	
	CString sAmount;
	sAmount.Format("%d", m_pCase->k_Payed);
	page->Print(pBoxR2,  C2I(1.3),  C2I(0.0), TEXT_CENTER|TEXT_VCENTER, 12,  sAmount);

	CPrintRegion * pBoxC2 = page->CreateSubRegion(the_area, 
		C2I(6.0), C2I(16.0), C2I(9.0), C2I(18.0));
	pBoxC2->DrawBorder();
	page->Print(pBoxC2,  C2I(1.3),  C2I(0.0), TEXT_CENTER|TEXT_VCENTER, 12,  "00");

	////////////////////////////////////////////////////////////////////

	CPrintRegion * pBoxR3 = page->CreateSubRegion(the_area, 
		C2I(9.0), C2I(13.0), C2I(10.0), C2I(16.0));
	pBoxR3->DrawBorder();

	CPrintRegion * pBoxC3 = page->CreateSubRegion(the_area, 
		C2I(9.0), C2I(16.0), C2I(10.0), C2I(18.0));
	pBoxC3->DrawBorder();
}

