using UnityEngine;

namespace Logic.Levels
{
    public class CurrentLevel : MonoBehaviour
    {
        [Header("Номер уровня")]
        [SerializeField] private int _levelNumber;

        public int LevelNumber =>
            _levelNumber;
        
        public int Score { get; private set; }
        
        private const int ItemReward = 2;
        
        public void ChangeScore() =>
            Score += ItemReward;
    }
}