using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Attack
{
    public struct AttackRequested : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct AttackInitRequired : IComponentData, IEnableableComponent
    {
        
    }
    
    [UpdateInGroup(typeof(InitOffSystemGroup))]
    public partial struct AttackVfxInitOffSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (requestFlag, initializedFlag) in
                     SystemAPI.Query<EnabledRefRW<AttackRequested>, EnabledRefRW<AttackInitRequired>>())
            {
                requestFlag.ValueRW = false;
                initializedFlag.ValueRW = false;
            }
        }
    }
    
    [UpdateInGroup(typeof(AttackSystemGroup), OrderFirst = false, OrderLast = true)]
    public partial struct AttackResetSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var requestFlag in
                     SystemAPI.Query<EnabledRefRW<AttackRequested>>())
            {
                requestFlag.ValueRW = false;
            }
        }
    }
}