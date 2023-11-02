using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Infrastructure.Services.StaticData;
using Zenject;

namespace CodeBase.Gameplay.World.NewTerrain
{
    public class TerrainFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataProvider _staticDataProvider;

        public TerrainFactory(DiContainer container, IStaticDataProvider staticDataProvider)
        {
            _container = container;
            _staticDataProvider = staticDataProvider;
        }

        public void Create()
        {
            var config = _staticDataProvider.Get<TerrainConfig>();
            var terrain = _container.Instantiate<TerrainObject>();

        }
    }
}