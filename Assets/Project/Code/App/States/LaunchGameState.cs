using App.Bootstrap;
using App.Services;
using App.Services.Assets;
using Game;
using UnityEngine;

namespace App.States
{
    public class LaunchGameState : IPayloadedState<GameMode>
    {
        private const string GAME_SCENE = "Game";
        private const string UI_ROOT_TAG = "UIRoot";

        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        private GameMode _gameMode;

        public LaunchGameState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter(GameMode gameMode)
        {
            _gameMode = gameMode;
            _sceneLoader.Load(GAME_SCENE, OnLoaded);
        }

        private void OnLoaded()
        {
            IAppAssetProvider assets = _appContext.Single<IAppAssetProvider>();
            Transform uiRoot = InitUIRoot();
            HomeButton homeButton = InitHomeButton(assets, uiRoot);
            InitScoreCounter(assets, uiRoot);
            GameController gameController = InitGameController(homeButton);

            _appStateMachine.Enter<GameState, GameController>(gameController);
        }

        private GameController InitGameController(HomeButton homeButton) =>
            new GameController(homeButton);

        private Transform InitUIRoot() =>
            GameObject.FindGameObjectWithTag(UI_ROOT_TAG).transform;

        private HomeButton InitHomeButton(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.HOME_BUTTON, uiRoot).GetComponent<HomeButton>();

        private void InitScoreCounter(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.SCORE_COUNTER, uiRoot);

        public void Exit() { }
    }
}
