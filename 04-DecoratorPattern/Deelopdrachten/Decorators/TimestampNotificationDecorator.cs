namespace NotificationApp
{
    public class TimestampNotificationDecorator : NotificationDecorator
    {
        public TimestampNotificationDecorator(INotificationService notificationService)
            : base(notificationService)
        {
        }

        public override void Send(string recipient, string message)
        {
            notificationService.Send(recipient, $"[{DateTime.Now:HH:mm}] {message}");
        }
    }
}
