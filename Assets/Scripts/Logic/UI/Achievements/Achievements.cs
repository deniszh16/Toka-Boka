using System.Collections.Generic;
using System.Linq;
using Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Logic.UI.Achievements
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

            if (_progressService.GetUserProgress.Attempts.Any(attempt => attempt >= 3))
                _achievements[2].UnblockCard();
            
            if (_progressService.GetUserProgress.GetNumberOfOpenPets(2) > 2)
                _achievements[3].UnblockCard();
            
            if (_progressService.GetUserProgress.Hearts >= 1000)
                _achievements[4].UnblockCard();
            
            if (_progressService.GetUserProgress.GetNumberOfOpenPets(3) > 2)
                _achievements[5].UnblockCard();
            
            if (_progressService.GetUserProgress.GetNumberOfOpenPets(4) > 2)
                _achievements[6].UnblockCard();
            
            if (_progressService.GetUserProgress.Progress > 5)
                _achievements[7].UnblockCard();
            
            if (_progressService.GetUserProgress.GetNumberOfOpenPets(5) > 2)
                _achievements[8].UnblockCard();
            
            if (_progressService.GetUserProgress.GetNumberOfOpenPets(6) > 2)
                _achievements[9].UnblockCard();
        }
    }
}