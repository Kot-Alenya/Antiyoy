using CodeBase.Infrastructure.Hub;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayLeaveState : IEnterState
    {
        private readonly IStateMachine _stateMachine;

        public GameplayLeaveState(IStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter() => _stateMachine.SwitchTo<HubState>();
    }
}