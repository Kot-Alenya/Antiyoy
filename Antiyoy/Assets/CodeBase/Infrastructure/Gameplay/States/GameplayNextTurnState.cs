using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.World.Terrain.Region.Rebuilder;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayNextTurnState : IEnterState
    {
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly RegionCoinsRebuilder _regionCoinsRebuilder;

        public GameplayNextTurnState(WorldVersionRecorder worldVersionRecorder,
            PlayerStateMachine playerStateMachine, RegionCoinsRebuilder regionCoinsRebuilder)
        {
            _worldVersionRecorder = worldVersionRecorder;
            _playerStateMachine = playerStateMachine;
            _regionCoinsRebuilder = regionCoinsRebuilder;
        }

        public void Enter()
        {
            _regionCoinsRebuilder.RebuildAll();
            _worldVersionRecorder.ClearRecords();
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }
    }
}