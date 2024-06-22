using DZGames.TokaBoka.Services;
using UnityEngine;
using VContainer;

namespace DZGames.TokaBoka.Sounds
{
    public class PlayingSound : MonoBehaviour
    {
        [Header("Автовоспроизведение")]
        [SerializeField] private bool _autoplay;
        
        [Header("Звук")]
        [SerializeField] private SoundsEnum _sound;
        
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
            _soundService.PlaySound(sound: _sound);
    }
}