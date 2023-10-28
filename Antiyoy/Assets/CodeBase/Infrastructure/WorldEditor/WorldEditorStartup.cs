using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.WorldEditor.States;
using Zenject;

namespace CodeBase.Infrastructure.WorldEditor
{
    public class WorldEditorStartup : IInitializable
    {
        private readonly IStateMachine _stateMachine;

        public WorldEditorStartup(IStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Initialize() => _stateMachine.SwitchTo<WorldEditorStartupState>();
    }
}