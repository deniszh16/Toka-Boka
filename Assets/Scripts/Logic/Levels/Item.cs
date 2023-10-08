using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Levels
{
    public class Item : MonoBehaviour
    {
        [Header("Затемненная иконка")]
        [SerializeField] private AssetReferenceGameObject _shadedIcon;

        public AssetReferenceGameObject ShadedIcon => _shadedIcon;
    }
}