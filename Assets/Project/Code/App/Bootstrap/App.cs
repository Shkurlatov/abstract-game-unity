using App.Services;
using App.States;

namespace App.Bootstrap
{
    public class App
    {
        private readonly AppServiceContainer _appContext;
        private readonly IAppStateMachine _appStateMachine;

        public App(SceneLoader sceneLoader)
        {
            _appContext = new AppServiceContainer();
            _appStateMachine = new AppStateMachine(sceneLoader, _appContext);
            _appStateMachine.Enter<BootstrapState>();
        }

        public void OnApplicationQuit()
        {
            _appStateMachine.Cleanup();
            _appContext.Cleanup();
        }
    }
}
