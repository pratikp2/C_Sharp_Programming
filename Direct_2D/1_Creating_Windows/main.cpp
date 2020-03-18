#include <Windows.h>

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	if (uMsg == WM_DESTROY)		// Clicking red Button to close the window
	{
		PostQuitMessage(0);
		return 0;
	}
	
	return DefWindowProc(hwnd, uMsg, wParam, lParam);	// Default window behaviour if uMsg is unknown.

}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE prevInstance, LPWSTR cmd, int nCmdShow)
{
	WNDCLASSEX windowclass;
	ZeroMemory(&windowclass, sizeof(WNDCLASSEX));

	windowclass.cbSize = sizeof(WNDCLASSEX);
	windowclass.hbrBackground = (HBRUSH)COLOR_WINDOW;
	windowclass.hInstance = hInstance;
	windowclass.lpfnWndProc = WindowProc;
	windowclass.lpszClassName = "MainWindow";
	windowclass.style = CS_HREDRAW | CS_VREDRAW;

	RegisterClassEx(&windowclass);		// Similar to #include

	RECT rect = {0, 0, 800, 600};
	AdjustWindowRectEx(&rect, WS_OVERLAPPEDWINDOW, false, WS_EX_OVERLAPPEDWINDOW);

	HWND windowHandle = CreateWindowEx(WS_EX_OVERLAPPEDWINDOW, "MainWindow", "Creating Window Assignment", WS_OVERLAPPEDWINDOW, 100, 100,
		rect.right - rect.left, rect.bottom - rect.top, NULL, NULL, hInstance, 0);

	if (windowHandle == NULL){return -1;}

	ShowWindow(windowHandle, nCmdShow);

	MSG	message;
	while (GetMessage(&message, NULL, 0, 0)) { DispatchMessage(&message); }

	return 0;
}