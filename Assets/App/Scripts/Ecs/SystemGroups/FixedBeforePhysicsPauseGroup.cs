using Unity.Entities;
using Unity.Physics.Systems;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
    public partial class FixedBeforePhysicsPauseGroup : ComponentSystemGroup
    {
        
    }
}