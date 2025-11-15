using Unity.Entities;

namespace App.Ecs.Death
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(ApplyDamageToHealth))]
    [UpdateBefore(typeof(DestroyDeadEntities))]
    public partial class DeathSystemGroup : ComponentSystemGroup
    {
        
    }
}