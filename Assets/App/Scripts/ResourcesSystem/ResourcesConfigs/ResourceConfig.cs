using UnityEngine;

namespace App.ResourcesSystem.ResourcesConfigs
{
    [CreateAssetMenu(fileName = nameof(ResourceConfig), menuName = ResourcesConsts.Path + nameof(ResourceConfig))]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int spriteAssetIndex;

        public Sprite Icon => icon;
        public int SpriteAssetIndex => spriteAssetIndex;
    }
}
