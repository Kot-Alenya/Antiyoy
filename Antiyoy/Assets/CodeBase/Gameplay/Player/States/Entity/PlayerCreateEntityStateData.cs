using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public readonly struct PlayerCreateEntityStateData : IStateParameter
    {
        public readonly EntityType EntityType;

        public PlayerCreateEntityStateData(EntityType entityType) => EntityType = entityType;
    }
}