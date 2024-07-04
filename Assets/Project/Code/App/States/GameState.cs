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
            //_menuPanel = menuPanel;
            //_menuPanel.StartAction += LaunchGame;
            //_menuPanel.ExitAction += QuitGame;
        }

        private void LaunchMenu()
        {
            //_menuPanel.StartAction -= LaunchGame;
            //_menuPanel.ExitAction -= QuitGame;

            //_appStateMachine.Enter<LaunchGameState, string>(GAME_SCENE);
        }

        public void Exit() { }
    }
}
