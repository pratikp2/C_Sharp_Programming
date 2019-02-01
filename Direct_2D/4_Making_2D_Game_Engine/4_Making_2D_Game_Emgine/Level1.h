#pragma once
# include "GameLevel.h"
# include "GameController.h"


class Level1 : public GameLevel
{
private :
	int frame;
	float y = 0.0f;
	float ySpeed = 0.0f;
	SpriteSheet * ptrSprisheet;

public : 
	void LoadLevel() override;
	void UnLoadLevel() override;
	void UpdateLevel() override;
	void RenderLevel() override;
};