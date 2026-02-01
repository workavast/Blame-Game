using App.Ecs.Attack;
using App.Ecs.Bullets;
using App.Ecs.Player;
using App.Ecs.Shooting;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks.ForwardShooter
{
    public struct ForwardShooterTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct ForwardShootSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<ForwardShooterTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            var globalDamageScale = SystemAPI.GetComponent<AttackDamage>(playerEntity);
            
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (data, damage, 
                         penetration, attackViewRequest, entity) in 
                     SystemAPI.Query<RefRO<BulletInitialData>, RefRO<AttackDamage>,
                             RefRO<BulletPenetration>, EnabledRefRW<AttackViewRequested>>()
                         .WithAll<ForwardShooterTag>()
                         .WithDisabled<AttackCooldown, AttackViewRequested>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var bulletPrefab = data.ValueRO.BulletPrefab;
                var bulletPosition = playerTransform.Position + new float3(0, data.ValueRO.SpawnVerticalOffset, 0);
                BulletBuilder.Build(ref ecb, bulletPrefab, data, bulletPosition, playerTransform.Rotation, damage, globalDamageScale, penetration);

                attackViewRequest.ValueRW = true;
            }
        }
    }
}