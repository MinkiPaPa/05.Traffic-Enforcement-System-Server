#pragma once
#include "IssuePrint.h"
//#include "data_Case.h"
#include "TRA.iTOP.MFCDLL.h"

class CIssue_Receipt :	public CIssuePrint
{
public:
	CIssue_Receipt(printCase* pcase);
	virtual ~CIssue_Receipt(void);

protected:
	printCase* m_pCase;

protected:
	void	print_receipt(CPrintRegion * the_main_area);
	void	print_receipt_text();

	void	print_title(CPrintRegion * the_area);
	void	print_unique_num(CPrintRegion * the_area);
	void	print_town(CPrintRegion * the_area);
	void	print_date(CPrintRegion * the_area);
	void	print_pay_method(CPrintRegion * the_area);

	void	print_offender_name(CPrintRegion * the_area);
	void	print_sun_of_amount(CPrintRegion * the_area);
	void	print_its_traffice_fine(CPrintRegion * the_area);
	void	print_unique_num2(CPrintRegion * the_area);
	void	print_signature(CPrintRegion * the_area);
	void	print_amount_box(CPrintRegion * the_area);


public:
	void	PrintReceipt();
	void	PrintReceipt_text();  // 열전사 프린트용

};
