using App.Ecs.Player;
using App.Ecs.SystemGroups;
using App.Ecs.Utils;
using Unity.Entities;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace App.Ecs.Enemies.Spawning
{
    public struct EnemySpawnerTag : IComponentData
    {
    }
    
    public struct EnemySpawnerSpawnData : IComponentData
    {
        public Entity Prefab;
        public float Interval;
        public float Distance;
    }
    
    public struct EnemySpawner : IComponentData
    {
        public float Timer;
        public Random Random;
    }
    
    public struct EnemySpawnCountPerSecond : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial class EnemiesSpawningSimulationGroup : ComponentSystemGroup
    {
        
    }

    [UpdateInGroup(typeof(EnemiesSpawningSimulationGroup), OrderFirst = true, OrderLast = false)]
    public partial struct EnemySpawnerTimerTickSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemySpawnCountPerSecond>();
            state.RequireForUpdate<EnemySpawnerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (spawner, data, countPerSecond) in 
                     SystemAPI.Query<RefRW<EnemySpawner>, RefRO<EnemySpawnerSpawnData>, RefRO<EnemySpawnCountPerSecond>>()
                         .WithAll<EnemySpawnerTag>())
            {
                spawner.ValueRW.Timer -= deltaTime * countPerSecond.ValueRO.Value;
            }
        }
    }
    
    [UpdateInGroup(typeof(EnemiesSpawningSimulationGroup), OrderFirst = false, OrderLast = true)]
    public partial struct EnemySpawnerTimerResetSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (spawner, data) in 
                     SystemAPI.Query<RefRW<EnemySpawner>, RefRO<EnemySpawnerSpawnData>>()
                         .WithAll<EnemySpawnerTag>())
            {
                if (spawner.ValueRO.Timer <= 0) 
                    spawner.ValueRW.Timer = data.ValueRO.Interval;
            }
        }
    }
    
    [UpdateInGroup(typeof(EnemiesSpawningSimulationGroup))]
    public partial struct EnemySpawnerSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;

            foreach (var (spawner, data) in 
                     SystemAPI.Query<RefRW<EnemySpawner>, RefRO<EnemySpawnerSpawnData>>()
                         .WithAll<EnemySpawnerTag>())
            {
                if (spawner.ValueRO.Timer > 0) 
                    continue;

                var enemyEntity = ecb.Instantiate(data.ValueRO.Prefab);
                var spawnPoint = RandomPosition.GetPointOnRadius(playerPosition, data.ValueRO.Distance, ref spawner.ValueRW.Random);
                
                ecb.SetComponent(enemyEntity, LocalTransform.FromPosition(spawnPoint));
            }
        }
    }
}