using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.DamageZone
{
    public class DamageZoneAuthoring : MonoBehaviour
    {
        [SerializeField] private float damage;
        
        private class Baker : Baker<DamageZoneAuthoring>
        {
            public override void Bake(DamageZoneAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new DamageZoneTag());
                
                AddComponent(entity, new AttackDamage() { Value = authoring.damage });
                AddComponent(entity, new DamageScale());
                
                AddBuffer<DamageFrameBuffer>(entity);
            }
        }
    }
}