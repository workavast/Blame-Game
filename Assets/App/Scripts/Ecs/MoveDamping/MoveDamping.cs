using App.Ecs.Moving;
using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace App.Ecs.MoveDamping
{
    public struct MoveDampingTag : IComponentData
    {
        
    }
    
    public struct InertialMoveDamping : IComponentData
    {
        public float BaseValue;
        public float Scale;
        public float ScaleMoveSpeedLimit;
    }

    public struct MoveDampingUtils
    {
        /// <summary>
        /// remove all related components from entity
        /// </summary>
        public static void FullRemove(Entity entity, ref EntityCommandBuffer ecb)
        {
            ecb.RemoveComponent<MoveDampingTag>(entity);
            ecb.RemoveComponent<InertialMoveDamping>(entity);
        }
    }
    
    [UpdateInGroup(typeof(IndependentMoveSystemGroup))]
    public partial struct InertialMoveUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MoveDampingTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (damping, moveSpeed) in 
                     SystemAPI.Query<RefRO<InertialMoveDamping>, RefRW<MoveSpeed>>()
                         .WithAll<MoveDampingTag>())
            {
                var speed = moveSpeed.ValueRW.Value;
                var baseDamping = damping.ValueRO.BaseValue;

                var t = math.saturate(speed / damping.ValueRO.ScaleMoveSpeedLimit);
                var effectiveDamping = math.lerp(baseDamping * damping.ValueRO.Scale, baseDamping, t);

                speed *= math.exp(-effectiveDamping * deltaTime);
                if (speed < 0.05f)
                    speed = 0f;

                moveSpeed.ValueRW.Value = speed;
            }
        }
    }
}