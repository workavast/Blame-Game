using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace App.Ecs.PlayerPerks
{
    public struct MachineGunTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct MachineGunSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
            
            _query = state.GetEntityQuery(
                ComponentType.ReadWrite<LocalToWorld>(),
                ComponentType.ReadWrite<EnemyTag>()
            );
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalToWorld>(playerEntity);

            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            var enemiesEntities = _query.ToEntityArray(Allocator.Temp);
            var enemiesCount = enemiesEntities.Length;

            if (enemiesCount <= 0)
                return;

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

            var direction = shootPoint - playerTransform.Position;
            var rotation = quaternion.LookRotation(direction, new float3(0, 1, 0));
            
            foreach (var (distanceReaction, data, entity) in
                     SystemAPI.Query<RefRO<ShootDistanceReaction>, RefRO<BulletInitialData>>()
                         .WithAll<MachineGunTag>()
                         .WithDisabled<ShootCooldown>()
                         .WithEntityAccess())
            {
                if (distance > distanceReaction.ValueRO.Value)
                    continue;
                
                SystemAPI.SetComponentEnabled<ShootCooldown>(entity, true);

                var bullet = ecb.Instantiate(data.ValueRO.BulletPrefab);
                var bulletSpawnPosition = playerTransform.Position + new float3(0, data.ValueRO.SpawnVerticalOffset, 0);
                ecb.SetComponent(bullet,
                    LocalTransform.FromPositionRotation(bulletSpawnPosition, rotation));
                
                BulletBuilder.Build(ref ecb, ref bullet, data);
            }
        }
    }
}