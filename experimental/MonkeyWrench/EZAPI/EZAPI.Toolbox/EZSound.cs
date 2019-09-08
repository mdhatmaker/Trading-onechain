using System;
using System.Collections.Generic;
using System.Media;
using System.Linq;
using System.Text;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Toolbox
{
    public class EZSound
    {
        private System.Media.SoundPlayer player;
        private string filename;

        public EZSound(string filename)
        {
            this.filename = filename;
        }

        public void Play(bool Loop = false)
        {
            try
            {
                player = new System.Media.SoundPlayer(filename);
                if (Loop == true)
                    player.PlayLooping();
                else
                    player.Play();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceException(ex);
            }
        }

        public void Stop()
        {
            player.Stop();
        }

        // Return the current volume (1 - 100)
        public static int GetVolume()
        {
            // By the default set the volume to 0
            uint CurrVol = 0;
            // At this point, CurrVol gets assigned the volume
            Win32.WaveOutGetVolume(IntPtr.Zero, out CurrVol);
            // Calculate the volume
            ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
            // Get the volume on a scale of 1 to 100
            int volume = CalcVol / (ushort.MaxValue / 100);

            return volume;
        }

        // Set the volume (1 - 100)
        public static void SetVolume(int volume)
        {
            // Calculate the volume that's being set.
            int NewVolume = ((ushort.MaxValue / 100) * volume);
            // Set the same volume for both the left and the right channels
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            // Set the volume
            Win32.WaveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

    } // class



} // namespace
