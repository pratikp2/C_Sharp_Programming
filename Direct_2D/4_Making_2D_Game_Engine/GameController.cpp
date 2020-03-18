#include "GameController.h"

GameLevel * GameController :: currentLevel = nullptr;

bool GameController::Loading = false;

void GameController::LoadInitialLevel(GameLevel * level)
{
	Loading = true;
	currentLevel = level;
	currentLevel->LoadLevel();
	Loading = false;
}

void GameController :: SwitchLevel(GameLevel * level)
{
	Loading = true;
	currentLevel->UnLoadLevel();
	delete currentLevel;
	currentLevel = level;
	currentLevel->LoadLevel();
	Loading = false;
}

void GameController::Render()
{ 
	if (Loading) return;
	currentLevel->RenderLevel();
}

void GameController::Update()
{
	if (Loading) return;
	currentLevel->UpdateLevel();
}