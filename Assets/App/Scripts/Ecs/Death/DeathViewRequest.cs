using App.Ecs.Health;
using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Entities;

namespace App.Ecs.Death
{
    public struct DeathViewRequestedFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct DeathViewInitRequiredFlag : IComponentData, IEnableableComponent
    {
        
    }
    
    [UpdateInGroup(typeof(InitOffSystemGroup))]
    public partial struct DeathViewRequestInitOffSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (requestFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<DeathViewRequestedFlag>, EnabledRefRW<DeathViewInitRequiredFlag>>())
            {
                requestFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }
    
    [UpdateInGroup(typeof(DeathSystemGroup), OrderFirst = true, OrderLast = false)]
    public partial struct ActivateDeathRequestSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (health, deathViewActivateFlag) in
                     SystemAPI.Query<RefRW<CurrentHealth>, EnabledRefRW<DeathViewRequestedFlag>>()
                         .WithDisabled<DeathViewRequestedFlag>())
            {
                if (health.ValueRO.Value <= 0)
                    deathViewActivateFlag.ValueRW = true;
            }
        }
    }
    
    [UpdateInGroup(typeof(DeathSystemGroup), OrderFirst = false, OrderLast = true)]
    public partial struct DeathViewRequestResetSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var requestFlag in
                     SystemAPI.Query<EnabledRefRW<DeathViewRequestedFlag>>())
            {
                requestFlag.ValueRW = false;
            }
        }
    }
}