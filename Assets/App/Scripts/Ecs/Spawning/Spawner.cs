using Unity.Entities;
using UnityEngine;

namespace App.Ecs.Spawning
{
    public struct SpawnerTag : IComponentData
    {
        
    }
    
    public struct PrefabCell : IBufferElementData
    {
        public int Key;
        public Entity Prefab;
    }

    public struct SpawnRequest : IBufferElementData
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
            state.RequireForUpdate<SpawnerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (prefabsBuffer, requestsBuffer) in
                     SystemAPI.Query<DynamicBuffer<PrefabCell>, DynamicBuffer<SpawnRequest>>())
            {
                foreach (var request in requestsBuffer)
                {
                    if (TryGetPrefabEntity(prefabsBuffer, request.Key, out var prefabEntity))
                    {
                        var newEntity = ecb.Instantiate(prefabEntity);

                        if (request.Owner != Entity.Null)
                            ecb.AddComponent(newEntity, new Owner() { Value = request.Owner });
                    }
                    else
                        Debug.LogError($"You try spawn entity that not exist in spawn buffer: key [{request.Key}], owner [{request.Owner}]");
                }
                requestsBuffer.Clear();
            }
        }

        private bool TryGetPrefabEntity(DynamicBuffer<PrefabCell> buffer, int key, out Entity prefabEntity)
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