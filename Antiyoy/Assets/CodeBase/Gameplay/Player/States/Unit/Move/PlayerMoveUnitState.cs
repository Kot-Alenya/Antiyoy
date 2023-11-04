using System.Collections.Generic;
using CodeBase.Gameplay.Player.Input;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Gameplay.Player.States.Unit.Move
{
    public class PlayerMoveUnitState : IEnterState<PlayerMoveUnitStateData>, IExitState
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerTerrainFocus _playerTerrainFocus;
        private List<TileData> _unitTilesToMove;

        public PlayerMoveUnitState(IPlayerInput playerInput, PlayerStateMachine playerStateMachine,
            PlayerTerrainFocus playerTerrainFocus)
        {
            _playerInput = playerInput;
            _playerStateMachine = playerStateMachine;
            _playerTerrainFocus = playerTerrainFocus;
        }

        public void Enter(PlayerMoveUnitStateData parameter)
        {
            _unitTilesToMove = GetUnitTilesToMove(parameter.Unit);

            _playerTerrainFocus.ShowShadowField();
            _playerTerrainFocus.SetTilesAboveShadowFiled(_unitTilesToMove);
            _playerTerrainFocus.ShowTilesOutline(_unitTilesToMove);

            _playerInput.OnPlayerInput += HandleInput;
        }

        public void Exit()
        {
            _playerTerrainFocus.HideShadowField();
            _playerTerrainFocus.SetAllTilesUnderShadowField();
            _playerTerrainFocus.HideAllTilesOutlines();

            _playerInput.OnPlayerInput -= HandleInput;
        }

        private void HandleInput(HexPosition hex)
        {
            _playerStateMachine.SwitchTo<PlayerDefaultState>();
        }

        private List<TileData> GetUnitTilesToMove(UnitData unit)
        {
            var result = new List<TileData>();
            var front = new List<TileData> { unit.RootTile };

            while (front.Count > 0)
            {
                var tile = front[0];

                foreach (var neighbor in tile.Neighbors)
                {
                    if (HexPosition.GetMagnitude(neighbor.Hex, unit.RootTile.Hex) > unit.Preset.MoveRange)
                        continue;
                    
                    if(tile.Region != unit.RootTile.Region)
                        continue;
                    
                    if (!front.Contains(neighbor) && !result.Contains(neighbor))
                        front.Add(neighbor);
                }

                front.Remove(tile);
                result.Add(tile);
            }

            return result;
        }
    }
}