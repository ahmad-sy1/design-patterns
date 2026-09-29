namespace NotificationApp
{

    public class NotificationManager
    {
        public void Notify(INotificationService notificationService, string recipient, string message)
        {
            notificationService.Send(recipient, message);
        }
    }
}
