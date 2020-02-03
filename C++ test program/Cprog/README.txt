Denna spelmotor är designad fö att skapa spel baserat runt att spelaren automatiskt ska hoppa och undvika/hoppa över fiender.

För att skapa ett spel via denna motor krävs ett antal saker:

I funktionen som startar och skapar spelet kommer två specifika funktioner behövas aktiveras: "GameSession::Instance()" och ""KeyboardFuntions::Instance()".
Dessa funktioner startar instansieringen av fönstret där spelet kommer synas samt funktionerna för att ta emot kortkomandon i form av funktionspekare.

Funktionspekare skapas på följande sätt: key->addKeyBind("asci vädre på knappen eller SDLK", std::bind(&"funktion från klass", "objekt funktionen ska kopplas till"));.
Går även att göra vi lamda på följade sätt: key->addKeyBind("asci värde på knapp eller SDLK", std::function<void()>([]() { "funktion"; }))
Detta kommer lägga in funktionspekaren in i en map som gameloopen kollar igenom efter inputs.

När du sedan ska designa flera nivåer/levels i spelet kan detta göras genom att använda level pekaren "lvl". denna pekare används för att lägga till objekt
i listor för olika instancer av level.
För att skapa en instance av Level klassen kan följande göras: "pekare till Level objekt" = "Level::createLevel();".
Detta kommer skapa en ny level som du sedan kan komma åt med pekaren du använde.

För att lägga till bilder till spelet gå till: Bot\Bot\Bilder mappen och lägg till de önskade bilderna du vill använda dig av i spelet.

För att skapa din första level görs följande:

"Level pekare"->add(Background::getInstance("Bilder/"bildnamn"", "boolean för animering eller inte"));
"shared_ptr för Player"= Player::getInstance("Bilder/"bildnamn"", "position i x värde", "position i y värde", "bool för animation", "uppdateringsfrekvens", "antal rader på spritesheet", "antal sprites på rad");
"Level pekare"->add("shared_ptr för Player");
"Level pekare"->add(Objects::getInstance("Bilder/"bildnamn"", "position i x värde", "position i y värde", "bool för animation", "bool för om objektet ska förstöras vid kontakt med spelaren")); (samma variabler som i getInstance() för Player)
"Level pekare"->add(Objects::getInstance("Bilder/"bildnamn"", "position i x värde", "position i y värde", "bool för animation", "bool för om objektet ska förstöras vid kontakt med spelaren"));

Skapa ny level genom att göra en till Level pekare via "Level::createLevel()" som tidigare förklarat.
Level kan bytas genom att sätta pekaren "lvl" till en av pekarna du skapade.


FPS kan sättas på följande sätt:
gs->setFps(60);

Starta spelet via:
gs->run();