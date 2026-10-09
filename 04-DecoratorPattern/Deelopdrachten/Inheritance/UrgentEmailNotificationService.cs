namespace NotificationApp
{
    public class UrgentEmailNotificationService : EmailNotificationService
    {
        public override void Send(string recipient, string message)
        {
            base.Send(recipient, $"[URGENT] {message}");
        }
    }
}
