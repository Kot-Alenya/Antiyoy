using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using Leopotam.EcsLite;

namespace CodeBase.Dev.TEMPO
{
    //думаем в ecs ключе...
    //компоненты
    //сущности 
    //системы.
    
    //мне нужне ecs для бработки сущностей.
    //системы должны заменить ребилдеры.
    
    //начинаем с тайла.
    //1 что мы делаем - создаём тайл.
    //тайл создаётся в фабрике.
    //при создании тайла 
    
    public struct TileComponent
    {
        public readonly List<TileComponent> Neighbors;
        public readonly TilePrefabData Instance;
        public readonly HexPosition Hex;

        public RegionData Region;
        public UnitData Unit;
        public int ProtectionLevel;
    }
    
    
    class WeaponSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world;
        EcsFilter _filter;
        EcsPool<Weapon> _weapons;

        public WeaponSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Init(IEcsSystems systems)
        {
            // Получаем экземпляр мира по умолчанию.
            EcsWorld world = _world;

            // Мы хотим получить все сущности с компонентом "Weapon" и без компонента "Health".
            // Фильтр хранит только сущности, сами даные лежат в пуле компонентов "Weapon".
            // Фильтр может собираться динамически каждый раз, но рекомендуется кеширование.
            _filter = world.Filter<Weapon>().Exc<Health>().End();

            // Запросим и закешируем пул компонентов "Weapon".
            _weapons = world.GetPool<Weapon>();

            // Создаем новую сущность для теста.
            int entity = world.NewEntity();

            // И добавляем к ней компонент "Weapon" - эта сущность должна попасть в фильтр.
            _weapons.Add(entity);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref Weapon weapon = ref _weapons.Get(entity);
                weapon.Ammo = System.Math.Max(0, weapon.Ammo - 1);
            }
        }
    }

    internal struct Health
    {
    }

    internal struct Weapon
    {
        public int Ammo { get; set; }
    }
}