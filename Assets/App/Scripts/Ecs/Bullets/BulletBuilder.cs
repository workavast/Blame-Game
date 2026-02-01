using App.Ecs.Attack;
using App.Ecs.Moving;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Bullets
{
    public struct BulletBuilder
    {
        public static Entity Build(ref EntityCommandBuffer ecb, Entity prefab, 
            RefRO<BulletInitialData> data,
            float3 position, quaternion rotation,
            RefRO<AttackDamage> damage,
            RefRO<BulletPenetration> penetration)
        {
            var bullet = ecb.Instantiate(prefab);
            ecb.SetComponent(bullet, LocalTransform.FromPositionRotation(position, rotation));

            ecb.SetComponent(bullet, new AttackDamage() { Value = damage.ValueRO.Value });
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = penetration.ValueRO.Value });

            return bullet;
        }

        public static Entity Build(ref EntityCommandBuffer ecb, Entity prefab, 
            RefRO<BulletInitialData> data,
            float3 position, quaternion rotation,
            RefRO<AttackDamage> damage,
            AttackDamage globalDamageScale,
            RefRO<BulletPenetration> penetration)
        {
            var bullet = ecb.Instantiate(prefab);
            ecb.SetComponent(bullet, LocalTransform.FromPositionRotation(position, rotation));

            ecb.SetComponent(bullet, new AttackDamage() { Value = damage.ValueRO.Value * (damage.ValueRO.Scale + globalDamageScale.Scale) });
            ecb.SetComponent(bullet, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
            ecb.SetComponent(bullet, new BulletPenetration() { Value = penetration.ValueRO.Value });

            return bullet;
        }
    }
}