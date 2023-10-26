using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.World.Terrain
{
    public interface IWorldTerrainController
    {
        public Vector2Int Size { get; }

        public bool IsInTerrain(HexPosition hex);

        public ITerrainTiles Tiles { get; } //tiles collection?

        public ITerrainRegions Regions { get; } //regions manager?

        //TILE
        public TileData GetTile(HexPosition hex);

        public bool TryGetTile(HexPosition hex, out TileData tile);

        public void SetTile(TileData tile, HexPosition hex, RegionType regionType);

        public void RemoveTile(TileData tile);
        //

        //ENTITY -
        public void SetEntity(EntityData entity, TileData rootTile);
        public void RemoveEntity(EntityData entity);

        //Regions
        public void AddToRegion(TileData tile, RegionType regionType);

        public void RemoveFromRegion(TileData tile);

        public void AddToRecalculateBuffer(RegionData region);

        public void RecalculateFromBufferAndClearBuffer();
    }

    public interface ITerrainManager
    {
        //регионы
        public void AddToRegion(TileData tile, RegionType regionType);

        public void RemoveFromRegion(TileData tile);

        public void AddToRecalculateBuffer(RegionData region);

        public void RecalculateFromBufferAndClearBuffer();

        //тайлы
        public Transform TilesRoot { get; }

        public bool IsInTerrain(HexPosition hex);

        public void Set(TileData tile, HexPosition hex, RegionType regionType);

        public void Remove(TileData tile);

        public TileData Get(HexPosition hex);

        public bool TryGet(HexPosition hex, out TileData tile);
    }

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
            var instance = CreateInstance(staticData);
            var tileCollection = new TileCollection(staticData.Size);

            _container.Bind<ITerrainEntities>().To<TerrainEntities>().AsSingle();
            _container.Bind<ITerrainRegions>().To<TerrainRegions>().AsSingle();
            _container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle().WithArguments(tileCollection);
            _container.Bind<ITerrainTiles>().To<TerrainTiles>().AsSingle()
                .WithArguments(tileCollection, instance.transform);
            _container.Bind<ITileFactory>().To<TileFactory>().AsSingle();

            return default;
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