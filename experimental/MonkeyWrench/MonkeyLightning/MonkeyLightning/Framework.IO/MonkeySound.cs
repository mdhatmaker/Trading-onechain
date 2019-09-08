using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using EZAPI.Toolbox.Debug;

namespace MonkeyLightning.Framework.IO
{
    public static class MonkeySound
    {
        private static SoundPlayer soundPlayer;

        public static void PlayErrorSound()
        {
            Spy.Print("playing sound...");

            //soundPlayer = new System.Media.SoundPlayer(@"G:\DATA\SOUNDS\1001 Sound Effects\Video Game Sounds\Arcade Action 04.wav");
            //soundPlayer = new System.Media.SoundPlayer(@"G:\DATA\SOUNDS\random\bye.wav");
            soundPlayer = new SoundPlayer(@"G:\DATA\SOUNDS\converted\ArcadeAction04.wav");
            soundPlayer.Play();

            /*Stream stream = Properties.Resources.ResourceManager.GetStream("ArcadeAction04", Properties.Resources.Culture);
            soundPlayer = new System.Media.SoundPlayer(stream);
            soundPlayer.Play();*/

        }

    } // class
} // namespace
