using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Tile.Operation
{
    public class TileVersionOperationFactory
    {
        private readonly CreateTileOperationHandler _createTileOperationHandler;
        private readonly DestroyTileOperationHandler _destroyTileOperationHandler;

        public TileVersionOperationFactory(CreateTileOperationHandler createTileOperationHandler,
            DestroyTileOperationHandler destroyTileOperationHandler)
        {
            _createTileOperationHandler = createTileOperationHandler;
            _destroyTileOperationHandler = destroyTileOperationHandler;
        }

        public TileOperationData GetCreateOperation(HexPosition hex, RegionType regionType) =>
            new(hex, regionType, _createTileOperationHandler);

        public TileOperationData GetDestroyOperation(HexPosition hex, RegionType regionType) =>
            new(hex, regionType, _destroyTileOperationHandler);
    }
}