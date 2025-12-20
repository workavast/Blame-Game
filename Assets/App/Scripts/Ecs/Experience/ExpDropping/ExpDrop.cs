using App.Ecs.Health.Death;
using App.Ecs.Moving;
using App.Ecs.Randomisation;
using App.Ecs.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Experience.ExpDropping
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
    
    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial struct ExpOrbCallSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExpGlobalDataTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var expEntity = SystemAPI.GetSingletonEntity<ExpGlobalDataTag>();
            var expOrbsRequestsBuffer = SystemAPI.GetBuffer<ExpOrbsDropRequest>(expEntity);

            foreach (var (transform, expOrbDropper) in
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<ExpOrbDropper>>()
                         .WithAll<DeathFlag>())
            {
                expOrbsRequestsBuffer.Add(new ExpOrbsDropRequest()
                {
                    OrbsCount = expOrbDropper.ValueRO.OrbsCount,
                    Position = transform.ValueRO.Position
                });
            }
        }
    }
    
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(DeathSystemGroup))]
    public partial struct ExpOrbSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExpGlobalDataTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            var expGlobalDataEntity = SystemAPI.GetSingletonEntity<ExpGlobalDataTag>();
            var randomHolder = SystemAPI.GetComponentRW<RandomHolder>(expGlobalDataEntity);
            var expOrbPrefabHolder = SystemAPI.GetComponent<ExpOrbPrefabHolder>(expGlobalDataEntity);
            var dropRequestsBuffer = SystemAPI.GetBuffer<ExpOrbsDropRequest>(expGlobalDataEntity);
            var dropImpulse = SystemAPI.GetComponent<ExpOrbDropImpulse>(expGlobalDataEntity);
            var verticalOffset = SystemAPI.GetComponent<ExpOrbDropHeight>(expGlobalDataEntity);

            for (var i = 0; i < dropRequestsBuffer.Length; i++)
            {
                var spawnExpOrbsRequest = dropRequestsBuffer[i];
                var spawnPoint = spawnExpOrbsRequest.Position;
                spawnPoint.y = verticalOffset.Value;

                for (var j = 0; j < spawnExpOrbsRequest.OrbsCount; j++)
                {
                    var direction = RandomPosition.GetDirection(ref randomHolder.ValueRW.Random);
                    var orb = ecb.Instantiate(expOrbPrefabHolder.OrbPrefab);

                    ecb.SetComponent(orb, LocalTransform.FromPosition(spawnPoint));
                    ecb.SetComponent(orb, new MoveSpeed() { Value = dropImpulse.Value });
                    ecb.SetComponent(orb, new MoveDirection() { Value = direction.xz });
                }
            }

            dropRequestsBuffer.Clear();
        }
    }
}