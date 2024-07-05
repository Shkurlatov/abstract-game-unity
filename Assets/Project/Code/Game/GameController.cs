using Game.Cards;
using System;

namespace Game
{
    public class GameController
    {
        private readonly ICards _cards;
        private readonly HomeButton _homeButton;
        private readonly GameMode _gameMode;

        public event Action GameCompleteAction;

        private int _pairsLeft;

        public GameController(ICards cards, HomeButton homeButton, GameMode gameMode)
        {
            _cards = cards;
            _homeButton = homeButton;
            _gameMode = gameMode;
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
                GameCompleteAction?.Invoke();
            }
        }

        public void Cleanup()
        {
            _homeButton.HomeAction -= OnGameComplete;
        }
    }
}
