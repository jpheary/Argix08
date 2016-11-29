// ScannerSort.cpp : Defines the entry point for the application.
//
#include "stdafx.h"
#include <commctrl.h>
#include <windows.h>
#include "ScreenLib.h"
#include "ScannerSort.h"
#include "ITC50.h"

#define MAX_LOADSTRING 100

//Forward declarations of functions included in this code module:
BOOL InitInstance(HINSTANCE, int);
ATOM MyRegisterClass(HINSTANCE, LPTSTR);
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT	CALLBACK SortDlgProc(HWND, UINT, WPARAM, LPARAM);
BOOL OnCreateDialog(HWND);

//Global Variables
HINSTANCE g_hinsApp;
HWND g_hwndMain = NULL;
HWND g_hwndDialog = NULL;

#ifdef WIN32_PLATFORM_PSPC
    static SHACTIVATEINFO s_sInfo;
#endif  /* WIN32_PLATFORM_PSPC */

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow) {
    //Application entry point; create our main application window
	if(!InitInstance(hInstance, nCmdShow)) return FALSE;
    
	HACCEL hAccelTable;
	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_SCANNERSORT));
    
	// Main message loop:
	MSG msg;
	while(GetMessage(&msg, NULL, 0, 0)) {
		if(!TranslateAccelerator(msg.hwnd, hAccelTable, &msg)) {
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
	return (int) msg.wParam;
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow) {
    //Createthe main window
    g_hinsApp = hInstance;

    // SHInitExtraControls should be called once during your application's initialization to initialize any of the device specific controls such as CAPEDIT and SIPPREF.
    SHInitExtraControls();
    TCHAR szTitle[MAX_LOADSTRING], szWindowClass[MAX_LOADSTRING];
    LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING); 
    LoadString(hInstance, IDC_SCANNERSORT, szWindowClass, MAX_LOADSTRING);

    //If it is already running, then focus on the window, and exit
    g_hwndMain = FindWindow(szWindowClass, szTitle);	
    if (g_hwndMain) {
        // Set focus to foremost child window; the "| 0x00000001" is used to bring any owned windows to the foreground and activate them.
        SetForegroundWindow((HWND)((ULONG) g_hwndMain | 0x00000001));
        return 0;
    } 

    if (!MyRegisterClass(hInstance, szWindowClass)) return FALSE;
    g_hwndMain = CreateWindow(szWindowClass, szTitle, WS_VISIBLE, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, NULL, NULL, hInstance, NULL);
    ShowWindow(g_hwndMain, nCmdShow);
    UpdateWindow(g_hwndMain);
    return TRUE;
}

ATOM MyRegisterClass(HINSTANCE hInstance, LPTSTR szWindowClass) {
    //Registers the window class.
	WNDCLASS wc;
	wc.style         = CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc   = WndProc;
	wc.cbClsExtra    = 0;
	wc.cbWndExtra    = 0;
	wc.hInstance     = hInstance;
	wc.hIcon         = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_SCANNERSORT));
	wc.hCursor       = 0;
	wc.hbrBackground = (HBRUSH) GetStockObject(WHITE_BRUSH);
	wc.lpszMenuName  = 0;
	wc.lpszClassName = szWindowClass;
	return RegisterClass(&wc);
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
    // Processes messages for the main window.
    int wmId, wmEvent;
    PAINTSTRUCT ps;
    HDC hdc;
    static SHACTIVATEINFO s_sInfo;
	
    switch (message) {
        case WM_CREATE:
            // Initialize the shell activate info structure
            memset(&s_sInfo, 0, sizeof (s_sInfo));
            s_sInfo.cbSize = sizeof (s_sInfo);

            SHMENUBARINFO mbi;
            memset(&mbi, 0, sizeof(SHMENUBARINFO));
            mbi.cbSize = sizeof(SHMENUBARINFO);
            mbi.hwndParent = hWnd;
 	        mbi.dwFlags = SHCMBF_HMENU;
            mbi.nToolBarId = IDR_MENU;
            mbi.hInstRes = g_hinsApp;
            SHCreateMenuBar(&mbi);
 			
 			DialogBox(g_hinsApp, MAKEINTRESOURCE(IDD_SORT), g_hwndMain, (DLGPROC)SortDlgProc);
            break;
        case WM_COMMAND:
            wmId    = LOWORD(wParam); 
            wmEvent = HIWORD(wParam); 
            // Parse the menu selections:
            switch (wmId) {
                case IDM_REFRESH:
                    break;
                default:
                    return DefWindowProc(hWnd, message, wParam, lParam);
            }
            break;
        case WM_PAINT:
            hdc = BeginPaint(hWnd, &ps);
            // TODO: Add any drawing code here...
            EndPaint(hWnd, &ps);
            break;
         case WM_DESTROY:
            PostQuitMessage(0);
            break;
        case WM_ACTIVATE:
            // Notify shell of our activate message
            SHHandleWMActivate(hWnd, wParam, lParam, &s_sInfo, FALSE);
            break;
        case WM_SETTINGCHANGE:
            SHHandleWMSettingChange(hWnd, wParam, lParam, &s_sInfo);
            break;
        default:
            return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}
LRESULT	CALLBACK SortDlgProc(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp) {
	switch (msg) {
		case WM_INITDIALOG:
            SHMENUBARINFO mbi;
            memset(&mbi, 0, sizeof(SHMENUBARINFO));
            mbi.cbSize = sizeof(SHMENUBARINFO);
            mbi.hwndParent = hwnd;
	        mbi.dwFlags = SHCMBF_HMENU;
            mbi.nToolBarId = IDR_MENU;
            mbi.hInstRes = g_hinsApp;
            SHCreateMenuBar(&mbi); 

			SetDlgItemText(hwnd, IDC_TXTTDS, _T("1234567890"));
			SetDlgItemText(hwnd, IDC_TXTTRAILER, _T("12345678"));
			
			SetDlgItemText(hwnd, IDC_TXTSCAN, _T(""));
			//int deviceType;
			//deviceType = ITCGetDeviceType();
			//if(deviceType != ITC_DEVICE_CN50) return NULL;
			TCHAR sn[50];
			if(ITCGetSerialNumber(sn, sizeof(sn)) == S_OK) SetDlgItemText(hwnd, IDC_TXTSCAN, sn);

	        SHINITDLGINFO shidi;
	        shidi.dwMask = SHIDIM_FLAGS;
	        shidi.hDlg = hwnd;
#ifdef WIN32_PLATFORM_WFSP
	        shidi.dwFlags = SHIDIF_SIZEDLGFULLSCREEN;
#else
	        shidi.dwFlags = SHIDIF_SIZEDLGFULLSCREEN|SHIDIF_DONEBUTTON|SHIDIF_SIPDOWN;
#endif  /* WIN32_PLATFORM_WFSP */
	        return SHInitDialog(&shidi);
		case WM_COMMAND:
			if (LOWORD(wp) == IDOK) {	
				EndDialog(hwnd, IDOK);
				return (INT_PTR)TRUE;
			}
			break;
		case WM_SIZE:
			// Use ScreenLib to maximize the use of available screen space.
			//CScreenLib::MakeSameSize(hwnd, CScreenLib::SCREENLIB_SIZE_HEIGHT, 1, IDC_LIST1, IDC_LIST2);
			//CScreenLib::DockControl(hwnd, IDC_LIST1, CScreenLib::SCREENLIB_DOCK_TOP);
			//CScreenLib::DockControl(hwnd, IDC_LIST2, CScreenLib::SCREENLIB_DOCK_BOTTOM);
			break;
#ifdef WIN32_PLATFORM_PSPC
		// Our dialog receives this when the Input Panel opens or closes
		case WM_SETTINGCHANGE:
            SHHandleWMSettingChange(hwnd, wp, lp, &s_sInfo);
            break;
#endif  /* WIN32_PLATFORM_PSPC */		
	}
	return DefWindowProc(hwnd, msg, wp, lp);
}
