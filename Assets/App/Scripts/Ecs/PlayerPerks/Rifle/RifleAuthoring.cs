using App.Ecs.Bullets;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.Rifle
{
    public class RifleAuthoring : MonoBehaviour
    {
        [SerializeField] private BulletAuthoring bulletPrefab;
        [SerializeField] private float spawnVerticalOffset;
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float shootPause;
        [SerializeField] private float distanceReaction;
        [SerializeField] private int penetration = 1;

        private class Baker : Baker<RifleAuthoring>
        {
            public override void Bake(RifleAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new RifleTag());
                AddComponent(entity, new ShootDistanceReaction() { Value = authoring.distanceReaction });

                AddComponent(entity, new DamageScale());
                AddComponent(entity, new AdditionalPenetration());
                
                AddComponent(entity, new DefaultAttackCooldown() { Timer = authoring.shootPause });
                AddComponent(entity, new AttackCooldown() { Timer = authoring.shootPause });
                AddComponent(entity, new AttackRateScale());

                AddComponent(entity, new BulletInitialData()
                {
                    BulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                    SpawnVerticalOffset = authoring.spawnVerticalOffset,
                    Damage = authoring.damage,
                    MoveSpeed = authoring.moveSpeed,
                    Penetration = authoring.penetration
                });
            }
        }
    }
}