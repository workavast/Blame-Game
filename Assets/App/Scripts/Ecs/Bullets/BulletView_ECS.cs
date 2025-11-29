using App.Ecs.EntityViews;
using App.Ecs.SystemGroups;
using Unity.Entities;
using Unity.Transforms;

namespace App.Ecs.Bullets
{
    public struct BulletViewHolder : IComponentData
    {
        public UnityObjectRef<BulletView> Instance;
    }
    
    public partial class BulletViewHolderInitSystem : ViewHolderInitializeSystem<BulletTag, BulletView, BulletViewHolder>
    {
        protected override BulletViewHolder CreateViewHolder(BulletView view)
            => new() { Instance = view };
    }
    
    [UpdateInGroup(typeof(AfterTransformPausableSimulationGroup))]
    public partial struct BulletViewUpdateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BulletTag>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (view, transform) in 
                     SystemAPI.Query<RefRW<BulletViewHolder>, RefRO<LocalTransform>>()
                         .WithAll<BulletTag>())
            {
                view.ValueRW.Instance.Value.SetPosition(transform.ValueRO.Position);
                view.ValueRW.Instance.Value.SetRotation(transform.ValueRO.Rotation);
            }
        }
    }
}