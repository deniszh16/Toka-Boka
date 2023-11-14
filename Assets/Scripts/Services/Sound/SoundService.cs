using System;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Services.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundService : MonoBehaviour, ISoundService
    {
        [Header("Фоновая музыка")]
        [SerializeField] private AudioClip[] _audioClips;
        
        public bool SoundActivity { get; set; }
        
        public event Action SoundChanged;

        private AudioSource _audioSource;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake() =>
            _audioSource = GetComponent<AudioSource>();
        
        public void SwitchSound()
        {
            bool activity = _progressService.UserProgress.Sound;
            SoundActivity = !activity;

            _progressService.UserProgress.Sound = SoundActivity;
            _saveLoadService.SaveProgress();
            
            SoundChanged?.Invoke();
            SettingBackgroundMusic();
        }

        public void SettingBackgroundMusic()
        {
            if (SoundActivity)
            {
                if (_audioSource.isPlaying == false)
                {
                    int randomMusic = UnityEngine.Random.Range(0, _audioClips.Length);
                    _audioSource.clip = _audioClips[randomMusic];
                    _audioSource.Play();
                }
            }
            else
            {
                _audioSource.Stop();
            }
        }

        public void StopBackgroundMusic() =>
            _audioSource.Stop();
    }
}