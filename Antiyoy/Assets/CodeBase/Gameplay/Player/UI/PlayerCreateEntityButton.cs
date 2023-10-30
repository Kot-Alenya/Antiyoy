using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Utilities.UI;
using UnityEngine;

namespace CodeBase.Gameplay.Player.UI
{
    public class PlayerCreateEntityButton : ButtonBase
    {
        [SerializeField] private EntityType _entityType;

        private protected override void OnClick()
        {
        }
    }
}