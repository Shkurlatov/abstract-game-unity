using App.Services.Assets;
using App.Services.Progress;
using Game.Cards;
using System;
using UI;
using UnityEngine;

namespace Game
{
    public class GameController
    {
        private readonly IAppAssetProvider _assets;
        private readonly IAppData _data;
        private readonly ICards _cards;
        private readonly HomeButton _homeButton;
        private readonly GameMode _gameMode;
        private readonly Transform _uiRoot;
        private readonly ScoreCounter _scoreCounter;

        private int _score;

        public event Action LeaveGameAction;

        private int _pairsLeft;

        public GameController(IAppAssetProvider assets, IAppData data, ICards cards, HomeButton homeButton, GameMode gameMode, Transform uiRoot, ScoreCounter scoreCounter, int score)
        {
            _assets = assets;
            _data = data;
            _cards = cards;
            _homeButton = homeButton;
            _gameMode = gameMode;
            _uiRoot = uiRoot;
            _scoreCounter = scoreCounter;

            _score = score;
            _pairsLeft = gameMode.PairsCount;
        }

        public void Initialize()
        {
            _cards.LayOut(_gameMode, ProcessProcessResult);
            _homeButton.HomeAction += LeaveGame;
        }

        private void LeaveGame()
        {
            LeaveGameAction?.Invoke();
        }

        private async void ProcessProcessResult(bool isMatch)
        {
            if (isMatch)
            {
                _pairsLeft--;
                _score++;
                _scoreCounter.UpdateScore(_score);
            }

            if (_pairsLeft == 0)
            {
                await _data.SaveProgressAsync(new ProgressData(_score));

                GameCompletePopup completePopup = _assets.Instantiate(AssetPath.GAME_COMPLETE_POPUP, _uiRoot).GetComponent<GameCompletePopup>();
                completePopup.Initialize(LeaveGame);
            }
        }

        public void Cleanup()
        {
            _homeButton.HomeAction -= LeaveGame;
        }
    }
}
