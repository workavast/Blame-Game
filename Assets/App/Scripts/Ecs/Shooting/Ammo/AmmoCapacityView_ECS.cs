using App.Ecs.Attack;
using App.Ecs.EntityViews;
using App.Ecs.Shooting.Ammo;
using Unity.Entities;

namespace App.Ecs.Shooting.Ammo
{
    public struct AmmoCapacityViewOwnerTag : IComponentData
    {
        
    }
    
    public struct AmmoCapacityViewIsVisibleTag : IComponentData, IEnableableComponent
    {
        
    }
    
    public struct AmmoCapacityViewHolder : IComponentData
    {
        public UnityObjectRef<AmmoCapacityView> Instance;
    }

    public partial class AmmoCapacityViewHolderInitSystem : ViewHolderInitializeSystem<AmmoCapacityViewOwnerTag, AmmoCapacityView, AmmoCapacityViewHolder>
    {
        protected override AmmoCapacityViewHolder CreateViewHolder(AmmoCapacityView view) 
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct AmmoCapacityViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AmmoCapacityViewHolder>();
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (capacity, viewHolder) in 
                     SystemAPI.Query<RefRO<AmmoCapacity>, RefRW<AmmoCapacityViewHolder>>()
                         .WithAll<AmmoCapacityViewIsVisibleTag>())
            {
                var capacityPercentage = (float)capacity.ValueRO.Value / capacity.ValueRO.DefaultValue;
                viewHolder.ValueRW.Instance.Value.SetCapacityPercentage(capacityPercentage);
            }
        }
    }
}