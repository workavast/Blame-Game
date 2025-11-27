using App.Ecs.AoeZones;
using App.Ecs.Attack;
using App.Ecs.Enemies;
using App.Ecs.EntityViews;
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

    public struct DamageZoneViewHolder : IComponentData
    {
        public UnityObjectRef<DamageZoneView> Instance;
    }
    
    public partial class DamageZoneViewHolderInitSystem
        : ViewHolderInitializeSystem<DamageZoneTag, DamageZoneView, DamageZoneViewHolder>
    {
        protected override void AddViewHolder(ref EntityCommandBuffer ecb, Entity entity, DamageZoneView view)
            => ecb.AddComponent(entity, new DamageZoneViewHolder() { Instance = view });
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct DamageZoneViewUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, view, radius) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<DamageZoneViewHolder>, RefRO<AoeZoneRadius>>()
                         .WithAll<DamageZoneTag>())
            {
                view.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                view.ValueRO.Instance.Value.SetRadius(radius.ValueRO.Value);
            }
        }
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct DamageZoneDamageSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var globalDamageScale = SystemAPI.GetComponent<AttackDamageScale>(playerEntity);
            
            foreach (var (zoneTransform, radius, 
                         damage, damageScale, entity) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<AoeZoneRadius>, RefRO<AttackDamage>, RefRO<AttackDamageScale>>()
                         .WithDisabled<AttackCooldown>()
                         .WithAll<DamageZoneTag>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var damageValue = damage.ValueRO.Value * (damageScale.ValueRO.Value + globalDamageScale.Value);
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