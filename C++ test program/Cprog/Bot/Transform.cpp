#include "Transform.h"
#include "Level.h"

//denna klass blev inte klar
namespace Bot {

	Transform::Transform(const char* path, int x, int y, int collide, bool anim, int speed, int index, int frames):Sprite(path, x, y, collide, anim, speed, index, frames)
	{
	}

	void Transform::collided(std::shared_ptr<Sprite> s) {
		if (s->getDestructable()) {
			lvl->toRemove(s);
		}
		velocity.at(1) = bounceHeight;
	}

	void Transform::setVelocityX(float x) {
		velocity.at(0) = x;
	}
	void Transform::setVelocityY(float y) {
		velocity.at(1) = y;
	}

	void Transform::updatePosition(float i) {
		int x = round(getRect()->x + velocity.at(0));
		int y = round(getRect()->y + velocity.at(1));

		getRect()->x = x;
		getRect()->y = y;

		if (velocity.at(1) < i) {
			velocity.at(1) += gravity.at(1) * (1.0F / gs->getFps());
		}
		if (velocity.at(1) > -0.3 && velocity.at(1) < 0.2) {
			velocity.at(1) = 0.6F;
		}
		
	}

	void Transform::updateObject() {
		int x = round(getRect()->x + velocity.at(0));
		int y = round(getRect()->y + velocity.at(1));

		getRect()->x = x;
		getRect()->y = y;
	}

	void Transform::setBounceHeight(float f) {
		bounceHeight = f;
	}
	void Transform::setMoveSpeed(float f) {
		moveSpeed = f;
	}
	void Transform::setGravityDrag(float f) {
		gravity.at(1) = f;
	}

	Transform::~Transform()
	{
		gravity.clear();
		velocity.clear();
	}
}// denna klass blev inte klar