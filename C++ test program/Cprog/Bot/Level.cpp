#include "Level.h"

namespace Bot {
	Level* lvl;

	Level::Level()
	{
	}

	void Level::add(std::shared_ptr<Sprite> s) {
		addSprite.push_back(s);
	}

	void Level::drawLevel() {
		for (std::shared_ptr<Sprite> s : spriteVec) {

			if (s->getTag() == 2) {
				s->updatePosition(9.82F);
			}
			else if (s->getTag() == 3) {
				s->updateObject();
			}
			s->draw();
		}//outer for

	}

	void Level::addingSprites() {
		for (std::shared_ptr<Sprite> s : addSprite) {
			spriteVec.push_back(s);
		}
		addSprite.clear();
	}

	void Level::nextLevel() {
		lvl;
	}

	Level* Level::createLevel() {

		 return new Level();
	}

	

	void Level::toRemove(std::shared_ptr<Sprite> s) {
		removeSprite.push_back(s);
	}

	void Level::remove() {
		for (std::shared_ptr<Sprite> s : removeSprite) {
			for (int i = spriteVec.size()-1; i >= 0; i--) {
				if (spriteVec[i] == s) {
					spriteVec.erase(spriteVec.begin() + i);
				}
			}
		}
		removeSprite.clear();
	}

	Level::~Level()
	{
		spriteVec.clear();
		removeSprite.clear();
		addSprite.clear();
		
	}
}