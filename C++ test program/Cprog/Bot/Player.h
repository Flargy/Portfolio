#ifndef PLAYER_H
#define PLAYER_H
#pragma once
#include "Transform.h"
#include <vector>

namespace Bot {

	class Player : public Transform
	{

	protected:
		Player(const char* path, int x, int y, bool anim, int speed, int index, int frames);
	public:
		void moRight();
		void move();
		void buttonEvent(SDL_KeyboardEvent event) {}
		static std::shared_ptr<Player> getInstance(const char* path, int x = 0, int y = 0, bool anim = true, int speed = 0, int index = 0, int frames = 0);
		~Player();
		Player* getPlayer() { return this; }



	private:

		int x, y;
	};


}
#endif