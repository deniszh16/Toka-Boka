using Logic.ListOfLevels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
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