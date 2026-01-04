using App.Ecs.Health.Death;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Health
{
    public class HealthAuthoring : MonoBehaviour
    {
        [SerializeField] private float health;

        private class Baker : Baker<HealthAuthoring>
        {
            public override void Bake(HealthAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new MaxHealth() { Value = authoring.health });
                AddComponent(entity, new CurrentHealth() { Value = authoring.health });
                AddBuffer<DamageToHealthFrameBuffer>(entity);
                
                AddComponent(entity, new DeathFlag());
                AddComponent(entity, new DeathInitRequiredFlag());
            }
        }
    }
}