namespace NotificationApp
{
    public class UrgentNotificationDecorator : NotificationDecorator
    {
        public UrgentNotificationDecorator(INotificationService notificationService)
            : base(notificationService)
        {
        }

        public override void Send(string recipient, string message)
        {
            notificationService.Send(recipient, $"[URGENT] {message}");
        }
    }
}
