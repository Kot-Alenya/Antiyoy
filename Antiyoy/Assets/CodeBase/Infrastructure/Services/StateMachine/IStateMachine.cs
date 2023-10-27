﻿using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Services.StateMachine
{
    public interface IStateMachine
    {
        public void SwitchTo<T>() where T : IState;
    }
}