namespace NotificationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Opdracht 1 ===");
            INotificationService notification = new EmailNotificationService();
            notification.Send("student@school.nl", "Je rooster is gewijzigd.");

            notification = new SmsNotificationService();
            notification.Send("0612345678", "Je rooster is gewijzigd.");


            Console.WriteLine("=== Opdracht 2 ===");
            INotificationService loggedEmail = new LoggedEmailNotificationService();
            loggedEmail.Send("student@school.nl", "Je rooster is gewijzigd.");

            INotificationService loggedSms = new LoggedSmsNotificationService();
            loggedSms.Send("0612345678", "Je rooster is gewijzigd.");


            Console.WriteLine("=== Opdracht 3 ===");
            INotificationService urgentEmail = new UrgentEmailNotificationService();
            urgentEmail.Send("student@school.nl", "De les van vandaag vervalt.");

            INotificationService urgentSms = new UrgentSmsNotificationService();
            urgentSms.Send("0612345678", "De les van vandaag vervalt.");


            Console.WriteLine("=== Opdracht 6 ===");
            INotificationService loggingEmail =
                new LoggingNotificationDecorator(new EmailNotificationService());
            loggingEmail.Send("student@school.nl", "Je rooster is gewijzigd.");

            INotificationService loggingSms =
                new LoggingNotificationDecorator(new SmsNotificationService());
            loggingSms.Send("0612345678", "Je rooster is gewijzigd.");


            Console.WriteLine("=== Opdracht 7 ===");
            INotificationService urgentDecoratedEmail =
                new UrgentNotificationDecorator(new EmailNotificationService());
            urgentDecoratedEmail.Send("student@school.nl", "De les van vandaag vervalt.");


            Console.WriteLine("=== Opdracht 8/9 variant A ===");
            INotificationService variantA =
                new LoggingNotificationDecorator(
                    new UrgentNotificationDecorator(
                        new EmailNotificationService()));
            variantA.Send("student@school.nl", "De toets begint over 10 minuten.");

            Console.WriteLine("=== Opdracht 9 variant B ===");
            INotificationService variantB =
                new UrgentNotificationDecorator(
                    new LoggingNotificationDecorator(
                        new EmailNotificationService()));
            variantB.Send("student@school.nl", "De toets begint over 10 minuten.");


            Console.WriteLine("=== Opdracht 10 Email ===");
            INotificationService timestampEmail =
                new LoggingNotificationDecorator(
                    new TimestampNotificationDecorator(
                        new UrgentNotificationDecorator(
                            new EmailNotificationService())));
            timestampEmail.Send("student@school.nl", "Het lokaal is gewijzigd.");

            Console.WriteLine("=== Opdracht 10 Sms / Eindopdracht ===");
            INotificationService eindopdracht =
                new LoggingNotificationDecorator(
                    new TimestampNotificationDecorator(
                        new UrgentNotificationDecorator(
                            new SmsNotificationService())));
            eindopdracht.Send("0612345678", "Het lokaal is gewijzigd.");


            Console.ReadLine();
        }
    }
}
