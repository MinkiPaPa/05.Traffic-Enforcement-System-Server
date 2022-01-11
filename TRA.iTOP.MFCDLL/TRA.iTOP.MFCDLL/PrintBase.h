#pragma once

#define		PRINT_DIRECT		0
#define		PRINT_INDIRECT		1

class CPrintBase
{
public:
	CPrintBase(HWND hwnd, int nmode);
	virtual ~CPrintBase();

protected:
	int			m_nMode;
	HDC			m_hPrtdc;
	HWND		m_hOwnerWnd;

protected:
	bool			print_init_direct();
	bool			print_init_indirect();

public:
	bool			Print_init();
	bool			Print_fini();
	//virtual bool	print_content() = 0;

};
