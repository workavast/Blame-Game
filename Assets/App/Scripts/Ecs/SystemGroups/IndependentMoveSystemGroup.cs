using Unity.Entities;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(BeforeTransformPauseSimulationGroup))]
    public partial class IndependentMoveSystemGroup : ComponentSystemGroup
    {
        
    }
}