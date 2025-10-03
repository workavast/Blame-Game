using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks
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
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);

            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (data, bulletData, entity) in 
                     SystemAPI.Query<RefRO<StarShooterData>, RefRO<BulletInitialData>>()
                         .WithAll<StarShooterTag>()
                         .WithDisabled<ShootCooldown>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<ShootCooldown>(entity, true);

                var bulletsCount = data.ValueRO.BulletsCount;
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

                    var spawnRotation = quaternion.LookRotation(spawnDirection, new float3(0, 1, 0));
                    var bullet = ecb.Instantiate(bulletData.ValueRO.BulletPrefab);
                    var bulletSpawnPosition = playerTransform.Position + new float3(0, bulletData.ValueRO.SpawnVerticalOffset, 0);
                    ecb.SetComponent(bullet, LocalTransform.FromPositionRotation(bulletSpawnPosition, spawnRotation));
                  
                    BulletBuilder.Build(ref ecb, ref bullet, bulletData);
                }
            }
        }
    }
}