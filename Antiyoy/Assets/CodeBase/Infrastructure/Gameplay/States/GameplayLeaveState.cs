using CodeBase.Infrastructure.Hub;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayLeaveState : IEnterState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IProgressSaveLoader _progressSaveLoader;

        public GameplayLeaveState(IStateMachine stateMachine, IProgressSaveLoader progressSaveLoader)
        {
            _stateMachine = stateMachine;
            _progressSaveLoader = progressSaveLoader;
        }

        public void Enter()
        {
            _progressSaveLoader.ClearWatchers();
            _stateMachine.SwitchTo<HubLoadingState>();
        }
    }
}