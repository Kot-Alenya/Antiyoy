using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.Controller
{
    public interface IWorldEditorController
    {
        public void SetMode(WorldEditorMode mode);

        public void SetRegionType(RegionType regionType);

        public void SetUnitType(UnitType unitType);

        public void SelectTile(HexPosition hex);

        public void ProcessTiles();

        public void ReturnBack();

        public void ReturnNext();

        public void SaveWorld();
    }
}