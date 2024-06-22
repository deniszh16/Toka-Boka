using DZGames.TokaBoka.ListOfLevels;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.TokaBoka.UI
{
    public class LevelOpenButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        private LevelSelection _levelSelection;
        
        [Inject]
        private void Construct(LevelSelection levelSelection) =>
            _levelSelection = levelSelection;

        private void OnEnable() =>
            _button.onClick.AddListener(_levelSelection.GoToLevel);

        private void OnDisable() =>
            _button.onClick.RemoveListener(_levelSelection.GoToLevel);
    }
}