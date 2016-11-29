// ScannerSort2.h : main header file for the ScannerSort2 application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resourceppc.h"

// CScannerSortApp:
// See ScannerSort2.cpp for the implementation of this class
//

class CScannerSortApp : public CWinApp
{
public:
	CScannerSortApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation
public:
	afx_msg void OnAppAbout();

	DECLARE_MESSAGE_MAP()
};

extern CScannerSortApp theApp;
