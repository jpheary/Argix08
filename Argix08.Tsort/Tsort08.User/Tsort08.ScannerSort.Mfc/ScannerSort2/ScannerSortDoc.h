// ScannerSortDoc.h : interface of the CScannerSortDoc class
//


#pragma once

class CScannerSortDoc : public CDocument
{
protected: // create from serialization only
	CScannerSortDoc();
	DECLARE_DYNCREATE(CScannerSortDoc)

// Attributes
public:

// Operations
public:

// Overrides
public:
	virtual BOOL OnNewDocument();

	virtual void Serialize(CArchive& ar);


// Implementation
public:
	virtual ~CScannerSortDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
};


