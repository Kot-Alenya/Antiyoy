using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.MapEditor.Data;

namespace CodeBase.MapEditor
{
    public interface IMapEditorController
    {
        void SetMode(MapEditorMode mode);
        void SetRegionType(RegionType regionType);
        void SetEntityType(EntityType entityType);
        void SelectTile(HexPosition hex);
        void ProcessTiles();
        void ReturnBack();
        void ReturnNext();
        void SaveWorld();
    }
}