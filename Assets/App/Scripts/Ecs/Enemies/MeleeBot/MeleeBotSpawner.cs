using App.Ecs.Player;
using App.Ecs.SystemGroups;
using App.Ecs.Utils;
using Unity.Entities;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace App.Ecs.Enemies.MeleeBot
{
    public struct MeleeBotSpawnerTag : IComponentData
    {
    }
    
    public struct MeleeBotSpawnData : IComponentData
    {
        public Entity Prefab;
        public float Interval;
        public float Distance;
    }
    
    public struct MeleeBotSpawner : IComponentData
    {
        public float Timer;
        public Random Random;
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct MeleeBotSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemiesSpawnCountPerSecond>();
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
            
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (spawner, data, countPerSecond) in 
                     SystemAPI.Query<RefRW<MeleeBotSpawner>, RefRO<MeleeBotSpawnData>, RefRO<EnemiesSpawnCountPerSecond>>()
                         .WithAll<MeleeBotSpawnerTag>())
            {
                spawner.ValueRW.Timer -= deltaTime * countPerSecond.ValueRO.Value;
                if (spawner.ValueRO.Timer > 0)
                    continue;

                spawner.ValueRW.Timer = data.ValueRO.Interval;
                var kamikaze = ecb.Instantiate(data.ValueRO.Prefab);
                var spawnPoint = RandomPosition.GetPointOnRadius(playerPosition, data.ValueRO.Distance, ref spawner.ValueRW.Random);
                
                ecb.SetComponent(kamikaze, LocalTransform.FromPosition(spawnPoint));
            }
        }
    }
}