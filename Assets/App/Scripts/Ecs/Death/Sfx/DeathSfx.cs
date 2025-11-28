using App.Audio.Sources;
using App.Ecs.EntityViews;
using App.Ecs.Health;
using App.Ecs.Sound;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs.Death.Sfx
{
    public struct DeathSfxInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public struct DeathSfxActivateFlag : IComponentData, IEnableableComponent
    {

    }

    public struct DeathSfxData : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> DeathSfxRef;
    }

    public struct DeathSfxViewHolder : IComponentData
    {
        public UnityObjectRef<DeathSfxView> Instance;
    }

    [UpdateInGroup(typeof(InitOffSystemGroup))]
    public partial struct DeathSfxInitOffSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (viewFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<DeathSfxActivateFlag>, EnabledRefRW<DeathSfxInitializeFlag>>())
            {
                viewFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }

    public partial class DeathSfxStartLoadingSystem : SfxStartLoadSystem<DeathSfxData>
    {
        protected override void StartLoading(DeathSfxData sfxData) 
            => sfxData.DeathSfxRef.LoadAsync();
    }

    public struct DeathSfxHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public partial class DeathSfxViewHolderInitSystem
        : ViewHolderInitializeSystem<DeathSfxHolderInitializeFlag, DeathSfxView, DeathSfxViewHolder>
    {
        protected override DeathSfxViewHolder CreateViewHolder(DeathSfxView view)
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(SfxSetSystemGroup))]
    public partial struct DeathSfxSetSystem : ISystem
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
                     SystemAPI.Query<RefRO<DeathSfxViewHolder>, RefRO<DeathSfxData>>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetDeathSfx(sfx.ValueRO.DeathSfxRef);
            }
        }
    }

    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial struct CallDeathSfxSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, deathSfxActivateFlag) in
                     SystemAPI.Query<RefRW<CurrentHealth>, EnabledRefRW<DeathSfxActivateFlag>>()
                         .WithDisabled<DeathSfxActivateFlag>())
            {
                if (health.ValueRO.Value <= 0)
                    deathSfxActivateFlag.ValueRW = true;
            }
        }
    }

    [UpdateInGroup(typeof(DeathSystemGroup))]
    [UpdateAfter(typeof(CallDeathSfxSystem))]
    public partial struct DeathSfxActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (deathSfx, deathPerformViewFlag) in
                     SystemAPI.Query<RefRO<DeathSfxViewHolder>, EnabledRefRW<DeathSfxActivateFlag>>())
            {
                deathPerformViewFlag.ValueRW = false;
                deathSfx.ValueRO.Instance.Value.Activate();
            }
        }
    }
}