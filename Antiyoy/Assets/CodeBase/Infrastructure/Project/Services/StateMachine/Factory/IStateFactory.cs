using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Project.Services.StateMachine.Factory
{
    public interface IStateFactory
    {
        public T Create<T>() where T : IState;
    }
}