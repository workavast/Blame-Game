using App.Ecs.Health.Death;
using Unity.Entities;

namespace App.Ecs.ResourcesDropping
{
    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial class DropSystemGroup : ComponentSystemGroup
    {
        
    }
}