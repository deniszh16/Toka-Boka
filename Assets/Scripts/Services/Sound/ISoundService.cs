using System;

namespace DZGames.TokaBoka.Services
{
    public interface ISoundService
    {
        public bool SoundActivity { get; set; }
        
        public event Action SoundChanged;
        
        public void SwitchSound();
        public void SettingBackgroundMusic();
        public void StopBackgroundMusic();
        public void PlaySound(SoundsEnum sound);
    }
}