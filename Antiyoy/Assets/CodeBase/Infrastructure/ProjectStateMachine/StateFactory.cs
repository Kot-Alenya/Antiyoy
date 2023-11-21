using Zenject;

namespace CodeBase.Infrastructure.ProjectStateMachine
{
    public class StateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public IState Create<T>() where T : IState => _instantiator.Instantiate<T>();
    }
}