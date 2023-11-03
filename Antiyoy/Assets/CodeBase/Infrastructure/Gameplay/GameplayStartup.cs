using CodeBase.Infrastructure.Gameplay.States;
using CodeBase.Infrastructure.Services.StateMachine;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay
{
    public class GameplayStartup : IInitializable
    {
        private readonly IStateMachine _stateMachine;

        public GameplayStartup(IStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Initialize() => _stateMachine.SwitchTo<GameplayLoadWorldState>();
    }
}