using Unity.Entities;

namespace App.Ecs.Shooting
{
    public struct AdditionalProjectilesCount : IComponentData
    {
        public int Value;
    }
    
    public struct ShootDistanceReaction : IComponentData
    {
        public float Value;
    }
}