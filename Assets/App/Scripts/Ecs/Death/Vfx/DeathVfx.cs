using App.Ecs.Characters;
using Unity.Entities;

namespace App.Ecs.Death.Vfx
{
    public struct DeathVfxInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct DeathVfxViewHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct DeathVfxActivateFlag : IComponentData, IEnableableComponent
    {

    }

    public struct DeathVfxViewHolder : IComponentData
    {
        public UnityObjectRef<DeathVfxView> Instance;
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct DeathVfxInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (viewFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<DeathVfxActivateFlag>, EnabledRefRW<DeathVfxInitializeFlag>>())
            {
                viewFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct DeathVfxViewHolderInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (characterView, deathViewHolderInitializedFlag, entity) in
                     SystemAPI.Query<RefRO<CharacterViewHolder>, EnabledRefRW<DeathVfxViewHolderInitializeFlag>>()
                         .WithEntityAccess())
            {
                if (characterView.ValueRO.Instance.Value.TryGetComponent(out DeathVfxView deathView))
                    ecb.AddComponent(entity, new DeathVfxViewHolder() { Instance = deathView });
                deathViewHolderInitializedFlag.ValueRW = false;
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial struct CallDeathVfxSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, deathViewActivateFlag) in
                     SystemAPI.Query<RefRW<CurrentHealth>, EnabledRefRW<DeathVfxActivateFlag>>()
                         .WithDisabled<DeathVfxActivateFlag>())
            {
                if (health.ValueRO.Value <= 0)
                    deathViewActivateFlag.ValueRW = true;
            }
        }
    }

    [UpdateInGroup(typeof(DeathSystemGroup))]
    [UpdateAfter(typeof(CallDeathVfxSystem))]
    public partial struct DeathVfxActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (deathView, deathPerformViewFlag) in
                     SystemAPI.Query<RefRO<DeathVfxViewHolder>, EnabledRefRW<DeathVfxActivateFlag>>())
            {
                deathPerformViewFlag.ValueRW = false;
                deathView.ValueRO.Instance.Value.Activate();
            }
        }
    }
}