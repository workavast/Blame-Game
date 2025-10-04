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
    
    public struct DamageZoneRadius : IComponentData
    {
        public float Value;
    }
    
    public struct DamageZoneTargetRadius : IComponentData
    {
        public float Value;
    }
    
    public struct DamageZoneRadiusFactor : IComponentData
    {
        public float Value;
    }
    
    public partial class DamageZoneViewInstallerSystem : ViewInstallerSystem<DamageZoneTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new DamageZoneViewHolder() { Instance = instance as DamageZoneView });
    } 
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct DamageZoneUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            UpdatePosition(ref state);
            UpdateSize(ref state);
            UpdateView(ref state);
            UpdateDamage(ref state);
        }

        private void UpdateView(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(player);

            foreach (var (view, radius) in 
                     SystemAPI.Query<RefRO<DamageZoneViewHolder>, RefRO<DamageZoneRadius>>()
                         .WithAll<DamageZoneTag>())
            {
                view.ValueRO.Instance.Value.SetPosition(playerTransform.Position);
                view.ValueRO.Instance.Value.SetRadius(radius.ValueRO.Value);
            }
        }

        private void UpdatePosition(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(player);
            
            foreach (var transform in 
                     SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<DamageZoneTag>())
            {
                transform.ValueRW.Position = playerTransform.Position;
            }
        }

        private void UpdateSize(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, radius, targetRadius, radiusFactor) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<DamageZoneRadius>, RefRO<DamageZoneTargetRadius>, RefRO<DamageZoneRadiusFactor>>()
                         .WithAll<DamageZoneTag>())
            {
                var radiusValue = radius.ValueRO.Value;
                var targetRadiusValue = targetRadius.ValueRO.Value;
                if (math.abs(targetRadiusValue - radiusValue) > 0.001f)
                {
                    var sign = math.sign(targetRadiusValue - radiusValue);
                    var radiusDelta = sign * radiusFactor.ValueRO.Value * deltaTime;
                    if (sign > 0)
                        radius.ValueRW.Value = math.clamp(radiusValue + radiusDelta, float.MinValue, targetRadiusValue);
                    else
                        radius.ValueRW.Value = math.clamp(radiusValue + radiusDelta, targetRadiusValue, float.MaxValue);
                    
                    transform.ValueRW.Scale = 2 * radius.ValueRW.Value;
                }
            }
        }
        
        private void UpdateDamage(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (zoneTransform, radius, damage) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<DamageZoneRadius>, RefRO<AttackDamage>>()
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