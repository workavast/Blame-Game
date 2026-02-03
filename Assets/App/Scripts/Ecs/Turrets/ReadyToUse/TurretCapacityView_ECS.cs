using App.Ecs.EntityViews;
using App.Ecs.Shooting;
using App.Ecs.SystemGroups;
using Unity.Entities;

namespace App.Ecs.Turrets.ReadyToUse
{
    public struct TurretCapacityViewHolder : IComponentData
    {
        public UnityObjectRef<TurretCapacityView> Instance;
    }

    public partial class TurretCapacityViewHolderInitSystem : ViewHolderInitializeSystem<TurretTag, TurretCapacityView, TurretCapacityViewHolder>
    {
        protected override TurretCapacityViewHolder CreateViewHolder(TurretCapacityView view) 
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    [UpdateAfter(typeof(TurretShootSystem))]
    public partial struct TurretCapacityViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretCapacityViewHolder>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (capacity, viewHolder) in 
                     SystemAPI.Query<RefRO<AmmoCapacity>, RefRW<TurretCapacityViewHolder>>()
                         .WithAll<TurretStateReadyToUseTag>())
            {
                var capacityPercentage = (float)capacity.ValueRO.Value / capacity.ValueRO.DefaultValue;
                viewHolder.ValueRW.Instance.Value.SetCapacityPercentage(capacityPercentage);
            }
        }
    }
}