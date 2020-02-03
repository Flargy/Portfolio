Denna spelmotor �r designad f� att skapa spel baserat runt att spelaren automatiskt ska hoppa och undvika/hoppa �ver fiender.

F�r att skapa ett spel via denna motor kr�vs ett antal saker:

I funktionen som startar och skapar spelet kommer tv� specifika funktioner beh�vas aktiveras: "GameSession::Instance()" och ""KeyboardFuntions::Instance()".
Dessa funktioner startar instansieringen av f�nstret d�r spelet kommer synas samt funktionerna f�r att ta emot kortkomandon i form av funktionspekare.

Funktionspekare skapas p� f�ljande s�tt: key->addKeyBind("asci v�dre p� knappen eller SDLK", std::bind(&"funktion fr�n klass", "objekt funktionen ska kopplas till"));.
G�r �ven att g�ra vi lamda p� f�ljade s�tt: key->addKeyBind("asci v�rde p� knapp eller SDLK", std::function<void()>([]() { "funktion"; }))
Detta kommer l�gga in funktionspekaren in i en map som gameloopen kollar igenom efter inputs.

N�r du sedan ska designa flera niv�er/levels i spelet kan detta g�ras genom att anv�nda level pekaren "lvl". denna pekare anv�nds f�r att l�gga till objekt
i listor f�r olika instancer av level.
F�r att skapa en instance av Level klassen kan f�ljande g�ras: "pekare till Level objekt" = "Level::createLevel();".
Detta kommer skapa en ny level som du sedan kan komma �t med pekaren du anv�nde.

F�r att l�gga till bilder till spelet g� till: Bot\Bot\Bilder mappen och l�gg till de �nskade bilderna du vill anv�nda dig av i spelet.

F�r att skapa din f�rsta level g�rs f�ljande:

"Level pekare"->add(Background::getInstance("Bilder/"bildnamn"", "boolean f�r animering eller inte"));
"shared_ptr f�r Player"= Player::getInstance("Bilder/"bildnamn"", "position i x v�rde", "position i y v�rde", "bool f�r animation", "uppdateringsfrekvens", "antal rader p� spritesheet", "antal sprites p� rad");
"Level pekare"->add("shared_ptr f�r Player");
"Level pekare"->add(Objects::getInstance("Bilder/"bildnamn"", "position i x v�rde", "position i y v�rde", "bool f�r animation", "bool f�r om objektet ska f�rst�ras vid kontakt med spelaren")); (samma variabler som i getInstance() f�r Player)
"Level pekare"->add(Objects::getInstance("Bilder/"bildnamn"", "position i x v�rde", "position i y v�rde", "bool f�r animation", "bool f�r om objektet ska f�rst�ras vid kontakt med spelaren"));

Skapa ny level genom att g�ra en till Level pekare via "Level::createLevel()" som tidigare f�rklarat.
Level kan bytas genom att s�tta pekaren "lvl" till en av pekarna du skapade.


FPS kan s�ttas p� f�ljande s�tt:
gs->setFps(60);

Starta spelet via:
gs->run();