using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Gameplay.World.Version.Operation;

namespace CodeBase.Gameplay.World.Version.Modules
{
    public class VersionHandler
    {
        private readonly ITileFactory _tileFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly ITerrainRegions _terrainRegions;

        public VersionHandler(ITileFactory tileFactory, IEntityFactory entityFactory, ITerrainRegions terrainRegions)
        {
            _tileFactory = tileFactory;
            _entityFactory = entityFactory;
            _terrainRegions = terrainRegions;
        }

        public void Revert(IWorldOperationData operation)
        {
            switch (operation)
            {
                case WorldCreateTileOperationData data:
                    _tileFactory.TryDestroy(data.Hex);
                    break;
                case WorldDestroyTileOperationData data:
                    _tileFactory.TryCreate(data.Hex, data.RegionType);
                    break;
                case WorldCreateEntityOperationData data:
                    _entityFactory.TryDestroy(data.Hex);
                    break;
                case WorldDestroyEntityOperationData data:
                    _entityFactory.TryCreate(data.Hex, data.EntityType);
                    break;
            }

            _terrainRegions.RecalculateFromBufferAndClearBuffer();
        }

        public void Apply(IWorldOperationData operation)
        {
            switch (operation)
            {
                case WorldCreateTileOperationData data:
                    _tileFactory.TryCreate(data.Hex, data.RegionType);
                    break;
                case WorldDestroyTileOperationData data:
                    _tileFactory.TryDestroy(data.Hex);
                    break;
                case WorldCreateEntityOperationData data:
                    _entityFactory.TryCreate(data.Hex, data.EntityType);
                    break;
                case WorldDestroyEntityOperationData data:
                    _entityFactory.TryDestroy(data.Hex);
                    break;
            }

            _terrainRegions.RecalculateFromBufferAndClearBuffer();
        }
    }
}