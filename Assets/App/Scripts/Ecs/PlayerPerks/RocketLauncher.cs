using App.Ecs.Utills;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks
{
    public struct RocketLauncherTag : IComponentData
    {
        
    }
    
    public struct RocketLauncherRandom : IComponentData
    {
        public Random Random;
    }
    
    public struct RocketLauncherData : IComponentData
    {
        public Entity RocketPrefab;
        public int RocketsCount;
        public float Damage;
        public float RandomInterval;
        public float MinDistance;
        public float MaxDistance;
        public float Height;
        public float ExplosionRadius;
        public float MoveSpeed;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct RocketLauncherSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
            
            var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (data, additionalProjectilesCount, damageScale, random, entity) in 
                     SystemAPI.Query<RefRO<RocketLauncherData>, RefRO<AdditionalProjectilesCount>, RefRO<DamageScale>, RefRW<RocketLauncherRandom>>()
                         .WithAll<RocketLauncherTag>()
                         .WithDisabled<ShootCooldown>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<ShootCooldown>(entity, true);

                var rocketsCount = data.ValueRO.RocketsCount + additionalProjectilesCount.ValueRO.Value;
                var damage = data.ValueRO.Damage * damageScale.ValueRO.Value;
                for (var i = 0; i < rocketsCount; i++)
                {
                    var spawnPoint = RandomPosition.GetPointInRadius(playerPosition, data.ValueRO.MinDistance, data.ValueRO.MaxDistance, ref random.ValueRW.Random);
                    spawnPoint += new float3(0, data.ValueRO.Height, 0);
                    
                    var rocketEntity = ecb.Instantiate(data.ValueRO.RocketPrefab);
                    var randomInterval = random.ValueRW.Random.NextFloat(0, data.ValueRO.RandomInterval);
                    
                    ecb.SetComponent(rocketEntity, LocalTransform.FromPosition(spawnPoint));
                    ecb.SetComponent(rocketEntity, new AttackDamage() { Value = damage });
                    ecb.SetComponent(rocketEntity, new RocketTargetHeight() { Value = playerPosition.y });
                    ecb.SetComponent(rocketEntity, new MoveSpeed() { Value = data.ValueRO.MoveSpeed });
                    ecb.SetComponent(rocketEntity, new RocketExplosionRadius() { Value = data.ValueRO.ExplosionRadius });
                    ecb.SetComponent(rocketEntity, new RocketAwaitTimer() { Value = randomInterval });
                }
            }
        }
    }
}