using UnityEngine;

namespace App.Resources
{
    [CreateAssetMenu(fileName = nameof(ResourcesValueConfig), menuName = ResourcesConsts.Path + nameof(ResourcesValueConfig))]
    public class ResourcesValueConfig : ScriptableObject
    {
        [SerializeField] private ResourcesValue resourcesAmount;
        
        public ResourcesValue ResourcesAmount => resourcesAmount;
    }
}