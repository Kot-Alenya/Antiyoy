using CodeBase.Gameplay.Ecs;
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
        private readonly GameplayEcsWorld _gameplayEcsWorld;
        private readonly GameplayEcsSystems _gameplayEcsSystems;

        public GameplayLeaveState(IStateMachine stateMachine, IProgressSaveLoader progressSaveLoader,
            GameplayEcsWorld gameplayEcsWorld, GameplayEcsSystems gameplayEcsSystems)
        {
            _stateMachine = stateMachine;
            _progressSaveLoader = progressSaveLoader;
            _gameplayEcsWorld = gameplayEcsWorld;
            _gameplayEcsSystems = gameplayEcsSystems;
        }

        public void Enter()
        {
            _progressSaveLoader.ClearWatchers();

            _gameplayEcsWorld.Destroy();
            _gameplayEcsSystems.Dispose();
            
            _stateMachine.SwitchTo<HubLoadingState>();
        }
    }
}