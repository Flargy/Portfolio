#include "GameSession.h"
#include <SDL.h>
#include "Sprite.h"
#include "Window.h"
#include "Player.h"
#include "Background.h"
#include "Objects.h"
#include "Transform.h"
#include <iostream>
#include <functional>
#include "KeyboardFunctions.h"
#include "Level.h"
#include "Text.h"

using namespace std;
using namespace Bot;
Level* lvl1;
Level* lvl2;
std::shared_ptr<Player> play;

void changeLevel(Level* l) {
	
	if (lvl == lvl1) {
		lvl = lvl2;
		play->resetPos(200, 200);
	}
	else {
		lvl = lvl1;
		play->resetPos(200, 200);

	}
}

int main(int argc, char** argv) {
	using namespace std::placeholders;
	GameSession::Instance();
	KeyboardFunctions::Instance();
	//Text::Instance();
	lvl1 = Level::createLevel();
	gs->addLevelToList(lvl1);
	lvl2 = Level::createLevel();
	gs->addLevelToList(lvl2);

	lvl = lvl1;

	lvl1->add(Background::getInstance("Bilder/bg.jpg", false));
	play = Player::getInstance("Bilder/FrictionBot.png", 200, 200, true, 100, 1, 4);
	lvl1->add(play);
	lvl1->add(Objects::getInstance("Bilder/robot.jpg", 200, 420, false, false));
	lvl1->add(Objects::getInstance("Bilder/platform.png", 400, 450, false, true));

	lvl2->add(Background::getInstance("Bilder/robot.jpg", false));
	lvl2->add(play);
	lvl2->add(Objects::getInstance("Bilder/bg.jpg", 200, 420, false));

	play->setBounceHeight(-2.0F);
	play->setGravityDrag(2.0F);
	play->setMoveSpeed(4.0F);
	key->addKeyBind(106, std::function<void()>([]() { changeLevel(lvl); }));
	key->addKeyBind(1073741903, std::bind(&Player::moRight, play));
	key->addKeyBind(1073741904, std::bind(&Player::move, play));
	//key->addKeyBind(SDLK_o, std::function<void()>([]() {txt->createText(); }));
	

	gs->setFps(60);

	gs->run();

	return 0;
}
// alltså fixa så att gameSessions destructor tarbort ALLA objekt.
// Fixa Text fält, Fixa förmågan att byta "level"
