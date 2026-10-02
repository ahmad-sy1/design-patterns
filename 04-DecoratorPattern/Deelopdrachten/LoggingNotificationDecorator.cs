namespace NotificationApp
{
    public class LoggingNotificationDecorator : NotificationDecorator
    {
        public LoggingNotificationDecorator(INotificationService notificationService)
            : base(notificationService)
        {
        }

        public override void Send(string recipient, string message)
        {
            // TODO Opdracht 6: voeg hier logging toe
            // TODO Opdracht 6: geef daarna het versturen door aan notificationService
        }
    }
}
