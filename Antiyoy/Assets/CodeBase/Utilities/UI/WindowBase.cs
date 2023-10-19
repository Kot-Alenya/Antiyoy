using UnityEngine;

namespace CodeBase.Utilities.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        public bool IsOpen { get; private set; }

        public virtual void Open() => IsOpen = true;

        public virtual void Close() => IsOpen = false;
    }
}