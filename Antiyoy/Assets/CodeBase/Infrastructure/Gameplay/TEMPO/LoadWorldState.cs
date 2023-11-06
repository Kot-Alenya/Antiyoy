using CodeBase.Gameplay.World.Progress;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public abstract class LoadWorldState : IEnterState
    {
        private readonly IProgressSaveLoader _progressSaveLoader;
        private readonly WorldProgressLoader _worldProgressLoader;
        private readonly WorldProgressSaver _worldProgressSaver;

        protected LoadWorldState(IProgressSaveLoader progressSaveLoader, WorldProgressLoader worldProgressLoader,
            WorldProgressSaver worldProgressSaver)
        {
            _progressSaveLoader = progressSaveLoader;
            _worldProgressLoader = worldProgressLoader;
            _worldProgressSaver = worldProgressSaver;
        }

        public virtual void Enter()
        {
            _progressSaveLoader.RegisterWatcher(_worldProgressLoader);
            _progressSaveLoader.RegisterWatcher(_worldProgressSaver);
            _progressSaveLoader.Load<WorldProgressData>("World");
        }
    }
}