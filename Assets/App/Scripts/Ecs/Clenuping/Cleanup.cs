using Unity.Entities;

namespace App.Ecs.Clenuping
{
    public struct CleanUpTag : IComponentData { }
    
    public struct CleanupCallback : ICleanupComponentData
    {
        public UnityObjectRef<App.CleanupCallback> Instance;
    }
    
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct CleanupSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<CleanupCallback>()
                .WithNone<CleanUpTag>()
                .Build();
    
            state.RequireForUpdate(_query);
        }
    
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (visual, entity) in 
                     SystemAPI.Query<RefRW<CleanupCallback>>()
                         .WithNone<CleanUpTag>()
                         .WithEntityAccess())
            {
                if (visual.ValueRW.Instance.IsValid()) 
                    visual.ValueRO.Instance.Value.Callback();
                
                ecb.RemoveComponent<CleanupCallback>(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}