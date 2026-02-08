using UnityEngine;

namespace App.ResourcesSystem.ResourcesConfigs
{
    [CreateAssetMenu(fileName = nameof(ResourceConfig), menuName = ResourcesConsts.Path + nameof(ResourceConfig))]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private Sprite icon;

        public Sprite Icon => icon;
    }
}
