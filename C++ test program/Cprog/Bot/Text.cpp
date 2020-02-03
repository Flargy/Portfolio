#include "Text.h"
#include <iostream>

// Ignorera denna klass

namespace Bot {
	Text* Text::instance = nullptr;
	Text* txt;
	Text* Text::Instance() {
		if (!instance)
			instance = new Text;
		txt = instance;
		return instance;
	}

	Text::Text()
	{
	}


	Text::~Text()
	{
	}

	void Text::createText() {

		TTF_Font* font = TTF_OpenFont("Font/ariblk.ttf", 36);
		std::cout << "creating text";
		bool textInput = true;
		//textColor = { 0, 0, 0 };
		textField = new SDL_Rect{ 500, 50, 30, 30 };


		if (textInput) {

			SDL_StartTextInput();
			SDL_SetTextInputRect(textField);
			while (textInput) {
				SDL_Event event;
				while (SDL_PollEvent(&event) != SDLK_KP_ENTER) {
					std::cout << "inside loop";
					inputText += event.text.text;
					textSurface = TTF_RenderText_Solid(font, inputText.c_str(), color);
					textTexture = SDL_CreateTextureFromSurface(gs->win->getRen(), textSurface);
					rect = { 100, 100, 50, 50 };
					SDL_RenderCopy(gs->win->getRen(), textTexture, NULL , &rect);

				}//inre while
				textInput = false;
				SDL_FreeSurface(textSurface);
				SDL_DestroyTexture(textTexture);
				TTF_CloseFont(font);

			}
			SDL_StopTextInput();
			textInput = false;
		}


	}

}