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


            emailService.Send(
                "student@school.nl",
                "Je nieuwe rooster staat klaar."
            );

            smsService.Send(
                "0612345678",
                "Je les begint over 15 minuten."
            );

            pushService.Send(
                "student123",
                "Er staat nieuwe feedback voor je klaar."
            );


            Console.ReadLine();
        }
    }
}