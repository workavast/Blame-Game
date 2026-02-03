using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using App.Ecs.Turrets.ReadyToUse;
using Unity.Entities;

namespace App.Ecs.Turrets.Deployment
{
    public struct TurretStateDeploymentTag : IComponentData
    {
        
    }
    
    public struct TurretStateDeploymentViewHolder : IComponentData
    {
        public UnityObjectRef<TurretStateDeploymentView> Instance;
    }
    
    public struct TurretStateDeploymentTimer : IComponentData
    {
        public float TargetValue;
        public float Value;
    }
    
    public partial class TurretStateDeploymentViewHolderInitSystem
        : ViewHolderInitializeSystem<TurretStateDeploymentTag, TurretStateDeploymentView, TurretStateDeploymentViewHolder>
    {
        protected override TurretStateDeploymentViewHolder CreateViewHolder(TurretStateDeploymentView view)
            => new() { Instance = view };
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct TurretStateDeploymentViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretStateDeploymentTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (deploymentTimer, viewHolder) in 
                     SystemAPI.Query<RefRW<TurretStateDeploymentTimer>, RefRW<TurretStateDeploymentViewHolder>>()
                         .WithAll<TurretStateDeploymentTag>())
            {
                deploymentTimer.ValueRW.Value += deltaTime;
                viewHolder.ValueRO.Instance.Value.SetDeployPercentageTime(deploymentTimer.ValueRO.Value/deploymentTimer.ValueRO.TargetValue);
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
                     SystemAPI.Query<RefRO<TurretStateDeploymentTimer>>()
                         .WithAll<TurretStateDeploymentTag>()
                         .WithEntityAccess())
            {
                if (deploymentTimer.ValueRO.Value >= deploymentTimer.ValueRO.TargetValue)
                {
                    ecb.RemoveComponent<TurretStateDeploymentTag>(entity);
                    ecb.RemoveComponent<TurretStateDeploymentTimer>(entity);
                    ecb.RemoveComponent<TurretStateDeploymentViewHolder>(entity);
                    ecb.AddComponent<TurretStateReadyToUseTag>(entity);
                }
            }
        }
    }
}