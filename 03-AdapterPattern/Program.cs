namespace NotificationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NotificationManager manager =
                new NotificationManager(new EmailNotificationService());

            manager.Notify(
                "student@school.nl",
                "Je nieuwe rooster staat klaar."
            );

            manager.SetNotificationService(new SmsNotificationService());
            manager.Notify(
                "0612345678",
                "Je les begint over 15 minuten."
            );

            manager.SetNotificationService(new PushNotificationService());
            manager.Notify(
                "student123",
                "Er staat nieuwe feedback voor je klaar."
            );


            Console.ReadLine();
        }
    }
}
