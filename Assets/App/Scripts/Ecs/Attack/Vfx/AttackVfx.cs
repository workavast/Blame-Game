using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Attack.Vfx
{
    public struct AttackVfxViewHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct AttackVfxViewHolder : IComponentData
    {
        public UnityObjectRef<AttackVfxView> Instance;
    }

    public partial class AttackVfxViewHolderInitSystem
        : ViewHolderInitializeSystem<AttackVfxViewHolderInitializeFlag, AttackVfxView, AttackVfxViewHolder>
    {
        protected override void AddViewHolder(ref EntityCommandBuffer ecb, Entity entity, AttackVfxView view)
            => ecb.AddComponent(entity, new AttackVfxViewHolder() { Instance = view });
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct AttackVfxActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackView, _) in
                     SystemAPI.Query<RefRO<AttackVfxViewHolder>, EnabledRefRO<AttackRequested>>())
            {
                attackView.ValueRO.Instance.Value.PerformAttack();
            }
        }
    }
}