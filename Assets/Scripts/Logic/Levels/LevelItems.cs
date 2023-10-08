using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

namespace Logic.Levels
{
    public class LevelItems : MonoBehaviour
    {
        [Header("Предметы уровня")]
        [SerializeField] private List<Item> _items;

        [Header("Количество заданий")]
        [SerializeField] private int _numberOfTasks;
        
        public List<Item> TaskItems { get; private set; }
        
        private int _count;
        private Item _uniqueObject;
        private int _uniqueObjectNumber;

        private void Awake() =>
            TaskItems = new List<Item>();

        public void SelectElementsForTask()
        {
            Random random = new Random();
            while (_count < _numberOfTasks)
            {
                _uniqueObjectNumber = random.Next(0, _items.Count);
                _uniqueObject = _items[_uniqueObjectNumber];

                if (TaskItems.Contains(_uniqueObject) == false)
                {
                    TaskItems.Add(_uniqueObject);
                    _count++;
                }
            }
        }
    }
}