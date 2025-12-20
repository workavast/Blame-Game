using App.Ecs.AoeZones;
using App.Ecs.Experience.ExpOrb;
using App.Ecs.Orbs;
using App.Ecs.SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Experience.ExpConsumeZone
{
    public struct ExpConsumeZoneTag : IComponentData
    {
        
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct ExpConsumeZoneStartConsumeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExpConsumeZoneTag>();
            state.RequireForUpdate<ExpOrbTag>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            
            foreach (var (zoneTransform, radius) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<AoeZoneRadius>>()
                         .WithAll<ExpConsumeZoneTag>())
            {
                foreach (var (expOrbTransform, expOrbEntity) in SystemAPI
                             .Query<RefRO<LocalTransform>>()
                             .WithAll<ExpOrbTag>()
                             .WithNone<OrbConsumeTag, OrbConsumedTag>()
                             .WithEntityAccess())
                {
                    if (math.distance(zoneTransform.ValueRO.Position, expOrbTransform.ValueRO.Position) <= radius.ValueRO.Value)
                        ecb.AddComponent<OrbConsumeTag>(expOrbEntity);
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}