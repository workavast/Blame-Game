using App.Ecs.Characters;
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

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct AttackVfxViewHolderInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (characterView, attackViewHolderInitializedFlag, entity) in
                     SystemAPI.Query<RefRO<CharacterViewHolder>, EnabledRefRW<AttackVfxViewHolderInitializeFlag>>()
                         .WithEntityAccess())
            {
                if (characterView.ValueRO.Instance.Value.TryGetComponent(out AttackVfxView attackViewMb))
                    ecb.AddComponent(entity, new AttackVfxViewHolder() { Instance = attackViewMb });
                attackViewHolderInitializedFlag.ValueRW = false;
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct AttackVfxInitializeSystem : ISystem
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