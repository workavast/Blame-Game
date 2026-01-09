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

    public struct DeathSfxLoadStartedTag : IComponentData
    {
        
    }
    
    public struct DeathSfxSetedTag : IComponentData
    {
        
    }
    
    public partial class DeathSfxStartLoadingSystem : SfxStartLoadSystem<DeathSfxData, DeathSfxLoadStartedTag>
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

    public partial class DeathSfxSetSystem : SfxSetSystem<DeathSfxViewHolder, DeathSfxData, DeathSfxSetedTag>
    {
        protected override void SetData(DeathSfxViewHolder viewHolder, DeathSfxData sfx) 
            => viewHolder.Instance.Value.SetDeathSfx(sfx.DeathSfxRef);
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