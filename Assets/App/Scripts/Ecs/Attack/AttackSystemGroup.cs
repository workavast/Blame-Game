using App.Ecs.Health;
using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.Attack
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(ApplyDamageToHealth))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial class AttackSystemGroup : ComponentSystemGroup
    {
        
    }
}