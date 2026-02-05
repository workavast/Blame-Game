using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Turrets.Deployment
{
    public struct TurretDeploymentViewOwnerTag : IComponentData
    {
    }
    
    public struct TurretStateDeploymentViewHolder : IComponentData
    {
        public UnityObjectRef<TurretDeploymentView> Instance;
    }
    
    public partial class TurretDeploymentViewHolderInitSystem
        : ViewHolderInitializeSystem<TurretDeploymentViewOwnerTag, TurretDeploymentView, TurretStateDeploymentViewHolder>
    {
        protected override TurretStateDeploymentViewHolder CreateViewHolder(TurretDeploymentView view)
            => new() { Instance = view };
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(TurretDeploymentTimerUpdateSystem))]
    public partial struct TurretDeploymentViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretStateDeploymentViewHolder>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (deploymentTimer, viewHolder) in 
                     SystemAPI.Query<RefRO<TurretDeploymentTimer>, RefRW<TurretStateDeploymentViewHolder>>())
            {
                viewHolder.ValueRO.Instance.Value.SetDeployTime(deploymentTimer.ValueRO.Value/deploymentTimer.ValueRO.TargetValue);
            }
        }
    }
}