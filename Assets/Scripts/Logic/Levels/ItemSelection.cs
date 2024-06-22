using TokaBoka.Services;
using UnityEngine;

namespace TokaBoka.Levels
{
    public class ItemSelection : MonoBehaviour
    {
        [Header("Слой объектов")]
        [SerializeField] private LayerMask _layerMask;
        
        [Header("Звездный эффект")]
        [SerializeField] private ParticleSystem _starEffect;

        [Header("Стрелка подсказки")]
        [SerializeField] private GameObject _arrow;
        
        private bool _activity;
        
        private Vector2 _tapPosition;
        private float _distance;
        
        private Ray _ray;
        private RaycastHit2D _hit;
        private Item _currentItem;

        private const float MaxDistance = 20f;
        private const float RayLength = 12f;

        private Camera _mainCamera;
        private SearchItem _searchItem;
        private ISoundService _soundService;
        private IMonoUpdateService _monoUpdateService;
        
        public void Init(ISoundService soundService, IMonoUpdateService monoUpdateService, SearchItem searchItem)
        {
            if (_monoUpdateService == null)
            {
                _soundService = soundService;
                _searchItem = searchItem;
                _monoUpdateService = monoUpdateService;
                _monoUpdateService.AddToUpdate(MyUpdate);
            }
        }

        private void Awake() =>
            _mainCamera = Camera.main;

        public void ChangeActivity(bool activity) =>
            _activity = activity;

        private void MyUpdate()
        {
            if (_activity == false)
                return;

            if (Input.GetMouseButtonDown(0))
                _tapPosition = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                _distance = Vector2.Distance(_tapPosition, Input.mousePosition);
                if (_distance < MaxDistance)
                {
                    _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                    _hit = Physics2D.Raycast(_ray.origin, _ray.direction, RayLength, _layerMask);
                    
                    if (_hit.collider != null)
                    {
                        if (_hit.collider.TryGetComponent(out Item item))
                        {
                            _currentItem = item;
                            CheckSelectedItem();
                        }
                        else if (_hit.collider.TryGetComponent(out OpeningFurniture furniture))
                            furniture.OpenFurniture();
                    }
                }
            }
        }
        
        private void CheckSelectedItem()
        {
            if (_searchItem.FindSelectedItem(_currentItem))
            {
                _currentItem.StartAnimation(clip: Item._correctItem);
                _currentItem.DisableCollider();
                _soundService.PlaySound(sound: SoundsEnum.RightChoice);
                _arrow.SetActive(false);
                _starEffect.transform.position = _currentItem.transform.position;
                _starEffect.Play();
            }
            else
            {
                _currentItem.StartAnimation(clip: Item._wrongItem);
                _soundService.PlaySound(sound: SoundsEnum.IncorrectChoice);
            }
        }

        private void OnDestroy() =>
            _monoUpdateService?.RemoveFromUpdate(MyUpdate);
    }
}