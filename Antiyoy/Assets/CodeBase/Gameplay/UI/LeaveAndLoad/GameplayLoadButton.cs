using CodeBase.Infrastructure.Gameplay.States;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Gameplay.UI.LeaveAndLoad
{
    public class GameplayLoadButton : ButtonBase
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private protected override void OnClick() => _stateMachine.SwitchTo<GameplayState>();
    }
}