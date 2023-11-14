using System;

namespace Services.Sound
{
    public interface ISoundService
    {
        public bool SoundActivity { get; set; }
        
        public event Action SoundChanged;
        
        public void SwitchSound();
        public void SettingBackgroundMusic();
        public void StopBackgroundMusic();
    }
}