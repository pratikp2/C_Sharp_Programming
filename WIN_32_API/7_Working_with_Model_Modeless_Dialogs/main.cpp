#include <Windows.h>

#define CLOSE_DLG_BOX_1 1 
#define CLOSE_DLG_BOX_2 2
#define FILE_MENU_EXIT 13
#define NEW_DIALOG_BOX_1 112
#define NEW_DIALOG_BOX_2 113
#define SUBMIT_INFORMATION 4

// Functions
void AddMenu(HWND);
void AddControls(HWND);
void LoadImages(HWND);
void RegisterDialogClass(HINSTANCE);

// Hepler Functions
void DisplayDialog1(HWND);
void DisplayDialog2(HWND);
void Handle_Window_WM_COMMAND(WPARAM, HWND);
void Handle_Dialog_WM_COMMAND(WPARAM, HWND);
LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp);
LRESULT CALLBACK DialogProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp);

// Globals
HMENU hMenu;										// Handle for Menu
HWND hParentWnd, hEditName, hEditAge, hEditOutput, hFrame;		// Handle for Text Window
HBITMAP hFrameImage, hFolderImager;					// Handle for Image

////////////////////////////////////////////////////// Main Function /////////////////////////////////////////////////////////////////

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

	RegisterDialogClass(hInstance);
		
	hParentWnd = CreateWindowW(L"My Window Class", L"Practice Proj", WS_OVERLAPPEDWINDOW | WS_VISIBLE, 975, 175, 500, 500, NULL, NULL, NULL, NULL);

	MSG msg = { 0 };

	while (GetMessage(&msg, NULL, NULL, NULL))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}

////////////////////////////////////////////////////// Creating Menus /////////////////////////////////////////////////////////////////

void AddMenu(HWND hwnd)
{
	hMenu = CreateMenu();
	HMENU popoutFile = CreateMenu();
	HMENU popoutView = CreateMenu();
	HMENU popoutWindow = CreateMenu();
	HMENU popoutHelp = CreateMenu();
	HMENU subMenuFile = CreateMenu();
	HMENU subMenuDialog = CreateMenu();


	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutFile, "File");				// Layer One 
	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutView, "View");
	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutWindow, "Window");
	AppendMenu(hMenu, MF_POPUP, (UINT_PTR)popoutHelp, "Help");

	AppendMenu(popoutFile, MF_POPUP, (UINT_PTR)subMenuDialog, "New File");
	AppendMenu(popoutFile, MF_POPUP, (UINT_PTR)subMenuFile, "Save File");		// Layer Two
	AppendMenu(popoutFile, MF_SEPARATOR, NULL, NULL);
	AppendMenu(popoutFile, MF_STRING, FILE_MENU_EXIT, "Exit");

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

	AppendMenu(subMenuFile, MF_STRING, 121, "Save");							// Layer Three
	AppendMenu(subMenuFile, MF_STRING, 122, "save As");

	AppendMenu(subMenuDialog, MF_STRING, 111, "File");			
	AppendMenu(subMenuDialog, MF_STRING, NEW_DIALOG_BOX_1, "Dialog Box (Modeless)");
	AppendMenu(subMenuDialog, MF_STRING, NEW_DIALOG_BOX_2, "Dialog Box (Mode)");


	SetMenu(hwnd, hMenu);
}

///////////////////////////////////////////////////// Creating Edit control, Static Control and Buttons //////////////////////////////

void AddControls(HWND hwnd)
{
	CreateWindowW(L"Static", L" Please Enter User Details ", WS_VISIBLE | WS_CHILD | SS_CENTER, 100, 50, 300, 25, hwnd, NULL, NULL, NULL);
	CreateWindowW(L"Static", L" Candidate's Name :", WS_VISIBLE | WS_CHILD | WS_BORDER | SS_CENTER, 31, 100, 140, 25, hwnd, NULL, NULL, NULL);
	CreateWindowW(L"Static", L" Candidate's Age :", WS_VISIBLE | WS_CHILD | WS_BORDER | SS_CENTER, 31, 150, 140, 25, hwnd, NULL, NULL, NULL);

	hEditName = CreateWindowW(L"Edit", L" ", WS_VISIBLE | WS_CHILD | WS_BORDER | SS_LEFT | ES_MULTILINE | ES_AUTOVSCROLL, 225, 100, 200, 25, hwnd, NULL, NULL, NULL);
	hEditAge = CreateWindowW(L"Edit", L" ", WS_VISIBLE | WS_CHILD | WS_BORDER | SS_LEFT | ES_MULTILINE | ES_AUTOVSCROLL, 225, 150, 50, 25, hwnd, NULL, NULL, NULL);
	hEditOutput = CreateWindowW(L"Edit", L"  ....", WS_VISIBLE | WS_CHILD | WS_BORDER | SS_LEFT | ES_MULTILINE | ES_AUTOVSCROLL, 60, 300, 375, 100, hwnd, NULL, NULL, NULL);

	CreateWindowW(L"Button", L" SUBMIT INFORMATION ", WS_VISIBLE | WS_CHILD | SS_CENTER, 130, 225, 240, 25, hwnd, (HMENU)SUBMIT_INFORMATION, NULL, NULL);

	hFrame = CreateWindowW(L"Static", NULL, WS_VISIBLE | WS_CHILD | SS_BITMAP, 350, 60, 100, 100, hwnd, NULL, NULL, NULL);
	SendMessageW(hFrame, STM_SETIMAGE, IMAGE_BITMAP, (LPARAM)hFrameImage);

	// Text "Static", "Edit" and "Button" passed in CreateWindowW function creates respective classes for edit static and button field.
}

///////////////////////////////////////////////////// Loading Images //////////////////////////////////////////////////////////////////

void LoadImages(HWND hwnd)
{
	hFrameImage = (HBITMAP)LoadImageW(NULL, L"D:\\My_GitHub_Repos\\Windows_Programming\\WIN_32_API\\5_Working_with_Images(Bitmap)\\5_Working_with_Images(Bitmap)\\Frame.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
}

///////////////////////////////////////////////////// Model and Modeless Dialogs //////////////////////////////////////////////////////

void RegisterDialogClass(HINSTANCE hInstance)
{
	WNDCLASSW dialog1 = { 0 }, dialog2 = { 0 };

	dialog1.hbrBackground = (HBRUSH)COLOR_WINDOW;
	dialog1.hCursor = LoadCursor(NULL, IDC_CROSS);
	dialog1.hInstance = hInstance;
	dialog1.lpszClassName = L"Dialog Class 1";
	dialog1.lpfnWndProc = DialogProcedure;
	RegisterClassW(&dialog1);

	dialog2.hbrBackground = (HBRUSH)COLOR_WINDOW;
	dialog2.hCursor = LoadCursor(NULL, IDC_CROSS);
	dialog2.hInstance = hInstance;
	dialog2.lpszClassName = L"Dialog Class 2";
	dialog2.lpfnWndProc = DialogProcedure;
	RegisterClassW(&dialog2);
}

void DisplayDialog1(HWND hwnd)
{
	HWND hDlg = CreateWindowW(L"Dialog Class 1", L"Dialog", WS_VISIBLE | WS_OVERLAPPEDWINDOW, 1525, 375, 300, 200, hwnd, NULL, NULL, NULL);
	CreateWindowW(L"Static", L"Mode Less Diaglog Box", WS_VISIBLE | WS_CHILD | SS_CENTER, 0, 20, 300, 25, hDlg, NULL, NULL, NULL);
	CreateWindowW(L"Button", L"Close", WS_VISIBLE | WS_CHILD | SS_CENTER, 125, 60, 50, 25, hDlg, (HMENU)CLOSE_DLG_BOX_1, NULL, NULL);
}

void DisplayDialog2(HWND hwnd)
{
	EnableWindow(hwnd, false);
	HWND hDlg = CreateWindowW(L"Dialog Class 2", L"Dialog", WS_VISIBLE | WS_OVERLAPPEDWINDOW, 1525, 375, 300, 200, hwnd, NULL, NULL, NULL);
	CreateWindowW(L"Static", L"Mode Diaglog Box", WS_VISIBLE | WS_CHILD | SS_CENTER, 0, 20, 300, 25, hDlg, NULL, NULL, NULL);
	CreateWindowW(L"Button", L"Close", WS_VISIBLE | WS_CHILD | SS_CENTER, 125, 60, 50, 25, hDlg, (HMENU)CLOSE_DLG_BOX_2, NULL, NULL);
	//EnableWindow(hwnd, true);
}

///////////////////////////////////////////////////// Helper Functions ///////////////////////////////////////////////////////////////

LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp)
{
	switch (msg)
	{
	case WM_COMMAND:	// will be passed to window proc for every commnad givent by IP devices. 
		Handle_Window_WM_COMMAND(wp, hwnd);
		break;

	case WM_CREATE:
		LoadImages(hwnd);
		AddMenu(hwnd);
		AddControls(hwnd);
		break;

	case  WM_DESTROY:
		PostQuitMessage(0);
		break;

	default:
		return DefWindowProcW(hwnd, msg, wp, lp);
	}
}

LRESULT CALLBACK DialogProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp) 
{
	switch (msg)
	{
	case WM_CLOSE:
		EnableWindow(hParentWnd, true);
		DestroyWindow(hwnd);
		break;

	case WM_COMMAND:
		Handle_Dialog_WM_COMMAND(wp, hwnd);
		break;

	default:
		return DefWindowProcW(hwnd,msg,wp,lp);
		break;
	}
}

void Handle_Window_WM_COMMAND(WPARAM wp, HWND hwnd)
{
	MessageBeep(MB_OK); // To create Sound 

	LPCSTR item = "NULL";
	int retVal = 0;

	switch (wp)
	{
	case FILE_MENU_EXIT:
		retVal = MessageBoxW(hwnd, L"Warning", L"Are You Sure you want to exit ?", MB_YESNO | MB_ICONQUESTION);
		if (retVal == IDYES) { DestroyWindow(hwnd); }
		break;

	case NEW_DIALOG_BOX_1:
		DisplayDialog1(hwnd);
		break;

	case NEW_DIALOG_BOX_2:
		DisplayDialog2(hwnd);
		break;

	case SUBMIT_INFORMATION:

		char TextName[40];
		char TextAge[5];
		char TextOutput[100];

		GetWindowText(hEditName, TextName, 100);
		GetWindowText(hEditAge, TextAge, 5);

		if (_strcmpi(TextName, "") == 0 || _strcmpi(TextAge, "") == 0)
		{
			retVal = MessageBoxW(hwnd, L"Please Enter Correct Information .... !", L"Error", MB_ABORTRETRYIGNORE | MB_ICONERROR);
			if (retVal == IDABORT) { DestroyWindow(hwnd); }
			else if (retVal == IDRETRY) { break; }
			else
			{
				strcpy_s(TextOutput, TextName);
				strcat_s(TextOutput, " is ");
				strcat_s(TextOutput, TextAge);
				strcat_s(TextOutput, " years Old. ");
			}
		}
		else
		{
			strcpy_s(TextOutput, TextName);
			strcat_s(TextOutput, " is ");
			strcat_s(TextOutput, TextAge);
			strcat_s(TextOutput, " years Old. ");
		}


		SetWindowText(hEditOutput, TextOutput);
		break;

	default:
		break;
	}
}

void Handle_Dialog_WM_COMMAND(WPARAM wp, HWND hwnd)
{
	switch (wp)
	{
	case CLOSE_DLG_BOX_1:
		DestroyWindow(hwnd);
		break;

	case CLOSE_DLG_BOX_2:
		EnableWindow(hParentWnd, true);
		DestroyWindow(hwnd);
		break;

	default:
		break;
	}
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Types of message boses
// MB_ABORTRETRYIGNORE : 3 - 1.ABORT 2.RETRY 3.IGNORE 
// MB_CANCELTRYCONTINEW : 3 - 1.CANCEL 2.TRY 3.CONTINUE
// MB_HELP : 1 - 1.HELP
// MB_OK : 1 - 1.OK
// MB_OKCANCEL : 2 - 1.OK 2.CANCEL
// MB_RETRYCANCEL : 2 - 1.RETRY 2.CANCEL
// MB_YESNOCANCEL : 3 - 1.YES 2.NO 3.CANCEL

