using Logic.StateMachine.States;
using Services.YandexService;
using Services.StateMachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using TMPro;

namespace Logic.UI.Levels
{
    public class LevelUI : MonoBehaviour
    {
        [Header("Панель победы")]
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Transform[] _effectsFireworks;
        
        [Header("Панель поражения")]
        [SerializeField] private GameObject _lossPanel;
        
        [Header("Затемнение экрана")]
        [SerializeField] private Image _dimmingScreen;
        
        [Header("Элементы паузы")]
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _heartPanel;

        private const float _duration = 0.3f;
        
        private GameStateMachine _gameStateMachine;
        private IYandexService _yandexService;
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IYandexService yandexService)
        {
            _gameStateMachine = gameStateMachine;
            _yandexService = yandexService;
        }

        public void ShowVictoryPanel(Vector3 effectPosition)
        {
            _victoryPanel.SetActive(true);
            ChangeVisibilityOfScreenDimming(state: true);
            ShowZoomAnimation(_victoryPanel.transform);
            ShowFireworksEffects(effectPosition);
            
            _ = StartCoroutine(ShowAds());
        }

        public void ChangeVisibilityOfScreenDimming(bool state) =>
            _dimmingScreen.gameObject.SetActive(state);

        private void ShowFireworksEffects(Vector3 effectPosition)
        {
            _effectsFireworks[0].position = new Vector3(x: effectPosition.x - 6, y: -5.2f, z: 0f);
            _effectsFireworks[0].gameObject.SetActive(true);
            _effectsFireworks[1].position = new Vector3(x: effectPosition.x + 6, y: -5.2f, z: 0f);
            _effectsFireworks[1].gameObject.SetActive(true);
        }

        public void ShowLossPanel(bool visibility)
        {
            _lossPanel.SetActive(visibility);
            ChangeVisibilityOfScreenDimming(visibility);
            ShowZoomAnimation(_lossPanel.transform);
            _ = StartCoroutine(ShowAds());
        }

        public void ShowCurrentScore(int score) =>
            _score.text = score.ToString();
        
        public void TogglePause(bool pause)
        {
            if (pause) _gameStateMachine.Enter<PauseState>();
            else _gameStateMachine.Enter<PlayState>();
        }
        
        public void ChangeButtonVisibility(bool visibility)
        {
            _pauseButton.SetActive(visibility);
            _heartPanel.SetActive(visibility);
        }
        
        public void ChangePausePanelVisibility(bool visibility)
        {
            _dimmingScreen.gameObject.SetActive(visibility);
            _pausePanel.SetActive(visibility);
        }

        private void ShowZoomAnimation(Transform panel)
        {
            panel.transform.localScale = Vector3.zero;
            panel.DOScale(Vector3.one, _duration).SetEase(Ease.OutQuad);
        }

        private IEnumerator ShowAds()
        {
            yield return new WaitForSeconds(0.7f);
            _yandexService.ShowFullScreenAds();
        }
    }
}