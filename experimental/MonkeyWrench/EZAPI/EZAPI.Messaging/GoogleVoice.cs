using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpVoice;

namespace EZAPI.Messaging
{
    public class GoogleVoice
    {
        Voice voice;

        public GoogleVoice(string email, string password)
        {
            // Create the Voice object with your Google Voice email and password.
            //string email = "email@domain.com";
            //string password = "password";
            voice = new Voice(email, password);
        }

        public void GoogleVoiceTest()
        {
            // Send SMS text message.
            voice.SendSMS("3126234015", "this is a test of SharpVoice");
            
            // Write SMS text messages to the console.
            foreach (Message msg in voice.SMS.Messages)
            {
                Console.WriteLine("[" + msg.type + "] " + msg.displayNumber + ": " + msg.Text);
            }

            string st = voice.Call("2194623737", "3126234015", "7146994015");
            Console.WriteLine(st);

            foreach (Message msg in voice.Inbox.Messages)
            {
                Console.WriteLine("[" + msg.type + "] " + msg.displayNumber + ": " + msg.Duration.ToString() + " second(s)");
            }

            Phone phone = new Phone(voice, "");
        }
        
    } // class
} // namespace
