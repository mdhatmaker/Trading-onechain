using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace EZAPI.Toolbox
{
    public enum Carrier { CINGULAR = 0, NEXTEL = 1, SPRINT = 3, TMOBILE = 4, VERIZON = 5, VIRGINMOBILE = 6, ATT = 7 }

    public class Messaging
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { set { _emailPassword = value; } }
        public string EmailAddress { get; set; }
        public bool EnableSSL { get; set; }

        private string _emailPassword;

        /// <summary>
        /// This constructor assumes a GMAIL account (so it requires fewer parameters).
        /// </summary>
        /// <param name="emailAddress">gmail email address (this is also your username to login and use gmail smtp)</param>
        /// <param name="emailPassword">password to login to gmail</param>
        public Messaging(string emailAddress, string emailPassword)
        {
            Host = "smtp.gmail.com";
            Port = 587;
            EmailUsername = emailAddress;
            EmailPassword = emailPassword;
            EmailAddress = emailAddress;
            EnableSSL = true;
        }

        public Messaging(string host, int port, string emailUsername, string emailPassword, string emailAddress)
        {
            Host = host;
            Port = port;
            EmailUsername = emailUsername;
            EmailPassword = emailPassword;
            EmailAddress = emailAddress;
            EnableSSL = true;
        }

        private void ShowMailMessage(string msg, bool b)
        {
            Console.WriteLine(msg);
        }

        private void Clear()
        {
        }

        public void SendTextMessage(CellularRecipient recipient, string body, string subject)
        {
            SendMail(recipient.EmailAddress, body, subject);
        }

        public void SendMail(string toAddress, string body, string subject)
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
                ShowMailMessage("Your message was sent successfully.", false);  //  A method to update a ui element with a message
                Clear();
            }
            catch (SmtpException ex)
            {
                ShowMailMessage(string.Format("There was an error sending you message. {0}", ex.Message), true);
            }
        }

    }   // class Messaging

    public class CellularRecipient
    {
        public Carrier Carrier { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public CellularRecipient(string carrier, string phoneNumber)
        {
            Carrier = (Carrier)Enum.Parse(typeof(Carrier), carrier);
            PhoneNumber = phoneNumber;
            EmailAddress = ConvertCellularNumberToEmail(Carrier, PhoneNumber);
        }

        public string ConvertCellularNumberToEmail(Carrier carrier, string phoneNumber)
        {
            string email = null;

            switch (carrier)
            {
                case Carrier.CINGULAR:
                    email = phoneNumber + "@cingularme.com";
                    break;
                case Carrier.NEXTEL:
                    email = phoneNumber + "@messaging.nextel.com";
                    break;
                case Carrier.SPRINT:
                    email = phoneNumber + "@messaging.sprintpcs.com";
                    break;
                case Carrier.TMOBILE:
                    email = phoneNumber + "@tmomail.net";
                    break;
                case Carrier.VERIZON:
                    email = phoneNumber + "@vtext.com";
                    break;
                case Carrier.VIRGINMOBILE:
                    email = phoneNumber + "@vmobl.com";
                    break;
                case Carrier.ATT:
                    email = phoneNumber + "@txt.att.net";
                    break;
            }
            return email;
        }

    }
}   // namespace MiscUtil
