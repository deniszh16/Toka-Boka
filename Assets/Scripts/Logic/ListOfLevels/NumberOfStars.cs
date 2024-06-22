using UnityEngine;
using TMPro;

namespace DZGames.TokaBoka.ListOfLevels
{
    public class NumberOfStars : MonoBehaviour
    {
        [Header("Текстовый компонент")]
        [SerializeField] private TextMeshProUGUI _quantity;

        public void ShowNumberOfStars(int stars) =>
            _quantity.text = stars.ToString();
    }
}