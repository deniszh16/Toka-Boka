using TokaBoka.Levels;
using TokaBoka.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class HintButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private Animator _animator;
        
        [Header("Стоимость подсказки")]
        [SerializeField] private int _price;

        [Header("Стрелка подсказки")]
        [SerializeField] private GameObject _arrow;

        private static readonly int Purchase = Animator.StringToHash(name: "Purchase");

        private Transform _currentItem;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private CameraMove _cameraMove;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            CameraMove cameraMove)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _cameraMove = cameraMove;
        }

        private void OnEnable() =>
            _button.onClick.AddListener(GetHint);

        public void CustomizeHint(Transform currentItem)
        {
            _currentItem = currentItem;
            
            if (_button != null)
                _button.interactable = true;
        }

        private void GetHint()
        {
            bool purchase = _progressService.GetUserProgress.SubtractHearts(_price);
            if (purchase)
            {
                _cameraMove.MoveCameraToTarget(_currentItem.position);
                _arrow.transform.position = _currentItem.position;
                _arrow.SetActive(true);
                _animator.gameObject.SetActive(true);
                _animator.SetTrigger(id: Purchase);
                _button.interactable = false;
                _saveLoadService.SaveProgress();
            }
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(GetHint);
    }
}