using UnityEngine;

namespace Logic.Levels
{
    public class CurrentLevel : MonoBehaviour
    {
        [Header("Номер уровня")]
        [SerializeField] private int _levelNumber;

        public int LevelNumber => _levelNumber;
    }
}