using Unity.Entities;

namespace App.Ecs
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PausableInitializationSystemGroup : ComponentSystemGroup
    {
        
    }
}