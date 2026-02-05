using App.Ecs.AoeZones;
using App.Ecs.Attack;
using App.Ecs.Attack.Cooldown;
using App.Ecs.Enemies;
using App.Ecs.Health;
using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks.DamageZone
{
    public struct DamageZoneTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct DamageZoneDamageSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<DamageZoneTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var globalDamageScale = SystemAPI.GetComponent<AttackDamage>(playerEntity);
            
            foreach (var (zoneTransform, radius, 
                         damage, entity) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<AoeZoneRadius>, RefRO<AttackDamage>>()
                         .WithDisabled<AttackCooldown>()
                         .WithAll<DamageZoneTag>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var damageValue = damage.ValueRO.Value * (damage.ValueRO.Scale + globalDamageScale.Scale);
                foreach (var (enemyTransform, damageBuffer) in SystemAPI
                             .Query<RefRO<LocalTransform>, DynamicBuffer<DamageToHealthFrameBuffer>>()
                             .WithAll<EnemyTag>())
                {
                    if (math.distance(zoneTransform.ValueRO.Position, enemyTransform.ValueRO.Position) <= radius.ValueRO.Value)
                        damageBuffer.Add(new DamageToHealthFrameBuffer() { Value = damageValue });
                }
            }
        }
    }
}