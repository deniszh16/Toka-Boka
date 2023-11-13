using UnityEngine;
using TMPro;

namespace Logic.UI.ListOfLevels
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class NumberOfStars : MonoBehaviour
    {
        private TextMeshProUGUI _quantity;

        private void Awake() =>
            _quantity = GetComponent<TextMeshProUGUI>();

        public void ShowNumberOfStars(int stars) =>
            _quantity.text = stars.ToString();
    }
}