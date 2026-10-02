namespace NotificationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Opdracht 1
            Console.WriteLine("=== Opdracht 1 ===");
            INotificationService notification = new EmailNotificationService();
            notification.Send("student@school.nl", "Je rooster is gewijzigd.");

            notification = new SmsNotificationService();
            notification.Send("0612345678", "Je rooster is gewijzigd.");


            // Opdracht 2 - logging met inheritance
            Console.WriteLine("=== Opdracht 2 ===");
            INotificationService loggedEmail = new LoggedEmailNotificationService();
            loggedEmail.Send("student@school.nl", "Je rooster is gewijzigd.");

            INotificationService loggedSms = new LoggedSmsNotificationService();
            loggedSms.Send("0612345678", "Je rooster is gewijzigd.");


            // Opdracht 3 - urgent met inheritance
            Console.WriteLine("=== Opdracht 3 ===");
            INotificationService urgentEmail = new UrgentEmailNotificationService();
            urgentEmail.Send("student@school.nl", "De les van vandaag vervalt.");

            INotificationService urgentSms = new UrgentSmsNotificationService();
            urgentSms.Send("0612345678", "De les van vandaag vervalt.");


            Console.ReadLine();
        }
    }
}
