using App.Ecs.Rockets;
using App.Ecs.Shooting;
using Unity.Entities;
using UnityEngine;

namespace App.Ecs.PlayerPerks.RocketLauncher
{
    public class RocketLauncherAuthoring : MonoBehaviour
    {
        [SerializeField] private RocketAuthoring rocketPrefab;
        [SerializeField] private int rocketsCount;
        [SerializeField] private float randomInterval;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float height;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float moveSpeed;
        
        private class Baker : Baker<RocketLauncherAuthoring>
        {
            public override void Bake(RocketLauncherAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new AdditionalProjectilesCount());
                
                AddComponent(entity, new RocketLauncherTag());
                AddComponent(entity, new RocketLauncherData()
                {
                    RocketPrefab = GetEntity(authoring.rocketPrefab, TransformUsageFlags.Dynamic),
                    RocketsCount = authoring.rocketsCount,
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