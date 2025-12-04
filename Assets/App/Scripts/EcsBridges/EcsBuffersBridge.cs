using Unity.Entities;
using UnityEngine;

namespace App.EcsBridges
{
    public static partial class EcsBridge
    {
        public static bool GetBufferOfSingleton<TSingleton, TBuffer>(out DynamicBuffer<TBuffer> component)
            where TSingleton: unmanaged, IComponentData
            where TBuffer: unmanaged, IBufferElementData
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("World is null");
                component = default;
                return false;
            }
            
            var query = world.EntityManager.CreateEntityQuery(typeof(TSingleton), typeof(TBuffer));
            if (query.TryGetSingletonBuffer<TBuffer>(out var buffer))
            {
                component = buffer;
                return true;
            }
            else
            {
                component = default;
                return false;
            }
        }
    }
}