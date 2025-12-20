using App.Ecs.Experience.ExpOrb;
using App.Ecs.Orbs;
using App.Ecs.Player;
using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Experience
{
    public struct ExpGlobalDataTag : IComponentData
    {
        
    }

    public struct ExpScale : IComponentData
    {
        public float Value;
    }
    
    public struct PlayerExp : IComponentData
    {
        public float Value;
    }
    
    public struct ExpOrbPrefabHolder : IComponentData
    {
        public Entity OrbPrefab;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(OrbsCheckConsumeOverSystem))]
    public partial struct ExpOrbsConsumeOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<ExpGlobalDataTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var expEntity = SystemAPI.GetSingletonEntity<ExpGlobalDataTag>();
            var playerExp = SystemAPI.GetComponentRW<PlayerExp>(expEntity);
            
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var globalExpScale = SystemAPI.GetComponent<ExpScale>(playerEntity);

            foreach (var expAmount in 
                     SystemAPI.Query<RefRO<ExpOrbAmount>>()
                         .WithAll<ExpOrbTag, OrbConsumedTag>())
            {
                playerExp.ValueRW.Value += expAmount.ValueRO.Value * globalExpScale.Value;
            }
        }
    }
}