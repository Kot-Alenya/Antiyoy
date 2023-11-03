using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Entity.Operation
{
    public class CreateUnitOperationHandler : IWorldVersionOperationHandler
    {
        private readonly ITerrain _terrain;

        public CreateUnitOperationHandler(ITerrain terrain) => _terrain = terrain;

        public void Apply(IWorldVersionOperationData data)
        {
            var applyData = (UnitOperationData)data;

            _terrain.CreateUnit(_terrain.GetTile(applyData.Hex), applyData.UnitType);
        }

        public void Revert(IWorldVersionOperationData data)
        {
            var revertData = (UnitOperationData)data;

            _terrain.DestroyUnit(_terrain.GetTile(revertData.Hex).Unit);
        }
    }
}