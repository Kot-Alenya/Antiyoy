﻿using CodeBase.Infrastructure.Services.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.Services.StateMachine.Factory
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container) => _container = container;

        public T Create<T>() where T : IState
        {
            var state = _container.Instantiate<T>();

            return state;
        }
    }
}