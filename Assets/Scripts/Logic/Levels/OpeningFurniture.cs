using UnityEngine;

namespace Logic.Levels
{
    public class OpeningFurniture : MonoBehaviour
    {
        [Header("Компоненты объекта")]
        [SerializeField] private Collider2D _collider; 
        [SerializeField] private GameObject _openFurniture;

        [Header("Эффект подсветки")]
        [SerializeField] private ParticleSystem _backlightEffect;

        [Header("Предметы внутри")]
        [SerializeField] private Item[] _items;

        public void OpenFurniture()
        {
            _openFurniture.SetActive(true);
            _backlightEffect.gameObject.SetActive(false);
            _collider.enabled = false;

            foreach (Item item in _items)
                item.gameObject.SetActive(true);
        }
    }
}