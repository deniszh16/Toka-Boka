using DG.Tweening;
using UnityEngine;

namespace DZGames.TokaBoka.UI
{
    public class MovingAnElement : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private RectTransform _rectTransform;
        
        [Header("Финальная позиция")]
        [SerializeField] private Vector2 _targetPosition;
        
        [Header("Длительность анимации")]
        [SerializeField] private float _duration;

        private void Start() =>
            MoveToTargetPosition();

        private void MoveToTargetPosition() =>
            _rectTransform.DOAnchorPos(_targetPosition, _duration).SetEase(Ease.OutQuad);
    }
}