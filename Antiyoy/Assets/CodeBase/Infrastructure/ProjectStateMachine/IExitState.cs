namespace CodeBase.Infrastructure.ProjectStateMachine
{
    public interface IExitState : IState
    {
        void Exit();
    }
}