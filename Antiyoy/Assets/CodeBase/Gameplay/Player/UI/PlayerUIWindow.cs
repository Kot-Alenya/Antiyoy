using CodeBase.Utilities.UI;
using TMPro;
using UnityEngine;

namespace CodeBase.Gameplay.Player.UI
{
    public class PlayerUIWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _playerCoinsCount;
        [SerializeField] private TextMeshProUGUI _playerIncome;

        public void Initialize() => Hide();

        public override void Show()
        {
            base.Show();
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            base.Show();
            gameObject.SetActive(false);
        }

        public void SetCoinsCount(int coinsCount) => _playerCoinsCount.text = coinsCount.ToString();

        public void SetIncomeCount(int incomeCount) => _playerIncome.text = $"+ {incomeCount.ToString()}";
    }
}