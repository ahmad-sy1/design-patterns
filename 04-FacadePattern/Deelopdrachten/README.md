# Facade Pattern — GameApp (Startopdracht)

Console-applicatie die een game start via losse subsystemen.

## Structuur

```
04-FacadePattern/Deelopdrachten/
├── GraphicSystem.cs      // GraphicsSystem
├── AudioSystem.cs
├── SaveSystem.cs
├── NetworkService.cs
├── GameEngine.cs
├── DebugConsole.cs       // deel 2: extra subsysteem voor DeveloperMode
├── GameMode.cs           // deel 2: Normal of Developer
├── ConnectionMode.cs     // deel 3: Online of Offline
├── GameFacade.cs         // deel 3: facade (in deel 2 nog GameManager)
└── Program.cs            // client
```

---

## Deel 1 — Onderzoeken

**Opdracht:**

1. Neem de sourcebestanden op in een nieuwe C# console app.
2. Bestudeer de applicatie GameApp.
3. Run het programma.
4. Werkt prima, toch?

### Uitwerking

De bestanden staan in `FacadePattern.csproj`. `Program` maakt vijf subsystemen aan en roept ze zelf één voor één aan.

```
Program
   |
   +--> GraphicsSystem   Initialize, SetResolution
   +--> AudioSystem      Initialize, SetVolume
   +--> SaveSystem       LoadSettings, LoadPlayer
   +--> NetworkService   Connect, Login
   +--> GameEngine       LoadWorld, Start
```

### Testresultaat

```
Graphics geïnitialiseerd.
Resolutie ingesteld op 1920x1080.
Audio geïnitialiseerd.
Volume ingesteld op 70%.
Instellingen geladen.
Spelergegevens geladen.
Verbonden met gameserver.
Speler ingelogd.
Gamewereld geladen.
Game gestart!
```

### Antwoorden op de vragen

**Werkt prima, toch?**
Ja. Maar `Program` moet alles weten: welke subsystemen, welke methodes en in welke volgorde. Bij meer manieren van starten wordt dat lastig.

---

## Deel 2 — Vervolg requirements

**Opdracht:**

1. Splitsing tussen NormalMode en DeveloperMode: zorg dat de GameApp in beide modi kan starten (pas Program.cs daarvoor aan).
2. Game moet ook kunnen worden gestopt: bedenk en schrijf zelf toepasselijke subsystemen hiervoor; pas Program.cs aan en geef het starten en stoppen (al dan niet in developer mode) een plek.
3. Welke problemen beginnen te ontstaan in je code?
4. Hoe zou je die problemen kunnen oplossen? (dubbele code voorkomen, verantwoordelijkheid aan classes geven)
5. Herschrijf de GameApp naar een betere versie.

### Uitwerking

**Modi**

| | NormalMode | DeveloperMode |
|---|---|---|
| Resolutie | 1920x1080 | 1280x720 |
| Volume | 70% | 0% |
| Debugconsole en FPS | nee | ja |
| Speler opslaan bij stoppen | ja | nee |

**Stoppen**

Elk subsysteem heeft een stopmethode gekregen. `DebugConsole` is nieuw.

| Klasse | Starten | Stoppen |
|---|---|---|
| `GraphicsSystem` | `Initialize`, `SetResolution` | `Shutdown` |
| `AudioSystem` | `Initialize`, `SetVolume` | `Shutdown` |
| `SaveSystem` | `LoadSettings`, `LoadPlayer` | `SavePlayer`, `SaveSettings` |
| `NetworkService` | `Connect`, `Login` | `Logout`, `Disconnect` |
| `GameEngine` | `LoadWorld`, `Start` | `Stop`, `UnloadWorld` |
| `DebugConsole` (nieuw) | `Open`, `ShowFps` | `Close` |

**Eerste versie: alles in `Program`**

Alles stond in `Main`, 86 regels. Veel code stond er twee keer:

```csharp
// NormalMode starten
graphics.Initialize();
graphics.SetResolution(1920, 1080);

audio.Initialize();
audio.SetVolume(70);

saveSystem.LoadSettings();
saveSystem.LoadPlayer();

network.Connect();
network.Login();

gameEngine.LoadWorld();
gameEngine.Start();

// ... NormalMode stoppen ...

// DeveloperMode starten
graphics.Initialize();
graphics.SetResolution(1280, 720);

audio.Initialize();
audio.SetVolume(0);

saveSystem.LoadSettings();
saveSystem.LoadPlayer();

network.Connect();
network.Login();

debugConsole.Open();
debugConsole.ShowFps();

gameEngine.LoadWorld();
gameEngine.Start();

// ... DeveloperMode stoppen ...
```

**Herschreven versie: `GameManager`**

Starten en stoppen staat nu in `GameManager`. De modus is een enum `GameMode`.

```csharp
public void StartGame(GameMode mode)
{
    this.mode = mode;

    graphics.Initialize();
    if (mode == GameMode.Developer)
    {
        graphics.SetResolution(1280, 720);
    }
    else
    {
        graphics.SetResolution(1920, 1080);
    }

    audio.Initialize();
    audio.SetVolume(mode == GameMode.Developer ? 0 : 70);

    saveSystem.LoadSettings();
    saveSystem.LoadPlayer();

    network.Connect();
    network.Login();

    if (mode == GameMode.Developer)
    {
        debugConsole.Open();
        debugConsole.ShowFps();
    }

    gameEngine.LoadWorld();
    gameEngine.Start();
}
```

```csharp
GameManager game = new GameManager();

game.StartGame(GameMode.Normal);
game.StopGame();

game.StartGame(GameMode.Developer);
game.StopGame();
```

### Testresultaat

Beide versies geven dezelfde uitvoer:

```
=== NormalMode starten ===
Graphics geïnitialiseerd.
Resolutie ingesteld op 1920x1080.
Audio geïnitialiseerd.
Volume ingesteld op 70%.
Instellingen geladen.
Spelergegevens geladen.
Verbonden met gameserver.
Speler ingelogd.
Gamewereld geladen.
Game gestart!

=== NormalMode stoppen ===
Game gestopt.
Gamewereld afgesloten.
Spelergegevens opgeslagen.
Instellingen opgeslagen.
Speler uitgelogd.
Verbinding met gameserver verbroken.
Audio afgesloten.
Graphics afgesloten.

=== DeveloperMode starten ===
Graphics geïnitialiseerd.
Resolutie ingesteld op 1280x720.
Audio geïnitialiseerd.
Volume ingesteld op 0%.
Instellingen geladen.
Spelergegevens geladen.
Verbonden met gameserver.
Speler ingelogd.
Debugconsole geopend.
FPS-teller zichtbaar.
Gamewereld geladen.
Game gestart!

=== DeveloperMode stoppen ===
Game gestopt.
Gamewereld afgesloten.
Instellingen opgeslagen.
Speler uitgelogd.
Verbinding met gameserver verbroken.
Debugconsole gesloten.
Audio afgesloten.
Graphics afgesloten.
```

### Antwoorden op de vragen

**3. Welke problemen beginnen te ontstaan?**
- Dubbele code: de modi lijken bijna helemaal op elkaar.
- `Program` weet te veel: alle subsystemen, methodes en de volgorde.
- Bij een wijziging moet je elk blok aanpassen. Vergeet je er één, dan gaat het mis.

**4. Hoe los je dat op?**
Zet starten en stoppen in één klasse. `Program` zegt alleen nog "start" of "stop".

**5. Betere versie**
Zie `GameManager` hierboven. `Program` ging van 86 naar 27 regels.

---

## Deel 3 — Facade Pattern

**Opdracht:**

1. Bestudeer de PowerPoint over de Facade Pattern.
2. Nieuwe feature: online-modus en offline-modus. De keuze bepaalt of NetworkService wordt aangeroepen of niet.
3. Zorg (en controleer) dat de GameApp volledig voldoet aan het Facade Design Pattern.
4. Wat zijn de verschillen en overeenkomsten tussen Facade en Adapter?

### Uitwerking

**1. Facade Pattern**

Een facade is één simpele klasse vóór een groep ingewikkelde klassen. De client praat alleen met de facade. (De PowerPoint staat niet in de repo, dit komt uit Head First H7.)

**2. Online en offline**

Nieuwe enum `ConnectionMode`. `NetworkService` wordt alleen bij `Online` aangeroepen:

```csharp
if (connection == ConnectionMode.Online)
{
    network.Connect();
    network.Login();
}
```

**3. Facade Pattern**

`GameManager` heet nu `GameFacade` en krijgt de subsystemen via de constructor:

```csharp
GameFacade game = new GameFacade(
    new GraphicsSystem(),
    new AudioSystem(),
    new SaveSystem(),
    new NetworkService(),
    new GameEngine(),
    new DebugConsole());

game.StartGame(GameMode.Normal, ConnectionMode.Online);
game.StopGame();
```

Controle:
- `Program` gebruikt alleen `GameFacade`.
- `GameFacade` geeft het werk door aan de subsystemen.
- De subsystemen kennen de facade niet en zijn ook los te gebruiken.

```
          Program (client)
              |
              |  StartGame(mode, connection) / StopGame()
              v
         +------------+
         | GameFacade |
         +------------+
              |
   +----------+-----------+-------------+------------+--------------+
   |          |           |             |            |              |
Graphics    Audio     SaveSystem  NetworkService  GameEngine  DebugConsole
System      System                (alleen online)             (alleen developer)
```

### Testresultaat

```
=== NormalMode online starten ===
Graphics geïnitialiseerd.
Resolutie ingesteld op 1920x1080.
Audio geïnitialiseerd.
Volume ingesteld op 70%.
Instellingen geladen.
Spelergegevens geladen.
Verbonden met gameserver.
Speler ingelogd.
Gamewereld geladen.
Game gestart!

=== NormalMode online stoppen ===
Game gestopt.
Gamewereld afgesloten.
Spelergegevens opgeslagen.
Instellingen opgeslagen.
Speler uitgelogd.
Verbinding met gameserver verbroken.
Audio afgesloten.
Graphics afgesloten.

=== NormalMode offline starten ===
Graphics geïnitialiseerd.
Resolutie ingesteld op 1920x1080.
Audio geïnitialiseerd.
Volume ingesteld op 70%.
Instellingen geladen.
Spelergegevens geladen.
Gamewereld geladen.
Game gestart!

=== NormalMode offline stoppen ===
Game gestopt.
Gamewereld afgesloten.
Spelergegevens opgeslagen.
Instellingen opgeslagen.
Audio afgesloten.
Graphics afgesloten.

=== DeveloperMode offline starten ===
Graphics geïnitialiseerd.
Resolutie ingesteld op 1280x720.
Audio geïnitialiseerd.
Volume ingesteld op 0%.
Instellingen geladen.
Spelergegevens geladen.
Debugconsole geopend.
FPS-teller zichtbaar.
Gamewereld geladen.
Game gestart!

=== DeveloperMode offline stoppen ===
Game gestopt.
Gamewereld afgesloten.
Instellingen opgeslagen.
Debugconsole gesloten.
Audio afgesloten.
Graphics afgesloten.
```

### Antwoorden op de vragen

**4. Facade en Adapter**

Overeenkomst: allebei zetten ze een klasse tussen de client en bestaande code, zonder die code aan te passen.

| | Adapter | Facade |
|---|---|---|
| Doel | passend maken | makkelijker maken |
| Pakt in | meestal één klasse | meerdere klassen |
| Interface | bestaande interface | nieuwe, simpele interface |

Kort: een adapter verandert de interface, een facade maakt hem simpeler.
