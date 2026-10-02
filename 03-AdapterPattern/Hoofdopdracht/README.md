# Adapter Pattern — Hoofdopdracht (Ducks & Turkeys)

Officiële Teams-opdracht. De startcode is aangeleverd door de docent (`007 - AdapterPattern.zip`), gebaseerd op het Duck/Turkey-voorbeeld uit hoofdstuk 7 van Head First Design Patterns.

## Structuur

```
03-AdapterPattern/Hoofdopdracht/
├── Interfaces/
│   ├── Duck.cs              // doel-interface (Target): Fly(), Quack()
│   ├── Turkey.cs            // interface van de Adaptee: Gobble(), Fly()
│   ├── TurkeyAdapter.cs     // voorbeeld-adapter: laat een Turkey zich gedragen als Duck
│   ├── Goose.cs             // interface van de Adaptee: Fly(), Honk()
│   └── GooseAdapter.cs      // adapter: laat een Goose zich gedragen als Duck
├── Ducks/
│   └── MallardDuck.cs
├── Turkeys/
│   └── WildTurkey.cs
├── Geese/
│   └── CanadaGoose.cs
├── AdapterPattern.csproj
└── Program.cs
```

---

## Opdracht

1. Maak een interface `Goose` met de methodes `Fly()` en `Honk()`.
2. Maak een class `CanadaGoose` die de interface `Goose` implementeert.
3. Maak een `GooseAdapter`, zodat een `Goose` gebruikt kan worden als `Duck`.

### Uitwerking

| Rol in het pattern | Class / interface |
|--------------------|-------------------|
| Target  | `Duck` |
| Adaptee | `Goose` (concreet: `CanadaGoose`) |
| Adapter | `GooseAdapter` |
| Client  | `Program.TestDuck(Duck duck)` |

`GooseAdapter` implementeert `Duck` en krijgt een `Goose` binnen via de constructor (compositie, object adapter). De adapter vertaalt de aanroepen:

| `Duck`-methode | wordt in `GooseAdapter` |
|----------------|-------------------------|
| `Quack()` | `goose.Honk()` |
| `Fly()`   | `goose.Fly()` |

In tegenstelling tot de `TurkeyAdapter`, die `Fly()` vijf keer aanroept omdat een kalkoen maar korte stukjes vliegt, hoeft dat bij een gans niet: een gans vliegt zelf al lange afstanden. Daarom stuurt `Fly()` gewoon één keer door.

`TestDuck()` in `Program.cs` hoeft niet aangepast te worden. De client werkt alleen met `Duck` en weet niet dat er een gans achter zit.

#### Uitvoer

```
The Goose says...
Honk honk
I'm flying a long distance in V-formation
The GooseAdapter says...
Honk honk
I'm flying a long distance in V-formation
```
