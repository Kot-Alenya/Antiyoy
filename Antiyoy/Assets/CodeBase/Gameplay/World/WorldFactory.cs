using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Entity.Operation;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Operation;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World
{
    public class WorldFactory
    {
        private readonly ITerrain _terrain;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly TileVersionOperationFactory _tileVersionOperationFactory;
        private readonly EntityVersionOperationFactory _entityVersionOperationFactory;

        public WorldFactory(ITerrain terrain, WorldVersionRecorder worldVersionRecorder,
            TileVersionOperationFactory tileVersionOperationFactory,
            EntityVersionOperationFactory entityVersionOperationFactory)
        {
            _terrain = terrain;
            _worldVersionRecorder = worldVersionRecorder;
            _tileVersionOperationFactory = tileVersionOperationFactory;
            _entityVersionOperationFactory = entityVersionOperationFactory;
        }

        public void CreateTile(HexPosition hex, RegionType regionType)
        {
            TryDestroyTile(hex);

            _terrain.CreateTile(hex, regionType);
            _worldVersionRecorder.AddToBuffer(_tileVersionOperationFactory.GetCreateOperation(hex, regionType));
        }

        public void TryDestroyTile(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                return;

            TryDestroyEntity(hex);

            _worldVersionRecorder.AddToBuffer(_tileVersionOperationFactory.GetDestroyOperation(hex, tile.Region.Type));
            _terrain.DestroyTile(tile);
        }

        public void CreateEntity(HexPosition hex, EntityType entityType)
        {
            TryDestroyEntity(hex);

            _terrain.CreateEntity(_terrain.GetTile(hex), entityType);
            _worldVersionRecorder.AddToBuffer(
                _entityVersionOperationFactory.GetCreateOperation(hex, entityType));
        }

        public void TryDestroyEntity(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                return;

            if (tile.Entity == null)
                return;

            _worldVersionRecorder.AddToBuffer(
                _entityVersionOperationFactory.GetDestroyOperation(hex, tile.Entity.Type));

            _terrain.DestroyEntity(tile.Entity);
        }
    }
}