using App.Ecs.MoveDamping;
using App.Ecs.Moving;
using App.Ecs.Turrets.Deployment;
using Unity.Burst;
using Unity.Entities;

namespace App.Ecs.Turrets.Place
{
    public struct TurretStatePlaceTag : IComponentData
    {
        
    }
    
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct TurretSetDeploymentStateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretStatePlaceTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (moveSpeed, entity) in 
                     SystemAPI.Query<RefRO<MoveSpeed>>()
                         .WithAll<TurretStatePlaceTag, MoveDampingTag>()
                         .WithEntityAccess())
            {
                if (moveSpeed.ValueRO.Value == 0)
                {
                    ecb.RemoveComponent<TurretStatePlaceTag>(entity);
                    MoveDampingUtils.FullRemove(entity, ecb);
                    ecb.AddComponent<TurretStateDeploymentTag>(entity);
                }
            }
        }
    }
}