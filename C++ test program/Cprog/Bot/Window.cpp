#include "Window.h"
#include <iostream>
#include "SDL_ttf.h"
using namespace std;
namespace Bot {


	Window::Window()
	{
		SDL_Init(SDL_INIT_EVERYTHING);
		//TTF_Init();
		wind = SDL_CreateWindow("Bot", 100, 100, 1200, 900, 0);
		ren = SDL_CreateRenderer(wind, -1, 0);
	}


	Window::~Window()
	{
		SDL_DestroyWindow(wind);
		SDL_DestroyRenderer(ren);
	}
}