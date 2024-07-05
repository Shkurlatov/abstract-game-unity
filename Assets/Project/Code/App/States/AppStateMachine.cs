using App.Bootstrap;
using App.Services;
using System;
using System.Collections.Generic;

namespace App.States
{
    public class AppStateMachine : IAppStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState;

        public AppStateMachine(SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, appContext),
                [typeof(LaunchMenuState)] = new LaunchMenuState(this, sceneLoader, appContext),
                [typeof(MenuState)] = new MenuState(this, appContext),
                [typeof(LaunchGameState)] = new LaunchGameState(this, sceneLoader, appContext),
                [typeof(GameState)] = new GameState(this, appContext),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Cleanup() =>
            _activeState?.Exit();

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}
