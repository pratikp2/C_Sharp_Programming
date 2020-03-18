#include <Windows.h>


// Functions
void AddMenu(HWND);
void Handle_WM_COMMAND(WPARAM);
void CreateBox(LPCSTR);
LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp);


// Globals
HMENU hMenu;		// Handle for Menu


int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR args, int nCmdShow)
{
	WNDCLASSW wc = { 0 };

	wc.hbrBackground = (HBRUSH)COLOR_WINDOW;
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hInstance = hInstance;
	wc.lpszClassName = L"My Window Class";
	wc.lpfnWndProc = WindowProcedure;

	if (!RegisterClassW(&wc))
		return -1;

	CreateWindowW(L"My Window Class", L"Practice Proj", WS_OVERLAPPEDWINDOW | WS_VISIBLE, 1200, 175, 500, 500, NULL, NULL, NULL, NULL);

	MSG msg = { 0 };

	while (GetMessage(&msg, NULL, NULL, NULL))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}

LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp)
{
	switch (msg)
	{
	case WM_COMMAND:	// will be passed to window proc for every commnad givent by IP devices. 
		Handle_WM_COMMAND(wp);
		break;

	case WM_CREATE :
		AddMenu(hwnd);
		break;

	case  WM_DESTROY:
		PostQuitMessage(0);
		break;

	default:
		return DefWindowProcW(hwnd, msg, wp, lp);
	}
}

void AddMenu(HWND hwnd)
{
	hMenu = CreateMenu();
	HMENU popoutFile = CreateMenu();
	HMENU popoutView = CreateMenu();
	HMENU popoutWindow = CreateMenu();
	HMENU popoutHelp = CreateMenu();
	HMENU subMenu = CreateMenu();

	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutFile, "File");				// Layer One 
	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutView, "View");
	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutWindow, "Window");
	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutHelp, "Help");

	AppendMenu(popoutFile, MF_STRING, 11, "New File");
	AppendMenu(popoutFile, MF_POPUP, (UINT_PTR)subMenu, "Save File");		// Layer Two
	AppendMenu(popoutFile, MF_SEPARATOR, NULL, NULL);
	AppendMenu(popoutFile, MF_STRING, 13, "Exit");

	AppendMenu(popoutView, MF_STRING, 21, "Vertical Partition");
	AppendMenu(popoutView, MF_STRING, 22, "HorixontL Partition");
	AppendMenu(popoutView, MF_STRING, 23, "Popout Window");
	AppendMenu(popoutView, MF_STRING, 24, "Restore To Dafault");

	AppendMenu(popoutWindow, MF_STRING, 31, "New Window");
	AppendMenu(popoutWindow, MF_STRING, 32, "Manage Layout");
	AppendMenu(popoutWindow, MF_STRING, 33, "Save Layout");
	AppendMenu(popoutWindow, MF_STRING, 34, "Properties");

	AppendMenu(popoutHelp, MF_STRING, 41, "Read Documentation");
	AppendMenu(popoutHelp, MF_STRING, 42, "Go online");

	AppendMenu(subMenu, MF_STRING, 121, "Save");							// Layer Three
	AppendMenu(subMenu, MF_STRING, 122, "save As");

	SetMenu(hwnd, hMenu);
}

void Handle_WM_COMMAND(WPARAM wp)
{
	MessageBeep(MB_OK); // To create Sound 

	LPCSTR item = "NULL";
	switch (wp)
	{
	//case 1 : item = "File"; break;
	//case 2 : item = "View"; break;
	//case 3 : item = "Window"; break;
	//case 4 : item = "Help"; break;
	default: break;
	}

	//CreateBox(item);
}

void CreateBox(LPCSTR text)
{
	LPCSTR caption = "Pressed Item";
	MessageBox(NULL, text, caption, MB_OK);
}

