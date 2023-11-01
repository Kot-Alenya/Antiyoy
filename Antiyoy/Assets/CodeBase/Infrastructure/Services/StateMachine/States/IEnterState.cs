namespace CodeBase.Infrastructure.Services.StateMachine.States
{
    public interface IEnterState : IState
    {
        public void Enter();
    }

    public interface IEnterState<in T> : IState where T : IStateParameter
    {
        public void Enter(T parameter);
    }
}