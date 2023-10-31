using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStateMachine(IStateFactory factory) : base(factory)
        {
        }
    }
}