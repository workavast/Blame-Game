using Unity.Entities;

namespace App.Ecs.Shooting.Ammo
{
    public struct AmmoCapacity : IComponentData
    {
        public int DefaultValue;
        public int Value;
    }
}