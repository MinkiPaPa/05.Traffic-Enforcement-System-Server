#pragma once

#include "CPage.h"

class CIssuePrint
{
public:
	CIssuePrint(void);
	virtual ~CIssuePrint(void);

protected:
	CDC   *		m_pTheDC;
	CPage *		m_pThePage;

	RECT		m_rectDraw;

	double		m_baseX;
	double		m_baseY;

	CString		m_sDocName;

protected:
	void		ready_print_doc();

public:
	CDC   *		GetDC()   {  return m_pTheDC;    };
	CPage *		GetPage() {  return m_pThePage;  };

	bool		SafePrinter() 
			{	return (m_pTheDC->m_hDC != NULL);  };

	inline double C2I(double dcenti) 
	{
		return (dcenti / 2.54);
	}

	inline double C2I_X(double dcenti) 	{
			return C2I(dcenti) + m_baseX;
		}

	inline double C2I_Y(double dcenti) 	{
			return C2I(dcenti) + m_baseY;

	}
};


