using App.Ecs.Spawning;
using Unity.Entities;
using UnityEngine;

namespace App
{
    public static class EcsSpawner
    {
        public static void Spawn(int prefabKey, Entity owner)
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return;
            }
            
            var entityManager = world.EntityManager;

            var entity = entityManager.CreateEntity();
            
            entityManager.AddComponent<SpawnRequest>(entity);
            entityManager.SetComponentData(entity, new SpawnRequest() { Owner = owner, Key = prefabKey });
        }
    }
}