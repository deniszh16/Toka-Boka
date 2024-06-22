using System.Collections.Generic;
using System.Linq;
using DZGames.TokaBoka.Services;
using UnityEngine;
using VContainer;

namespace DZGames.TokaBoka.UI
{
    public class Achievements : MonoBehaviour
    {
        [Header("Карточки достижений")]
        [SerializeField] private List<Achievement> _achievements;

        private IPersistentProgressService _progressService;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;
        
        private void OnEnable() =>
            CheckAchievements();

        private void CheckAchievements()
        {
            if (_progressService.GetUserProgress.Progress > 1)
                _achievements[0].UnblockCard();
            
            if (_progressService.GetUserProgress.GetNumberOfOpenPets(1) > 2)
                _achievements[1].UnblockCard();
        }
    }
}