using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public interface ICameraController
    {
        public void Move(Vector2 direction);

        public void Zoom(bool isIncrease);

        public RaycastHit2D RaycastFromMouseScreenPoint();
        
        public RaycastHit2D RaycastAllFromMouseScreenPointAndGetNearestBySortingOrder();
    }
}