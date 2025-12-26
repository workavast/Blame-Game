using App.Ecs.AoeZones;
using App.Ecs.HealthOrbs.Orb;
using App.Ecs.Orbs;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.HealthOrbs.ConsumeZone
{
    public struct HealthConsumeZoneTag : IComponentData
    {
        
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct HealthConsumeZoneStartConsumeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<HealthConsumeZoneTag>();
            state.RequireForUpdate<HealthOrbTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            
            foreach (var (zoneTransform, radius) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<AoeZoneRadius>>()
                         .WithAll<HealthConsumeZoneTag>())
            {
                foreach (var (healthOrbTransform, healthOrbEntity) in SystemAPI
                             .Query<RefRO<LocalTransform>>()
                             .WithAll<HealthOrbTag>()
                             .WithNone<OrbConsumeTag, OrbConsumedTag>()
                             .WithEntityAccess())
                {
                    if (math.distance(zoneTransform.ValueRO.Position, healthOrbTransform.ValueRO.Position) <= radius.ValueRO.Value)
                        ecb.AddComponent<OrbConsumeTag>(healthOrbEntity);
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}