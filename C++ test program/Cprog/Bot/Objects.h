#ifndef OBJECTS_H
#define OBJECTS_H
#include <SDL.h>
#include <SDL_image.h>
#include "Transform.h"
#pragma once


namespace Bot {
	class Objects : public Transform
		
	{

	protected:
		Objects(const char* path , int x, int y, bool anim, bool destructable, int speed, int index, int frames);
	public:
		bool getDestructable() { return destructable; }
		~Objects();
		static std::shared_ptr<Objects> getInstance(const char* path, int x = 0, int y = 0, bool anim = false, bool destructable = false, int speed = 0, int index = 0, int frames = 0);

	private:
		bool destructable;
		
	};

}
#endif
