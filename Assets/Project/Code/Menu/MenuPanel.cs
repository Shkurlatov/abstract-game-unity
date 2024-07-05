using App.Services.Progress;
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

        private IAppData _data;
        private int _gameModeValue;

        public event Action<GameMode> StartAction;
        public event Action ExitAction;

        private void OnEnable()
        {
            _gameModeSlider.onValueChanged.AddListener(OnGameModeValueChanged);
            _startButton.onClick.AddListener(OnStartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _gameModeSlider.onValueChanged.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        public async void Initialize(IAppData data)
        {
            _data = data;

            SettingsData settingsData = await _data.LoadSettingsAsync();

            _gameModeSlider.value = settingsData.GameMode;
            _gameModeValue = settingsData.GameMode;
            UpdateGameModeText();
        }

        private void OnGameModeValueChanged(float value)
        {
            _gameModeValue = (int)value;
            UpdateGameModeText();
        }

        private async void OnStartButtonClick()
        {
            StartAction?.Invoke(GetGameMode());

            await _data.SaveSettingsAsync(new SettingsData(_gameModeValue));
        }

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
