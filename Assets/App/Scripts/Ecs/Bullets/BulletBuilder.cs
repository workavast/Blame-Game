using App.Ecs.Shooting;
using Unity.Entities;

namespace App.Ecs.Bullets
{
    public struct BulletBuilder
    {
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage });
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration });
        }
        
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data, 
            RefRO<DamageScale> damageScale)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * damageScale.ValueRO.Value});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration });
        }
        
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data, 
            RefRO<DamageScale> damageScale, RefRO<AdditionalPenetration> additionalPenetration)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * damageScale.ValueRO.Value});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration + additionalPenetration.ValueRO.Value});
        }
        
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data, 
            RefRO<DamageScale> damageScale, DamageScale globalDamageScale, RefRO<AdditionalPenetration> additionalPenetration)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * (damageScale.ValueRO.Value + globalDamageScale.Value)});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration + additionalPenetration.ValueRO.Value});
        }
    }
}