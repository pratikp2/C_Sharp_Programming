#pragma once

#include <Windows.h>
#include <d2d1.h>

class Graphics 
{
	ID2D1Factory * factory;						// Helps to create different resources required for grphics programming.
	ID2D1HwndRenderTarget * rendertarget;		// Includes the resources which helps paint on screen 
	ID2D1SolidColorBrush * brush;

public :
	Graphics();
	~Graphics();

	bool Init(HWND windowHandle);
	void BeginDraw() { rendertarget->BeginDraw(); }
	void EndDraw() { rendertarget->EndDraw(); }
	void ClearScreen(float r, float g, float b);
	void DrawCircle(float x, float y, float radius, float r, float g, float b, float alpha);
	void DrawCaps();
};
