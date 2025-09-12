using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

namespace App.Ecs
{
    public struct MoveDirection : IComponentData
    {
        public float2 Value;
    }

    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }
    
    public partial struct MoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (physicsVelocity,direction,speed) 
                     in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<MoveDirection>, RefRO<MoveSpeed>>())
            {
                var step2D = direction.ValueRO.Value * speed.ValueRO.Value * Time.deltaTime;
                physicsVelocity.ValueRW.Linear += new float3(step2D.x, 0, step2D.y);
            }
        }
    }
}