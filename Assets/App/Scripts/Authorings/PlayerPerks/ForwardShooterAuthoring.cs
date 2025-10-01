using App.Ecs;
using App.Ecs.PlayerPerks;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings.PlayerPerks
{
    public class ForwardShooterAuthoring : MonoBehaviour
    {
        [SerializeField] private BulletAuthoring bulletPrefab;
        [SerializeField] private float spawnVerticalOffset;
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float shootPause;
        [SerializeField] private int penetration = 1;
        
        private class Baker : Baker<ForwardShooterAuthoring>
        {
            public override void Bake(ForwardShooterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new IsActiveTag());
              
                AddComponent(entity, new ForwardShooterTag());
                AddComponent(entity, new BulletInitialData()
                {
                    BulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                    SpawnVerticalOffset = authoring.spawnVerticalOffset,
                    Damage = authoring.damage,
                    MoveSpeed = authoring.moveSpeed,
                    Penetration = authoring.penetration
                });

                AddComponent(entity, new DefaultShootCooldown() { Timer = authoring.shootPause });
                AddComponent(entity, new ShootCooldown() { Timer = authoring.shootPause });
            }
        }
    }
}