﻿using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Levels
{
    public class Item : MonoBehaviour
    {
        [Header("Затемненная иконка")]
        [SerializeField] private AssetReferenceGameObject _shadedIcon;
        
        [Header("Компоненты предмета")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;

        public AssetReferenceGameObject ShadedIcon => _shadedIcon;

        public static readonly int _correctItem = Animator.StringToHash("CorrectItem");
        public static readonly int _wrongItem = Animator.StringToHash("WrongItem");

        public void StartAnimation(int clip)
        {
            _animator.enabled = true;
            _animator.SetTrigger(clip);
        }

        public void DisableCollider() =>
            _collider.enabled = false;
    }
}