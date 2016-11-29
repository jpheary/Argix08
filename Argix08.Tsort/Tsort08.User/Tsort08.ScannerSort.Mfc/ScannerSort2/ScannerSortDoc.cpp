// ScannerSortDoc.cpp : implementation of the CScannerSortDoc class
//

#include "stdafx.h"
#include "ScannerSort2.h"

#include "ScannerSortDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CScannerSortDoc

IMPLEMENT_DYNCREATE(CScannerSortDoc, CDocument)

BEGIN_MESSAGE_MAP(CScannerSortDoc, CDocument)
END_MESSAGE_MAP()

// CScannerSortDoc construction/destruction

CScannerSortDoc::CScannerSortDoc()
{
	// TODO: add one-time construction code here

}

CScannerSortDoc::~CScannerSortDoc()
{
}

BOOL CScannerSortDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}

// CScannerSortDoc serialization


void CScannerSortDoc::Serialize(CArchive& ar)
{
	(ar);
}



// CScannerSortDoc diagnostics

#ifdef _DEBUG
void CScannerSortDoc::AssertValid() const
{
	CDocument::AssertValid();
}
#endif //_DEBUG


// CScannerSortDoc commands

