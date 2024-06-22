using DZGames.TokaBoka.Services;
using UnityEngine;
using VContainer;

namespace DZGames.TokaBoka.Sounds
{
    public class BackgroundMusic : MonoBehaviour
    {
        private ISoundService _soundService;
        
        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;

        private void Start() =>
            _soundService.SettingBackgroundMusic();
    }
}