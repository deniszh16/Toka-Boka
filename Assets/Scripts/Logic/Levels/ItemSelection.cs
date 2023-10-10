using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class ItemSelection : MonoBehaviour
    {
        [Header("Слой объектов")]
        [SerializeField] private LayerMask _layerMask;
        
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

        [Inject]
        private void Construct(SearchItem searchItem) =>
            _searchItem = searchItem;

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
            }
            else
            {
                _currentItem.StartAnimation(Item.WrongItem);
            }
        }
    }
}