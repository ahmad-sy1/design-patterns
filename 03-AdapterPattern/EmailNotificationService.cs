using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationApp
{
    public class EmailNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine("EMAIL");
            Console.WriteLine($"Aan: {recipient}");
            Console.WriteLine($"Bericht: {message}");
            Console.WriteLine();
        }
    }
}
