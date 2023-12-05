using Logic.Levels;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    public class HintButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        [Header("Стоимость подсказки")]
        [SerializeField] private int _price;

        [Header("Анимация подсказки")]
        [SerializeField] private Animator _animator;
        
        private ItemIcon _currentItem;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void OnEnable() =>
            _button.onClick.AddListener(GetHint);

        public void CustomizeHint(ItemIcon itemIcon)
        {
            _currentItem = itemIcon;
            
            if (_button != null)
                _button.interactable = true;
        }

        private void GetHint()
        {
            bool purchase = _progressService.UserProgress.SubtractHearts(_price);
            if (purchase)
            {
                _currentItem.RemoveBlackout();
                _animator.gameObject.SetActive(true);
                _animator.Rebind();
                _button.interactable = false;
                _saveLoadService.SaveProgress();
            }
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(GetHint);
    }
}