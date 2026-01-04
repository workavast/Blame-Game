using App.Audio.Sources;
using App.Ecs.EntityViews;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs.Health.Death.Sfx
{
    public struct DeathSfxData : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> DeathSfxRef;
    }

    public struct DeathSfxViewHolder : IComponentData
    {
        public UnityObjectRef<DeathSfxView> Instance;
    }
    
    public struct DeathSfxHolderInitializeFlag : IComponentData, IEnableableComponent
    {

    }

    public partial class DeathSfxStartLoadingSystem : SfxStartLoadSystem<DeathSfxData>
    {
        protected override void StartLoading(DeathSfxData sfxData) 
            => sfxData.DeathSfxRef.LoadAsync();
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
            var query = SystemAPI.QueryBuilder()
                .WithAll<DeathSfxViewHolder, DeathSfxData>()
                .WithNone<SfxInitedTag>()
                .Build();
            
            state.RequireForUpdate(query);
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
    public partial struct DeathSfxActivateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<DeathSfxViewHolder, DeathFlag>()
                .Build();
            
            state.RequireForUpdate(query);
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (deathSfx, _) in
                     SystemAPI.Query<RefRW<DeathSfxViewHolder>, EnabledRefRO<DeathFlag>>())
            {
                deathSfx.ValueRW.Instance.Value.Activate();
            }
        }
    }
}