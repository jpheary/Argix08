// ScannerSortView.cpp : implementation of the CScannerSortView class
//

#include "stdafx.h"
#include "ScannerSort2.h"

#include "ScannerSortDoc.h"
#include "ScannerSortView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CScannerSortView

IMPLEMENT_DYNCREATE(CScannerSortView, CView)

BEGIN_MESSAGE_MAP(CScannerSortView, CView)
END_MESSAGE_MAP()

// CScannerSortView construction/destruction

CScannerSortView::CScannerSortView()
{
	// TODO: add construction code here

}

CScannerSortView::~CScannerSortView()
{
}

BOOL CScannerSortView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}


// CScannerSortView drawing
void CScannerSortView::OnDraw(CDC* /*pDC*/)
{
	CScannerSortDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	// TODO: add draw code for native data here
}



// CScannerSortView diagnostics

#ifdef _DEBUG
void CScannerSortView::AssertValid() const
{
	CView::AssertValid();
}

CScannerSortDoc* CScannerSortView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CScannerSortDoc)));
	return (CScannerSortDoc*)m_pDocument;
}
#endif //_DEBUG


// CScannerSortView message handlers
