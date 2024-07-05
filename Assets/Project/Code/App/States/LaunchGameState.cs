using App.Bootstrap;
using App.Services;
using App.Services.Assets;
using App.Services.Progress;
using App.Services.Randomizer;
using Game;
using Game.Cards;
using UI;
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

        private async void OnLoaded()
        {
            IAppAssetProvider assets = _appContext.Single<IAppAssetProvider>();
            IAppRandomizer randomizer = _appContext.Single<IAppRandomizer>();
            IAppData data = _appContext.Single<IAppData>();

            SettingsData settingsData = await data.LoadSettingsAsync();
            ProgressData progressData = await data.LoadProgressAsync();

            Transform uiRoot = InitUIRoot();
            HomeButton homeButton = InitHomeButton(assets, uiRoot);
            ScoreCounter scoreCounter = InitScoreCounter(assets, uiRoot, progressData.Score);

            ICards cards = InitCards(assets, randomizer);
            GameController gameController = InitGameController(assets, cards, homeButton, uiRoot);

            _appStateMachine.Enter<GameState, GameController>(gameController);
        }

        private Transform InitUIRoot() =>
            GameObject.FindGameObjectWithTag(UI_ROOT_TAG).transform;

        private HomeButton InitHomeButton(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.HOME_BUTTON, uiRoot).GetComponent<HomeButton>();

        private ScoreCounter InitScoreCounter(IAppAssetProvider assets, Transform uiRoot, int score)
        {
            ScoreCounter scoreCounter = assets.Instantiate(AssetPath.SCORE_COUNTER, uiRoot).GetComponent<ScoreCounter>();
            scoreCounter.UpdateCounter(score);
            return scoreCounter;
        }

        private ICards InitCards(IAppAssetProvider assets, IAppRandomizer randomizer) =>
            new CardManager(assets, randomizer);

        private GameController InitGameController(IAppAssetProvider assets, ICards cards, HomeButton homeButton, Transform uiRoot) =>
            new GameController(assets, cards, homeButton, _gameMode, uiRoot);

        public void Exit() { }
    }
}
