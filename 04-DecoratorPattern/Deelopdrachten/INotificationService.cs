namespace NotificationApp
{
    public interface INotificationService
    {
        void Send(string recipient, string message);
    }
}
