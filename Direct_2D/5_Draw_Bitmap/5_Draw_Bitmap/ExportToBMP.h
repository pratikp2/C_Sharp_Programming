#pragma once
# include "sources.h"
# include "patterns.h"

class ExportToBMP
{
private:

	static const UINT sc_bitmapWidth;
	static const UINT sc_bitmapHeight;
	static ID2D1RenderTarget * pRT;
	static ID2D1SolidColorBrush * pGridBrush;
	static ID2D1BitmapRenderTarget * pCompatibleRenderTarget;

	ID2D1Factory * pD2DFactory;
	IWICImagingFactory * pWICFactory;
	IWICBitmap * pWICBitmap;
	IWICStream * pStream;
	IWICBitmapEncoder * pEncoder;
	IWICBitmapFrameEncode * pFrameEncode;

	IDWriteFactory * pDWriteFactory;
	IDWriteTextFormat * pTextFormat;
	ID2D1PathGeometry * pPathGeometry;
	ID2D1GeometrySink * pSink;
	ID2D1GradientStopCollection * pGradientStops;
	ID2D1LinearGradientBrush * pLGBrush;

	ID2D1Bitmap * pGridBitmap;
	ID2D1BitmapBrush * pBitmapBrush;

public:
	ExportToBMP();
	~ExportToBMP();
	void DrawBitmap();

	static ID2D1RenderTarget * GetRenderTarget() { return pRT; }
	static ID2D1SolidColorBrush * GetSolidColorBrush() { return pGridBrush; }
	static ID2D1BitmapRenderTarget * GetBitmapRenderTarget() { return pCompatibleRenderTarget; }
};