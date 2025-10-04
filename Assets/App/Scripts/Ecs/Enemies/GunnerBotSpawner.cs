using App.Ecs.Utills;
using Unity.Entities;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace App.Ecs.Enemies
{
    public struct GunnerBotSpawnData : IComponentData
    {
        public Entity Prefab;
        public float Interval;
        public float Distance;
    }
    
    public struct GunnerBotSpawner : IComponentData
    {
        public float Timer;
        public Random Random;
    }

    public partial struct GunnerBotSpawnSystem : ISystem
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
            
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (spawner, data) in 
                     SystemAPI.Query<RefRW<GunnerBotSpawner>, RefRO<GunnerBotSpawnData>>())
            {
                spawner.ValueRW.Timer -= deltaTime;
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