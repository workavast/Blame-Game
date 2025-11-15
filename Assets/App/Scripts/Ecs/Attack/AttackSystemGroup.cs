using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.Attack
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial class AttackSystemGroup : ComponentSystemGroup
    {
        
    }
}