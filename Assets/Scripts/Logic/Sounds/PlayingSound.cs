using Services.Sound;
using UnityEngine;
using Zenject;

namespace Logic.Sounds
{
    public class PlayingSound : MonoBehaviour
    {
        [Header("Автовоспроизведение звука")]
        [SerializeField] private bool _autoplay;
        
        [Header("Звук объекта")]
        [SerializeField] private Services.Sound.Sounds _sound;
        
        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;

        private void Start()
        {
            if (_autoplay)
                PlaySound();
        }

        public void PlaySound() =>
            _soundService.PlaySound(sound: _sound, overrideSound: true);
    }
}