using Unity.Entities;

namespace App.Ecs.Experience.ExpOrb
{
    public struct ExpOrbTag : IComponentData
    {
        
    }

    public struct ExpOrbAmount : IComponentData
    {
        public float Value;
    }
}