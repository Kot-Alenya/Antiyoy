using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.WorldEditor.States;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Infrastructure.WorldEditor.UI
{
    public class WorldEditorLeaveButton : ButtonBase
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private protected override void OnClick() => _stateMachine.SwitchTo<WorldEditorLeaveState>();
    }
}