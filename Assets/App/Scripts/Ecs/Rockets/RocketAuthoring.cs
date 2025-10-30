using App.Audio.Sources;
using App.Ecs.Clenuping;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.Rockets
{
    public class RocketAuthoring : MonoBehaviour
    {
        [SerializeField] private WeakObjectReference<CleanupView> viewPrefab;
        [SerializeField] private WeakObjectReference<AudioPoolRelease> explosionPrefab;
        
        private class Baker : Baker<RocketAuthoring>
        {
            public override void Bake(RocketAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new RocketTag());
                AddComponent(entity, new RocketTargetHeight());
                AddComponent(entity, new RocketAwaitTimer());
                AddComponent(entity, new RocketExplosionRadius());
                AddComponent(entity, new RocketViewExplosionRadiusSetFlag());
                
                AddComponent(entity, new RocketSfxData(){SfxPrefab = authoring.explosionPrefab});    
                    
                AddComponent(entity, new ViewPrefabHolder() { Prefab = authoring.viewPrefab });
                AddComponent(entity, new AttackDamage());

                AddComponent(entity, new MoveSpeed());
            }
        }
    }
}