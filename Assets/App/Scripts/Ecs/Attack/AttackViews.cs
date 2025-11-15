using App.Ecs.Characters;
using Unity.Entities;

namespace App.Ecs.Attack
{
    public struct AttackViewInitializeFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct AttackViewHolderInitializeFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct AttackViewActivateFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct AttackViewHolder : IComponentData
    {
        public UnityObjectRef<AttackView> Instance;
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct AttackViewHolderInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            
            foreach (var (characterView, attackViewHolderInitializedFlag, entity) in 
                     SystemAPI.Query<RefRO<CharacterViewHolder>, EnabledRefRW<AttackViewHolderInitializeFlag>>()
                     .WithEntityAccess())
            {
                if (characterView.ValueRO.Instance.Value.TryGetComponent(out AttackView attackViewMb)) 
                    ecb.AddComponent(entity, new AttackViewHolder(){Instance = attackViewMb});
                attackViewHolderInitializedFlag.ValueRW = false;
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct AttackViewInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (viewFlag, initializedFlag) in 
                     SystemAPI.Query<EnabledRefRW<AttackViewActivateFlag>, EnabledRefRW<AttackViewInitializeFlag>>())
            {
                viewFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }
    
    public partial struct AttackViewActivateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (attackView, attackPerformViewFlag) in 
                     SystemAPI.Query<RefRO<AttackViewHolder>, EnabledRefRW<AttackViewActivateFlag>>())
            {
                attackPerformViewFlag.ValueRW = false;
                attackView.ValueRO.Instance.Value.PerformAttack();
            }
        }
    }
}