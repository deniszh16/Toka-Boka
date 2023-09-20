using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.ListOfLevels
{
    [RequireComponent(typeof(Button))]
    public class LevelOpenButton : MonoBehaviour
    {
        private Button _button;

        private LevelSelection _levelSelection;

        [Inject]
        private void Construct(LevelSelection levelSelection) =>
            _levelSelection = levelSelection;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(_levelSelection.GoToLevel);

        private void OnDisable() =>
            _button.onClick.RemoveListener(_levelSelection.GoToLevel);
    }
}