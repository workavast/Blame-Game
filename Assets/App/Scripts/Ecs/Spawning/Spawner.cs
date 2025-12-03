using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Spawning
{
    public struct PrefabCell : IBufferElementData
    {
        public int Key;
        public Entity Prefab;
    }

    public struct SpawnRequest : IComponentData
    {
        public int Key;
        public Entity Owner;
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PrefabCell>();
            state.RequireForUpdate<SpawnRequest>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var spawnData = SystemAPI.GetSingletonBuffer<PrefabCell>();
            
            foreach (var (spawnRequest, requestEntity) in 
                     SystemAPI.Query<RefRO<SpawnRequest>>()
                         .WithEntityAccess())
            {
                if (BufferContains(spawnData, spawnRequest.ValueRO.Key, out var prefabEntity))
                {
                    var entity = ecb.Instantiate(prefabEntity);

                    if (spawnRequest.ValueRO.Owner != Entity.Null) 
                        ecb.AddComponent(entity, new Owner() { Value = spawnRequest.ValueRO.Owner });
                }
                else
                    Debug.LogError($"You try spawn entity that not exist in spawn buffer: key [{spawnRequest.ValueRO.Key}]");
                
                ecb.DestroyEntity(requestEntity);
            }
        }

        private bool BufferContains(DynamicBuffer<PrefabCell> buffer, int key, out Entity prefabEntity)
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