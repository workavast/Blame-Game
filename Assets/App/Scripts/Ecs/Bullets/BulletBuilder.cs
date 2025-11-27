using App.Ecs.Attack;
using App.Ecs.Moving;
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
            RefRO<AttackDamageScale> damageScale)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * damageScale.ValueRO.Value});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration });
        }
        
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data, 
            RefRO<AttackDamageScale> damageScale, RefRO<AdditionalPenetration> additionalPenetration)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * damageScale.ValueRO.Value});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration + additionalPenetration.ValueRO.Value});
        }
        
        public static void Build(ref EntityCommandBuffer ecb, ref Entity bullet, RefRO<BulletInitialData> data, 
            RefRO<AttackDamageScale> damageScale, AttackDamageScale globalAttackDamageScale, RefRO<AdditionalPenetration> additionalPenetration)
        {
            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * (damageScale.ValueRO.Value + globalAttackDamageScale.Value)});
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = data.ValueRO.Penetration + additionalPenetration.ValueRO.Value});
        }
    }
}