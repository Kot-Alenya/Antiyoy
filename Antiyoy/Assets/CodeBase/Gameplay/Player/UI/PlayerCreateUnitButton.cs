using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Utilities.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player.UI
{
    public class PlayerCreateUnitButton : ButtonBase
    {
        [SerializeField] private UnitType _unitType;
        private IPlayerUIMediator _uiMediator;

        [Inject]
        private void Construct(IPlayerUIMediator uiMediator) => _uiMediator = uiMediator;

        private protected override void OnClick() => _uiMediator.CreateUnit(_unitType);
    }
}