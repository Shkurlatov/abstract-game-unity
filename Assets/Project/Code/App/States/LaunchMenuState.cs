using App.Bootstrap;
using App.Services;
using App.Services.Assets;
using Menu;
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

        private void OnLoaded()
        {
            IAppAssetProvider assets = _appContext.Single<IAppAssetProvider>();
            Transform uiRoot = InitUIRoot();
            MenuPanel menuPanel = InitMenuPanel(assets, uiRoot);

            _appStateMachine.Enter<MenuState, MenuPanel>(menuPanel);
        }

        private Transform InitUIRoot() =>
            GameObject.FindGameObjectWithTag(UI_ROOT_TAG).transform;

        private MenuPanel InitMenuPanel(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.MENU_PANEL, uiRoot).GetComponent<MenuPanel>();

        public void Exit() { }
    }
}
