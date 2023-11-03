using CodeBase.Gameplay.World.Progress;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayLoadWorldState : LoadWorldState
    {
        private readonly IStateMachine _stateMachine;

        public GameplayLoadWorldState(IProgressSaveLoader progressSaveLoader, WorldProgressLoader worldProgressLoader,
            WorldProgressSaver worldProgressSaver, IStateMachine stateMachine) : base(progressSaveLoader,
            worldProgressLoader, worldProgressSaver) =>
            _stateMachine = stateMachine;

        public override void Enter()
        {
            base.Enter();
            _stateMachine.SwitchTo<GameplayStartupState>();
        }
    }
}