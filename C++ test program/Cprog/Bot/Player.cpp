#include "Player.h"
#include <iostream>
#include <memory>

using namespace Bot;


	Player::Player(const char* path, int x, int y, bool anim, int speed, int index, int frames):Transform(path,x,y, 2, anim, speed, index, frames)
	{ 
	}
	void Player::move() {
		getRect()->x--;
	}
	void Player::moRight() {
		getRect()->x++;
	}
	
	std::shared_ptr<Player> Player::getInstance(const char* path, int x, int y, bool anim, int speed, int index, int frames) {
		return std::shared_ptr<Player> (new Player(path, x, y, anim, speed, index, frames));
	}

	Player::~Player()
	{
		Sprite::~Sprite();

	}
