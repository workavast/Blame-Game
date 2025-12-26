using Unity.Entities;

namespace App.Ecs.HealthOrbs.Orb
{
    public struct HealthOrbTag : IComponentData
    {
        
    }

    public struct HealthOrbAmount : IComponentData
    {
        public float Value;
    }
}