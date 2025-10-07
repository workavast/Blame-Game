using App.Ecs.Experience;
using Unity.Entities;
using Unity.Transforms;

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
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExpTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var expEntity = SystemAPI.GetSingletonEntity<ExpTag>();
            var requestsBuffer = SystemAPI.GetBuffer<ExpOrbsDropRequest>(expEntity);
            
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (transform, health, damageBuffer, entity) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<CurrentHealth>, DynamicBuffer<DamageFrameBuffer>>()
                         .WithAll<IsActiveTag>()
                         .WithEntityAccess())
            {
                if (damageBuffer.IsEmpty)
                    continue;

                foreach (var damage in damageBuffer) 
                    health.ValueRW.Value -= damage.Value;
                
                damageBuffer.Clear();

                if (health.ValueRO.Value <= 0)
                {
                    if (SystemAPI.HasComponent<ExpOrbDropper>(entity))
                    {
                        var expOrbDropper = SystemAPI.GetComponent<ExpOrbDropper>(entity);
                        requestsBuffer.Add(new ExpOrbsDropRequest()
                        {
                            OrbsCount = expOrbDropper.OrbsCount,
                            Position = transform.ValueRO.Position
                        });
                    }
                    
                    ecb.DestroyEntity(entity);
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}