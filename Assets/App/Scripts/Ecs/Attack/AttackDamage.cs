using Unity.Entities;

namespace App.Ecs.Attack
{
    public struct AttackDamage : IComponentData
    {
        public float Value;
        public float Scale;
    }
    
    public static class AttackUtils
    {
        public static float GetDamage(this AttackDamage damage, AttackDamage globalDamageScale) 
            => damage.Value * (damage.Scale + globalDamageScale.Scale);

        public static float GetDamage(RefRO<AttackDamage> damage, AttackDamage globalDamageScale) 
            => damage.ValueRO.Value * (damage.ValueRO.Scale + globalDamageScale.Scale);
    }
}