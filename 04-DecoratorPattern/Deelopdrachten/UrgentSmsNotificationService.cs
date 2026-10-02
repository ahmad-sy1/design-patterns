namespace NotificationApp
{
    public class UrgentSmsNotificationService : SmsNotificationService
    {
        public override void Send(string recipient, string message)
        {
            base.Send(recipient, $"[URGENT] {message}");
        }
    }
}
