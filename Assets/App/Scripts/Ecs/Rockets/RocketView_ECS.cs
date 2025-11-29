using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.Rockets
{
    public struct RocketViewHolder : IComponentData
    {
        public UnityObjectRef<RocketView> Instance;
    }
    
    public partial class RocketViewHolderInitSystem
        : ViewHolderInitializeSystem<RocketTag, RocketView, RocketViewHolder>
    {
        protected override RocketViewHolder CreateViewHolder(RocketView view)
            => new() { Instance = view };
    }
    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(ViewsInitializationSystemGroup))]
    public partial struct RocketViewExplosionRadiusInitializeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RocketTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (radiusSetFlag, viewHolder, explosionRadius)  in 
                     SystemAPI.Query<EnabledRefRW<RocketViewExplosionRadiusSetFlag>, RefRO<RocketViewHolder>, RefRO<RocketExplosionRadius>>()
                         .WithAll<RocketTag>())
            {
                viewHolder.ValueRO.Instance.Value.SetExplosionRadius(explosionRadius.ValueRO.Value);
                radiusSetFlag.ValueRW = false;
            }
        }
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct RocketViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RocketTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, viewHolder) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<RocketViewHolder>>()
                         .WithAll<RocketTag>())
            {
                viewHolder.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
            }
        }
    }
}