using System.Collections.Generic;
using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Gameplay.Region
{
    public class RegionController
    {
        private readonly GameplayEcsWorld _world;
        private EcsPool<TileComponent> _tilePool;

        public RegionController(RegionType type, Color color, GameplayEcsWorld world, List<int> entities)
        {
            _world = world;
            Type = type;
            Color = color;
            Entities = entities;
        }

        public void Initialize() => _tilePool = _world.GetPool<TileComponent>();

        public List<int> Entities { get; }

        public RegionType Type { get; }

        public Color Color { get; }

        public void MatchColors()
        {
            foreach (var entity in Entities)
                _tilePool.Get(entity).Controller.SetColor(Color);
        }

        public void Add(int entity)
        {
            Entities.Add(entity);
            _tilePool.Get(entity).Controller.SetColor(Color);
        }

        public void Remove(int entity)
        {
            Entities.Remove(entity);
        }
    }
}