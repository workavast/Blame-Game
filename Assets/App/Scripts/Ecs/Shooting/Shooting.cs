using App.Ecs.Shooting.Ammo;
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
    
    public struct ShootingUtils
    {
        public static AmmoCapacity CreateAmmoCapacity(int capacity)
        {
            return new AmmoCapacity
            {
                DefaultValue = capacity, 
                Value = capacity
            };
        }
    }
}