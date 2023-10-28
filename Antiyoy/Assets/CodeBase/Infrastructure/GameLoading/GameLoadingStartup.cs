using CodeBase.Infrastructure.GameHub;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameLoading
{
    public class GameLoadingStartup : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => _stateMachine = stateMachine;

        private void Start() => _stateMachine.SwitchTo<GameHubLoadingState>();
    }
}