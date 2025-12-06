using Unity.Entities;

namespace App.Ecs.Attack
{
    public struct AttackDamage : IComponentData
    {
        public float Value;
    }

    public struct AttackDamageScale : IComponentData
    {
        public float Value;
    }
}