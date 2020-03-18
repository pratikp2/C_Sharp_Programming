#pragma once
#include "GameLevel.h"

class GameController 
{
private : 
	GameController(){}
	static bool Loading;
	static GameLevel * currentLevel;

public :
	static void LoadInitialLevel(GameLevel * level);
	static void SwitchLevel(GameLevel * level);

	static void Render();
	static void Update();
};