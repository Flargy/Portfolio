#ifndef GAMESESSION_H
#define GAMESESSION_H
#include "Sprite.h"
#include "Window.h"
#include <vector>
#include <functional>
#include <map>
#include <memory>
#include "Level.h"


namespace Bot {
	class GameSession
	{
	public:
		GameSession();
		void run();
		void pauseG();
		~GameSession();
		static GameSession* Instance();
		void addLevelToList(Level* l);
		void setFps(int FPSN);
		int getFps() { return FPS; }
		Window* getWindow() { return win; }
		Window* win;
	
		

	private:
		bool pause = false;
		std::vector<Level*> levelList;
		static GameSession* instance;
		int FPS = 60;
		int frameDelay = 1000 / FPS;
		Uint32 frameStart;
		int frameTime;
	};

	extern GameSession* gs;
	
}
#endif
