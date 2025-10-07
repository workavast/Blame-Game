using Unity.Entities;
using Unity.Physics.Systems;

namespace App.Ecs
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
    public partial class FixedBeforePhysicsPauseGroup : ComponentSystemGroup
    {
        
    }
}