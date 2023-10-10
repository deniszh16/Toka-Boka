using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Logic.Levels
{
    public class SearchItem : MonoBehaviour
    {
        [Header("Контейнер иконки")]
        [SerializeField] private Transform _container;

        public GameObject IconContainer =>
            _container.gameObject;
        
        private LevelItems _levelItems;
        private Timer _timer;

        [Inject]
        private void Construct(LevelItems levelItems, Timer timer)
        {
            _levelItems = levelItems;
            _timer = timer;
        }

        private int _currentItemNumber;
        private AssetReferenceGameObject _item;
        private GameObject _currentItem;

        public void ShowCurrentItem()
        {
            if (_currentItem != null && _item != null)
                _item.ReleaseInstance(_currentItem.gameObject);
            
            _item = _levelItems.TaskItems[_currentItemNumber].ShadedIcon;
            if (_item != null) CreateItem();
        }

        private void CreateItem() =>
            _item.InstantiateAsync(_container).Completed += OnItemInstantiated;

        private void OnItemInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _currentItem = handle.Result;
                _timer.SetTimer();
            }
        }

        public bool FindSelectedItem(Item item)
        {
            Item itemOfTask = _levelItems.TaskItems[_currentItemNumber];
            if (item.Equals(itemOfTask))
            {
                _currentItemNumber++;
                if (_currentItemNumber < _levelItems.TaskItems.Count)
                    ShowCurrentItem();

                return true;
            }

            return false;
        }
    }
}