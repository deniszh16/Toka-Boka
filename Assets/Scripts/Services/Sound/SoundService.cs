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

        public void PlaySound(Sounds sound, bool overrideSound)
        {
            if (SoundActivity)
            {
                if (_audioSourceSounds.isPlaying && overrideSound == false)
                    return;
                
                _audioSourceSounds.clip = _uiSounds[(int)sound];
                _audioSourceSounds.Play();
            }
        }
    }
}