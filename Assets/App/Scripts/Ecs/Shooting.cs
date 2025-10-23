using Unity.Entities;

namespace App.Ecs
{
    public struct AdditionalProjectilesCount : IComponentData
    {
        public int Value;
    }
    
    public struct AdditionalPenetration : IComponentData
    {
        public int Value;
    }
    
    public struct ShootDistanceReaction : IComponentData
    {
        public float Value;
    }
}