using Unity.Entities;

namespace App.Ecs.EntityViews
{
    public struct RequiredCleanupTag : IComponentData { }
    
    public struct CleanupCallbackHolder : ICleanupComponentData
    {
        public UnityObjectRef<CleanupCallback> Instance;
    }
    
    [UpdateInGroup(typeof(LateSimulationSystemGroup), OrderFirst = false, OrderLast = true)]
    public partial struct CleanupSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<CleanupCallbackHolder>()
                .WithNone<RequiredCleanupTag>()
                .Build();
    
            state.RequireForUpdate(_query);
        }
    
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (visual, entity) in 
                     SystemAPI.Query<RefRW<CleanupCallbackHolder>>()
                         .WithNone<RequiredCleanupTag>()
                         .WithEntityAccess())
            {
                if (visual.ValueRW.Instance.IsValid()) 
                    visual.ValueRO.Instance.Value.Callback();
                
                ecb.RemoveComponent<CleanupCallbackHolder>(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}