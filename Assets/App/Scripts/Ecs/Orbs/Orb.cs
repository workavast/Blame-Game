using App.Ecs.MoveDamping;
using App.Ecs.Moving;
using Unity.Burst;
using Unity.Entities;

namespace App.Ecs.Orbs
{
    public struct OrbTag : IComponentData
    {
        
    }

    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct OrbMoveDampingOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<OrbTag>();
            state.RequireForUpdate<MoveDampingTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (moveSpeed, entity) in 
                     SystemAPI.Query<RefRO<MoveSpeed>>()
                         .WithAll<OrbTag, MoveDampingTag>()
                         .WithNone<OrbConsumeTag>()
                         .WithEntityAccess())
            {
                if (moveSpeed.ValueRO.Value == 0)
                    MoveDampingUtils.FullRemove(entity, ecb);
            }
        }
    }
}