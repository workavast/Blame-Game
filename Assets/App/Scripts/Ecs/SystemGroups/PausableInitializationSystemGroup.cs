using Unity.Entities;

namespace App.Ecs.SystemGroups
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PausableInitializationSystemGroup : ComponentSystemGroup
    {
        
    }
}