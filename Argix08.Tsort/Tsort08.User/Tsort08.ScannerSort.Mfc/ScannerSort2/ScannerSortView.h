// ScannerSortView.h : interface of the CScannerSortView class
//


#pragma once

class CScannerSortView : public CView
{
protected: // create from serialization only
	CScannerSortView();
	DECLARE_DYNCREATE(CScannerSortView)

// Attributes
public:
	CScannerSortDoc* GetDocument() const;

// Operations
public:

// Overrides
public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

protected:

// Implementation
public:
	virtual ~CScannerSortView();
#ifdef _DEBUG
	virtual void AssertValid() const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in ScannerSortView.cpp
inline CScannerSortDoc* CScannerSortView::GetDocument() const
   { return reinterpret_cast<CScannerSortDoc*>(m_pDocument); }
#endif

