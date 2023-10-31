using CodeBase.Gameplay.Terrain.Entity.Data;

namespace CodeBase.Gameplay.Player.Controller
{
    public interface IPlayerUIMediator
    {
        public void ShowUIWindow();

        public void HideUIWindow();

        public void SetCoinsCount(int coinsCount);

        public void SetIncomeCount(int incomeCount);

        public void CreateEntity(EntityType entityType);
    }
}