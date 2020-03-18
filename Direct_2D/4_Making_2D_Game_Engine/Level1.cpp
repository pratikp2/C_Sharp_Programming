#include "Level1.h"


void Level1::LoadLevel()
{
	frame = 0;
	y = ySpeed = 0.0f;
	ptrSprisheet = new SpriteSheet(L"Exp.png",gfx,64,64);

}

void Level1::UnLoadLevel()
{
	delete ptrSprisheet;
}

void Level1::UpdateLevel()
{
	ySpeed = ySpeed + 1.0f;
	y = y + ySpeed;
	if (y > 600)
	{
		y = 600;
		ySpeed = -30.0f;
	}
	frame++;
}

void Level1::RenderLevel()
{
	gfx->ClearScreen(0.0f, 0.0f, 0.5f);
	gfx->DrawCircle(400.0f, y, 50.0f, 1.0f, 1.0f, 1.0f, 1.0f);

	ptrSprisheet->Draw((frame/10)%12,100,100);
}