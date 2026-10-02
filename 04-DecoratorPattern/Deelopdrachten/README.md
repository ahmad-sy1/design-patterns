# Decorator Pattern — NotificationApp

Console-applicatie voor het versturen van notificaties (email en sms), uit te breiden met decorators.

## Structuur

```
04-DecoratorPattern/Deelopdrachten/
├── INotificationService.cs              // component-interface
├── EmailNotificationService.cs          // concreet component
├── SmsNotificationService.cs            // concreet component
├── LoggedEmailNotificationService.cs    // opdracht 2 (inheritance)
├── LoggedSmsNotificationService.cs      // opdracht 2 (inheritance)
├── UrgentEmailNotificationService.cs    // opdracht 3 (inheritance)
├── UrgentSmsNotificationService.cs      // opdracht 3 (inheritance)
├── NotificationDecorator.cs             // abstracte decorator
├── LoggingNotificationDecorator.cs      // concrete decorator
└── Program.cs
```

---

## Opdracht 1 — Onderzoeken

Voer de app uit en beantwoord vragen over INotificationService: waarom de variabele beide services kan bevatten, welke interface en methode ze delen, en het voordeel van programmeren tegen de interface.

**Uitwerking:**

**Waarom kan de variabele beide services bevatten?**
De variabele `notification` is van het type `INotificationService`. Elke klasse die `INotificationService` implementeert, moet de methode `Send` hebben en past daarom in die variabele. `EmailNotificationService` en `SmsNotificationService` implementeren allebei die interface. Ze doen niet precies hetzelfde (de uitvoer is anders), maar ze houden zich aan dezelfde afspraak.

**Welke interface en methode delen ze?**
Ze delen de interface `INotificationService` met de methode:

```csharp
void Send(string recipient, string message);
```

**Wat is het voordeel van programmeren tegen de interface?**
`Program` kent alleen de interface en niet de concrete klasse. Je kunt de service dus verwisselen (Email, Sms of later bijvoorbeeld Push) zonder de code die `Send` aanroept aan te passen. Alleen de regel met `new` verandert. Dat heet losse koppeling.

---

## Opdracht 2 — Logging met inheritance

Maak LoggedEmailNotificationService als subclass van EmailNotificationService die vóór het versturen een logregel toont. Maak daarna ook LoggedSmsNotificationService. Test beide.

**Uitwerking:**

Om `Send` in een subclass te kunnen overschrijven, moet hij in de base class `virtual` zijn. Daarom is `Send` in `EmailNotificationService` en `SmsNotificationService` `public virtual void Send(...)` geworden. In Java is dat niet nodig, want daar zijn methodes standaard te overschrijven.

```csharp
public class LoggedEmailNotificationService : EmailNotificationService
{
    public override void Send(string recipient, string message)
    {
        Console.WriteLine($"LOG: notificatie naar {recipient}: {message}");
        base.Send(recipient, message);
    }
}
```

`LoggedSmsNotificationService` is precies hetzelfde, alleen erft hij van `SmsNotificationService`. De logcode staat nu dus twee keer in de code.

**Testresultaat:**

```
LOG: notificatie naar student@school.nl: Je rooster is gewijzigd.
EMAIL
Aan: student@school.nl
Bericht: Je rooster is gewijzigd.

LOG: notificatie naar 0612345678: Je rooster is gewijzigd.
SMS
Aan: 0612345678
Bericht: Je rooster is gewijzigd.
```

---

## Opdracht 3 — Urgent notifications

Maak UrgentEmailNotificationService en UrgentSmsNotificationService (inheritance) die [URGENT] voor het bericht plaatsen. Test beide.

**Uitwerking:**

```csharp
public class UrgentEmailNotificationService : EmailNotificationService
{
    public override void Send(string recipient, string message)
    {
        base.Send(recipient, $"[URGENT] {message}");
    }
}
```

`UrgentSmsNotificationService` werkt op dezelfde manier, maar erft van `SmsNotificationService`. Ook hier is de code weer dubbel.

**Testresultaat:**

```
EMAIL
Aan: student@school.nl
Bericht: [URGENT] De les van vandaag vervalt.

SMS
Aan: 0612345678
Bericht: [URGENT] De les van vandaag vervalt.
```

---

## Opdracht 4 — Denk eerst na

Geen code. Beantwoord vragen over het aantal subclasses bij Logged + Urgent, wat er gebeurt als Timestamp erbij komt, en wat het probleem is van deze aanpak (class explosion).

**Uitwerking:**

**Hoeveel subclasses heb je nodig voor Logged + Urgent?**
Per service heb je Logged, Urgent en LoggedUrgent nodig, dus 3 subclasses. Met Email en Sms zijn dat **2 × 3 = 6 subclasses**.

**Wat gebeurt er als Timestamp erbij komt?**
Elke extra kan aan of uit staan. Met 3 extra's zijn er 2³ − 1 = 7 combinaties per service (L, U, T, LU, LT, UT, LUT). Met Email en Sms zijn dat **2 × 7 = 14 subclasses**. Bij elke nieuwe extra verdubbelt het aantal ongeveer, en elke nieuwe service (bijvoorbeeld Push) krijgt weer al die combinaties.

| Extra's | Subclasses per service | Email + Sms |
|---|---|---|
| Logged, Urgent | 3 | 6 |
| Logged, Urgent, Timestamp | 7 | 14 |

**Wat is het probleem van deze aanpak?**
- **Class explosion:** het aantal klassen groeit heel snel.
- **Dubbele code:** de logcode en de `[URGENT]`-code staan in meerdere klassen. Bij een wijziging moet je ze allemaal aanpassen.
- **Vast tijdens compileren:** inheritance ligt vast in de code. Je kunt niet tijdens het draaien kiezen welke extra's je wilt.

---

## Opdracht 5 — Begrijpen

Beantwoord vragen over de abstracte NotificationDecorator: waarom hij INotificationService implementeert, waarom hij een field van dat type heeft, waarom de interface en niet EmailNotificationService, en het verschil tussen IS-A en HAS-A.

**Uitwerking:** _nog in te vullen_

---

## Opdracht 6 — Maak LoggingDecorator af

Maak LoggingNotificationDecorator.Send() af, test met Email en daarna met SMS. Vraag: moest de decorator aangepast worden voor SMS? Waarom wel of niet?

**Uitwerking:** _nog in te vullen_

---

## Opdracht 7 — UrgentNotificationDecorator

Maak een decorator die [URGENT] voor het bericht plaatst en doorstuurt naar het ingepakte object. INotificationService, EmailNotificationService en SmsNotificationService mogen niet aangepast worden.

**Uitwerking:** _nog in te vullen_

---

## Opdracht 8 — Volg de keten

Teken welke objecten elkaar aanroepen bij Logging → Urgent → Email en beantwoord welke Send() eerst en daarna wordt uitgevoerd, welke class de echte e-mail verstuurt, en waarom een decorator een andere decorator kan bevatten.

**Uitwerking:** _nog in te vullen_

---

## Opdracht 9 — Maakt de volgorde uit?

Voer variant A (Logging → Urgent → Email) en variant B (Urgent → Logging → Email) uit, teken beide aanroepvolgordes en leg uit of en waarom de volgorde de uitvoer beïnvloedt.

**Uitwerking:** _nog in te vullen_

---

## Opdracht 10 — TimestampNotificationDecorator

Maak een decorator die de huidige tijd [HH:mm] voor het bericht zet. Test de combinatie Logging → Timestamp → Urgent → Email en daarna dezelfde decorators met SMS.

**Uitwerking:** _nog in te vullen_

---

## Opdracht 11 — Vergelijken

Vergelijk decorators met één Send-methode met boolean parameters (urgent, logging, timestamp): leesbaarheid van true, true, false, wat er gebeurt bij Encryption/Retry/AuditLogging, welke class te veel verantwoordelijkheden krijgt, en wat overzichtelijker is.

**Uitwerking:** _nog in te vullen_

---

## Pattern Detective

Bepaal voor situatie A t/m E of Adapter, Facade of Decorator het beste past.

**Uitwerking:** _nog in te vullen_

---

## Adapter, Facade en Decorator

Vul de tabel in (probleem + onthoudzin) met de begrippen passend maken, makkelijker maken en uitbreiden.

**Uitwerking:** _nog in te vullen_

---

## Eindopdracht

Bouw een notificatie die via SMS gaat, urgent is, een timestamp bevat en gelogd wordt, zonder bestaande classes aan te passen. Laat de objectstructuur als schema zien met de echte classnamen.

**Uitwerking:** _nog in te vullen_
