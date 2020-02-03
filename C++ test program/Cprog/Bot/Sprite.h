#ifndef SPRITE_H
#define SPRITE_H
#include <SDL.h>
#include <SDL_image.h>
#include <map>
#include <memory>
#include <SDL_ttf.h>


namespace Bot {

	class Sprite
	{

	protected:

		Sprite(const char* path, int x, int y, int collide, bool anim, int speed, int index, int frames);

	public:

		const char* path;
		SDL_Rect* getRect() { return &rect; }
		SDL_Rect* getSRect() { return &sourceRect; }
		void resetPos(int x, int y);
		SDL_Texture* getTexture() { return tx; }
		int getTag() { return collide; }
		bool getAnim() { return anim; }
		void setTexture(SDL_Texture* tx) {}
		virtual ~Sprite();
		void draw();
		virtual void moveLeft() {}
		virtual void moveRight() {}
		virtual void updatePosition(float i) {}
		virtual void updateObject() {}
		virtual void collided(std::shared_ptr<Sprite>) {}
		virtual void fallSpeed(float i) {}
		virtual bool getDestructable() { return false; }


		
	private:

		SDL_Surface* surf;
		SDL_Rect rect;
		SDL_Rect sourceRect;
		SDL_Texture* tx;
		int collide;
		bool anim;
		bool success;
		int speed;
		int index;
		int frames;
		int x;
		int y;
	};


}
#endif