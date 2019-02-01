# include "ExportToBMP.h"

const UINT ExportToBMP::sc_bitmapWidth = 300;
const UINT ExportToBMP::sc_bitmapHeight = 300;
ID2D1RenderTarget * ExportToBMP::pRT = NULL;
ID2D1SolidColorBrush * ExportToBMP::pGridBrush = NULL;
ID2D1BitmapRenderTarget * ExportToBMP::pCompatibleRenderTarget = NULL;

ExportToBMP::ExportToBMP()
{
	pWICFactory = NULL;
	pRT = NULL;
	pWICBitmap = NULL;
	pStream = NULL;
	pEncoder = NULL;
	pFrameEncode = NULL;

	pDWriteFactory = NULL;
	pTextFormat = NULL;
	pD2DFactory = NULL;
	pPathGeometry = NULL;
	pSink = NULL;
	pGradientStops = NULL;
	pLGBrush = NULL;

	pGridBitmap = NULL;
	pBitmapBrush = NULL;
}

void ExportToBMP::DrawBitmap()
{
	HRESULT hr = S_OK;
	CoInitializeEx(NULL, COINIT_MULTITHREADED);
	hr = CoCreateInstance(CLSID_WICImagingFactory, NULL, CLSCTX_INPROC_SERVER, IID_IWICImagingFactory, reinterpret_cast<void **>(&pWICFactory));

	if (SUCCEEDED(hr)) { hr = pWICFactory->CreateBitmap(sc_bitmapWidth, sc_bitmapHeight, GUID_WICPixelFormat32bppBGR, WICBitmapCacheOnLoad, &pWICBitmap); }

	// Set the render target type to D2D1_RENDER_TARGET_TYPE_DEFAULT to use software rendering.
	if (SUCCEEDED(hr)) { hr = D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED, &pD2DFactory); }
	if (SUCCEEDED(hr)) { hr = pD2DFactory->CreateWicBitmapRenderTarget(pWICBitmap, D2D1::RenderTargetProperties(), &pRT); }

/*	// Create text format and a path geometry representing an hour glass. 
	if (SUCCEEDED(hr)) { hr = DWriteCreateFactory(DWRITE_FACTORY_TYPE_SHARED, __uuidof(IDWriteFactory), reinterpret_cast<IUnknown**>(&pDWriteFactory)); }
	if (SUCCEEDED(hr))
	{
		static const WCHAR sc_fontName[] = L"Calibri";
		static const FLOAT sc_fontSize = 50;
		hr = pDWriteFactory->CreateTextFormat(sc_fontName, NULL, DWRITE_FONT_WEIGHT_NORMAL, DWRITE_FONT_STYLE_NORMAL, DWRITE_FONT_STRETCH_NORMAL, sc_fontSize, L"", &pTextFormat);
	}
	if (SUCCEEDED(hr))
	{
		pTextFormat->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_CENTER);
		pTextFormat->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_CENTER);
		hr = pD2DFactory->CreatePathGeometry(&pPathGeometry);
	}

	if (SUCCEEDED(hr)) { hr = pPathGeometry->Open(&pSink); }
	if (SUCCEEDED(hr))
	{
		pSink->SetFillMode(D2D1_FILL_MODE_ALTERNATE);
		pSink->BeginFigure(D2D1::Point2F(0, 0), D2D1_FIGURE_BEGIN_FILLED);
		pSink->AddLine(D2D1::Point2F(200, 0));
		pSink->AddBezier(D2D1::BezierSegment(D2D1::Point2F(150, 50), D2D1::Point2F(150, 150), D2D1::Point2F(200, 200)));
		pSink->AddLine(D2D1::Point2F(0, 200));
		pSink->AddBezier(D2D1::BezierSegment(D2D1::Point2F(50, 150), D2D1::Point2F(50, 50), D2D1::Point2F(0, 0)));
		pSink->EndFigure(D2D1_FIGURE_END_CLOSED);
		hr = pSink->Close();
	}

	if (SUCCEEDED(hr))
	{
		static const D2D1_GRADIENT_STOP stops[] = { { 0.f,{ 0.f, 1.f, 1.f, 1.f } },{ 1.f,{ 0.f, 0.f, 1.f, 1.f } }, };
		hr = pRT->CreateGradientStopCollection(stops, ARRAYSIZE(stops), &pGradientStops);
	}

	if (SUCCEEDED(hr)) { hr = pRT->CreateLinearGradientBrush(D2D1::LinearGradientBrushProperties(D2D1::Point2F(100, 0), D2D1::Point2F(100, 200)), D2D1::BrushProperties(), pGradientStops, &pLGBrush); }
	if (SUCCEEDED(hr)) { hr = pRT->CreateSolidColorBrush(D2D1::ColorF(D2D1::ColorF::Black), &pBlackBrush); }
	if (SUCCEEDED(hr))
	{
		// Render into the bitmap.
		pRT->BeginDraw();
		pRT->Clear(D2D1::ColorF(D2D1::ColorF::White));
		D2D1_SIZE_F rtSize = pRT->GetSize();

		// Set the world transform to a 45 degree rotation at the center of the render target
		// and write "Hello, World".
		pRT->SetTransform(D2D1::Matrix3x2F::Rotation(45, D2D1::Point2F(rtSize.width / 2, rtSize.height / 2)));

		static const WCHAR sc_helloWorld[] = L"Hello, World!";
		pRT->DrawText(sc_helloWorld, ARRAYSIZE(sc_helloWorld) - 1, pTextFormat, D2D1::RectF(0, 0, rtSize.width, rtSize.height), pBlackBrush);

		// Reset back to the identity transform.
		pRT->SetTransform(D2D1::Matrix3x2F::Translation(0, rtSize.height - 200));
		pRT->FillGeometry(pPathGeometry, pLGBrush);
		pRT->SetTransform(D2D1::Matrix3x2F::Translation(rtSize.width - 200, 0));
		pRT->FillGeometry(pPathGeometry, pLGBrush);
		hr = pRT->EndDraw();
	}
*/
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Create Required Brush Pattern
	if (SUCCEEDED(hr)) { hr = pRT->CreateCompatibleRenderTarget(D2D1::SizeF(16.0f, 16.0f), &pCompatibleRenderTarget);}
	if (SUCCEEDED(hr))
	{
		// Draw a pattern.
		hr = pCompatibleRenderTarget->CreateSolidColorBrush(D2D1::ColorF(D2D1::ColorF(0.93f, 0.94f, 0.96f, 1.0f)), &pGridBrush);
		if (SUCCEEDED(hr))
		{
			pCompatibleRenderTarget->BeginDraw();
			//DrawPattern::DrawGridPattern();
			//pCompatibleRenderTarget->FillRectangle(D2D1::RectF(0.0f, 0.0f, 4.0f, 1.0f), pGridBrush);
			//pCompatibleRenderTarget->FillRectangle(D2D1::RectF(0.0f, 0.1f, 1.0f, 4.0f), pGridBrush);

			pCompatibleRenderTarget->FillRectangle(D2D1::RectF(0.0f, 0.0f, 16.0f, 2.0f), pGridBrush);

			//pCompatibleRenderTarget->FillRectangle(D2D1::RectF(0.0f, 0.0f, 16.0f, 8.0f), pGridBrush);


			pCompatibleRenderTarget->FillRectangle(D2D1::RectF(0.0f, 14.0f, 16.0f, 16.0f), pGridBrush);

			pCompatibleRenderTarget->EndDraw();

			// Retrieve the bitmap from the render target.
			hr = pCompatibleRenderTarget->GetBitmap(&pGridBitmap);
		}
	}

	// Choose the tiling mode for the bitmap brush.
	D2D1_BITMAP_BRUSH_PROPERTIES brushProperties = D2D1::BitmapBrushProperties(D2D1_EXTEND_MODE_WRAP, D2D1_EXTEND_MODE_WRAP);

	// Create the bitmap brush.
	pRT->CreateBitmapBrush(pGridBitmap, brushProperties, &pBitmapBrush);

	pRT->BeginDraw();
	pRT->Clear(D2D1::ColorF(0.0f, 0.0f, 0.0f));
	pRT->FillRectangle(D2D1::RectF(0.0f, 0.0f, 300.0f, 300.0f), pBitmapBrush);
	pRT->EndDraw();

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	if (SUCCEEDED(hr)) { hr = pWICFactory->CreateStream(&pStream); }
	WICPixelFormatGUID format = GUID_WICPixelFormatDontCare;

	// Use InitializeFromFilename to write to a file. If there is need to write inside the memory, use InitializeFromMemory. 
	if (SUCCEEDED(hr))
	{
		static const WCHAR filename[] = L"D:/Code_Practice/Windows_Programming/Direct_2D/5_Draw_Bitmap/output.png";
		hr = pStream->InitializeFromFilename(filename, GENERIC_WRITE);
	}

	if (SUCCEEDED(hr)) { hr = pWICFactory->CreateEncoder(GUID_ContainerFormatPng, NULL, &pEncoder); }
	if (SUCCEEDED(hr)) { hr = pEncoder->Initialize(pStream, WICBitmapEncoderNoCache); }
	if (SUCCEEDED(hr)) { hr = pEncoder->CreateNewFrame(&pFrameEncode, NULL); }

	// Use IWICBitmapFrameEncode to encode the bitmap into the picture format you want.
	if (SUCCEEDED(hr)) { hr = pFrameEncode->Initialize(NULL); }
	if (SUCCEEDED(hr)) { hr = pFrameEncode->SetSize(sc_bitmapWidth, sc_bitmapHeight); }
	if (SUCCEEDED(hr)) { hr = pFrameEncode->SetPixelFormat(&format); }
	if (SUCCEEDED(hr)) { hr = pFrameEncode->WriteSource(pWICBitmap, NULL); }
	if (SUCCEEDED(hr)) { hr = pFrameEncode->Commit(); }
	if (SUCCEEDED(hr)) { hr = pEncoder->Commit(); }
}

ExportToBMP ::~ExportToBMP()
{
	pWICFactory->Release();
	pRT->Release();
	pWICBitmap->Release();
	pD2DFactory->Release();
	pStream->Release();
	pEncoder->Release();
	pFrameEncode->Release();
/*
	pDWriteFactory->Release();
	pTextFormat->Release();
	pPathGeometry->Release();
	pSink->Release();
	pGradientStops->Release();
	pBlackBrush->Release();
	pLGBrush->Release();
*/
	pGridBitmap->Release();
	pBitmapBrush->Release();
	pCompatibleRenderTarget->Release();
}