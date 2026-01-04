using App.Ecs.Health.Death;
using App.Ecs.Orbs;
using App.Ecs.Randomisation;
using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.HealthOrbs.Dropping
{
    public struct HealthDropGlobalDataTag : IComponentData
    {
        
    }
    
    public struct HealthOrbDropChance : IComponentData
    {
        public float Value;
    }
    
    public struct HealthOrbDropper : IComponentData
    {
        public int OrbsCount;
    }
    
    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial struct HealthOrbCallSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<HealthDropGlobalDataTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var healthGlobalDataEntity = SystemAPI.GetSingletonEntity<HealthDropGlobalDataTag>();
            var healthOrbsRequestsBuffer = SystemAPI.GetBuffer<OrbsDropRequest>(healthGlobalDataEntity);
            var dropChance = SystemAPI.GetComponentRO<HealthOrbDropChance>(healthGlobalDataEntity);
            var randomHolder = SystemAPI.GetComponentRW<RandomHolder>(healthGlobalDataEntity);

            foreach (var (transform, healthOrbDropper) in
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<HealthOrbDropper>>()
                         .WithAll<DeathFlag>())
            {
                if (randomHolder.ValueRW.Random.NextFloat() <= dropChance.ValueRO.Value)
                {
                    healthOrbsRequestsBuffer.Add(new OrbsDropRequest()
                    {
                        OrbsCount = healthOrbDropper.ValueRO.OrbsCount,
                        Position = transform.ValueRO.Position
                    });
                }
            }
        }
    }
}