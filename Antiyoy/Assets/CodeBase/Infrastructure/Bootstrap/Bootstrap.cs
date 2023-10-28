using CodeBase.Infrastructure.Bootstrap.GameLoading;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private void Start() => _stateMachine.SwitchTo<GameLoadingState>();
    }
}