using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Utilities.UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] private Button _selectableButton;

        private protected void OnEnable()
            => _selectableButton.onClick.AddListener(OnClick);

        private protected virtual void OnDisable()
            => _selectableButton.onClick.RemoveListener(OnClick);

        private protected abstract void OnClick();
    }
}