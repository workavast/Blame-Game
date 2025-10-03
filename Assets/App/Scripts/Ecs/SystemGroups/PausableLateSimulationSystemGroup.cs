using Unity.Entities;

namespace App.Ecs
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial class PausableLateSimulationSystemGroup : ComponentSystemGroup
    {
        
    }
}