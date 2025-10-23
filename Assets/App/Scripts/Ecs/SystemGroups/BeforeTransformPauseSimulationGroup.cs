using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(VariableRateSimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial class BeforeTransformPauseSimulationGroup : ComponentSystemGroup
    {
        
    }
}