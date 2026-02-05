using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Attack.Vfx
{
    public struct AttackVfxViewOwnerTag : IComponentData
    {

    }

    public struct AttackVfxViewHolder : IComponentData
    {
        public UnityObjectRef<AttackVfxView> Instance;
    }

    public partial class AttackVfxViewHolderInitSystem
        : ViewHolderInitializeSystem<AttackVfxViewOwnerTag, AttackVfxView, AttackVfxViewHolder>
    {
        protected override AttackVfxViewHolder CreateViewHolder(AttackVfxView view)
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct AttackVfxActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackView, _) in
                     SystemAPI.Query<RefRW<AttackVfxViewHolder>, EnabledRefRO<AttackViewRequested>>())
            {
                attackView.ValueRW.Instance.Value.PerformAttack();
            }
        }
    }
}