using App.Bootstrap;
using App.Services;
using App.Services.Assets;
using App.Services.Audio;
using App.Services.Progress;
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
            RegisterAppServices();

            _appStateMachine.Enter<LaunchMenuState>();
        }

        private void RegisterAppServices()
        {
            IAppAssetProvider assets = RegisterAssetProvider();
            RegisterRandomizer();
            RegisterData();
            RegisterAudio(assets);
        }

        private IAppAssetProvider RegisterAssetProvider()
        {
            IAppAssetProvider assets = new AssetProvider();
            _appContext.RegisterSingle(assets);
            return assets;
        }

        private void RegisterRandomizer() =>
            _appContext.RegisterSingle<IAppRandomizer>(new SystemRandomizer());
        
        private void RegisterData() =>
            _appContext.RegisterSingle<IAppData>(new PlayerPrefsSaveLoadManager());

        private void RegisterAudio(IAppAssetProvider assets)
        {
            IAppAudio audio = assets.Instantiate(AssetPath.AUDIO_PLAYER).GetComponent<AudioPlayer>();
            _appContext.RegisterSingle(audio);
        }

        public void Exit() { }
    }
}
