using App.Bootstrap;
using App.Services;
using App.Services.Assets;
using App.Services.Randomizer;
using Game;
using Game.Cards;
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
            IAppRandomizer randomizer = _appContext.Single<IAppRandomizer>();

            Transform uiRoot = InitUIRoot();
            HomeButton homeButton = InitHomeButton(assets, uiRoot);
            InitScoreCounter(assets, uiRoot);

            ICards cards = InitCards(assets, randomizer);
            GameController gameController = InitGameController(cards, homeButton);

            _appStateMachine.Enter<GameState, GameController>(gameController);
        }

        private Transform InitUIRoot() =>
            GameObject.FindGameObjectWithTag(UI_ROOT_TAG).transform;

        private HomeButton InitHomeButton(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.HOME_BUTTON, uiRoot).GetComponent<HomeButton>();

        private void InitScoreCounter(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.SCORE_COUNTER, uiRoot);

        private ICards InitCards(IAppAssetProvider assets, IAppRandomizer randomizer) =>
            new CardManager(assets, randomizer);

        private GameController InitGameController(ICards cards, HomeButton homeButton) =>
            new GameController(cards, homeButton, _gameMode);

        public void Exit() { }
    }
}
