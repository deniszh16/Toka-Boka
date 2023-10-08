using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Levels
{
    [RequireComponent(typeof(Image))]
    public class Timer : MonoBehaviour
    {
        private bool _activity;
        private float _seconds;
        private float _currentTime;
        private float _fillingImage;

        public event Action TimerCompleted;

        private Image _timerImage;

        private void Awake() =>
            _timerImage = GetComponent<Image>();

        private void Update()
        {
            if (_activity == false)
                return;
            
            _currentTime -= Time.deltaTime;
            _fillingImage = _currentTime / _seconds;
            _timerImage.fillAmount = _fillingImage;

            if (_currentTime <= 0)
                TimerCompleted?.Invoke();
        }

        public void SetTimer()
        {
            _seconds = 35;
            _currentTime = _seconds;
        }

        public void ChangeTimerActivity(bool value) =>
            _activity = value;
    }
}