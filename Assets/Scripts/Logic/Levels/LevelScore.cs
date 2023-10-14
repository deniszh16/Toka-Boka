using UnityEngine;

namespace Logic.Levels
{
    public class LevelScore : MonoBehaviour
    {
        public int Score { get; private set; }

        private const int ItemReward = 5;

        public void ChangeScore(int timerValue) =>
            Score += ItemReward + timerValue / 3;
    }
}