using Unity.Entities;

namespace App.Ecs.Health.Death
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(ApplyDamageToHealth))]
    public partial class DeathSystemGroup : ComponentSystemGroup
    {
        
    }
}