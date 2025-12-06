using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.EntityViews
{
    public class EntityViewAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<EntityView> viewPrefab;
        
        private class Baker : Baker<EntityViewAuthoring>
        {
            public override void Bake(EntityViewAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new RequiredCleanupTag());
                AddComponent(entity, new EntityViewPrefabHolder()
                {
                    Prefab = authoring.viewPrefab
                });
            }
        }
    }
}