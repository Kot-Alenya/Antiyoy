﻿using CodeBase.Gameplay.Player.States;
using CodeBase.Gameplay.Player.States.Entity;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Utilities.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player.UI
{
    public class PlayerUIWindow : WindowBase, IPlayerUIMediator
    {
        [SerializeField] private TextMeshProUGUI _playerCoinsCount;
        [SerializeField] private TextMeshProUGUI _playerIncome;
        
        private PlayerStateMachine _playerStateMachine;

        [Inject]
        private void Construct(PlayerStateMachine playerStateMachine) => _playerStateMachine = playerStateMachine;

        public void Initialize() => HideUIWindow();

        public void ShowUIWindow()
        {
            base.Show();
            gameObject.SetActive(true);
        }

        public void HideUIWindow()
        {
            base.Hide();
            gameObject.SetActive(false);
        }

        public void SetCoinsCount(int coinsCount) => _playerCoinsCount.text = coinsCount.ToString();

        public void SetIncomeCount(int incomeCount) => _playerIncome.text = $"+ {incomeCount.ToString()}";

        public void CreateEntity(EntityType entityType) =>
            _playerStateMachine.SwitchTo<PlayerCreateEntityState, PlayerCreateEntityStateData>(
                new PlayerCreateEntityStateData(entityType));
    }
}