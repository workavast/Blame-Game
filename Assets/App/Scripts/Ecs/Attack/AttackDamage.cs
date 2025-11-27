using Unity.Entities;

namespace App.Ecs.Attack
{
    public struct AttackDamage : IComponentData
    {
        public float Value;
    }

    public struct AttackDamageScale : IComponentData
    {
        public float Value;
    }
    
    public struct DamageToHealthFrameBuffer : IBufferElementData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct ApplyDamageToHealth : ISystem
    {
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