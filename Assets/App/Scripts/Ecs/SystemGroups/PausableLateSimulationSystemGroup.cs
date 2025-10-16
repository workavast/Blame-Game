using Unity.Entities;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial class PausableLateSimulationSystemGroup : ComponentSystemGroup
    {
        
    }
}