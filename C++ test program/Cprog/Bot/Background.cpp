#include "Background.h"



namespace Bot {


	Background::Background(const char* path, int x, int y, bool anim, int speed, int index, int frames):Sprite(path,x,y, 1, anim, speed, index, frames)
	{ 
	}

	std::shared_ptr<Background> Background::getInstance(const char* path, int x, int y, bool anim, int speed, int index, int frames) {
		return std::shared_ptr<Background> (new Background(path, x, y, anim, speed, index, frames));
	}


	Background::~Background()
	{
		Sprite::~Sprite();

	}
}