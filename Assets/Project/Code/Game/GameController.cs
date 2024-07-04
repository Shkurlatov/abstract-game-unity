using Game.Cards;

namespace Game
{
    public class GameController
    {
        private readonly ICards _cards;
        private readonly HomeButton _homeButton;
        private readonly GameMode _gameMode;

        public GameController(ICards cards, HomeButton homeButton, GameMode gameMode)
        {
            _cards = cards;
            _homeButton = homeButton;
            _gameMode = gameMode;
        }

        public void Initialize()
        {
            _cards.LayOut(_gameMode);
        }
    }
}
