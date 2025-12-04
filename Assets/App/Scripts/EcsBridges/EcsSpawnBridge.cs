using App.Ecs.Spawning;
using Unity.Entities;
using UnityEngine;

namespace App.EcsBridges
{
    public static class EcsSpawnBridge
    {
        public static void Spawn(int prefabKey, Entity owner)
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return;
            }
            
            if (EcsBridge.GetBufferOfSingleton<SpawnerTag, SpawnRequest>(out var buffer)) 
                buffer.Add(new SpawnRequest() { Owner = owner, Key = prefabKey});
        }
    }
}