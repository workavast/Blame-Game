using App.Views;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks
{
    public struct DamageZoneTag : IComponentData
    {
        
    }

    public struct DamageZoneViewHolder : IComponentData
    {
        public UnityObjectRef<DamageZoneView> Instance;
    }
    
    public partial class DamageZoneViewInstallerSystem : ViewInstallerSystem<DamageZoneTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new DamageZoneViewHolder() { Instance = instance as DamageZoneView });
    } 
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(AoeZonePositionUpdateSystem))]
    [UpdateAfter(typeof(AoeZoneSizeUpdateSystem))]
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
    [UpdateAfter(typeof(AoeZonePositionUpdateSystem))]
    [UpdateAfter(typeof(AoeZoneSizeUpdateSystem))]
    public partial struct DamageZoneDamageSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (zoneTransform, radius, damage) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<AoeZoneRadius>, RefRO<AttackDamage>>()
                         .WithAll<DamageZoneTag>())
            {
                var damageValue = damage.ValueRO.Value * deltaTime;
                foreach (var (enemyTransform, damageBuffer) in SystemAPI
                             .Query<RefRO<LocalTransform>, DynamicBuffer<DamageFrameBuffer>>()
                             .WithAll<EnemyTag>())
                {
                    if (math.distance(zoneTransform.ValueRO.Position, enemyTransform.ValueRO.Position) <= radius.ValueRO.Value)
                        damageBuffer.Add(new DamageFrameBuffer() { Value = damageValue });
                }
            }
        }
    }
}