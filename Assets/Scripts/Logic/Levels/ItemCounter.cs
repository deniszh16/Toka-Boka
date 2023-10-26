using UnityEngine;
using TMPro;

namespace Logic.Levels
{
    public class ItemCounter : MonoBehaviour
    {
        [Header("Текст счетчика")]
        [SerializeField] private TextMeshProUGUI _counter;

        public void UpdateCounter(int currentItem, int totalItems) =>
            _counter.text = currentItem + " / " + totalItems;
    }
}