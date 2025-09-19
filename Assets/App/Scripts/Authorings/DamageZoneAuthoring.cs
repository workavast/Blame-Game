using App.Ecs;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Authorings
{
    public class DamageZoneAuthoring : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private float radiusFactor;
        [SerializeField] private float damage;
        [SerializeField] private WeakObjectReference<EntityView> prefab;
        
        private class Baker : Baker<DamageZoneAuthoring>
        {
            public override void Bake(DamageZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new IsAliveTag());
                
                AddComponent(entity, new DamageZoneTag());
                AddComponent(entity, new DamageZoneRadius() { Value = 1 });
                AddComponent(entity, new DamageZoneTargetRadius() { Value = authoring.radius });
                AddComponent(entity, new DamageZoneRadiusFactor() { Value = authoring.radiusFactor });
                
                AddComponent(entity, new AttackDamage() { Value = authoring.damage });
                
                AddComponent(entity, new CharacterVisualPrefab() { Prefab = authoring.prefab });

                AddBuffer<DamageFrameBuffer>(entity);
            }
        }
    }
}