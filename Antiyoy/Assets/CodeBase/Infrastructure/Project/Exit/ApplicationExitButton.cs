using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Infrastructure.Project.Exit
{
    public class ApplicationExitButton : ButtonBase
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private protected override void OnClick() => _stateMachine.SwitchTo<ApplicationExitState>();
    }
}