using App.Bootstrap;
using App.Services;
using Game;

namespace App.States
{
    public class GameState : IPayloadedState<GameController>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        private GameController _gameController;

        public GameState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter(GameController gameController)
        {
            _gameController = gameController;
            _gameController.Initialize();
            _gameController.GameCompleteAction += ReturnToMenu;
        }

        private void ReturnToMenu()
        {
            _gameController.GameCompleteAction -= ReturnToMenu;

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
