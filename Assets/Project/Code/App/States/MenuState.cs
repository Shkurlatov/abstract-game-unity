using App.Services;
using Menu;
using Game;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace App.States
{
    public class MenuState : IPayloadedState<MenuPanel>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly AppServiceContainer _appContext;

        private MenuPanel _menuPanel;

        public MenuState(IAppStateMachine appStateMachine, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _appContext = appContext;
        }

        public void Enter(MenuPanel menuPanel)
        {
            _menuPanel = menuPanel;
            _menuPanel.StartAction += LaunchGame;
            _menuPanel.ExitAction += QuitGame;
        }

        private void LaunchGame(GameMode gameMode)
        {
            _menuPanel.StartAction -= LaunchGame;
            _menuPanel.ExitAction -= QuitGame;

            _appStateMachine.Enter<LaunchGameState, GameMode>(gameMode);
        }

        private void QuitGame()
        {
            _menuPanel.StartAction -= LaunchGame;
            _menuPanel.ExitAction -= QuitGame;

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public void Exit() { }
    }
}
