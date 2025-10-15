using System.Media;
using System.Windows.Media;
using TownBuilder.Core;

namespace TownBuilder.Helppers
{
    public static class SoundController
    {
        internal static MediaPlayer BackgroundMusic = new();
        internal static SoundPlayer[] Sounds = new SoundPlayer[3];
        internal static bool Mute = false;
        private static void LoadSounds()
        {
            Sounds[(int)SoundsTipos.Pay] = new SoundPlayer("Resources/Sounds/pay.wav");
            Sounds[(int)SoundsTipos.Card] = new SoundPlayer("Resources/Sounds/card.wav");
            Sounds[(int)SoundsTipos.Destruir] = new SoundPlayer("Resources/Sounds/destroy.wav");
            foreach (var sound in Sounds)
            {
                sound.Load();
            }
        }
        private static void LoadMusic()
        {
            BackgroundMusic.Open(new Uri("Resources/Sounds/farm.wav", UriKind.Relative));
            BackgroundMusic.MediaEnded += BackgroundMusic_Ended;
            Replay();
        }
        private static void BackgroundMusic_Ended(object? sender, EventArgs e)
        {
            BackgroundMusic.Position = TimeSpan.Zero;
            Replay();
        }
        public static void Load()
        {
            LoadSounds();
            LoadMusic();
        }
        public static void Play(SoundsTipos tipo)
        {
            new Thread(() => { Sounds[(int)tipo].Play(); }).Start();
        }
        public static void Replay()
        {
            if (Mute) return;
            BackgroundMusic.Play();
        }
        public static void Pause()
        {
            BackgroundMusic.Pause();
            BackgroundMusic.Stop();
        }
    }
}
