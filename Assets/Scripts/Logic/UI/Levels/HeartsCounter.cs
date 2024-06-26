﻿using TokaBoka.Services;
using UnityEngine;
using VContainer;
using TMPro;

namespace TokaBoka.UI
{
    public class HeartsCounter : MonoBehaviour
    {
        [Header("Ссылки на комопненты")]
        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private Animator _animator;
        
        private static readonly int NotEnough = Animator.StringToHash("NotEnough");

        private IPersistentProgressService _progressService;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;

        private void Start() =>
            UpdateCounter();

        private void OnEnable()
        {
            _progressService.GetUserProgress.HeartsAdded += UpdateCounter;
            _progressService.GetUserProgress.NotEnoughHearts += StartAnimation;
        }

        private void UpdateCounter() =>
            _counterText.text = _progressService.GetUserProgress.Hearts.ToString();

        private void StartAnimation() =>
            _animator.SetTrigger(id: NotEnough);

        private void OnDisable()
        {
            _progressService.GetUserProgress.HeartsAdded -= UpdateCounter;
            _progressService.GetUserProgress.NotEnoughHearts -= StartAnimation;
        }
    }
}