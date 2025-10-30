using App.Audio.Sources;
using App.Ecs.Clenuping;
using App.Ecs.Shooting;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;

namespace App.Ecs
{
    public struct AdditionalProjectilesCount : IComponentData
    {
        public int Value;
    }
    
    public struct AdditionalPenetration : IComponentData
    {
        public int Value;
    }
    
    public struct ShootDistanceReaction : IComponentData
    {
        public float Value;
    }

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
    
    public partial class ShooterSfxViewInstaller : ViewInstallerSystem<ShooterSfxTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new ShooterSfxViewHolder() { Instance = instance as ShooterSfxView });
    }
    
    public partial class ShooterSfxInitializeSystem : SfxInitializeSystem<ShooterSfxDataHolder, ShooterSfxTag>
    {
        protected override void StartLoading(ShooterSfxDataHolder sfxData)
        {
            sfxData.ShootSfxRef.LoadAsync();
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(ShooterSfxInitializeSystem))]
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
                         .WithAll<ShooterSfxTag>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetShootSfx(sfxData.ValueRO.ShootSfxRef);
            }
        }
    }
}