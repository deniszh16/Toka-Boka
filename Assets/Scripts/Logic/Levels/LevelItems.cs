using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Logic.Levels
{
    public class LevelItems : MonoBehaviour
    {
        [Header("Предметы уровня")]
        [SerializeField] private List<GameObject> _items;

        [Header("Количество заданий")]
        [SerializeField] private int _numberOfTasks;
        
        private List<GameObject> _taskItems;
        
        private int _count;
        private GameObject _uniqueObject;
        private int _uniqueObjectNumber;

        public void SelectElementsForTask()
        {
            Random random = new Random();
            while (_count < _numberOfTasks)
            {
                _uniqueObjectNumber = random.Next(0, _items.Count);
                _uniqueObject = _items[_uniqueObjectNumber];

                if (_taskItems.Contains(_uniqueObject) == false)
                {
                    _taskItems.Add(_uniqueObject);
                    _count++;
                }
            }
        }
    }
}