using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Unit
{
    public class UnitAnimator : MonoBehaviour
    {
        private static readonly int CanMoveStateHash = Animator.StringToHash("CanMove");
        private static readonly int CantMoveStateHash = Animator.StringToHash("CantMove");

        [SerializeField] private Animator _animator;

        public void CanMove()
            => _animator.SetBool(CanMoveStateHash, true);
        
        public void CantMove()
            => _animator.SetBool(CantMoveStateHash, false);
    }
}