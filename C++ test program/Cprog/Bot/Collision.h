#ifndef COLLISION_H
#define COLLISION_H
#include <string>
#include "SDL.h"
#pragma once


class Collision
{
public:
	static bool AABB(const SDL_Rect* recA, const SDL_Rect* recB);
	Collision();
	~Collision();


};

#endif