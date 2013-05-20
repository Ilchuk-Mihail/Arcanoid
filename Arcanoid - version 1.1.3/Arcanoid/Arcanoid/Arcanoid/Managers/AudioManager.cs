using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Arcanoid.AudioManager 
{
    static class AudioManager
    {
        private static AudioEngine engine;
        private static SoundBank soundBank;
        private static WaveBank waveBank;
        private static Cue mainTheme;

        static AudioManager()
        {
            engine = new AudioEngine(@"Content\Sounds\Arcanoid.xgs");
            soundBank = new SoundBank(engine, @"Content\Sounds\Sound Bank.xsb");
            waveBank = new WaveBank(engine, @"Content\Sounds\Wave Bank.xwb");
        }
        public static void Update()
        {
            engine.Update();
        }
    /*    public static void PlayShootSound()
        {
            soundBank.PlayCue("fire");
        }*/
        public static void StartMenuMusic()
        {
            if (mainTheme == null)
            {
                mainTheme = soundBank.GetCue("Bill Psarras - Feel the same");
                mainTheme.Play();
            }    
        }

        public static void StopMenuMusic()
        {
            if (mainTheme != null)
            {
                mainTheme.Stop(AudioStopOptions.Immediate);
                mainTheme = null;
            }
        }
    }
}
