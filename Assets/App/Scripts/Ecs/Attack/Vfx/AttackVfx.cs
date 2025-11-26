using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Attack.Vfx
{
    public struct AttackVfxInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct AttackVfxViewHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct AttackVfxActivateFlag : IComponentData, IEnableableComponent
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

    [UpdateInGroup(typeof(InitOffSystemGroup))]
    public partial struct AttackVfxInitOffSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (viewFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<AttackVfxActivateFlag>, EnabledRefRW<AttackVfxInitializeFlag>>())
            {
                viewFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct AttackVfxActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackView, attackPerformViewFlag) in
                     SystemAPI.Query<RefRO<AttackVfxViewHolder>, EnabledRefRW<AttackVfxActivateFlag>>())
            {
                attackPerformViewFlag.ValueRW = false;
                attackView.ValueRO.Instance.Value.PerformAttack();
            }
        }
    }
}