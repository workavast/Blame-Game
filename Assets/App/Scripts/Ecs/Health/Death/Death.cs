using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Entities;

namespace App.Ecs.Health.Death
{
    public struct DeathFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct DeathInitRequiredFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    [UpdateInGroup(typeof(InitOffSystemGroup))]
    public partial struct DeathFlagInitOffSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<DeathFlag, DeathInitRequiredFlag>()
                .Build();
            state.RequireForUpdate(query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (requestFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<DeathFlag>, EnabledRefRW<DeathInitRequiredFlag>>())
            {
                requestFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }
    
    [UpdateInGroup(typeof(DeathSystemGroup), OrderFirst = true, OrderLast = false)]
    public partial struct CheckDeathSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<CurrentHealth>()
                .WithDisabled<DeathFlag>()
                .Build();
            state.RequireForUpdate(query);
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, deathViewActivateFlag) in
                     SystemAPI.Query<RefRO<CurrentHealth>, EnabledRefRW<DeathFlag>>()
                         .WithDisabled<DeathFlag>())
            {
                if (health.ValueRO.Value <= 0)
                    deathViewActivateFlag.ValueRW = true;
            }
        }
    }
    
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct DestroyDeadEntitiesSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<DeathFlag>()
                .Build();
            state.RequireForUpdate(query);
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (deathFlag, entity) in 
                     SystemAPI.Query<EnabledRefRO<DeathFlag>>()
                         .WithEntityAccess())
            {
                ecb.DestroyEntity(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }   
    }
}