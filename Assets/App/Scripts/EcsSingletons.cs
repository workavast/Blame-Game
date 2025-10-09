using Unity.Entities;
using UnityEngine;

namespace App
{
    public static class EcsSingletons
    {
        public static bool Exist<TSingleton>()
            where TSingleton: unmanaged, IComponentData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return false;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton));
            
            return query.HasSingleton<TSingleton>();
        }
        
        public static bool TryGetSingletonRO<TSingleton>(out TSingleton component)
            where TSingleton: unmanaged, IComponentData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                component = default;
                return false;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton));
            
            return query.TryGetSingleton(out component);
        }

        public static TSingleton GetSingletonRO<TSingleton>()
            where TSingleton: unmanaged, IComponentData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return default;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton));
            if (query.TryGetSingleton<TSingleton>(out var component))
            {
                return component;
            }
            else
            {
                Debug.LogError($"Cant find singleton component: [{nameof(TSingleton)}]");
                return default;
            }
        }
        
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
    }
}