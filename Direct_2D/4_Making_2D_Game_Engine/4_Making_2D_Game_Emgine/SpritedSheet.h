#pragma once

# include <wincodec.h>		// WIC Codec Hrader decoding image from file 
# include "graphics.h"
// Link to windowscodecs.lib
class SpriteSheet
{
private :
	Graphics * gfx;
	ID2D1Bitmap * bmp;		// Bitmap Loaded File and converted Direct 2D format
	int spriteHeight, spriteWidth;
	int spriteAcross;

public :
	SpriteSheet(wchar_t * filename, Graphics * gfx);
	SpriteSheet(wchar_t * filename, Graphics * gfx, int spriteHeight, int spriteWidth);
	~SpriteSheet();

	void Draw();
	void Draw(int index, int x, int y	);
};