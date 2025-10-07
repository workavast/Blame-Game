using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace App.Ecs
{
    public struct AoeZoneTag : IComponentData
    {
        
    }
    
    public struct AoeZoneRadius : IComponentData
    {
        public float Value;
    }
    
    public struct AoeZoneTargetRadius : IComponentData
    {
        public float Value;
    }
    
    public struct AoeZoneRadiusFactor : IComponentData
    {
        public float Value;
    }

    [UpdateInGroup(typeof(DependentMoveSystemGroup))]
    public partial struct AoeZonePositionUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(player);

            foreach (var transform in
                     SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<AoeZoneTag>())
            {
                transform.ValueRW.Position = playerTransform.Position;
            }
        }
    }

    [UpdateInGroup(typeof(BeforeTransformPauseSimulationGroup))]
    public partial struct AoeZoneSizeUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, radius, targetRadius, radiusFactor) in
                     SystemAPI
                         .Query<RefRW<LocalTransform>, RefRW<AoeZoneRadius>, RefRO<AoeZoneTargetRadius>,
                             RefRO<AoeZoneRadiusFactor>>()
                         .WithAll<AoeZoneTag>())
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
    }
}