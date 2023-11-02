using CodeBase.Gameplay.World.Progress;
using CodeBase.Infrastructure.Gameplay.States;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine;

namespace CodeBase.Infrastructure.WorldEditor.States
{
    public class WorldEditorLoadWorldState : LoadWorldState
    {
        private readonly IStateMachine _stateMachine;

        public WorldEditorLoadWorldState(IProgressSaveLoader progressSaveLoader,
            WorldProgressLoader worldProgressLoader, IStateMachine stateMachine,
            WorldProgressSaver worldProgressSaver) :
            base(progressSaveLoader, worldProgressLoader, worldProgressSaver) => _stateMachine = stateMachine;

        public override void Enter()
        {
            base.Enter();
            _stateMachine.SwitchTo<WorldEditorSetupState>();
        }
    }
}