using CodeBase.Gameplay.World.Progress;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayLoadWorldState : IEnterState
    {
        private readonly IProgressSaveLoader _progressSaveLoader;
        private readonly WorldProgressLoader _worldProgressLoader;
        private readonly WorldProgressSaver _worldProgressSaver;
        private readonly IStateMachine _stateMachine;

        protected GameplayLoadWorldState(IProgressSaveLoader progressSaveLoader,
            WorldProgressLoader worldProgressLoader, WorldProgressSaver worldProgressSaver, IStateMachine stateMachine)
        {
            _progressSaveLoader = progressSaveLoader;
            _worldProgressLoader = worldProgressLoader;
            _worldProgressSaver = worldProgressSaver;
            _stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            _progressSaveLoader.RegisterWatcher(_worldProgressLoader);
            _progressSaveLoader.RegisterWatcher(_worldProgressSaver);
            _progressSaveLoader.Load<WorldProgressData>("World");
            _stateMachine.SwitchTo<GameplayStartupState>();
        }
    }
}