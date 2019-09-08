using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace EZAPI.Messaging.Speech
{
    public class Synthesizer
    {
        SpeechSynthesizer speaker;
        public Synthesizer()
        {
            speaker = new SpeechSynthesizer();
        }

        public void SynthesizerTest()
        {
            speaker.Rate = 1;
            speaker.Volume = 100;
            speaker.Speak("Hello world.");

            // Save the spoken string to a WAV file.
            // REMEMBER to reset the out device or the next call to speak
            // will try to write to a file...
            speaker.SetOutputToWaveFile(@"c:\temp\soundfile.wav");
            speaker.Speak("Hello world.");
            speaker.SetOutputToDefaultAudioDevice();
        }
    } // class
    
    public class Recognition
    {
        public Recognition()
        {
        }

        public void RecognitionTest()
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {

            }
            catch (InvalidOperationException ex)
            {
                string msg = String.Format("Could not recognize input from default aduio device. Is a microphone or sound card available?\r\n{0} - {1}.", ex.Source, ex.Message);
                Trace.WriteLine(msg);
            }
            finally
            {
                recognizer.UnloadAllGrammars();
            }
        }

    } // class

} // namespace
