using App.Services;

namespace App.States
{
    public class MenuState : IPayloadedState<string>
    {
        private const string GAME_SCENE = "Game";

        private readonly IAppStateMachine _appStateMachine;
        private readonly AppServiceContainer _appContext;

        public MenuState(IAppStateMachine appStateMachine, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _appContext = appContext;
        }

        public void Enter(string payload)
        {
            //_mainMenuOverlay.ReadyToLaunchAction += LaunchGame;
        }

        private void LaunchGame()
        {
            //_mainMenuOverlay.ReadyToLaunchAction -= LaunchGame;
            _appStateMachine.Enter<LaunchGameState, string>(GAME_SCENE);
        }

        public void Exit() { }
    }
}
