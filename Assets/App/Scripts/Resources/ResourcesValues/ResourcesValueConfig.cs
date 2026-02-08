using UnityEngine;

namespace App.Resources.ResourcesValues
{
    [CreateAssetMenu(fileName = nameof(ResourcesValueConfig), menuName = ResourcesConsts.Path + nameof(ResourcesValueConfig))]
    public class ResourcesValueConfig : ScriptableObject
    {
        [SerializeField] private ResourcesValue resourcesAmount;
        
        public ResourcesValue ResourcesAmount => resourcesAmount;
    }
}