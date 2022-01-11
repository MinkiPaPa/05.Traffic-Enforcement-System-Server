// PrnPaper.cpp: implementation of the CPrnPaper class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "PrnPaper.h"

CPrnPaper::CPrnPaper()
{
	m_X = m_Y = 0;
	m_pLines = NULL;
}

CPrnPaper::~CPrnPaper()
{
	init();
}

void CPrnPaper::init()
{
	for (int y=0; y<m_Y; y++) {
		delete [] m_pLines[y];
	}
	
	delete [] m_pLines;
}

void CPrnPaper::Clear()
{
	for (int y=0; y<m_Y; y++) {
		memset(m_pLines[y], ' ', m_X);
		m_pLines[y][m_X] = 0;
	}
}

bool CPrnPaper::SetPaper(int nx, int ny)
{
	init();

	m_X = nx; 
	m_Y = ny;

	m_pLines = new char*[m_Y];

	for (int y=0; y<m_Y; y++) {
		m_pLines[y] = new char[m_X+1];
		memset(m_pLines[y], ' ', m_X);
		m_pLines[y][m_X] = 0;
	}

	return true;
}

void CPrnPaper::Dump()
{
	for (int y=0; y<m_Y; y++) {
		ATLTRACE("%s\n", m_pLines[y]);
	}
}

bool CPrnPaper::Attach(int nX1, int nY1, LPCSTR pline, int nX2, int nY2)
{
	if (nX2 == 0)
		nX2 = m_X;

	if (nY2 == 0)
		nY2 = m_Y;

	// 
	int nXmax = max(nX1, nX2);
	int nYmax = max(nY1, nY2);
	int nx=0, ny=0;
	
	int nlength = strlen(pline);

	for (int n=0;n<nlength; n++, nx++) {
		int nXcurr = (nX1+nx);
		if (nXcurr >= nXmax || pline[n] == '\n')
		{
			nx = 0;
			nXcurr = nX1;
			ny++;

			if (pline[n] == '\n') {
				nx--;
				continue;
			}
		}
		int nYcurr = (nY1+ny);
		if (nYcurr > nYmax)
			break;

		// 
		m_pLines[nYcurr][nXcurr] = pline[n];
	}

	return true;
}

LPCSTR	CPrnPaper::GetLine(int nth)
{
	m_sline = m_pLines[nth];

	m_sline.TrimRight();
	
	return (LPCSTR) m_sline;
}

