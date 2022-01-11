#pragma once

#include "IssuePrint.h"

class CIssuePrint_Form : public CIssuePrint
{
public:
	CIssuePrint_Form(void);
	virtual ~CIssuePrint_Form(void);

protected:
	void	test_rotation();

	void	nbs_sides();
	void	nbs_lines();
	void	nbs_area_A();
	void	nbs_area_A2();
	void	nbs_area_B();
	void	nbs_area_C();
	void	nbs_area_C2();
	void	nbs_area_D();
	void	nbs_area_E();
	void	nbs_area_F();

	void	nbs_sub_title(CPrintRegion *pregion, LPCSTR ptitle);

	void	print_form();

public:
	void	PrintPage();
	void	PrintForm_NBS();

};

typedef struct _rect_region_centi
{
	double	x1, y1;
	double  x2, y2;
} RECT_REGION_CENTI;
