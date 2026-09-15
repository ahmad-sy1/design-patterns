using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationApp
{
    public class PushNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine("PUSH NOTIFICATION");
            Console.WriteLine($"Gebruiker: {recipient}");
            Console.WriteLine($"Bericht: {message}");
            Console.WriteLine();
        }
    }
}
