using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Terrain.Tile;
using CodeBase.Infrastructure.Services.StaticData;
using Zenject;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly TileFactory _tileFactory;

        public TerrainFactory(DiContainer container, IStaticDataProvider staticDataProvider, TileFactory tileFactory)
        {
            _container = container;
            _staticDataProvider = staticDataProvider;
            _tileFactory = tileFactory;
        }

        public void Create()
        {
            var config = _staticDataProvider.Get<TerrainConfig>();
            var instance = _container.InstantiatePrefab(config.Prefab);
            var tileArray = new TileArray(config.Size);
            
            _container.Bind<ITerrain>().To<TerrainObject>().AsSingle().WithArguments(tileArray);
            
            _tileFactory.Initialize(instance.transform);
        }
    }
}