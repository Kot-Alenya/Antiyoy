using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Project.Services.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly IStateFactory _factory;
        private protected IState CurrentState;

        public StateMachine(IStateFactory factory) => _factory = factory;

        public void SwitchTo<T>() where T : IEnterState
        {
            if (CurrentState is IExitState exitState)
                exitState.Exit();

            var state = _factory.Create<T>();
            state.Enter();

            CurrentState = state;
        }

        public void SwitchTo<TState, TParameter>(TParameter parameter) where TState : IEnterState<TParameter>
            where TParameter : IStateParameter
        {
            if (CurrentState is IExitState exitState)
                exitState.Exit();

            var state = _factory.Create<TState>();
            state.Enter(parameter);

            CurrentState = state;
        }
    }
}