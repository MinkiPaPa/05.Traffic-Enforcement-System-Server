#pragma once

#include "Print_TBos.h"
//#include "TBosCommonLook.h"

typedef struct _info_list_row
{
	char		pCol[20];
	char*		pValue;
} InfoList_Row;


class CPrintCaseDetail : public CPrint_TBos
{
public:
	CPrintCaseDetail(HWND hwnd, int nmode);
	virtual ~CPrintCaseDetail(void);

public:
	bool	Print_content(ONE_CASE* pcase, 
				InfoList_Row* p1, int nsz1, 
				InfoList_Row* p2, int nsz2, 
				InfoList_Row* p3, int nsz3,
				InfoList_Row* p4, int nsz4);
	
};
