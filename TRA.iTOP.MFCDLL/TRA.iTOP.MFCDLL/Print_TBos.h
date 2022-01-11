#pragma once

#include <atltypes.h>
#include <atlimage.h>
#include "PrintBase.h"
//#include "data_Case.h"
#include "TRA.iTOP.MFCDLL.h"

#define		LINE_OFFSET			100

typedef printCase  ONE_CASE;
//typedef printReceipt REC_CASE;

class CPrint_TBos : public CPrintBase
{
public:
	CPrint_TBos(HWND hwnd, int nmode);
	virtual ~CPrint_TBos(void);

protected:
	int			m_nXmax;
	int			m_nYmax;

protected:
	void	init_resolution();
	void	print_image_there(LPCSTR pfilename, CRect rectDraw);

public:
	virtual bool	Print_CaseInfo(ONE_CASE* pcase, LPCSTR ptitle);
};
