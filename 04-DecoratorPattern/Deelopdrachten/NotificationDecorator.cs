namespace NotificationApp
{
    public abstract class NotificationDecorator : INotificationService
    {
        protected INotificationService notificationService;

        public NotificationDecorator(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public abstract void Send(string recipient, string message);
    }
}
