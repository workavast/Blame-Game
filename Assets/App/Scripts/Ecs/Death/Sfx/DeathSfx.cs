using App.Audio.Sources;
using App.Ecs.Characters;
using App.Ecs.Sound;
using Unity.Entities;
using Unity.Entities.Content;
using UnityEngine;

namespace App.Ecs.Death
{
    public struct DeathSfxInitializeFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct DeathSfxHolderInitializeFlag : IComponentData, IEnableableComponent
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
    
    public partial class DeathSfxInitializer : SfxInitializeSystem<DeathSfxData>
    {
        protected override void StartLoading(DeathSfxData sfxData)
        {
            sfxData.DeathSfxRef.LoadAsync();
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(DeathSfxInitializer))]
    public partial struct DeathSfxHolderCreateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (characterViewHolder, sfx, entity)  in 
                     SystemAPI.Query<RefRO<CharacterViewHolder>, RefRO<DeathSfxData>>()
                         .WithNone<DeathSfxViewHolder, SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new DeathSfxViewHolder()
                {
                    Instance = characterViewHolder.ValueRO.Instance.Value.GetComponent<DeathSfxView>()
                });
            }
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(DeathSfxInitializer))]
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
            
            foreach (var (viewHolder, sfx, entity)  in 
                     SystemAPI.Query<RefRO<DeathSfxViewHolder>, RefRO<DeathSfxData>>()
                         .WithNone<SfxInitedTag>()
                         .WithEntityAccess())
            {
                ecb.AddComponent(entity, new SfxInitedTag());
                viewHolder.ValueRO.Instance.Value.SetDeathSfx(sfx.ValueRO.DeathSfxRef);
            }
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct DeathSfxHolderInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            
            foreach (var (characterView, deathSfxHolderInitializedFlag, entity) in 
                     SystemAPI.Query<RefRO<CharacterViewHolder>, EnabledRefRW<DeathSfxHolderInitializeFlag>>()
                     .WithEntityAccess())
            {
                if (characterView.ValueRO.Instance.Value.TryGetComponent(out DeathSfxView deathSfx)) 
                    ecb.AddComponent(entity, new DeathSfxViewHolder(){Instance = deathSfx});
                deathSfxHolderInitializedFlag.ValueRW = false;
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct DeathSfxInitializeSystem : ISystem
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
    
    [UpdateAfter(typeof(ApplyDamageToHealth))]
    [UpdateBefore(typeof(DestroyDeadEntities))]
    public partial struct CallDeathSfxView : ISystem
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
    
    [UpdateAfter(typeof(CallDeathSfxView))]
    [UpdateBefore(typeof(DestroyDeadEntities))]
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