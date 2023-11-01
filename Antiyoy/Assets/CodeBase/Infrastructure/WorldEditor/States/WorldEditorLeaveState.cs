﻿using CodeBase.Infrastructure.Hub;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.WorldEditor.States
{
    public class WorldEditorLeaveState : IEnterState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IProgressSaveLoader _progressSaveLoader;

        public WorldEditorLeaveState(IStateMachine stateMachine, IProgressSaveLoader progressSaveLoader)
        {
            _stateMachine = stateMachine;
            _progressSaveLoader = progressSaveLoader;
        }

        public void Enter()
        {
            _progressSaveLoader.ClearWatchers();
            _stateMachine.SwitchTo<HubLoadingState>();
        }
    }
}