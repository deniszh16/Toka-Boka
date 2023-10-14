using UnityEngine;
using TMPro;

namespace Logic.Levels
{
    public class LevelResults : MonoBehaviour
    {
        [Header("Панель победы")]
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private TextMeshProUGUI _score;
        
        [Header("Панель поражения")]
        [SerializeField] private GameObject _lossPanel;

        public void ShowVictoryPanel() =>
            _victoryPanel.SetActive(true);

        public void ShowLossPanel(bool visibility) =>
            _lossPanel.SetActive(visibility);

        public void ShowCurrentScore(int score) =>
            _score.text = score.ToString();
    }
}