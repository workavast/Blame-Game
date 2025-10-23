using App.Ecs.Bullets;
using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Enemies.GunnerBot
{
    public struct GunnerBotTag : IComponentData
    {
        
    }

    public struct GunnerBotOffsetInitializedFlag : IComponentData, IEnableableComponent
    {
        
    }

    public struct GunnerBotOffsetData : IComponentData
    {
        public float MinOffset;
        public float MaxOffset;
    }
    
    public struct GunnerBotData : IComponentData
    {
        public float MinDistance => MinDistanceInternal + Offset;
        public float MaxDistance => MaxDistanceInternal + Offset;
        
        public float MinTarget => MinTargetInternal + Offset;
        public float MaxTarget => MaxTargetInternal + Offset;
        
        public float MinDistanceInternal;
        public float MaxDistanceInternal;
        
        public float MinTargetInternal;
        public float MaxTargetInternal;
        
        public float Offset;
    }

    public struct GunnerBotInZoneFlag : IComponentData, IEnableableComponent
    {
        
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct GunnerBotOffsetInitializeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SingletonRandom>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (offsetInitializedFlag, data, offsetData) in 
                     SystemAPI.Query<EnabledRefRW<GunnerBotOffsetInitializedFlag>, RefRW<GunnerBotData>, RefRW<GunnerBotOffsetData>>()
                         .WithAll<GunnerBotOffsetInitializedFlag, GunnerBotTag>())
            {
                var random = SystemAPI.GetSingletonRW<SingletonRandom>();
                data.ValueRW.Offset = random.ValueRW.Random.NextFloat(offsetData.ValueRO.MinOffset, offsetData.ValueRO.MaxOffset);
                offsetInitializedFlag.ValueRW = true;
            }
        }
    }
    
    [UpdateInGroup(typeof(DependentMoveSystemGroup))]
    [UpdateBefore(typeof(AutoMoveSystem))]
    public partial struct GunnerBotHoldDistanceSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            
            MoveToTargetZone(ref state, playerTransform);
            StayInZone(ref state, playerTransform);
        }
        
        private void MoveToTargetZone(ref SystemState state, LocalTransform playerTransform)
        {
            foreach (var (transform, moveDirection, data, inZone) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<MoveDirection>, RefRO<GunnerBotData>, 
                             EnabledRefRW<GunnerBotInZoneFlag>>()
                         .WithAll<GunnerBotTag>()
                         .WithNone<AutoMoveTag>()
                         .WithDisabled<GunnerBotInZoneFlag>())
            {
                var distance = math.distance(playerTransform.Position, transform.ValueRO.Position);

                if (distance < data.ValueRO.MinTarget)
                {
                    var directionFromPlayer = transform.ValueRO.Position - playerTransform.Position;
                    moveDirection.ValueRW.Value = math.normalize(directionFromPlayer.xz);
                    continue;
                }

                if (data.ValueRO.MaxTarget < distance)
                {
                    var directionToPlayer = playerTransform.Position - transform.ValueRO.Position;
                    moveDirection.ValueRW.Value = math.normalize(directionToPlayer.xz);
                    continue;
                }

                inZone.ValueRW = true;
                moveDirection.ValueRW.Value = float2.zero;
            }
        }

        private void StayInZone(ref SystemState state, LocalTransform playerTransform)
        {
            foreach (var (transform, moveDirection, data, inZoneFlag) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRW<MoveDirection>, RefRO<GunnerBotData>, 
                             EnabledRefRW<GunnerBotInZoneFlag>>()
                         .WithAll<GunnerBotTag, GunnerBotInZoneFlag>()
                         .WithNone<AutoMoveTag>())
            {
                var distance = math.distance(playerTransform.Position, transform.ValueRO.Position);

                if (distance < data.ValueRO.MinDistance)
                {
                    var directionFromPlayer = transform.ValueRO.Position - playerTransform.Position;
                    moveDirection.ValueRW.Value = math.normalize(directionFromPlayer.xz);
                    inZoneFlag.ValueRW = false;
                    continue;
                }

                if (data.ValueRO.MaxDistance < distance)
                {
                    var directionToPlayer = playerTransform.Position - transform.ValueRO.Position;
                    moveDirection.ValueRW.Value = math.normalize(directionToPlayer.xz);
                    inZoneFlag.ValueRW = false;
                    continue;
                }
                
                moveDirection.ValueRW.Value = float2.zero;
            }            
        }
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct GunnerBotShootSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (transform, bulletData, entity) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<BulletInitialData>>()
                         .WithAll<GunnerBotTag, GunnerBotInZoneFlag>()
                         .WithDisabled<AttackCooldown>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);
                
                var bullet = ecb.Instantiate(bulletData.ValueRO.BulletPrefab);
                var bulletSpawnPosition = transform.ValueRO.Position + new float3(0, bulletData.ValueRO.SpawnVerticalOffset, 0);
                ecb.SetComponent(bullet,
                    LocalTransform.FromPositionRotation(bulletSpawnPosition, transform.ValueRO.Rotation));
                
                BulletBuilder.Build(ref ecb, ref bullet, bulletData);
            }
        }
    }
}