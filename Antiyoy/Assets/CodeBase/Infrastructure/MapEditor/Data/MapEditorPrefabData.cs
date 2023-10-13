using CodeBase.Infrastructure.MapEditor.UI;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CodeBase.Infrastructure.MapEditor.Data
{
    public class MapEditorPrefabData : SerializedMonoBehaviour
    {
        [OdinSerialize] public IMapEditorUIElement[] UIElements;
    }
}