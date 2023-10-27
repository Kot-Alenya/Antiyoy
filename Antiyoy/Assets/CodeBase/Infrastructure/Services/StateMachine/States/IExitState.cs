namespace CodeBase.Infrastructure.Services.StateMachine.States
{
    public interface IExitState : IState
    {
        public void Exit();
    }
}