using App.Services.Assets;
using App.Services.Progress;
using System;
using UI;

namespace Game
{
    public class GameController
    {
        private readonly GameContext _context;

        private int _score;
        private int _pairsLeft;

        public event Action LeaveGameAction;

        public GameController(GameContext gameContext)
        {
            _context = gameContext;

            _score = _context.Score;
            _pairsLeft = _context.GameMode.PairsCount;
        }

        public void Initialize()
        {
            _context.Cards.LayOut(_context.GameMode, ProcessProcessResult);
            _context.HomeButton.HomeAction += LeaveGame;
        }

        private async void ProcessProcessResult(bool isMatch)
        {
            if (isMatch)
            {
                _pairsLeft--;
                _score++;
                _context.ScoreCounter.UpdateScore(_score);
                _context.Audio.PlayMatchSound();
            }
            else
            {
                _context.Audio.PlayMismatchSound();
            }

            if (_pairsLeft == 0)
            {
                _context.Audio.PlayGameCompleteSound();

                GameCompletePopup completePopup = _context.Assets
                    .Instantiate(AssetPath.GAME_COMPLETE_POPUP, _context.UIRoot)
                    .GetComponent<GameCompletePopup>();

                completePopup.Initialize(LeaveGame);

                await _context.Data.SaveProgressAsync(new ProgressData(_score));
            }
        }

        private void LeaveGame()
        {
            LeaveGameAction?.Invoke();
        }

        public void Cleanup()
        {
            _context.HomeButton.HomeAction -= LeaveGame;
            _context.Cards.Cleanup();
        }
    }
}
