using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        public event Action StartAction;
        public event Action ExitAction;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void OnStartButtonClick()
        {
            StartAction?.Invoke();
        }

        private void OnExitButtonClick()
        {
            ExitAction?.Invoke();
        }
    }
}
