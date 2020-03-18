#include <Windows.h>


LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp)
{
	switch (msg)
	{
	case  WM_DESTROY:
		PostQuitMessage(0);
		break;

	default:
		return DefWindowProcW(hwnd,msg,wp,lp);
	}
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR args, int nCmdShow)
{
	WNDCLASSW wc = { 0 };

	wc.hbrBackground = (HBRUSH)COLOR_WINDOW;
	wc.hCursor = LoadCursor(NULL,IDC_ARROW);
	wc.hInstance = hInstance;
	wc.lpszClassName = L"My Window Class";
	wc.lpfnWndProc = WindowProcedure;

	if (!RegisterClassW(&wc))
		return -1;

	CreateWindowW(L"My Window Class",L"Practice Proj", WS_OVERLAPPEDWINDOW | WS_VISIBLE,1200, 175, 500, 500, NULL, NULL, NULL, NULL);

	MSG msg = { 0 };

	while (GetMessage(&msg, NULL, NULL, NULL))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}