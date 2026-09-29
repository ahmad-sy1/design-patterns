# Adapter Pattern — Hoofdopdracht (Ducks & Turkeys)

Officiële Teams-opdracht. De startcode is aangeleverd door de docent (`007 - AdapterPattern.zip`), gebaseerd op het Duck/Turkey-voorbeeld uit hoofdstuk 7 van Head First Design Patterns.

## Structuur

```
03-AdapterPattern/Hoofdopdracht/
├── Interfaces/
│   ├── Duck.cs              // doel-interface (Target): Fly(), Quack()
│   ├── Turkey.cs            // interface van de Adaptee: Gobble(), Fly()
│   └── TurkeyAdapter.cs     // voorbeeld-adapter: laat een Turkey zich gedragen als Duck
├── Ducks/
│   └── MallardDuck.cs
├── Turkeys/
│   └── WildTurkey.cs
├── AdapterPattern.csproj
└── Program.cs
```

---

## Opdracht

1. Maak een interface `Goose` met de methodes `Fly()` en `Honk()`.
2. Maak een class `CanadaGoose` die de interface `Goose` implementeert.
3. Maak een `GooseAdapter`, zodat een `Goose` gebruikt kan worden als `Duck`.

### Uitwerking

_Nog in te vullen._
