using Unity.Entities;
using UnityEngine;

namespace App.EcsBridges
{
    public static partial class EcsBridge
    {
        public static bool TryGetComponentOfSingletonRO<TSingleton, TComponent>(out TComponent component)
            where TSingleton: unmanaged, IComponentData
            where TComponent: unmanaged, IComponentData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                component = default;
                return false;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton));
            if (query.TryGetSingletonEntity<TSingleton>(out var entity))
            {
               component = world.EntityManager.GetComponentData<TComponent>(entity);
               return true;
            }
            else
            {
                component = default;
                return false;
            }
        }
        
        public static TComponent GetComponentOfSingletonRO<TSingleton, TComponent>()
            where TSingleton: unmanaged, IComponentData
            where TComponent: unmanaged, IComponentData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return default;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton));
            if (query.TryGetSingletonEntity<TSingleton>(out var entity))
            {
                return world.EntityManager.GetComponentData<TComponent>(entity);
            }
            else
            {
                Debug.LogError($"Cant find singleton component: [{nameof(TSingleton)}]");
                return default;
            }
        }
        
        public static bool TrySetComponentOfSingleton<TSingleton, TComponent>(TComponent component)
            where TSingleton: unmanaged, IComponentData
            where TComponent: unmanaged, IComponentData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return false;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton));
            if (query.TryGetSingletonEntity<TSingleton>(out var entity) && world.EntityManager.HasComponent<TComponent>(entity))
            {
                world.EntityManager.SetComponentData(entity, component);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}