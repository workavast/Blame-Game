using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs
{
    public struct AutoMoveTag : IComponentData
    {
        
    }
    
    public struct MoveDirection : IComponentData
    {
        public float2 Value;
    }

    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct PhysicsMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (physicsVelocity,direction,speed) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<MoveDirection>, RefRO<MoveSpeed>>()
                         .WithAll<IsActiveTag>())
            {
                var step2D = direction.ValueRO.Value * speed.ValueRO.Value * Time.deltaTime;
                physicsVelocity.ValueRW.Linear += new float3(step2D.x, 0, step2D.y);
            }
        }
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct AutoMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, direction,speed) 
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveDirection>, RefRO<MoveSpeed>>()
                         .WithAll<AutoMoveTag>()
                         .WithNone<PhysicsVelocity>())
            {
                var step2D = direction.ValueRO.Value * speed.ValueRO.Value * Time.deltaTime;
                transform.ValueRW.Position += new float3(step2D.x, 0, step2D.y);
            }
        }
    }
}