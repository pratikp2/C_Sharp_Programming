#pragma once
# include "graphics.h"
# include "SpritedSheet.h"

class GameLevel 
{
protected :
	static Graphics * gfx;

public : 
	static void Init(Graphics * graphics) { gfx = graphics; }
	virtual void LoadLevel() = 0;
	virtual void UnLoadLevel() = 0;
	virtual void UpdateLevel() = 0;
	virtual void RenderLevel() = 0;
};
