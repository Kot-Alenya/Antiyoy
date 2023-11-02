using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.World.Version;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Gameplay.UI
{
    public class GameplayReturnBackButton : ButtonBase
    {
        private WorldVersionManager _versionManager;
        private PlayerStateMachine _playerStateMachine;

        [Inject]
        private void Construct(WorldVersionManager versionManager, PlayerStateMachine playerStateMachine)
        {
            _versionManager = versionManager;
            _playerStateMachine = playerStateMachine;
        }

        private protected override void OnClick()
        {
            _versionManager.ReturnBack();
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }
    }
}