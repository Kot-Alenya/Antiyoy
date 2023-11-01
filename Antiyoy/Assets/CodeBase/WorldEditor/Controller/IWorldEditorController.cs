using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.WorldEditor.Data;

namespace CodeBase.WorldEditor.Controller
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