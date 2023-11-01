using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Infrastructure.Services.StateMachine.Factory;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStateMachine(IStateFactory factory) : base(factory)
        {
        }

        public IState GetCurrentState() => CurrentState;
    }
}