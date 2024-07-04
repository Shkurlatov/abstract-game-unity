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

        public GameController(ICards cards, HomeButton homeButton, GameMode gameMode)
        {
            _cards = cards;
            _homeButton = homeButton;
            _gameMode = gameMode;
        }

        public void Initialize()
        {
            _cards.LayOut(_gameMode);
            _homeButton.HomeAction += OnGameComplete;
        }

        public void OnGameComplete()
        {
            GameCompleteAction?.Invoke();
        }

        public void Cleanup()
        {
            _homeButton.HomeAction -= OnGameComplete;
        }
    }
}
