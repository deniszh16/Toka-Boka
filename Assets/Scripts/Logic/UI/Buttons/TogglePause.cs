using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class TogglePause : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        [Header("Включение паузы")]
        [SerializeField] private bool _pauseSetting;
        
        private LevelUI _levelUI;
        
        [Inject]
        private void Construct(LevelUI levelUI) =>
            _levelUI = levelUI;

        private void OnEnable() =>
            _button.onClick.AddListener(() => _levelUI.TogglePause(_pauseSetting));

        private void OnDisable() =>
            _button.onClick.RemoveAllListeners();
    }
}