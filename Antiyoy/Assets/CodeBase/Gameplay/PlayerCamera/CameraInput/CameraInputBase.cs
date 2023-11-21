using UnityEngine;

namespace CodeBase.Gameplay.PlayerCamera.CameraInput
{
    public abstract class CameraInputBase : MonoBehaviour
    {
        [SerializeField] private protected CameraController CameraController;
    }
}