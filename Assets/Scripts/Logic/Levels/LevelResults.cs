using UnityEngine;
using TMPro;

namespace Logic.Levels
{
    public class LevelResults : MonoBehaviour
    {
        [Header("Панель победы")]
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Transform _confettiEffect;
        
        [Header("Панель поражения")]
        [SerializeField] private GameObject _lossPanel;

        public void ShowVictoryPanel(Vector3 effectPosition)
        {
            _victoryPanel.SetActive(true);
            _confettiEffect.position = effectPosition;
            _confettiEffect.gameObject.SetActive(true);
        }

        public void ShowLossPanel(bool visibility) =>
            _lossPanel.SetActive(visibility);

        public void ShowCurrentScore(int score) =>
            _score.text = score.ToString();
    }
}