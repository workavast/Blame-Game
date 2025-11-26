using App.Audio.Sources;
using App.Ecs.EntityViews;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs.Shooting
{
    public struct ShooterSfxViewHolder : IComponentData
    {
        public UnityObjectRef<ShooterSfxView> Instance;
    }
    
    public struct ShooterSfxDataHolder : IComponentData
    {
        public WeakObjectReference<AudioPoolRelease> ShootSfxRef;
    }

    public struct ShooterSfxTag : IComponentData
    {
        
    }
    
    public partial class ShooterSfxViewInit : ViewHolderInitializeSystem<ShooterSfxTag, ShooterSfxView, ShooterSfxViewHolder>
    {
        protected override void AddViewHolder(ref EntityCommandBuffer ecb, Entity entity, ShooterSfxView view)
            => ecb.AddComponent(entity, new ShooterSfxViewHolder() { Instance = view });
    }
    
    public partial class ShooterSfxStartLoadSystem : SfxStartLoadSystem<ShooterSfxDataHolder>
    {
        protected override void StartLoading(ShooterSfxDataHolder sfxData)
        {
            sfxData.ShootSfxRef.LoadAsync();
        }
    }
    
    [UpdateInGroup(typeof(SfxSetSystemGroup))]
    public partial struct ShooterSfxSetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (viewHolder, sfxData, entity)  in 
                     SystemAPI.Query<RefRO<ShooterSfxViewHolder>, RefRO<ShooterSfxDataHolder>>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetShootSfx(sfxData.ValueRO.ShootSfxRef);
            }
        }
    }
}