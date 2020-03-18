# include "SpritedSheet.h"

SpriteSheet::SpriteSheet(wchar_t * filename, Graphics * gfx)
{
	this->gfx = gfx;
	bmp = NULL;
	HRESULT hr;

	// Create WIC Factory 
	IWICImagingFactory * wicFactory = NULL;
	hr = CoCreateInstance(CLSID_WICImagingFactory, NULL, CLSCTX_INPROC_SERVER, IID_IWICImagingFactory,(LPVOID*)&wicFactory);

	// CoCreateInstance()		: Creates a WIC factory, which can be used to load images from file.
	// CLSID_WICImagingFactory	: CLS ID of the Object we are making 
	// NULL						: Not Part of and aggreate
	// CLSCTX_INPROC_SERVER		: dll runs in the same process	
	// IID_IWICImagingFactory	: Reference to interface that communicates with the object 
	// (LPVOID*)&wicFactory		: The pointer that will contain our factory 

	IWICBitmapDecoder * wicDecoder = NULL;
	hr = wicFactory->CreateDecoderFromFilename(filename, NULL, GENERIC_READ, WICDecodeMetadataCacheOnLoad, &wicDecoder);

	// CreateDecoderFromFilename	:	Decoder will read a file and decode into bitmap 
	// filename						:	Name of the file.
	// NULL							:	No Preferred Vendor
	// GENERIC_READ					:	Read only
	// WICDecodeMetadataCacheOnLoad	:	Cache on load.
	// wicDecoder					:	Pointer to the decoder.

	IWICBitmapFrameDecode * wicFrame = NULL;
	hr = wicDecoder->GetFrame(0, &wicFrame);

	// GetFrame		:	Returns Frame
	// 0			:	The number for frame requested from file (in Case of .gif files)
	// &wicFrame	:	returnes the frame in pointer.

	IWICFormatConverter * wicConverter = NULL;
	hr = wicFactory->CreateFormatConverter(&wicConverter);

	// CreateFormatConverter	:	Converts Windows Imaging Compomponent Format (WIC) in to Direct 2D bitmap format.
	// wicConverter				:	Pointer to the format converter.

	hr = wicConverter->Initialize(wicFrame, GUID_WICPixelFormat32bppPBGRA, WICBitmapDitherTypeNone, NULL, 0.0, WICBitmapPaletteTypeCustom);

	// Initialize						:	Setup Converter to create 32bpp BGRA Bitmap.
	//	wicFrame						:	Frame.
	// GUID_WICPixelFormat32bppPBGRA	:	Pixel Format.
	//	WICBitmapDitherTypeNone			:	Irrelevent.
	// NULL								:	No Palette Needed, irrelevent.
	// 0.0								:	Alpha Transparency.
	// WICBitmapPaletteTypeCustom		:	Irrelevent.

	gfx->GetRenderTarget()->CreateBitmapFromWicBitmap (wicConverter, NULL, &bmp);

	if (wicFactory) wicFactory->Release();
	if (wicDecoder) wicDecoder->Release();
	if (wicConverter) wicConverter->Release();
	if (wicFrame) wicFrame->Release();

	spriteHeight = bmp->GetSize().height;
	spriteWidth = bmp->GetSize().width;
	spriteAcross = 1;
}

SpriteSheet::SpriteSheet(wchar_t * filename, Graphics * gfx, int spriteHeight, int spriteWidth):SpriteSheet(filename,gfx)
{
	this->spriteHeight = spriteHeight;
	this->spriteWidth = spriteWidth;
	this->spriteAcross = (int)bmp->GetSize().width / spriteWidth; 
}

SpriteSheet :: ~SpriteSheet()
{
	if (bmp) bmp->Release();
}

void SpriteSheet::Draw()
{
	gfx->GetRenderTarget()->DrawBitmap(
		bmp,		//	Bitmap
		D2D1::RectF(0.0f, 0.0f, bmp->GetSize().width, bmp->GetSize().height),		// Destantion Rectangle
		0.25f,		// Opacity
		D2D1_BITMAP_INTERPOLATION_MODE::D2D1_BITMAP_INTERPOLATION_MODE_NEAREST_NEIGHBOR,	// Interpolation Mode
		D2D1::RectF(0.0f, 0.0f, bmp->GetSize().width, bmp->GetSize().height)		// Source Rectangle
	);
}

void SpriteSheet::Draw(int index, int x, int y)
{
	D2D_RECT_F src = D2D1::RectF(
					(float)((index % spriteAcross) * spriteWidth),
					(float)((index / spriteAcross) * spriteHeight),
					(float)((index % spriteAcross) * spriteWidth) + spriteWidth,
					(float)((index / spriteAcross) * spriteHeight) + spriteHeight);

	D2D_RECT_F dest = D2D1::RectF(x, y, x+spriteWidth, y+spriteHeight);

	gfx->GetRenderTarget()->DrawBitmap(
		bmp,		//	Bitmap
		dest,		// Destantion Rectangle
		0.25f,		// Opacity
		D2D1_BITMAP_INTERPOLATION_MODE::D2D1_BITMAP_INTERPOLATION_MODE_NEAREST_NEIGHBOR,	// Interpolation Mode
		src			// Source Rectangle
	);
}