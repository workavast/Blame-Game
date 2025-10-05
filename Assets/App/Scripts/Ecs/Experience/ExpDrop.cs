using App.Ecs.Utills;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Experience
{
    public struct ExpOrbDropper : IComponentData
    {
        public int OrbsCount;
    }
    
    public struct ExpOrbsDropRequest : IBufferElementData
    {
        public int OrbsCount;
        public float3 Position;
    }
    
    public struct ExpOrbDropImpulse : IComponentData
    {
        public float Value;
    }

    public struct ExpOrbDropHeight : IComponentData
    {
        public float Value;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct ExpOrbDropSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExpTag>();
            state.RequireForUpdate<SingletonRandom>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            var random = SystemAPI.GetSingletonRW<SingletonRandom>().ValueRW.Random;

            var expEntity = SystemAPI.GetSingletonEntity<ExpTag>();
            var orbPrefabHolder = SystemAPI.GetComponent<ExpOrbPrefabHolder>(expEntity);
            var requestsBuffer = SystemAPI.GetBuffer<ExpOrbsDropRequest>(expEntity);
            var dropImpulse = SystemAPI.GetComponent<ExpOrbDropImpulse>(expEntity);
            var verticalOffset = SystemAPI.GetComponent<ExpOrbDropHeight>(expEntity);

            for (var i = 0; i < requestsBuffer.Length; i++)
            {
                var spawnExpOrbsRequest = requestsBuffer[i];
                var spawnPoint = spawnExpOrbsRequest.Position;
                spawnPoint.y = verticalOffset.Value;

                for (var j = 0; j < spawnExpOrbsRequest.OrbsCount; j++)
                {
                    var direction = RandomPosition.GetDirection(ref random);
                    var orb = ecb.Instantiate(orbPrefabHolder.OrbPrefab);

                    ecb.SetComponent(orb, LocalTransform.FromPosition(spawnPoint));
                    ecb.SetComponent(orb, new MoveSpeed() { Value = dropImpulse.Value });
                    ecb.SetComponent(orb, new MoveDirection() { Value = direction.xz });
                }
            }

            requestsBuffer.Clear();
        }
    }
}