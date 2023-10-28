using CodeBase.Infrastructure.GameLoading;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Project.Bootstrap
{
    public class BootstrapState : IEnterState
    {
        private readonly IStateMachine _stateMachine;

        public BootstrapState(IStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter() => _stateMachine.SwitchTo<GameLoadingState>();
    }
}