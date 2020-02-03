#ifndef TEXT_H
#define TEXT_H
#include <SDL.h>
#include "SDL_ttf.h"
#include <string>
#include "GameSession.h"
#include "KeyboardFunctions.h"

// Ignorera denna klass
namespace Bot {
	class Text
	{
	public:
		static Text* Instance();
		void createText();
		Text();
		~Text();
	private:
		static Text* instance;
		std::string inputText = "fisk";
		SDL_Rect* textField;
		SDL_Color color{ 0,0,0 };
		SDL_Surface* textSurface;
		SDL_Texture* textTexture;
		SDL_Rect rect;
	};
	extern Text* txt;

	// Ignorera denna klass
}
#endif // !1
