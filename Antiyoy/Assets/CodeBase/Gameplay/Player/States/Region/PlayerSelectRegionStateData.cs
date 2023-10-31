﻿using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Region
{
    public readonly struct PlayerSelectRegionStateData : IStateParameter
    {
        public readonly RegionData Region;

        public PlayerSelectRegionStateData(RegionData region) => Region = region;
    }
}