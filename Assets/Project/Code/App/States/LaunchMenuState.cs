using App.Bootstrap;
using App.Services;
using App.Services.Assets;
using App.Services.Progress;
using Menu;
using UI;
using UnityEngine;

namespace App.States
{
    public class LaunchMenuState : IState
    {
        private const string MENU_SCENE = "Menu";
        private const string UI_ROOT_TAG = "UIRoot";

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

        private async void OnLoaded()
        {
            IAppAssetProvider assets = _appContext.Single<IAppAssetProvider>();
            IAppData data = _appContext.Single<IAppData>();

            SettingsData settingsData = await data.LoadSettingsAsync();
            ProgressData progressData = await data.LoadProgressAsync();

            Transform uiRoot = InitUIRoot();
            MenuPanel menuPanel = InitMenuPanel(assets, uiRoot);
            InitScoreCounter(assets, uiRoot, progressData.Score);

            _appStateMachine.Enter<MenuState, MenuPanel>(menuPanel);
        }

        private Transform InitUIRoot() =>
            GameObject.FindGameObjectWithTag(UI_ROOT_TAG).transform;

        private MenuPanel InitMenuPanel(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.MENU_PANEL, uiRoot).GetComponent<MenuPanel>();

        private void InitScoreCounter(IAppAssetProvider assets, Transform uiRoot, int score)
        {
            ScoreCounter scoreCounter = assets.Instantiate(AssetPath.SCORE_COUNTER, uiRoot).GetComponent<ScoreCounter>();
            scoreCounter.UpdateScore(score);
        }

        public void Exit() { }
    }
}
