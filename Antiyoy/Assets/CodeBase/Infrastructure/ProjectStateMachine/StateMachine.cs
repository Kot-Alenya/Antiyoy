namespace CodeBase.ProjectInfrastructure.ProjectStateMachine
{
    public class StateMachine
    {
        private readonly StateFactory _stateFactory;
        private IState _currentState;

        public StateMachine(StateFactory stateFactory) => _stateFactory = stateFactory;

        public void SwitchTo<T>() where T : IState
        {
            if (_currentState is IExitState exitState)
                exitState.Exit();

            _currentState = _stateFactory.Create<T>();

            if (_currentState is IEnterState enterState)
                enterState.Enter();
        }
    }
}