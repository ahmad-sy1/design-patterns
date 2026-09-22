using ExternalWhatsAppLibrary;

namespace NotificationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            INotificationService emailService =
                new EmailNotificationService();

            INotificationService smsService =
                new SmsNotificationService();

            INotificationService pushService =
                new PushNotificationService();

            INotificationService whatsAppService =
                new WhatsAppNotificationAdapter(new WhatsAppClient(), true);

            NotificationManager manager = new NotificationManager();


            manager.Notify(
                emailService,
                "student@school.nl",
                "Je nieuwe rooster staat klaar."
            );

            manager.Notify(
                smsService,
                "0612345678",
                "Je les begint over 15 minuten."
            );

            manager.Notify(
                pushService,
                "student123",
                "Er staat nieuwe feedback voor je klaar."
            );

            manager.Notify(
                whatsAppService,
                "0612345678",
                "Je toets is verplaatst naar morgen."
            );


            Console.ReadLine();
        }
    }
}
