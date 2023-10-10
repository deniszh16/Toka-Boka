using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Levels
{
    public class Item : MonoBehaviour
    {
        [Header("Затемненная иконка")]
        [SerializeField] private AssetReferenceGameObject _shadedIcon;

        public AssetReferenceGameObject ShadedIcon => _shadedIcon;

        private Animator _animator;
        private Collider2D _collider;
        
        public static readonly int CorrectItem = Animator.StringToHash("CorrectItem");
        public static readonly int WrongItem = Animator.StringToHash("WrongItem");

        private void Awake()
        {
            if (gameObject.TryGetComponent(out Animator animator))
                _animator = animator;

            if (gameObject.TryGetComponent(out Collider2D collider))
                _collider = collider;
        }

        public void StartAnimation(int clip)
        {
            if (_animator != null)
                _animator.SetTrigger(clip);
        }

        public void DisableCollider()
        {
            if (_collider != null)
                _collider.enabled = false;
        }
    }
}