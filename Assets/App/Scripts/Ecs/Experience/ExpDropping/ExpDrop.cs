using App.Ecs.Health.Death;
using App.Ecs.Orbs;
using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.Experience.ExpDropping
{
    public struct ExpOrbDropper : IComponentData
    {
        public int OrbsCount;
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
            var expGlobalDataEntity = SystemAPI.GetSingletonEntity<ExpGlobalDataTag>();
            var expOrbsRequestsBuffer = SystemAPI.GetBuffer<OrbsDropRequest>(expGlobalDataEntity);

            foreach (var (transform, expOrbDropper) in
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<ExpOrbDropper>>()
                         .WithAll<DeathFlag>())
            {
                expOrbsRequestsBuffer.Add(new OrbsDropRequest()
                {
                    OrbsCount = expOrbDropper.ValueRO.OrbsCount,
                    Position = transform.ValueRO.Position
                });
            }
        }
    }
}