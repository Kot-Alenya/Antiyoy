using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Collection;
using CodeBase.Gameplay.World.Region.Factory;
using CodeBase.Gameplay.World.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Tile.Collection;
using CodeBase.Gameplay.World.Tile.Factory;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly DiContainer _container;

        public TerrainFactory(IStaticDataProvider staticDataProvider, DiContainer container)
        {
            _staticDataProvider = staticDataProvider;
            _container = container;
        }

        public void Create()
        {
            var staticData = _staticDataProvider.Get<TerrainStaticData>();
            var instance = CreateInstance(staticData);
            staticData.Initialize(instance);
            
            CreateRegion();

            _container.Bind<ITileCollection>().To<TileArray>().AsSingle();
            _container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
            _container.Bind<ITileFactory>().To<TileFactory>().AsSingle();
        }

        private void CreateRegion()
        {
            _container.Bind<IRegionFactory>().To<RegionFactory>().AsSingle();
            _container.Bind<RegionCollection>().To<RegionCollection>().AsSingle();
            _container.Bind<IRegionRebuilder>().To<RegionRebuilder>().AsSingle();
            _container.Bind<RegionSplitter>().AsSingle();
            _container.Bind<RegionJoiner>().AsSingle();
            _container.Bind<RegionIncomeRebuilder>().AsSingle();
        }

        private TerrainPrefabData CreateInstance(TerrainStaticData staticData)
        {
            var instance = Object.Instantiate(staticData.Prefab);
            StretchBackground(instance.BackgroundTransform, staticData);

            return instance;
        }

        private void StretchBackground(Transform backgroundTransform, TerrainStaticData staticData)
        {
            var maxArrayIndex = staticData.Size - Vector2Int.one;
            var halfTileSize = new Vector2(HexMath.InnerRadius, HexMath.OuterRadius);
            var lastPointOffset = maxArrayIndex.y % 2f == 0
                ? Vector2.right * HexMath.InnerRadius
                : Vector2.zero;

            var firstTileHex = HexMath.FromArrayIndex(Vector2Int.zero);
            var lastTileHex = HexMath.FromArrayIndex(maxArrayIndex);

            var firstPoint = HexMath.ToWorldPosition(firstTileHex) - halfTileSize;
            var lastPoint = HexMath.ToWorldPosition(lastTileHex) + halfTileSize + lastPointOffset;

            var scale = lastPoint - firstPoint;

            var center = firstPoint + scale / 2f;
            var position = new Vector3(center.x, center.y, backgroundTransform.position.z);

            backgroundTransform.localScale = scale;
            backgroundTransform.position = position;
        }
    }
}