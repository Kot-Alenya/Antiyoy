using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Tile.Operation
{
    public class DestroyTileOperationHandler : IWorldVersionOperationHandler
    {
        private readonly ITerrain _terrain;

        public DestroyTileOperationHandler(ITerrain terrain) => _terrain = terrain;

        public void Apply(IWorldVersionOperationData data)
        {
            var applyData = (TileOperationData)data;

            _terrain.DestroyTile(_terrain.GetTile(applyData.Hex));
        }

        public void Revert(IWorldVersionOperationData data)
        {
            var revertData = (TileOperationData)data;

            _terrain.CreateTile(revertData.Hex, revertData.RegionType);
        }
    }
}