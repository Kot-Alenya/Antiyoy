using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Infrastructure.Level
{
    public class LoadGameplayButton : ButtonBase
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private protected override void OnClick() => _stateMachine.SwitchTo<GameplayState>();
    }
}