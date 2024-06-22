using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TokaBoka.Services;
using TokaBoka.StateMachine;
using VContainer;
using TMPro;

namespace TokaBoka.UI
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
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void ShowVictoryPanel(Vector3 effectPosition)
        {
            _victoryPanel.SetActive(true);
            ChangeVisibilityOfScreenDimming(state: true);
            ShowZoomAnimation(_victoryPanel.transform);
            ShowFireworksEffects(effectPosition);
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
    }
}