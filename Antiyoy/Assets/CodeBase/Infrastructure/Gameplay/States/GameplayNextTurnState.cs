using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayNextTurnState : IEnterState
    {
        private readonly RegionCoinsCounter _regionCoinsCounter;
        private readonly IVersionRecorder _versionRecorder;
        private readonly PlayerStateMachine _playerStateMachine;

        public GameplayNextTurnState(RegionCoinsCounter regionCoinsCounter, IVersionRecorder versionRecorder,
            PlayerStateMachine playerStateMachine)
        {
            _regionCoinsCounter = regionCoinsCounter;
            _versionRecorder = versionRecorder;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _regionCoinsCounter.RecountAllRegions();
            _versionRecorder.ClearRecords();
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }
    }
}