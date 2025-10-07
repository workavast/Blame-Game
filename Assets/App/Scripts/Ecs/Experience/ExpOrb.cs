using Unity.Entities;
using Unity.Mathematics;

namespace App.Ecs.Experience
{
    public struct ExpOrbTag : IComponentData
    {
        
    }

    public struct ExpOrbDamping : IComponentData
    {
        public float Value;
    }
    
    public struct ExpOrbAmount : IComponentData
    {
        public float Value;
    }

    public struct ExpOrbIsConsumeTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct ExpOrbUpdateMoveDampingSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (damping, moveSpeed) in 
                     SystemAPI.Query<RefRO<ExpOrbDamping>, RefRW<MoveSpeed>>()
                         .WithAll<ExpOrbTag>()
                         .WithNone<ExpOrbIsConsumeTag>())
            {
                moveSpeed.ValueRW.Value *= math.pow(1 - damping.ValueRO.Value, deltaTime);
                if (moveSpeed.ValueRW.Value < 0.01f) 
                    moveSpeed.ValueRW.Value = 0;
            }
        }
    }
}