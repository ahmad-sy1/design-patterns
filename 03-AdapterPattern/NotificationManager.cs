namespace NotificationApp
{

    public class NotificationManager
    {
        private INotificationService _notificationService;

        public NotificationManager(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void SetNotificationService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Notify(string recipient, string message)
        {
            _notificationService.Send(recipient, message);
        }
    }
}
