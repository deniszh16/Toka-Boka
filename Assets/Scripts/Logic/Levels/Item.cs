using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Levels
{
    [RequireComponent(typeof(Animator), typeof(Collider2D))]
    public class Item : MonoBehaviour
    {
        [Header("Затемненная иконка")]
        [SerializeField] private AssetReferenceGameObject _shadedIcon;

        public AssetReferenceGameObject ShadedIcon => _shadedIcon;

        public static readonly int _correctItem = Animator.StringToHash("CorrectItem");
        public static readonly int _wrongItem = Animator.StringToHash("WrongItem");
        
        private Animator _animator;
        private Collider2D _collider;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        public void StartAnimation(int clip) =>
            _animator.SetTrigger(clip);

        public void DisableCollider() =>
            _collider.enabled = false;
    }
}