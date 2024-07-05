using App.Services;
using Game;

namespace App.States
{
    public class GameState : IPayloadedState<GameController>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly AppServiceContainer _appContext;

        private GameController _gameController;

        public GameState(IAppStateMachine appStateMachine, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _appContext = appContext;
        }

        public void Enter(GameController gameController)
        {
            _gameController = gameController;
            _gameController.Initialize();
            _gameController.LeaveGameAction += ReturnToMenu;
        }

        private void ReturnToMenu()
        {
            _gameController.LeaveGameAction -= ReturnToMenu;

            _appStateMachine.Enter<LaunchMenuState>();
        }

        public void Exit()
        {
            if (_gameController != null)
            {
                _gameController.Cleanup();
                _gameController = null;
            }
        }
    }
}
