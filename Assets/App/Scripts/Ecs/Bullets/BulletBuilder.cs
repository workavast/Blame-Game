using App.Ecs.Attack;
using App.Ecs.Moving;
using App.Ecs.Shooting;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Bullets
{
    public struct BulletBuilder
    {
        public static Entity Build(ref EntityCommandBuffer ecb, Entity prefab, RefRO<BulletInitialData> data,
            float3 position, quaternion rotation,
            RefRO<BulletPenetration> penetration)
        {
            var bullet = ecb.Instantiate(prefab);
            ecb.SetComponent(bullet, LocalTransform.FromPositionRotation(position, rotation));

            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage });
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = penetration.ValueRO.Value });

            return bullet;
        }

        public static Entity Build(ref EntityCommandBuffer ecb, Entity prefab, RefRO<BulletInitialData> data,
            float3 position, quaternion rotation,
            RefRO<AttackDamageScale> damageScale,
            AttackDamageScale globalAttackDamageScale,
            RefRO<BulletPenetration> penetration)
        {
            var bullet = ecb.Instantiate(prefab);
            ecb.SetComponent(bullet, LocalTransform.FromPositionRotation(position, rotation));

            ecb.SetComponent(bullet, new AttackDamage() { Value = data.ValueRO.Damage * (damageScale.ValueRO.Value + globalAttackDamageScale.Value) });
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = penetration.ValueRO.Value });

            return bullet;
        }
    }
}