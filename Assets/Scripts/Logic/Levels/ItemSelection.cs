using Services.Sound;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class ItemSelection : MonoBehaviour
    {
        [Header("Слой объектов")]
        [SerializeField] private LayerMask _layerMask;
        
        [Header("Звездный эффект")]
        [SerializeField] private ParticleSystem _starEffect;
        
        private bool _activity;
        
        private Vector2 _tapPosition;
        private float _distance;
        
        private Ray _ray;
        private RaycastHit2D _hit;
        private Item _currentItem;

        private const float MaxDistance = 20f;
        private const float RayLength = 20f;

        private Camera _mainCamera;
        private SearchItem _searchItem;
        private ISoundService _soundService;

        [Inject]
        private void Construct(SearchItem searchItem, ISoundService soundService)
        {
            _searchItem = searchItem;
            _soundService = soundService;
        }

        private void Awake() =>
            _mainCamera = Camera.main;

        public void ChangeActivity(bool activity) =>
            _activity = activity;

        private void Update()
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

                        if (_hit.collider.TryGetComponent(out OpeningFurniture furniture))
                            furniture.OpenFurniture();
                    }
                }
            }
        }
        
        private void CheckSelectedItem()
        {
            if (_searchItem.FindSelectedItem(_currentItem))
            {
                _currentItem.StartAnimation(Item.CorrectItem);
                _currentItem.DisableCollider();
                _soundService.PlaySound(Services.Sound.Sounds.RightChoice, overrideSound: false);
                _starEffect.transform.position = _currentItem.transform.position;
                _starEffect.Play();
            }
            else
            {
                _currentItem.StartAnimation(Item.WrongItem);
                _soundService.PlaySound(Services.Sound.Sounds.IncorrectChoice, overrideSound: false);
            }
        }
    }
}