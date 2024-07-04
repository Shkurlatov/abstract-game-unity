namespace App.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}