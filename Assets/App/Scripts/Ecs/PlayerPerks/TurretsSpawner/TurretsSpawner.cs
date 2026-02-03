using App.Ecs.Attack;
using App.Ecs.Moving;
using App.Ecs.Player;
using App.Ecs.Randomisation;
using App.Ecs.Shooting;
using App.Ecs.SystemGroups;
using App.Ecs.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks.TurretsSpawner
{
    public struct TurretsSpawnerTag : IComponentData
    {
        
    }
    
    public struct TurretsSpawnerData : IComponentData
    {
        public Entity TurretPrefab;
        public int TurretsCount;
        public float Height;
        public float MinDropImpulse;
        public float MaxDropImpulse;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct TurretsSpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<TurretsSpawnerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
            var globalDamageScale = SystemAPI.GetComponent<AttackDamage>(playerEntity);

            var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (data, additionalProjectilesCount, damage, randomHolder, entity) in 
                     SystemAPI.Query<RefRO<TurretsSpawnerData>, RefRO<AdditionalProjectilesCount>, RefRO<AttackDamage>, 
                             RefRW<RandomHolder>>()
                         .WithAll<TurretsSpawnerTag>()
                         .WithDisabled<AttackCooldown>()
                         .WithEntityAccess())
            {
                SystemAPI.SetComponentEnabled<AttackCooldown>(entity, true);

                var turretsCount = data.ValueRO.TurretsCount + additionalProjectilesCount.ValueRO.Value;
                var resultDamage = damage.ValueRO.Value * (damage.ValueRO.Scale + globalDamageScale.Scale);
                for (var i = 0; i < turretsCount; i++)
                {
                    var spawnPoint = playerPosition;
                    spawnPoint += new float3(0, data.ValueRO.Height, 0);
                    var direction = RandomPosition.GetDirectionFloat2(ref randomHolder.ValueRW.Random);

                    var dropImpulse =
                        randomHolder.ValueRW.Random.NextFloat(data.ValueRO.MinDropImpulse, data.ValueRO.MaxDropImpulse);
                    
                    var turretEntity = ecb.Instantiate(data.ValueRO.TurretPrefab);
                    
                    ecb.SetComponent(turretEntity, LocalTransform.FromPosition(spawnPoint));
                    
                    ecb.SetComponent(turretEntity, new MoveSpeed() { Value = dropImpulse });
                    ecb.SetComponent(turretEntity, new MoveDirection() { Value = direction });
                    
                    ecb.SetComponent(turretEntity, new AttackDamage() { Value = resultDamage, Scale = 1f });
                }
            }
        }
    }
}