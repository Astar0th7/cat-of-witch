namespace Game.Scripts.Root.Services.StateMachines
{
    public interface IExitableState
    {
        void Tick();
        void Exit();
    }

    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadState<T> : IExitableState
    {
        void Enter(T payload);
    }
}