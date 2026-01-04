using App.Ecs.Health.Death;
using App.Ecs.Moving;
using App.Ecs.Randomisation;
using App.Ecs.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Orbs
{
    public struct OrbsDropGlobalDataTag : IComponentData
    {
        
    }
    
    public struct OrbsDropRequest : IBufferElementData
    {
        public int OrbsCount;
        public float3 Position;
    }
    
    public struct OrbDropHeight : IComponentData
    {
        public float Value;
    }
    
    public struct OrbDropImpulse : IComponentData
    {
        public float Value;
    }
    
    public struct OrbPrefabHolder : IComponentData
    {
        public Entity Prefab;
    }
    
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(DeathSystemGroup))]
    public partial struct ExpOrbSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<OrbsDropGlobalDataTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (randomHolder, orbPrefabHolder, 
                         dropImpulse, dropHeight, orbsDropRequests) in 
                     SystemAPI.Query<RefRW<RandomHolder>, RefRO<OrbPrefabHolder>, 
                         RefRO<OrbDropImpulse>, RefRO<OrbDropHeight>, DynamicBuffer<OrbsDropRequest>>())
            {
                for (var i = 0; i < orbsDropRequests.Length; i++)
                {
                    var spawnExpOrbsRequest = orbsDropRequests[i];
                    var spawnPoint = spawnExpOrbsRequest.Position;
                    spawnPoint.y = dropHeight.ValueRO.Value;

                    for (var j = 0; j < spawnExpOrbsRequest.OrbsCount; j++)
                    {
                        var direction = RandomPosition.GetDirection(ref randomHolder.ValueRW.Random);
                        var orb = ecb.Instantiate(orbPrefabHolder.ValueRO.Prefab);

                        ecb.SetComponent(orb, LocalTransform.FromPosition(spawnPoint));
                        ecb.SetComponent(orb, new MoveSpeed() { Value = dropImpulse.ValueRO.Value });
                        ecb.SetComponent(orb, new MoveDirection() { Value = direction.xz });
                    }
                }

                orbsDropRequests.Clear(); 
            }
        }
    }
}