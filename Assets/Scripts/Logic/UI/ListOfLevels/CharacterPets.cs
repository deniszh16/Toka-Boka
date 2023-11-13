using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.UI.ListOfLevels
{
    public class CharacterPets : MonoBehaviour
    {
        [Header("Питомцы")]
        [SerializeField] private AssetReferenceGameObject[] _pets;

        public void CreatePets()
        {
            
        }
    }
}