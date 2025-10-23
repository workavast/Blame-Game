using Unity.Entities;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(BeforeTransformPauseSimulationGroup))]
    [UpdateAfter(typeof(IndependentMoveSystemGroup))]
    public partial class DependentMoveSystemGroup : ComponentSystemGroup
    {
        
    }
}