#ifndef ANIMATION_H
#define ANIMATION_H


#pragma once
namespace Bot {
	class Animation
	{
	public:
		bool animCreate();
		~Animation();

	private:
		bool success;
		int speed;
		int index;
		int frames;
	protected:
		Animation();
		Animation(int i, int f, int s);
	};
}
#endif