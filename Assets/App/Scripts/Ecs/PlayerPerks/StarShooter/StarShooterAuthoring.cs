using App.Ecs.Attack;
using App.Ecs.Bullets;
using App.Ecs.Shooting;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.StarShooter
{
    public class StarShooterAuthoring : MonoBehaviour
    {
        [SerializeField] private BulletAuthoring bulletPrefab;
        [SerializeField] private float spawnVerticalOffset;
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float bulletsCount;
        [SerializeField] private int penetration;
        
        private class Baker : Baker<StarShooterAuthoring>
        {
            public override void Bake(StarShooterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new StarShooterTag());
                AddComponent(entity, new StarShooterData()
                {
                    BulletsCount = authoring.bulletsCount
                });
                
                AddComponent(entity, new AdditionalProjectilesCount());
                AddComponent(entity, new DamageScale());
                AddComponent(entity, new AdditionalPenetration());
                
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