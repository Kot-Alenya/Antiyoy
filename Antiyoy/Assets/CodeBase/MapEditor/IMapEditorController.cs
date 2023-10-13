using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.MapEditor
{
    public interface IMapEditorController
    {
        public void CreateTilesMode(RegionType regionType);

        public void DestroyTilesMode();

        public void SelectTile(HexPosition hex);

        public void ProcessTiles();

        public void ReturnBack();

        public void ReturnNext();
    }
}