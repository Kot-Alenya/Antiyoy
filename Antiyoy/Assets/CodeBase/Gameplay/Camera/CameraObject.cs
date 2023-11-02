using System.Collections.Generic;
using CodeBase.Gameplay.Camera.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class CameraObject : ICameraController
    {
        private readonly CameraMovement _movement;
        private readonly CameraPrefabData _data;

        public CameraObject(CameraPrefabData data, CameraMovement movement)
        {
            _data = data;
            _movement = movement;
        }

        public void Move(Vector2 direction) => _movement.Move(direction);

        public void Zoom(bool isIncrease) => _movement.Zoom(isIncrease);

        public RaycastHit2D RaycastFromMouseScreenPoint()
        {
            var ray = GetRayFromMouseScreenPoint();

            return Physics2D.Raycast(ray.origin, ray.direction);
        }

        public RaycastHit2D RaycastAllFromMouseScreenPointAndGetNearestBySortingOrder()
        {
            var ray = GetRayFromMouseScreenPoint();
            var rawHits = Physics2D.RaycastAll(ray.origin, ray.direction);
            var hits = RemoveNotSpriteRenderer(rawHits);

            if (hits.Count <= 0)
                return default;

            if (hits.Count <= 1)
                return hits[0].Item1;

            var nearestId = 0;

            for (var i = 1; i < hits.Count; i++)
                if (hits[i].Item2.sortingOrder > hits[i - 1].Item2.sortingOrder)
                    nearestId = i;

            return hits[nearestId].Item1;
        }

        private Ray GetRayFromMouseScreenPoint() => _data.Camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

        private List<(RaycastHit2D, SpriteRenderer)> RemoveNotSpriteRenderer(RaycastHit2D[] hits)
        {
            var result = new List<(RaycastHit2D, SpriteRenderer)>();

            foreach (var hit in hits)
                if (hit.transform != null)
                    if (hit.transform.TryGetComponent<SpriteRenderer>(out var renderer))
                        result.Add((hit, renderer));

            return result;
        }
    }
}