using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Turrets.Deployment
{
    public struct TurretStateDeploymentTag : IComponentData
    {
        
    }
    
    public struct TurretDeploymentTimer : IComponentData
    {
        public float TargetValue;
        public float Value;
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct TurretDeploymentTimerUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretStateDeploymentTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var deploymentTimer in 
                     SystemAPI.Query<RefRW<TurretDeploymentTimer>>()
                         .WithAll<TurretStateDeploymentTag>())
            {
                deploymentTimer.ValueRW.Value += deltaTime;
            }
        }
    }
    
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct TurretSetReadyToUseStateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretStateDeploymentTag>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var ecbWorld = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbWorld.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (deploymentTimer, entity) in 
                     SystemAPI.Query<RefRO<TurretDeploymentTimer>>()
                         .WithAll<TurretStateDeploymentTag>()
                         .WithEntityAccess())
            {
                if (deploymentTimer.ValueRO.Value >= deploymentTimer.ValueRO.TargetValue)
                {
                    ecb.RemoveComponent<TurretStateDeploymentTag>(entity);
                    ecb.RemoveComponent<TurretDeploymentTimer>(entity);
                    ecb.RemoveComponent<TurretStateDeploymentViewHolder>(entity);
                    
                    TurretStatesUtils.SetReadyToUseState(entity, ref ecb);
                }
            }
        }
    }
}