﻿using CodeBase.Gameplay.World.Version;

namespace CodeBase.Gameplay.World.Terrain.Entity.Operation
{
    public class DestroyEntityOperationHandler : IWorldVersionOperationHandler
    {
        private readonly ITerrain _terrain;

        public DestroyEntityOperationHandler(ITerrain terrain) => _terrain = terrain;

        public void Apply(IWorldVersionOperationData data)
        {
            var applyData = (EntityOperationData)data;

            _terrain.DestroyEntity(_terrain.GetTile(applyData.Hex).Entity);
        }

        public void Revert(IWorldVersionOperationData data)
        {
            var revertData = (EntityOperationData)data;

            _terrain.CreateEntity(_terrain.GetTile(revertData.Hex), revertData.EntityType);
        }
    }
}