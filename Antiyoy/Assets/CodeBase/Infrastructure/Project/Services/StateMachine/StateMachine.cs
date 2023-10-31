using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Project.Services.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly IStateFactory _factory;
        private IState _currentState;

        public StateMachine(IStateFactory factory) => _factory = factory;

        public void SwitchTo<T>() where T : IEnterState
        {
            if (_currentState is IExitState exitState)
                exitState.Exit();

            var state = _factory.Create<T>();
            state.Enter();

            _currentState = state;
        }

        public void SwitchTo<TState, TParameter>(TParameter parameter) where TState : IEnterState<TParameter>
            where TParameter : IStateParameter
        {
            if (_currentState is IExitState exitState)
                exitState.Exit();

            var state = _factory.Create<TState>();
            state.Enter(parameter);

            _currentState = state;
        }
    }
}