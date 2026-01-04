using App.Ecs.Moving;
using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace App.Ecs.Orbs
{
    public struct OrbTag : IComponentData
    {
        
    }

    public struct OrbDamping : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(IndependentMoveSystemGroup))]
    public partial struct OrbUpdateMoveDampingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<OrbTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (damping, moveSpeed) in 
                     SystemAPI.Query<RefRO<OrbDamping>, RefRW<MoveSpeed>>()
                         .WithAll<OrbTag>()
                         .WithNone<OrbConsumeTag>())
            {
                moveSpeed.ValueRW.Value *= math.pow(1 - damping.ValueRO.Value, deltaTime);
                if (moveSpeed.ValueRW.Value < 0.01f) 
                    moveSpeed.ValueRW.Value = 0;
            }
        }
    }
}