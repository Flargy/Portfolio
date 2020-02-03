#include "KeyboardFunctions.h"
#include <iostream>
namespace Bot {
	KeyboardFunctions* KeyboardFunctions::instance = nullptr;
	KeyboardFunctions* key;
	KeyboardFunctions* KeyboardFunctions::Instance() {
		if (!instance)
			instance = new KeyboardFunctions;
		key = instance;
		return instance;
	}

	
	void KeyboardFunctions::buttonDown(const int button) {
		try
		{
			bindKey[button]();
		}
		catch (const std::exception&)
		{
			std::cerr << "No Key\n";
		}


	}
	void KeyboardFunctions::addKeyBind(int i, std::function<void()> f) {
		bindKey[i] = f;
	}
	KeyboardFunctions::KeyboardFunctions()
	{
	}


	KeyboardFunctions::~KeyboardFunctions()
	{
		bindKey.clear();
		//delete key;
	}
}