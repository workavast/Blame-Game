using App.Ecs.Health;
using Unity.Entities;

namespace App.Ecs.Death
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(ApplyDamageToHealth))]
    public partial class DeathSystemGroup : ComponentSystemGroup
    {
        
    }
}