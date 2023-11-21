using Leopotam.EcsLite;
using Zenject;

namespace CodeBase.Gameplay.CommonEcs
{
    public class GameplayEcsSystemFactory
    {
        private readonly IInstantiator _instantiator;

        private GameplayEcsSystemFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public T Create<T>() where T : IEcsSystem => _instantiator.Instantiate<T>();
    }
}