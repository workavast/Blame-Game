using Unity.Entities;

namespace App.Ecs
{
    public struct IsActiveTag : IComponentData { }
    
    public struct CleanupCallback : ICleanupComponentData
    {
        public UnityObjectRef<App.CleanupCallback> Instance;
    }
    
    public partial struct CleanupSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<CleanupCallback>()
                .WithNone<IsActiveTag>()
                .Build();
    
            state.RequireForUpdate(_query);
        }
    
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (visual, entity) in 
                     SystemAPI.Query<RefRW<CleanupCallback>>().WithNone<IsActiveTag>().WithEntityAccess())
            {
                visual.ValueRO.Instance.Value.Callback();
                
                ecb.RemoveComponent<CleanupCallback>(entity);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}