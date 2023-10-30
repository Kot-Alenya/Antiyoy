using UnityEngine;

namespace CodeBase.Utilities.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        public bool IsOpen { get; private set; }

        public virtual void Show() => IsOpen = true;

        public virtual void Hide() => IsOpen = false;
    }
}