using Unity.Burst;
using Unity.Entities;

namespace App.Ecs.Health
{
    public struct MaxHealth : IComponentData
    {
        public float Value;
    }
    
    public struct CurrentHealth : IComponentData
    {
        public float Value;
    }
    
    public struct DamageToHealthFrameBuffer : IBufferElementData
    {
        public float Value;
    }
    
    public partial struct ApplyDamageToHealth : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, damageBuffer) in 
                     SystemAPI.Query<RefRW<CurrentHealth>, DynamicBuffer<DamageToHealthFrameBuffer>>())
            {
                if (damageBuffer.IsEmpty)
                    continue;

                foreach (var damage in damageBuffer) 
                    health.ValueRW.Value -= damage.Value;
                
                damageBuffer.Clear();
            }
        }
    }

    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct DestroyDeadEntities : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (health, entity) in 
                     SystemAPI.Query<RefRO<CurrentHealth>>()
                         .WithEntityAccess())
            {
                if (health.ValueRO.Value <= 0) 
                    ecb.DestroyEntity(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }   
    }
}