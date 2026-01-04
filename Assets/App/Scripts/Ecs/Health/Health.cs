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
}