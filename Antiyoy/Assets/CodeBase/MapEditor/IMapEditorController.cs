using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.MapEditor.Data;
using CodeBase.MapEditor.UI;

namespace CodeBase.MapEditor
{
    public interface IMapEditorController
    {
        public void SetMode(MapEditorMode mode);

        public void SetRegionType(RegionType regionType);
        
        public void SetEntityType(EntityType entityType);
        
        public void SelectTile(HexPosition hex);

        public void ProcessTiles();

        public void ReturnBack();

        public void ReturnNext();
    }
}