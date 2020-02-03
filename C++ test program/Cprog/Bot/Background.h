#ifndef BACKGROUND_H
#define BACKGROUND_H
#include "Sprite.h"
#include <SDL.h>
#include <SDL_image.h>


namespace Bot {

	class Background : public Sprite
	{

	protected:
		Background(const char* path, int x, int y, bool anim, int speed, int index, int frames);
	public:
		static std::shared_ptr<Background> getInstance(const char* path, int x = 0, int y = 0, bool anim = false, int speed = 0, int index = 0, int frames = 0);
		~Background();
	private:
		int x, y;
	};
}
#endif
