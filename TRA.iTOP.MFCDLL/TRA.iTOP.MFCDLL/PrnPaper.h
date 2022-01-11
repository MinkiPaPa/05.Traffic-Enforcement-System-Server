#pragma once

class CPrnPaper  
{
public:
	CPrnPaper();
	virtual ~CPrnPaper();

protected:
	int			m_X;
	int			m_Y;

	char **		m_pLines;
	CString		m_sline;

protected:
	void		init();

public:
	bool		SetPaper(int nx, int ny);
	void		Clear();

	void		Dump();
	bool		Attach(int nX1, int nY1, LPCSTR pline, int nX2=0, int nY2=0); 
	LPCSTR		GetLine(int nth);
};
