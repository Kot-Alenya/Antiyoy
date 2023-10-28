using CodeBase.Infrastructure.Hub;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Infrastructure.WorldEditor.States
{
    public class WorldEditorLeaveState : IEnterState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IProgressSaveLoader _progressSaveLoader;

        public WorldEditorLeaveState(IStateMachine stateMachine, IProgressSaveLoader progressSaveLoader)
        {
            _stateMachine = stateMachine;
            _progressSaveLoader = progressSaveLoader;
        }

        public void Enter()
        {
            _progressSaveLoader.ClearWatchers();
            _stateMachine.SwitchTo<HubState>();
        }
    }
}