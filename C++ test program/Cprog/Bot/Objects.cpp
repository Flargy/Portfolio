#include "Objects.h"


using namespace Bot;


	Objects::Objects(const char* path, int x, int y, bool anim, bool destructable,  int speed, int index, int frames):Transform(path, x,y, 3, anim, speed, index, frames)
	{ 
		this->destructable = destructable;
	}
	std::shared_ptr<Objects> Objects::getInstance(const char* path, int x, int y, bool anim, bool destructable, int speed, int index, int frames) {
		
		return std::shared_ptr<Objects> (new Objects(path, x, y, anim, destructable, speed, index, frames));
	}

	Objects::~Objects()
	{
		Sprite::~Sprite();
	}




