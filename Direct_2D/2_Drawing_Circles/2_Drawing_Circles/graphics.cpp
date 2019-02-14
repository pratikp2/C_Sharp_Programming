#include "graphics.h"

Graphics::Graphics()
{
	factory = NULL;
	rendertarget = NULL;
	brush = NULL;
}

Graphics :: ~Graphics()
{
	if (factory) factory->Release();
	if (rendertarget) rendertarget->Release();
	if(brush) brush->Release();
}

bool Graphics::Init(HWND windowHandle)
{
	HRESULT  res = D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED,&factory);
	if (res != S_OK) { return false;}		// HRESULT is status of the function Value S_OK for HRESULT means function have succeeded.

	RECT rect;
	GetClientRect(windowHandle, &rect); // To set the Size of the rect equal to the window size.

	res = factory->CreateHwndRenderTarget(D2D1 :: RenderTargetProperties(),
										  D2D1 :: HwndRenderTargetProperties(windowHandle, D2D1 :: SizeU(rect.right,rect.bottom)),
										  &rendertarget);
	if (res != S_OK) { return false; }

	res = rendertarget->CreateSolidColorBrush(D2D1::ColorF(0, 0, 0, 0), &brush);
	if (res != S_OK) { return false; }

	return true;
}

void Graphics :: ClearScreen(float r, float g, float b)
{
	rendertarget->Clear(D2D1 ::ColorF (r, g, b));
}

void Graphics :: DrawCircle(float x, float y, float radius, float r, float g, float b, float alpha)
{
	brush->SetColor(D2D1::ColorF(r, g, b, alpha));
	rendertarget->DrawEllipse(D2D1::Ellipse(D2D1::Point2F(x, y), radius, radius), brush, 1.0f);
}

void Graphics::DrawCaps()
{
	ID2D1StrokeStyle * m_pStrokeStyleCustomOffsetZero;

	factory->CreateStrokeStyle(
		D2D1::StrokeStyleProperties(
			D2D1_CAP_STYLE_FLAT,
			D2D1_CAP_STYLE_TRIANGLE,
			D2D1_CAP_STYLE_FLAT,
			D2D1_LINE_JOIN_MITER,
			10.0f,
			D2D1_DASH_STYLE_SOLID,
			0.0f),
		0,
		0,
		&m_pStrokeStyleCustomOffsetZero
	);

	rendertarget->DrawLine(
		D2D1::Point2F(350, 300),
		D2D1::Point2F(449, 300),
		brush,
		1.0f,
		m_pStrokeStyleCustomOffsetZero
	);

	rendertarget->DrawLine(
		D2D1::Point2F(449, 300),
		D2D1::Point2F(450, 300),
		brush,
		8.0f,
		m_pStrokeStyleCustomOffsetZero
	);

}
