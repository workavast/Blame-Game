using Unity.Entities;

namespace App.Ecs
{
    [UpdateInGroup(typeof(BeforeTransformPauseSimulationGroup))]
    public partial class IndependentMoveSystemGroup : ComponentSystemGroup
    {
        
    }
}