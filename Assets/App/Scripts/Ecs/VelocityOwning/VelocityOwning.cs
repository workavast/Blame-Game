using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace App.Ecs.VelocityOwning
{
    public struct VelocityViewOwner : IComponentData
    {

    }

    public struct VelocityViewHolder : IComponentData
    {
        public UnityObjectRef<VelocityView> Instance;
    }

    public partial class VelocityViewHolderInitSystem : ViewHolderInitializeSystem<VelocityViewOwner, VelocityView,
        VelocityViewHolder>
    {
        protected override VelocityViewHolder CreateViewHolder(VelocityView view)
            => new() { Instance = view };
    }

    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct VelocityViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<PhysicsVelocity, VelocityViewHolder>()
                .Build();

            state.RequireForUpdate(query);
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (physicsVelocity, viewHolder) in
                     SystemAPI.Query<RefRO<PhysicsVelocity>, RefRW<VelocityViewHolder>>())
            {
                var velocity = math.length(physicsVelocity.ValueRO.Linear);
                viewHolder.ValueRW.Instance.Value.SetVelocity(velocity);
            }
        }
    }
}