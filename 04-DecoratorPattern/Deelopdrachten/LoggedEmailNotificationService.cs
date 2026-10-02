namespace NotificationApp
{
    public class LoggedEmailNotificationService : EmailNotificationService
    {
        public override void Send(string recipient, string message)
        {
            Console.WriteLine($"LOG: notificatie naar {recipient}: {message}");
            base.Send(recipient, message);
        }
    }
}
