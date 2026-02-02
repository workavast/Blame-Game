using App.Ecs.Attack;
using App.Ecs.Bullets;
using App.Ecs.Enemies;
using App.Ecs.ExistTime;
using App.Ecs.Player;
using App.Ecs.Shooting;
using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Turrets
{
    public struct TurretTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct TurretSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<TurretTag>();
            state.RequireForUpdate<EnemyTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var globalDamageScale = SystemAPI.GetComponent<AttackDamage>(playerEntity);

            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (transform, distanceReaction, data,
                         damage, penetration, entity) in
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<ShootDistanceReaction>, RefRO<BulletInitialData>,
                             RefRO<AttackDamage>, RefRO<BulletPenetration>>()
                         .WithAll<TurretTag>()
                         .WithDisabled<AttackCooldown>()
                         .WithEntityAccess())
            {
                var shootPoint = float3.zero;
                var distance = float.MaxValue;
                foreach (var enemyTransform in
                         SystemAPI.Query<RefRO<LocalToWorld>>()
                             .WithAll<EnemyTag>())
                {
                    var curDistance = math.distance(transform.ValueRO.Position, enemyTransform.ValueRO.Position);
                    if (curDistance < distance)
                    {
                        distance = curDistance;
                        shootPoint = enemyTransform.ValueRO.Position;
                    }
                }
                
                if (distance > distanceReaction.ValueRO.Value)
                    continue;
                
                var direction = shootPoint - transform.ValueRO.Position;
                var rotation = quaternion.LookRotation(direction, new float3(0, 1, 0));
                
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var bulletPrefab = data.ValueRO.BulletPrefab;
                var bulletPosition = transform.ValueRO.Position + new float3(0, data.ValueRO.SpawnVerticalOffset, 0);
                BulletBuilder.Build(ref ecb, bulletPrefab, data, bulletPosition, rotation, damage, globalDamageScale, penetration);
            }
        }
    }
    
    [UpdateInGroup(typeof(PausableInitializationSystemGroup))]
    [UpdateAfter(typeof(ExistTimerSystem))]
    public partial struct BulletExistTimeOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<TurretTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (existTimer, entity) in 
                     SystemAPI.Query<RefRO<ExistTimer>>()
                         .WithAll<TurretTag>()
                         .WithEntityAccess())
            {
                if (existTimer.ValueRO.Value <= 0) 
                    ecb.DestroyEntity(entity);
            }
        }
    }
}