﻿using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Utilities.UI;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class LoadWorldEditorButton : ButtonBase
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private protected override void OnClick() => _stateMachine.SwitchTo<WorldEditorState>();
    }
}