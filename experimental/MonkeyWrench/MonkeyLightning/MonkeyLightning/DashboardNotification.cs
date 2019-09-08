using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning
{
    public enum NotificationType { ENTER_TRADE, EXIT_TRADE, STOP_TRADE }

    public delegate void NotificationEventHandler(object sender, NotificationEventArgs e);

    public class NotificationEventArgs : EventArgs
    {
        public string Message { get { return message; } }
        public NotificationType MessageType { get { return notificationType; } } 

        private string message;
        private NotificationType notificationType;

        public NotificationEventArgs(NotificationType nType, string msg)
        {
            this.message = msg;
            this.notificationType = nType;
        }
    } // class


} // namespace
