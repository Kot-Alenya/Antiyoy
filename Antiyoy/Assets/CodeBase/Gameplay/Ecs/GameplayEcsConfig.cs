using UnityEngine;

namespace CodeBase.Gameplay.Ecs
{
    [CreateAssetMenu(menuName = "Configurations/Gameplay/Ecs", fileName = "GameplayEcsConfig", order = 0)]
    public class GameplayEcsConfig : ScriptableObject
    {
        public GameplayEcsController ControllerPrefab;
    }
}