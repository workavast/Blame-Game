using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    [UpdateBefore(typeof(LateSimulationSystemGroup))]
    public partial class AfterTransformPausableSimulationGroup : ComponentSystemGroup
    {
        
    }
}