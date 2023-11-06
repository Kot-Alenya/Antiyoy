using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Operation;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Operation;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World
{
    public class WorldFactory
    {
        private readonly ITerrain _terrain;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly TileVersionOperationFactory _tileVersionOperationFactory;
        private readonly UnitVersionOperationFactory _unitVersionOperationFactory;

        public WorldFactory(ITerrain terrain, WorldVersionRecorder worldVersionRecorder,
            TileVersionOperationFactory tileVersionOperationFactory,
            UnitVersionOperationFactory unitVersionOperationFactory)
        {
            _terrain = terrain;
            _worldVersionRecorder = worldVersionRecorder;
            _tileVersionOperationFactory = tileVersionOperationFactory;
            _unitVersionOperationFactory = unitVersionOperationFactory;
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

            TryDestroyUnit(hex);

            _worldVersionRecorder.AddToBuffer(_tileVersionOperationFactory.GetDestroyOperation(hex, tile.Region.Type));
            _terrain.DestroyTile(tile);
        }

        public void TryCreateUnit(HexPosition hex, UnitType unitType, bool isCanMove)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                return;

            TryDestroyUnit(hex);

            _terrain.CreateUnit(tile, unitType, isCanMove);
            _worldVersionRecorder.AddToBuffer(
                _unitVersionOperationFactory.GetCreateOperation(hex, unitType, isCanMove));
        }

        public void TryDestroyUnit(HexPosition hex)
        {
            if (!_terrain.TryGetTile(hex, out var tile))
                return;

            if (tile.Unit == null)
                return;

            _worldVersionRecorder.AddToBuffer(
                _unitVersionOperationFactory.GetDestroyOperation(hex, tile.Unit.Type, tile.Unit.IsCanMove));

            _terrain.DestroyUnit(tile.Unit);
        }
    }
}