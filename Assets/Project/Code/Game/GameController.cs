using App.Services.Assets;
using Game.Cards;
using System;
using UnityEngine;

namespace Game
{
    public class GameController
    {
        private readonly IAppAssetProvider _assets;
        private readonly ICards _cards;
        private readonly HomeButton _homeButton;
        private readonly GameMode _gameMode;
        private readonly Transform _uiRoot;

        public event Action GameCompleteAction;

        private int _pairsLeft;

        public GameController(IAppAssetProvider assets, ICards cards, HomeButton homeButton, GameMode gameMode, Transform uiRoot)
        {
            _assets = assets;
            _cards = cards;
            _homeButton = homeButton;
            _gameMode = gameMode;
            _uiRoot = uiRoot;
            _pairsLeft = gameMode.PairsCount;
        }

        public void Initialize()
        {
            _cards.LayOut(_gameMode, ProcessProcessResult);
            _homeButton.HomeAction += OnGameComplete;
        }

        public void OnGameComplete()
        {
            GameCompleteAction?.Invoke();
        }

        private void ProcessProcessResult(bool isMatch)
        {
            if (isMatch)
            {
                _pairsLeft--;
            }

            if (_pairsLeft == 0)
            {
                GameCompletePopup completePopup = _assets.Instantiate(AssetPath.GAME_COMPLETE_POPUP, _uiRoot).GetComponent<GameCompletePopup>();
                completePopup.Initialize(OnGameComplete);
            }
        }

        public void Cleanup()
        {
            _homeButton.HomeAction -= OnGameComplete;
        }
    }
}
