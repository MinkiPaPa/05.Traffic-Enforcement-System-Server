#pragma once

#include "Print_TBos.h"
//#include "TRA.iTOP.MFCDLL.h"


typedef struct _info_status_description
{
	int			nStatus;
	char		pAction[80];
	char		pOption[40];
} DescribeStatus;


class CPrintRepresent : public CPrint_TBos
{
public:
	CPrintRepresent(HWND hwnd, int nmode);
	virtual ~CPrintRepresent(void);

public:
	bool	Print_content(ONE_CASE* pcase, ONE_REPRESENT* prepresent);

};
