using App.Bootstrap;
using App.Services;

namespace App.States
{
    public class LaunchMenuState : IState
    {
        private const string MENU_SCENE = "Menu";

        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        public LaunchMenuState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter() =>
            _sceneLoader.Load(MENU_SCENE, OnLoaded);

        private void OnLoaded()
        {
            //_appStateMachine.Enter<MenuState, MainMenuOverlay>(_mainMenuOverlay);
        }

        public void Exit() { }
    }
}
