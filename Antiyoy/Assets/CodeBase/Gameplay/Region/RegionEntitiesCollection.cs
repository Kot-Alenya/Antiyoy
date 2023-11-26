using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Tile.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Gameplay.Region
{
    public class RegionEntitiesCollection : IEnumerable<int>
    {
        private readonly GameplayEcsWorld _world;
        private readonly List<int> _entities;
        private EcsPool<TileComponent> _tilePool;

        public RegionEntitiesCollection(Color color, List<int> entities, GameplayEcsWorld world)
        {
            Color = color;
            _world = world;
            _entities = entities;
        }

        public int Count => _entities.Count;

        public Color Color { get; }

        public int this[int index]
        {
            get => _entities[index];
            set => _entities[index] = value;
        }

        public void Initialize() => _tilePool = _world.GetPool<TileComponent>();

        public void Add(int entity) => _entities.Add(entity);

        public void AddAndMatchColor(int entity)
        {
            _entities.Add(entity);
            _tilePool.Get(entity).Controller.SetColor(Color);
        }

        public void MatchColorsAll()
        {
            foreach (var entity in _entities)
                _tilePool.Get(entity).Controller.SetColor(Color);
        }

        public bool Remove(int item) => _entities.Remove(item);

        public bool Contains(int item) => _entities.Contains(item);

        public void Clear() => _entities.Clear();

        public IEnumerator<int> GetEnumerator() => _entities.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}