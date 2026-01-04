using Unity.Entities;
using UnityEngine;

namespace App.EcsBridges
{
    public static partial class EcsBridge
    {
        public static Entity GetSingletonEntity<TSingleton>()
            where TSingleton: unmanaged, IComponentData
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
                return entity;
            }
            else
            {
                Debug.LogError($"Cant find singleton component: [{typeof(TSingleton)}]");
                return default;
            }
        }
    }
}