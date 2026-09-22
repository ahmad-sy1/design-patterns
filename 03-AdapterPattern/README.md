# Adapter Pattern — NotificationApp

Console-applicatie voor het versturen van notificaties aan gebruikers (email, sms, push en WhatsApp).

## Structuur

```
03-AdapterPattern/
├── INotificationService.cs            // doel-interface (Target)
├── EmailNotificationService.cs
├── SmsNotificationService.cs
├── PushNotificationService.cs
├── NotificationManager.cs             // client
├── WhatsAppNotificationAdapter.cs     // adapter
├── ExternalWhatsAppLibrary/
│   └── WhatsAppClient.cs              // externe library (Adaptee)
└── Program.cs
```

---

## Startopdracht — deel 1

**Opdracht:** maak een `NotificationManager` die als client voor de notificaties werkt, en pas `Program.cs` aan zodat die via de manager werkt. In dit deel gebruik je nog geen Adapter Pattern.

```
                 <<interface>>
              INotificationService
                       |
        +--------------+--------------+
        |              |              |
EmailNotification SmsNotification PushNotification


NotificationManager
        |
        v
INotificationService
```

**Uitwerking:**
- `NotificationManager` kent alleen de interface `INotificationService`, geen concrete klassen.
- De manager bewaart geen service en beslist niets. Hij krijgt de service mee en geeft het bericht door:
  ```csharp
  public void Notify(INotificationService notificationService, string recipient, string message)
  {
      notificationService.Send(recipient, message);
  }
  ```
- `Program` maakt de services aan en kiest per bericht welke service gebruikt wordt.

**Verwerkte feedback:** in de eerste versie deed de manager te veel: hij bewaarde de service en had een `SetNotificationService`-methode. Dat is weggehaald. Program handelt nu meer af en de manager geeft alleen door.

---

## Startopdracht — deel 2

**Opdracht:** de opdrachtgever wil ook WhatsApp-notificaties versturen. Daarvoor is een externe library gekocht:

```csharp
namespace ExternalWhatsAppLibrary
{
    public class WhatsAppClient
    {
        public void SendWhatsAppMessage(string phoneNumber, string text, bool urgent) { ... }
    }
}
```

1. Zorg dat deze externe `WhatsAppClient` gebruikt kan worden door de bestaande `NotificationManager`.
2. Test de app opnieuw. Wat concludeer je? Kunnen we `WhatsAppClient` aanpassen? Of kunnen we `INotificationService` aanpassen? Waarom wel of niet?

### Uitwerking

`WhatsAppClient` past niet op `INotificationService`:

| | `INotificationService` | `WhatsAppClient` |
|---|---|---|
| Methode | `Send` | `SendWhatsAppMessage` |
| Parameters | `recipient`, `message` | `phoneNumber`, `text`, `urgent` |
| Implementeert interface | — | nee |

Daarom is er een **adapter** toegevoegd: `WhatsAppNotificationAdapter`. Die implementeert `INotificationService` en zet de aanroep door naar de `WhatsAppClient`:

```csharp
public class WhatsAppNotificationAdapter : INotificationService
{
    private readonly WhatsAppClient _whatsAppClient;
    private readonly bool _urgent;

    public WhatsAppNotificationAdapter(WhatsAppClient whatsAppClient, bool urgent)
    {
        _whatsAppClient = whatsAppClient;
        _urgent = urgent;
    }

    public void Send(string recipient, string message)
    {
        _whatsAppClient.SendWhatsAppMessage(recipient, message, _urgent);
        Console.WriteLine();
    }
}
```

Onze interface kent geen `urgent`, dus die waarde wordt via de constructor van de adapter meegegeven.

In `Program` wordt de adapter net zo gebruikt als de andere services:

```csharp
INotificationService whatsAppService =
    new WhatsAppNotificationAdapter(new WhatsAppClient(), true);

manager.Notify(whatsAppService, "0612345678", "Je toets is verplaatst naar morgen.");
```

```
                 <<interface>>
              INotificationService  <-----------  NotificationManager
                       |
     +---------+-------+--------+-----------------------+
     |         |                |                       |
   Email      Sms             Push       WhatsAppNotificationAdapter
                                                        |
                                                        v
                                         WhatsAppClient (extern)
```

### Testresultaat

```
EMAIL
Aan: student@school.nl
Bericht: Je nieuwe rooster staat klaar.

SMS
Aan: 0612345678
Bericht: Je les begint over 15 minuten.

PUSH NOTIFICATION
Gebruiker: student123
Bericht: Er staat nieuwe feedback voor je klaar.

EXTERNE WHATSAPP SERVICE
Telefoonnummer: 0612345678
Tekst: Je toets is verplaatst naar morgen.
Urgent: True
```

### Antwoorden op de vragen

**Wat concludeer je?**
De externe `WhatsAppClient` kan niet direct door de `NotificationManager` gebruikt worden, omdat hij een andere interface heeft. Met een adapter ertussen werkt het wel. `NotificationManager`, `INotificationService` en de bestaande services hoefden daarvoor niet aangepast te worden. Er is alleen een nieuwe klasse bijgekomen.

**Kunnen we `WhatsAppClient` aanpassen?**
Nee. Het is een gekochte externe library. Meestal hebben we de broncode niet, en als we die wel hebben, zijn we onze wijzigingen kwijt bij elke update van de leverancier. Externe code pas je niet zelf aan.

**Kunnen we `INotificationService` aanpassen?**
Technisch wel, maar het is geen goed idee. Email, Sms, Push en de `NotificationManager` gebruiken deze interface allemaal. Als je bijvoorbeeld de parameter `urgent` toevoegt, moeten al die klassen mee veranderen voor één externe library. Dat is in strijd met het **Open/Closed-principe**: code moet open zijn voor uitbreiding en gesloten voor wijziging.

**Conclusie:** gebruik het **Adapter Pattern**. Een adapter zet de interface van een bestaande klasse (de Adaptee: `WhatsAppClient`) om naar de interface die de client verwacht (de Target: `INotificationService`). Zo kunnen klassen die niet op elkaar passen toch samenwerken, zonder bestaande code te wijzigen.

| Rol in het pattern | Klasse |
|---|---|
| Client | `NotificationManager` |
| Target | `INotificationService` |
| Adapter | `WhatsAppNotificationAdapter` |
| Adaptee | `WhatsAppClient` |
