using App.Ecs.Attack;
using Unity.Entities;

namespace App.Ecs.Death
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(AttackSystemGroup))]
    public partial class DeathSystemGroup : ComponentSystemGroup
    {
        
    }
}