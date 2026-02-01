using App.Ecs.Attack;
using App.Ecs.Bullets;
using App.Ecs.Player;
using App.Ecs.Shooting;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks.StarShooter
{
    public struct StarShooterTag : IComponentData
    {
        
    }

    public struct StarShooterData : IComponentData
    {
        public float BulletsCount;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct StarShootSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<StarShooterTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            var globalDamageScale = SystemAPI.GetComponent<AttackDamageScale>(playerEntity);
            
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (data, additionalBulletsCount, 
                         bulletData, damageScale, penetration, 
                         attackViewRequest, entity) in 
                     SystemAPI.Query<RefRO<StarShooterData>, RefRO<AdditionalProjectilesCount>, 
                             RefRO<BulletInitialData>, RefRO<AttackDamageScale>, RefRO<BulletPenetration>,
                             EnabledRefRW<AttackViewRequested>>()
                         .WithAll<StarShooterTag>()
                         .WithDisabled<AttackCooldown, AttackViewRequested>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var bulletsCount = data.ValueRO.BulletsCount + additionalBulletsCount.ValueRO.Value;
                var angleStep = math.TAU / bulletsCount;
                var angle = 0f;
                
                for (int i = 0; i < bulletsCount; i++)
                {
                    var spawnDirection = new float3()
                    {
                        x = math.sin(angle),
                        y= 0f,
                        z = math.cos(angle),
                    };
                    angle += angleStep;

                    var bulletPrefab = bulletData.ValueRO.BulletPrefab;
                    var bulletPosition = playerTransform.Position + new float3(0, bulletData.ValueRO.SpawnVerticalOffset, 0);
                    var bulletRotation = quaternion.LookRotation(spawnDirection, new float3(0, 1, 0));
                    BulletBuilder.Build(ref ecb, bulletPrefab, bulletData, bulletPosition, bulletRotation, damageScale, globalDamageScale, penetration);
                }
                
                attackViewRequest.ValueRW = true;
            }
        }
    }
}