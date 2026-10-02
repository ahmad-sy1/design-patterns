namespace NotificationApp
{
    public class SmsNotificationService : INotificationService
    {
        public virtual void Send(string recipient, string message)
        {
            Console.WriteLine("SMS");
            Console.WriteLine($"Aan: {recipient}");
            Console.WriteLine($"Bericht: {message}");
            Console.WriteLine();
        }
    }
}
