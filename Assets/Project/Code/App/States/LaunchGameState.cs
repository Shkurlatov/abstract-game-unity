using App.Bootstrap;
using App.Services;

namespace App.States
{
    public class LaunchGameState : IPayloadedState<string>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        public LaunchGameState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter(string sceneName) =>
            _sceneLoader.Load(sceneName, OnLoaded);

        private void OnLoaded()
        {
            RegisterGameServices();

            _appStateMachine.Enter<GameState, string>("game context");
        }

        private void RegisterGameServices()
        {

        }

        public void Exit()
        {

        }
    }
}
