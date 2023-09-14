using AYellowpaper;
using CodeBase.Infrastructure.MapEditor.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor.Data
{
    public class MapEditorPrefabData : MonoBehaviour
    {
        [RequireInterface(typeof(IMapEditorUIElement))]
        public MonoBehaviour[] UIElements;
    }
}