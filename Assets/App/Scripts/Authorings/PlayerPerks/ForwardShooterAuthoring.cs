using App.Ecs;
using App.Ecs.PlayerPerks;
using Unity.Entities;
using UnityEngine;

namespace App.Authorings.PlayerPerks
{
    public class ForwardShooterAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float spawnVerticalOffset;
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float shootPause;
        
        private class Baker : Baker<ForwardShooterAuthoring>
        {
            public override void Bake(ForwardShooterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new ForwardShooterTag());
                AddComponent(entity, new BulletInitialData()
                {
                    BulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                    SpawnVerticalOffset = authoring.spawnVerticalOffset,
                    Damage = authoring.damage,
                    MoveSpeed = authoring.moveSpeed,
                    ShootPause =  authoring.shootPause
                });
                AddComponent(entity, new ForwardShooterPause()
                {
                    Timer = authoring.shootPause
                });
            }
        }
    }
}