using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor
{
    public interface IWorldEditorController
    {
        void SetMode(WorldEditorMode mode);
        void SetRegionType(RegionType regionType);
        void SetEntityType(EntityType entityType);
        void SelectTile(HexPosition hex);
        void ProcessTiles();
        void ReturnBack();
        void ReturnNext();
        void SaveWorld();
    }
}