using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayNextTurnState : IEnterState
    {
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly PlayerStateMachine _playerStateMachine;

        public GameplayNextTurnState(WorldVersionRecorder worldVersionRecorder,
            PlayerStateMachine playerStateMachine)
        {
            _worldVersionRecorder = worldVersionRecorder;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            //_regionCoinsCounter.RecountAllRegions();
            _worldVersionRecorder.ClearRecords();
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }
    }
}