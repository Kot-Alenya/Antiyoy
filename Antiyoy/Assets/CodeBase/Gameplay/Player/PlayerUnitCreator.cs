using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.States.Unit;
using CodeBase.Gameplay.Player.States.Unit.Create;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.Player.States
{
    public class PlayerUnitCreator
    {
        private readonly PlayerData _playerData;
        private readonly WorldFactory _worldFactory;
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly UnitStaticDataHelper _unitStaticDataHelper;

        public PlayerUnitCreator(PlayerData playerData, WorldFactory worldFactory,
            WorldVersionRecorder worldVersionRecorder, UnitStaticDataHelper unitStaticDataHelper)
        {
            _playerData = playerData;
            _worldFactory = worldFactory;
            _worldVersionRecorder = worldVersionRecorder;
            _unitStaticDataHelper = unitStaticDataHelper;
        }

        public List<TileData> GetTilesToCreateUnit(UnitType unitType) =>
            PlayerCreateUnitUtilities.GetTilesToCreateUnit(unitType, _playerData.SelectedRegion);

        public void CreateUnit(TileData tile, UnitType unitType)
        {
            if (tile.Unit != null &&
                PlayerCombineUnitUtilities.TryCombinedUnitType(unitType, tile.Unit.Type, out var type))
                CreateUnit_1(tile, type);
            else
                CreateUnit_1(tile, unitType);
        }

        private void CreateUnit_1(TileData tile, UnitType unitType)
        {
            var isUnitCanMoveAfterCreation = IsUnitCanMoveAfterCreation(tile, unitType);

            _worldFactory.CreateTile(tile.Hex, _playerData.RegionType);
            _worldFactory.TryCreateUnit(tile.Hex, unitType, isUnitCanMoveAfterCreation);
            _worldVersionRecorder.RecordFromBufferAndClearBuffer();
        }

        private bool IsUnitCanMoveAfterCreation(TileData tile, UnitType unitType)
        {
            if (!unitType.IsCombat())
                return false;

            if (tile.Region.Type != _playerData.RegionType)
                return false;

            if (tile.Unit == null)
                return true;

            if (tile.Unit.Type == UnitType.Tree)
                return false;

            if (tile.Unit.Type == UnitType.Grave)
                return false;

            if (tile.Unit.Type.IsCombat())
                return tile.Unit.IsCanMove;

            return true;
        }
    }
}