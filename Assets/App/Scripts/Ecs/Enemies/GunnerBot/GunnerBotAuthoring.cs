using App.Ecs.Bullets;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Enemies.GunnerBot
{
    public class GunnerBotAuthoring : MonoBehaviour
    {
        [SerializeField] private BulletAuthoring bulletPrefab;
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int penetration;
        [SerializeField] private float spawnVerticalOffset;
        [Space]
        [SerializeField] private float minOffset;
        [SerializeField] private float maxOffset;

        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float minTarget;
        [SerializeField] private float maxTarget;
        [SerializeField] private float shootCooldown;
        
        private class KamikazeBaker : Baker<GunnerBotAuthoring>
        {
            public override void Bake(GunnerBotAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new GunnerBotTag());
                AddComponent(entity, new GunnerBotOffsetData()
                {
                    MinOffset = authoring.minOffset,
                    MaxOffset = authoring.maxOffset
                });
                AddComponent(entity, new GunnerBotInZoneFlag());
                AddComponent(entity, new GunnerBotOffsetInitializedFlag());
                AddComponent(entity, new GunnerBotData()
                {
                    MinDistanceInternal = authoring.minDistance,
                    MaxDistanceInternal = authoring.maxDistance,
                    
                    MinTargetInternal = authoring.minTarget,
                    MaxTargetInternal = authoring.maxTarget,
                    
                    Offset = Random.Range(0f, 3f)
                });
                
                AddComponent(entity, new BulletInitialData()
                {
                    BulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                    Damage = authoring.damage,
                    MoveSpeed = authoring.moveSpeed,
                    Penetration = authoring.penetration,
                    SpawnVerticalOffset = authoring.spawnVerticalOffset
                });
                
                AddComponent(entity, new DefaultShootCooldown() {Timer = authoring.shootCooldown});
                AddComponent(entity, new ShootCooldown() {Timer = authoring.shootCooldown});
            }
        }
    }
}