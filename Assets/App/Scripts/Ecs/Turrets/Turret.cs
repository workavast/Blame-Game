using App.Ecs.Turrets.ReadyToUse;
using Unity.Burst;
using Unity.Entities;

namespace App.Ecs.Turrets
{
    public struct TurretTag : IComponentData
    {
        
    }
    
    public struct TurretCapacity : IComponentData
    {
        public int DefaultValue;
        public int Value;
    }
    
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct TurretOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<TurretTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (capacity, entity) in
                     SystemAPI.Query<RefRO<TurretCapacity>>()
                         .WithAll<TurretTag, TurretStateReadyToUseTag>()
                         .WithEntityAccess())
            {
                if (capacity.ValueRO.Value <= 0)
                    ecb.DestroyEntity(entity);
            }
        }
    }
}