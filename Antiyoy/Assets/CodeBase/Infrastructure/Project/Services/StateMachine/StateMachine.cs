using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Project.Services.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly IStateFactory _factory;
        private IState _currentState;

        public StateMachine(IStateFactory factory) => _factory = factory;

        public void SwitchTo<T>() where T : IState
        {
            if (_currentState is IExitState exitState)
                exitState.Exit();

            _currentState = _factory.Create<T>();

            if (_currentState is IEnterState enterState)
                enterState.Enter();
        }
    }
}