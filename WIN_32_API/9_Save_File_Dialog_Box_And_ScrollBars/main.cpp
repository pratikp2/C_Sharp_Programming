#include <Windows.h>
#include <iostream>

#define OPEN_FILE 1
#define SAVE_FILE 2
#define BOX_CHECKED 3

// Functions
void AddControls(HWND);
void OpenFile(HWND);
void SaveFile(HWND);
void DisplayFile(char*);
void WriteFile(char*);

// Hepler Functions
void Handle_Window_WM_COMMAND(WPARAM, HWND);
LRESULT CALLBACK WindowProcedure(HWND, UINT, WPARAM, LPARAM);


// Globals
HWND hMainWindow, hEdit;		// Handle for Text Window

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

	hMainWindow = CreateWindowW(L"My Window Class", L"Practice Proj", WS_OVERLAPPEDWINDOW | WS_VISIBLE, 975, 175, 500, 500, NULL, NULL, NULL, NULL);

	MSG msg = { 0 };

	while (GetMessage(&msg, NULL, NULL, NULL))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}

///////////////////////////////////////////////////// Creating Edit control, Static Control and Buttons //////////////////////////////

void AddControls(HWND hwnd)
{
	CreateWindowW(L"Button", L" Open File ", WS_VISIBLE | WS_CHILD | SS_CENTER, 20, 20, 150, 30, hwnd, (HMENU)OPEN_FILE, NULL, NULL);
	CreateWindowW(L"Button", L" Save File ", WS_VISIBLE | WS_CHILD | SS_CENTER, 250, 20, 150, 30, hwnd, (HMENU)SAVE_FILE, NULL, NULL);

	CreateWindow("BUTTON", "CheckBox", BS_CHECKBOX | WS_VISIBLE | WS_CHILD, 425, 20, 25, 25, hwnd, (HMENU)BOX_CHECKED, NULL, NULL);

	hEdit = CreateWindowW(L"Edit", L"", WS_VISIBLE | WS_CHILD | WS_HSCROLL | WS_VSCROLL | WS_BORDER | ES_MULTILINE, 20, 80, 450, 350, hwnd, NULL, NULL, NULL);
}

///////////////////////////////////////////////////// Creating Open File Dialog /////////////////////////////////////////////////////

void OpenFile(HWND hwnd)
{
	OPENFILENAME ofn;
	ZeroMemory(&ofn, sizeof(OPENFILENAME));

	char fileName[100];

	ofn.lStructSize = sizeof(OPENFILENAME);
	ofn.hwndOwner = hwnd;
	ofn.lpstrFile = fileName;
	ofn.lpstrFile[0] = '\0';		// If Function GetOpenFileName return sccessfully then path of the file will be stored in this attribute
	ofn.nMaxFile = 100;
	ofn.lpstrFilter = "All Files\0*.*\0Source Files\0*.CPP\0Text Files\0*.TXT\0";
	ofn.nFilterIndex = 1;

	GetOpenFileName(&ofn);			//Need To add reference of libcomdlg32.a in linker (from MinGW)
	DisplayFile(ofn.lpstrFile);
}

void SaveFile(HWND hwnd)
{
	OPENFILENAME ofn;
	ZeroMemory(&ofn, sizeof(OPENFILENAME));

	char fileName[100];

	ofn.lStructSize = sizeof(OPENFILENAME);
	ofn.hwndOwner = hwnd;
	ofn.lpstrFile = fileName;
	ofn.lpstrFile[0] = '\0';		
	ofn.nMaxFile = 100;
	ofn.lpstrFilter = "All Files\0*.*\0Source Files\0*.CPP\0Text Files\0*.TXT\0";
	ofn.nFilterIndex = 1;

	GetSaveFileName(&ofn);			
	WriteFile(ofn.lpstrFile);
}

///////////////////////////////////////////////////// Helper Functions ///////////////////////////////////////////////////////////////

LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp)
{
	switch (msg)
	{
	case WM_CREATE:
		AddControls(hwnd);
		break;

	case WM_COMMAND:
		Handle_Window_WM_COMMAND(wp, hwnd);
		break;

	case  WM_DESTROY:
		PostQuitMessage(0);
		break;

	default:
		return DefWindowProcW(hwnd, msg, wp, lp);
	}
}

void Handle_Window_WM_COMMAND(WPARAM wp, HWND hwnd)
{
	switch (wp)
	{
	case OPEN_FILE:
		OpenFile(hwnd);
		break;

	case SAVE_FILE:
		SaveFile(hwnd);
		break;

	case BOX_CHECKED:
		CheckDlgButton(hwnd, 1, BST_CHECKED);
		break;
	default:
		break;
	}
}

void DisplayFile(char * path)
{
	FILE * file;						//	Assign a file pointer
	file = fopen(path, "rb");			//  Assign the path of the file to the file pointer and give read permission
	fseek(file, 0, SEEK_END);			//	Make file pointer point to the end of the file, to determine the size of the file
	int _size = ftell(file);			//	Store the size of the file
	rewind(file);						//	Reset the file pointer to the initial position of the file
	char * data = new char[_size + 1];	//	Add one more charecter for null termination.
	fread(data, _size, 1, file);		//	Read File into data, once .
	data[_size] = '\0';					//	Make last character of the data a null termination 

	SetWindowText(hEdit, data);
	fclose(file);

	delete[] data;
	data = NULL;
}

void WriteFile(char * path)
{
	FILE * file;						
	file = fopen(path, "w");	

	int _size = GetWindowTextLength(hEdit);
	char * data = new char[_size + 1];
	GetWindowText(hEdit, data, _size + 1);

	fwrite(data, _size + 1, 1, file);

	fclose(file);
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

