#include "GameSession.h"
#include "Background.h"
#include <iostream>
#include "Collision.h"
#include "KeyboardFunctions.h"
#include<memory>


namespace Bot {
	GameSession* GameSession::instance = nullptr;
	GameSession* gs;
	

	void GameSession::pauseG() {
		pause = !pause;
	}
	GameSession* GameSession::Instance() {
		if (!instance)
			instance = new GameSession;
		gs = instance;
		return instance;
	}

	GameSession::GameSession()
	{
		win = new Window();
		
	}
	

	void GameSession::setFps(int FPSN) {
		FPS = FPSN;
		frameDelay = 1000 / FPS;
	}

	void GameSession::addLevelToList(Level* l) {
		levelList.push_back(l);
	}

	void GameSession::run() {
		bool quit = false;
		
		while (!quit) {
			frameStart = SDL_GetTicks();

			SDL_Event event;
			while (SDL_PollEvent(&event)|| pause) {
				switch (event.type) {
				case SDL_KEYDOWN:
					key->buttonDown(event.key.keysym.sym);
					break;
				case SDL_MOUSEBUTTONDOWN: break;
				case SDL_QUIT: quit = true; break;
				}//switch
			}//inre while

			lvl->addingSprites();
			lvl->remove();

			for (std::shared_ptr<Sprite> s : lvl->getVec()) {
				if (s->getTag() == 2) {
					std::shared_ptr<Sprite> p = s;
					for (std::shared_ptr<Sprite> r : lvl->getVec()) {
						if (r->getTag() == 3) {
							if (Collision::AABB(p->getRect(), r->getRect())) {
								p->collided(r);
							}
						
						}
					}
				}
			}
			SDL_RenderClear(win->getRen());

			lvl->drawLevel();

			SDL_RenderPresent(win->getRen());
			frameTime = SDL_GetTicks() - frameStart;
			if (frameDelay > frameTime) {
				SDL_Delay(frameDelay - frameTime);
			}


		}//yttre while
		gs->~GameSession();
	}//GameSession run
	
	GameSession::~GameSession()
	{
		delete win;
		
		for (Level* l : levelList) {
			l->~Level();
		}
		
		key->~KeyboardFunctions();
	


	}
}