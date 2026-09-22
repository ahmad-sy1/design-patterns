using ExternalWhatsAppLibrary;

namespace NotificationApp
{
    public class WhatsAppNotificationAdapter : INotificationService
    {
        private readonly WhatsAppClient _whatsAppClient;
        private readonly bool _urgent;

        public WhatsAppNotificationAdapter(WhatsAppClient whatsAppClient, bool urgent)
        {
            _whatsAppClient = whatsAppClient;
            _urgent = urgent;
        }

        public void Send(string recipient, string message)
        {
            _whatsAppClient.SendWhatsAppMessage(recipient, message, _urgent);
            Console.WriteLine();
        }
    }
}
