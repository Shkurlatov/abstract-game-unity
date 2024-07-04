using App.Bootstrap;
using App.Services;
using App.Services.Assets;
using App.Services.Randomizer;

namespace App.States
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE = "Boot";

        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        public BootstrapState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter() =>
            _sceneLoader.Load(INITIAL_SCENE, OnLoaded);

        private void OnLoaded()
        {
            RegisterServices();

            _appStateMachine.Enter<LaunchMenuState>();
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterRandomizer();
        }

        private void RegisterAssetProvider() =>
            _appContext.RegisterSingle<IAppAssetProvider>(new AssetProvider());

        private void RegisterRandomizer() =>
            _appContext.RegisterSingle<IAppRandomizer>(new SystemRandomizer());

        public void Exit() { }
    }
}
