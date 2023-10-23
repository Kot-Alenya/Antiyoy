using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Version.Operation;

namespace CodeBase.Gameplay.World.Version.Modules
{
    public class VersionHandler
    {
        private readonly IWorldTerrainController _terrainController;

        public VersionHandler(IWorldTerrainController terrainController) => _terrainController = terrainController;

        public void Revert(IWorldOperationData operation)
        {
            switch (operation)
            {
                case WorldCreateTileOperationData data:
                    _terrainController.TryDestroyTile(data.Hex);
                    break;
                case WorldDestroyTileOperationData data:
                    _terrainController.TryCreateTile(data.Hex, data.RegionType);
                    break;
                case WorldCreateEntityOperationData data:
                    _terrainController.TryDestroyEntity(data.Hex);
                    break;
                case WorldDestroyEntityOperationData data:
                    _terrainController.TryCreateEntity(data.Hex, data.EntityType);
                    break;
            }

            _terrainController.RecalculateChangedRegions();
        }

        public void Apply(IWorldOperationData operation)
        {
            switch (operation)
            {
                case WorldCreateTileOperationData data:
                    _terrainController.TryCreateTile(data.Hex, data.RegionType);
                    break;
                case WorldDestroyTileOperationData data:
                    _terrainController.TryDestroyTile(data.Hex);
                    break;
                case WorldCreateEntityOperationData data:
                    _terrainController.TryCreateEntity(data.Hex, data.EntityType);
                    break;
                case WorldDestroyEntityOperationData data:
                    _terrainController.TryDestroyEntity(data.Hex);
                    break;
            }

            _terrainController.RecalculateChangedRegions();
        }
    }
}