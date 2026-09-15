using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationApp
{
    public class SmsNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine("SMS");
            Console.WriteLine($"Aan: {recipient}");
            Console.WriteLine($"Bericht: {message}");
            Console.WriteLine();
        }
    }
}
