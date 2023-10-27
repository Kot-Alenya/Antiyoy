﻿using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Region.Factory;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainFactory : ITerrainFactory
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

            BindRegion();
            BindTile(instance.transform);
            BindEntity();
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

        private void BindRegion()
        {
            _container.Bind<IRegionFactory>().To<RegionFactory>().AsSingle();
            _container.Bind<RegionCollection>().To<RegionCollection>().AsSingle();
            _container.Bind<IRegionRebuilder>().To<RegionRebuilder>().AsSingle();
            _container.Bind<RegionSplitter>().AsSingle();
            _container.Bind<RegionJoiner>().AsSingle();
            _container.Bind<RegionIncomeRebuilder>().AsSingle();
        }

        private void BindTile(Transform tileRoot)
        {
            _container.Bind<ITileCollection>().To<TileArray>().AsSingle();
            _container.Bind<ITileFactory>().To<TileFactory>().AsSingle().WithArguments(tileRoot);
        }

        private void BindEntity() => _container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
    }
}