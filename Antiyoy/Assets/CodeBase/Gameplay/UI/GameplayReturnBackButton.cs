using CodeBase.Gameplay.Player.States;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Gameplay.UI
{
    public class GameplayReturnBackButton : ButtonBase
    {
        private IVersionHandler _versionHandler;
        private PlayerStateMachine _playerStateMachine;

        [Inject]
        private void Construct(IVersionHandler versionHandler, PlayerStateMachine playerStateMachine)
        {
            _versionHandler = versionHandler;
            _playerStateMachine = playerStateMachine;
        }

        private protected override void OnClick()
        {
            _versionHandler.ReturnBack();
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }
    }
}