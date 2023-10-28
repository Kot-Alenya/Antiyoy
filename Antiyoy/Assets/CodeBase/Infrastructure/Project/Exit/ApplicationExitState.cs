using CodeBase.Infrastructure.Project.Services.StateMachine.States;
using UnityEditor;

namespace CodeBase.Infrastructure.Project.Exit
{
    public class ApplicationExitState : IEnterState
    {
        public void Enter()
#if UNITY_EDITOR
            => EditorApplication.isPlaying = false;
#else
            => UnityEngine.Application.Quit();
#endif
    }
}