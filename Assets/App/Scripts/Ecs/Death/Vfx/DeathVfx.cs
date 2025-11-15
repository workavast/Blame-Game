using App.Ecs.Characters;
using Unity.Entities;

namespace App.Ecs.Death
{
    public struct DeathVfxViewInitializeFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct DeathVfxViewHolderInitializeFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct DeathVfxViewActivateFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct DeathVfxViewHolder : IComponentData
    {
        public UnityObjectRef<DeathVfxView> Instance;
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
                    ecb.AddComponent(entity, new DeathVfxViewHolder(){Instance = deathView});
                deathViewHolderInitializedFlag.ValueRW = false;
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct DeathVfxViewInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (viewFlag, initializedFlag) in 
                     SystemAPI.Query<EnabledRefRW<DeathVfxViewActivateFlag>, EnabledRefRW<DeathVfxViewInitializeFlag>>())
            {
                viewFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }
    
    [UpdateAfter(typeof(ApplyDamageToHealth))]
    [UpdateBefore(typeof(DestroyDeadEntities))]
    public partial struct CallDeathVfxView : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, deathViewActivateFlag) in 
                     SystemAPI.Query<RefRW<CurrentHealth>, EnabledRefRW<DeathVfxViewActivateFlag>>()
                         .WithDisabled<DeathVfxViewActivateFlag>())
            {
                if (health.ValueRO.Value <= 0) 
                    deathViewActivateFlag.ValueRW = true;
            }
        }
    }
    
    [UpdateAfter(typeof(CallDeathVfxView))]
    [UpdateBefore(typeof(DestroyDeadEntities))]
    public partial struct DeathVfxViewActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (deathView, deathPerformViewFlag) in 
                     SystemAPI.Query<RefRO<DeathVfxViewHolder>, EnabledRefRW<DeathVfxViewActivateFlag>>())
            {
                deathPerformViewFlag.ValueRW = false;
                deathView.ValueRO.Instance.Value.Activate();
            }
        }
    }
}