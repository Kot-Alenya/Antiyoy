using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Version.Recorder;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayNextTurnState : IEnterState
    {
        private readonly RegionCoinsCounter _regionCoinsCounter;
        private readonly IWorldVersionRecorder _worldVersionRecorder;
        private readonly PlayerStateMachine _playerStateMachine;

        public GameplayNextTurnState(RegionCoinsCounter regionCoinsCounter, IWorldVersionRecorder worldVersionRecorder,
            PlayerStateMachine playerStateMachine)
        {
            _regionCoinsCounter = regionCoinsCounter;
            _worldVersionRecorder = worldVersionRecorder;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _regionCoinsCounter.RecountAllRegions();
            _worldVersionRecorder.ClearRecords();
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }
    }
}