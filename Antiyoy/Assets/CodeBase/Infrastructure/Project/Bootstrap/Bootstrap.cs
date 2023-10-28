using CodeBase.Infrastructure.GameLoading;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Project.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private void Start() => _stateMachine.SwitchTo<GameLoadingState>();
    }
}