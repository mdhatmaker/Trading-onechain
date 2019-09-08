using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SKYPE4COMLib;

namespace EZAPI.Messaging
{
    public class SkypeEZ
    {
        /*
        Skype skype;

        public SkypeEZ()
        {
            skype = new Skype();
        }

        public void SearchForUsers(string username)
        {
            IUserCollection iusercollection = skype.SearchForUsers(username);
            if (iusercollection.Count > 0)
            {
                Console.WriteLine(iusercollection[0].FullName);
            }
        }

        public void SkypeEZTest()
        {
            if (!skype.Client.IsRunning)
            {
                // start minimized with no splash screen
                skype.Client.Start(true, true);
            }

            // wait for the client to be connected and ready
            skype.Attach(6, true);

            // access skype objects
            Console.WriteLine("Missed message count: {0}", skype.MissedMessages.Count);

            // do some stuff
            Console.WriteLine("Enter a skype name to search for: ");
            string username = Console.ReadLine();
            foreach (User user in skype.SearchForUsers(username))
            {
                Console.WriteLine(user.FullName);
            }

            Console.WriteLine("Say hello to: ");
            username = Console.ReadLine();
            skype.SendMessage(username, "Hello World");
        }
        */
    } // class
} // namespace
