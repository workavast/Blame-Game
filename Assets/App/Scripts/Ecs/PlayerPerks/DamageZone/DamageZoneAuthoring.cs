using App.Ecs.Clenuping;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.PlayerPerks.DamageZone
{
    public class DamageZoneAuthoring : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private WeakObjectReference<CleanupView> prefab;
        
        private class Baker : Baker<DamageZoneAuthoring>
        {
            public override void Bake(DamageZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new DamageZoneTag());
                AddComponent(entity, new ViewPrefabHolder() { Prefab = authoring.prefab });
                
                AddComponent(entity, new AttackDamage() { Value = authoring.damage });
                AddComponent(entity, new DamageScale());
                
                AddBuffer<DamageFrameBuffer>(entity);
            }
        }
    }
}