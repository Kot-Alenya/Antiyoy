using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Unit
{
    public class UnitFactory
    {
        private readonly UnitStaticDataHelper _staticDataHelper;

        public UnitFactory(UnitStaticDataHelper staticDataHelper) => _staticDataHelper = staticDataHelper;

        public UnitData Create(TileData rootTile, UnitType unitType, bool isCanMove)
        {
            var preset = _staticDataHelper.GetPreset(unitType);
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var unit = new UnitData(unitType, instance, rootTile, isCanMove);

            if (isCanMove)
                unit.Instance.Animator.CanMove();

            return unit;
        }

        public void Destroy(UnitData unit) => Object.Destroy(unit.Instance.gameObject);
    }
}