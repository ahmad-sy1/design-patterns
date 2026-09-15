using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationApp
{
    public interface INotificationService
    {
        void Send(string recipient, string message);
    }
}
