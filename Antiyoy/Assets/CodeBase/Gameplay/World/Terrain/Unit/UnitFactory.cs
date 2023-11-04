using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Unit
{
    public class UnitFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public UnitFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public UnitData Create(TileData rootTile, UnitType unitType, bool isCanMove)
        {
            var config = _staticDataProvider.Get<UnitsConfig>();
            var preset = config.Presets[unitType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var unit = new UnitData
            {
                Instance = instance,
                Preset = preset,
                RootTile = rootTile,
                Type = unitType,
                IsCanMove = isCanMove
            };

            if (isCanMove)
                unit.Instance.Animator.CanMove();
            
            return unit;
        }

        public void Destroy(UnitData unit)
        {
            unit.RootTile.Unit = default;
            Object.Destroy(unit.Instance.gameObject);
        }
    }
}