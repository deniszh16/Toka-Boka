using System;
using UnityEngine;
using VContainer;

namespace DZGames.TokaBoka.Services
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundService : MonoBehaviour, ISoundService
    {
        [Header("Фоновая музыка")]
        [SerializeField] private AudioClip[] _audioClips;
        
        [Header("Игровые звуки")]
        [SerializeField] private AudioClip[] _uiSounds;
        
        public bool SoundActivity { get; set; }
        
        public event Action SoundChanged;

        private AudioSource _audioSourceBackgroundMusic;
        private AudioSource _audioSourceSounds;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService; 
        
        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _audioSourceBackgroundMusic = GetComponent<AudioSource>();
            _audioSourceSounds = transform.GetChild(0).GetComponent<AudioSource>();
        }

        public void SwitchSound()
        {
            bool activity = _progressService.GetUserProgress.SettingsData.Sound;
            SoundActivity = !activity;

            _progressService.GetUserProgress.SettingsData.Sound = SoundActivity;
            _saveLoadService.SaveProgress();
            
            SoundChanged?.Invoke();
            SettingBackgroundMusic();
        }

        public void SettingBackgroundMusic()
        {
            if (SoundActivity)
            {
                if (_audioSourceBackgroundMusic.isPlaying == false)
                {
                    int randomMusic = UnityEngine.Random.Range(0, _audioClips.Length);
                    _audioSourceBackgroundMusic.clip = _audioClips[randomMusic];
                    _audioSourceBackgroundMusic.Play();
                }
            }
            else
            {
                _audioSourceBackgroundMusic.Stop();
            }
        }

        public void StopBackgroundMusic() =>
            _audioSourceBackgroundMusic.Stop();

        public void PlaySound(SoundsEnum sound)
        {
            if (SoundActivity)
            {
                _audioSourceSounds.clip = _uiSounds[(int)sound];
                _audioSourceSounds.Play();
            }
        }
    }
}