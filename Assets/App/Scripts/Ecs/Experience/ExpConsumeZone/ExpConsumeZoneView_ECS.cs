using App.Ecs.AoeZones;
using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.Experience.ExpConsumeZone
{
    public struct ExpConsumeZoneViewHolder : IComponentData
    {
        public UnityObjectRef<ExpConsumeZoneView> Instance;
    }
    
    public partial class ExpConsumeZoneViewHolderInitSystem
        : ViewHolderInitializeSystem<ExpConsumeZoneTag, ExpConsumeZoneView, ExpConsumeZoneViewHolder>
    {
        protected override ExpConsumeZoneViewHolder CreateViewHolder(ExpConsumeZoneView view)
            => new() { Instance = view };
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct ExpConsumeZoneViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExpConsumeZoneTag>();
        }

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
}