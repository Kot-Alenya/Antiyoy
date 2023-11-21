using CodeBase.Gameplay.Infrastructure.States;
using CodeBase.Infrastructure.ProjectStateMachine;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Infrastructure
{
    public class GameplayStartup : MonoBehaviour
    {
        private StateMachine _stateMachine;

        [Inject]
        private void Constructor(StateMachine stateMachine) => _stateMachine = stateMachine;

        private void Start() => _stateMachine.SwitchTo<GameplayStartupState>();
    }
}