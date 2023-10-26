using UnityEngine;
using UnityEngine.UI;

namespace Logic.Levels
{
    public class ItemIcon : MonoBehaviour
    {
        [Header("Компоненты изображения")]
        [SerializeField] private Image[] _images;

        public void RemoveBlackout()
        {
            foreach (Image image in _images)
                image.color = Color.white;
        }
    }
}