using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.Achievements
{
    public class Achievement : MonoBehaviour
    {
        [Header("Элементы карточки")]
        [SerializeField] private Image _background;
        [SerializeField] private GameObject _icon;

        [Header("Цвет открытой карточки")]
        [SerializeField] private Color _color;

        public void UnblockCard()
        {
            _background.color = _color;
            _icon.SetActive(true);
        }
    }
}