using Unity.Entities;
using UnityEngine;

namespace App.EcsBridges
{
    public static partial class EcsBridge
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
        
        public static bool TryGetSingletonRW<TSingleton>(out RefRW<TSingleton> component)
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
            
            return query.TryGetSingletonRW(out component);
        }
        
        public static RefRW<TSingleton> GetSingletonRW<TSingleton>()
            where TSingleton: unmanaged, IComponentData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                return default;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton));
            if (query.TryGetSingletonRW<TSingleton>(out var component))
            {
                return component;
            }
            else
            {
                Debug.LogError($"Cant find singleton component: [{nameof(TSingleton)}]");
                return default;
            }
        }
    }
}