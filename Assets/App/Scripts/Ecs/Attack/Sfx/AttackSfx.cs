using App.Audio.Sources;
using App.Ecs.Characters;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs.Attack.Sfx
{
    public struct AttackSfxInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct AttackSfxActivateFlag : IComponentData, IEnableableComponent
    {

    }

    public struct AttackSfxData : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> AttackSfxRef;
    }

    public struct AttackSfxViewHolder : IComponentData
    {
        public UnityObjectRef<AttackSfxView> Instance;
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct AttackSfxInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (viewFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<AttackSfxActivateFlag>, EnabledRefRW<AttackSfxInitializeFlag>>())
            {
                viewFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }

    public partial class AttackSfxStartLoadingSystem : SfxInitializeSystem<AttackSfxData>
    {
        protected override void StartLoading(AttackSfxData sfxData)
        {
            sfxData.AttackSfxRef.LoadAsync();
        }
    }

    public struct AttackSfxHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(AttackSfxStartLoadingSystem))]
    public partial struct AttackSfxViewHolderInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (characterView, attackSfxHolderInitializedFlag, entity) in
                     SystemAPI.Query<RefRO<CharacterViewHolder>, EnabledRefRW<AttackSfxHolderInitializeFlag>>()
                         .WithEntityAccess())
            {
                if (characterView.ValueRO.Instance.Value.TryGetComponent(out AttackSfxView attackSfx))
                    ecb.AddComponent(entity, new AttackSfxViewHolder() { Instance = attackSfx });
                attackSfxHolderInitializedFlag.ValueRW = false;
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(AttackSfxViewHolderInitializeSystem))]
    public partial struct AttackSfxSetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (viewHolder, sfx, entity) in
                     SystemAPI.Query<RefRO<AttackSfxViewHolder>, RefRO<AttackSfxData>>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetSfxRef(sfx.ValueRO.AttackSfxRef);
            }
        }
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct CallAttackSfxSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, attackSfxActivateFlag) in
                     SystemAPI.Query<RefRW<CurrentHealth>, EnabledRefRW<AttackSfxActivateFlag>>()
                         .WithDisabled<AttackSfxActivateFlag>())
            {
                if (health.ValueRO.Value <= 0)
                    attackSfxActivateFlag.ValueRW = true;
            }
        }
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    [UpdateAfter(typeof(CallAttackSfxSystem))]
    public partial struct AttackSfxActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackSfx, attackPerformViewFlag) in
                     SystemAPI.Query<RefRO<AttackSfxViewHolder>, EnabledRefRW<AttackSfxActivateFlag>>())
            {
                attackPerformViewFlag.ValueRW = false;
                attackSfx.ValueRO.Instance.Value.Activate();
            }
        }
    }
}