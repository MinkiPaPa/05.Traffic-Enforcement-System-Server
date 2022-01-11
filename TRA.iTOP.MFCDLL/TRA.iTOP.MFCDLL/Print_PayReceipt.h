#pragma once

#include "Print_TBos.h"

typedef struct _info_pay_type
{
	int			nCode;
	char		pMethod[20];
} InfoPayType;


InfoPayType		ThePayMethods[] =
{
	{ 0, "Cash" },
	{ 1, "Cheque" },
	{ 2, "Credit Card" },
	{ 3, "Other ..." },
};



class CPrint_PayReceipt : public CPrint_TBos
{
public:
	CPrint_PayReceipt(HWND hwnd, int nmode);
	virtual ~CPrint_PayReceipt(void);

public:
	bool	Print_content(ONE_CASE* pcase);
};
