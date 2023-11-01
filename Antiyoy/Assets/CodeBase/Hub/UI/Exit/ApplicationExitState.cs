using CodeBase.Infrastructure.Services.StateMachine.States;
using UnityEditor;

namespace CodeBase.Hub.UI.Exit
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