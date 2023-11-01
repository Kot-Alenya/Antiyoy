using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Services.StateMachine.Factory
{
    public interface IStateFactory
    {
        public T Create<T>() where T : IState;
    }
}