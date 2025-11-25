using App.Ecs.EntityViews;
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

    public partial class DeathVfxViewHolderInitializeSystem
        : ViewHolderInitializeSystem<DeathVfxViewHolderInitializeFlag, DeathVfxView, DeathVfxViewHolder>
    {
        protected override void AddViewHolder(ref EntityCommandBuffer ecb, Entity entity, DeathVfxView view)
            => ecb.AddComponent(entity, new DeathVfxViewHolder() { Instance = view });
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