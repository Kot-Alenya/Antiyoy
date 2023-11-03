using CodeBase.Gameplay.Ecs;
using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayNextTurnState : IEnterState
    {
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly GameplayEcsSystems _systems;

        public GameplayNextTurnState(WorldVersionRecorder worldVersionRecorder,
            PlayerStateMachine playerStateMachine,GameplayEcsSystems systems)
        {
            _worldVersionRecorder = worldVersionRecorder;
            _playerStateMachine = playerStateMachine;
            _systems = systems;
        }

        public void Enter()
        {
            _systems.Run();
            _worldVersionRecorder.ClearRecords();
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }
    }
}