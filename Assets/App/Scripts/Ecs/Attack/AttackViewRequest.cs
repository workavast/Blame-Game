using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Attack
{
    public struct AttackViewRequested : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct AttackViewInitRequired : IComponentData, IEnableableComponent
    {
        
    }
    
    [UpdateInGroup(typeof(InitOffSystemGroup))]
    public partial struct AttackViewRequestInitOffSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<AttackViewRequested, AttackViewInitRequired>()
                .Build();
            
            state.RequireForUpdate(query);
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (requestFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<AttackViewRequested>, EnabledRefRW<AttackViewInitRequired>>())
            {
                requestFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }
    
    [UpdateInGroup(typeof(AttackSystemGroup), OrderFirst = false, OrderLast = true)]
    public partial struct AttackViewRequestResetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackViewRequested>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var requestFlag in
                     SystemAPI.Query<EnabledRefRW<AttackViewRequested>>())
            {
                requestFlag.ValueRW = false;
            }
        }
    }
}