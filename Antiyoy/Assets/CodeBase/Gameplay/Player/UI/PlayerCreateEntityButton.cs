using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Utilities.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player.UI
{
    public class PlayerCreateEntityButton : ButtonBase
    {
        [SerializeField] private EntityType _entityType;
        private IPlayerUIMediator _uiMediator;

        [Inject]
        private void Construct(IPlayerUIMediator uiMediator) => _uiMediator = uiMediator;

        private protected override void OnClick() => _uiMediator.CreateEntity(_entityType);
    }
}