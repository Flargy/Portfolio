#pragma once
#ifndef KEYBOARDFUNCTIONS_H
#define KEYBOARDFUNCTIONS_H


#include <functional>
#include <map>
#include <vector>
namespace Bot {
	class KeyboardFunctions
	{
	public:
		static KeyboardFunctions* Instance();
		void addKeyBind(int i, std::function<void()>);
		void buttonDown(const int button);
	
		~KeyboardFunctions();
	private:
		static KeyboardFunctions* instance;
		std::map <int, std::function<void()>> bindKey;

	protected:
		KeyboardFunctions();
	};
	extern KeyboardFunctions* key;
}
#endif // !KEYBOARDFUNCTIONS_H
