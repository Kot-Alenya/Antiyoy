using System;
using CodeBase.Gameplay.World.Hex;

namespace CodeBase.Gameplay.Player.Input
{
    public interface IPlayerInput
    {
        public event Action<HexPosition> OnPlayerInput;
    }
}