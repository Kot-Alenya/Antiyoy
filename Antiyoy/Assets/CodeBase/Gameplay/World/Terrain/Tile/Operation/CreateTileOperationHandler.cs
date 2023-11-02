using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Tile.Operation
{
    public class CreateTileOperationHandler : IWorldVersionOperationHandler
    {
        private readonly ITerrain _terrain;

        public CreateTileOperationHandler(ITerrain terrain) => _terrain = terrain;

        public void Apply(IWorldVersionOperationData data)
        {
            var applyData = (TileOperationData)data;

            _terrain.CreateTile(applyData.Hex, applyData.RegionType);
        }

        public void Revert(IWorldVersionOperationData data)
        {
            var revertData = (TileOperationData)data;

            _terrain.DestroyTile(_terrain.GetTile(revertData.Hex));
        }
    }
}