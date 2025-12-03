using App.Ecs.Attack;
using App.Ecs.Bullets;
using App.Ecs.Enemies;
using App.Ecs.Player;
using App.Ecs.Shooting;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks.Rifle
{
    public struct RifleTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct RifleSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<EnemyTag>();
            state.RequireForUpdate<RifleTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(playerEntity);
            var globalDamageScale = SystemAPI.GetComponent<AttackDamageScale>(playerEntity);

            var shootPoint = float3.zero;
            var distance = float.MaxValue;
            foreach (var enemyTransform in
                     SystemAPI.Query<RefRO<LocalToWorld>>()
                         .WithAll<EnemyTag>())
            {
                var curDistance = math.distance(playerTransform.Position, enemyTransform.ValueRO.Position);
                if (curDistance < distance)
                {
                    distance = curDistance;
                    shootPoint = enemyTransform.ValueRO.Position;
                }
            }
            
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            var direction = shootPoint - playerTransform.Position;
            var rotation = quaternion.LookRotation(direction, new float3(0, 1, 0));
            foreach (var (distanceReaction, data, damageScale
                         , additionalPenetration, attackViewRequest, entity) in
                     SystemAPI.Query<RefRO<ShootDistanceReaction>, RefRO<BulletInitialData>,
                            RefRO<AttackDamageScale>, RefRO<AdditionalPenetration>, EnabledRefRW<AttackViewRequested>>()
                         .WithAll<RifleTag>()
                         .WithDisabled<AttackCooldown, AttackViewRequested>()
                         .WithEntityAccess())
            {
                if (distance > distanceReaction.ValueRO.Value)
                    continue;

                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var bullet = ecb.Instantiate(data.ValueRO.BulletPrefab);
                var bulletSpawnPosition = playerTransform.Position + new float3(0, data.ValueRO.SpawnVerticalOffset, 0);
                ecb.SetComponent(bullet,
                    LocalTransform.FromPositionRotation(bulletSpawnPosition, rotation));
                
                BulletBuilder.Build(ref ecb, ref bullet, data, damageScale, globalDamageScale, additionalPenetration);
                
                attackViewRequest.ValueRW = true;
            }
        }
    }
}