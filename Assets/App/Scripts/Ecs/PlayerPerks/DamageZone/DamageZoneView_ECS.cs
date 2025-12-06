using App.Ecs.AoeZones;
using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.PlayerPerks.DamageZone
{
    public struct DamageZoneViewHolder : IComponentData
    {
        public UnityObjectRef<DamageZoneView> Instance;
    }
    
    public partial class DamageZoneViewHolderInitSystem
        : ViewHolderInitializeSystem<DamageZoneTag, DamageZoneView, DamageZoneViewHolder>
    {
        protected override DamageZoneViewHolder CreateViewHolder(DamageZoneView view)
            => new() { Instance = view };
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct DamageZoneViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DamageZoneTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, view, radius) in 
                     SystemAPI.Query<RefRO<LocalToWorld>, RefRO<DamageZoneViewHolder>, RefRO<AoeZoneRadius>>()
                         .WithAll<DamageZoneTag>())
            {
                view.ValueRO.Instance.Value.SetPosition(transform.ValueRO.Position);
                view.ValueRO.Instance.Value.SetRadius(radius.ValueRO.Value);
            }
        }
    }
}