# Facade Pattern — GameApp (Startopdracht)

Console-applicatie die een game opstart via losse subsystemen (graphics, audio, opslag, netwerk en engine).

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

De sourcebestanden staan in de console-app `FacadePattern.csproj`, namespace `GameApp`. De app bestaat uit vijf subsystemen:

| Klasse | Bestand | Methodes |
|---|---|---|
| `GraphicsSystem` | `GraphicSystem.cs` | `Initialize`, `SetResolution` |
| `AudioSystem` | `AudioSystem.cs` | `Initialize`, `SetVolume` |
| `SaveSystem` | `SaveSystem.cs` | `LoadSettings`, `LoadPlayer` |
| `NetworkService` | `NetworkService.cs` | `Connect`, `Login` |
| `GameEngine` | `GameEngine.cs` | `LoadWorld`, `Start` |

`Program` maakt alle vijf subsystemen zelf aan en roept daarna tien methodes aan, in een vaste volgorde en met vaste waarden (1920x1080, volume 70%). Let op: het bestand heet `GraphicSystem.cs`, maar de klasse heet `GraphicsSystem`.

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
Ja, het programma doet wat het moet doen. Maar `Program` moet wel alles weten: welke vijf subsystemen er zijn, welke methodes ze hebben, met welke waarden ze aangeroepen worden en in welke volgorde. Dat gaat goed zolang er maar één manier is om de game te starten. Komen er meer manieren bij (zie deel 2), dan moet al die kennis op meerdere plekken in `Program` staan.

---

## Deel 2 — Vervolg requirements

**Opdracht:**

1. Splitsing tussen NormalMode en DeveloperMode: zorg dat de GameApp in beide modi kan starten (pas Program.cs daarvoor aan).
2. Game moet ook kunnen worden gestopt: bedenk en schrijf zelf toepasselijke subsystemen hiervoor; pas Program.cs aan en geef het starten en stoppen (al dan niet in developer mode) een plek.
3. Welke problemen beginnen te ontstaan in je code?
4. Hoe zou je die problemen kunnen oplossen? (dubbele code voorkomen, verantwoordelijkheid aan classes geven)
5. Herschrijf de GameApp naar een betere versie.

### Uitwerking

**NormalMode en DeveloperMode**

DeveloperMode is bedoeld om tijdens het ontwikkelen snel te testen. Daarom verschilt hij op een paar punten van NormalMode:

| | NormalMode | DeveloperMode |
|---|---|---|
| Resolutie | 1920x1080 | 1280x720 |
| Volume | 70% | 0% (geluid uit) |
| Debugconsole en FPS-teller | nee | ja |
| Spelergegevens opslaan bij stoppen | ja | nee, zodat testvoortgang de echte save niet overschrijft |

**Subsystemen voor stoppen**

Voor het stoppen heeft elk subsysteem een tegenhanger van zijn startmethode gekregen. Voor DeveloperMode is er één nieuw subsysteem bijgekomen: `DebugConsole`.

| Klasse | Starten | Stoppen |
|---|---|---|
| `GraphicsSystem` | `Initialize`, `SetResolution` | `Shutdown` |
| `AudioSystem` | `Initialize`, `SetVolume` | `Shutdown` |
| `SaveSystem` | `LoadSettings`, `LoadPlayer` | `SavePlayer`, `SaveSettings` |
| `NetworkService` | `Connect`, `Login` | `Logout`, `Disconnect` |
| `GameEngine` | `LoadWorld`, `Start` | `Stop`, `UnloadWorld` |
| `DebugConsole` (nieuw) | `Open`, `ShowFps` | `Close` |

Bij stoppen is de volgorde ongeveer omgekeerd: eerst de game stoppen, dan opslaan, dan uitloggen en als laatste audio en graphics afsluiten.

**Eerste versie: alles in `Program`**

In de eerste versie staan alle vier de situaties (NormalMode starten en stoppen, DeveloperMode starten en stoppen) achter elkaar in `Main`. `Program` werd daardoor 86 regels lang. Een stuk ervan:

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

Het starten en stoppen is verplaatst naar een eigen klasse, `GameManager`. Die maakt de subsystemen aan, onthoudt in welke modus de game gestart is, en roept de subsystemen in de juiste volgorde aan. De verschillen tussen de modi staan op één plek, in een `if`. De modus is een enum `GameMode` en geen `bool`, zodat je in `Program` meteen leest welke modus bedoeld is.

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

`StopGame()` werkt op dezelfde manier. Hij gebruikt de onthouden modus om te bepalen of de spelergegevens opgeslagen worden en of de debugconsole gesloten moet worden.

`Program` kent nu alleen nog `GameManager` en `GameMode`:

```csharp
GameManager game = new GameManager();

game.StartGame(GameMode.Normal);
game.StopGame();

game.StartGame(GameMode.Developer);
game.StopGame();
```

### Testresultaat

De eerste versie en de herschreven versie geven precies dezelfde uitvoer:

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

**3. Welke problemen beginnen te ontstaan in je code?**
- **Dubbele code:** NormalMode en DeveloperMode starten bijna op dezelfde manier. Acht van de tien aanroepen zijn gelijk, alleen een paar waarden en de debugconsole verschillen. Bij stoppen is het net zo.
- **`Program` weet te veel:** `Program` kent zes subsystemen, al hun methodes, de juiste waarden en de juiste volgorde. Dat is sterke koppeling.
- **Volgorde kan misgaan:** bij stoppen moet je eerst opslaan en pas daarna uitloggen. Die volgorde staat op meerdere plekken, en op elke plek kun je hem fout doen.
- **Wijzigen is foutgevoelig:** komt er een subsysteem bij, of verandert er een stap, dan moet je elk blok in `Program` aanpassen. Vergeet je er één, dan werkt die modus anders.
- **Te veel verantwoordelijkheden:** `Program` moet de app starten én precies weten hoe de game technisch opstart en afsluit. Dat is in strijd met het **Single Responsibility-principe**.

**4. Hoe zou je die problemen kunnen oplossen?**
- Schrijf de stappen die in beide modi gelijk zijn maar één keer, en laat alleen de verschillen afhangen van de modus.
- Geef één klasse de verantwoordelijkheid voor het starten en stoppen van de game. Die klasse kent de subsystemen en de volgorde.
- `Program` vraagt die klasse alleen nog "start in deze modus" en "stop". Hoe dat gebeurt, hoeft `Program` niet te weten.

**5. Herschrijf de GameApp naar een betere versie.**
Zie **Herschreven versie: `GameManager`** hierboven. `Program` ging van 86 naar 27 regels en kent geen subsystemen meer. Elke stap staat nog maar één keer in de code.

---

## Deel 3 — Facade Pattern

**Opdracht:**

1. Bestudeer de PowerPoint over de Facade Pattern.
2. Nieuwe feature: online-modus en offline-modus. De keuze bepaalt of NetworkService wordt aangeroepen of niet.
3. Zorg (en controleer) dat de GameApp volledig voldoet aan het Facade Design Pattern.
4. Wat zijn de verschillen en overeenkomsten tussen Facade en Adapter?

### Uitwerking

**1. Het Facade Pattern**

De PowerPoint staat niet in deze repo. Deze samenvatting is gebaseerd op hoofdstuk 7 van Head First Design Patterns.

Een facade biedt één eenvoudige interface voor een groep klassen in een subsysteem. De client praat alleen met de facade, en de facade roept de juiste klassen in de juiste volgorde aan. De subsystemen blijven bestaan en zijn nog steeds los te gebruiken. De facade maakt ze alleen makkelijker te gebruiken.

Het pattern volgt het **Principle of Least Knowledge**: praat alleen met je directe vrienden. `Program` hoeft niet meer te weten hoe de game technisch opstart, alleen dát hij moet starten.

**2. Online-modus en offline-modus**

Er is een tweede enum bijgekomen, `ConnectionMode`, met `Online` en `Offline`. `StartGame` krijgt die mee en onthoudt hem, net als de `GameMode`. Alleen bij `Online` wordt `NetworkService` aangeroepen, zowel bij starten (`Connect`, `Login`) als bij stoppen (`Logout`, `Disconnect`):

```csharp
if (connection == ConnectionMode.Online)
{
    network.Connect();
    network.Login();
}
```

De modi zijn los te combineren, dus DeveloperMode kan zowel online als offline. `NetworkService` en de andere subsystemen zijn hiervoor niet aangepast. Alleen de facade en `Program` zijn veranderd.

**3. Voldoet de GameApp aan het Facade Pattern?**

De `GameManager` uit deel 2 was eigenlijk al een facade. In deel 3 is hij daarom alleen nog op twee punten aangepast:

- **Hernoemd naar `GameFacade`**, zodat je aan de naam ziet welk pattern er gebruikt wordt (net als `HomeTheaterFacade` in het boek).
- **Subsystemen via de constructor:** de facade maakt de subsystemen niet meer zelf aan, maar krijgt ze mee. De facade *gebruikt* de subsystemen, maar is er geen eigenaar van. Zo kan de client een subsysteem ook nog direct gebruiken als dat nodig is.

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

| Eis van het Facade Pattern | Voldoet? | Hoe in GameApp |
|---|---|---|
| Eén klasse met een eenvoudige interface voor het subsysteem | ja | `GameFacade` met alleen `StartGame` en `StopGame` |
| De client gebruikt alleen de facade | ja | `Program` roept geen methodes van subsystemen aan, alleen `StartGame` en `StopGame` |
| De facade geeft het werk door aan de subsystemen | ja | elke stap is een aanroep van een subsysteem; de facade bepaalt alleen welke en in welke volgorde |
| De subsystemen kennen de facade niet | ja | geen enkel subsysteem verwijst naar `GameFacade` |
| De subsystemen blijven los bruikbaar | ja | ze zijn `public` en worden via de constructor meegegeven |
| Principle of Least Knowledge | ja | `Program` praat alleen met `GameFacade` |

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

Offline ontbreken de regels van `NetworkService` (verbinden, inloggen, uitloggen, verbinding verbreken). De rest is gelijk aan online.

### Antwoorden op de vragen

**4. Wat zijn de verschillen en overeenkomsten tussen Facade en Adapter?**

Overeenkomsten:
- Allebei zetten ze een klasse tussen de client en bestaande code.
- Allebei laten ze de klassen erachter ongewijzigd: bij Adapter bleef `WhatsAppClient` ongewijzigd, en voor de facade in deel 3 zijn de subsystemen niet aangepast.
- Allebei zorgen ze voor lossere koppeling: de client kent alleen de adapter of de facade.

Verschillen:

| | Adapter | Facade |
|---|---|---|
| Doel | **Passend maken:** een interface omzetten naar de interface die de client verwacht | **Makkelijker maken:** één eenvoudige interface vóór een ingewikkeld subsysteem |
| Aantal ingepakte klassen | meestal één (`WhatsAppClient`) | meerdere (zes subsystemen) |
| Interface | implementeert een bestaande interface (`INotificationService`) | bedenkt een nieuwe, eenvoudigere interface (`StartGame`, `StopGame`) |
| Waarom nodig | de interfaces passen niet op elkaar | de interfaces passen wel, maar het gebruik is te ingewikkeld |

Kort gezegd: een adapter verandert de interface, een facade vereenvoudigt hem.

| Rol in het pattern | Klasse |
|---|---|
| Client | `Program` |
| Facade | `GameFacade` |
| Subsystemen | `GraphicsSystem`, `AudioSystem`, `SaveSystem`, `NetworkService`, `GameEngine`, `DebugConsole` |
