using Unity.Entities;

namespace App.Ecs
{
    public struct MaxHealth : IComponentData
    {
        public float Value;
    }
    
    public struct CurrentHealth : IComponentData
    {
        public float Value;
    }

    public struct AttackDamage : IComponentData
    {
        public float Value;
    }
    
    public struct DamageFrameBuffer : IBufferElementData
    {
        public float Value;
    }

    public partial struct ApplyDamage : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (health, damageBuffer, entity) in 
                     SystemAPI.Query<RefRW<CurrentHealth>, DynamicBuffer<DamageFrameBuffer>>()
                         .WithAll<IsAliveTag>()
                         .WithEntityAccess())
            {
                if (damageBuffer.IsEmpty)
                    continue;

                foreach (var damage in damageBuffer) 
                    health.ValueRW.Value -= damage.Value;
                
                damageBuffer.Clear();

                if (health.ValueRO.Value <= 0) 
                    ecb.DestroyEntity(entity);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}