using System;
using System.Collections;
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

        public event Action AllItemsFound;
        
        private int _currentItemNumber;
        private AssetReferenceGameObject _item;
        private GameObject _currentItem;
        
        private LevelItems _levelItems;
        private LevelScore _levelScore;
        private LevelTimer _levelTimer;

        [Inject]
        private void Construct(LevelItems levelItems, LevelScore levelScore, LevelTimer levelTimer)
        {
            _levelItems = levelItems;
            _levelScore = levelScore;
            _levelTimer = levelTimer;
        }

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
                _levelTimer.SetTimer();
            }
        }

        public bool FindSelectedItem(Item item)
        {
            Item itemOfTask = _levelItems.TaskItems[_currentItemNumber];
            if (item.Equals(itemOfTask))
            {
                _currentItemNumber++;
                if (_currentItemNumber < _levelItems.TaskItems.Count)
                {
                    ShowCurrentItem();
                    _levelScore.ChangeScore(_levelTimer.GetCurrentTime());
                }
                else
                {
                    _ = StartCoroutine(OnAllItemsFound());
                }

                return true;
            }

            _levelTimer.ChangeTimerSeconds(-10);
            return false;
        }

        private IEnumerator OnAllItemsFound()
        {
            yield return new WaitForSeconds(0.5f); 
            AllItemsFound?.Invoke();
        }
    }
}