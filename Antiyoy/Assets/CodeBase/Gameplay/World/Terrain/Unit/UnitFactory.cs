using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Entity
{
    public class UnitFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public UnitFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public UnitData Create(TileData rootTile, UnitType unitType)
        {
            var config = _staticDataProvider.Get<UnitsConfig>();
            var preset = config.Presets[unitType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var entity = new UnitData(instance, rootTile, unitType, preset, true);

            return entity;
        }

        public void Destroy(UnitData unit) => Object.Destroy(unit.Instance.gameObject);
    }
}