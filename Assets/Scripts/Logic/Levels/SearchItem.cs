﻿using System;
using System.Collections;
using Logic.UI.Buttons;
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
        
        [Header("Иконка загрузки")]
        [SerializeField] private GameObject _loadingAnObject;

        public GameObject IconContainer =>
            _container.gameObject;

        public event Action AllItemsFound;
        
        private int _currentItemNumber;
        private AssetReferenceGameObject _item;
        private GameObject _currentItem;

        private LevelItems _levelItems;
        private LevelScore _levelScore;
        private LevelTimer _levelTimer;
        private ItemCounter _itemCounter;
        private HintButton _hintButton;

        [Inject]
        private void Construct(LevelItems levelItems, LevelScore levelScore,
            LevelTimer levelTimer, ItemCounter itemCounter, HintButton hintButton)
        {
            _levelItems = levelItems;
            _levelScore = levelScore;
            _levelTimer = levelTimer;
            _itemCounter = itemCounter;
            _hintButton = hintButton;
        }

        public void ShowCurrentItem()
        {
            ItemCleanup();
            
            _item = _levelItems.TaskItems[_currentItemNumber].ShadedIcon;
            if (_item != null) CreateItem();
        }

        private void ItemCleanup()
        {
            if (_currentItem != null && _item != null)
                _item.ReleaseInstance(_currentItem.gameObject);
        }

        private void CreateItem()
        {
            _loadingAnObject.SetActive(true);
            _item.InstantiateAsync(_container).Completed += OnItemInstantiated;
        }

        private void OnItemInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadingAnObject.SetActive(false);
                _currentItem = handle.Result;
                _itemCounter.UpdateCounter(currentItem: _currentItemNumber + 1, totalItems: _levelItems.NumberOfTasks);
                _hintButton.CustomizeHint(_currentItem.GetComponent<ItemIcon>());
                _levelTimer.SetTimer();
                _levelTimer.ChangeTimerActivity(value: true);
            }
        }

        public bool FindSelectedItem(Item item)
        {
            Item itemOfTask = _levelItems.TaskItems[_currentItemNumber];
            if (item.Equals(itemOfTask))
            {
                _currentItemNumber++;
                _levelTimer.ChangeTimerActivity(value: false);
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

        private void OnDestroy() =>
            ItemCleanup();
    }
}