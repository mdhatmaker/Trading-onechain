using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace EZAPI.Messaging
{
    public enum Carrier { CINGULAR = 0, NEXTEL = 1, SPRINT = 3, TMOBILE = 4, VERIZON = 5, VIRGINMOBILE = 6, ATT = 7 }

    public class TextMessage
    {
        public Email Email { get; set; }
        public Carrier Carrier { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public TextMessage(Email email, string carrier, string phoneNumber)
        {
            Email = email;
            Carrier = (Carrier)Enum.Parse(typeof(Carrier), carrier);
            PhoneNumber = phoneNumber;
            EmailAddress = ConvertCellularNumberToEmail(Carrier, PhoneNumber);
        }

        public void SendTextMessage(string subject, string message)
        {
            Email.SendMail(EmailAddress, subject, message);
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

    } // class
} // namespace
