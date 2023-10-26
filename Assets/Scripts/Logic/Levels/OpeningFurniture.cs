using UnityEngine;

namespace Logic.Levels
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class OpeningFurniture : MonoBehaviour
    {
        [Header("Открытый спрайт")]
        [SerializeField] private Sprite _openSprite;

        [Header("Эффект подсветки")]
        [SerializeField] private ParticleSystem _backlightEffect;

        [Header("Предметы внутри")]
        [SerializeField] private Item[] _items;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        public void OpenFurniture()
        {
            _spriteRenderer.sprite = _openSprite;
            _backlightEffect.gameObject.SetActive(false);
            _collider.enabled = false;

            foreach (Item item in _items)
                item.gameObject.SetActive(true);
        }
    }
}