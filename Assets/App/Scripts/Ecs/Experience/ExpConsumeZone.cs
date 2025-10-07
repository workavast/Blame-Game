using App.Views;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace App.Ecs.Experience
{
    public struct ExpConsumeZoneTag : IComponentData
    {
        
    }
    
    public struct ExpConsumeZoneViewHolder : IComponentData
    {
        public UnityObjectRef<ExpConsumeZoneView> Instance;
    }
    
    public partial class ExpConsumeZoneViewInstallerSystem : ViewInstallerSystem<ExpConsumeZoneTag>
    {
        protected override void AddViewHolder(Entity entity, CleanupView instance, ref EntityCommandBuffer ecb) 
            => ecb.AddComponent(entity, new ExpConsumeZoneViewHolder() { Instance = instance as ExpConsumeZoneView });
    } 
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(AoeZonePositionUpdateSystem))]
    [UpdateAfter(typeof(AoeZoneSizeUpdateSystem))]
    public partial struct ExpConsumeZoneViewUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, view, radius) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<ExpConsumeZoneViewHolder>, RefRO<AoeZoneRadius>>()
                         .WithAll<ExpConsumeZoneTag>())
            {
                view.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                view.ValueRO.Instance.Value.SetRadius(radius.ValueRO.Value);
            }
        }
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(AoeZonePositionUpdateSystem))]
    [UpdateAfter(typeof(AoeZoneSizeUpdateSystem))]
    public partial struct ExpConsumeZoneStartConsumeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            
            foreach (var (zoneTransform, radius) in 
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<AoeZoneRadius>>()
                         .WithAll<ExpConsumeZoneTag>())
            {
                foreach (var (expOrbTransform, expOrbEntity) in SystemAPI
                             .Query<RefRO<LocalTransform>>()
                             .WithEntityAccess()
                             .WithAll<ExpOrbTag>()
                             .WithNone<ExpOrbIsConsumeTag>())
                {
                    if (math.distance(zoneTransform.ValueRO.Position, expOrbTransform.ValueRO.Position) <= radius.ValueRO.Value)
                        ecb.AddComponent<ExpOrbIsConsumeTag>(expOrbEntity);
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}