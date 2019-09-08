using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace EZAPI.Messaging
{
    public delegate void EmailSendResultHandler(string message, bool success);

    public class Email
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { set { _emailPassword = value; } }
        public string EmailAddress { get; set; }
        public bool EnableSSL { get; set; }

        public event EmailSendResultHandler OnEmailSendResult;

        private string _emailPassword;

        /// <summary>
        /// This constructor assumes a GMAIL account (so it requires fewer parameters).
        /// </summary>
        /// <param name="emailAddress">gmail email address (this is also your username to login and use gmail smtp)</param>
        /// <param name="emailPassword">password to login to gmail</param>
        public Email(string emailAddress, string emailPassword)
        {
            Host = "smtp.gmail.com";
            Port = 587;
            EmailUsername = emailAddress;
            EmailPassword = emailPassword;
            EmailAddress = emailAddress;
            EnableSSL = true;
        }

        public Email(string host, int port, string emailUsername, string emailPassword, string emailAddress)
        {
            Host = host;
            Port = port;
            EmailUsername = emailUsername;
            EmailPassword = emailPassword;
            EmailAddress = emailAddress;
            EnableSSL = true;
        }

        public void SendMail(string toAddress, string subject, string body)
        {
            MailMessage msg = new MailMessage(new MailAddress(EmailAddress), new MailAddress(toAddress));    //  Create a MailMessage object with a from and to address
            msg.Subject = subject;  //  Add your subject
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;    //  Add the body of your message
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false; //  Does the body contain html

            SmtpClient client = new SmtpClient(Host, Port); //  Create an instance of SmtpClient with your smtp host and port
            client.Credentials = new NetworkCredential(EmailUsername, _emailPassword); //  Assign your username and password to connect to gmail
            client.EnableSsl = EnableSSL;  //  Enable SSL

            try
            {
                client.Send(msg);   //  Try to send your message
                if (OnEmailSendResult != null) OnEmailSendResult("Your message was sent successfully.", true);
            }
            catch (SmtpException ex)
            {
                if (OnEmailSendResult != null) OnEmailSendResult(string.Format("There was an error sending you message. {0}", ex.Message), false);
            }
        }

    } // class
} // namespace
