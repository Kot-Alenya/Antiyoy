using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Entity.Operation
{
    public class DestroyUnitOperationHandler : IWorldVersionOperationHandler
    {
        private readonly ITerrain _terrain;

        public DestroyUnitOperationHandler(ITerrain terrain) => _terrain = terrain;

        public void Apply(IWorldVersionOperationData data)
        {
            var applyData = (UnitOperationData)data;

            _terrain.DestroyUnit(_terrain.GetTile(applyData.Hex).Unit);
        }

        public void Revert(IWorldVersionOperationData data)
        {
            var revertData = (UnitOperationData)data;

            _terrain.CreateUnit(_terrain.GetTile(revertData.Hex), revertData.UnitType);
        }
    }
}