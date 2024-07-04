using App.Services;
using App.States;

namespace App.Bootstrap
{
    public class App
    {
        private readonly AppServiceContainer _appContext;
        private readonly IAppStateMachine _appStateMachine;

        public App()
        {
            _appContext = new AppServiceContainer();
            _appStateMachine = new AppStateMachine(new SceneLoader(), _appContext);
            _appStateMachine.Enter<BootstrapState>();
        }

        public void OnApplicationQuit()
        {
            _appStateMachine.Cleanup();
            _appContext.Cleanup();
        }
    }
}
