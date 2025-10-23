using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Spawning
{
    public struct SpawnCell : IBufferElementData
    {
        public int Key;
        public Entity Prefab;
    }

    public struct SpawnRequest : IComponentData
    {
        public int Key;
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SpawnCell>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var spawnData = SystemAPI.GetSingletonBuffer<SpawnCell>();
            
            foreach (var (spawnRequest, entity) in 
                     SystemAPI.Query<RefRO<SpawnRequest>>()
                         .WithEntityAccess())
            {
                if (BufferContains(spawnData, spawnRequest.ValueRO.Key, out var prefabEntity))
                    ecb.Instantiate(prefabEntity);
                else
                    Debug.LogError($"You try spawn entity that not exist in spawn buffer: key [{spawnRequest.ValueRO.Key}]");
                
                ecb.DestroyEntity(entity);
            }
        }

        private bool BufferContains(DynamicBuffer<SpawnCell> buffer, int key, out Entity prefabEntity)
        {
            for (var i = 0; i < buffer.Length; i++)
                if (buffer[i].Key == key)
                {
                    prefabEntity = buffer[i].Prefab;
                    return true;
                }

            prefabEntity = Entity.Null;
            return false;
        }
    }
}