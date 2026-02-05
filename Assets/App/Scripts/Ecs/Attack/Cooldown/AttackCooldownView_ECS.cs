using App.Ecs.EntityViews;
using Unity.Entities;

namespace App.Ecs.Attack.Cooldown
{
    public struct AttackCooldownViewOwnerTag : IComponentData
    {
        
    }
    
    public struct AttackCooldownViewHolder : IComponentData
    {
        public UnityObjectRef<AttackCooldownView> Instance;
    }

    public partial class AttackCooldownViewHolderInitSystem : ViewHolderInitializeSystem<AttackCooldownViewOwnerTag, AttackCooldownView, AttackCooldownViewHolder>
    {
        protected override AttackCooldownViewHolder CreateViewHolder(AttackCooldownView view) 
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct UpdateAttackCooldownViewSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackCooldownViewHolder>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (defaultCooldown, cooldown, viewHolder) in 
                     SystemAPI.Query<RefRO<DefaultAttackCooldown>, RefRO<AttackCooldown>, RefRW<AttackCooldownViewHolder>>())
            {
                viewHolder.ValueRW.Instance.Value.UpdateCooldownPercentage(cooldown.ValueRO.Timer/defaultCooldown.ValueRO.Timer);
            }
        }
    }
}