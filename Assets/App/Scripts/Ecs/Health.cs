using App.Ecs.Experience;
using App.Ecs.Experience.ExpDropping;
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

    public struct DamageScale : IComponentData
    {
        public float Value;
    }
    
    public struct DamageFrameBuffer : IBufferElementData
    {
        public float Value;
    }
    
    public partial struct ApplyDamageToHealth : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, damageBuffer) in 
                     SystemAPI.Query<RefRW<CurrentHealth>, DynamicBuffer<DamageFrameBuffer>>())
            {
                if (damageBuffer.IsEmpty)
                    continue;

                foreach (var damage in damageBuffer) 
                    health.ValueRW.Value -= damage.Value;
                
                damageBuffer.Clear();
            }
        }
    }

    [UpdateAfter(typeof(ApplyDamageToHealth))]
    public partial struct DestroyDeadEntities : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExpTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var expEntity = SystemAPI.GetSingletonEntity<ExpTag>();
            var expOrbsRequestsBuffer = SystemAPI.GetBuffer<ExpOrbsDropRequest>(expEntity);
            
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (transform, health, entity) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<CurrentHealth>>()
                         .WithEntityAccess())
            {
                if (health.ValueRO.Value <= 0)
                {
                    if (SystemAPI.HasComponent<ExpOrbDropper>(entity))
                    {
                        var expOrbDropper = SystemAPI.GetComponent<ExpOrbDropper>(entity);
                        expOrbsRequestsBuffer.Add(new ExpOrbsDropRequest()
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