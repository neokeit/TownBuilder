using System.Media;
using TownBuilder.Core;
using System.Windows.Media;

namespace TownBuilder.Helppers
{
    public class SoundController
    {
        #region propierty
        private MediaPlayer BackgroundMusic = new();
        private SoundPlayer[] Sounds = new SoundPlayer[3];
        #endregion propierty

        public bool Mute = false;
        public SoundController()
        {
            LoadSounds();
            LoadMusic();
        }
        
        private void LoadSounds()
        {
            Sounds[(int)SoundsTipos.Pay] = new SoundPlayer("Resources/Sounds/pay.wav");
            Sounds[(int)SoundsTipos.Card] = new SoundPlayer("Resources/Sounds/card.wav");
            Sounds[(int)SoundsTipos.Destruir] = new SoundPlayer("Resources/Sounds/destroy.wav");
            foreach (var sound in Sounds)
            {
                sound.Load();
            }
        }
        private void LoadMusic()
        {
            BackgroundMusic.Open(new Uri("Resources/Sounds/farm.wav", UriKind.Relative));
            BackgroundMusic.MediaEnded += BackgroundMusic_Ended;
            PauseMusic();
        }
        private void BackgroundMusic_Ended(object? sender, EventArgs e)
        {
            BackgroundMusic.Position = TimeSpan.Zero;
            PauseMusic();
        }

        public void Play(SoundsTipos tipo)
        {
            new Thread(() => { Sounds[(int)tipo].Play(); }).Start();
        }
        public void PlayMusic()
        {
            if (Mute) return;
            BackgroundMusic.Play();
        }
        public void PauseMusic()
        {
            BackgroundMusic.Pause();
            BackgroundMusic.Stop();
        }
    }
}
