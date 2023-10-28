using CodeBase.Infrastructure.Hub;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.WorldEditor.States
{
    public class WorldEditorLeaveState : IEnterState
    {
        private readonly IStateMachine _stateMachine;

        public WorldEditorLeaveState(IStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter() => _stateMachine.SwitchTo<HubState>();
    }
}