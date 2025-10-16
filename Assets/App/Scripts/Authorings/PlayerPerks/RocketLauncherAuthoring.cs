using App.Ecs;
using App.Ecs.PlayerPerks;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Authorings.PlayerPerks
{
    public class RocketLauncherAuthoring : MonoBehaviour
    {
        [SerializeField] private RocketAuthoring rocketPrefab;
        [SerializeField] private int rocketsCount;
        [SerializeField] private float damage;
        [SerializeField] private float interval;
        [SerializeField] private float randomInterval;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float height;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float moveSpeed;
        [SerializeField] private uint seed;
        
        private class Baker : Baker<RocketLauncherAuthoring>
        {
            public override void Bake(RocketLauncherAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new DefaultShootCooldown() { Timer = authoring.interval });
                AddComponent(entity, new ShootCooldown() { Timer = authoring.interval });
                AddComponent(entity, new FireRateScale());

                AddComponent(entity, new DamageScale());
                AddComponent(entity, new AdditionalProjectilesCount());
                
                AddComponent(entity, new RocketLauncherTag());
                AddComponent(entity, new RocketLauncherRandom() { Random =  Random.CreateFromIndex(authoring.seed)});
                AddComponent(entity, new RocketLauncherData()
                {
                    RocketPrefab = GetEntity(authoring.rocketPrefab, TransformUsageFlags.Dynamic),
                    RocketsCount = authoring.rocketsCount,
                    Damage = authoring.damage,
                    RandomInterval = authoring.randomInterval,
                    MinDistance = authoring.minDistance,
                    MaxDistance = authoring.maxDistance,
                    Height = authoring.height,
                    ExplosionRadius = authoring.explosionRadius,
                    MoveSpeed = authoring.moveSpeed
                });
            }
        }
    }
}