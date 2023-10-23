using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Region.Model;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainFactory
    {
        private const string TerrainRootName = "Terrain";

        private readonly IStaticDataProvider _staticDataProvider;
        private readonly RegionFactory _regionFactory;
        private readonly EntityFactory _entityFactory;
        private readonly DiContainer _container;

        public TerrainFactory(IStaticDataProvider staticDataProvider, RegionFactory regionFactory,
            DiContainer container)
        {
            _staticDataProvider = staticDataProvider;
            _regionFactory = regionFactory;
            _container = container;
        }

        public ITerrain Create()
        {
            var staticData = _staticDataProvider.Get<TerrainStaticData>();
            var root = new GameObject(TerrainRootName).transform;
            var tileCollection = new TileCollection(staticData.Size);
            var regionsModel = new RegionsModel(_regionFactory);
            var terrainModel = new TerrainModel(regionsModel, tileCollection);

            CreateBackground(root, staticData);

            _container.Bind<ITerrain>().FromInstance(terrainModel).AsSingle();

            var entityFactory = _container.Instantiate<EntityFactory>(new object[] { regionsModel, tileCollection });

            _container.Bind<IEntityFactory>().FromInstance(entityFactory).AsSingle();

            _container.Bind<ITileFactory>().To<TileFactory>().AsSingle()
                .WithArguments(root, tileCollection, regionsModel, entityFactory);

            return terrainModel;
        }

        private void CreateBackground(Transform root, TerrainStaticData terrainStaticData)
        {
            var instance = Object.Instantiate(terrainStaticData.BackgroundPrefabData, root);

            var maxArrayIndex = terrainStaticData.Size - Vector2Int.one;
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
            var position = new Vector3(center.x, center.y, instance.Transform.position.z);

            instance.Transform.localScale = scale;
            instance.Transform.position = position;
        }
    }
}