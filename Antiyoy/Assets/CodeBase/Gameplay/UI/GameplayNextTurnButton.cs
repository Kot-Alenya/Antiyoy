using CodeBase.Infrastructure.Gameplay.States;
using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Gameplay.UI
{
    public class GameplayNextTurnButton : ButtonBase
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private protected override void OnClick() => _stateMachine.SwitchTo<GameplayNextTurnState>();
    }
}