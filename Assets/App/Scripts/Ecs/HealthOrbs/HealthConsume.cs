using App.Ecs.Health;
using App.Ecs.HealthOrbs.Orb;
using App.Ecs.Orbs;
using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;

namespace App.Ecs.HealthOrbs
{
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(OrbsCheckConsumeOverSystem))]
    public partial struct ExpOrbsConsumeOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var maxHealth = SystemAPI.GetComponent<MaxHealth>(playerEntity);
            var currentHealth = SystemAPI.GetComponentRW<CurrentHealth>(playerEntity);

            foreach (var healthAmount in 
                     SystemAPI.Query<RefRO<HealthOrbAmount>>()
                         .WithAll<HealthOrbTag, OrbConsumedTag>())
            {
                var health = math.clamp(currentHealth.ValueRO.Value + healthAmount.ValueRO.Value, 0, maxHealth.Value);
                currentHealth.ValueRW.Value = health;
            }
        }
    }
}