using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.States.Unit;
using CodeBase.Gameplay.Player.States.Unit.Move;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerUnitMover
    {
        private readonly PlayerData _playerData;
        private readonly WorldFactory _worldFactory;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly UnitStaticDataHelper _unitStaticDataHelper;

        public PlayerUnitMover(PlayerData playerData, WorldFactory worldFactory,
            WorldVersionRecorder worldVersionRecorder, UnitStaticDataHelper unitStaticDataHelper)
        {
            _playerData = playerData;
            _worldFactory = worldFactory;
            _worldVersionRecorder = worldVersionRecorder;
            _unitStaticDataHelper = unitStaticDataHelper;
        }

        public List<TileData> GetTilesToMoveUnit(UnitData unit) =>
            PlayerMoveUnitUtilities.GetTilesToMoveUnit(unit, _unitStaticDataHelper);

        public void MoveUnit(TileData tile)
        {
            if (tile.Unit != null &&
                PlayerCombineUnitUtilities.TryCombinedUnitType(_playerData.SelectedUnit.Type, tile.Unit.Type, out var type))
                MoveUnit(tile, type, tile.Unit.IsCanMove);
            else
                MoveUnit(tile, _playerData.SelectedUnit.Type, false);
        }

        private void MoveUnit(TileData toTile, UnitType unitToCreateType, bool isCanMove)
        {
            _worldFactory.CreateTile(toTile.Hex, _playerData.RegionType);
            _worldFactory.TryCreateUnit(toTile.Hex, unitToCreateType, isCanMove);
            _worldFactory.TryDestroyUnit(_playerData.SelectedUnit.RootTile.Hex);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }
    }
}