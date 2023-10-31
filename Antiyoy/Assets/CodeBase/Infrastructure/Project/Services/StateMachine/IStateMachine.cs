using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Project.Services.StateMachine
{
    public interface IStateMachine
    {
        public void SwitchTo<T>() where T : IEnterState;

        public void SwitchTo<TState, TParameter>(TParameter parameter) where TParameter : IStateParameter
            where TState : IEnterState<TParameter>;
    }
}