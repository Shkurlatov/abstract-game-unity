using App.Bootstrap;
using App.Services;

namespace App.States
{
    public class GameState : IPayloadedState<string>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        public GameState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter(string payload)
        {
            //_gameContext.Single<IGameBuildings>().ClickOnTavernAction += OpenHeroShopScene;
        }

        public void Exit()
        {
            //_gameContext.Single<IGameBuildings>().ClickOnTavernAction -= OpenHeroShopScene;
        }
    }
}
