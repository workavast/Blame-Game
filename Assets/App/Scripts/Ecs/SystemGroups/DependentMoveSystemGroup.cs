using Unity.Entities;

namespace App.Ecs
{
    [UpdateInGroup(typeof(BeforeTransformPauseSimulationGroup))]
    [UpdateAfter(typeof(IndependentMoveSystemGroup))]
    public partial class DependentMoveSystemGroup : ComponentSystemGroup
    {
        
    }
}