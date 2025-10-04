using App.Ecs;
using App.Views;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Authorings
{
    public class RocketAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<CleanupView> viewPrefab;
        
        private class Baker : Baker<RocketAuthoring>
        {
            public override void Bake(RocketAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new IsActiveTag());
                
                AddComponent(entity, new RocketTag());
                AddComponent(entity, new RocketTargetHeight());
                AddComponent(entity, new RocketAwaitTimer());
                AddComponent(entity, new RocketExplosionRadius());
                AddComponent(entity, new RocketViewExplosionRadiusSetFlag());
                
                AddComponent(entity, new ViewPrefabHolder() { Prefab = authoring.viewPrefab });
                AddComponent(entity, new AttackDamage());

                AddComponent(entity, new MoveSpeed());
            }
        }
    }
}