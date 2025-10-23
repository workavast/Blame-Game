using App.Ecs.Player;
using App.Ecs.Rockets;
using App.Ecs.SystemGroups;
using App.Ecs.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks.RocektLauncher
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
            var globalDamageScale = SystemAPI.GetComponent<DamageScale>(playerEntity);

            var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (data, additionalProjectilesCount, damageScale, random, entity) in 
                     SystemAPI.Query<RefRO<RocketLauncherData>, RefRO<AdditionalProjectilesCount>, RefRO<DamageScale>, RefRW<RocketLauncherRandom>>()
                         .WithAll<RocketLauncherTag>()
                         .WithDisabled<AttackCooldown>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var rocketsCount = data.ValueRO.RocketsCount + additionalProjectilesCount.ValueRO.Value;
                var damage = data.ValueRO.Damage * (damageScale.ValueRO.Value + globalDamageScale.Value);
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