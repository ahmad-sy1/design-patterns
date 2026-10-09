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
└── Program.cs
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

_nog in te vullen_

---

## Deel 3 — Facade Pattern

**Opdracht:**

1. Bestudeer de PowerPoint over de Facade Pattern.
2. Nieuwe feature: online-modus en offline-modus. De keuze bepaalt of NetworkService wordt aangeroepen of niet.
3. Zorg (en controleer) dat de GameApp volledig voldoet aan het Facade Design Pattern.
4. Wat zijn de verschillen en overeenkomsten tussen Facade en Adapter?

### Uitwerking

_nog in te vullen_
