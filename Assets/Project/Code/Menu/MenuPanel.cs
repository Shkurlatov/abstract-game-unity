using Game;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gameModeText;
        [SerializeField] private Slider _gameModeSlider;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        private int _gameModeValue;

        public event Action<GameMode> StartAction;
        public event Action ExitAction;

        private void OnEnable()
        {
            _gameModeSlider.onValueChanged.AddListener(OnGameModeValueChanged);
            _startButton.onClick.AddListener(OnStartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);

            _gameModeValue = (int)_gameModeSlider.value;
            UpdateGameModeText();
        }

        private void OnDisable()
        {
            _gameModeSlider.onValueChanged.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void OnGameModeValueChanged(float value)
        {
            _gameModeValue = (int)value;
            UpdateGameModeText();
        }

        private void OnStartButtonClick() => 
            StartAction?.Invoke(GetGameMode());

        private void OnExitButtonClick() => 
            ExitAction?.Invoke();

        private void UpdateGameModeText()
        {
            int rows = _gameModeValue / 2;
            int columns = _gameModeValue - rows;
            _gameModeText.text = $"{rows} x {columns}";
        }

        private GameMode GetGameMode()
        {
            int rows = _gameModeValue / 2;
            int columns = _gameModeValue - rows;
            return new GameMode(rows, columns);
        }
    }
}
