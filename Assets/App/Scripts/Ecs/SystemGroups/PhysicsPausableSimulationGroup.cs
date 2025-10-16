using Unity.Entities;
using Unity.Physics.Systems;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    public partial class PhysicsPausableSimulationGroup : ComponentSystemGroup
    {
        
    }
}